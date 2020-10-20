using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LogOut : System.Web.UI.Page
{
    //This is function helps the user to logout from session
    protected void Page_Load(object sender, EventArgs e)
    {
        ProjectUtility.bLogoutClicked = 1;
        ProjectUtility.bLogin = 0;

        {
            if (Session["User"] != null)
            {
                FormsAuthentication.SignOut();
                Session.Remove("User");
                Session.Abandon();
                Session.RemoveAll();
                Session.Clear();
                Response.Redirect("~/Home.aspx");
            }
            else
            {
                Response.Redirect("~/Home.aspx");
            }
        }
    }
}