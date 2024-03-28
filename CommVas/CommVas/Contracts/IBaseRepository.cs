using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommVas.Contracts
{
    public enum ErrorCode
    {
        Sucess,
        Error
    }
    public interface IBaseRepository<T>
    {
        T Get(object id);
        List<T> GetAll();
        ErrorCode Create(T t);
        ErrorCode Update(object id, T t);
        ErrorCode Delete(object id);
    }
}