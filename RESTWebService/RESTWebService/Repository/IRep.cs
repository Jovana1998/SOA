using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTWebService.Model;

namespace RESTWebService.Repository
{
    interface IRep
    {
        Task AddDataAsync(DataModel dm);
        Task<IList<DataModel>> GetAllDataAsync();
        Task<DataModel> GetDataByIdAsync(string id);
        Task<DataModel> GetDataByValueAsync(string value);
        Task<DataModel> GetDataByTypeAsync(string type);
        Task RemoveDataAsync();
        Task RemoveDataByIdAsync(string id);
        Task RemoveDataByValueAsync(string value);
        Task ModifyDataAsync(DataModel dm);
        Task ModifyDataByIdAsync(string id, string value);
    }
}
