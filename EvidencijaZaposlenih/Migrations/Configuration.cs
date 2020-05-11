namespace EvidencijaZaposlenih.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EvidencijaZaposlenih.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EvidencijaZaposlenih.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EvidencijaZaposlenih.Models.ApplicationDbContext context)
        {
            context.kompanije.AddOrUpdate(x => x.Id,

                 new Kompanija() { Id = 1, Naziv = "Google", GodinaOsnivanja = 1998 },
                 new Kompanija() { Id = 2, Naziv = "Apple", GodinaOsnivanja = 1976 },
                 new Kompanija() { Id = 3, Naziv = "Microsoft", GodinaOsnivanja = 1975 });

            context.zaposleni.AddOrUpdate(x => x.Id,

                new Zaposlen() { Id = 1, ImePrezime = "Pera Peric", GodinaRodjenja = 1980, GodinaZaposlenja = 2008, Plata = 3000, KompanijaId = 1 },
                new Zaposlen() { Id = 2, ImePrezime = "Mika Mikic", GodinaRodjenja = 1976, GodinaZaposlenja = 2005, Plata = 6000, KompanijaId = 1 },
                new Zaposlen() { Id = 3, ImePrezime = "Iva Ivic", GodinaRodjenja = 1990, GodinaZaposlenja = 2016, Plata = 4000, KompanijaId = 2 },
                new Zaposlen() { Id = 4, ImePrezime = "Zika Zikic", GodinaRodjenja = 1985, GodinaZaposlenja = 2005, Plata = 5000, KompanijaId = 2 },
                new Zaposlen() { Id = 5, ImePrezime = "Sara Saric", GodinaRodjenja = 1982, GodinaZaposlenja = 2007, Plata = 5500, KompanijaId = 3 });
        }
    }
}
