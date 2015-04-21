using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Shared;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System.Data;

namespace MetroMonitor.App_Start
{
    public class DataAccess : IDisposable
    {
        private MongoServer server;
        private MongoDatabase database;
        private System.Configuration.Configuration webConfigInstance;

        public DataAccess()
        {
            // get the web config data
            this.webConfigInstance = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
            this.CreateConnection();
        }

        public void CreateConnection()
        {
            // set up the mongo server
            MongoServerSettings settings = new MongoServerSettings();
            string mongoServerName = "jonfastofficial.com"; //this.webConfigInstance.AppSettings.Settings["MongoServer"].Value;
            string mongoPortName = "27017"; //this.webConfigInstance.AppSettings.Settings["MongoPort"].Value;
            string mongoDatabaseName = "MetroData"; //this.webConfigInstance.AppSettings.Settings["MongoDatabase"].Value;
            
            // server instance
            settings.Server = new MongoServerAddress(mongoServerName, int.Parse(mongoPortName));
            this.server = new MongoServer(settings);
            
            // database instance
            this.database = server.GetDatabase(mongoDatabaseName);
        }

        public void DestroyConnection()
        {
            this.database = null;
            this.server.Disconnect();
            this.server = null;
        }

        public DataTable FindAllLimit(string documentName, int limit = -1)
        {
            var results = new List<BsonDocument>();
            if (this.database != null)
            {
                if (limit > 0)
                {
                    results.AddRange(database.GetCollection(documentName).FindAll().SetLimit(limit));
                }
                else
                {
                    results.AddRange(database.GetCollection(documentName).FindAll());
                }
            }
            return Utility.MongoDBDocumentListToDataTable(results, documentName);
        }

        public void Dispose()
        {
            this.DestroyConnection();
        }
    }

    public static class DataAccessFactory
    {
        static DataAccess GetDataAccess()
        {
            try
            {
                return new DataAccess();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}