using Microsoft.EntityFrameworkCore;
using RestauranteAPI.Context.Core.Repositories;
using RestauranteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteAPI.Context.Persistence.Repositories
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(RestauranteContext context) : base(context) { }

        public Conta GetContaByNumeroMesa(int numero)
        {
            var conta = RestauranteContext.Conta.
                Include(_pedido => _pedido.Pedidos).
                ThenInclude(_itensPedido => _itensPedido.ItensPedido).
                ThenInclude(_item => _item.Item).
                Where(_conta => _conta.Mesa.Numero == numero && _conta.Status == "aberta").
                FirstOrDefault();

            return conta;
        }

        public RestauranteContext RestauranteContext
        {
            get { return Context as RestauranteContext; }
        }
    }
}
