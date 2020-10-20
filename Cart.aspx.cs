using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Cart : System.Web.UI.Page
{
    // depending on user login and logout visibility is checked
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Cart"] != null)
        {
            if (!Page.IsPostBack)
                BindGrid();
        }
        else
        {
            divtotal.Visible = false;
            dvMain.Visible = false;
            dvempty.Visible = true;
        }
    }
    //Binding data to the Ui end from Db
    public void BindGrid()
    {
        DataTable dt = (DataTable)Session["Cart"];
        gvCart.DataSource = dt;
        gvCart.DataBind();
        if (dt.Rows.Count <= 0)
            btnChekout.Visible = false;
        Totalprice();
    }

    //To edit a particular row
    protected void gvCart_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        gvCart.EditIndex = -1;
        BindGrid();
    }
    //rows are been updated and are stored in database
    protected void gvCart_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gvCart.Rows[e.RowIndex];
        Image img = (Image)row.FindControl("imgPd");
        Label lblnam = (Label)row.FindControl("lblName");
        Label price = (Label)row.FindControl("lblPrice");
        Label lbltotalpricegv = (Label)row.FindControl("lbltotPriceGv");
        TextBox txtqty = (TextBox)row.FindControl("txtQty");
        DataTable dt = (DataTable)Session["Cart"];
        DataRow dr = dt.Rows[e.RowIndex];
        if (txtqty.Text == "")
        {
            lblmess.Visible = true;
            btnChekout.Enabled = false;
        }
        else
        {
            lblmess.Visible = false;
            btnChekout.Enabled = true;
            lbltotalpricegv.Text = (decimal.Parse(price.Text) * decimal.Parse(txtqty.Text)).ToString();
            dr["Product_name"] = lblnam.Text;
            dr["ProductPrice"] = price.Text;
            dr["Qty"] = txtqty.Text;
            Decimal Totalprices = Decimal.Parse(lbltotalpricegv.Text);
            dr["TotalPrice"] = Totalprices.ToString();
            dr["Product_img"] = img.ImageUrl;
            Session["Cart"] = dt;
            gvCart.EditIndex = -1;
            BindGrid();
        }
    }
    //function which calculates total price
    private void Totalprice()
    {
        Decimal totalprice = 0;
        for (int i = 0; i < gvCart.Rows.Count; i++)
        {
            Label lb = this.gvCart.Rows[i].FindControl("lbltotPriceGv") as Label;
            Decimal VALUE = Convert.ToDecimal(lb.Text);
            totalprice = totalprice + VALUE;
        }
        lbltotPrice.Text = totalprice.ToString();
    }
    protected void gvCart_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvCart.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    //function to delete particular rows
    protected void gvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)Session["Cart"];
        DataRow dr = dt.Rows[e.RowIndex];
        dr.Delete();
        BindGrid();
        Response.Redirect(Request.RawUrl);
    }
    //On click on success navigation to order success page
    protected void btnChekout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Order.aspx");
    }
}