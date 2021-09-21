using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Helpers
{
    public class ListaPaginada<T>
    {
        public Paginacao paginacao { get; set; }
        public List<T> Lista { get; set; } = new List<T>();
    }
}
