using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Data.DTO_s.Categorias
{
    public class CreateCategoriaDTO
    { 
        [Required]
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        
    }
}
