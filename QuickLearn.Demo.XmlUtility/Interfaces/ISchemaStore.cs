using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLearn.Demo.XmlUtility
{
    public interface ISchemaStore
    {

        Task<string[]> GetSchemaListAsync();

        Task<string> GetSchemaAsync(string schemaName);

    }
}
