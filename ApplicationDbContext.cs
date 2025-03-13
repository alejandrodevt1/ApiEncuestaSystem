using ApiEncuestaSystem.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ApiEncuestaSystem
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Encuesta> Encuesta { get; set; }
        public DbSet<Preguntas> Preguntas { get; set; }
        public DbSet<Opciones> Opciones { get; set; }
        public DbSet<Respuestas> Respuestas { get; set; }
        public DbSet<UsuarioEncuesta> UsuariosEncuesta { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUser>()
            .Property(pp => pp.Id)
            .HasMaxLength(100);

            builder.Entity<UsuarioEncuesta>()
             .Property(pp => pp.UsuarioId)
             .HasMaxLength(100);

            builder.Entity<UsuarioEncuesta>()
            .HasKey(pp => new { pp.UsuarioId, pp.EncuestaID });

            builder.Entity<UsuarioEncuesta>()
                .HasOne(e => e.Encuesta)
                .WithMany()
                .HasForeignKey(e => e.EncuestaID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UsuarioEncuesta>()
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UsuarioId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Respuestas>().HasOne(r => r.UsuarioEncuestas).WithMany().
                HasForeignKey(r => new { r.UsuarioId, r.EncuestaId }).OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(builder);
        }
    }
}
