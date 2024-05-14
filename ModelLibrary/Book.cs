

namespace ModelLibrary
{
    public class Book
    {
        private static int increment = 0;
        
        public int BookID { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string PublishingHouse { get; set; }
        public uint Quantity { get; set; }

        public Book()
        {
            BookID = increment++;
        }
        public Book(string title, string authorName, string authorSurname, string publishingHouse, uint quantity)
        {
            BookID = increment++;

            Title = title;

            AuthorName = authorName;

            AuthorSurname = authorSurname;

            PublishingHouse = publishingHouse;

            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Titolo: {Title} \nNome autore: {AuthorName} \nCognome autore: {AuthorSurname} \nCasa editrice: {PublishingHouse}";
        }
    }
}