using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using SWII6.TP01.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SWII6.TP01.Negocio
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }
        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("nomeLivro/{_titulo}", NomeLivro);
            builder.MapRoute("toString/{_titulo}", toString);
            builder.MapRoute("nomeAutores/{_titulo}", NomesAutores);
            builder.MapRoute("livro/ApresentarLivro/{_titulo}", ApresentarLivro);
            var rotas = builder.Build();

            app.UseRouter(rotas);
        }
        public Task NomeLivro(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var titulo = context.GetRouteValue("_titulo").ToString();
            return context.Response.WriteAsync(repo.MostrarTitulo(titulo));
        }
        public Task toString(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var titulo = context.GetRouteValue("_titulo").ToString();
            var livro = repo.books.Find(l => l.Name.Equals(titulo));

            return context.Response.WriteAsync(livro.toString());
        }
        public Task NomesAutores(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var titulo = context.GetRouteValue("_titulo").ToString();
            var livro = repo.books.Find(l => l.Name.Equals(titulo));

            return context.Response.WriteAsync(livro.getAuthorNames());
        }
        public Task ApresentarLivro(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var titulo = context.GetRouteValue("_titulo").ToString();
            var livro = repo.books.Find(l => l.Name.Equals(titulo));
            
            var html = @"<!DOCTYPE html>
<html lang = 'pt-BR' xlmns='http://www.w3.org/1999/xhtml'>
<head>
    <meta charset = 'UTF-8'>
    <title>Livro</title>
</head>
    <body>
        <h1>#titulo#</h1>
        <h3>#autor#</h3>
    </body>
</html>";
            var autor = repo.NomesAutores(titulo);
            html = html.Replace("#titulo#", $"Titulo: {titulo}");
            html = html.Replace("#autor#", $"Autor(es): {autor}");

            var caminhoCompleto = @"C:\Users\Giselle\Downloads\SWII6.TP01\SWII6.TP01\HTML\livro.html";
            var res = CriarHTML(caminhoCompleto, html);

            return context.Response.WriteAsync(res);
        }
        private string CriarHTML(string caminho, string conteudo)
        {
            if (File.Exists(caminho)) { File.Delete(caminho); }
            File.AppendAllLines(caminho, [conteudo]);
            return conteudo;
        }
    }
}
