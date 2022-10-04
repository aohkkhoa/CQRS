using Domain.Models.DTO;

namespace Application.Interfaces
{
    public interface IStorageRepository
    {

        /// <summary>
        /// kiểm tra số lượng trong kho còn để order hay không
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        string HandleQuantityStorage(int bookId, int quantity);


        /// <summary>
        /// lấy tất thông tin mặt hàng có trong kho
        /// </summary>
        /// <returns></returns>
        IEnumerable<StorageUnit> GetAllStorage();
    }
}
