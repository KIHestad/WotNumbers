using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace WinApp.Code
{
	class vBAddict
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
				
				XmlNode node = xmlDoc.GetElementsByTagName("status").Item(0);  // get status node from xml
				string responseCode = node.FirstChild.Value.ToString();
				node = xmlDoc.GetElementsByTagName("message").Item(0);   // get message node from xml
				string responseText = node.FirstChild.Value.ToString();
				
				//return result + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
				
				if (responseCode == "0")
					return "Test successfully completed!";
				else
					return "The connection could not be established. " + Environment.NewLine 
						 + responseText + Environment.NewLine 
						 + "Error code: " + responseCode + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
			}
			catch (Exception ex)
			{
				return "Error connecting to vBAddict. Error message:" + Environment.NewLine + Environment.NewLine + ex.Message;
			}
		}
		
		public static bool UploadDossier(string dossierFile, string playerName, string playerServer, string playerToken, out string msg)
		{
			msg = "Starting upload dossier file to vBAddict...";
			bool result = true;
			try
			{
				string url = "http://carius.vbaddict.net:82/upload_file/dossier/@SERVER/@USERNAME/@TOKEN/xml/";
				url = url.Replace("@USERNAME", playerName);
				url = url.Replace("@SERVER", playerServer);
				if (playerToken == "")
					playerToken = "-";
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
				msg = XmlHelper.XmlToString(xmlDoc) + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
				// Check result
				XmlNodeList response = xmlDoc.GetElementsByTagName("response");
				string status = "";
				foreach (XmlNode item in response[0].ChildNodes)
				{
					if (item.Name == "status") status = item.InnerText;
				}
				result = (status == "0");
			}
			catch (Exception ex)
			{
				msg = "Error uploading dossier file. Error message:" + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine;
				result = false;
			}
			return result;
		}

		public static bool UploadBattle(string battleFile, string playerName, string playerServer, string playerToken, out string msg)
		{
			msg = "Starting upload battle file to vBAddict...";
			bool result = true;
			try
			{
				string url = "http://carius.vbaddict.net:82/upload_file/battleresult/@SERVER/@USERNAME/@TOKEN/xml/";
				url = url.Replace("@USERNAME", playerName);
				url = url.Replace("@SERVER", playerServer);
				if (playerToken == "")
					playerToken = "-";
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
				string localFile = battleFile;
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
				msg = XmlHelper.XmlToString(xmlDoc) + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
				// Check result
				XmlNodeList response = xmlDoc.GetElementsByTagName("response");
				string status = "";
				foreach (XmlNode item in response[0].ChildNodes)
				{
					if (item.Name == "status") status = item.InnerText;
				}
				result = (status == "0");
			}
			catch (Exception ex)
			{
				result = false;
				msg = "Error uploading battle file. Error message:" + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine;
			}
			return result;
		}

	}
}
