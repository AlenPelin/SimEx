namespace SIM.Tool.Windows.MainWindowComponents
{
  using System.IO;
  using System.Windows;
  using SIM.Instances;
  using SIM.Pipelines;
  using SIM.Products;
  using SIM.Tool.Base;
  using SIM.Tool.Base.Plugins;
  using JetBrains.Annotations;

  [UsedImplicitly]
  public class OpenToolboxButton : IMainWindowButton
  {
    #region Fields

    private const string PackageName = "Support Toolbox.zip";
    private bool BypassSecurity { get; }

    #endregion

    #region Constructors

    public OpenToolboxButton()
    {
      BypassSecurity = false;
    }

    public OpenToolboxButton(string param)
    {
      BypassSecurity = param == "bypass";
    }

    #endregion

    #region Public methods

    public bool IsEnabled(Window mainWindow, Instance instance)
    {
      return instance != null;
    }

    public void OnClick(Window mainWindow, Instance instance)
    {
      if (!EnvironmentHelper.CheckSqlServer())
      {
        return;
      }

      if (instance == null)
      {
        return;
      }

      if (BypassSecurity)
      {
        InstanceHelperEx.OpenInBrowserAsAdmin(instance, mainWindow, @"/sitecore/admin");
      }
      else
      {
        InstanceHelperEx.BrowseInstance(instance, mainWindow, @"/sitecore/admin", false);
      }
    }

    #endregion
  }
}