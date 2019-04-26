<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemoUserControl.aspx.cs" Inherits="MISA.WEBFORM.Demo.DemoUserControl" %>

<%@ Register Src="~/UserControl/Banner.ascx" TagPrefix="uc1" TagName="Banner" %>
<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc1" TagName="Header" %>



<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Demo User Control</title>
</head>
<body>
    <uc1:Header runat="server" id="Header" />
    <uc1:Banner runat="server" ID="Banner" />
</body> 
</html>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" >
            <Columns>
                <asp:TemplateField HeaderText="Column 1">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblField1" Text='<%#  Eval("Title") %>' runat="server" />
                                </td>
                                <td>
                                   <asp:Label ID="lblField2" Text='<%#  Eval("Description") %>' runat="server" />
                                </td>
                                <td>
                                    <a id="Inputtest" Text="Xem chi tiết" runat ="server"  href ='<%#Eval("link")%>'>Xem chi tiết</a>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                   <%-- <EditItemTemplate>
                        <uc:MyControl ID="editForm" runat="server" />
                    </EditItemTemplate>--%>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
