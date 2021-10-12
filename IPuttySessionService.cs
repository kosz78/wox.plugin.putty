using System.Collections.Generic;

namespace PowerToys.Plugin.Putty {
  public interface IPuttySessionService {
    /// <summary>
    /// Returns a List of all Putty Sessions
    /// </summary>
    /// <returns>A List of all Putty Sessions</returns>
    IEnumerable<PuttySession> GetAll();
  }
}