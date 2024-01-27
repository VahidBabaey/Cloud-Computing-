package main

import (
    "bufio"
    "fmt"
    "os"
    "sort"
    "strings"
    "strconv"
)

// CountWords reads a file and returns a map of word counts
func CountWords(filename string) (map[string]int, error) {
    file, err := os.Open(filename)
    if err != nil {
        return nil, err
    }
    defer file.Close()

    counts := make(map[string]int)
    scanner := bufio.NewScanner(file)
    scanner.Split(bufio.ScanWords)

    for scanner.Scan() {
        word := strings.ToLower(scanner.Text())
        counts[word]++
    }

    if err := scanner.Err(); err != nil {
        return nil, err
    }

    return counts, nil
}

// printTopKWords prints the top K words by frequency
func printTopKWords(counts map[string]int, k int) {
    // Convert map to slice of key-value pairs
    type kv struct {
        Key   string
        Value int
    }

    var ss []kv
    for k, v := range counts {
        ss = append(ss, kv{k, v})
    }

    // Sort slice based on frequency
    sort.Slice(ss, func(i, j int) bool {
        return ss[i].Value > ss[j].Value
    })

    // Print top K words
    for i := 0; i < k && i < len(ss); i++ {
        fmt.Printf("%s: %d\n", ss[i].Key, ss[i].Value)
    }
}

// main is the entry point of the program
func main() {
    if len(os.Args) < 3 {
        fmt.Println("Usage: go run topwords.go <filename> <K>")
        os.Exit(1)
    }

    filename := os.Args[1]
    k, err := strconv.Atoi(os.Args[2])
    if err != nil {
        fmt.Println("Invalid number for K:", err)
        os.Exit(1)
    }

    counts, err := CountWords(filename)
    if err != nil {
        fmt.Println("Error reading file:", err)
        os.Exit(1)
    }

    printTopKWords(counts, k)
}

