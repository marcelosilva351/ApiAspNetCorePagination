using AutoMapper;
using CatalogoApi.Data.DTO_s;
using CatalogoApi.Data.DTO_s.Produtos;
using CatalogoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Data.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<CreateProdutoDTO, Produto>();
            CreateMap<Produto, ReadProdutoDTO>();
            CreateMap<UpdateProdutoDTO, Produto>();
           
        }
    }
}
