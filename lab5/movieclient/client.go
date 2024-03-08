// Package main implements a client for MovieInfo service.
package main

import (
	"context"
	"log"
	"time"

	"github.com/VahidBabaey/CloudComputing/lab5/movieapi"
	"google.golang.org/grpc"
)

const (
	address = "localhost:50051"
)

func main() {
	// Set up a connection to the server.
	conn, err := grpc.Dial(address, grpc.WithInsecure(), grpc.WithBlock())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	defer conn.Close()
	
	//Creates a new client for the MovieInfo service using the established connection.
	c := movieapi.NewMovieInfoClient(conn)

	// Creates a context with a 5-second timeout. This context will be used in gRPC calls to ensure they don't run indefinitely. defer cancel() ensures the context is cancelled, freeing resources if the operation completes before the timeout.
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	// Defines a MovieData object with information about the movie "Inception". This data structure is defined in the protobuf file for the MovieInfo service.
	movieData := &movieapi.MovieData{
		Title:    "Inception",
		Year:     2010,
		Director: "Christopher Nolan",
		Cast:     []string{"Leonardo DiCaprio", "Ellen Page", "Tom Hardy"},
	}
	// Calls the SetMovieInfo method on the client c with the context ctx and the example movie data movieData. This method is defined in the MovieInfo service.
	_, err = c.SetMovieInfo(ctx, movieData)
	if err != nil {
		log.Fatalf("could not set movie info: %v", err)
	}
	log.Printf("Successfully set movie info for: %s", movieData.Title)

	// Calls the GetMovieInfo method on the client c with the context ctx and a MovieRequest object containing the title of the movie for which information is requested.
	r, err := c.GetMovieInfo(ctx, &movieapi.MovieRequest{Title: movieData.Title})
	if err != nil {
		log.Fatalf("could not get movie info: %v", err)
	}
	log.Printf("Movie Info for %s: %d %s %v", movieData.Title, r.GetYear(), r.GetDirector(), r.GetCast())
}
