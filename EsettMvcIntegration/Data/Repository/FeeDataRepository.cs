using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EsettMvcIntegration.Data;
using EsettMvcIntegration.Models;

public class FeeDataRepository
{
    private readonly ApplicationDbContext _context;

    public FeeDataRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Add a new fee to the database
    public async Task AddFeeAsync(FeeDataModel fee)
    {
        _context.FeeData.Add(fee);
        await _context.SaveChangesAsync();
    }

    // Retrieve a fee by its ID
    public async Task<FeeDataModel> GetFeeByIdAsync(int id)
    {
        return await _context.FeeData.FindAsync(id);
    }

    // Retrieve all fees from the database
    public async Task<List<FeeDataModel>> GetAllFeesAsync()
    {
        return await _context.FeeData.ToListAsync();
    }
}
