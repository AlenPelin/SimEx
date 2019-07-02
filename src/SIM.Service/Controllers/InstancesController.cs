using SIM.Core.Commands;
using SIM.IO;
using SIM.IO.Real;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Hosting;
using System.IO;

namespace SIM.Service.Controllers
{
  public class InstancesController : ApiController
  {
    private static IFileSystem FileSystem { get; } = new RealFileSystem();
    private static string AppDataPath { get; } = HostingEnvironment.MapPath("/App_Data");

    public InstancesController()
    {
    }

    // GET api/values
    public HttpResponseMessage Get([FromUri] string token)
    {
      if (WebApiApplication.Token != token)
      {
        return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new HttpError("Token is missing or invalid"));
      }

      var result = new ListCommand(FileSystem) { Detailed = true, Everywhere = true }.Execute();

      return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
    }


    // GET api/values/5
    public HttpResponseMessage Get(string id, [FromUri] string token)
    {
      if (WebApiApplication.Token != token)
      {
        return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new HttpError("Token is missing or invalid"));
      }

      var result = new ListCommand(FileSystem) { Detailed = true, Everywhere = true, Filter = id }.Execute();

      return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
    }

    // POST api/values
    public HttpResponseMessage Post([FromBody] InstallCommandEx command, [FromUri] string token)
    {
      if (WebApiApplication.Token != token)
      {
        return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new HttpError("Token is missing or invalid"));
      }

      var result = command.Execute();

      return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
    }

    public class InstallCommandEx : InstallCommand
    {
      private IProfile _Profile;

      public InstallCommandEx() 
        : base(InstancesController.FileSystem)
      {
      }

      protected override IProfile Profile => _Profile ?? (_Profile = SIM.Core.Common.Profile.Read(FileSystem, Path.Combine(AppDataPath, "profile.xml")));
    }

    // DELETE api/values/5
    public HttpResponseMessage Delete(string id, [FromUri] string token)
    {
      if (WebApiApplication.Token != token)
      {
        return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new HttpError("Token is missing or invalid"));
      }

      var result = new DeleteCommand(FileSystem) { Name = id }.Execute();

      return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
    }
  }
}
