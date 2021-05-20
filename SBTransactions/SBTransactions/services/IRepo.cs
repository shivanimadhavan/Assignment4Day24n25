using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBTransactions.services
{
    interface IRepo<T>
    {
        void Get(T t);
        void Add(T t);
        void Update(int id, T t);
        void Delete(T t);
    }
}
