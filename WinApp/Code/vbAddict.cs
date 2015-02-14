using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace WinApp.Code
{
	class vbAddict
	{
		public static string TestConnection()
		{
			try
			{
				string url = "http://carius.vbaddict.net:82/upload_check/xml/";
				HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
				httpRequest.Timeout = 10000;     // 10 secs
				httpRequest.UserAgent = "Wot Numbers " + AppVersion.AssemblyVersion;
				httpRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
				HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
				StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
				string xmlResult = responseStream.ReadToEnd(); // Read result into string
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(xmlResult); // Load string into xml doc
				string result = XmlHelper.XmlToString(xmlDoc);
				return result + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
			}
			catch (Exception ex)
			{
				return "Error connecting to vbAddict. Error message:" + Environment.NewLine + Environment.NewLine + ex.Message;
			}
		}
		
		public static string UploadDossier(string dossierFile, string playerName, string playerServer, string playerToken)
		{
			string result = "";
			try
			{
				string url = "http://carius.vbaddict.net:82/upload_file/dossier/@SERVER/@USERNAME/@TOKEN/debug/";
				url = url.Replace("@USERNAME", playerName);
				url = url.Replace("@SERVER", playerServer);
				if (playerToken == "")
					playerToken = "notoken";
				url = url.Replace("@TOKEN", playerToken);
				HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
				httpRequest.Timeout = 10000; // 10 secs
				httpRequest.UserAgent = "Wot Numbers " + AppVersion.AssemblyVersion;
				httpRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
				// Get ready to pu file with request
				httpRequest.Method = "PUT";
				httpRequest.KeepAlive = false;
				httpRequest.SendChunked = false;
				httpRequest.AllowWriteStreamBuffering = true;
				// Read file into stream
				Stream reqStream = httpRequest.GetRequestStream();
				string localFile = dossierFile;
				FileStream rdr = new FileStream(localFile, FileMode.Open, FileAccess.Read);
				byte[] inData = new byte[4096];
				int bytesRead = rdr.Read(inData, 0, inData.Length);
				while (bytesRead > 0)
				{
					reqStream.Write(inData, 0, bytesRead);
					bytesRead = rdr.Read(inData, 0, inData.Length);
				}
				reqStream.Close();
				rdr.Close();
				// Perform the web request
				HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
				// Get result
				StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
				string xmlResult = responseStream.ReadToEnd(); // Read result into string
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(xmlResult); // Load string into xml doc
				result = XmlHelper.XmlToString(xmlDoc) + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
			}
			catch (Exception ex)
			{
				result = "Error uploading dossier file. Error message:" + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine;
			}
			return result;
		}


	}
}
