using CatalogoApi.Data;
using CatalogoApi.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Repository
{
    public class Repositorio<T> : IRepository<T> where T : class
    {
        protected readonly Context context;

        public Repositorio(Context context)
        {
            this.context = context;
        }

        public void Atualizar(T Entidade)
        {
            context.Set<T>().Update(Entidade);
        }

        public void Cadastrar(T Entidade)
        {
            context.Set<T>().AddAsync(Entidade);
        }

        public void Deletar(T entidade)
        {
            context.Set<T>().Remove(entidade);
        }

        public async Task<T> Obter(int id)
        {
            T entidade = await context.Set<T>().FindAsync(id);
            return entidade;
        }


        public  Task<List<T>> ObterTodos()
        {
            var entidades = context.Set<T>();
            return entidades.ToListAsync();
        }

       
             




        }
    }

