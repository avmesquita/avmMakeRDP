using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace avmMakeRDP.App
{
	public class IpFinder
	{

		public string IP { get { return GetIP(); } }

		public IpFinder()
		{

		}

		private string GetIP()
		{
			string ip = string.Empty;
			try
			{
				ip = avmGetPublicIP();
				if (string.IsNullOrEmpty(ip) || ip == "0.0.0.0")
				{
					ip = GetPublicIP();
					if (string.IsNullOrEmpty(ip) || ip == "0.0.0.0")
					{
						ip = Convert.ToString(GetExternalIp());
					}
				}
			}
			catch
			{
				return ip;
			}
			return ip;
		}
		private IPAddress GetExternalIp()
		{
			string whatIsMyIp = "http://www.whatismyip.com";
			string getIpRegex = @"(?<=<TITLE>.*)\d*\.\d*\.\d*\.\d*(?=</TITLE>)";
			WebClient wc = new WebClient();
			UTF8Encoding utf8 = new UTF8Encoding();
			string requestHtml = "";
			try
			{
				requestHtml = wc.DownloadString(whatIsMyIp);
			}
			catch (WebException we)
			{
				// do something with exception
				Console.Write(we.ToString());
			}
			Regex r = new Regex(getIpRegex);
			Match m = r.Match(requestHtml);
			IPAddress externalIp = null;
			if (m.Success)
			{
				externalIp = IPAddress.Parse(m.Value);
			}
			return externalIp;
		}

		private string GetPublicIP()
		{
			String direction = "";
			WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
			using (WebResponse response = request.GetResponse())
			using (StreamReader stream = new StreamReader(response.GetResponseStream()))
			{
				direction = stream.ReadToEnd();
			}

			//Search for the ip in the html
			int first = direction.IndexOf("Address: ") + 9;
			int last = direction.LastIndexOf("</body>");
			direction = direction.Substring(first, last - first);

			return direction;
		}

		private string avmGetPublicIP()
		{
			String direction = "";
			WebRequest request = WebRequest.Create("http://www.avmsistemas.net/ip.php");
			using (WebResponse response = request.GetResponse())
			using (StreamReader stream = new StreamReader(response.GetResponseStream()))
			{
				direction = stream.ReadToEnd();
			}
			return direction;
		}


	}
}

