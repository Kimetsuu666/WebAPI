using System.Linq.Expressions;
using WebAPI.Models;

namespace WebAPI.Repository.IRepository;

public interface IVillaRepository : IRepository<Villa>
{ 
    Task<Villa> UpdateAsync(Villa entity);
}