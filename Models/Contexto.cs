using Microsoft.EntityFrameworkCore;

namespace Andor.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Formacao> Formacoes { get; set; }
        public DbSet<Experiencia> Experiencias { get; set; }
        public DbSet<Trabalho> Trabalhos { get; set; }
        public DbSet<Moradia> Moradias { get; set; }
        public DbSet<Imagem> Imagens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // conexao local
            // optionsBuilder.UseSqlServer(connectionString: @"Server=DESKTOP-VV6TL78;Database=BETA_DBANDOR;Integrated Security=True");

            // conexao azure ferretti
            // optionsBuilder.UseSqlServer(connectionString: @"Data Source = andorserver.database.windows.net; Initial Catalog = BETE_DBANDOR; User ID = squad46; Password =#RecodePro;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


            // conexao azure bruna
             optionsBuilder.UseSqlServer(connectionString: @"Data Source = dbandor.database.windows.net; Initial Catalog = DBANDOR; User ID = squad46; Password =#RecodePro;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            

            // conexao somee.com
            // optionsBuilder.UseSqlServer(connectionString: @"workstation id = DBANDOR.mssql.somee.com; packet size = 4096; user id = Ferretti_SQLLogin_1; pwd = khoi5d5m8w; data source = DBANDOR.mssql.somee.com; persist security info = False; initial catalog = DBANDOR");



        }
    }        
}
