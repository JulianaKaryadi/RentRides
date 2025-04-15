<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSignUp.aspx.cs" Inherits="CarRental.AdminSignUp" MasterPageFile="~/CustNav.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/AdminSignUp.css" rel="stylesheet" />
    <title>Admin Sign Up</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="signup-container">
        <h2>Admin Sign Up</h2>
        <asp:Label ID="lblStatus" runat="server" Text="" CssClass="status-label"></asp:Label>
        <br />
        <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required" ForeColor="Red" Display="Dynamic" />
        <br /><br />
        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" ForeColor="Red" Display="Dynamic" />
        <br /><br />
        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" />
        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid email format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Display="Dynamic" />
        <br /><br />
        <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label>
        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone number is required" ForeColor="Red" Display="Dynamic" />
        <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Invalid phone number format" ValidationExpression="^\d{10,15}$" ForeColor="Red" Display="Dynamic" />
        <br /><br />
        <asp:Label ID="lblRole" runat="server" Text="Role"></asp:Label>
        <asp:DropDownList ID="ddlRole" runat="server">
            <asp:ListItem Value="user" Text="User"></asp:ListItem>
            <asp:ListItem Value="admin" Text="Admin"></asp:ListItem>
        </asp:DropDownList>
        <br /><br />
        <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" OnClick="btnSignUp_Click" />
    </div>
</asp:Content>
