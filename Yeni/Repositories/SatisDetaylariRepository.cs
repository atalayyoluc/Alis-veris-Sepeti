using Microsoft.EntityFrameworkCore;
using Yeni.Models;

namespace Yeni.Repositories
{
    public class SatisDetaylariRepository:GenericRepository<SatisDetaylari>
    {
        AlısVerisContext context = new AlısVerisContext();

       
    }
}
