namespace SIM.Client.Commands
{
  using CommandLine;
  using JetBrains.Annotations;
  using SIM.Core.Commands;
  using SIM.IO.Real;

  [Verb("connect", HelpText = "Configure server connection.")]
  public class ConnectCommandFacade : ConnectCommand
  {
    [UsedImplicitly]
    public ConnectCommandFacade()
      : base(new RealFileSystem())
    {
    }

    [Option('u', "url", Required = true)]
    public override string Url { get; set; }

    [Option('t', "token", Required = true)]
    public override string Token { get; set; }
  }
}