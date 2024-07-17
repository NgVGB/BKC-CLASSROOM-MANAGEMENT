namespace N5PMQLPH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phong")]
    public partial class Phong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phong()
        {
            PhanCongs = new HashSet<PhanCong>();
            ThietBis = new HashSet<ThietBi>();
        }

        [Key]
        [StringLength(20)]
        public string UUIDP { get; set; }

        [StringLength(100)]
        public string TenPhong { get; set; }

        public int IDPhong { get; set; }

        [StringLength(40)]
        public string Khu { get; set; }

        [StringLength(40)]
        public string Lau { get; set; }

        [StringLength(100)]
        public string TrangThai { get; set; }

        public virtual LoaiPhong LoaiPhong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanCong> PhanCongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThietBi> ThietBis { get; set; }
    }
}
