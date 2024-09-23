using System.Text;

namespace SWII6.TP01.Negocio
{
    public class Book
    {
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        public double Price;
        public int Qty { get; set; }

        public Book(string name, List<Author> authors, double price)
        {
            Name = name;
            Authors = authors;
            Price = price;
            Qty = 0;
        }
        public Book(string name, List<Author> authors, double price, int qty)
        {
            Name = name;
            Authors = authors;
            Price = price;
            Qty = qty;
        }
        public Book() { }
        public string getName()
        {
            return Name;
        }
        public List<Author> getAuthors()
        {
            return Authors;
        }
        public double getPrice()
        {
            return Price;
        }
        public void setPrice(double price)
        {
            Price = price;
        }
        public int getQty()
        {
            return Qty;
        }
        public void setQty(int qty)
        {
            Qty = qty;
        }
        public string toString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Book[name={Name}");
            stringBuilder.Append($",authors={{");
            
            foreach( Author author in Authors )
            {
                stringBuilder.Append($"Author[name={author.Name}");
                stringBuilder.Append($",email={author.Email}");
                stringBuilder.Append($",gender={author.Gender}],");
            }

            stringBuilder.Append($"price={Price}");
            stringBuilder.Append($",qty={Qty}]");
            return stringBuilder.ToString();
        }
        public string getAuthorNames()
        {
            StringBuilder authorNames = new StringBuilder();
            foreach( Author author in Authors )
            {
                authorNames.Append($"{author.Name}, ");
            }
            
            if(authorNames.Length > 0)
            {
                authorNames.Remove((authorNames.Length - 2), 2);
            }

            return authorNames.ToString();
        }
    }
}
