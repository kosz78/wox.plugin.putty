using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Wox.Plugin;

namespace PowerToys.Plugin.Putty {
  public class Plugin : IPlugin {
    public static string PluginID => "54A6672A06E047A48A7D88A78FA5B13E";
    private PluginInitContext _context;

    public List<Result> Query(Query query) {
      var results = new List<Result>();
      var querySearch = query.Search;

      if (string.IsNullOrEmpty(querySearch))
        querySearch = string.Empty;

      var puttySessions = new PuttySessionService()
        .GetAll()
        .Where(session => session.Identifier.ToLowerInvariant().Contains(querySearch.ToLowerInvariant()));
      foreach (var puttySession in puttySessions)
        results.Add(createResult(puttySession.Identifier, puttySession.ToString()));

      if (results.Count == 0)
        results.Add(new Result {
          Title = "PuTTY",
          SubTitle = "Connect to " + querySearch,
          IcoPath = "icon.png",
          Action = context => launchPuttyFromSearch(querySearch),
        });

      return results;
    }

    public void Init(PluginInitContext context) {
      _context = context;
    }

    public string Name => "PuTTY SSH Plugin";
    public string Description => "Search and execute stored sessions in PuTTY";


    private Result createResult(string title = "putty.exe", string subTitle = "Launch Clean Putty") {
      return new Result {
        Title = title,
        SubTitle = subTitle,
        IcoPath = "icon.png",
        Action = context => launchPuttySession(title),
      };
    }

    private bool launchPuttySession(string sessionIdentifier) {
      try {
        var p = new Process { StartInfo = { FileName = "putty" } };

        // Optionally pass the session identifier
        if (!string.IsNullOrEmpty(sessionIdentifier))
          p.StartInfo.Arguments = "-load \"" + sessionIdentifier + "\"";

        p.Start();

        return true;
      } catch (Exception ex) {
        // Report the exception to the user. No further actions required
        _context.API.ShowMsg("Putty Error: " + sessionIdentifier, ex.Message, "");

        return false;
      }
    }

    private bool launchPuttyFromSearch(string host) {
      try {
        var p = new Process { StartInfo = { FileName = "putty" } };

        // Optionally pass the session identifier or host+port
        if (!string.IsNullOrEmpty(host)) {
          p.StartInfo.Arguments = "-ssh \"" + host + "\"";
          if (host.Contains(':')) {
            var parts = host.Split(':', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
              p.StartInfo.Arguments = $"-ssh \"{parts[0]}\" -P {parts[1].Trim()}";
            else throw new Exception("Supported format <host>:[<port>]");
          }
        }

        p.Start();

        return true;
      } catch (Exception ex) {
        // Report the exception to the user. No further actions required
        _context.API.ShowMsg("Putty Error: " + host, ex.Message, "");

        return false;
      }
    }
  }
}