using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Repository
{
    public interface IUnityOfWorks
    {
        IProdutoRepository ProdutoRepository { get; }
        ICategoriaRepository categoriaRepository { get; }

        bool commit();
        void Dispose();

       
    }
}
