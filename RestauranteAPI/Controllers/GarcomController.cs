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
    public class GarcomController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public GarcomController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Visualizar todos os garçons
        /// </summary>
        /// <response code="200">Lista com todos os garçons</response>
        /// <response code="404">Nenhum garçom cadastrado</response>
        [HttpGet]
        public ActionResult<IEnumerable<Garcom>> GetGarcons()
        {
            var garcons = _uow.Garcons.GetAll();

            if (garcons.Count() == 0)
            {
                return NotFound("Ainda não há garcons cadastrados");
            }

            return garcons.ToList();
        }

        /// <summary>
        /// Criar um novo garçom
        /// </summary>
        /// <param name="nome"></param>
        /// <response code="200">Garçom inserido com sucesso</response>
        [HttpPost]
        public ActionResult<Garcom> PostGarcom(string nome)
        {
            Garcom garcom = new Garcom
            {
                Nome = nome
            };

            _uow.Garcons.Add(garcom);

            _uow.Complete();

            return garcom;
        }

        /// <summary>
        /// Deletar um garçom pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Garçom removido com sucesso</response>
        /// <response code="404">Nenhum garçom encontrado</response>
        [HttpDelete]
        public ActionResult DeleteGarcom(int id)
        {
            var garcom = _uow.Garcons.Get(id);

            if (garcom == null)
            {
                return NotFound("Nenhum garcom encontrado com o ID inserido");
            }

            _uow.Garcons.Remove(garcom);

            _uow.Complete();

            return Ok();
        }
    }
}
