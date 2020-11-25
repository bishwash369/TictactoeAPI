﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TictactoeApi.Models;

namespace TictactoeApi.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {  }

        public DbSet<Player> Players { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<Game> Games { get; set; }


    }
}
