

using LibraryManagement;
using DataInitialization.DataInitialization;
public class Program
{
    public static void Main(string[] args)
    {
        DataInitializating dA = new(); dA.CreateDatabase();  // This method creates a Datahase in C:/ModelLibraryDatabase

        Management m = new();

        m.Title("BENVENUTO NELLA NOSTRA BIBLIOTECA!");

        m.Info("Premi \"1\" + invio per effettuare il login, qualsiasi altra scelta + invio per uscire...");
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                var loginResult = m.Login();
                if (loginResult == 1)
                {
                    do
                    {
                        Console.WriteLine("Premi 1 per prenotare un libro, premi 2 per aggiungere un libro, 3 per modificarne uno, 4 per eliminarlo, 5 per cercare un libro, 6 per visualizzare la lista, 7 per verificare le prenotazioni attive di un determinato utente, 8 per verificare quali prenotazioni hai attive tu USER, premi 9 per restituire un libro, premi 10 per visualizzare tutte le prenotazioni attive, premi 0 per uscire.");
                        input = Console.ReadLine();
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
                            case "11":
                                m.ReadAllUserReservations(); break;
                            default: break;
                        }
                    }
                    while (input != "0");
                }

                else if (loginResult == 2)
                {
                    do
                    {
                        Console.WriteLine("Premi 1 per prenotare un libro, 2 per cercare un libro, 3 per visualizzare la lista, 4 per verificare le tue prenotazioni attive, premi 5 per restituire un libro, premi 0 per uscire.");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                m.ReserveABook(); break;
                            case "2":
                                m.SearchBooksByAllProperties(); break;
                            case "3":
                                m.GetAllBooks(); break;
                            case "4":
                                m.UserActiveReservations(); break;
                            case "5":
                                m.BorrowedBookReturn(); break;
                            default: break;
                        }
                    }
                    while (input != "0");
                    }
                else { break; }
                break;
            default: return;
        }
        m.Success("Arrivederci!");
    }
}