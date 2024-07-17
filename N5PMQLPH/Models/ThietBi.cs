namespace N5PMQLPH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThietBi")]
    public partial class ThietBi
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(100)]
        public string TenThietBi { get; set; }

        [StringLength(100)]
        public string LoaiThietBi { get; set; }

        [StringLength(100)]
        public string TrangThai { get; set; }

        [Required]
        [StringLength(20)]
        public string UUIDP { get; set; }

        public virtual Phong Phong { get; set; }
    }
}
