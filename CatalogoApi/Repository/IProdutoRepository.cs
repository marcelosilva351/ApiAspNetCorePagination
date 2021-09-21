using CatalogoApi.Helpers;
using CatalogoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ProdutosComPrecoAcimaDe(decimal preco);
        Task<IEnumerable<Produto>> ObterProdutosInclude();
        Task<ListaPaginada<Produto>> ObterProdutosPaginacaco(Paginacao paginacao);
    }
}
