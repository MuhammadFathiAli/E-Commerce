using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Idisposal to be implemented and dispose our context when we finish transactions in uow 

        //template of repos that will be used in UOW
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        
        //return number of changes to our DB, EF will track all changes of entites happened inside UOW
        // then complete method run to save changes to DB and return number of changes 
        Task<int> Complete();
    }
}
 