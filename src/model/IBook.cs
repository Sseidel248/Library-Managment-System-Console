namespace LibraryManager.src.model
{
    public interface IBook
    {
        string Title { get; }
        bool IsBorrowed { get; }
    }
}