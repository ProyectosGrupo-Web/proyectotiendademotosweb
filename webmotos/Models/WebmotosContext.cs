using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace webmotos.Models;

public partial class WebmotosContext : DbContext
{
    public WebmotosContext()
    {
    }

    public WebmotosContext(DbContextOptions<WebmotosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accesorio> Accesorios { get; set; }

    public virtual DbSet<Categoriasaccesorio> Categoriasaccesorios { get; set; }

    public virtual DbSet<Foto> Fotos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Moto> Motos { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Home> Home { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Elimina esta línea
        // => optionsBuilder.UseMySql("server=localhost;port=3306;database=webmotos;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));
       
    }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseMySql("server=localhost;port=3306;database=webmotos;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");
        modelBuilder.Entity<Home>(entity =>
        {
            entity.HasKey(e => e.IdContenido).HasName("PRIMARY");
            entity.ToTable("home");
            entity.Property(e => e.IdContenido)
                .HasColumnType("int(11)")
                .HasColumnName("id_contenido");

            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.Property(e => e.UrlImagen)
                .HasMaxLength(255)
                .HasColumnName("url_imagen");

            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");

            entity.Property(e => e.Prioridad)
                .HasDefaultValue(1)
                .HasColumnType("int(11)")
                .HasColumnName("prioridad");

        }
        );
        modelBuilder.Entity<Accesorio>(entity =>
        {
            entity.HasKey(e => e.IdAccesorio).HasName("PRIMARY");

            entity.ToTable("accesorios");

            entity.HasIndex(e => e.IdCategoria, "id_categoria");

            entity.Property(e => e.IdAccesorio)
                .HasColumnType("int(11)")
                .HasColumnName("id_accesorio");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("creado_en");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Disponible).HasColumnName("disponible");
            entity.Property(e => e.IdCategoria)
                .HasColumnType("int(11)")
                .HasColumnName("id_categoria");
            entity.Property(e => e.NombreAccesorio)
                .HasMaxLength(255)
                .HasColumnName("nombre_accesorio");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Accesorios)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("accesorios_ibfk_1");
        });

        modelBuilder.Entity<Categoriasaccesorio>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");

            entity.ToTable("categoriasaccesorios");

            entity.HasIndex(e => e.NombreCategoria, "nombre_categoria").IsUnique();

            entity.Property(e => e.IdCategoria)
                .HasColumnType("int(11)")
                .HasColumnName("id_categoria");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreCategoria).HasColumnName("nombre_categoria");
        });

        modelBuilder.Entity<Foto>(entity =>
        {
            entity.HasKey(e => e.IdFoto).HasName("PRIMARY");

            entity.ToTable("fotos");

            entity.HasIndex(e => e.IdMoto, "id_moto");

            entity.Property(e => e.IdFoto)
                .HasColumnType("int(11)")
                .HasColumnName("id_foto");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("creado_en");
            entity.Property(e => e.IdMoto)
                .HasColumnType("int(11)")
                .HasColumnName("id_moto");
            entity.Property(e => e.UrlFoto)
                .HasMaxLength(255)
                .HasColumnName("url_foto");

            entity.HasOne(d => d.IdMotoNavigation)
                .WithMany(p => p.Fotos)
                .HasForeignKey(d => d.IdMoto)
                .HasConstraintName("fotos_ibfk_1");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PRIMARY");

            entity.ToTable("marcas");

            entity.Property(e => e.IdMarca)
                .HasColumnType("int(11)")
                .HasColumnName("id_marca");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("creado_en");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(255)
                .HasColumnName("nombre_marca");
            entity.Property(e => e.PaisOrigen)
                .HasMaxLength(255)
                .HasColumnName("pais_origen");
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.IdModelo).HasName("PRIMARY");

            entity.ToTable("modelos");

            entity.HasIndex(e => e.IdMarca, "id_marca");

            entity.HasIndex(e => e.IdTipo, "id_tipo");

            entity.Property(e => e.IdModelo)
                .HasColumnType("int(11)")
                .HasColumnName("id_modelo");
            entity.Property(e => e.Anio)
                .HasColumnType("int(11)")
                .HasColumnName("anio");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("creado_en");
            entity.Property(e => e.IdMarca)
                .HasColumnType("int(11)")
                .HasColumnName("id_marca");
            entity.Property(e => e.IdTipo)
                .HasColumnType("int(11)")
                .HasColumnName("id_tipo");
            entity.Property(e => e.NombreModelo)
                .HasMaxLength(255)
                .HasColumnName("nombre_modelo");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("modelos_ibfk_1");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modelos_ibfk_2");
        });

        modelBuilder.Entity<Moto>(entity =>
        {
            entity.HasKey(e => e.IdMoto).HasName("PRIMARY");

            entity.ToTable("motos");

            entity.HasIndex(e => e.IdModelo, "id_modelo");

            entity.Property(e => e.IdMoto)
                .HasColumnType("int(11)")
                .HasColumnName("id_moto");
            entity.Property(e => e.Cilindrada)
                .HasColumnType("int(11)")
                .HasColumnName("cilindrada");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("creado_en");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Disponible).HasColumnName("disponible");
            entity.Property(e => e.IdModelo)
                .HasColumnType("int(11)")
                .HasColumnName("id_modelo");
            entity.Property(e => e.Potencia)
                .HasColumnType("int(11)")
                .HasColumnName("potencia");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.velocidadMax)
                .HasColumnType("int(11)")
                .HasColumnName("velocidadMax");

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.Motos)
                .HasForeignKey(d => d.IdModelo)
                .HasConstraintName("motos_ibfk_1");

            entity.HasMany(d => d.IdAccesorios).WithMany(p => p.IdMotos)
                .UsingEntity<Dictionary<string, object>>(
                    "Motoaccesorio",
                    r => r.HasOne<Accesorio>().WithMany()
                        .HasForeignKey("IdAccesorio")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("motoaccesorios_ibfk_2"),
                    l => l.HasOne<Moto>().WithMany()
                        .HasForeignKey("IdMoto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("motoaccesorios_ibfk_1"),
                    j =>
                    {
                        j.HasKey("IdMoto", "IdAccesorio")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("motoaccesorios");
                        j.HasIndex(new[] { "IdAccesorio" }, "id_accesorio");
                        j.IndexerProperty<int>("IdMoto")
                            .HasColumnType("int(11)")
                            .HasColumnName("id_moto");
                        j.IndexerProperty<int>("IdAccesorio")
                            .HasColumnType("int(11)")
                            .HasColumnName("id_accesorio");
                    });
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.IdTipo).HasName("PRIMARY");

            entity.ToTable("tipos");

            entity.HasIndex(e => e.Tipo1, "tipo").IsUnique();

            entity.Property(e => e.IdTipo)
                .HasColumnType("int(11)")
                .HasColumnName("id_tipo");
            entity.Property(e => e.Tipo1)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("creado_en");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
