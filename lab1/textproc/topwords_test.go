package textproc

import "testing"

func TestCountWords(t *testing.T) {
    // Setup a test with a known output
    // This would typically be a temporary file with known contents
    const testFilename = "test.txt"
    expectedCount := map[string]int{"word1": 2, "word2": 1}

    // Run the function
    wordCounts, err := CountWords(testFilename)
    if err != nil {
        t.Errorf("Error counting words: %v", err)
    }

    // Check the results
    for word, count := range expectedCount {
        if wordCounts[word] != count {
            t.Errorf("Expected %d occurrences of word '%s', got %d", count, word, wordCounts[word])
        }
    }
}
