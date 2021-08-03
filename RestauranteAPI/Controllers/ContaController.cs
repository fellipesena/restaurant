using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
    public class ContaController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ContaController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Visualizar conta pelo número da mesa
        /// </summary>
        /// <param name="numeroMesa"></param>
        /// <response code="200">Conta de acordo com a mesa</response>
        /// <response code="404">Não há conta aberta para a mesa</response>
        [HttpGet("{numeroMesa}")]
        public ActionResult<Conta> GetConta(int numeroMesa)
        {
            var conta = _uow.Contas.GetContaByNumeroMesa(numeroMesa);

            if (conta == null)
            {
                return NotFound("Nenhuma conta aberta encontrada para a mesa inserida");
            }

            return conta;
        }


        /// <summary>
        /// Criar uma nova conta
        /// </summary>
        /// <param name="numeroMesa"></param>
        /// <response code="200">Conta criada com sucesso</response>
        /// <response code="404">Falha na criação da conta</response>
        [HttpPost]
        public ActionResult<Conta> PostConta(int numeroMesa)
        {
            var mesa = _uow.Mesas.Find(_mesa => _mesa.Numero == numeroMesa).FirstOrDefault();

            if(mesa == null)
            {
                return BadRequest("Não foi encontrada uma mesa com o ID inserido");
            }
            if (!mesa.Disponivel)
            {
                return BadRequest("A mesa escolhida não está disponível");
            }

            Conta conta = new Conta
            {
                Status = "aberta",
                IdMesa = mesa.Id,
                Valor = 0
            };

            _uow.Contas.Add(conta);

            mesa.Disponivel = false;

            _uow.Complete();

            return conta;
        }

        /// <summary>
        /// Encerra a conta de acordo com a mesa
        /// </summary>
        /// <param name="numeroMesa"></param>
        /// <response code="200">Conta encerrada de acordo com a mesa</response>
        /// <response code="404">Não há conta aberta para a mesa</response>
        [HttpPut("{numeroMesa}")]
        public ActionResult<Conta> PutConta(int numeroMesa)
        {
            var conta = _uow.Contas.GetContaByNumeroMesa(numeroMesa);

            if (conta == null)
            {
                return NotFound("Nenhuma conta aberta encontrada para a mesa inserida");
            }

            var mesa = _uow.Mesas.Get(conta.IdMesa);

            mesa.Disponivel = true;
            conta.Status = "fechada";

            _uow.Complete();

            return conta;
        }
    }
}
