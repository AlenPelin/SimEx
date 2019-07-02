namespace SIM
{
  using JetBrains.Annotations;

  public interface IConnection
  {
    [CanBeNull]
    string Url { get; set; }

    [CanBeNull]
    string Token { get; set; }

    void Save();
  }
}