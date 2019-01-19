using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Repositories
{
    public interface IRepository<T>
    {
        T Get(int id);
        T Save(T obj);
        List<T> GetAll();
        List<T> GetAllWithFilter(ArrayList filters);
        bool Exists(ArrayList list);
    }
}
