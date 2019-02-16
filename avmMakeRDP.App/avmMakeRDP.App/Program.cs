using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avmMakeRDP.App
{
	class Program
	{
		static void Main(string[] args)
		{
			string user = string.Empty;
			string ip = "127.0.0.1";
			bool fullscreen = true;
			string passwd = string.Empty;
			string domain = string.Empty;

			if (args.Length > 0)
			{
				ip = new IpFinder().IP;

				if (args.Length >= 2)
				{
					user = args[1].ToString();
				}

				if (args.Length >= 3)
				{
					fullscreen = bool.Parse(args[2].ToString());
				}

				if (args.Length >= 4)
				{
					passwd = args[3].ToString();
				}

				if (args.Length >= 5)
				{
					domain = args[4].ToString();
				}

				StreamWriter generator = null;
				try
				{
					generator = new StreamWriter(args[0].ToString());
					generator.AutoFlush = true;
					if (fullscreen)
					{
						generator.WriteLine("screen mode id:i:2");
					}
					else
					{
						generator.WriteLine("screen mode id:i:1");
					}

					generator.WriteLine("use multimon:i:0");
					generator.WriteLine("desktopwidth:i:800");
					generator.WriteLine("desktopheight:i:600");
					generator.WriteLine("session bpp:i:32");
					generator.WriteLine("compression:i:1");
					generator.WriteLine("keyboardhook:i:2");
					generator.WriteLine("audiocapturemode:i:0");
					generator.WriteLine("videoplaybackmode:i:1");
					generator.WriteLine("connection type:i:7");
					generator.WriteLine("networkautodetect:i:1");
					generator.WriteLine("bandwidthautodetect:i:1");
					generator.WriteLine("displayconnectionbar:i:1");
					generator.WriteLine("enableworkspacereconnect:i:0");
					generator.WriteLine("disable wallpaper:i:0");
					generator.WriteLine("allow font smoothing:i:0");
					generator.WriteLine("allow desktop composition:i:0");
					generator.WriteLine("disable full window drag:i:1");
					generator.WriteLine("disable menu anims:i:1");
					generator.WriteLine("disable themes:i:0");
					generator.WriteLine("disable cursor setting:i:0");
					generator.WriteLine("bitmapcachepersistenable:i:1");
					generator.Write("full address:s:");
					if (user.Equals("brettc"))
					{
						generator.WriteLine("spsrv");
					}
					else
					{
						generator.WriteLine(ip);
					}
					//generator.WriteLine("enablecredsspsupport:i:0");
					generator.WriteLine("prompt for credentials on client:i:0");
					generator.WriteLine("audiomode:i:2");
					generator.WriteLine("redirectprinters:i:0");
					generator.WriteLine("redirectcomports:i:0");
					generator.WriteLine("redirectsmartcards:i:0");
					generator.WriteLine("redirectclipboard:i:0");
					generator.WriteLine("redirectposdevices:i:0");
					generator.WriteLine("autoreconnection enabled:i:1");
					generator.WriteLine("authentication level:i:0");
					generator.WriteLine("prompt for credentials:i:0");
					generator.WriteLine("negotiate security layer:i:1");
					generator.WriteLine("remoteapplicationmode:i:0");
					generator.WriteLine("alternate shell:s:");
					generator.WriteLine("shell working directory:s:");
					generator.WriteLine("gatewayhostname:s:");
					generator.WriteLine("gatewayusagemethod:i:4");
					generator.WriteLine("gatewaycredentialssource:i:4");
					generator.WriteLine("gatewayprofileusagemethod:i:0");
					generator.WriteLine("promptcredentialonce:i:0");
					generator.WriteLine("gatewaybrokeringtype:i:0");
					generator.WriteLine("use redirection server name:i:0");
					generator.WriteLine("rdgiskdcproxy:i:0");
					generator.WriteLine("kdcproxyname:s:");
					generator.WriteLine("username:s:" + domain + "\\" + user);
					generator.WriteLine("drivestoredirect:s:");

					Criptografia crypt = new Criptografia();

					passwd = crypt.encryptpw(passwd);
					generator.WriteLine("password 51:b:" + passwd);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error on Generator => ", ex.Message.ToString());
				}
				finally
				{
					generator.Close();
					generator.Dispose();
				}
			}
			else
			{
				Console.WriteLine("Path/File is a required parameter.");
			}
		}
	}
}
