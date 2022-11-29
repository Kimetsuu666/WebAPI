using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Repository.IRepository;

namespace WebAPI.Repository;

public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
{
    private readonly AppDbContext _db;

    public VillaNumberRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }
    
    public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
    {
        entity.UpdatedTime = DateTime.Now;
        _db.VillaNumbers.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}