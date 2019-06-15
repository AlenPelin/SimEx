using System;
using System.Diagnostics;
using System.Security.Principal;

namespace SIM.Tool.Base
{
  public static class AppHelper
  {
    public static bool CheckPermissions()
    {
      if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
      {
        return true;
      }

      if (Debugger.IsAttached)
      {
        throw new InvalidOperationException("SIM requires administrator permissions to operate. Relaunch Visual Studio with elevated permissions to debug SIM.");
      }
      
      return false;
    }
  }
}
