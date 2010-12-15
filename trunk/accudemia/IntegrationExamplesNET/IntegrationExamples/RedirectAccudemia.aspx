<%@ Import Namespace="System.IO"%>
<%@ Import Namespace="System.Net"%>
<%@ Page Language="C#"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<title>Accudemia - Loading...</title>
</head>
<body>

	<%
		// USER CONFIGURATIONS
		const string domain = "yourcollege.accudemia.net";
		const string devKey = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
		string userId = System.Threading.Thread.CurrentPrincipal.Identity.Name;
		
		

		// Request the login token to the service
		string serviceUrl = string.Format("https://{0}/Services/PublicHttp.ashx?devkey={1}&action=getlogintoken&user={2}", domain, devKey, userId);

		WebRequest req = WebRequest.Create(serviceUrl);

		string loginToken;

		try
		{
			WebResponse response = req.GetResponse();
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream))
			{
				loginToken = reader.ReadToEnd();
			}
		}
		catch (Exception e)
		{
			// Handle errors, and show a message
			Response.Write("<h2>Unable to access Accudemia.</h2>");
			Response.Write("Please try again in a few minutes or contact your system administrator.<br><br>");
			Response.Write("<b>Error returned by the server:</b><br>");
			
			WebException we = e as WebException;
			using (Stream stream = we.Response.GetResponseStream())
			{
				byte[] buffer = new byte[stream.Length];
				stream.Read(buffer, 0, buffer.Length);

				Response.OutputStream.Write(buffer, 0, buffer.Length);
			}
			return;
		}


		// If a login token was obtained, redirect to Accudemia
		
		// Example URL: https://yourcollege.accudemia.net/?token=xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx&fwd=/Private/Appointments/List.aspx
		const string redirectUrl = "https://{0}/?token={1}&fwd={2}&Referer={3}";

		string forwardUrl = Request.QueryString["fwd"] ?? "";
		if (string.IsNullOrEmpty(forwardUrl))
		{
			forwardUrl = "/Private/Main.aspx";
		}

		string specifiedReferer = Request["Referer"];
		string httpReferer = Request.UrlReferrer != null ? Request.UrlReferrer.OriginalString : null;
		string currentDomain = Request.Url.Scheme + "://" + Request.Url.Authority;
		
		string referer = specifiedReferer ?? httpReferer ?? currentDomain;
		referer = Server.UrlEncode(referer);

		Response.Redirect(string.Format(redirectUrl, domain, loginToken, forwardUrl, referer));
		//Response.Write(string.Format(redirectUrl, domain, loginToken, forwardUrl, referer));

		%>

</body>
</html>
