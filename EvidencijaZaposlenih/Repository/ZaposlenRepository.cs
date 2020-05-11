using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EvidencijaZaposlenih.Models;
using EvidencijaZaposlenih.Interfaces;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;


namespace EvidencijaZaposlenih.Repository
{
    public class ZaposleniRepository : IDisposable, IZaposlenRepository
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

        public IEnumerable<Zaposlen> GetAll()
        {
            return db.zaposleni.Include(z => z.Kompanija).OrderByDescending(z => z.Plata);

        }

        public Zaposlen GetById(int id)
        {
            return db.zaposleni.Include(f => f.Kompanija).FirstOrDefault(f => f.Id == id);

        }

        public IEnumerable<Zaposlen> GetByGodina(int godina)
        {
            return db.zaposleni.Include(z => z.Kompanija).Where(k => k.GodinaRodjenja >= godina).OrderByDescending(k => k.GodinaRodjenja);
        }
        public void Add(Zaposlen zaposlen)
        {
            db.zaposleni.Add(zaposlen);
            db.SaveChanges();
        }

        public void Update(Zaposlen zaposlen)
        {
            db.Entry(zaposlen).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
        public void Delete(Zaposlen zaposlen)
        {
            db.zaposleni.Remove(zaposlen);
            db.SaveChanges();
        }

        public IEnumerable<Zaposlen> GetByZaposlenje(int pocetak, int kraj)
        {
            return db.zaposleni.Include(f => f.Kompanija).Where(f => f.GodinaZaposlenja >= pocetak && f.GodinaZaposlenja <= kraj).OrderBy(f => f.GodinaZaposlenja);
        }

    }
}