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
    public class MesaController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public MesaController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Visualizar todas as mesas
        /// </summary>
        /// <response code="200">Lista com todas as mesas</response>
        /// <response code="404">Nenhuma mesa cadastrada</response>
        [HttpGet]
        public ActionResult<IEnumerable<Mesa>> GetMesas()
        {
            var mesas = _uow.Mesas.GetAll();

            if(mesas.Count() == 0)
            {
                return NotFound("Ainda não há mesas cadastradas");
            }

            return mesas.ToList();
        }

        /// <summary>
        /// Visualizar uma mesa pelo número
        /// </summary>
        /// <param name="numero"></param>
        /// <response code="200">Mesa de acordo com o número</response>
        /// <response code="404">Nenhuma mesa encontrada</response>
        [HttpGet("{numero}")]
        public ActionResult<Mesa> GetMesa(int numero)
        {
            var mesa = _uow.Mesas.Find(_mesa => _mesa.Numero == numero);

            if(mesa.Count() == 0)
            {
                return NotFound("Nenhuma mesa encontrada com o número inserido");
            }

            return mesa.First();
        }

        /// <summary>
        /// Visualizar todas as mesas disponíveis
        /// </summary>
        /// <response code="200">Lista com mesas disponíveis</response>
        /// <response code="404">Nenhuma mesa disponível</response>
        [HttpGet("disponiveis")]
        public ActionResult<IEnumerable<Mesa>> GetMesasDisponiveis()
        {
            var mesas = _uow.Mesas.Find(_mesa => _mesa.Disponivel);

            if (mesas.Count() == 0)
            {
                return NotFound("Não há mesas disponíveis");
            }

            return mesas.ToList();
        }

        /// <summary>
        /// Criar uma nova mesa
        /// </summary>
        /// <param name="numero"></param>
        /// <response code="200">Mesa cadastrada com sucesso</response>
        /// <response code="404">Número da mesa indisponível</response>
        [HttpPost]
        public ActionResult<Mesa> PostMesa(int numero)
        {
            var existeMesa = _uow.Mesas.Find(_mesa => _mesa.Numero == numero);

            if (existeMesa.Count() > 0)
            {
                return BadRequest("Já existe uma mesa com esse número!");
            }

            Mesa mesa = new Mesa
            {
                Numero = numero,
                Disponivel = true
            };

            _uow.Mesas.Add(mesa);

            _uow.Complete();

            return mesa;
        }

        /// <summary>
        /// Deletar uma mesa pelo do número
        /// </summary>
        /// <param name="numero"></param>
        /// <response code="200">Mesa deletada com sucesso</response>
        /// <response code="404">Nenhuma mesa encontrada</response>
        [HttpDelete]
        public ActionResult DeleteMesa(int numero)
        {
            var mesa = _uow.Mesas.Find(_mesa => _mesa.Numero == numero);

            if(mesa.Count() == 0)
            {
                return NotFound("Nenhuma mesa encontrada com o número inserido");
            }

            _uow.Mesas.Remove(mesa.First());

            _uow.Complete();

            return Ok();
        }
    }
}
