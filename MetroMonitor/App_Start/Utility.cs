using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MetroMonitor.App_Start
{
    public static class Utility
    {
        public static DataTable MongoDBDocumentListToDataTable(List<BsonDocument> documentList, string tableName = "")
        {
            if (documentList != null && documentList.Count() > 0)
            {

                DataTable dt = new DataTable(tableName);
                foreach (BsonDocument doc in documentList)
                {

                    foreach (BsonElement elm in doc.Elements)
                    {
                        if (!dt.Columns.Contains(elm.Name))
                        {
                            dt.Columns.Add(new DataColumn(elm.Name));
                        }

                    }
                    DataRow dr = dt.NewRow();
                    foreach (BsonElement elm in doc.Elements)
                    {
                        dr[elm.Name] = elm.Value;

                    }
                    dt.Rows.Add(dr);
                }
                return dt;

            }
            return null;
        }
    }
}