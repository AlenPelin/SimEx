using SIM.Tool.Base;
using System.Web;
using System.Web.Http;

namespace SIM.Service
{
  public class WebApiApplication : HttpApplication
  {
    protected void Application_Start()
    {
      GlobalConfiguration.Configure(WebApiConfig.Register);
    }
  }
}
