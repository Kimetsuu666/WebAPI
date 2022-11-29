using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Repository.IRepository;

namespace WebAPI.Repository;

public class VillaRepository : Repository<Villa>, IVillaRepository
{
    private readonly AppDbContext _db;

    public VillaRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }
    
    public async Task<Villa> UpdateAsync(Villa entity)
    {
        entity.UpdatedTime = DateTime.Now;
        _db.Villas.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}