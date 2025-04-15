<%@ Page Title="Booking" Language="C#" MasterPageFile="~/CustNav.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="CarRental.Booking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Booking.css" rel="stylesheet" />
    <title>Booking</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="booking-container">
        <div class="booking-form">
            <h2>Book a Car</h2>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
            <asp:Label ID="lblCar" runat="server" Text="Car:"></asp:Label>
            <asp:ListBox ID="lbCars" runat="server" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="lbCars_SelectedIndexChanged"></asp:ListBox>
            <asp:RequiredFieldValidator ID="rfvCars" runat="server" ControlToValidate="lbCars" InitialValue="" ErrorMessage="Please select at least one car." ForeColor="Red" />
            <br /><br />
            <asp:Label ID="lblStartDate" runat="server" Text="Start Date:"></asp:Label>
            <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate" ErrorMessage="Start Date is required." ForeColor="Red" />
            <br /><br />
            <asp:Label ID="lblEndDate" runat="server" Text="End Date:"></asp:Label>
            <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate" ErrorMessage="End Date is required." ForeColor="Red" />
            <br /><br />
            <asp:Label ID="lblTotalDays" runat="server" Text="Total Days:"></asp:Label>
            <asp:Label ID="lblDaysValue" runat="server" Text=""></asp:Label>
            <br /><br />
            <asp:Label ID="lblInsurance" runat="server" Text="Insurance:"></asp:Label>
            <asp:Label ID="lblInsuranceValue" runat="server" Text=""></asp:Label>
            <br /><br />
            <asp:Label ID="lblTax" runat="server" Text="Tax:"></asp:Label>
            <asp:Label ID="lblTaxValue" runat="server" Text=""></asp:Label>
            <br /><br />
            <asp:Label ID="lblTotalCost" runat="server" Text="Total Cost:"></asp:Label>
            <asp:Label ID="lblCostValue" runat="server" Text=""></asp:Label>
            <br /><br />
            <asp:Label ID="lblCalculatedDeposit" runat="server" Text="Calculated Deposit (40% of Total Cost):"></asp:Label>
            <asp:Label ID="lblDepositValue" runat="server" Text=""></asp:Label>
            <br /><br />
            <asp:Button ID="btnCalculate" runat="server" Text="Calculate Cost" OnClick="btnCalculate_Click" />
            <br /><br />
            <asp:Label ID="lblDepositInput" runat="server" Text="Deposit:"></asp:Label>
            <asp:TextBox ID="txtDeposit" runat="server"></asp:TextBox>
            <br /><br />
            <asp:Button ID="btnBook" runat="server" Text="Book" OnClick="btnBook_Click" />
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </div>
        <div class="car-details" id="carDetails" runat="server" Visible="false">
            <asp:Image ID="imgCar" runat="server" AlternateText="Car Image" />
            <h3><asp:Label ID="lblCarName" runat="server" Text=""></asp:Label></h3>
            <p>Year: <asp:Label ID="lblYear" runat="server" Text=""></asp:Label></p>
            <p>Color: <asp:Label ID="lblColor" runat="server" Text=""></asp:Label></p>
            <p>Transmission: <asp:Label ID="lblTransmission" runat="server" Text=""></asp:Label></p>
            <p>Seats: <asp:Label ID="lblSeats" runat="server" Text=""></asp:Label></p>
            <p>Fuel Type: <asp:Label ID="lblFuelType" runat="server" Text=""></asp:Label></p>
            <p>Daily Rate: $<asp:Label ID="lblDailyRate" runat="server" Text=""></asp:Label></p>
        </div>
    </div>
</asp:Content>
