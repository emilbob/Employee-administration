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
    public class ZaposleniController : ApiController
    {
        IZaposlenRepository _repository { get; set; }

        public ZaposleniController(IZaposlenRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<Zaposlen> Get()
        {
            return _repository.GetAll();
        }

        
        public IHttpActionResult Get(int id)
        {
            var zapolsen = _repository.GetById(id);
            if (zapolsen == null)
            {
                return NotFound();
            }
            return Ok(zapolsen);
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<Zaposlen> GetByGodiste(int godina)
        {
            return _repository.GetByGodina(godina);
        }

        public IHttpActionResult Post(Zaposlen zaposlen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(zaposlen);
            return CreatedAtRoute("DefaultApi", new { id = zaposlen.Id }, zaposlen);
        }

        [Authorize]
        
        public IHttpActionResult PutId(int id, Zaposlen zaposlen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != zaposlen.Id)
            {
                return BadRequest();
            }
            try
            {
                _repository.Update(zaposlen);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(zaposlen);

        }


        //[Authorize]
        public IHttpActionResult Delete(int id)
        {
            var zaposlen = _repository.GetById(id);
            if (zaposlen == null)
            {
                return NotFound();
            }
            _repository.Delete(zaposlen);
            return Ok(zaposlen);
        }
        [Authorize]
        [HttpGet]
        [Route("api/pretraga/")]
        public IEnumerable<Zaposlen> GetByZaposlenje(int pocetak, int kraj)
        {
            return _repository.GetByZaposlenje(pocetak, kraj);
        }
    }
}
