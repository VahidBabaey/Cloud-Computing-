// Demonstration of channels with a chat application
// Copyright © 2016 Alan A. A. Donovan & Brian W. Kernighan.
// License: https://creativecommons.org/licenses/by-nc-sa/4.0/

// Chat is a server that lets clients chat with each other.

package main

import (
	"bufio"
	"fmt"
	"log"
	"net"
	"strconv"
)


type client struct {
	channel chan<- string // Channel to chat between clients.
	name    string        // Client's Nmae
}

var (
	entering = make(chan client)    
	leaving  = make(chan client)    
	messages = make(chan string)    // all incoming client messages
)

func main() {
	
	listener, err := net.Listen("tcp", "localhost:8000")
	if err != nil {
		log.Fatal(err) 
	}

	go broadcaster() 
	for {
		conn, err := listener.Accept() 
		if err != nil {
			log.Print(err) 
			continue
		}
		go handleConn(conn) 
	}
}

func broadcaster() {
	clients := make(map[client]bool) // all connected clients
	for {
		select {
		case msg := <-messages:
			for cli := range clients {
				cli.channel <- msg // broadcast message to all clients
			}

		case cli := <-entering:
			clients[cli] = true 
			var listofClients string
			for c := range clients {
				listofClients += c.name + ", "
			}
			cli.channel <- "The number of current clients: " + strconv.Itoa(len(clients)) + ",  " + "List of Current clients: " + listofClients 

		case cli := <-leaving:
			delete(clients, cli) 
			close(cli.channel)   
		}
	}
}


func handleConn(conn net.Conn) {
	ch := make(chan string) // outgoing client messages
	go clientWriter(conn, ch)

	who := conn.RemoteAddr().String()
	cli := client{channel: ch, name: who}

	ch <- "You are " + who
	messages <- who + " has arrived"
	entering <- cli

	input := bufio.NewScanner(conn)
	for input.Scan() {
		messages <- who + ": " + input.Text()
	}

	// NOTE: ignoring potential errors from input.Err()

	leaving <- cli
	messages <- who + " has left"
	if err := conn.Close(); err != nil {
		log.Println("closing connection:", err)
	}
}

func clientWriter(conn net.Conn, ch <-chan string) {
	for msg := range ch {
		fmt.Fprintln(conn, msg) // NOTE: ignoring network errors
	}
}
