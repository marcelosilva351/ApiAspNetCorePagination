using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Data.DTO_s
{
    public class UpdateProdutoDTO
    {
        [Required(ErrorMessage = "Nome do produto é obrigatorio")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Descrição do produto é obrigatorio")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Preço do produto é obrigatorio")]
        public decimal Preco { get; set; }
        public string ImagemUrl { get; set; }
        public int Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "É obrigatorio a categoria deste produto")]
        public int CategoriaId { get; set; }
    }
}
