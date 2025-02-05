using LibraryManager.src.model;

public class Book : IBook{
    public string Title { get; private set; }
    public bool IsBorrowed { get; set; }

    public Book(string title)
    {
        Title = title;
        IsBorrowed = false;
    }
}
