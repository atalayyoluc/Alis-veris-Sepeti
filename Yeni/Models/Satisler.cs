using System;
using System.Collections.Generic;

namespace Yeni.Models
{
    public partial class Satisler
    {
        public Satisler()
        {
            SatisDetaylaris = new HashSet<SatisDetaylari>();
        }

        public int SatisId { get; set; }
        public int KullaniciId { get; set; }
        public string ?SatisAdi { get; set; }
        public bool AlisVerisKontrol { get; set; }
        public virtual Kullaniciler Kullanici { get; set; } = null!;
        public virtual ICollection<SatisDetaylari> SatisDetaylaris { get; set; }
    }
}
