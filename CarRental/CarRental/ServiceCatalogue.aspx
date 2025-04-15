<%@ Page Title="Service Catalogue" Language="C#" MasterPageFile="~/CustNav.Master" AutoEventWireup="true" CodeBehind="ServiceCatalogue.aspx.cs" Inherits="CarRental.ServiceCatalogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/ServiceCatalogue.css" rel="stylesheet" />
    <title>Service Catalogue</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="filter-container">
        <label for="ddlBrand">Brand:</label>
        <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterCars">
        </asp:DropDownList>

        <label for="ddlSeats">Seats:</label>
        <asp:DropDownList ID="ddlSeats" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterCars">
            <asp:ListItem Value="" Text="All" />
            <asp:ListItem Value="5" Text="5" />
            <asp:ListItem Value="7" Text="7" />
        </asp:DropDownList>

        <label for="txtMinRate">Min Rate:</label>
        <asp:TextBox ID="txtMinRate" runat="server" TextMode="Number" AutoPostBack="true" OnTextChanged="FilterCars" />

        <label for="txtMaxRate">Max Rate:</label>
        <asp:TextBox ID="txtMaxRate" runat="server" TextMode="Number" AutoPostBack="true" OnTextChanged="FilterCars" />

        <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="FilterCars" />
    </div>

    <div class="car-catalogue">
        <asp:Repeater ID="rptCarDetails" runat="server">
            <ItemTemplate>
                <div class="car-item">
                    <asp:Image ID="imgCar" runat="server" ImageUrl='<%# Eval("CarImage", "~/Images/{0}") %>' AlternateText="Car Image" />
                    <h3><%# Eval("Brand") %> <%# Eval("Type") %></h3>
                    <p>Year: <%# Eval("Year") %></p>
                    <p>Color: <%# Eval("Color") %></p>
                    <p>Transmission: <%# Eval("Transmission") %></p>
                    <p>Seats: <%# Eval("Seats") %></p>
                    <p>Fuel Type: <%# Eval("FuelType") %></p>
                    <p>Daily Rate: RM<%# Eval("DailyRate", "{0:F2}") %></p>
                    <p>License Plate: <%# Eval("NoPlat") %></p>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
