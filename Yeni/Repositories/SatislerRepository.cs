using Yeni.Models;

namespace Yeni.Repositories
{
    public class SatislerRepository:GenericRepository<Satisler>
    {
        AlısVerisContext context = new AlısVerisContext();

        public List<Satisler> GetSatisler(int id)
        {
            
            return context.Satislers.Where(a => a.KullaniciId == id).ToList();
        }
    }
}
