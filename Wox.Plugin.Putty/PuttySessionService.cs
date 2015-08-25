namespace Wox.Plugin.Putty
{
    using System.Collections.Generic;
    using Microsoft.Win32;

    public class PuttySessionService : IPuttySessionService
    {
        /// <summary>
        /// Returns a List of all Putty Sessions
        /// </summary>
        /// <returns>A List of all Putty Sessions</returns>
        public IEnumerable<PuttySession> GetAll()
        {
            var results = new List<PuttySession>();

            using (var root = Registry.CurrentUser.OpenSubKey("Software\\SimonTatham\\PuTTY\\Sessions"))
            {
                if (root == null)
                {
                    return results;
                }

                foreach (var subKey in root.GetSubKeyNames())
                {
                    using (var puttySessionSubKey = root.OpenSubKey(subKey))
                    {
                        if (puttySessionSubKey == null)
                        {
                            continue;
                        }

                        results.Add(new PuttySession
                        {
                            Identifier = subKey,
                            Protocol = puttySessionSubKey.GetValue("Protocol").ToString(),
                            Username = puttySessionSubKey.GetValue("UserName").ToString(),
                            Hostname = puttySessionSubKey.GetValue("HostName").ToString(),
                        });
                    }
                }
            }

            return results;
        } 
    }
}
