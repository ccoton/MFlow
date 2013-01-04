using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MFlow.Samples.WebApi.Models;

namespace MFlow.Samples.WebApi.Controllers
{
    public class AuditController : ApiController
    {
        public string[] Get([FromUri]CreateAuditEventModel model)
        {
            if (!ModelState.IsValid)
            {

                return ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToArray();
            }

            return new string[] { "Success" };
        }
    }
}
