<%@ Page Title="RentRides" Language="C#" MasterPageFile="~/CustNav.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarRental.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
        }
        .center-table {
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
            margin: 20px 0;
        }
        .table-container {
            width: 100%;
            padding: 20px;
            background-color: #f9f9f9;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 10px;
        }
        .table-responsive {
            width: auto;
            overflow-x: auto;
        }
        .available-cars-heading {
            text-align: center;
            font-size: 2em;
            color: #333;
            margin-bottom: 20px;
            text-transform: uppercase;
        }
        .banner-image {
            display: block;
            margin: 20px auto;
            max-width: 100%;
            height: auto;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .table {
            width: 100%;
            margin-bottom: 1rem;
            color: #212529;
        }
        .table-striped tbody tr:nth-of-type(odd) {
            background-color: rgba(0, 0, 0, 0.05);
        }
        .table-bordered {
            border: 1px solid #dee2e6;
        }
        .table thead th {
            vertical-align: bottom;
            border-bottom: 2px solid #dee2e6;
        }
        .table-bordered td, .table-bordered th {
            border: 1px solid #dee2e6;
        }
        .table th, .table td {
            padding: 0.75rem;
            vertical-align: top;
            border-top: 1px solid #dee2e6;
        }
        .table thead th {
            background-color: #333;
            color: white;
            text-align: left;
        }
        .table tfoot th {
            background-color: #f1f1f1;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-container center-table">
        <img src="images/carbannerr.png" />
       
        <h2 class="available-cars-heading">Available Cars to Rent</h2>
        <div class="table-responsive">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connRental %>" SelectCommand="SELECT [Brand], [Type], [Year], [Color], [Transmission], [Seats], [DailyRate], [CarImage] FROM [CarDetails]"></asp:SqlDataSource>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Brand" DataSourceID="SqlDataSource1" AllowPaging="True" CssClass="table table-striped table-bordered" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                <Columns>
                    <asp:ImageField DataImageUrlField="CarImage" DataImageUrlFormatString="~/images/{0}" HeaderText="Image" ControlStyle-ForeColor="#660066" ControlStyle-Height="200px">
                        <ControlStyle ForeColor="#660066" Height="200px"></ControlStyle>
                    </asp:ImageField>
                    <asp:BoundField DataField="Brand" HeaderText="Brand" SortExpression="Brand" ControlStyle-ForeColor="#660066">
                        <ControlStyle ForeColor="#660066" Width="150px"></ControlStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" ControlStyle-ForeColor="#660066">
                        <ControlStyle ForeColor="#660066" Width="150px"></ControlStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Seats" HeaderText="Seats" SortExpression="Seats" ControlStyle-ForeColor="#660066">
                        <ControlStyle ForeColor="#660066" Width="150px"></ControlStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DailyRate" HeaderText="Daily Rate" SortExpression="DailyRate" DataFormatString="{0:c2}" ControlStyle-ForeColor="#660066" ControlStyle-Width="60px">
                        <ControlStyle ForeColor="#660066" Width="150px"></ControlStyle>
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
