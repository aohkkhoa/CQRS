using Domain.Models.Entities;
using Shared.Wrapper;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// lấy danh sách loại hàng
        /// </summary>
        /// <returns></returns>
        IEnumerable<Category> GetAllCategories();

        /// <summary>
        /// xóa loại hàng
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<Result<int>> DeleteCategory(int categoryId);
    }
}