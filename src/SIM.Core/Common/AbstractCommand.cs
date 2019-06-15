﻿namespace SIM.Core.Common
{
  using System;
  using System.Diagnostics;
  using JetBrains.Annotations;
  using Sitecore.Diagnostics.Base;
  using Sitecore.Diagnostics.Logging;
  using SIM.IO;

  public abstract class AbstractCommand<TResult> : AbstractCommand
  {                                             
    protected AbstractCommand([NotNull] IFileSystem fileSystem)
      : base(fileSystem)
    {                                                           
    }

    protected sealed override CommandResult CreateResult()
    {
      return new CommandResult<TResult>();
    }

    protected sealed override void DoExecute(CommandResult result)
    {
      DoExecute((CommandResult<TResult>)result);
    }

    protected abstract void DoExecute([NotNull] CommandResult<TResult> result);
  }

  public abstract class AbstractCommand : SIM.Core.Common.ICommand
  {
    [NotNull]
    protected IO.IFileSystem FileSystem { get; }

    protected AbstractCommand([NotNull] IFileSystem fileSystem)
    {
      Assert.ArgumentNotNull(fileSystem, nameof(fileSystem));

      FileSystem = fileSystem;
    }

    [NotNull]
    public CommandResult Execute()
    {
      var result = CreateResult();
      var timer = new Stopwatch();
      timer.Start();
      try
      {
        try
        {
          DoExecute(result);
        }
        finally
        {
          timer.Stop();
        }

        result.Success = true;
      }
      catch (MessageException ex)
      {
        result.Message = ex.Message;
        result.Success = false;
      }
      catch (Exception ex)
      {
        Log.Error(ex, $"{GetType().Name} command has failed with unhandled exception");
        result.Success = false;
        result.Error = new CustomException(ex);
      }

      result.Elapsed = timer.Elapsed;
      return result;
    }

    protected virtual CommandResult CreateResult()
    {
      return new CommandResult();
    }

    protected abstract void DoExecute([NotNull] CommandResult result);
  }
}