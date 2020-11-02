using BlueMile.Coc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Coc.WebApi.Interfaces
{
    public interface IOwnerRepository
    {
        bool DoesOwnerExist(Guid id);
        IEnumerable<OwnerEntity> FindAllOwners { get; }
        OwnerEntity FindOwner(Guid id);
        void Insert(OwnerEntity item);
        void Update(OwnerEntity item);
        void Delete(Guid id);
    }
}
