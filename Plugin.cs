using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Win32;

namespace Wox.Plugin.Putty
{
	
	public class PuttyPlugin: IPlugin
	{
		private PluginInitContext context;
		
		public void Init(PluginInitContext context)
		{
			this.context = context;
		}
		
		public List<Result> Query(Query query)
		{
			List<Result> results = new List<Result>();
			using (RegistryKey root = Registry.CurrentUser.OpenSubKey("Software\\SimonTatham\\PuTTY\\Sessions")) {
				if (root != null) {
					if (query.ActionParameters.Count == 0) {
						results.Add(MakeResult(null, null));
					}
					foreach (string key in root.GetSubKeyNames()) {
						if (query.ActionParameters.Count == 0 || key.ToLower().Contains(query.ActionParameters[0].ToLower())) {
							using (RegistryKey session = root.OpenSubKey(key)) {
								results.Add(MakeResult(key, session.GetValue("Protocol") + "://" + session.GetValue("UserName") + "@" + session.GetValue("HostName")));
							}
						}
					}
					if (query.ActionParameters.Count != 0) {
						results.Add(MakeResult(null, null));
					}
				} else {
					results.Add(MakeResult(null, null));
				}
			}
			return results;			
		}

		private Result MakeResult(string name, string desc)
		{
			return new Result() {
				Title = name ?? "putty.exe",
				SubTitle = desc ?? "Launch Clean Putty",
				IcoPath = "Images\\plugin.png",
				Action = e => {
					try {
						var p = new Process();
						p.StartInfo.FileName = "putty";
						if (name != null) {
							p.StartInfo.Arguments = "-load \"" + name + "\"";
						}
						p.Start();
					} catch (Exception ex) {
						context.ShowMsg.Invoke("Putty Error: " + name, ex.Message, ""); 
						return false;
						// ignore exception
					}
					return true;
				}
			};
		}
	}
	// end namespace
}