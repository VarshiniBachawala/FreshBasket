using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;

public partial class Home : System.Web.UI.Page
{
    public static int i = 0;
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    //This function helps to get images
    public static Slide[] GetImages()
    {
        List<Slide> slides = new List<Slide>();
        string path = HttpContext.Current.Server.MapPath("~/imageslide/");
        if (path.EndsWith("\\"))
        {
            path = path.Remove(path.Length - 1);
        }
        Uri pathUri = new Uri(path, UriKind.Absolute);
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            Uri filePathUri = new Uri(file, UriKind.Absolute);
            slides.Add(new Slide
            {
                Name = Path.GetFileNameWithoutExtension(file),
                Description = Path.GetFileNameWithoutExtension(file) + " Description.",
                ImagePath = pathUri.MakeRelativeUri(filePathUri).ToString()
            });
        }
        return slides.ToArray();
    }
    //this function is called at the time of page load and checks the presence of user
    protected void Page_Load(object sender, EventArgs e)
    {

        if (ProjectUtility.bLogoutClicked == 1)
        {
            authFrom.Visible = true;
            welcomeLable1.Visible = false;

        }
        if (ProjectUtility.bLogin == 1)
        {

            authFrom.Visible = false;
            welcomeLable1.Visible = true;
            SqlConnection cn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["GroceryVarshDB"].ConnectionString);
            SqlCommand cmd1 = new SqlCommand(@"Select * From Registration where Email='" + Session["User"].ToString() + "'", cn1);
            cn1.Open();
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                welcomeLable1.Text = "Hi" + " " + dr1["Name"].ToString();
            }
            cn1.Close();
        }
        if (i == 1)
        {
            lblResult.Text = "UserName Or Password is Incorrect";
            i = 0;
        }
        else
        {
        }

        if (Session["User"] != null)
        {
            authFrom.Visible = false;
            welcomeLable1.Visible = true;
            SqlConnection cn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["GroceryVarshDB"].ConnectionString);
            SqlCommand cmd1 = new SqlCommand(@"Select * From Registration where Email='" + Session["User"].ToString() + "'", cn1);
            cn1.Open();
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                welcomeLable1.Text = "Hi" + " " + dr1["Name"].ToString();
            }
            cn1.Close();
        }

        ProjectUtility.bLogin = 0;
    }
    //This function is called when login button is clicked and helps the user to direct the page to user's products
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["GroceryVarshDB"].ConnectionString);
        SqlCommand cmd = new SqlCommand(@"select 1 from Registration r right join [login] l on r.Email=l.UserEmail where l.UserEmail='" + txtUserName.Text.Trim()+ "' and l.Password='"+txtPassword.Text.Trim()+"' And r.Active=1", cn);
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.HasRows)
        {
            Session["User"] = txtUserName.Text.Trim();

            ProjectUtility.bLogin = 1;
            ProjectUtility.bLogoutClicked = 0;

            Response.Redirect("Products.aspx");
        }
        else
        {
            i = 1;
            Response.Redirect("Home.aspx");
        }
        cn.Close();
    }
}