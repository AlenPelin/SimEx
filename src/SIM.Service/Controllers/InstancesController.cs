using SIM.Core.Commands;
using SIM.Instances;
using SIM.IO;
using SIM.IO.Real;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SIM.Service.Controllers
{
  public class InstancesController : ApiController
  {
    private IFileSystem FileSystem { get; } = new RealFileSystem();

    public InstancesController()
    {
    }

    // GET api/values
    public HttpResponseMessage Get()
    {
      var result = new ListCommand(FileSystem) { Detailed = true, Everywhere = true }.Execute();

      return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
    }


    // GET api/values/5
    public object Get(string id)
    {
      return new ListCommand(FileSystem) { Detailed = true, Everywhere = true, Filter = id }.Execute();
    }

    // POST api/values
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    public void Delete(int id)
    {
    }
  }
}
