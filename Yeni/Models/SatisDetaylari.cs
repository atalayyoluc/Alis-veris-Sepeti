using System;
using System.Collections.Generic;

namespace Yeni.Models
{
    public partial class SatisDetaylari
    {
        public int SatisId { get; set; }
        public int UrunId { get; set; }

        public short UrunMiktari { get; set; }

        public virtual Satisler Satis { get; set; } = null!;
        public virtual Urunler Urun { get; set; } = null!;
    }
}
