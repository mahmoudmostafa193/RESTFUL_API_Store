using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParamters specificationsparamters);
        Task<ProductResultDto?> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();


    }

}
