using Microsoft.EntityFrameworkCore;
using Yeni.Models;

namespace Yeni.Repositories
{
    public class GenericRepository<T> where T : class,new()
    {
        AlısVerisContext context=new AlısVerisContext();

        public List<T> TList()
        {
            return context.Set<T>().ToList();
        }
        public void TEkle(T p)
        {

            context.Set<T>().Add(p);
            context.SaveChanges();
        }
        public void Tsil(T p)
        {
            context.Set<T>().Remove(p);
            context.SaveChanges();
        }
        public void TGuncelle(T p)
        {
            context.Set<T>().Update(p);
            context.SaveChanges();
        }
        public T TGET(int id)
        {
          return context.Set<T>().Find(id);
        }
        public List<T> TList(string p)
        {

            return context.Set<T>().Include(p).ToList();
        }






    }
}
