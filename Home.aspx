<%@ Page Title="" Language="C#" MasterPageFile="~/GrocerySite.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <div class="jumbotron">
        <h1>FruitsVeggiesBasket</h1>
        <p class="lead">Fresh Vegetables & Fruits at Your Doorstep!.</p>
       </div>

     
    <div>
        <div style="float: left; width:300px;">
            <div class="sideLogin" id="authFrom" runat="server">
               
                <table >
                    <tr>
                        <td>UserName
                        </td>
                        <td style="width: 140px">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="textLogin" />
                        </td>
                        <td style="font-size: 11px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Name" ControlToValidate="txtUserName" ForeColor="#990000"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Password
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="textLogin" />
                        </td>
                        <td style="font-size: 11px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Password" ControlToValidate="txtPassword" ForeColor="#990000" CssClass="font"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="button" OnClick="btnLogin_Click" />&nbsp;&nbsp;
                           
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="authError" style="color:red">
                            <asp:Label ID="lblResult" runat="server" Font-Size="10pt" />
                        </td>
                    </tr>
                </table>
                <br />
             
            </div>
            <br />
            <asp:Label ID="welcomeLable1" runat="server" Font-Size="18pt" style="padding-top: 3px;padding-left: 57px;font-weight: 600;display:flex;"/>
        </div>
</div>
</asp:Content>

