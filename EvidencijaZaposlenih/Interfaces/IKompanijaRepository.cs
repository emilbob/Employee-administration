using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EvidencijaZaposlenih.Models;

namespace EvidencijaZaposlenih.Interfaces
{
    public interface IKompanijaRepository
    {
        IEnumerable<Kompanija> GetAll();
        Kompanija GetById(int id);
        ///*IEnumerable<Kompanija> GetTradicija()*/;
    }
}