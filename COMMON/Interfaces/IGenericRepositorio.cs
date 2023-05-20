using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using COMMON.Entidades;

namespace COMMON.Interfaces
{
    public interface IGenericRepositorio<T> where T : Base

    {
        string Error { get; }
        IEnumerable<T> Get { get; }

        T GetById(string id);
        T Insert(T entity);
        T Update(T entity);
        bool Delete(string id);

        IEnumerable<T> Query(Expression<Func<T, bool>> predicado);

    }
}

