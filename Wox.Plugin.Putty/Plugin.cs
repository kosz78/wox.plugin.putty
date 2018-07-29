namespace Wox.Plugin.Putty
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Controls;

    public class PuttyPlugin : IPlugin, ISettingProvider
    {
        /// <summary>
        /// A refernce to the current PluginInitContext
        /// </summary>
        private PluginInitContext _context;

        private Settings _settings;

        /// <summary>
        /// A reference to the Putty PuttySessionService
        /// </summary>
        public IPuttySessionService PuttySessionService { get; set; }

        /// <summary>
        /// A reference to the SettingsService
        /// </summary>
        private SettingsService SettingsService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public PuttyPlugin()
        {
            SettingsService = new SettingsService();
            _settings = SettingsService.LoadSettings();
            PuttySessionService = new PuttySessionService();
        }

        /// <summary>
        /// Initializes the Putty plugin
        /// </summary>
        /// <param name="context"></param>
        public void Init(PluginInitContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a filtered Putty sessions list based on the given Query.
        /// If no Query.ActionParameter is provided only the default Putty item is returned.
        /// </summary>
        /// <param name="query">A Query that contains an ActionParameter to filter the Putty session list</param>
        /// <returns>The filtered Putty session list</returns>
        public List<Result> Query(Query query)
        {
            var results = new List<Result> { };
            if (_settings.AddPuttyExeToResults)
            {
                results.Add(CreateResult());
            }
            var querySearch = query.ActionParameters.FirstOrDefault();

            if (string.IsNullOrEmpty(querySearch))
            {
                if (_settings.AddPuttyExeToResults)
                {
                    return results;
                }
                else
                {
                    querySearch = string.Empty;
                }
            }

            var puttySessions = PuttySessionService.GetAll().Where(session => session.Identifier.ToLowerInvariant().Contains(querySearch.ToLowerInvariant()));
            foreach (var puttySession in puttySessions)
            {
                results.Add(CreateResult(puttySession.Identifier, puttySession.ToString()));
            }

            return results;
        }

        /// <summary>
        /// Creates a new Result item
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subTitle"></param>
        /// <returns>A Result object containing the PuttySession identifier and its connection string</returns>
        private Result CreateResult(string title = "putty.exe", string subTitle = "Launch Clean Putty")
        {
            return new Result
            {
                Title = title,
                SubTitle = subTitle,
                IcoPath = "icon.png",
                Action = context => LaunchPuttySession(title),
            };
        }

        /// <summary>
        /// Launches a new Putty session
        /// </summary>
        /// <param name="sessionIdentifier">The session identifier</param>
        /// <returns>If launching Putty succeeded</returns>
        private bool LaunchPuttySession(string sessionIdentifier)
        {
            try
            {
                var p = new Process { StartInfo = { FileName = "putty" } };

                // Optionally pass the session identifier
                if (!string.IsNullOrEmpty(sessionIdentifier))
                {
                    p.StartInfo.Arguments = "-load \"" + sessionIdentifier + "\"";
                }

                if (_settings.AlwaysStartsSessionMaximized)
                {
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                }

                p.Start();

                return true;
            }
            catch (Exception ex)
            {
                // Report the exception to the user. No further actions required
                _context.API.ShowMsg("Putty Error: " + sessionIdentifier, ex.Message, "");

                return false;
            }
        }

        public Control CreateSettingPanel()
        {
            return new PuttySettings(_settings);
        }
    }
}