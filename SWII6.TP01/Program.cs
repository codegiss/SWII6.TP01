// See https://aka.ms/new-console-template for more information
using SWII6.TP01.Negocio;
using SWII6.TP01.Repositorio;
using Microsoft.AspNetCore.Hosting;

var repo = new LivroRepositorioCSV();

IWebHost host = new WebHostBuilder()
    .UseKestrel()
    .UseStartup<Startup>()
    .Build();
host.Run();

repo.MudarQuantidade("Carrie, a estranha", 6);
repo.MudarPreco("Fun Retrospectives", 70);

Author gaiman = new Author("Neil Gaiman", "gaiman@gmail.com", 'H');
Author terry = new Author("Terry Pratchett", "pratchett@gmail.com", 'H');
List<Author> list = new List<Author>();
list.Add(gaiman);
list.Add(terry);
repo.Incluir(new Book("Good Omens", list, 90, 5));

Author rachel = new Author("Rachel Cohn", "rachel@gmail.com", 'M');
Author david = new Author("David Levithan", "levithan@gmail.com", 'H');
List<Author> lista = new List<Author>();
lista.Add(gaiman);
lista.Add(terry);
repo.Incluir(new Book("Nick & Norah's Infinite Playlist", lista, 70));

repo.MostrarLivros();
Console.WriteLine("==========");
Console.WriteLine(repo.MostrarAutores("Scrum, Gestao Agil"));
Console.WriteLine($"Autores de 'The Illuminae Files': " + repo.NomesAutores("The Illuminae Files"));

Console.WriteLine(repo.MostrarTitulo("Dom Casmurro"));
Console.WriteLine("Livro: \"Mundo de Sofia\". Preço: R$" + repo.VerPreco("Mundo de Sofia"));
Console.WriteLine("Livro: \"Mundo de Sofia\". Quantidade: " + repo.VerQuantidade("Mundo de Sofia"));