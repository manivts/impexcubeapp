<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup.aspx.cs" Inherits="ImpexCube.Billing.popup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function assignvalues(QuoteParticulars) { debugger
            QuoteParticulars = window.open("PIPLinvoiceSTAX.aspx?QuoteParticulars=" + QuoteParticulars, 'QuoteParticulars', 'height=300,width=800,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no, status=No');

        }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <table>

                                                                        <tr>
                                                                            <td align="center" style="vertical-align: top; text-align: left;">
                                                                           
                                                                                    <asp:GridView ID="GridView2" runat="server" Width="336px" Font-Size="8pt" Font-Names="Arial"
                                                                                        CellPadding="4" AutoGenerateColumns="False" DataKeyNames="QuoteNo" OnSelectedIndexChanged="GridView2_SelectedIndexChanged"
                                                                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                                                                        ForeColor="Black" GridLines="Vertical">
                                                                                        <FooterStyle BackColor="#CCCC99" />
                                                                                        <RowStyle Font-Names="Arial" Font-Size="8pt" BackColor="#F7F7DE" />
                                                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" Font-Names="Arial"
                                                                                            Font-Size="8pt" />
                                                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" Font-Names="Arial"
                                                                                            Font-Size="8pt" />
                                                                                        <Columns>
                                                                                            <asp:CommandField ShowSelectButton="True" ButtonType="Button">
                                                                                                <ControlStyle Font-Names="Arial" Font-Size="8pt" />
                                                                                            </asp:CommandField>
                                                                                            <asp:BoundField DataField="QuoteNo" HeaderText="Code" SortExpression="QuoteNo">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="CustomerName" HeaderText="Contract Name" SortExpression="CustomerName">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="CustomerName" HeaderText="Party Name" SortExpression="CustomerName">
                                                                                                <HeaderStyle Wrap="False" />
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                         
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                    </asp:GridView>
                                                                          
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="vertical-align: top;" align="left">
                                                                         
                                                                                    <asp:GridView ID="GridView3" runat="server" Width="336px" Font-Size="8pt" Font-Names="Arial"
                                                                                        CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="GridView3_RowDataBound"
                                                                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                                                                        ForeColor="Black" GridLines="Vertical" 
                                                                                        onselectedindexchanged="GridView3_SelectedIndexChanged">
                                                                                        <FooterStyle BackColor="#CCCC99" />
                                                                                        <RowStyle Font-Names="Arial" Font-Size="8pt" BackColor="#F7F7DE" />
                                                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" Font-Names="Arial"
                                                                                            Font-Size="8pt" />
                                                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                                                        <HeaderStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chk" runat="server" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="ID" SortExpression="sno">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="ActualRate" HeaderText="Actual Rate" SortExpression="ActualRate">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="MinRate" HeaderText="Min Rate" SortExpression="MinRate">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="VarRate" HeaderText="Var Rate" SortExpression="VarRate">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="MaxRate" HeaderText="Max Rate" SortExpression="MaxRate">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="FixRate" HeaderText="Fixed Rate" SortExpression="FixRate">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Unit" HeaderText="unit" SortExpression="unit">
                                                                                                <ItemStyle Wrap="False" />
                                                                                            </asp:BoundField>
                                                                                     
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                    </asp:GridView>
                                                                             
                                                                            </td>
                                                                        </tr>
                                                                   
                                                                        <tr id="Tr2" runat="server">
                                                                            <td style="vertical-align: top;" align="left">
                                                                     
                                                                                    <asp:GridView ID="GrdPaddr" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                                        BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" DataKeyNames="BranchId"
                                                                                        Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="GrdPaddr_SelectedIndexChanged"
                                                                                        Width="674px">
                                                                                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="AccountCode" HeaderText="PCODE" SortExpression="AccountCode" />
                                                                                            <asp:BoundField DataField="BranchId" HeaderText="Branch" SortExpression="BranchId" />
                                                                                            <asp:BoundField DataField="Address1" HeaderText="Address" SortExpression="Address1" />
                                                                                            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                                                                                            <asp:BoundField DataField="State" HeaderText="State" SortExpression="Pin" />
                                                                                            <asp:BoundField DataField="Pincode" HeaderText="Pin" SortExpression="Pincode" />
                                                                                            <asp:CommandField ButtonType="Image" HeaderText="CL" SelectImageUrl="image/select.JPG"
                                                                                                ShowSelectButton="True">
                                                                                                <HeaderStyle Height="5px" />
                                                                                                <ItemStyle Height="5px" Width="5px" />
                                                                                            </asp:CommandField>
                                                                                        </Columns>
                                                                                        <RowStyle BackColor="White" ForeColor="#003399" />
                                                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                                                        <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" />
                                                                                    </asp:GridView>
                                                                        
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                    <td>
                                                        <asp:Button ID="BtnContract_Submit" runat="server" Text="Submit" OnClick="BtnContract_Submit_Click" />
                                                        <asp:Button ID="BtnClose" runat="server" Text="Close" OnClick="BtnClose_Click" />
                                                    </td>
                                                </tr>
                                                                 </table>

    </form>
</body>
</html>



