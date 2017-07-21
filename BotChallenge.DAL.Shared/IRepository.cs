using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.DAL.Shared
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        string Add(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
