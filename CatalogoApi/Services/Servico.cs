using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Services
{
    public class Servico : IServico
    {
        public string Saudacao(string nome)
        {
            return $"Oi {nome}, Hora atual: {DateTime.Now.Hour}";
        }
    }
}
