using Demo.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Demo.Server.Products;

public class ProductService(AppDbContext db)
{
    public async Task<List<ProductDto>> GetProducts(CancellationToken ctn = default)
    {
        return await db.Products
            .AsNoTracking()
            .Select(x=> new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price
            })
            .ToListAsync(ctn);
    }
}