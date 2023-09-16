namespace BookLook.Domain.Book
{
    public sealed class Book : IEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
    }
}
