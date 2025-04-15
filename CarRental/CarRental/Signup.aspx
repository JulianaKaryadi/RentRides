<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/CustNav.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="CarRental.SignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/SignUp.css" rel="stylesheet" />
    <title>Sign Up</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="signup-container">
        <h2>Sign Up</h2>
        <asp:ValidationSummary ID="vsSummary" runat="server" CssClass="status-label" HeaderText="Please correct the following errors:" />

        <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required." ForeColor="Red"></asp:RequiredFieldValidator>

        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password must be at least 6 characters long and contain at least one number and one letter." ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$" ForeColor="Red"></asp:RegularExpressionValidator>

        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid email format." ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ForeColor="Red"></asp:RegularExpressionValidator>

        <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label>
        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone number is required." ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Invalid phone number format." ValidationExpression="^\d{10}$" ForeColor="Red"></asp:RegularExpressionValidator>

        <asp:Label ID="lblStatus" runat="server" CssClass="status-label"></asp:Label>
        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CausesValidation="false" />
    </div>
</asp:Content>
