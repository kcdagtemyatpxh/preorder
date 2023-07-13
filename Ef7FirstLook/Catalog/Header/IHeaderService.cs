using Ef7FirstLook.Catalog.Header.Dtos;

namespace Ef7FirstLook.Catalog.Header
{
    public interface IHeaderService
    {
        Task<int> Create(HeaderCreateRequest request);

        Task<int> Update(HeaderCreateRequest request);

        Task<int> Delete(int id);

        Task<List<HeaderViewModel>> GetAll();
        Task<List<HeaderViewModel>> GetDetail(int id);
    }
}
