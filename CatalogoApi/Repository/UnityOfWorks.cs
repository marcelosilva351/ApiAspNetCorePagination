using CatalogoApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Repository
{
    public class UnityOfWorks : IUnityOfWorks
    {
        private  ProdutoRepository _produtoRepo;
        private  CategoriaRepository _categoriaRepo;
        public readonly Context _context;

        public UnityOfWorks(Context context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
            }
        }
        public ICategoriaRepository categoriaRepository 
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
            }
        }
        public bool commit()
        {
            return (_context.SaveChanges()) > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
