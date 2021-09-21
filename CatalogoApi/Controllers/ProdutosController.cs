using AutoMapper;
using CatalogoApi.Data;
using CatalogoApi.Data.DTO_s;
using CatalogoApi.Helpers;
using CatalogoApi.Models;
using CatalogoApi.Repository;
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
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {

        private readonly IUnityOfWorks _uof;
        private readonly IMapper _mapper;
        public ProdutosController(IUnityOfWorks context, IMapper mapper)
        {
            _mapper = mapper;
            _uof = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> ObterTodos()
        {
            try
            {
                var produtos = await _uof.ProdutoRepository.ObterProdutosInclude();
                return Ok(produtos);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }


        [HttpGet("produtosPaginacao")]
        public async Task<ActionResult<IEnumerable<Produto>>> ObterTodosPaginacao(Paginacao paginacao)
        {
            try
            {
                var produtosPaginacao = await _uof.ProdutoRepository.ObterProdutosPaginacaco(paginacao);
                var paginacaoHeader = produtosPaginacao.paginacao;
                var lista = produtosPaginacao.Lista;
                Response.Headers.Add("Pagination-x", JsonConvert.SerializeObject(paginacaoHeader));
                return Ok(lista);
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Banco de dados falhou");
            }

        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Produto>> Obter(int id)
        {
            Produto produtoPeloId = await _uof.ProdutoRepository.Obter(id);
            if (produtoPeloId == null)
            {
                return NotFound();
            }

            return Ok(produtoPeloId);
        }


        [HttpPost]
        public async Task<IActionResult> CadastrarProduto([FromBody] CreateProdutoDTO produto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            Produto produtoAdd = _mapper.Map<Produto>(produto);
             _uof.ProdutoRepository.Cadastrar(produtoAdd);

            if (_uof.commit())
            {
                return CreatedAtAction(nameof(Obter), new { id = produtoAdd.Id }, produtoAdd);
            }
            else
            {
                return StatusCode(500, "Não foi possivel Cadastrar o Produto");
            }
        }


        [HttpPut("{id}")]

        public IActionResult AtualizarProduto(int id, [FromBody] UpdateProdutoDTO updateProduto)
        {
            Produto produto = _uof.ProdutoRepository.Obter(id).Result;
            if (produto == null)
            {
                return NotFound("Produto não existe no banco");
            }
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(produto, updateProduto);

            _uof.ProdutoRepository.Atualizar(produto);

            if (_uof.commit())
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }


        }


        [HttpDelete("{id}")]

        public IActionResult DeletarProduto(int id)
        {
            Produto produtoDelete = _uof.ProdutoRepository.Obter(id).Result;
            if (produtoDelete == null)
            {
                return NotFound("Produto não existe no banco de dados");
            }
            _uof.ProdutoRepository.Deletar(produtoDelete);
            if (_uof.commit())
            {
                return NoContent();
            }
            return StatusCode(500);
        }


        [HttpPost("addCategoria/{id}")]

        public IActionResult AddCategoria(int id, [FromQuery] int idCategoria)
        {
            var ProdutoAddCategoria = _uof.ProdutoRepository.Obter(id).Result;
            ProdutoAddCategoria.CategoriaId = idCategoria;
            _uof.ProdutoRepository.Atualizar(ProdutoAddCategoria);
            if (_uof.commit())
            {
                return NoContent();
            }
            return StatusCode(500);
        }


    }
}
