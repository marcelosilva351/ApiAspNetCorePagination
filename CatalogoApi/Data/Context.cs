using CatalogoApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Data
{
    public class Context : DbContext
    {


        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasMany(Categoria => Categoria.Produtos).WithOne(produto => produto.Categoria).HasForeignKey(produto => produto.CategoriaId);
            base.OnModelCreating(modelBuilder);
        }
        public Context( DbContextOptions options) : base(options)
        {
        }
    }
}
