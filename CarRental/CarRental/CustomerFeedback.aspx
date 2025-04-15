<%@ Page Title="Customer Feedback" Language="C#" MasterPageFile="~/CustNav.Master" AutoEventWireup="true" CodeBehind="CustomerFeedback.aspx.cs" Inherits="CarRental.CustomerFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/CustomerFeedback.css" rel="stylesheet" />
    <title>Customer Feedback</title>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>Customer Feedback</h2>
        <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnGenerateCSV" runat="server" Text="Generate CSV Report" OnClick="btnGenerateCSV_Click" />
        <asp:Button ID="btnGenerateJSON" runat="server" Text="Generate JSON Report" OnClick="btnGenerateJSON_Click" />
        <asp:Button ID="btnGenerateXML" runat="server" Text="Generate XML Report" OnClick="btnGenerateXML_Click" />
        <br />
        <br />
        <asp:Label ID="lblTotalIncome" runat="server" Text="Total Income: RM" />
        <br />
        <br />
        <div class="stats-container">
            <asp:Label ID="lblTopUser" runat="server" Text="Top User: " />
            <br />
            <asp:Label ID="lblTotalRating" runat="server" Text="Total Rating: " />
        </div>
        <div class="chart-container">
            <canvas id="monthlyBookingsChart"></canvas>
        </div>
        <div class="chart-container">
            <canvas id="monthlyIncomeChart"></canvas>
        </div>
        <div class="table-container">
            <h3>Monthly Income</h3>
            <asp:GridView ID="gvMonthlyIncome" runat="server" AutoGenerateColumns="true" />
        </div>
        <div class="table-container">
            <h3>Income by Car</h3>
            <asp:GridView ID="gvIncomeByCar" runat="server" AutoGenerateColumns="true" />
        </div>
        <div class="chart-container">
            <canvas id="feedbackSentimentChart" style="width: 400px; height: 350px;"></canvas>
        </div>
        <div class="stats-container">
            <asp:Label ID="lblPositiveFeedback" runat="server" Text="Positive Feedback: " />
            <br />
            <asp:Label ID="lblNeutralFeedback" runat="server" Text="Neutral Feedback: " />
            <br />
            <asp:Label ID="lblNegativeFeedback" runat="server" Text="Negative Feedback: " />
        </div>
        <div class="table-container">
            <h3>Contact Users</h3>
            <asp:GridView ID="gvContactUsers" runat="server" AutoGenerateColumns="false" OnRowCommand="gvContactUsers_RowCommand">
                <Columns>
                    <asp:BoundField DataField="UserId" HeaderText="User ID" />
                    <asp:BoundField DataField="Username" HeaderText="Username" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone Number" />
                    <asp:TemplateField HeaderText="Contact">
                        <ItemTemplate>
                            <asp:Button ID="btnContact" runat="server" Text="Contact" CommandName="ContactUser" CommandArgument='<%# Eval("Email") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="table-container">
            <asp:GridView ID="gvFeedback" runat="server" AutoGenerateColumns="true" />
        </div>
    </div>
</asp:Content>
