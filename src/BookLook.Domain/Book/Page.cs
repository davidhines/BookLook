namespace BookLook.Domain.Book
{
    public sealed class Page
    {
        public int PageNumber { get; set; }
        public string Content { get; set; } = string.Empty;
        public int WordCount { get; set; }
        public int CharacterCount { get; set; }
        public int AverageWordLength {  get; set; }
        public string LongestWord {  get; set; }
        private List<string> TokenizedWords { get; set; }
        public IReadOnlyCollection<string> CommonGroupings { get; set; }
        public IReadOnlyDictionary<string, int> WordFrequencies { get; set; }

        public Page(string content, int pageNumber, List<string> tokenizedWords)
        {
            Content = content;
            PageNumber = pageNumber;
            TokenizedWords = tokenizedWords;
        }
    }
}
