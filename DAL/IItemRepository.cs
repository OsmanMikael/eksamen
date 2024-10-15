using Eksamen.Models;

namespace Eksamen.DAL;

public interface IItemRepository
{
	Task<IEnumerable<Product>?> GetAll();
    Task<Product?> GetItemById(int id);
	Task<bool> Create(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(int id);
}