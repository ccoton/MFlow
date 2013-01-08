using System.Web.Http;
using MFlow.Samples.WebApi.Models;
using System.Linq;

namespace MFlow.Samples.WebApi.Controllers
{
    public class AuditController : ApiController
    {
        public string[] Get([FromUri]CreateAuditEventModel model)
        {
            if (!ModelState.IsValid)
                return ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToArray();

            return new string[] { "Success" };
        }
    }
}
