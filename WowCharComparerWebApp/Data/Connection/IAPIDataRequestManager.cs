using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowCharComparerWebApp.Models.Abstract;

namespace WowCharComparerWebApp.Data.Connection
{
    public interface IAPIDataRequestManager
    {
        Task<T> GetDataByHttpRequest<T>(Uri uriAddress) where T : CommonAPIResponse, new();
    }
}
