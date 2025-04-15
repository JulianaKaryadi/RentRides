<%@ Page Title="History" Language="C#" MasterPageFile="~/CustNav.Master" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="CarRental.History" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/History.css" rel="stylesheet" />
    <title>History</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="history-container">
        <h2>History</h2>
        <asp:Repeater ID="rptHistory" runat="server">
            <ItemTemplate>
                <div class="booking-item">
                    <asp:Image ID="imgCar" runat="server" ImageUrl='<%# Eval("CarImage", "~/Images/{0}") %>' AlternateText="Car Image" />
                    <h3><%# Eval("Brand") %> <%# Eval("Type") %></h3>
                    <p>Booking ID: <%# Eval("BookingId") %></p>
                    <p>Start Date: <%# Eval("StartDate", "{0:yyyy-MM-dd}") %></p>
                    <p>End Date: <%# Eval("EndDate", "{0:yyyy-MM-dd}") %></p>
                    <p>Rental Cost: $<%# Eval("TotalCost", "{0:F2}") %></p>
                    <p>Deposit: $<%# (Convert.ToDecimal(Eval("TotalCost")) * 0.4m).ToString("F2") %></p>
                    <p>Damage Cost: $<%# Eval("DamageCost", "{0:F2}") %></p>
                    <p>Total Cost: $<%# (Convert.ToDecimal(Eval("TotalCost")) + Convert.ToDecimal(Eval("DamageCost"))) %></p>
                    <asp:Label ID="lblRatingDisplay" runat="server" Text='<%# Eval("Rating") != DBNull.Value ? "Your Rating: " + Eval("Rating") + " stars" : "Rating: Not rated yet" %>'></asp:Label>
                    <br />
                    <asp:Label ID="lblCommentDisplay" runat="server" Text='<%# Eval("Comment") != DBNull.Value ? "Your Comment: " + Eval("Comment") : "Comment: No comment yet" %>'></asp:Label>
                    <br /><br />
                    <asp:Label ID="lblRating" runat="server" Text="Rate:"></asp:Label>
                    <asp:RadioButtonList ID="rblRating" runat="server" RepeatDirection="Horizontal" ValidationGroup='<%# "Group" + Eval("BookingId") %>'>
                        <asp:ListItem Value="1" Text="★"></asp:ListItem>
                        <asp:ListItem Value="2" Text="★"></asp:ListItem>
                        <asp:ListItem Value="3" Text="★"></asp:ListItem>
                        <asp:ListItem Value="4" Text="★"></asp:ListItem>
                        <asp:ListItem Value="5" Text="★"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="rfvRating" runat="server" ControlToValidate="rblRating" InitialValue="" ErrorMessage="Please select a rating." ForeColor="Red" ValidationGroup='<%# "Group" + Eval("BookingId") %>' />
                    <br /><br />
                    <asp:Label ID="lblComment" runat="server" Text="Comment:"></asp:Label>
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="3" CssClass="comment-box" Placeholder="Enter your comment" ValidationGroup='<%# "Group" + Eval("BookingId") %>'></asp:TextBox>
                    <asp:Button ID="btnRate" runat="server" Text="Submit" CommandArgument='<%# Eval("BookingId") %>' OnClick="btnRate_Click" ValidationGroup='<%# "Group" + Eval("BookingId") %>' />
                    <asp:Label ID="lblAppreciation" runat="server" CssClass="appreciation" Visible="False"></asp:Label>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
