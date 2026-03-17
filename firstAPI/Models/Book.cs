namespace FirstAPI.Models
{
    public class Book //like book is a table
    {
        public int ID { get; set; }

        public string Title { get; set; } = null!;  //null! was added to avoid warnings

        public string Author { get; set; } = null!;

        public int YearPublished { get; set; }
    }
}
