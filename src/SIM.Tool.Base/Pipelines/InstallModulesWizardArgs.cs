namespace SIM.Tool.Base.Pipelines
{
  using System;
  using System.Collections.Generic;
  using SIM.Instances;
  using SIM.Pipelines.Processors;
  using SIM.Products;
  using SIM.Tool.Base.Profiles;
  using SIM.Tool.Base.Wizards;
  using JetBrains.Annotations;

  [UsedImplicitly]
  public abstract class InstallModulesWizardArgs : WizardArgs
  {
    #region Fields

    public Instance Instance { get; }

    private string _WebRootPath;

    #endregion

    #region Constructors

    public InstallModulesWizardArgs()
    { 
    }

    public InstallModulesWizardArgs(Instance instance)
    {
      Instance = instance;
      if (instance != null)
      {
        WebRootPath = instance.WebRootPath;
      }
    }

    #endregion

    #region Properties

    [UsedImplicitly]
    public string InstanceName
    {
      get
      {
        return Instance != null ? Instance.Name : string.Empty;
      }
    }

    [CanBeNull]
    public virtual Product Product
    {
      get
      {
        return Instance != null ? Instance.Product : null;
      }

      set
      {
        throw new NotImplementedException();
      }
    }

    #endregion

    #region Public properties

    public string WebRootPath
    {
      get
      {
        return _WebRootPath ?? Instance.WebRootPath;
      }

      set
      {
        _WebRootPath = value;
      }
    }

    #endregion
  }
}