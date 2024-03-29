﻿using ProjetoFinal.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ProjetoFinal.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tatuador> Tatuadores { get; set; }


    }
}
