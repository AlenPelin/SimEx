using log4net.Core;
using log4net.Layout;
using log4net.Util;
using SIM.Client;
using SIM.Core;
using SIM.Core.Logging;
using SIM.Tool.Base;
using System;
using System.IO;
using System.Web;
using System.Web.Http;

namespace SIM.Service
{
  public class WebApiApplication : HttpApplication
  {
    protected void Application_Start()
    {
      var appData = Server.MapPath("/App_Data");
      Directory.CreateDirectory(appData);

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
