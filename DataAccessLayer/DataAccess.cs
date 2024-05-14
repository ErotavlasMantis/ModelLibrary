using ModelLibrary;
using System.Data;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataAccessLayer
{
    public class DataAccess
    {
        internal const string databaseXMLFilePath = "C:\\Users\\user\\Desktop\\ModelLibrary\\XMLDirectory\\Database.xml";

        internal LoginResult result = LoginResult.GetInstance();
        public XDocument LoadXml()
        {
            XDocument xmlDocument = XDocument.Load(databaseXMLFilePath);
            return xmlDocument;
        }

        public void SetXDocument(XDocument xmlDocument)
        {
            xmlDocument.Save(databaseXMLFilePath);
        }
        public string GetDatabasePath()
        {
            return databaseXMLFilePath;
        }

        public LoginResult Login(string username, string password)
        {
            // Trova l'utente corrispondente alle credenziali fornite
            XElement? user = LoadXml().Descendants("Users")
                                     .FirstOrDefault(u => (string)u.Element("UserName") == username && (string)u.Element("Password") == password);
            LoginResult result = LoginResult.GetInstance();
            if (user != null)
            {
                int userId = (int)user.Element("UserId");
                string userName = (string)user.Element("UserName");
                string role = (string)user.Element("Role");


                result.Success = true;
                result.ID = userId;
                result.Username = userName;
                result.Role = role;

                return result;
            }
            else
            {
                result.Success = false;
                return result;
            }
        }

        public XElement FindABook(string title, string authorName, string authorSurname, string publishingHouse)
        {
            XDocument xmlDocument = LoadXml();
            // Cerca un libro esistente con lo stesso titolo, autore e casa editrice
            XElement? foundBook = xmlDocument.Descendants("Books")
    .FirstOrDefault(book =>
    book.Element("Title") != null && book.Element("Title").Value == title &&
    book.Element("AuthorName") != null && book.Element("AuthorName").Value == authorName &&
    book.Element("AuthorSurname") != null && book.Element("AuthorSurname").Value == authorSurname &&
    book.Element("PublishingHouse") != null && book.Element("PublishingHouse").Value == publishingHouse);

            return foundBook;
        }
        public void AddBook(string title, string authorName, string authorSurname, string publishingHouse, uint quantity)
        {
            XDocument xmlDocument = LoadXml();
            XElement foundBook = FindABook(title, authorName, authorSurname, publishingHouse);

            // Se il libro non esiste, crealo
            int newBookId = 1;
            if (xmlDocument.Descendants("Books").Any())
            {
                var booksWithId = xmlDocument.Descendants("Books")
                                             .Where(book => book.Element("BookID") != null);
                if (booksWithId.Any())
                {
                    int lastBookId = booksWithId.Max(book => (int)book.Element("BookID"));
                    newBookId = lastBookId + 1;
                }
            }
            XElement newBook = new XElement("Books",
                new XElement("BookID", newBookId),
                new XElement("Title", title),
                new XElement("AuthorName", authorName),
                new XElement("AuthorSurname", authorSurname),
                new XElement("PublishingHouse", publishingHouse),
                new XElement("Quantity", quantity));
            xmlDocument.Root.Add(newBook);
            SetXDocument(xmlDocument);
        }
        public void UpdateBook(string title, string authorName, string authorSurname, string publishingHouse, uint quantity)
        {
            XDocument xmlDocument = LoadXml();
            XElement? foundBook = xmlDocument.Descendants("Books")
    .FirstOrDefault(book =>
    book.Element("Title") != null && book.Element("Title").Value == title &&
    book.Element("AuthorName") != null && book.Element("AuthorName").Value == authorName &&
    book.Element("AuthorSurname") != null && book.Element("AuthorSurname").Value == authorSurname &&
    book.Element("PublishingHouse") != null && book.Element("PublishingHouse").Value == publishingHouse);

            uint existingQuantity = uint.Parse(foundBook.Element("Quantity").Value);
            foundBook.Element("Quantity").Value = (existingQuantity + quantity).ToString();
            SetXDocument(xmlDocument);
        }
        public IEnumerable<Book> GetAllBooks()
        {
            XDocument xmlDocument = LoadXml();

            try
            {
                // Trova tutti gli elementi "Books" nel database
                IEnumerable<XElement> books = xmlDocument.Descendants("Books");

                // Estrai i dettagli di ciascun libro e crea una lista di oggetti Book
                IEnumerable<Book> bookList = books.Select(book => new Book
                {
                    BookID = (int)book.Element("BookID"),
                    Title = (string)book.Element("Title"),
                    AuthorName = (string)book.Element("AuthorName"),
                    AuthorSurname = (string)book.Element("AuthorSurname"),
                    PublishingHouse = (string)book.Element("PublishingHouse"),
                    Quantity = (uint)book.Element("Quantity")
                }).ToList();

                return bookList;
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Errore durante il recupero dei libri: " + ex.Message);
                return null;
            }
        }
        public IEnumerable<Book> SearchBooksByTitle(string title)
        {
            XDocument xmlDocument = LoadXml();
            try
            {
                // Effettua la ricerca dei libri per titolo
                var searchResults = xmlDocument.Descendants("Books")
                    .Where(book => ((string)book.Element("Title")).Contains(title, StringComparison.OrdinalIgnoreCase))
                    .Select(book => new Book
                    {
                        BookID = (int)book.Element("BookID"),
                        Title = (string)book.Element("Title"),
                        AuthorName = (string)book.Element("AuthorName"),
                        AuthorSurname = (string)book.Element("AuthorSurname"),
                        PublishingHouse = (string)book.Element("PublishingHouse"),
                        Quantity = (uint)book.Element("Quantity")
                    }).ToList();

                return searchResults;
            }
            catch (Exception ex)
            {
                //  Console.WriteLine("Errore durante la ricerca dei libri per titolo: " + ex.Message);
                return null;
            }
        }
        public IEnumerable<Book> SearchBooksByAuthorName(string authorName)
        {
            XDocument xmlDocument = LoadXml();
            try
            {
                // Effettua la ricerca dei libri per il nome dell'autore
                var searchResults = xmlDocument.Descendants("Books")
                    .Where(book => ((string)book.Element("AuthorName")).Contains(authorName, StringComparison.OrdinalIgnoreCase))
                    .Select(book => new Book
                    {
                        BookID = (int)book.Element("BookID"),
                        Title = (string)book.Element("Title"),
                        AuthorName = (string)book.Element("AuthorName"),
                        AuthorSurname = (string)book.Element("AuthorSurname"),
                        PublishingHouse = (string)book.Element("PublishingHouse"),
                        Quantity = (uint)book.Element("Quantity")
                    }).ToList();

                return searchResults;
            }
            catch (Exception ex)
            {
                //  Console.WriteLine("Errore durante la ricerca dei libri per nome autore: " + ex.Message);
                return null;
            }
        }
        public IEnumerable<Book> SearchBooksByAuthorSurname(string authorSurname)
        {
            XDocument xmlDocument = LoadXml();
            try
            {
                // Effettua la ricerca dei libri per il cognome dell'autore
                var searchResults = xmlDocument.Descendants("Books")
                    .Where(book => ((string)book.Element("AuthorSurname")).Contains(authorSurname, StringComparison.OrdinalIgnoreCase))
                    .Select(book => new Book
                    {
                        BookID = (int)book.Element("BookID"),
                        Title = (string)book.Element("Title"),
                        AuthorName = (string)book.Element("AuthorName"),
                        AuthorSurname = (string)book.Element("AuthorSurname"),
                        PublishingHouse = (string)book.Element("PublishingHouse"),
                        Quantity = (uint)book.Element("Quantity")
                    }).ToList();

                return searchResults;
            }
            catch (Exception ex)
            {
                //  Console.WriteLine("Errore durante la ricerca dei libri per nome autore: " + ex.Message);
                return null;
            }
        }
        public IEnumerable<Book> SearchBooksByPublishingHouse(string publishingHouse)
        {
            XDocument xmlDocument = LoadXml();
            try
            {
                // Effettua la ricerca dei libri per la casa editrice
                var searchResults = xmlDocument.Descendants("Books")
                    .Where(book => ((string)book.Element("PublishingHouse")).Contains(publishingHouse, StringComparison.OrdinalIgnoreCase))
                    .Select(book => new Book
                    {
                        BookID = (int)book.Element("BookID"),
                        Title = (string)book.Element("Title"),
                        AuthorName = (string)book.Element("AuthorName"),
                        AuthorSurname = (string)book.Element("AuthorSurname"),
                        PublishingHouse = (string)book.Element("PublishingHouse"),
                        Quantity = (uint)book.Element("Quantity")
                    }).ToList();

                return searchResults;
            }
            catch (Exception ex)
            {
                //  Console.WriteLine("Errore durante la ricerca dei libri per nome autore: " + ex.Message);
                return null;
            }
        }
        public IEnumerable<Book> SearchBooksByAllProperties(string? title = null, string? authorName = null, string? authorSurname = null, string? publishingHouse = null)
        {
            XDocument xmlDocument = LoadXml();

            // Effettua la ricerca dei libri in base alle proprietà specificate
            var searchResults = xmlDocument.Descendants("Books")
                .Where(book =>
                    (title == null || ((string)book.Element("Title")).Contains(title, StringComparison.OrdinalIgnoreCase)) &&
                    (authorName == null || ((string)book.Element("AuthorName")).Contains(authorName, StringComparison.OrdinalIgnoreCase)) &&
                    (authorSurname == null || ((string)book.Element("AuthorSurname")).Contains(authorSurname, StringComparison.OrdinalIgnoreCase)) &&
                    (publishingHouse == null || ((string)book.Element("PublishingHouse")).Contains(publishingHouse, StringComparison.OrdinalIgnoreCase)))
                .Select(book => new Book
                {
                    BookID = (int)book.Element("BookID"),
                    Title = (string)book.Element("Title"),
                    AuthorName = (string)book.Element("AuthorName"),
                    AuthorSurname = (string)book.Element("AuthorSurname"),
                    PublishingHouse = (string)book.Element("PublishingHouse"),
                    Quantity = (uint)book.Element("Quantity")
                }).ToList();

            return searchResults;
        }

        public (List<Book>, string) OneResultOnly(string? title = null, string? authorName = null, string? authorSurname = null, string? publishingHouse = null)
        {
            var booksList = SearchBooksByAllProperties(title, authorName, authorSurname, publishingHouse).ToList();
            if (booksList.Count == 1)
            {
                return (booksList, "Un risultato!");
            }

            else if (booksList.Count < 1)
            {
                return (booksList, "Nessun risultato");
            }

            else return (booksList, "Più di un risultato");
        }

        public bool UpdateBookProperties(string? title, string? authorName, string? authorSurname, string? publishingHouse, string? newTitle = null, string? newAuthorName = null, string? newAuthorSurname = null, string? newPublishingHouse = null)
        {
            XDocument xmlDocument = LoadXml();

            // Trova nodo titolo
            var bookTitle = xmlDocument.Descendants("Books")
            .FirstOrDefault(b => (string)b.Element("Title") == title);

            // Modifica il titolo del libro
            bookTitle.Element("Title").Value = newTitle;

            // Trova nodo nome autore
            var bookAuthorName = xmlDocument.Descendants("Books")
            .FirstOrDefault(b => (string)b.Element("AuthorName") == authorName);

            // Modifica il nome dell'autore
            bookAuthorName.Element("AuthorName").Value = newAuthorName;

            // Trova nodo cognome autore
            var bookAuthorSurname = xmlDocument.Descendants("Books")
            .FirstOrDefault(b => (string)b.Element("AuthorSurname") == authorSurname);

            // Modifica il cognome dell'autore
            bookAuthorSurname.Element("AuthorSurname").Value = newAuthorSurname;

            // Trova nodo casa editrice
            var bookPublishingHouse = xmlDocument.Descendants("Books")
            .FirstOrDefault(b => (string)b.Element("PublishingHouse") == publishingHouse);

            // Modifica il nome della casa editrice
            bookPublishingHouse.Element("PublishingHouse").Value = newPublishingHouse;


            // Salva le modifiche
            SetXDocument(xmlDocument);

            return true;
        }

        public bool DeleteBook(string? title, string? authorName, string? authorSurname, string? publishingHouse)
        {
            XDocument xmlDocument = LoadXml();

            // Cerca il libro da eliminare utilizzando il metodo di ricerca
            var bookToDelete = SearchBookByAllProperties(xmlDocument, title, authorName, authorSurname, publishingHouse);

            // Se il libro è stato trovato, procedi con l'eliminazione
            if (bookToDelete != null)
            {
                // Rimuovi l'elemento del libro dal documento XML
                bookToDelete.Remove();

                // Salva le modifiche nel documento XML
                SetXDocument(xmlDocument);

                return true;
            }
            else
            {
                // Restituiamo false se libro è stato trovato con le proprietà specificate.");
                return false;
            }
        }
        private XElement? SearchBookByAllProperties(XDocument xmlDocument, string? title, string? authorName, string? authorSurname, string? publishingHouse)
        {
            return xmlDocument.Descendants("Books")
                              .FirstOrDefault(book =>
                                  (title == null || (string)book.Element("Title") == title) &&
                                  (authorName == null || (string)book.Element("AuthorName") == authorName) &&
                                  (authorSurname == null || (string)book.Element("AuthorSurname") == authorSurname) &&
                                  (publishingHouse == null || (string)book.Element("PublishingHouse") == publishingHouse));
        }

        public (int, int) IDUserAndIDBookTuples(string? title, string? authorName = null, string? authorSurname = null, string? publishingHouse = null)
        {
            (List<Book> booksList, string returning) = OneResultOnly(title, authorName, authorSurname, publishingHouse);
            var bookID = booksList[0].BookID;
            var userID = result.ID;
            return (userID, bookID);
        }

        public Reservation ReserveABook(string? title, string? authorName = null, string? authorSurname = null, string? publishingHouse = null)
        {
            XDocument xmlDocument = LoadXml();

            (List<Book> booksList, string returning) = OneResultOnly(title, authorName, authorSurname, publishingHouse);
            int bookID = booksList[0].BookID;
            int userID = result.ID;

            var now = DateTime.Now;

            // Verifica se esiste già una prenotazione attiva con lo stesso UserID e BookID

            var exists = xmlDocument.Descendants("Reservation")
                .Where(r => (int)r.Element("UserID") == userID && (int)r.Element("BookID") == bookID)
                .Any(r => (DateTime)r.Element("EndDate") > DateTime.Now);

            if (exists)
            {
                // Se esiste, restituisci null o genera un'eccezione, creare un IF nella console per evitare eccezione
                return null;
            }
            int newReservationId = 1;
            if (xmlDocument.Descendants("Reservation").Any())
            {
                int lastReservationId = xmlDocument.Descendants("Reservation")
                                            .Where(res => res.Element("ID") != null)
                                            .Max(res => (int)res.Element("ID"));
                newReservationId = lastReservationId + 1;
            }
            // Impostazione date di inizio e fine prenotazione
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(30);
            XElement newReservation = new XElement("Reservation",
                new XElement("ID", newReservationId),
                new XElement("UserID", userID),
                new XElement("BookID", bookID),
                 new XElement("StartDate", startDate),
                 new XElement("EndDate", endDate));

            xmlDocument.Root.Add(newReservation);

            SetXDocument(xmlDocument);
            XmlSerializer serializer = new XmlSerializer(typeof(Reservation));

            // Usa un MemoryStream per deserializzare l'elemento XML
            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(newReservation.ToString())))
            {
                Reservation? reservationObject = (Reservation)serializer.Deserialize(memoryStream);
                // 'reservationObject' è ora un oggetto prenotazione ricreato dall'XElement
                return reservationObject;
            }
        }
        public void BorrowedBookReturn(string? title, string? authorName = null, string? authorSurname = null, string? publishingHouse = null)
        {
            XDocument xmlDocument = LoadXml();

            (List<Book> booksList, string returning) = OneResultOnly(title, authorName, authorSurname, publishingHouse);
            int bookID = booksList[0].BookID;
            int userID = result.ID;


            var reservation = xmlDocument.Root.Elements("Reservation")
                .LastOrDefault(r => r.Element("UserID") != null && (int)r.Element("UserID") == userID
                                     && r.Element("BookID") != null && (int)r.Element("BookID") == bookID);

            if (reservation != null)
            {
                // Imposta la data di restituzione sulla data corrente il file XML divide data e ora
                string formattedDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                reservation.SetElementValue("EndDate", formattedDate);

                SetXDocument(xmlDocument);
            }
        }

        public int? FindUserIdByUsername(string username)
        {
            XDocument xmlDocument = LoadXml();

            // Cerca l'utente corrispondente all'username specificato
            var user = xmlDocument.Descendants("Users")
                                  .FirstOrDefault(u => (string)u.Element("UserName") == username);

            // Se l'utente è stato trovato, restituisci il suo ID
            if (user != null)
            {
                return (int)user.Element("UserId");
            }
            else
            {
                // Utente non trovato con lo username specificato.;
                return null;
            }
        }

        public List<string> UserActiveReservations(int? usersId)
        {
            XDocument xmlDocument = LoadXml();

            var now = DateTime.Now;
            var activeReservations = new List<string>();

            var reservations = xmlDocument.Root.Elements("Reservation").ToList();
            foreach (var reservation in reservations)
            {
                DateTime startDate = DateTime.Parse(reservation.Element("StartDate").Value);
                DateTime endDate = DateTime.Parse(reservation.Element("EndDate").Value);

                if (startDate <= now && endDate >= now) // Se la prenotazione è attiva
                {
                    string reservationInfo = $"Data inizio prenotazione: {startDate}. Data fine della prenotazione: {endDate}";
                    activeReservations.Add(reservationInfo);
                }
            }
            return activeReservations;
        }

        public List<string> ReadAllReservationsActive(int[] usersId)
        {
            XDocument xmlDocument = LoadXml();

            var now = DateTime.Now;
            var activeReservations = new List<string>();

            var reservations = xmlDocument.Root.Elements("Reservation");
            foreach (var reservation in reservations)
            {
                DateTime startDate = DateTime.Parse(reservation.Element("StartDate").Value);
                DateTime endDate = DateTime.Parse(reservation.Element("EndDate").Value);

                if (startDate <= now && endDate >= now) // Se la prenotazione è attiva
                {
                    string reservationInfo = $"Data inizio prenotazione: {startDate}. Data fine della prenotazione: {endDate}";
                    activeReservations.Add(reservationInfo);
                }
            }

            return activeReservations;
        }
        public string[] GetAllUsernames()
        {
                XDocument xmlDocument = LoadXml();

                // Ottieni tutti gli elementi "UserName" dal documento XML
                var usernames = xmlDocument.Descendants("UserName")
                                            .Select(u => (string)u)
                                            .ToArray();

                return usernames;
        }
        public int[] GetAllUserIDs()
        {
                XDocument xmlDocument = LoadXml();

                // Ottieni tutti gli elementi "UserID" dal documento XML e li converte in interi
                var userIDs = xmlDocument.Descendants("UserID")
                                          .Select(u => (int)u)
                                          .ToArray();

                return userIDs;
        }
        public int[] ReadAllUserReservationsID(string? title = null, string? authorName = null, string? authorSurname = null, string? publishingHouse = null) // RESTITUISCE LE IDRESERVATION ATTIVE DEL LIBRO
        {
            (List<Book> booksList, string returning) = OneResultOnly(title, authorName, authorSurname, publishingHouse);
            int bookID = booksList[0].BookID;

            XDocument xmlDocument = LoadXml();

            DateTime now = DateTime.Now;
            var allReservations = new List<int>();
            var reservations = xmlDocument.Root.Elements("Reservation").ToList();
            foreach (var reservation in reservations)
            {
                DateTime startDate = DateTime.Parse(reservation.Element("StartDate").Value);
                DateTime endDate = DateTime.Parse(reservation.Element("EndDate").Value);
                int reservationBookID = int.Parse(reservation.Element("BookID").Value);

                if (startDate <= now && endDate >= now && reservationBookID == bookID) // Verifichiamo se la prenotazione è attiva e il BookID corrisponde
                {
                    int userID = int.Parse(reservation.Element("UserID").Value);
                    allReservations.Add(userID);
                }
            }
            return allReservations.ToArray();
        }
        public List<(string Title, string Username)> GetActiveReservations()
        {

                XDocument xmlDocument = LoadXml();

                // Ottieni tutte le reservation attive dal documento XML
                var activeReservations = xmlDocument.Descendants("Reservation")
                                                    .Where(r => DateTime.Parse((string)r.Element("EndDate")) >= DateTime.Now)
                                                    .Select(r => (
                                                        Title: FindTitleByBookId((int)r.Element("BookID")),
                                                        Username: FindUsernameByUserID((int)r.Element("UserID"))
                                                    ))
                                                    .ToList();

                return activeReservations;
        }
        public string FindTitleByBookId(int bookId)
        {

                XDocument xmlDocument = LoadXml();

                // Cerca il titolo del libro con l'ID corrispondente nel documento XML
                var title = xmlDocument.Descendants("Books")
                                       .Where(b => (int)b.Element("BookID") == bookId)
                                       .Select(b => (string)b.Element("Title"))
                                       .FirstOrDefault();

            return title;
        }
        public string FindUsernameByUserID(int userID)
        {
                XDocument xmlDocument = LoadXml();

                // Cerca lo username dell'utente con l'ID corrispondente nel documento XML
                var username = xmlDocument.Descendants("Users")
                                          .Where(u => (int)u.Element("UserId") == userID)
                                          .Select(u => (string)u.Element("UserName"))
                                          .FirstOrDefault();

            return username;
        }
        public Dictionary<int, string> ReadAllUserReservations(string? title = null, string? authorName = null, string? authorSurname = null, string? publishingHouse = null)
        {
            (List<Book> booksList, string returning) = OneResultOnly(title, authorName, authorSurname, publishingHouse);
            int bookID = booksList[0].BookID;

            XDocument xmlDocument = LoadXml();
            DateTime now = DateTime.Now;

            var userNames = new Dictionary<int, string>();

            var reservations = xmlDocument.Root.Elements("Reservation").ToList();
            foreach (var reservation in reservations)
            {
                DateTime startDate = DateTime.Parse(reservation.Element("StartDate").Value);
                DateTime endDate = DateTime.Parse(reservation.Element("EndDate").Value);
                int reservationBookID = int.Parse(reservation.Element("BookID").Value);

                if (startDate <= now && endDate >= now && reservationBookID == bookID) // Verifica se la prenotazione è attiva e il BookID corrisponde
                {
                    int userID = int.Parse(reservation.Element("UserID").Value);
                    userNames[userID] = FindUsernameByUserID(userID); // Aggiungiamo l'ID utente e il suo username al dizionario
                }
            }

            return userNames;
        }
    }
}






//var path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
//Console.WriteLine(path);
//var pathDir = Path.Combine(path, "XML");

//if (!Directory.Exists(pathDir))
//{
//    Directory.CreateDirectory(pathDir);
//} CREAZIONE CARTELLA PER AVERE DATABASE PATH VALIDO PER TUTTI I PC E COMPATIBILE CON I VARI SISTEMI OPERATIVI, DA RIVEDERE