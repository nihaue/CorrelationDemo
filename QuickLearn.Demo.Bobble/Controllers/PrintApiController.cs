using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TRex.Metadata;

namespace QuickLearn.Demo.Bobble.Controllers
{

    [RoutePrefix("api")]
    public class PrintApiController : ApiController
    {
        public static Dictionary<string, Tuple<string,string>> BobbleState = new Dictionary<string, Tuple<string,string>>();

        [Metadata("Submit 3D Print Job", "Request Bobblehead Head", VisibilityType.Important)]
        [HttpPost, Route("print")]
        
        public IHttpActionResult SubmitPrintJob(string JobId, string Head)
        {
            BobbleState[JobId] = new Tuple<string,string>(Head, "None");
            return Ok();
        }

        [Metadata("Update 3D Print Job", "Body is Ready for Bobblehead Head", VisibilityType.Important)]
        [HttpPost, Route("printupdate")]
        public IHttpActionResult UpdatePrintJob(string JobId, string Body)
        {
            BobbleState[JobId] = new Tuple<string, string>(BobbleState[JobId].Item1, Body);
            return Ok();
        }
        
    }
}
