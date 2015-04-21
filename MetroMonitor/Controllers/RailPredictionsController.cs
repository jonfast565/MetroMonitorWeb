using MetroMonitor.App_Start;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MetroMonitor.Controllers
{
    public class RailPredictionsController : ApiController
    {
        // GET api/<controller>
        public DataTable Get()
        {
            DataAccess database = new DataAccess();
            return database.FindAllLimit("RailPredictions", 100);
        }
    }
}