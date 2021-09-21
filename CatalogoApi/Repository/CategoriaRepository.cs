using CatalogoApi.Data;
using CatalogoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using CatalogoApi.Helpers;

namespace CatalogoApi.Repository
{
    public class CategoriaRepository : Repositorio<Categoria>, ICategoriaRepository
    {
        
        public CategoriaRepository(Context context) : base(context)
        {
        }

        public async Task<ListaPaginada<Categoria>> ObterCategoriasPaginadas(Paginacao paginacao)
        {
            ListaPaginada<Categoria> CategoriaPaginada = new ListaPaginada<Categoria>();
            var categoriasContext = await context.Categorias.ToListAsync();
            var categoriasPaginadas = categoriasContext.Skip((paginacao.NumeroPagina - 1) * paginacao.QuantidadeDeRegistros);
            Paginacao paginacaoAdd = new Paginacao
            {
                NumeroPagina = paginacao.NumeroPagina,
                QuantidadeDeRegistros = paginacao.QuantidadeDeRegistros,
                TotalRegistros = categoriasContext.Count(),
                TotalDePaginas = (int)Math.Ceiling((double)paginacao.QuantidadeDeRegistros / categoriasContext.Count())
            };
            CategoriaPaginada.paginacao = paginacaoAdd;
            CategoriaPaginada.Lista.AddRange(categoriasPaginadas);
            return CategoriaPaginada;
        }

        public Task<List<Categoria>> ProdutosDaCategoria(int CategoriaId)
        {
            var Categoriaprodutos = context.Categorias.Where(c => c.Id == CategoriaId).Include(C => C.Produtos).ToListAsync();
            return Categoriaprodutos;
       
        }

        
    }
}
