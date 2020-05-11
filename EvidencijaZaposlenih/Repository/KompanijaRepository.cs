using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EvidencijaZaposlenih.Interfaces;
using EvidencijaZaposlenih.Models;

namespace EvidencijaZaposlenih.Repository
{


    public class KompanijaRepository : IDisposable, IKompanijaRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Kompanija> GetAll()
        {
            return db.kompanije;
        }
        public Kompanija GetById(int id)
        {
            return db.kompanije.FirstOrDefault(k => k.Id == id);
        }

        //public IEnumerable<Kompanija> GetTradicija()
        //{
        //    IEnumerable<Kompanija> kompanije = GetAll();

        //    List<Kompanija> rezultat = new List<Kompanija>();

        //    var min = kompanije.OrderBy(k => k.GodinaOsnivanja).Take(1).ToList();
        //    var max = kompanije.OrderByDescending(k => k.GodinaOsnivanja).Take(1);

        //    rezultat.
        //}

    }
}