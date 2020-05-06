using System.Collections.Generic;

namespace ManagementProject.Services
{
    public interface IDataServices<T>
    {
        ICollection<T> GetAllDatas();
    }
}
