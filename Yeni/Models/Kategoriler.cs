using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
namespace Yeni.Models
{
    public partial class Kategoriler
    {
        public Kategoriler()
        {
            Urunlers = new HashSet<Urunler>();
        }

        public int KategoriId { get; set; }

        [Required(ErrorMessage ="Kategori Adı Boş Olamaz")]
        
        public string KategoriAdi { get; set; } 
        public string? KategoriResmi { get; set; }
   
        public virtual ICollection<Urunler> Urunlers { get; set; }
    }
}
