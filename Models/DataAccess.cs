using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using PikyServer.Models;

namespace mysqlefcore
{
  public class PikyContext : DbContext
  {
    public DbSet<Pelicula> Pelicula { get; set; }
    public DbSet<Novela> Novela { get; set; }
    public DbSet<Serie> Serie { get; set; }
    public DbSet<Reality> Reality { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseMySQL("server=localhost;database=piky;user=KhozmoS;password=TitulodeOro5!");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Pelicula>(entity =>
      {
          entity.HasKey( e => e.Pelicula_Id );
          entity.Property(e => e.Pelicula_Nombre).IsRequired();
          entity.Property(e => e.Pelicula_Genero);
          entity.Property(e => e.Pelicula_Pais);
          entity.Property(e => e.Pelicula_Publicacion);
          entity.Property(e => e.Pelicula_Sipopsis);
      });

      modelBuilder.Entity<Novela>(entity =>
      {
          entity.HasKey( e => e.Novela_Id );
          entity.Property(e => e.Novela_Nombre).IsRequired();
          entity.Property(e => e.Novela_Genero);
          entity.Property(e => e.Novela_Pais);
          entity.Property(e => e.Novela_Publicacion);
          entity.Property(e => e.Novela_Sipopsis);
          entity.Property(e => e.Novela_CantidadCapitulos);
          
      });

      modelBuilder.Entity<Serie>(entity =>
      {
          entity.HasKey( e => e.Serie_Id );
          entity.Property(e => e.Serie_Nombre).IsRequired();
          entity.Property(e => e.Serie_Genero);
          entity.Property(e => e.Serie_Pais);
          entity.Property(e => e.Serie_Publicacion);
          entity.Property(e => e.Serie_Sipopsis);
          entity.Property(e => e.Serie_CantidadCapitulos);
          
      });

      modelBuilder.Entity<Reality>(entity =>
      {
          entity.HasKey( e => e.Reality_Id );
          entity.Property(e => e.Reality_Nombre).IsRequired();
          entity.Property(e => e.Reality_Genero);
          entity.Property(e => e.Reality_Pais);
          entity.Property(e => e.Reality_Publicacion);
          entity.Property(e => e.Reality_Sipopsis);
          entity.Property(e => e.Reality_CantidadCapitulos);          
      });

    }
  }
}