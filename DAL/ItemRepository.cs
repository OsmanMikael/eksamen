using Eksamen.Models;
using Microsoft.EntityFrameworkCore;

namespace Eksamen.DAL;

public class ItemRepository : IItemRepository
{
    private readonly ProductDbContext _db;
    private readonly ILogger<ItemRepository> _logger;


    public ItemRepository(ProductDbContext db, ILogger<ItemRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<Product>?> GetAll()
        {
            try
            {
                return await _db.Products.ToListAsync();
            }
            catch(Exception e)
            {
                _logger.LogError("[ProductRepository] products ToListAsync() failed when GetAll(), error message: {e}", e.Message);
                return null;
            }
        }
    public async Task<Product?> GetItemById(int id)
        {
            try
            {
                return await _db.Products.FindAsync(id);
            }
            catch(Exception e)
            {
                _logger.LogError("[ProductRepository] product FindAsync(id) failed when GetItemById() for ProductId {ProductId: 0000}, error message: {e}", id, e.Message);
                return null;

            }
           
        }
     public async Task<bool> Create(Product mat)
        {
            try
            {
                _db.Products.Add(mat);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[ProductRepository] product creating failed for mat {@item}, error message: {e}", mat, e.Message);
                return false;
            }
           
        }

     public async Task<bool> Update(Product mat)
        {
            try
            {
                _db.Products.Update(mat);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError("[ProductRepository] product FindAsync(id) failed when updating the ProductId {ProductId: 0000}, error message: {e}", mat, e.Message);
                return false;
            }
           
        }


    public async Task<bool> Delete(int id)
        {
            try
            {
                var mat = await _db.Products.FindAsync(id);
                if (mat == null)
                {
                    _logger.LogError("[ProductRepository] product not found for the ProductId {ProductId: 0000}", id);

                    return false;
                }
                _db.Products.Remove(mat);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError("[ProductRepository] product deletion failed for the ProductId {ProductId: 0000}, error message: {e}", id, e.Message);
                return false;
            }
           
        }

}