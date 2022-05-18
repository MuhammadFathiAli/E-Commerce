using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        //UOW own the DBContext, di in ctor
        //and any repo we use inside uow will be stored inside hashtable repositories 
        private readonly StoreContext context;
        private  Hashtable repositories;


        public UnitOfWork(StoreContext _context)
        { 
            context = _context;
        }


        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            //create new hashtable if no repos created 
            if (repositories == null) repositories = new Hashtable();

            //example to create and return :
            //IGenericRepository<Product> productRepo 
            var type = typeof(TEntity).Name;  //Product

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>); //Product
                var repositoryInstance = Activator.CreateInstance(repositoryType.
                    MakeGenericType(typeof(TEntity)), context);
                repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)repositories[type]; 

        }
        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
