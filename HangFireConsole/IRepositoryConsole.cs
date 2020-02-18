using System;
using System.Collections.Generic;
using System.Text;

namespace HangFireConsole
{
    public interface IRepositoryConsole
    {
        List<T> GetRecords<T>();
    }
}
