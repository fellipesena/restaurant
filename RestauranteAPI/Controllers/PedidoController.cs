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
    public class PedidoController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public PedidoController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        /// <summary>
        /// Criar um novo pedido
        /// </summary>
        /// <param name="pedidos"></param>
        /// <param name="idGarcom"></param>
        /// <param name="numeroMesa"></param>
        /// <response code="200">Pedido efetuado com sucesso</response>
        /// <response code="400">Falha ao processar pedido</response>
        [HttpPost]
        public ActionResult<Pedido> PostPedido(IEnumerable<ItensPedido> pedidos, int idGarcom, int numeroMesa)
        {
            var existeGarcom = _uow.Garcons.Get(idGarcom);
            if(existeGarcom == null)
            {
                return BadRequest("Não foi encontrado nenhum garçom com o ID inserido");
            }

            var conta = _uow.Contas.GetContaByNumeroMesa(numeroMesa);
            if (conta == null)
            {
                return BadRequest("Não foi encontrada nenhuma conta em aberto para a mesa inserida");
            }

            decimal valorPedido = 0;

            foreach(var itens in pedidos)
            {
                var item = _uow.Itens.Get(itens.IdItem);

                if(item.QtdEstoque < itens.Quantidade)
                {
                    return BadRequest("Não há " + item.Nome + " suficiente em estoque");
                }

                itens.ValorUnitario = item.Valor;
                itens.ValorTotal = item.Valor * itens.Quantidade;
                item.QtdEstoque -= itens.Quantidade;

                valorPedido += itens.ValorTotal;
            }

            var pedido = new Pedido
            {
                Valor = valorPedido,
                IdGarcom = idGarcom,
                IdConta = conta.Id,
                IdMesa = conta.IdMesa,
                Horario = DateTime.Now
            };

            conta.Valor += valorPedido;

            _uow.Pedidos.Add(pedido);

            _uow.Complete();

            foreach (var itens in pedidos)
            {
                itens.IdPedido = pedido.Id;
            }

            _uow.ItensPedido.AddRange(pedidos);

            _uow.Complete();

            return pedido;
        }
    }
}
