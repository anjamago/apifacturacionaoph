using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.OpheliaTest.Entities.Interface.Repository
{
    public interface IBaseRepositoryOPH<T> : IBaseRepository<T> where T : class
    {
    }
}
