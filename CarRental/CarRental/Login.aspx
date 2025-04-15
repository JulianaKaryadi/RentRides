<%@ Page Title="Login" Language="C#" MasterPageFile="~/CustNav.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CarRental.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Login.css" rel="stylesheet" />
    <title>Login</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <h2>Login</h2>
        <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required." ForeColor="Red" />

        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." ForeColor="Red" />

        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CausesValidation="false" />

        <asp:Label ID="lblStatus" runat="server" CssClass="status-label"></asp:Label>
    </div>
</asp:Content>
