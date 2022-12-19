namespace Yeni.DTO
{
    public class UrunEkleDTO
    {
        public int UrunId { get; set; }


        public string UrunAdi { get; set; } = null!;
        public int KategoriId { get; set; }
        public string? UrunAciklamasi { get; set; }
        public IFormFile UrunResmi { get; set; }

    }
}
