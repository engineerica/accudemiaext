<?
session_start();
session_unregister("userid");

$loginError = false;
if (isset($HTTP_POST_VARS['password'])) {
	if ($_POST["password"] == "demo")
	{
		session_register("userid");
		$userid = $_POST["user"];
		
		header( 'Location: privatepage.php' );
		exit();
	}
	else
	{
		$loginError = true;
	}
}

?> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>Your Site Sign In</title>
	<style type="text/css">
	* {font-family: Tahoma, Verdana, Arial;}
	</style>
</head>
<body>
	
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
		
		<form name="form1" method="post" action="index.php" id="form1" style="text-align: left;">
		
			<div style="padding-left: 180px;">
				User Name: <input type="text" name="user" style="width: 200px;" />
			</div>
			<div style="padding-left: 191px; padding-top: 10px;">
				Password: <input type="password" name="password" style="width: 200px;" />
			</div>
			
			<div style="padding-left: 170px; padding-top: 10px; color: #f00; font-size: 10pt;">
				<?
					if ($loginError) {
						echo "Your login attempt was not successful. Please try again.";
					}
				?>
			</div>
			
			<div style="padding-left: 268px; padding-top: 10px;">
				<input type="submit" value="Log In" style="width: 60px;" />
			</div>
			
		
		</form>
		
		
		<br />
		
	</div>
</form>
</body>
</html>
