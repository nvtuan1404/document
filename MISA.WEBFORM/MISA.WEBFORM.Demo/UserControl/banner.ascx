<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banner.ascx.cs" Inherits="MISA.WEBFORM.Demo.UserControl.banner" %>

<asp:Panel ID="MyPanel" runat="server" Width="100%" Height="700">
    <a href="../Contact.aspx"> 
        <asp:Image runat="server" ImageUrl="~/Images/banner.jpg" Width="400" Height="300" />
    </a>
</asp:Panel>