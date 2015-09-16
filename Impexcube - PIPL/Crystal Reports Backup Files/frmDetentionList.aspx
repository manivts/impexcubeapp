<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmDetentionList.aspx.cs" Inherits="ImpexCube.frmDetentionList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<%--<script type="text/javascript">
        function validateSupplier()
        {
            if(document.getElementById('<%=drParty.ClientID%>').selectedIndex == 0)
            {
                alert("Please Select Supplier Name");
                document.getElementById('<%=drParty.ClientID%>').focus();
                return false;
            }
        }
    </script>--%>
    <table style="background-color: White; width: 80%;">
        <tr>
            <td>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <table>
                    <tr>
                        <td style="width: 700px; background-color: #ccccff;">
                            <center>
                                <asp:Label ID="Label35" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12pt"
                                    Text="Ground Rent & Detention Reports"></asp:Label></center>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 700px">
                            <asp:Label ID="lbldoc" runat="server" Font-Names="Arial" Font-Size="8pt" Text="DOC Received From :"></asp:Label>
                            <asp:TextBox ID="txtStartDate" runat="server" CausesValidation="True" Font-Names="Arial"
                                Font-Size="8pt" Width="100"></asp:TextBox>
                            &nbsp;
                            <asp:Label ID="lblto" runat="server" Font-Names="Arial" Font-Size="8pt" Text="TO:"></asp:Label>
                            <asp:TextBox ID="txtEndDate" runat="server" Font-Names="Arial" Font-Size="8pt" Width="100"></asp:TextBox>
                            &nbsp;
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                Text="SUPPLIER:" Width="65px"></asp:Label>&nbsp;
                            <asp:DropDownList ID="drParty" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Height="20px" Width="228px">
                            </asp:DropDownList>
                            <%--<asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="*"
                                ForeColor="Red"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 700px; text-align: center;">
                            <asp:Button ID="Search" runat="server" Font-Names="Arial" Font-Size="8pt" OnClick="Search_Click"
                                Text="Search" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnCancel" runat="server" Font-Names="Arial" Font-Size="9pt" OnClick="BtnCancel_Click"
                                Text="Cancel" CausesValidation="False" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="ExportPage" runat="server" Font-Names="Arial" Font-Size="8pt" Height="28px"
                                OnClick="ExportPage_Click" Text="Export to Excel" Width="98px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 655px; height: 154px;">
                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" Font-Names="Arial" Font-Size="8pt"
                                OnRowDataBound="GridView1_RowDataBound" BackColor="White" BorderColor="#E0E0E0"
                                BorderStyle="Solid" BorderWidth="1px">
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <RowStyle BackColor="White" ForeColor="Black" />
                                <EditRowStyle ForeColor="Black" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Wrap="False"
                                    HorizontalAlign="Left" />
                                <AlternatingRowStyle ForeColor="Black" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:RequiredFieldValidator ID="RFV2" runat="server" ControlToValidate="txtStartDate"
                    Display="None" ErrorMessage="Please Give Dates" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="red" Width="94px"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                        ID="VC2" runat="server" TargetControlID="RFV2" Width="250px">
                    </cc1:ValidatorCalloutExtender>
            </td>
        </tr>
    </table>
</asp:Content>
