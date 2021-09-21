using AutoMapper;
using CatalogoApi.Data;
using CatalogoApi.Data.DTO_s.Categorias;
using CatalogoApi.Helpers;
using CatalogoApi.Models;
using CatalogoApi.Repository;
using CatalogoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoApi.Controllers
{

    [ApiController]
    [Route("api/categorias")]
    public class CategoriaController : ControllerBase
    {

        private readonly IUnityOfWorks _uow;
        private readonly IMapper _mapper;
        public CategoriaController(IUnityOfWorks context, IMapper mapper)
        {
            _mapper = mapper;
            _uow = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> ObterCategorias()
        {
            try
            {
                var categorias = await _uow.categoriaRepository.ObterTodos();
                return Ok(categorias);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Banco de dados falhou");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Obter(int id)
        {
            Categoria categoriaPeloId = await _uow.categoriaRepository.Obter(id);
            if (categoriaPeloId == null)
            {
                return NotFound("Categoria não existe no banco de dados");
            }
            return Ok(categoriaPeloId);
        }

        [HttpGet("CategoriasPaginadas")]

        public async Task<ActionResult<IEnumerable<Categoria>>> ObterCategoriasPaginadas(Paginacao paginacao)
        {
            try
            {
                var CategoriaPaginada = await _uow.categoriaRepository.ObterCategoriasPaginadas(paginacao);
               
                Response.Headers.Add("Pagination-X", JsonConvert.SerializeObject(CategoriaPaginada.paginacao));
                if(CategoriaPaginada != null)
                {
                    return Ok(CategoriaPaginada.Lista);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(500, "Banco de dados falhou");
            }
        }



        [HttpGet("produtosCategoria/{id}")]
        public async Task<ActionResult<List<Categoria>>> ObterProdutosDaCategoria(int id)
        {

            //var produtosCategorias = _context.Categorias.Where(c => c.Id == id).Include(c => c.Produtos);
            //return Ok(produtosCategorias);

            
            var categoriasProdutos = _uow.categoriaRepository.ProdutosDaCategoria(id);
            if(categoriasProdutos == null)
            {
                return NotFound();
            }
            return (await categoriasProdutos);
        }


        [HttpPost]
        public IActionResult CadastrarCategoria([FromBody] CreateCategoriaDTO categoriaDTO)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            Categoria categoriaAdd = _mapper.Map<Categoria>(categoriaDTO);
             _uow.categoriaRepository.Cadastrar(categoriaAdd);
            if (_uow.commit())
            {
                return CreatedAtAction(nameof(Obter), new { id = categoriaAdd.Id }, categoriaAdd);
            }
            return StatusCode(500);
        }
        [HttpPut("{id}")]
        public ActionResult AtualizarCategoria(int id, [FromBody] UpdateCategoriaDTO categoriaDTO)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            Categoria categoriaAdd = _uow.categoriaRepository.Obter(id).Result;
            _uow.categoriaRepository.Atualizar(categoriaAdd);
            if (_uow.commit())
            {
                return NoContent();
            }
            return StatusCode(500);
        }
        [HttpDelete("{id}")]
        public IActionResult DeletarCategoria(int id)
        {
            Categoria categoriaDeletar = _uow.categoriaRepository.Obter(id).Result;
            if(categoriaDeletar == null)
            {
                NotFound("Catgoria não existe no banco");
            }
            _uow.categoriaRepository.Deletar(categoriaDeletar);
            if (_uow.commit())
            {
                return NoContent();
            }
            return StatusCode(500);
        }
        [HttpGet("saudacao/{nome}")]
        public ActionResult<string> Saudacao ([FromServices] IServico servico, string nome)
        {
            return servico.Saudacao(nome);
        }
    }
}
