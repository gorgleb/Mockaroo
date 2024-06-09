using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MockarooLibrary.Helpers;
using MockarooLibrary.Model;
using System.Diagnostics;
using Table = MockarooLibrary.Model.Table;

namespace MockarooWebApi.Helpers
{
    public static class TableConverter
    {
        public static Table ConvertToTable(string tableName, int rowCount, List<(string, string)> parameters)
        {
            var table = new Table(tableName, rowCount);

            foreach (var item in parameters)
            {
                var entity = TextToDataTypeConversionHelper.ConvertClassNameToDataClass(item.Item1, item.Item2);
                table.AddNewEntityToTableSchema(entity);
            }

            return table;
        }
    }
}