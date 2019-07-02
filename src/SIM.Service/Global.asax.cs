using log4net.Core;
using log4net.Layout;
using log4net.Util;
using SIM.Core;
using SIM.Core.Logging;
using SIM.Products;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Http;

namespace SIM.Service
{
  public class WebApiApplication : HttpApplication
  {
    internal static string Token = ConfigurationManager.AppSettings["token"] ?? throw new Exception("No 'token' app setting specified");

    protected void Application_Start()
    {
      var appData = Server.MapPath("/App_Data");
      Directory.CreateDirectory(appData);

      ManifestHelper._CustomManifestsLocations = new[] {new ManifestHelper.LookupFolder(Server.MapPath("/Manifests"), true)};

      CacheManager.ClearAll();

      var info = new LogFileAppender
      {
        AppendToFile = true,
        File = $"{appData}\\sim.log",
        Layout = new PatternLayout("%4t %d{ABSOLUTE} %-5p %m%n"),
        SecurityContext = new WindowsSecurityContext(),
        Threshold = Level.Info
      };

      var debug = new LogFileAppender
      {
        AppendToFile = true,
        File = $"{appData}\\sim.debug",
        Layout = new PatternLayout("%4t %d{ABSOLUTE} %-5p %m%n"),
        SecurityContext = new WindowsSecurityContext(),
        Threshold = Level.Debug
      };

      CoreApp.InitializeLogging(info, debug);

      CoreApp.LogMainInfo();

      GlobalConfiguration.Configure(WebApiConfig.Register);
    }
  }
}
