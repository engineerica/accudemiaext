<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IntegrationExamples._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Your Site Sign In</title>
</head>
<body>
	<form id="form1" runat="server">
	
	<div style="margin: 0 auto; width: 650px; text-align: center; border: solid 4px #cc7;">
		
		<h1 style="background-color: #ffa; margin-top: 0px;">Your Site - Sign In</h1>
		
		<div style="margin: 0 auto; width: 100%; text-align: left; margin: 10px;">
			Welcome to this Accudemia integration example.
			<br />
			As this is an example, users are not authenticated against any database. So please
			enter the ID of the user you want to login as, and 'demo' as password (without the quotes).
			<br />
			Once you're logged in to your site, you will be able to access Accudemia using the same
			credentials.
			
			
			<br /><br />
			<b>Demo User:</b>
			<br />
			User Name: (anything)
			<br />
			Password: demo
		</div>
		
		<br />
		<center>
			<asp:Login ID="ctrlLogin" runat="server" DestinationPageUrl="PrivatePage.aspx" 
				onauthenticate="ctrlLogin_Authenticate">
			</asp:Login>
		</center>
		
		<br />
		
	</div>
	
	</form>
</body>
</html>
