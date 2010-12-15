<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrivatePage.aspx.cs" Inherits="IntegrationExamples.PrivatePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			
		<h1>Now you are logged in your site as <asp:LoginName ID="LoginName2" runat="server" Font-Bold="true" />!</h1>
		<asp:LoginStatus ID="LoginStatus1" runat="server" LogoutText="[Logout]" />
		<br />
		
		Clicking on the links below will log you in Accudemia, using the user: 
		<asp:LoginName runat="server" />
		
		<br />
		<br />
		<hr />
		
		<b>Access Accudemia:</b>
		<div style="padding: 10px;">
			<a href="RedirectAccudemia.aspx">Accudemia Home</a>
			<br />
			<a href="RedirectAccudemia.aspx?fwd=/Private/Persons/EditPerson.aspx?p=my">Edit My Profile</a>
			<br />
			<a href="RedirectAccudemia.aspx?fwd=/Private/Surveys/PendingList.aspx">My Surveys</a>
		</div>
		<br />
		
		<b>Appointments:</b>
		<div style="padding: 10px;">
			<a href="RedirectAccudemia.aspx?fwd=/Private/Appointments/OpenSlots.aspx">Create New</a>
			<br />
			<a href="RedirectAccudemia.aspx?fwd=/Private/Appointments/List.aspx">View All</a>
			<br />
			<a href="RedirectAccudemia.aspx?fwd=/Private/Appointments/NewAppt.aspx">Wizard</a>
		</div>
		
		<br />
		<br />
		
		
		
    </div>
    </form>
</body>
</html>
