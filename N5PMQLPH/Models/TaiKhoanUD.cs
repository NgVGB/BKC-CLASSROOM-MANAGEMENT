namespace N5PMQLPH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoanUD")]
    public partial class TaiKhoanUD
    {
        [StringLength(40)]
        public string ID { get; set; }

        [StringLength(40)]
        public string TenDangNhap { get; set; }

        [StringLength(100)]
        public string TenNhanVien { get; set; }

        [StringLength(40)]
        public string MatKhau { get; set; }

        [StringLength(100)]
        public string Quyen { get; set; }
    }
}
