using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Library
{
    enum InputType
    {
        Add = 1,
        Remove,
        View,
        Borrow,
        Return,
        Exist,
        WrongInput,
        InvalidOption
    }
    class Program
    {
       
        static List<Book> books = new List<Book>();
        static List<Borrower> persons = new List<Borrower>();
        
        public class Book
        {
            public int Book_Id { get; set; }
            public string BookName { get; set; }
            public string AuthorName { get; set; }
            public int Qty { get; set; }
        }
        class Borrower
        {
            public int borrowedBookID;
           public string name=string.Empty;
        }
        
        public static InputType GetChoiceInput()
        {
            Console.WriteLine("\t\t\t\t Library Management");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. View Book");
            Console.WriteLine("4. Borrow Book");
            Console.WriteLine("5. Return Book");
            Console.WriteLine("6. Exit");
            Console.Write("Provide your input=>");
            try
            {
                return (InputType)Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                return InputType.InvalidOption;
            }
        }//End GetChoiceInput
        
        static int BookId()
        {
            Console.Write("Enter Id of the Book=>");
            return Convert.ToInt32(Console.ReadLine());
        }
        
        static void BookDetails(out string bookName, out string authorName, out int qty)
        {
            //studentName = string.Empty;
            //marks = 0;
            Console.Write("Enter Book Name=>");
            bookName = Console.ReadLine();
            Console.Write("Enter Author Name=>");
            authorName = Console.ReadLine();
            Console.Write("Enter Qty=>");
            qty = Convert.ToInt32(Console.ReadLine());
        }//End BookDetails
        
        static void AddOperation()
        {
            int book_id = BookId();
            int qty = 0;
            string bname = string.Empty;
            string aname = string.Empty;
            
            
            BookDetails(out bname, out aname, out qty);
            Book book = GetExistingStudent(book_id);
            if (book == null)
            {
                books.Add(new Book()
                {
                    Qty = qty,
                    BookName = bname,
                    AuthorName = aname,
                    Book_Id = book_id
                });
                Console.WriteLine("Book Is Added Successfully!!!");
            }
            else
            {
                int finalqty = book.Qty + qty;
                Console.WriteLine("New Stock Added" + finalqty);
                book.Qty = finalqty;
            }
        }//End AddOperation
        
        static void RemoveOperation()
        {
            int book_id = BookId();
            Book book = GetExistingStudent(book_id);
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine("Book Detail Removed Successfully!!!");
            }
            else
            {
                Console.WriteLine("Book With {0} Id Does Not Exist", book_id);
            }
        }//End RemoveOperation
        
        static void ViewOperation()
        {
            foreach (Book book in books)
            {
                Console.WriteLine($"{book.Book_Id} - {book.BookName} - {book.AuthorName} - {book.Qty}");
            }
        }// End View Operation
        
        static Book GetExistingStudent(int book_id)
        {
            foreach (Book book in books)
            {
                if (book.Book_Id == book_id)
                {
                    return book;
                }
            }
            return null;
        }//End GetExistingStudent Operation
        
        static void BorrowOperation()
        {
            string bookName;
            Console.Write("Enter Book Name=>");
            bookName = Console.ReadLine();
            foreach (Book book in books)
            {
                if (book.BookName == bookName)
                {
                  Console.WriteLine($"{book.Book_Id} - {book.BookName} - {book.AuthorName} - {book.Qty} ");
                   
                }
                
            }
            Console.WriteLine("Choose A Book Of Your Choice : Enter Book ID");
            int Enteredid = Convert.ToInt32(Console.ReadLine());
            foreach (Book book1 in books)
            {
                /*Console.WriteLine("Choose a book of your choice : Enter Book ID");
                int Enteredid = Convert.ToInt32(Console.ReadLine());*/
                if (book1.Book_Id == Enteredid)
                {
                    if (book1.Book_Id == Enteredid && book1.Qty != 0)
                    {
                        Console.WriteLine("Book Is Available \n Please Enter Your Name=>");
                        string borrower = Console.ReadLine();
                        Console.WriteLine($"{borrower} Has Borrowed A Book With Id=> {book1.Book_Id} And Name=> {book1.BookName} ");
                        //in list borrower person
                        persons.Add(new Borrower()
                        {
                            borrowedBookID = Enteredid,
                            name = borrower
                        }) ;
                       
                        Console.WriteLine("Book Is Added Successfully!!!");
                        book1.Qty--;
                        
                    }
                    else
                    {
                        Console.WriteLine("The Book Is Currently Not In Stock");
                        
                    }
                    }
                else
                {
                    Console.WriteLine("We Do Not Have The Book You Are Asking For.!!!");
                }             
            }
            
        }//EndBorrow Operation
        
        static void Returnoperation()
        {
            Console.Write("Enter Your Name=>");
            string Borrow_name = Console.ReadLine();
            Book book = new Book();
            foreach(Borrower person in persons)
            {
                if (Borrow_name ==person.name)
                {
                    Console.WriteLine($"Borrower Name:{person.name} returned his/her book: and Borrowed book id {person.borrowedBookID}");
                }
            }
           
           
        }
        static void Main(string[] args)
        {
            while (true)
            {
                InputType choice = GetChoiceInput();
                if (choice == InputType.Exist)
                {
                    break;
                }
                switch (choice)
                {
                    case InputType.Add:
                        Console.WriteLine("You Have Selected \"Add\" Operation");
                        AddOperation();
                        break;
                    case InputType.Remove:
                        Console.WriteLine("You Have Selected \"Remove\" Operation");
                        RemoveOperation();
                        break;
                    case InputType.View:
                        Console.WriteLine("You Have Selected \"View\" Operation");
                        ViewOperation();
                        break;
                    case InputType.Borrow:
                        Console.WriteLine("You Have Selected \"Borrow\" Operation");
                        BorrowOperation();
                        break;
                    case InputType.Return:
                        Console.WriteLine("You Have Selected \"Return\" Operation");
                        Returnoperation();
                        break;
                    case InputType.Exist:
                        Console.WriteLine("You Have Selected \"Exit\" Operation");
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("You Have Provided Wrong Input. Please Try Again!!!");
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}