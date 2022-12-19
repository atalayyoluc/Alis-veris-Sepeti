using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace Yeni.Models
{
    public partial class Kullaniciler
    {
        public Kullaniciler()
        {
            Satislers = new HashSet<Satisler>();
        }

        public int KullaniciId { get; set; }
        public string Eposta { get; set; } = null!;
 
        public string Sifre { get; set; } = null!;
        public string Adi { get; set; } = null!;
        public string Soyadi { get; set; } = null!;

        public virtual ICollection<Satisler> Satislers { get; set; }
    }
}
