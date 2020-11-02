using BlueMile.Coc.Data;
using BlueMile.Coc.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Coc.WebApi.Services
{
    public class OwnerRepository : IOwnerRepository
    {
        private List<OwnerEntity> ownerEntities;

        public OwnerRepository()
        {
            InitializeData();
        }

        public IEnumerable<OwnerEntity> FindAllOwners
        {
            get { return ownerEntities; }
        }

        public bool DoesOwnerExist(Guid id)
        {
            return ownerEntities.Any(item => item.Id == id);
        }

        public OwnerEntity FindOwner(Guid id)
        {
            return ownerEntities.FirstOrDefault(item => item.Id == id);
        }

        public void Insert(OwnerEntity owner)
        {
            ownerEntities.Add(owner);
        }

        public void Update(OwnerEntity owner)
        {
            var OwnerEntity = this.FindOwner(owner.Id);
            var index = ownerEntities.IndexOf(OwnerEntity);
            ownerEntities.RemoveAt(index);
            ownerEntities.Insert(index, owner);
        }

        public void Delete(Guid id)
        {
            ownerEntities.Remove(this.FindOwner(id));
        }

        private void InitializeData()
        {
            ownerEntities = new List<OwnerEntity>();
        }
    }
}
