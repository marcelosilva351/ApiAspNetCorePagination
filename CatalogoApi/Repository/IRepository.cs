using CatalogoApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Repository
{
    public interface IRepository<T>
    {
        Task<List<T>> ObterTodos();
        Task<T> Obter(int id);
        void Cadastrar(T Entidade);
        void Atualizar(T Entidade);
        void Deletar(T entidade);

    }
}
