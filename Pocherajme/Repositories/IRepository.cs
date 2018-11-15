using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Repositories
{
    public interface IRepository<T>
    {
        T Get(int id);
        void Save(T obj);
        List<T> GetAll();
    }
}
