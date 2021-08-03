using RestauranteAPI.Context.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteAPI.Context.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IContaRepository Contas { get; }
        IGarcomRepository Garcons { get; }
        IItemRepository Itens { get; }
        IItensPedidoRepository ItensPedido { get; }
        IMesaRepository Mesas { get; }
        IPedidoRepository Pedidos { get; }
        int Complete();
    }
}
