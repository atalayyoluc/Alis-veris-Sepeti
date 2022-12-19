using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yeni.Models
{
    public partial class Urunler
    {
        public Urunler()
        {
            SatisDetaylaris = new HashSet<SatisDetaylari>();
        }

        public int UrunId { get; set; }


        public string UrunAdi { get; set; } = null!;
        public int KategoriId { get; set; }
        public string? UrunAciklamasi { get; set; }
        public string? UrunResmi { get; set; }

        public virtual Kategoriler Kategori { get; set; } = null!;
        public virtual ICollection<SatisDetaylari> SatisDetaylaris { get; set; }
    }
}
