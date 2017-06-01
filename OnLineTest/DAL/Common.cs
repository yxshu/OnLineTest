using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;//导入命名空间

namespace OnLineTest.DAL
{
    public class Common
    {
        /// <summary>
        /// 用数据行的数据填充实体对象
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <param name="obj">实体对象</param>
        /// <returns>是否成功</returns>
        public static bool dr2model(DataRow dr, object obj)
        {
            if (dr != null && obj != null)
            {
                foreach (PropertyInfo Property in obj.GetType().GetProperties())
                {
                    try
                    {
                        //验证数据行中是否存在属性所对应的列并且值不为空
                        if (dr.Table.Columns.Contains(Property.Name) && dr[Property.Name] != DBNull.Value)
                        {
                            //将数据行中读取出来的数据填充到对象实体的属性里（这里存在一个数据属性匹配的问题）
                            //这个方法吊爆了，它居然可以自动帮我匹配数据类型，其中起作用的是最后一个参数 null表示使用默认匹配方法，当然这个应该可以重写方法啦
                            Property.SetValue(obj, dr[Property.Name], null);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 将datarow一行数据转化为一个字典对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Dictionary<string, object> dr2dictionary(DataRow dr)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (dr != null)
            {
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    string colname = dr.Table.Columns[i].ColumnName;
                    dic.Add(colname, dr[colname]);
                }

            }
            else
            {
                dic = null;
            }
            return dic;
        }

        /// <summary>
        /// 利用sqldatareader 填充dataset
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static DataSet ConvertDataReaderToDataSet(SqlDataReader reader)
        {
            DataSet dataSet = new DataSet();
            do
            {
                // Create new data table
                DataTable schemaTable = reader.GetSchemaTable();
                DataTable dataTable = new DataTable();

                if (schemaTable != null)
                {
                    // A query returning records was executed

                    for (int i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        DataRow dataRow = schemaTable.Rows[i];
                        // Create a column name that is unique in the data table
                        string columnName = (string)dataRow["ColumnName"]; //+ " // Add the column definition to the data table
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }

                    dataSet.Tables.Add(dataTable);

                    // Fill the data table we just created

                    while (reader.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();

                        for (int i = 0; i < reader.FieldCount; i++)
                            dataRow[i] = reader.GetValue(i);

                        dataTable.Rows.Add(dataRow);
                    }
                }
                else
                {
                    // No records were returned

                    DataColumn column = new DataColumn("RowsAffected");
                    dataTable.Columns.Add(column);
                    dataSet.Tables.Add(dataTable);
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = reader.RecordsAffected;
                    dataTable.Rows.Add(dataRow);
                }
            }
            while (reader.NextResult());
            return dataSet;
        }
        /// <summary>
        /// 利用datareader填充datatable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static DataTable ConvertDataReaderToDataTable(SqlDataReader reader)
        {

            DataTable dt = new DataTable();
            DataTable schemaTable = reader.GetSchemaTable();
            if (schemaTable != null)
            {
                for (int i = 0; i < schemaTable.Rows.Count; i++)//为表填充结构
                {
                    DataRow dataRow = schemaTable.Rows[i];
                    // Create a column name that is unique in the data table
                    string columnName = (string)dataRow["ColumnName"]; //+ " // Add the column definition to the data table
                    DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                    dt.Columns.Add(column);
                }
                do//如果有多个结果集，则全部填到同一个表中
                {
                    while (reader.Read())//为表填充数据
                    {
                        DataRow dataRow = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                            dataRow[i] = reader.GetValue(i);
                        dt.Rows.Add(dataRow);
                    }
                }
                while (reader.NextResult());
            }
            else
            {
                // No records were returned

                DataColumn column = new DataColumn("RowsAffected");
                dt.Columns.Add(column);
                DataRow dataRow = dt.NewRow();
                dataRow[0] = reader.RecordsAffected;
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

    }
}
