<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="ImpexCube.Billing.WebForm2" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
        <table>
            <tr>
                <td style="height: 90%; vertical-align: top; background-color: white; width: 100%;">
                    <table>
                     <tr>
    <td>
        <asp:Label ID="Label1" runat="server" Text="Select Export Type :" Font-Names="verdana" Font-Size="8pt"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="drTpye" Font-Names="verdana" Font-Size="8pt" runat="server">
            <asp:ListItem Value="PD">PDF File Format</asp:ListItem>
            <asp:ListItem Value="WF">Word Format</asp:ListItem>
        </asp:DropDownList>
    </td>
    <td>
        <asp:Button ID="BtnExport" runat="server" Font-Names="verdana" Font-Size="8pt" 
            Text="Export File" />
    </td>
    </tr>
                        <tr>
                            <td style="width: 572px">
                                 <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                                     AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="1202px" 
                                     ReportSourceID="CrystalReportSource1" ToolbarImagesFolderUrl="" 
                                     ToolPanelWidth="200px" Width="1104px" ReuseParameterValuesOnRefresh="True" 
                                     ToolPanelView="None" />
                               
                                
                              
                               
                                 <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                                     <Report FileName="D:\Dharma\Doing\ImpexCube\ImpexCube\Billing\CryInvoice.rpt">
                                     </Report>
                                 </CR:CrystalReportSource>
                               
                                
                              
                               
                               </td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 686px">
                                &nbsp;</td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
