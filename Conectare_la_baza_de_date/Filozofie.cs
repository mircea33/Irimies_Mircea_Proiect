namespace Conectare_la_baza_de_date
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Filozofie")]
    public partial class Filozofie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Filozofie()
        {
            Alegeri_utilizatori = new HashSet<Alegeri_utilizatori>();
        }

        [Key]
        public int Filozofie_Id { get; set; }

        [StringLength(50)]
        public string Denumire { get; set; }

        [StringLength(50)]
        public string Fondator { get; set; }

        [StringLength(50)]
        public string Perioada { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alegeri_utilizatori> Alegeri_utilizatori { get; set; }
    }
}
