// Package main implements a server for movieinfo service.
package main

import (
	"context"
	"log"
	"net"
	"strconv"
	"strings"

	"github.com/VahidBabaey/CloudComputing/lab5/movieapi"
	"google.golang.org/grpc"
)

const (
	port = ":50051"
)
//Defines a server struct that embeds the unimplemented server interface. 
// server is used to implement movieapi.MovieInfoServer
type server struct {
	movieapi.UnimplementedMovieInfoServer
}
//A mock database using a Go map to store movie information. Pre-populated with "Pulp Fiction" data.
// Map representing a database
var moviedb = map[string][]string{"Pulp fiction": []string{"1994", "Quentin Tarantino", "John Travolta,Samuel Jackson,Uma Thurman,Bruce Willis"}}

// Adds a new movie to the database. It extracts data from the MovieData message, logs the insertion, and returns a success status.
func (s *server) SetMovieInfo(ctx context.Context, in *movieapi.MovieData) (*movieapi.Status, error) {
	title := in.GetTitle()
	year := strconv.Itoa(int(in.GetYear()))
	director := in.GetDirector()
	cast := strings.Join(in.GetCast(), ",")
	
	// Store the movie data
	moviedb[title] = []string{year, director, cast}
	log.Printf("Inserted movie: %s", title)
	
	return &movieapi.Status{Code: "Success"}, nil
}

// Retrieves movie information from the database. It checks if the requested title exists, constructs a response based on the query, and handles missing or incorrect data gracefully.
func (s *server) GetMovieInfo(ctx context.Context, in *movieapi.MovieRequest) (*movieapi.MovieReply, error) {
	title := in.GetTitle()
	log.Printf("Received: %v", title)
	reply := &movieapi.MovieReply{}
	if val, ok := moviedb[title]; !ok { // Title not present in database
		return reply, nil
	} else {
		if year, err := strconv.Atoi(val[0]); err != nil {
			reply.Year = -1
		} else {
			reply.Year = int32(year)
		}
		reply.Director = val[1]
		cast := strings.Split(val[2], ",")
		reply.Cast = append(reply.Cast, cast...)

	}

	return reply, nil

}

func main() {
	lis, err := net.Listen("tcp", port)
	if err != nil {
		log.Fatalf("failed to listen: %v", err)
	}
	s := grpc.NewServer()
	movieapi.RegisterMovieInfoServer(s, &server{})
	if err := s.Serve(lis); err != nil {
		log.Fatalf("failed to serve: %v", err)
	}
}
