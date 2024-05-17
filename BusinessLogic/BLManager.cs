using DataAccessLayer;
using ModelLibrary;
using System.Xml.Linq;

namespace BusinessLogic
{
    public class BLManager
    {
        DataAccess dA = new DataAccess();

        internal LoginResult result = LoginResult.GetInstance();

        public int Login(string username, string password)
        {
            var result = dA.Login(username, password);

            // Restituisci una stringa diversa a seconda del ruolo
            if (result.Role == "Admin")
            {
                return 1;
            }
            else if (result.Role == "User")
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
        public int AddOrUpdate(string title, string authorName, string authorSurname, string publishingHouse, uint quantity)
        {
            XElement foundBook = dA.FindABook(title, authorName, authorSurname, publishingHouse);

            if (foundBook != null)
            {
                dA.UpdateBook(title, authorName, authorSurname, publishingHouse, quantity);
                return 1;
            }
            else
            {
                dA.AddBook(title, authorName, authorSurname, publishingHouse, quantity);
                return 2;
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return dA.GetAllBooks().ToList();
        }

        public IEnumerable<Book> SearchBooksByTitle(string title)
        {
            return dA.SearchBooksByTitle(title);
        }

        public IEnumerable<Book> SearchBooksByAuthorName(string authorName)
        {
            return dA.SearchBooksByAuthorName(authorName);
        }

        public IEnumerable<Book> SearchBooksByAuthorSurname(string authorSurname)
        {
            return dA.SearchBooksByAuthorSurname(authorSurname);
        }

        public IEnumerable<Book> SearchBooksByPublishingHouse(string publishingHouse)
        {
            return dA.SearchBooksByPublishingHouse(publishingHouse);
        }

        public IEnumerable<Book> SearchBooksByAllProperties(string? title, string? authorName = null, string? authorSurname = null, string? publishingHouse = null)
        {
            return dA.SearchBooksByAllProperties(title, authorName, authorSurname, publishingHouse);
        }

        public bool UpdateBookProperties(string? title, string? authorName, string? authorSurname, string? publishingHouse, string? newTitle = null, string? newAuthorName = null, string? newAuthorSurname = null, string? newPublishingHouse = null)
        {
            return dA.UpdateBookProperties(title, authorName, authorSurname, publishingHouse, newTitle, newAuthorName, newAuthorSurname, newPublishingHouse);
        }

        public bool DeleteBook(string? title, string? authorName, string? authorSurname, string? publishingHouse)
        {
            return dA.DeleteBook(title, authorName, authorSurname, publishingHouse);
        }
        public Reservation ReserveABook(string? title, string? authorName = null, string? authorSurname = null, string? publishingHouse = null)
        {
            // verifica che la quantità di prenotazioni attive sia munore alla quantità disponibile
            if (dA.IsBookReservable(title, authorName, authorSurname, publishingHouse) == false) 
                return null;
            else
            return dA.ReserveABook(title, authorName, authorSurname, publishingHouse);
        }
        public List<string> UserActiveReservations()
        {
            return dA.UserActiveReservations(result.ID);
        }

        public bool BorrowedBookReturn(string? title, string? authorName = null, string? authorSurname = null, string? publishingHouse = null)
        {
            if (dA.BorrowedBookReturn(title, authorName, authorSurname, publishingHouse) == true) return true;
            else return false;
        }

        public List<string> CheckUserReservations(string userName)
        {
            int? userID = dA.FindUserIdByUsername(userName);

            List<string> activeReservationBooks = dA.UserActiveReservations(userID);

            return activeReservationBooks;
        }

        public List<(string Title, string Username)> GetActiveReservations()
        {

            List<(string Title, string Username)> reservationsList = dA.GetActiveReservations();

            return reservationsList;
        }
        public int[] ReadAllUserReservationsID(string? title = null, string? authorName = null, string? authorSurname = null, string? publishingHouse = null)
        {
            var intArray = dA.ReadAllUserReservationsID(title, authorName, authorSurname, publishingHouse);

            return intArray;
        }

        public Dictionary<int, string> ReadAllUserReservations(string? title = null, string? authorName = null, string? authorSurname = null, string? publishingHouse = null)
        {
            var usersAndReservations = dA.ReadAllUserReservations(title, authorName, authorSurname, publishingHouse);

            return usersAndReservations;
        }
    }
}
