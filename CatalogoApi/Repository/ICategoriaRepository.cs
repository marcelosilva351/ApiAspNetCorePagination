using CatalogoApi.Helpers;
using CatalogoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
       Task<List<Categoria>> ProdutosDaCategoria(int CategoriaId);
        Task<ListaPaginada<Categoria>> ObterCategoriasPaginadas(Paginacao paginacao);
    }
}
