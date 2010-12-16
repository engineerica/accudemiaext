<?
session_start();

if (!isset($HTTP_SESSION_VARS['userid'])) {
	header( 'Location: index.php' );
	exit();
}

?>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Your Private Page</title>
	<style type="text/css">
	* {font-family: Tahoma, Verdana, Arial;}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			
		<h1>Now you are logged in your site as <? echo $HTTP_SESSION_VARS['userid']; ?>!</h1>
		<a href="index.php">[Logout]</a>
		<br />
		
		Clicking on the links below will log you in Accudemia, using the user: <? echo $HTTP_SESSION_VARS['userid']; ?>
		
		<br />
		<br />
		<hr />
		
		<b>Access Accudemia:</b>
		<div style="padding: 10px;">
			<a href="redirectaccudemia.php">Accudemia Home</a>
			<br />
			<a href="redirectaccudemia.php?fwd=/Private/Persons/EditPerson.aspx?p=my">Edit My Profile</a>
			<br />
			<a href="redirectaccudemia.php?fwd=/Private/Surveys/PendingList.aspx">My Surveys</a>
		</div>
		<br />
		
		<b>Appointments:</b>
		<div style="padding: 10px;">
			<a href="redirectaccudemia.php?fwd=/Private/Appointments/OpenSlots.aspx">Create New</a>
			<br />
			<a href="redirectaccudemia.php?fwd=/Private/Appointments/List.aspx">View All</a>
			<br />
			<a href="redirectaccudemia.php?fwd=/Private/Appointments/NewAppt.aspx">Wizard</a>
		</div>
		
		<br />
		<br />
		
		
		
    </div>
    </form>
</body>
</html>
