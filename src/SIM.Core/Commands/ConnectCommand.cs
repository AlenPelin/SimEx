namespace SIM.Core.Commands
{
  using System;
  using Sitecore.Diagnostics.Base;
  using JetBrains.Annotations;
  using SIM.Core.Common;

  public class ConnectCommand : AbstractCommand
  {
    public ConnectCommand([NotNull] IO.IFileSystem fileSystem)
      : base(fileSystem)
    {
      Assert.ArgumentNotNull(fileSystem, nameof(fileSystem));
    }

    [CanBeNull]
    public virtual string Url { get; [UsedImplicitly] set; }

    [CanBeNull]
    public virtual string Token { get; [UsedImplicitly] set; }

    protected override void DoExecute(CommandResult result)
    {
      Assert.ArgumentNotNull(result, nameof(result));

      var connection = new Connection(FileSystem);

      connection.Url = Url ?? throw new ArgumentNullException(nameof(Url));
      connection.Token = Token ?? throw new ArgumentNullException(nameof(Token));

      connection.Save();
    }
  }
}