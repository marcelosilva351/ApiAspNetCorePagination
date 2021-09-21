using CatalogoApi.Data;
using CatalogoApi.Helpers;
using CatalogoApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Repository
{
    public class ProdutoRepository : Repositorio<Produto>, IProdutoRepository
    {
        
        public ProdutoRepository(Context context) : base(context)
        {
        }

        public async Task<IEnumerable<Produto>> ObterProdutosInclude()
        {
            var produtos = await context.Produtos.Include(p => p.Categoria).ToListAsync();
            return produtos;
        }

        public async Task<ListaPaginada<Produto>> ObterProdutosPaginacaco(Paginacao paginacao)
        {
            var produtoPaginados = new ListaPaginada<Produto>();
            produtoPaginados.paginacao.QuantidadeDeRegistros = paginacao.QuantidadeDeRegistros;
            produtoPaginados.paginacao.NumeroPagina = paginacao.NumeroPagina;
            var produtos = await context.Produtos.ToListAsync();
            produtoPaginados.paginacao.TotalRegistros = produtos.Count();
            var produtosSkipTake =  produtos.Skip((paginacao.NumeroPagina - 1) * paginacao.QuantidadeDeRegistros).Take(paginacao.QuantidadeDeRegistros);
            produtoPaginados.paginacao.TotalDePaginas = (int)Math.Ceiling((double)produtoPaginados.paginacao.TotalRegistros/paginacao.QuantidadeDeRegistros);
            produtoPaginados.Lista.AddRange(produtosSkipTake);
            return  produtoPaginados;
        }

        public async Task<IEnumerable<Produto>> ProdutosComPrecoAcimaDe(decimal preco)
        {
            var produtos = ObterTodos();
            return await produtos;
        }



       
    }
}
