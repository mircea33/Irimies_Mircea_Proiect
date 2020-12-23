using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Conectare_la_baza_de_date
{
    public partial class Entitatiile_bazei_mele_de_date : DbContext
    {
        public Entitatiile_bazei_mele_de_date()
            : base("name=Entitatiile_bazei_mele_de_date")
        {
        }

        public virtual DbSet<Alegeri_utilizatori> Alegeri_utilizatori { get; set; }
        public virtual DbSet<Filozofie> Filozofies { get; set; }
        public virtual DbSet<Utilizator> Utilizators { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
