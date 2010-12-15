namespace IntegrationExamples
{
	using System;
	using System.Web.UI;

	public partial class _Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Request["ReturnUrl"]))
				Response.Redirect("/", true);
		}

		protected void ctrlLogin_Authenticate(object sender, System.Web.UI.WebControls.AuthenticateEventArgs e)
		{
			if (ctrlLogin.Password == "demo")
			{
				e.Authenticated = true;
			}
		}
	}
}