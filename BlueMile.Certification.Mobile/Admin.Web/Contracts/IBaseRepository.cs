using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueMile.Certification.Admin.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Get(string url, int id);

        Task<IList<T>> Get(string url);

        Task<bool> Create(string url, T newEntity);

        Task<bool> Update(string url, T updatedEntity, int id);

        Task<bool> Delete(string url, int id);
    }
}
