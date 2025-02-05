using LibraryManager.src.model;

public class NullBook : IBook{
    public string Title => "No Book Available";
    public bool IsBorrowed => false;
        
    private static readonly NullBook _instance = new NullBook();
    public static NullBook Instance => _instance;

    private NullBook() { }
}