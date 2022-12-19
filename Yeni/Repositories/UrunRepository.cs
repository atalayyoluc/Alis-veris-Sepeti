using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using Yeni.Models;

namespace Yeni.Repositories
{
    public class UrunRepository:GenericRepository<Urunler>
    {
        AlısVerisContext context = new AlısVerisContext();
     
    }
}
