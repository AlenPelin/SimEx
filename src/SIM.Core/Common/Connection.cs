namespace SIM.Core.Common
{
  using System.IO;
  using System.Xml.Serialization;
  using JetBrains.Annotations;
  using Sitecore.Diagnostics.Base;
  using SIM.IO;
  using System;

  public class Connection : IConnection
  {
    public Connection()
    {
    }

    public Connection([NotNull] IFileSystem fileSystem)
    {
      FileSystem = fileSystem 
                   ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    [NotNull]
    private static string ConnectionFilePath { get; } = Path.Combine(ApplicationManager.ProfilesFolder, "connection.xml");

    public string Url { get; set; }

    public string Token { get; set; }

    private IFileSystem FileSystem { get; set; }

    public void Save()
    {
      var deserializer = new XmlSerializer(typeof(Connection));
      using (var textWriter = new StreamWriter(ConnectionFilePath))
      {
        deserializer.Serialize(textWriter, this);
      }
    }

    [NotNull]
    public static IConnection TryRead([NotNull] IO.IFileSystem fileSystem, [NotNull] string connectionFilePath = null)
    {
      Assert.ArgumentNotNull(fileSystem, nameof(fileSystem));

      var connectionFile = fileSystem.ParseFile(connectionFilePath ?? ConnectionFilePath);
      if (!connectionFile.Exists)
      {
        return null;
      }

      var deserializer = new XmlSerializer(typeof(Connection));
      using (var textReader = new StreamReader(connectionFile.OpenRead()))
      {
        var profile = (Connection)deserializer.Deserialize(textReader);
        profile.FileSystem = fileSystem;

        return profile;
      }
    }
  }
}