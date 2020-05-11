using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EvidencijaZaposlenih.Models;
using EvidencijaZaposlenih.Interfaces;

namespace EvidencijaZaposlenih.Controllers
{
    public class KompanijeController : ApiController
    {
        IKompanijaRepository _repository { get; set; }

        public KompanijeController(IKompanijaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Kompanija> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult GetById(int id)
        {
            var kompanija = _repository.GetById(id);
            if (kompanija == null)
            {
                return NotFound();
            }
            return Ok(kompanija);
        }
    }
}
