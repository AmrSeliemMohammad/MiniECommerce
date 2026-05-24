using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MiniECommerce.Categories
{
    [Authorize(Roles = MiniECommerceConsts.AdminRole)]
    public class CategoriesAppService : CrudAppService<Category, CategoryDto, int, PagedAndSortedResultRequestDto, CreateUpdateCategoryDto>, ICategoriesAppService
    {
        public CategoriesAppService(IRepository<Category, int> repository) : base(repository)
        {
        }
    }
}
