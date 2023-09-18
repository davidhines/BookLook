namespace BookLook.Domain.Book
{
    public sealed class Book : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public IEnumerable<Page> Pages { get; set; } = Enumerable.Empty<Page>();

        public Book(string title, string author, IEnumerable<Page> pages)
        {
            Id = Guid.NewGuid();
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Pages = pages ?? throw new ArgumentNullException(nameof(pages));
        }

        private int? _wordCount;

        public int? WordCount
        {
            get
            {
                _wordCount ??= Pages.Sum(page => page.WordCount);

                return _wordCount;
            }
        }

        private int? _averageWordLength;

        public int? AverageWordLength
        {
            get
            {
                _averageWordLength ??= (int) Math.Round(Pages.Average(page => page.AverageWordLength));

                return _averageWordLength;
            }
        }

        private string? _longestWord;

        public string? LongestWord
        {
            get
            {
                _longestWord ??= Pages
                    .Select(page => page.LongestWord)
                    .OrderByDescending(word => word.Length)
                    .FirstOrDefault();

                return _longestWord;
            }
        }

        private int? _characterCount;

        public int? CharacterCount
        {
            get
            {
                _characterCount ??= Pages.Sum(page => page.CharacterCount);

                return _characterCount.Value;
            }
        }

        private IDictionary<string, int>? _wordFrequencies;

        public IDictionary<string, int>? WordFrequencies
        {
            get
            {
                if (_wordFrequencies == null)
                {
                    _wordFrequencies = new Dictionary<string, int>();

                    foreach (var page in Pages) 
                    {
                        var pageWordFrequencies = page.WordFrequencies;

                        if (pageWordFrequencies != null)
                        {
                            foreach (var keyValuePair in pageWordFrequencies)
                            {
                                if (_wordFrequencies.ContainsKey(keyValuePair.Key))
                                {
                                    _wordFrequencies[keyValuePair.Key] += keyValuePair.Value;
                                }
                                else
                                {
                                    _wordFrequencies[keyValuePair.Key] = keyValuePair.Value;
                                }
                            }
                        }
                    }
                }

                return _wordFrequencies;
            }
        }
    }
}
