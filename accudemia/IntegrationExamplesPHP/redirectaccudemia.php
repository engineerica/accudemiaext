<?

error_reporting(!E_ALL);

session_start();


if (!isset($HTTP_SESSION_VARS['userid'])) {
	header( 'Location: index.php' );
	exit();
}


// USER CONFIGURATIONS
$domain = "yourcollege.accudemia.net";
$devKey = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
$userId = $_SESSION["userid"];


// Request the login token to the service
$serviceUrl = "https://$domain/Services/PublicHttp.ashx?devkey=$devKey&action=getlogintoken&user=$userId";

$ch = curl_init();

// Set the URL and other options
curl_setopt($ch, CURLOPT_URL, $serviceUrl);
curl_setopt($ch, CURLOPT_HEADER, 0);

// Start output buffering to save the output, and get it without flushing!
ob_start();
curl_exec($ch);
$aInfo = curl_getinfo($ch);
curl_close ($ch);

$szContents = ob_get_contents();

ob_end_clean();

// Status code
$status_code = $aInfo['http_code'];

switch($status_code) {
	case 200:

		if (isset($HTTP_GET_VARS["fwd"])) {
			$forwardUrl = $_GET["fwd"];
		}
		else
		{
			$forwardUrl = "/Private/Main.aspx";
		}
		
		
		$specifiedReferer = "";
		if (isset($HTTP_GET_VARS["Referer"])) {
			$specifiedReferer = $_GET["Referer"];
		}

		$httpReferer = "";
		if (isset($HTTP_REFERER))
		{
			$httpReferer = $HTTP_REFERER;
		}
		 
		// test if it's using HTTPS
		$protocol = strpos(strtolower($_SERVER['SERVER_PROTOCOL']),'https') === FALSE ? 'http' : 'https';
		
		$currentDomain = $protocol . "://" . $_SERVER['HTTP_HOST'];
		
		// Select the best referer
		if ($specifiedReferer != null && $specifiedReferer != "") {
			$referer = $specifiedReferer;
		}
		else if ($httpReferer != null && $httpReferer != "") {
			$referer = $httpReferer;
		}
		else {
			$referer = $currentDomain;
		}
			
			
		$referer = urlencode($referer);
		
		// Example URL: https://yourcollege.accudemia.net/?token=xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx&fwd=/Private/Appointments/List.aspx
		$redirectUrl = "https://$domain/?token=$szContents&fwd=$forwardUrl&Referer=$referer";
		
		header( "Location: $redirectUrl" );
		
		exit();
		break;
	default:
		$errormsg = "<h2>Unable to access Accudemia.</h2>".
					"Please try again in a few minutes or contact your system administrator.<br><br>".
					"<b>Error returned by the server:</b><br>".
					$szContents;
		break;
}
/*

string loginToken;

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

*/







?>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<title>Accudemia - Loading...</title>
</head>
<body>

	<? echo $errormsg; ?> 

</body>
</html>
