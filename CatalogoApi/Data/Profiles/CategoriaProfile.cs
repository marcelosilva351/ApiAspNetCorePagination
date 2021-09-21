using AutoMapper;
using CatalogoApi.Data.DTO_s.Categorias;
using CatalogoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Data.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaDTO, Categoria>();

        }
    }
}
