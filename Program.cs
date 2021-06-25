using api.NET.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.NET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //using (DisneyDbContext context = new DisneyDbContext())
            //{
          
            //    Genero genero1 = new Genero() { Nombre = "Animada", Imagen = "animada.jpg",  };
            //    context.Add(genero1);
         
             
            //    //Guardamos los cambios
            //    context.SaveChanges();
            //}
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
