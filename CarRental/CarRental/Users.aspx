<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="CarRental.Users" MasterPageFile="~/CustNav.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Users.css" rel="stylesheet" />
    <title>Manage Users</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>Manage Users</h2>
        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserId"
            CssClass="gridview" OnRowEditing="gvUsers_RowEditing" OnRowUpdating="gvUsers_RowUpdating"
            OnRowCancelingEdit="gvUsers_RowCancelingEdit" OnRowDeleting="gvUsers_RowDeleting">
            <Columns>
                <asp:BoundField DataField="UserId" HeaderText="User ID" ReadOnly="True" />
                <asp:TemplateField HeaderText="Username">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtUsername" runat="server" Text='<%# Bind("Username") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required." Display="Dynamic" ForeColor="Red" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("Username") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid email format." ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" Display="Dynamic" ForeColor="Red" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Phone">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone is required." Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Invalid phone number format." ValidationExpression="^\d{10}$" Display="Dynamic" ForeColor="Red" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Role">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRole" runat="server" Text='<%# Bind("Role") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="txtRole" ErrorMessage="Role is required." Display="Dynamic" ForeColor="Red" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRole" runat="server" Text='<%# Bind("Role") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Enabled">
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkEnabled" runat="server" Checked='<%# Bind("Enabled") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEnabledItem" runat="server" Checked='<%# Bind("Enabled") %>' Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
