using System.Text.RegularExpressions;
using Wox.Plugin;

namespace PowerToys.Plugin.IdGen {
  public class Plugin : IPlugin {
    private PluginInitContext _context;

    public List<Result> Query(Query query) {
      var results = new List<Result>();
      var querySearch = query.Search;

      if (string.IsNullOrEmpty(querySearch))
        querySearch = string.Empty;

      var match = Regex.Match(querySearch.Trim(), @"[dD]{1}(?<NUM>[0-9]+)");
      if (match.Success)
        results.Add(createDiceResult(querySearch, int.Parse(match.Groups["NUM"].Value)));
      else {
        results.Add(createResult("GUID(n)", Guid.NewGuid().ToString("N")));
        results.Add(createResult("GUID(N)", Guid.NewGuid().ToString("N").ToUpper()));
        results.Add(createResult("GUID(D)", Guid.NewGuid().ToString("D")));
        results.Add(createResult("GUID(X)", Guid.NewGuid().ToString("X")));
      }

      return results;
    }

    private Random _random = null!;

    public void Init(PluginInitContext context) {
      _context = context;
      _random = new Random();
    }

    public string Name => "ID Generator Plugin";
    public string Description => "Generate IDs of various types";


    private Result createResult(string title, string value) => new() {
      Title = title,
      SubTitle = $"Copy '{value}' to clipboard",
      IcoPath = "icon.png",
      Action = context => {
        Clipboard.SetText(value, TextDataFormat.Text);
        return true;
      },
    };

    private Result createDiceResult(string title, int sides) {
      var result = _random.Next(1, sides + 1);
      return new Result {
        Title = title,
        SubTitle = $"Rolled one d{sides} == {result}",
        IcoPath = "icon.png",
        Action = context => true,
      };
    }
  }
}