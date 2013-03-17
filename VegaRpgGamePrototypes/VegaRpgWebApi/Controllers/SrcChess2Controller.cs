using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using log4net;
using System.Reflection;

namespace VegaRpgWebApi.Controllers
{
    [Serializable]
    public class ChessBoardPoco
    {
        public string dummy { get; set; }
    }
    public class SrcChess2Controller : ApiController
    {
        // GET api/values
        
        public string Post(string dummy)
        {
            return "success";
        }        

    }
}
