using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TictactoeApi.Models;

namespace TictactoeApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                /*entity.HasOne(d => d.FirstPlayer)
                .WithMany(p => p.Games)
                .HasForeignKey(d => d.FirstPlayerId)
                .HasConstraintName("FK_FirstPlayerId");*/

                entity.HasOne(d => d.LastPlayer)
               .WithMany(p => p.Games)
               .HasForeignKey(d => d.LastPlayerId)
               .HasConstraintName("FK_LastPlayerId");
            });

        }

        internal bool SaveChangesAsync(string v)
        {
            throw new NotImplementedException();
        }
    }
}
