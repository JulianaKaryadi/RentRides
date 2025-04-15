<%@ Page Title="Returns" Language="C#" MasterPageFile="~/CustNav.Master" AutoEventWireup="true" CodeBehind="Returns.aspx.cs" Inherits="CarRental.Returns" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Returns.css" rel="stylesheet" />
    <title>Returns</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="returns-container">
        <asp:Repeater ID="rptReturns" runat="server">
            <ItemTemplate>
                <div class="booking-item">
                    <asp:Image ID="imgCar" runat="server" ImageUrl='<%# Eval("CarImage", "~/Images/{0}") %>' AlternateText="Car Image" />
                    <h3><%# Eval("Brand") %> <%# Eval("Type") %></h3>
                    <p>Booking ID: <%# Eval("BookingId") %></p>
                    <p>Start Date: <%# Eval("StartDate", "{0:MM/dd/yyyy}") %></p>
                    <p>End Date: <%# Eval("EndDate", "{0:MM/dd/yyyy}") %></p>
                    <p>Total Cost: $<asp:Label ID="lblTotalCost" runat="server" Text='<%# Eval("TotalCost") %>'></asp:Label></p>
                    <p>Deposit: $<asp:Label ID="lblDeposit" runat="server" Text='<%# Eval("Deposit") %>'></asp:Label></p>
                    <p>Damage:</p>
                    <asp:DropDownList ID="ddlDamage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDamage_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="No Damage"></asp:ListItem>
                        <asp:ListItem Value="50" Text="Minor Damage - $50"></asp:ListItem>
                        <asp:ListItem Value="100" Text="Moderate Damage - $100"></asp:ListItem>
                        <asp:ListItem Value="200" Text="Severe Damage - $200"></asp:ListItem>
                    </asp:DropDownList>
                    <p>Amount Paid:</p>
                    <asp:TextBox ID="txtAmountPaid" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAmountPaid" runat="server" ControlToValidate="txtAmountPaid" ErrorMessage="Amount Paid is required" ForeColor="Red" ValidationGroup='<%# "Group" + Eval("BookingId") %>'></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvAmountPaid" runat="server" ControlToValidate="txtAmountPaid" MinimumValue="0" MaximumValue="10000" Type="Double" ErrorMessage="Enter a valid amount" ForeColor="Red" ValidationGroup='<%# "Group" + Eval("BookingId") %>'></asp:RangeValidator>
                    <p>New Total Cost: RM<asp:Label ID="lblNewTotalValue" runat="server"></asp:Label></p>
                    <asp:Button ID="btnReturn" runat="server" Text="Return" CommandArgument='<%# Eval("BookingId") %>' OnClick="btnReturn_Click" ValidationGroup='<%# "Group" + Eval("BookingId") %>' />
                    <asp:Label ID="lblStatus" runat="server" CssClass="success-message" Visible="false"></asp:Label>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
