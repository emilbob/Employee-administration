using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EvidencijaZaposlenih.Models;


namespace EvidencijaZaposlenih.Interfaces
{
    public interface IZaposlenRepository
    {
        IEnumerable<Zaposlen> GetAll();
        Zaposlen GetById(int id);
        IEnumerable<Zaposlen> GetByGodina(int godina);
        void Add(Zaposlen zaposlen);
        void Update(Zaposlen zaposlen);
        void Delete(Zaposlen zaposlen);
        IEnumerable<Zaposlen> GetByZaposlenje(int pocetak, int kraj);
    }
}