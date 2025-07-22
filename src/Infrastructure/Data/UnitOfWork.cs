using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHAFacilitationAPIs.Application.Interface.Repository;

namespace DHAFacilitationAPIs.Infrastructure.Data;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public UnitOfWork(ApplicationDbContext db) => _db = db;
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}
