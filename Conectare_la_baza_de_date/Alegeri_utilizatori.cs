namespace Conectare_la_baza_de_date
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Alegeri_utilizatori
    {
        [Key]
        public int Alegeri_Utilizatori_Id { get; set; }

        public int Utilizator_Id { get; set; }

        public int Filozofie_Id { get; set; }

        public virtual Filozofie Filozofie { get; set; }

        public virtual Utilizator Utilizator { get; set; }
    }
}
