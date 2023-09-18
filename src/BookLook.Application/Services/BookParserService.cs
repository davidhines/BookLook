using BookLook.Domain.Book;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace BookLook.Application.Services
{
    public sealed class BookParserService
    {
        private const string TITLE_DELIMITER = "Title:";
        private const string AUTHOR_DELIMITER = "Author:";
        private const string METADATA_END_DELIMITER = "*** START OF THIS PROJECT";
        private const int MAX_PAGE_LENGTH = 1000;
        private readonly Regex _wordRegex = new(@"\b[\w.?!,:;'\""-]+\b");

        public async Task ParseEmbeddedBooksAsync()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                var embeddedResources = assembly.GetManifestResourceNames();

                foreach (var embeddedResource in embeddedResources)
                {
                    var book = await ParseEmbeddedBookAsync(embeddedResource, assembly);
                }
            }
            catch (Exception ex) { }
        }

        private async Task<Domain.Book.Book?> ParseEmbeddedBookAsync(string embeddedResource, Assembly assembly)
        {
            Domain.Book.Book? book = null;

            try
            {
                using Stream stream = assembly.GetManifestResourceStream(embeddedResource);

                if (stream == null) return null;

                using StreamReader reader = new(stream);
                var title = string.Empty;
                var author = string.Empty;

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();

                    if (line.StartsWith(METADATA_END_DELIMITER))
                    {
                        break;
                    }

                    if (line.StartsWith(AUTHOR_DELIMITER))
                    {
                        author = line[AUTHOR_DELIMITER.Length..].Trim();
                    }

                    if (line.StartsWith(TITLE_DELIMITER))
                    {
                        title = line[TITLE_DELIMITER.Length..].Trim();
                    }
                }

                var pages = new List<Page>();
                var contentBuilder = new StringBuilder();
                int pageNumber = 1;

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    contentBuilder.AppendLine(line);

                    if (contentBuilder.Length >= MAX_PAGE_LENGTH)
                    {
                        var page = await CreatePageAsync(contentBuilder.ToString(), pageNumber++);
                        pages.Add(page);
                        contentBuilder.Clear();
                    }
                }

                book = new Domain.Book.Book(title, author, pages);
            }
            catch { }

            return book;
        }

        private Task<Page> CreatePageAsync(string pageContent, int pageNumber)
        {
            var tokenizedWords = _wordRegex
                .Matches(pageContent)
                .Cast<Match>()
                .Select(match => match.Value)
                .ToList();

            var wordCount = tokenizedWords.Count;

            var characterCount = Regex
                .Replace(pageContent, @"[\s\p{P}]", "")
                .Length;

            string longestWord = string.Empty;
            int totalWordLength = 0;
            var wordFrequencies = new Dictionary<string, int>();

            foreach (var word in tokenizedWords)
            {
                if (word.Length > longestWord.Length)
                {
                    longestWord = word;
                }

                totalWordLength += word.Length;

                if (wordFrequencies.ContainsKey(word))
                {
                    wordFrequencies[word]++;
                }
                else
                {
                    wordFrequencies[word] = 1;
                }
            }

            var page = new Page(pageContent, pageNumber, tokenizedWords)
            {
                WordCount = wordCount,
                AverageWordLength = totalWordLength / wordCount,
                LongestWord = longestWord,
                WordFrequencies = wordFrequencies,
                CharacterCount = characterCount
            };

            return Task.FromResult(page);
        }
    }
}
