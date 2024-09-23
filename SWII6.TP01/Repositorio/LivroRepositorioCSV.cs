using SWII6.TP01.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWII6.TP01.Repositorio
{
    public class LivroRepositorioCSV
    {
        private static readonly string livros = @"C:\Users\Giselle\Downloads\SWII6.TP01\SWII6.TP01\Repositorio\books.csv";
        public List<Book> books;
        public List<Author> authors;

        public LivroRepositorioCSV()
        {
            books = new List<Book>();

            using (var file = File.OpenText(LivroRepositorioCSV.livros))
            {
                while (!file.EndOfStream)
                {
                    var textoLivro = file.ReadLine();
                    
                    if(string.IsNullOrEmpty(textoLivro))
                    {
                        continue;
                    }
                    var infoLivro = textoLivro.Split(';');
                    var titulo = infoLivro[0].Trim('\"');

                    int start = textoLivro.IndexOf(";\"");
                    int end = textoLivro.LastIndexOf("\";");
                    authors = new List<Author>();

                    // o substring de autores só inicia 2 caracteres depois do index selecionado (ponto e virgula, aspas)
                    var autores = textoLivro.Substring(start+2);
                    autores = autores.Remove(end-start-2);

                    var priceQty = textoLivro.Substring(end + 2);
                    var numbers = priceQty.Split(';');

                    var autor = autores.Split(';');

                    foreach(string a in autor)
                    {
                        var infoAutor = a.Split(',');
                        var newAuthor = new Author
                        {
                            Name = infoAutor[0],
                            Email = infoAutor[1],
                            Gender = Char.Parse(infoAutor[2])
                        };
                        authors.Add(newAuthor);
                    }

                    var livro = new Book
                    {
                        Name = titulo,
                        Authors = authors,
                        Price = Double.Parse(numbers[0]),
                        Qty = int.Parse(numbers[1])
                    };

                    books.Add(livro);
                }
            }
        }
        public void Incluir(Book _livro)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"\"{_livro.Name}\";\"");

            foreach(Author a in _livro.Authors)
            {
                stringBuilder.Append($"{a.Name},{a.Email},{a.Gender};");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);

            stringBuilder.Append($"\";{_livro.Price}");
            stringBuilder.Append($";{_livro.Qty}");

            File.AppendAllLines(livros, [stringBuilder.ToString()]);
            books.Add(_livro);
        }
        public void MostrarLivros()
        {
            foreach(Book b in books)
            {
                Console.WriteLine(b.toString());
            }
        }
        public string MostrarAutores(string _titulo)
        {
            StringBuilder sb = new StringBuilder();
            Book found = books.Find(b => b.Name.Equals(_titulo));
            
            if(found != null)
            {
                List<Author> mostrarAutores = found.getAuthors();

                sb.AppendLine("==========");
                sb.AppendLine($"Título do livro: {found.Name}");

                foreach (Author a in mostrarAutores)
                {
                    sb.AppendLine($"Nome do autor: {a.Name}");
                    sb.AppendLine($"E-mail do autor: {a.Email}");
                    sb.AppendLine($"Gênero do autor: {a.Gender}");
                    sb.AppendLine($"----------");
                }
                return sb.ToString();
            }
            return null;
        }
        public void MudarPreco(string _titulo, double _preco)
        {
            string[] linhas = File.ReadAllLines(livros);

            for (int i = 0; i < linhas.Length; i++)
            {
                var textoLivro = linhas[i];

                if (textoLivro.StartsWith("\"" + _titulo))
                {
                    // setando no csv
                    var endFirstSubstring = textoLivro.LastIndexOf("\";");
                    var startSecondSubstring = textoLivro.LastIndexOf(';');

                    var subst1 = textoLivro.Remove(endFirstSubstring);
                    var subst2 = textoLivro.Substring(startSecondSubstring + 1);

                    linhas[i] = subst1 + "\";" + _preco + ";" + subst2;
                    File.WriteAllLines(livros, linhas);

                    // setando na memória
                    foreach(Book b in books)
                    {
                        if(b.Name == _titulo)
                        {
                            b.setPrice(_preco);
                            continue;
                        }
                    }
                }
            }
        }
        public double VerPreco(string _titulo)
        {
            Book find = books.Find(b => b.Name.Equals(_titulo));

            if(find!=null)
            {
                return find.getPrice();
            }
            return 0;
        }
        public void MudarQuantidade(string _titulo, int _qty)
        {
            string[] linhas = File.ReadAllLines(livros);

            for (int i = 0; i < linhas.Length; i++)
            {
                var textoLivro = linhas[i];

                if (textoLivro.StartsWith("\"" + _titulo))
                {
                    // setando no csv
                    var endSubstring = textoLivro.LastIndexOf(';');

                    var substring = textoLivro.Remove(endSubstring);

                    linhas[i] = substring + ";" + _qty;
                    File.WriteAllLines(livros, linhas);

                    // setando na memória
                    foreach (Book b in books)
                    {
                        if (b.Name == _titulo)
                        {
                            b.setQty(_qty);
                            continue;
                        }
                    }
                }
            }
        }
        public double VerQuantidade(string _titulo)
        {
            Book find = books.Find(b => b.Name.Equals(_titulo));

            if (find != null)
            {
                return find.getQty();
            }
            return 0;
        }
        public string NomesAutores(string _titulo)
        {
            Book found = books.Find(b => b.Name.Equals(_titulo));
            return found.getAuthorNames();
        }
        public string MostrarTitulo(string _titulo)
        {
            var find = books.Find(b => b.Name.Equals(_titulo));
            if (find == null)
            {
                return "Livro não encontrado";
            }
            return $"Título: {find.getName()}";
        }
    }
}
