namespace N5PMQLPH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhanCong")]
    public partial class PhanCong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDPC { get; set; }

        [Required]
        [StringLength(40)]
        public string IDGV { get; set; }

        [Required]
        [StringLength(20)]
        public string UUIDP { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public virtual GiangVien GiangVien { get; set; }

        public virtual Phong Phong { get; set; }
    }
}
