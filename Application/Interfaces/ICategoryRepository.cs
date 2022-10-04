using Domain.Models.Entities;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {

        /// <summary>
        /// xóa loại hàng
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        int DeleteCategory(int categoryId);


        /// <summary>
        /// lấy danh sách loại hàng
        /// </summary>
        /// <returns></returns>
        IEnumerable<Category> GetAllCategories();
    }
}
