using BusinessLogic;
using ModelLibrary;
using DataAccessLayer;
using System.Collections.Generic;
namespace LibraryManagement
{
    public class Management
    {
        BLManager bL = new();
        DataAccess dA = new();

        public void Title(string title)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(title + "\n");
            Console.ResetColor();
        }

        public void Info(string info)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(info + "\n");
            Console.ResetColor();
        }

        public void Success(string success)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(success + "\n");
            Console.ResetColor();
        }

        public void Error(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error + "\n");
            Console.ResetColor();
        }

        public int Login()
        {
            Title("Inserisci le tue credenziali...");
            Console.Write("Inserisci username: ");
            string? username = Console.ReadLine();
            Console.Write("Inserisci password: ");
            string? password = Console.ReadLine();
            var result = bL.Login(username, password);

            if (result == 1)
            {
                Success($"Benvenuto {username}! Ora avrai i tuoi privilegi da Admin!");
                return 1;
            }
            else if (result == 2)
            {
                Success($"Benvenuto {username}!");
                return 2;
            }
            else
            {
                Error("Credenziali non valide.");
                return 0;
            }
        }
        public void AddOrUpdate()
        {
            Console.Write("Inserisci titolo: ");
            string? title = Console.ReadLine();
            Console.Write("Inserisci nome autore: ");
            string? authorName = Console.ReadLine();
            Console.Write("Inserisci cognome autore: ");
            string? authorSurname = Console.ReadLine();
            Console.Write("Inserisci casa editrice: ");
            string? publishingHouse = Console.ReadLine();
            Console.Write("Inserisci quantità: ");
            string? quantity = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(authorName) || string.IsNullOrWhiteSpace(authorSurname) || string.IsNullOrWhiteSpace(publishingHouse) || string.IsNullOrWhiteSpace(quantity) || !uint.TryParse(quantity, out uint result) || result == 0)
            {
                Error("Uno o più valori da te inseriti non contengono alcun carattere, riprova");
            }
            else
            {
                var b = bL.AddOrUpdate(title, authorName, authorSurname, publishingHouse, result);

                if (b == 1)
                {
                    Info("Il libro da te inserito già esisteva, è stata aggiunta la nuova quantità a quelle precedenti");
                }
                else
                    Success("Libro aggiunto correttamente!");
            }
        }

        public void GetAllBooks()
        {
            IEnumerable<Book> booksList = bL.GetAllBooks();

            Console.WriteLine($"Ecco la lista completa dei libri:\n");

            foreach (Book book in booksList)
            {
                Console.WriteLine(book.Title);
            }
        }
        public void SearchBooksByTitle()
        {
            Title("Inserisci il titolo del libro stai cercando: ");
            string? title = Console.ReadLine();
            IEnumerable<Book> booksList = bL.SearchBooksByTitle(title);

            Info("Risultato/i trovato/i\n");

            foreach (Book book in booksList)
            {
                Console.WriteLine(book.Title + "\n");
            }
        }

        public void SearchBooksByAuthorName()
        {
            Title("Inserisci il nome dell'autore del libro che stai cercando: ");
            string? authorName = Console.ReadLine();
            IEnumerable<Book> booksList = bL.SearchBooksByAuthorName(authorName);

            Info("Risultato/i trovato/i\n");

            foreach (Book book in booksList)
            {
                Console.WriteLine(book.ToString() + "\n");
            }
        }

        public void SearchBooksByAuthorSurname()
        {
            Title("Inserisci il cognome dell'autore del libro che stai cercando: ");
            string? authorSurname = Console.ReadLine();
            IEnumerable<Book> booksList = bL.SearchBooksByAuthorSurname(authorSurname);

            Info("Risultato/i trovato/i\n");

            foreach (Book book in booksList)
            {
                Console.WriteLine(book.ToString() + "\n");
            }
        }

        public void SearchBooksByPublishingHouse()
        {
            Title("Inserisci il nome della casa editrice del libro che stai cercando: ");
            string? publishingHouse = Console.ReadLine();
            IEnumerable<Book> booksList = bL.SearchBooksByPublishingHouse(publishingHouse);

            Info("Risultato/i trovato/i\n");

            foreach (Book book in booksList)
            {
                Console.WriteLine(book.ToString() + "\n");
            }
        }
        public void SearchBooksByAllProperties()
        {
            Title("Inserisci il titolo del libro stai cercando: ");
            string? title = Console.ReadLine();

            Title("Inserisci il nome dell'autore del libro che stai cercando: ");
            string? authorName = Console.ReadLine();

            Title("Inserisci il cognome dell'autore del libro che stai cercando: ");
            string? authorSurname = Console.ReadLine();

            Title("Inserisci il nome della casa editrice del libro che stai cercando: ");
            string? publishingHouse = Console.ReadLine();

            IEnumerable<Book> booksList = bL.SearchBooksByAllProperties(title, authorName, authorSurname, publishingHouse);

            Info("Risultato/i trovato/i\n");

            foreach (Book book in booksList)
            {
                Console.WriteLine(book.ToString() + "\n");
            }
        }
        public void UpdateBookProperties()
        {
            Title("Inserisci il titolo del libro stai cercando: ");
            string? title = Console.ReadLine();

            Title("Inserisci il nome dell'autore del libro che stai cercando: ");
            string? authorName = Console.ReadLine();

            Title("Inserisci il cognome dell'autore del libro che stai cercando: ");
            string? authorSurname = Console.ReadLine();

            Title("Inserisci il nome della casa editrice del libro che stai cercando: ");
            string? publishingHouse = Console.ReadLine();

            Title("Inserisci il nuovo titolo del libro: ");
            string? newTitle = Console.ReadLine();

            Title("Inserisci il nome dell'autore: ");
            string? newAuthorName = Console.ReadLine();

            Title("Inserisci il cognome dell'autore: ");
            string? newAuthorSurname = Console.ReadLine();

            Title("Inserisci il nome della casa editrice: ");
            string? newPublishingHouse = Console.ReadLine();

            if (bL.UpdateBookProperties(title, authorName, authorSurname, publishingHouse, newTitle, newAuthorName, newAuthorSurname, newPublishingHouse) == true)
            {
                Success("Libro modificato correttamewnte");
            }
            else
            {
                Error("Spiacente, si è verificarto un errore");
            }
        }
        public (string, string, string, string) BookSearching(string? title, string? authorName, string? authorSurname, string? publishingHouse)
        {
            Console.Write("Inserisci titolo: ");
            title = Console.ReadLine();
            Console.Write("Inserisci nome autore: ");
            authorName = Console.ReadLine();
            Console.Write("Inserisci cognome autore: ");
            authorSurname = Console.ReadLine();
            Console.Write("Inserisci casa editrice: ");
            publishingHouse = Console.ReadLine();

            return (title, authorName, authorSurname, publishingHouse);
        }
        public void DeleteBook()
        {
            Console.Write("Inserisci titolo: ");
            string? title = Console.ReadLine();
            Console.Write("Inserisci nome autore: ");
            string? authorName = Console.ReadLine();
            Console.Write("Inserisci cognome autore: ");
            string? authorSurname = Console.ReadLine();
            Console.Write("Inserisci casa editrice: ");
            string? publishingHouse = Console.ReadLine();

            bool deleted = bL.DeleteBook(title, authorName, authorSurname, publishingHouse);

            if (deleted)
            {
                Success($"Il libro dal titolo {title} è stato eliminato!");
            }
            else Error("Spiacente, si è, verificato un errore, probabilmente uno o più valori da te inseriti non sono corretti. Inserisci le proprietà del libro rispettandone perfettamente campi e maiuscole. Se preferisci puoi aiutarti facendo una ricerca.");
        }
        public void AddOrUpdateBook()
        {
            Title("Che libro stai cercando? Inserisci il titolo: ");
            string? title = Console.ReadLine();

            Title("Inserisci il nome dell'autore del libro che stai cercando: ");
            string? authorName = Console.ReadLine();

            Title("Inserisci il cognome dell'autore del libro che stai cercando: ");
            string? authorSurname = Console.ReadLine();

            Title("Inserisci il nome della casa editrice del libro che stai cercando: ");
            string? publishingHouse = Console.ReadLine();

            if (bL.DeleteBook(title, authorName, authorSurname, publishingHouse) == true)
            {
                Success($"Il libro intitolato {title} è stato eliminato con successo!");
            }
        }
        public void ReserveABook()
        {
            (string title, string authorName, string authorSurname, string publishingHouse) = BookSearching(title = null, authorName = null, authorSurname = null, publishingHouse = null);

            if (bL.ReserveABook(title, authorName, authorSurname, publishingHouse) == null)
            {
                Error("Spiacente. Il risultato della tua ricerca potrebbe non aver trovato nessun risultato, potresti avere già una prenotazione attiva oppure non ci sono più libri disponibili nella nostra libreria!!");
            }
        }
        public void UserActiveReservations()
        {
            foreach (var reservation in bL.UserActiveReservations())
            {
                Console.WriteLine(reservation.ToString());
            }
        }

        public void BorrowedBookReturn()
        {
            (string title, string authorName, string authorSurname, string publishingHouse) = BookSearching(title = null, authorName = null, authorSurname = null, publishingHouse = null);

            bL.BorrowedBookReturn(title, authorName, authorSurname, publishingHouse);
        }
        public void CheckUserReservations()
        {
            Console.Write("Inserisci il nome dell'utente del quale vuoi visualizzare le prenotazioni attive: ");
            string userName = Console.ReadLine();
            List<string> reservationList = bL.CheckUserReservations(userName);

            Success($"L'utente {userName} ha le seguenti prenotazioni attive:");

            foreach (string reservation in reservationList)
            {
                Console.WriteLine($"L'utente {userName} ha le seguenti prenotazioni attive: ");
                Console.WriteLine(reservation);
            }
        }
        public void GetActiveReservations()
        {
            List<(string Title, string Username)> reservationsList = bL.GetActiveReservations();

            if (reservationsList.Count == 0)
            {
                Error("Non risultano prenotazioni attive");
            }
            else
            Success("Ecco la lista delle prenotazioni all'interno del Database:");

            foreach ((string Title, string Username) in reservationsList)
            {
                Console.WriteLine($"L'utente {Username} ha prenotato il libro {Title}");
            }
        }
        public void ReadAllUserReservationsID()
        {
            (string title, string authorName, string authorSurname, string publishingHouse) = BookSearching(title = null, authorName = null, authorSurname = null, publishingHouse = null);

            int[] userReservationID =  bL.ReadAllUserReservationsID(title, authorName, authorSurname, publishingHouse);

            foreach (int reservationID in userReservationID) { Console.WriteLine(userReservationID); }
        }
        public void ReadAllUserReservations()
        {
           (string title, string authorName, string authorSurname, string publishingHouse) = BookSearching(title = null, authorName = null, authorSurname = null, publishingHouse = null);

            Dictionary<int, string> usersAndReservations = bL.ReadAllUserReservations(title, authorName, authorSurname, publishingHouse);
           
            foreach(var d in usersAndReservations)
            {
                Console.WriteLine($"Libro prenotato da: {d.Value}");
            }
        }

    }
}



