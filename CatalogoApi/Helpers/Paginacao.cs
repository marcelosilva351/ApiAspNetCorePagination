using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Helpers
{
    public class Paginacao
    {
        public int QuantidadeDeRegistros { get; set; }
        private int _NumeroPagina;
        public int TotalDePaginas { get; set; }
        public int TotalRegistros { get; set; }
        public int NumeroPagina {
            get
            {
                return _NumeroPagina;
            }
            set 
            {
                if(value > 50)
                {
                    throw new InvalidOperationException("Não é permitido pagina maior que 50");
                }
                _NumeroPagina = value;      
            } 
        }
    }
}
