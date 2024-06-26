﻿namespace DataInitialization
{
    using ModelLibrary;
    using System.Data;
    using System.Xml;
    using System.Xml.Linq;

    namespace DataInitialization
    {
        public class DataInitializating
        {

            internal DataSet? dS;

            internal DataTable? booksTable;

            internal DataTable? usersTable;

            internal DataTable? reservationsTable;

            public static string DatabasePath { get; set; } = "C:";
            public string FileName { get; set; } = "Database.xml";

            public void XmlFileManager(string directoryPath, string fileName)
            {
                DatabasePath = directoryPath;
                FileName = fileName;
            }

            public void CreateDatabase()
            {
                string directoryPath = Path.Combine(DatabasePath, "ModelLibraryDatabase");
                string filePath = Path.Combine(directoryPath, FileName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                if (!File.Exists(filePath))
                {
                    CreateXmlFile(filePath);
                }
            }

            private void CreateXmlFile(string filePath)
            {
                // Creazione DataSet
                DataSet dataSet = new DataSet("Database");

                // Creazione DataTable per Users
                DataTable usersTable = new DataTable("Users");
                usersTable.Columns.Add("UserId", typeof(int));
                usersTable.Columns.Add("UserName", typeof(string));
                usersTable.Columns.Add("Password", typeof(string));
                usersTable.Columns.Add("Role", typeof(string));

                // Aggiunta righe per Users
                usersTable.Rows.Add(0, "Zarbo", "Salvatore90", "Admin");
                usersTable.Rows.Add(1, "Seeyote", "Hotuto", "Admin");
                usersTable.Rows.Add(2, "Arthemysia", "Valentina88", "User");
                usersTable.Rows.Add(3, "AkaneTakibana", "AkyTaky", "User");
                usersTable.Rows.Add(4, "TrafalgarKidd", "1", "User");
                usersTable.Rows.Add(5, "IlaryTheReturn", "Francesca91", "User");
                usersTable.Rows.Add(6, "1", "1", "User");


                // Aggiunta DataTable al DataSet
                dataSet.Tables.Add(usersTable);

                // Creazione DataTable per Books
                DataTable booksTable = new DataTable("Books");
                booksTable.Columns.Add("BookID", typeof(int));
                booksTable.Columns.Add("Title", typeof(string));
                booksTable.Columns.Add("AuthorName", typeof(string));
                booksTable.Columns.Add("AuthorSurname", typeof(string));
                booksTable.Columns.Add("PublishingHouse", typeof(string));
                booksTable.Columns.Add("Quantity", typeof(int));

                // Aggiunta righe per Books
                booksTable.Rows.Add(0, "Lolita", "Vladimir", "Nabokov", "Adelphi", 9);
                booksTable.Rows.Add(1, "Confucio Maestro di Vita", "Alexis", "Lavis", "Armenia", 6);
                booksTable.Rows.Add(2, "Uno, Nessuno E Centomila", "Luigi", "Pirandello", "Mondadori", 10);
                booksTable.Rows.Add(3, "Risveglio: Con Esercizi Delle Antiche Scuole Esoteriche", "Salvatore", "Brizzi", "Anima Edizioni", 9);
                booksTable.Rows.Add(4, "Jimmy Della Collina", "Massimo", "Carlotto", "Giulio Einaudi Autore", 2);
                booksTable.Rows.Add(5, "I Tre Porcellini", "James Orchard", "Halliwell-Phillipps", "Giunti Editore", 9);
                booksTable.Rows.Add(6, "Le Cinque Ferite E Come Guarirle", "Lise", "Bourbeau", "Amrita Edizioni", 9);
                booksTable.Rows.Add(7, "Narciso E Boccadoro", "Hermann", "Hesse", "Mondadori", 13);
                booksTable.Rows.Add(8, "Cime Tempestose", "Emily", "Brontë", "Mondadori", 9);
                booksTable.Rows.Add(9, "Madame Bovary", "Gustave", "Brizzi", "Flaubert", 9);



                // Aggiunta DataTable al DataSet
                dataSet.Tables.Add(booksTable);

                // Salvataggio del DataSet come XML
                dataSet.WriteXml(filePath);
            }



            //public void CreateDatabase() // METODO CHE RICHIAMERO' IN PROGRAM PER CREARE UN DATABASE DI DEFAULT
            //{
            //    // Creazione del documento XML
            //    XmlDocument xmlDoc = new XmlDocument();

            //    // Creazione dell'elemento radice
            //    XmlElement root = xmlDoc.CreateElement("Root");
            //    xmlDoc.AppendChild(root);

            //    // Salvataggio del documento XML nel file specificato
            //    xmlDoc.Save(DatabasePath);

            //    //dS = new DataSet("Database");
            //    //dS.Tables.Add("User");
            //    //dS.Tables.Add("Book");
            //    //dS.Tables.Add("Reservation");

            //    //CreateUsers();
            //    //CreateBooks();
            //    //CreateReservations();

            //    //dS.WriteXml(DatabasePath);
            //}

            //public DataTable CreateUsers()
            //    {
            //        XDocument xmlDocument = XDocument.Load(DatabasePath);

            //        XElement users = xmlDocument.Descendants("User").FirstOrDefault();

            //        usersTable = dS.Tables["User"];
            //        usersTable.Columns.Add("UserId", typeof(int));
            //        usersTable.Columns.Add("UserName", typeof(string));
            //        usersTable.Columns.Add("Password", typeof(string));
            //        usersTable.Columns.Add("Role", typeof(string));

            //        Admin u1 = new()
            //        {
            //            UserName = "Zarbo",
            //            Password = "Salvatore90",
            //            Role = Role.Admin,
            //        };

            //        User u2 = new()
            //        {
            //            UserName = "FirstUser",
            //            Password = "FirstIsMe",
            //            Role = Role.User,
            //        };

            //        User u3 = new()
            //        {
            //            UserName = "1",
            //            Password = "1",
            //            Role = Role.User,
            //        };

            //        DataRow row1 = usersTable.NewRow();
            //        row1["UserID"] = u1.UserID;
            //        row1["UserName"] = u1.UserName;
            //        row1["Password"] = u1.Password;
            //        row1["Role"] = u1.Role;
            //        usersTable.Rows.Add(row1);

            //        DataRow row2 = usersTable.NewRow();
            //        row2["UserID"] = u2.UserID;
            //        row2["UserName"] = u2.UserName;
            //        row2["Password"] = u2.Password;
            //        row2["Role"] = u2.Role;
            //        usersTable.Rows.Add(row2);

            //        DataRow row3 = usersTable.NewRow();
            //        row3["UserID"] = u3.UserID;
            //        row3["UserName"] = u3.UserName;
            //        row3["Password"] = u3.Password;
            //        row3["Role"] = u3.Role;
            //        usersTable.Rows.Add(row3);

            //        usersTable.WriteXml(DatabasePath);

            //        return usersTable;
            //    }

            //    public DataTable CreateBooks()
            //    {
            //        XDocument xmlDocument = XDocument.Load(DatabasePath);

            //        XElement users = xmlDocument.Descendants("Book").FirstOrDefault();
            //        booksTable = dS.Tables["Book"];
            //        booksTable.Columns.Add("BookID", typeof(int));
            //        booksTable.Columns.Add("Title", typeof(string));
            //        booksTable.Columns.Add("AuthorName", typeof(string));
            //        booksTable.Columns.Add("AuthorSurname", typeof(string));
            //        booksTable.Columns.Add("PublishingHouse", typeof(string));
            //        booksTable.Columns.Add("Quantity", typeof(int));

            //        Book b1 = new()
            //        {
            //            Title = "Le Cinque Ferite E Come Guarirle",
            //            AuthorName = "Lise",
            //            AuthorSurname = "Bourbeau",
            //            PublishingHouse = "Amrita Edizioni",
            //            Quantity = 9
            //        };

            //        Book b2 = new()
            //        {
            //            Title = "Confucio Maestro di Vita",
            //            AuthorName = "Alexis",
            //            AuthorSurname = "Lavis",
            //            PublishingHouse = "Armenia",
            //            Quantity = 6
            //        };

            //        Book b3 = new()
            //        {
            //            Title = "Risveglio: Con Esercizi Delle Antiche Scuole Esoteriche",
            //            AuthorName = "Salvatore",
            //            AuthorSurname = "Brizzi",
            //            PublishingHouse = "Anima Edizioni",
            //            Quantity = 3
            //        };

            //        DataRow row1 = booksTable.NewRow();
            //        row1["BookID"] = b1.BookID;
            //        row1["Title"] = b1.Title;
            //        row1["AuthorName"] = b1.AuthorName;
            //        row1["AuthorSurname"] = b1.AuthorSurname;
            //        row1["PublishingHouse"] = b1.PublishingHouse;
            //        row1["Quantity"] = b1.Quantity;
            //        booksTable.Rows.Add(row1);

            //        DataRow row2 = booksTable.NewRow();
            //        row2["BookID"] = b2.BookID;
            //        row2["Title"] = b2.Title;
            //        row2["AuthorName"] = b2.AuthorName;
            //        row2["AuthorSurname"] = b2.AuthorSurname;
            //        row2["PublishingHouse"] = b2.PublishingHouse;
            //        row2["Quantity"] = b2.Quantity;
            //        booksTable.Rows.Add(row2);

            //        DataRow row3 = booksTable.NewRow();
            //        row3["BookID"] = b3.BookID;
            //        row3["Title"] = b3.Title;
            //        row3["AuthorName"] = b3.AuthorName;
            //        row3["AuthorSurname"] = b3.AuthorSurname;
            //        row3["PublishingHouse"] = b3.PublishingHouse;
            //        row3["Quantity"] = b3.Quantity;
            //        booksTable.Rows.Add(row3);

            //        dS.WriteXml(DatabasePath);

            //        return booksTable;
            //    }

            //    public DataTable CreateReservations()
            //    {
            //        XDocument xmlDocument = XDocument.Load(DatabasePath);

            //        XElement? users = xmlDocument.Descendants("Reservations").FirstOrDefault();

            //        reservationsTable = dS.Tables["Reservation"];
            //        reservationsTable.Columns.Add("UserID", typeof(int));
            //        reservationsTable.Columns.Add("BookID", typeof(int));
            //        reservationsTable.Columns.Add("StartDate", typeof(DateTime));
            //        reservationsTable.Columns.Add("EndDate", typeof(DateTime));

            //        return reservationsTable;
            //    }
        }
    }
}
