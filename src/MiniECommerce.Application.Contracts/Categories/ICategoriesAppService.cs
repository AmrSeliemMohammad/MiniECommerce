using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MiniECommerce.Categories
{
    public interface ICategoriesAppService : ICrudAppService<CategoryDto, int, PagedAndSortedResultRequestDto, CreateUpdateCategoryDto>
    {

    }
}
