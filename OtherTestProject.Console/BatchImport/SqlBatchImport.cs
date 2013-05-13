using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace OtherTestProject.Console.BatchImport
{




    partial class SqlBatchImport
    {
        private static string connstring = "server=192.168.21.107;uid=sa;pwd=cw_0824;database=TempTest2";
        public static void TestMain()
        {
            using (SqlConnection connection = new SqlConnection(connstring))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction("Transaction1");
                DataTable dtTestMain = GetTableSchema("BulkTestMain");//构建BulkTestMain表结构  
                DataTable dtTestDetail = GetTableSchema("BulkTestDetail");//构建BulkTestDetail表结构  
                Guid Batch = Guid.NewGuid();//插入的批次，为后面查询dtTestMainTmp 做条件  
                for (int i = 0; i < 1000; i++)//测试100w条数据  
                {
                    DataRow dr = dtTestMain.NewRow();
                    Guid newGuid = Guid.NewGuid();
                    dr["_GuidId"] = newGuid.ToString();
                    dr["_UserName"] = "测试" + i.ToString();
                    dtTestMain.Rows.Add(dr);

                    for (int j = 0; j < 10; j++)//给从表每次插入10条数据  
                    {
                        DataRow dr1 = dtTestDetail.NewRow();
                        dr1["_GuidId"] = newGuid.ToString();
                        dr1["_Lesson"] = "课程" + j.ToString();
                        dtTestDetail.Rows.Add(dr1);
                    }
                    //这样做的目的，让主表与从表可以临时通过GuidId关联起来  
                }
                BulkToDB(dtTestMain, "BulkTestMain", connection, transaction);//先让BulkTestMain插入了大量的数据，注意这些数据是临时的，在SqlTransaction提交之前，查询时要用with(nolock)  

                DataSet dtTestMainTmp = GetNewImportData(Batch.ToString());//好吧，我们来查询下，刚才大量插入的10w条数据，这里只需要查询标识的2列字段  
                Dictionary<string, long> dicGuidToID = new Dictionary<string, long>();
                foreach (DataRow dr in dtTestMainTmp.Tables[0].Rows)
                {
                    dicGuidToID.Add(dr[1].ToString(), Convert.ToInt64(dr[0]));
                }//dicGuidToID：guid字段与插入的主表ID字段关联起来成字典，用字典是为了访问起来效率（为什么获取字典key的值效率很高，有兴趣的可以去研究“散列表”的概念）  

                foreach (DataRow dr in dtTestDetail.Rows)//现在给dtTestDetail的PId字段赋值(PId字段与主表Id外键关联)  
                {
                    dr["_PId"] = dicGuidToID[dr["_GuidId"].ToString()].ToString();
                }
                dtTestDetail.Columns.Remove("_GuidId");//移除dtTestDetail的GuidId字段，使它与数据库列匹配  
                BulkToDB(dtTestDetail, "BulkTestDetail", connection, transaction);//给从表插入数据  
                transaction.Commit();
                connection.Close();
            }

        }
        /// <summary>  
        /// 根据批次Batch获取导进来的临时数据  
        /// </summary>  
        /// <returns></returns>  
        public static DataSet GetNewImportData(string batch)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [Id],[GuidId]")
                .Append(" FROM BulkTestMain WITH(NOLOCK)");

            SqlHelper helper = new SqlHelper();

            DataSet ds = helper.ExecuteDataset(connstring, CommandType.Text, strSql.ToString());
            return ds;
        }

        public static void BulkToDB(DataTable dtSource, string TableName, SqlConnection connection, SqlTransaction transaction)
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, transaction))
            {
                sqlBulkCopy.DestinationTableName = TableName;//要插入数据的表的名称  
                sqlBulkCopy.BatchSize = dtSource.Rows.Count;//数据的行数  

                List<SqlBulkCopyColumnMapping> mpList = getMapping(TableName);//获取表映射关系  

                foreach (SqlBulkCopyColumnMapping mp in mpList)
                {
                    sqlBulkCopy.ColumnMappings.Add(mp);
                }

                if (dtSource != null && dtSource.Rows.Count != 0)
                {
                    sqlBulkCopy.WriteToServer(dtSource);//插入数据  
                }
            }
        }

        public static List<SqlBulkCopyColumnMapping> getMapping(string TableName)
        {
            List<SqlBulkCopyColumnMapping> mpList = new List<SqlBulkCopyColumnMapping>();
            switch (TableName)
            {
                case "BulkTestMain":
                    {
                        //mpList.Add(new SqlBulkCopyColumnMapping("_Id", "Id"));
                        mpList.Add(new SqlBulkCopyColumnMapping("_GuidId", "GuidId"));
                        //mpList.Add(new SqlBulkCopyColumnMapping("_Batch", "Batch"));
                        mpList.Add(new SqlBulkCopyColumnMapping("_UserName", "UserName"));
                    } break;
                case "BulkTestDetail":
                    {
                        //mpList.Add(new SqlBulkCopyColumnMapping("_Id", "Id"));
                        mpList.Add(new SqlBulkCopyColumnMapping("_PId", "PId"));
                        mpList.Add(new SqlBulkCopyColumnMapping("_Lesson", "Lesson"));
                    } break;
            }
            return mpList;
        }
        private static DataTable GetTableSchema(string TableName)
        {
            DataTable dataTable = new DataTable();
            switch (TableName)
            {
                case "BulkTestMain":
                    {
                        dataTable.Columns.AddRange(new DataColumn[] {    
                                        new DataColumn("_Id",typeof(Int32)),  
                                        new DataColumn("_GuidId",typeof(string)),  
                                        //new DataColumn("_Batch",typeof(string)),  
                                        new DataColumn("_UserName",typeof(String))  
                                    });
                    } break;
                case "BulkTestDetail":
                    {
                        dataTable.Columns.AddRange(new DataColumn[] {    
                                        new DataColumn("_Id",typeof(Int32)),  
                                        new DataColumn("_PId",typeof(Int32)),  
                                        new DataColumn("_GuidId",typeof(string)),  
                                        new DataColumn("_Lesson",typeof(String))});
                    } break;
            }
            return dataTable;
        }
    }
}

namespace OtherTestProject.Console
{
    partial class Program
    {
        private static void SqlBatchImportTest()
        {
            OtherTestProject.Console.BatchImport.SqlBatchImport.TestMain();
        }
    }
}