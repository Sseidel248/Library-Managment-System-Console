/*
* Library Management System
* Using Null Object Design Pattern
* Exercise for C# course
* Sebastian Baumert @ 2025
*/

using System;
using LibraryManager.src.model;

class Program
{
    static int maxBorrowedBooks = 3;
    static int borrowedBooksCount = 0;
    static List<IBook> library = new List<IBook>();

    public static void Main()
    {
        string[] books = new string[5];
        while (true)
        {
            Console.WriteLine("\nLibrary Management System");
            Console.WriteLine("1. Add a Book");
            Console.WriteLine("2. Remove a Book");
            Console.WriteLine("3. Display Books");
            Console.WriteLine("4. Search for a Book");
            Console.WriteLine("5. Borrow a Book");
            Console.WriteLine("6. Return a Book");
            Console.WriteLine("7. Display Borrowed Books");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option: ");
            
            string choice = ReadUserInput();
            
            switch (choice)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    RemoveBook();
                    break;
                case "3":
                    DisplayBooks();
                    break;
                case "4":
                    SearchBooks();
                    break;
                case "5":
                    BorrowBook();
                    break;
                case "6":
                    ReturnBook();
                    break;
                case "7":
                    DisplayBorrowedBooks();
                    break;
                case "8":
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }

    public static string ReadUserInput(){
        // Null-coalescing operator (??) is used to assign an empty string if the input is null
        return Console.ReadLine() ?? "";
    }

    public static IBook FindBook(string title){
        return library.FirstOrDefault(b => b.Title == title) ?? NullBook.Instance;
    }

    public static void AddBook()
    {
        Console.Write("Enter book title to add: ");  
        string newTitle = ReadUserInput();
        IBook book = FindBook(newTitle);

        // Check if a book with the same title already exists in the library
        if (book != NullBook.Instance)
        {
            Console.WriteLine("Book already exists in the library.");
            return;
        }

        library.Add(new Book(newTitle));
        Console.WriteLine("Book added successfully!");
    }

    public static void RemoveBook()
    {
        Console.Write("Enter book title to remove: ");
        string removeBook = ReadUserInput();
        IBook book = FindBook(removeBook);
        
        // Check if a book with the same title already exists in the library
        if (book != NullBook.Instance)
        {
            library.Remove(book);
            Console.WriteLine("Book removed successfully!");
            return;
        }
        Console.WriteLine("Book not found in the library.");
    }

    public static void DisplayBooks()
    {
        Console.WriteLine("\nBooks in Library:");
        bool hasBooks = false;

        foreach (var book in library)
        {
            if (book != NullBook.Instance && !book.IsBorrowed)
            {
                Console.WriteLine("- " + book.Title);
                hasBooks = true;
            }
        }
        
        if (!hasBooks)
        {
            Console.WriteLine("No books available in the library.");
        }
    }

    public static void SearchBooks()
    {
        Console.Write("Enter book title to search: ");
        string searchTitle = ReadUserInput();
        IBook book = FindBook(searchTitle);
        
        // Check if a book with the same title already exists in the library
        if (book != NullBook.Instance)
        {
            Console.WriteLine("Book found in the library.");
            return;
        }
        Console.WriteLine("Book not found in the library.");
    }

    public static void BorrowBook()
    {
        if (borrowedBooksCount >= maxBorrowedBooks)
        {
            Console.WriteLine($"You have reached the borrowing limit ({maxBorrowedBooks}).");
            return;
        }
        
        Console.Write("Enter book title to borrow: ");
        string borrowTitle = ReadUserInput();
        IBook book = FindBook(borrowTitle);

        if (book != NullBook.Instance)
        {
            if (book.IsBorrowed)
            {
                Console.WriteLine("Book is already borrowed.");
                return;
            } 
            else if (book is Book validBook)
            {
                Console.WriteLine("Book borrowed successfully!");
                validBook.IsBorrowed = true;
                return;
            }
        }   
        Console.WriteLine("Book not found in the library.");
    }

    public static void ReturnBook()
    {
        bool existBurrowedBook = library.Any(b => b.IsBorrowed);
        if (!existBurrowedBook)
        {
            Console.WriteLine("No books currently borrowed.");
            return;
        }
        
        Console.Write("Enter book title to return: ");
        string returnTitle = ReadUserInput();
        IBook book = FindBook(returnTitle);    

        if (book != NullBook.Instance)
        {
            if (!book.IsBorrowed)
            {
                Console.WriteLine("Book is not borrowed.");
                return;
            } 
            else if (book is Book validBook)
            {
                Console.WriteLine("Book returned successfully!");
                validBook.IsBorrowed = false;
                return;
            }
        }
    }

    public static void DisplayBorrowedBooks()
    {
        Console.WriteLine("\nBorrowed Books:");
        bool hasBorrowedBooks = false;
        
        foreach (var book in library)
        {
            if (book != NullBook.Instance && book.IsBorrowed)
            {
                Console.WriteLine("- " + book.Title);
                hasBorrowedBooks = true;
            }
        }
        
        if (!hasBorrowedBooks)
        {
            Console.WriteLine("No books currently borrowed.");
        }
    }
}
