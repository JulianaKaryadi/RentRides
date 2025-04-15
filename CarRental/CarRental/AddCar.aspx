<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCar.aspx.cs" Inherits="CarRental.AddCar" MasterPageFile="~/CustNav.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/AddCar.css" rel="stylesheet" />
    <title>Add New Car</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="form-container">
            <h2>Add New Car</h2>
            <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
            <label for="txtBrand">Brand</label>
            <asp:TextBox ID="txtBrand" runat="server"></asp:TextBox>
            <label for="txtType">Type</label>
            <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
            <label for="txtYear">Year</label>
            <asp:TextBox ID="txtYear" runat="server"></asp:TextBox>
            <label for="txtColor">Color</label>
            <asp:TextBox ID="txtColor" runat="server"></asp:TextBox>
            <label for="txtTransmission">Transmission</label>
            <asp:TextBox ID="txtTransmission" runat="server"></asp:TextBox>
            <label for="txtSeats">Seats</label>
            <asp:TextBox ID="txtSeats" runat="server"></asp:TextBox>
            <label for="txtFuelType">Fuel Type</label>
            <asp:TextBox ID="txtFuelType" runat="server"></asp:TextBox>
            <label for="txtDailyRate">Daily Rate</label>
            <asp:TextBox ID="txtDailyRate" runat="server"></asp:TextBox>
            <label for="txtNoPlat">License Plate</label>
            <asp:TextBox ID="txtNoPlat" runat="server"></asp:TextBox>
            <label for="fuCarImage">Car Image</label>
            <asp:FileUpload ID="fuCarImage" runat="server" />
            <asp:Button ID="btnAddCar" runat="server" Text="Add Car" OnClick="btnAddCar_Click" />
        </div>
        <div class="grid-container">
            <asp:GridView ID="gvCarDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvCarDetails_PageIndexChanging" OnRowCommand="gvCarDetails_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Brand" HeaderText="Brand" />
                    <asp:BoundField DataField="Type" HeaderText="Type" />
                    <asp:BoundField DataField="Year" HeaderText="Year" />
                    <asp:BoundField DataField="Color" HeaderText="Color" />
                    <asp:BoundField DataField="Transmission" HeaderText="Transmission" />
                    <asp:BoundField DataField="Seats" HeaderText="Seats" />
                    <asp:BoundField DataField="FuelType" HeaderText="Fuel Type" />
                    <asp:BoundField DataField="DailyRate" HeaderText="Daily Rate" />
                    <asp:BoundField DataField="NoPlat" HeaderText="License Plate" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteCar" CommandArgument='<%# Eval("NoPlat") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
