using System;
using System.Collections.Generic;

namespace PowerToys.Plugin.Putty {
  public class PuttySessionService : IPuttySessionService {
    public IEnumerable<PuttySession> GetAll() {
      var results = new List<PuttySession>();

      var reg = new Registry();
      var root = reg.EnumKeys(Registry.ROOT_KEY.HKEY_CURRENT_USER, "Software\\SimonTatham\\PuTTY\\Sessions");
      if (root == null)
        return results;

      foreach (string key in root) {
        var subKey = reg.EnumValues(Registry.ROOT_KEY.HKEY_CURRENT_USER, "Software\\SimonTatham\\PuTTY\\Sessions\\" + key);
        if (subKey == null)
          continue;

        try {
          var protocol = reg.ReadString(Registry.ROOT_KEY.HKEY_CURRENT_USER, "Software\\SimonTatham\\PuTTY\\Sessions\\" + key, "Protocol");
          var user = reg.ReadString(Registry.ROOT_KEY.HKEY_CURRENT_USER, "Software\\SimonTatham\\PuTTY\\Sessions\\" + key, "UserName");
          var host = reg.ReadString(Registry.ROOT_KEY.HKEY_CURRENT_USER, "Software\\SimonTatham\\PuTTY\\Sessions\\" + key, "HostName");

          results.Add(new PuttySession {
            Identifier = key,
            Protocol = protocol,
            Username = user,
            Hostname = host,
          });
        } catch (Exception) {
          // If there is any exception related to the registry access, just do nothing for that key, but don't let the whole results fails.
        }
      }

      return results;
    }
  }
}