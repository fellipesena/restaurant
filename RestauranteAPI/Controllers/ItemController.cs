using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Context.Core;
using RestauranteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ItemController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Visualizar todos os itens
        /// </summary>
        /// <response code="200">Lista com todos os itens</response>
        /// <response code="404">Nenhum item cadastrado</response>
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItens()
        {
            var itens = _uow.Itens.GetAll();

            if (itens.Count() == 0)
            {
                return NotFound("Ainda não há itens cadastrados");
            }

            return itens.ToList();
        }

        /// <summary>
        /// Visualizar item pelo ID
        /// </summary>
        /// <response code="200">Item de acordo com o ID</response>
        /// <response code="404">Nenhum item encontrado</response>
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id)
        {
            var item = _uow.Itens.Get(id);

            if(item == null)
            {
                return NotFound("Nenhum item encontrado com o ID inserido");
            }

            return item;
        }

        /// <summary>
        /// Criar um novo item
        /// </summary>
        /// <response code="200">Item criado com sucesso</response>
        [HttpPost]
        public ActionResult<Item> PostItem(Item item)
        {
            _uow.Itens.Add(item);

            _uow.Complete();

            return item;
        }

        /// <summary>
        /// Atualizar um item pelo ID
        /// </summary>
        /// <response code="200">Item atualizado com sucesso</response>
        /// <response code="404">Nenhum item encontrado</response>
        [HttpPut("{id}")]
        public ActionResult<Item> PutItem(int id, Item item)
        {
            var itemExiste = _uow.Itens.Get(id);
            if(itemExiste == null)
            {
                return NotFound("Nenhum item encontrado com o ID inserido");
            }
            itemExiste.Nome = item.Nome;
            itemExiste.Descricao = item.Descricao;
            itemExiste.Tipo = item.Tipo;
            itemExiste.Valor = item.Valor;
            itemExiste.QtdEstoque = item.QtdEstoque;

            _uow.Complete();

            return item;
        }

        /// <summary>
        /// Deletar um item pelo ID
        /// </summary>
        /// <response code="200">Item deletado com sucesso</response>
        /// <response code="404">Nenhum item encontrado</response>
        [HttpDelete]
        public ActionResult DeleteItem(int id)
        {
            var item = _uow.Itens.Get(id);

            if (item == null)
            {
                return NotFound("Nenhum item encontrado com o ID inserido");
            }

            _uow.Itens.Remove(item);

            _uow.Complete();

            return Ok();
        }
    }
}
