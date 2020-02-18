using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Repository
{
    public interface IRepositoryManagement
    {
        T Get<T>(string Id);
        T Create<T>(T record);
        void UpdateInfo<T>(string Id, T record);
    }
}
