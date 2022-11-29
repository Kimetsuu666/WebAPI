using System.Linq.Expressions;
using WebAPI.Models;

namespace WebAPI.Repository.IRepository;

public interface IVillaNumberRepository : IRepository<VillaNumber>
{ 
    Task<VillaNumber> UpdateAsync(VillaNumber entity);
}