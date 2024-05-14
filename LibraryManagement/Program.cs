using LibraryManagement;
using BusinessLogic;
using DataInitialization.DataInitialization;
public class Program
{
    public static void Main(string[] args)
    {
       //  DataInitializating dA = new(); dA.CreateDatabase();  // Uncomment this method to restore the default database

        Management m = new();
        BLManager bL = new();

        m.Title("BENVENUTO NELLA NOSTRA BIBLIOTECA!");
        m.Login();

        Console.WriteLine("Premi 1 per prenotare un libro, premi 2 per aggiungere un libro, 3 per modificarne uno, 4 per eliminarlo, 5 per cercare un libro, 6 per visualizzare la lista, 7 per verificare le prenotazioni attive di un determinato utente, 8 per verificare quali prenotazioni hai attive tu USER, premi 9 per restituire un libro, premi 10 per visualizzare tutte le prenotazioni attive");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                m.ReserveABook(); break;
            case "2":
                m.AddOrUpdate(); break;
            case "3":
                m.UpdateBookProperties(); break;
            case "4":
                m.DeleteBook(); break;
            case "5":
                m.SearchBooksByAllProperties(); break;
            case "6":
                m.GetAllBooks(); break;
            case "7":
                m.CheckUserReservations(); break;
            case "8":
                m.UserActiveReservations(); break;
            case "9":
                m.BorrowedBookReturn(); break;
            case "10":
                m.GetActiveReservations(); break;
            default: break;
            case "11":
                m.ReadAllUserReservations(); break;
                // TODO Il menu è da sistemare dopo aver provato tutti i metodi
        }
    }
}