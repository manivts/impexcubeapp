<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmSEWCheckList.aspx.cs" Inherits="ImpexCube.frmSEWCheckList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/css">
 .watermarked {
     font-size: 9pt; 
     color: #2461bf; 
     font-family: Arial; 
   } 
    </script>
    <script type="text/javascript">
        function OpenPopup() {
            window.open("frmImpJobStage.aspx", "List", "scrollbars=no,resizable=no,left=20,top=20,width=700,height=500");
            return false;
        }
    </script>
    <script type="text/javascript">
        function MouseEvents(objRef, evt) {
            var checkbox = objRef.getElementsByTagName("input")[0];
            if (evt.type == "mouseover") {
                objRef.style.backgroundColor = "orange";
            }
            else if (evt.type == "mouseout") {
                if (objRef.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    objRef.style.backgroundColor = "#C2D69B";
                }
                else {
                    objRef.style.backgroundColor = "white";
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="background-color: White; width: 100%;">
        <tr>
            <td>
                <cc1:AutoCompleteExtender ID="autoComplete3" runat="server" CompletionListCssClass="completionList"
                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                    EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetJobNoSEW" ServicePath="~/AutoComplete.asmx"
                    TargetControlID="txtPName">
                </cc1:AutoCompleteExtender>
                <cc1:TextBoxWatermarkExtender ID="TWE1" runat="server" TargetControlID="txtPName"
                    WatermarkText="50102" WatermarkCssClass="watermarked">
                </cc1:TextBoxWatermarkExtender>
                <table style="width: 100%; height: 54px;">
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Label ID="Label1" runat="server" Text="CHECK LIST - REPORTS " Font-Names="Verdana"
                                Font-Size="12pt" ForeColor="Black" Height="20px" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 14px;" colspan="4">
                            <table>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:RadioButtonList ID="rbExpType" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            RepeatDirection="Horizontal" Visible="False">
                                            <asp:ListItem Value="CDE">Cess Duty - Enable</asp:ListItem>
                                            <asp:ListItem Value="CDD">Cess Duty - Disable</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lbldoc" runat="server" Text="Select Job No:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPName" runat="server" CausesValidation="True" Font-Names="Arial"
                                            Font-Size="8pt" Width="166px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="BtnSearch" runat="server" Text="Search" Height="30px" Width="80px"
                                            Font-Names="Arial" Font-Size="8pt" OnClick="BtnSearch_Click" />
                                        <asp:Button ID="BtnCancel" runat="server" Font-Names="Arial" Font-Size="8pt" OnClick="BtnCancel_Click"
                                            Text="Cancel" Height="30px" Width="80px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50PX">
                                        <asp:Label ID="lbldoc0" runat="server" Text="Job No:" 
                                Font-Names="Arial" Font-Size="8pt"></asp:Label>
                        </td>
                        <td style="height: 14px;" width="50PX">
                        <asp:Label ID="lblJobNo" runat="server"
                                Font-Names="Arial" Font-Size="8pt" style="font-weight: 700"></asp:Label>
                            </td>
                        <td width="100PX">
                        <asp:Label ID="Label2" runat="server" Text="Importer Name :" 
                                Font-Names="Arial" Font-Size="8pt"></asp:Label>
                            </td>
                        <td style="height: 14px;">
                        <asp:Label ID="lblImpeName" runat="server" 
                                Font-Names="Arial" Font-Size="8pt" style="font-weight: 700"></asp:Label>
                            </td>
                    </tr>
                </table>
                <table style="width: 100%; height: 54px;">
                    <tr>
                        <td>
                            <div style="width: 76%; height: 350px" class="grid_scroll">
                                <asp:GridView ID="GrdAllJob" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="700px" Font-Names="Arial"
                                    Font-Size="8pt" OnPageIndexChanging="GrdAllJob_PageIndexChanging" OnRowDataBound="GrdAllJob_RowDataBound"
                                    AutoGenerateColumns="False" DataKeyNames="job_no" Font-Strikeout="False" PageSize="20">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle Font-Bold="False" BorderColor="#E0E0E0" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" />
                                    <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Height="10px" />
                                    <%-- <Columns>
                                        <asp:BoundField DataField="be_no" HeaderText="BOE No" SortExpression="be_no">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="be_date" HeaderText="BOE Date" SortExpression="be_date">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="inv_no" HeaderText="INVOICE NO." SortExpression="inv_no">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="inv_date" HeaderText="INVOICE DATE" SortExpression="inv_date">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="INV_VALUE" HeaderText="INVOICE VALUE" SortExpression="INV_VALUE">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CURRENCY" HeaderText="INVOICE CURRENCY" SortExpression="CURRENCY">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="pur_ordno" HeaderText="PO NO" SortExpression="pur_ordno">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="po_itemNo" HeaderText="PO.ITEM NO" SortExpression="po_itemNo">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="model" HeaderText="PART NO" SortExpression="model">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="prod_desc" HeaderText="DESCRIPTION" SortExpression="prod_desc">
                                            <ItemStyle Wrap="false" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="qty" HeaderText="QTY" SortExpression="qty">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nvd" HeaderText="NVD" SortExpression="nvd">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="totalCVDamt" HeaderText="CVD" SortExpression="totalCVDamt">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cus_edu_cess" HeaderText="Customs Educational Cess" SortExpression="cus_edu_cess">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cus_she_cess" HeaderText="Cus Sec &amp; High Edu. Cess"
                                            SortExpression="cus_she_cess">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="addlDutyAmt" HeaderText="Additional Duty" SortExpression="addlDutyAmt">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="totalDUTYamt" HeaderText="TOTAL DUTY" SortExpression="totalDUTYamt">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                    </Columns>--%>
                                </asp:GridView>
                                <br />
                                <asp:GridView ID="GrdCESS" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="76%" Font-Names="Arial"
                                    Font-Size="8pt" OnRowDataBound="GrdCESS_RowDataBound" AutoGenerateColumns="false"
                                    DataKeyNames="jobno" Font-Strikeout="False" PageSize="20">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle Font-Bold="False" BorderColor="#E0E0E0" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" />
                                    <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Height="10px" />
                                    <Columns>
                                        <asp:BoundField DataField="BENo" HeaderText="BE Number">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BEDate" HeaderText="BE Date">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice no">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice date">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InvoiceProductINRValues" HeaderText="Total invoice value(INR)">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InvoiceCurrency" HeaderText="Invoice Currency">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PONo" HeaderText="PO Number">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProductCode" HeaderText="Part No">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="POSrNo" HeaderText="PO Item No.">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProductDesc" HeaderText="Product Description">
                                            <ItemStyle Wrap="false" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Qty" HeaderText="Product Quantity">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotBasicDutyAmt" HeaderText="NVD">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalCVDAmt" HeaderText="CVD">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CVDEDU" HeaderText="Edu CVD">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CVDSEC" HeaderText="Sec&High Edu  CVD">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CEduCess" HeaderText="Customs Edu Cess">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CSHECess" HeaderText="Cus Sec &amp; High Edu. Cess">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SADAmt" HeaderText="Additional Duty" >
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <%--  <asp:BoundField DataField="ExEduCessAmount" HeaderText="Cess Duty" SortExpression="cess_duty">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="TotalDutyAmt" HeaderText="TOTAL DUTY" >
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField  HeaderText="Product Quantity (Addl)" >
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField  HeaderText="Product Quantity Unit (Addl)" >
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" Font-Names="Arial"
                                    Font-Size="8pt" OnPageIndexChanging="GrdAllJob_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                                    AutoGenerateColumns="False" DataKeyNames="job_no" Font-Strikeout="False" PageSize="30"
                                    ShowFooter="True">
                                    <FooterStyle BackColor="White" ForeColor="#000066" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="10px" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle Font-Bold="False" BorderColor="#E0E0E0" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" />
                                    <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" />
                                    <%--  <Columns>
                                        <asp:BoundField DataField="materialCode" HeaderText="PART NO" SortExpression="materialCode">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="prod_desc" HeaderText="PART NAME" SortExpression="prod_desc">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="pur_ordno" HeaderText="PO NO" SortExpression="pur_ordno">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="qty" HeaderText="QTY IN PCS" SortExpression="qty">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="adump_curnc" HeaderText="Currency" SortExpression="adump_curnc">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="adump_CRate" HeaderText="Price" SortExpression="adump_CRate">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="unit2" HeaderText="Total" SortExpression="unit2">
                                            <ItemStyle Wrap="True" />
                                            <FooterStyle BackColor="#FFFFCC" />
                                        </asp:BoundField>
                                    </Columns>--%>
                                </asp:GridView>
                                <br />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="ExportPage" OnClick="ExportPage_Click" runat="server" Text="Export to Excel"
                                Font-Size="8pt" Font-Names="Arial" Width="85px" Height="30px"></asp:Button>
                            <asp:Button ID="BtnExport_CSV" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Height="30px" Text="Export to CSV " Width="85px" OnClick="BtnExport_CSV_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <asp:Label ID="lbROnly" runat="server" ForeColor="Red" Font-Size="9pt" Font-Names="Arial"
                        Width="383px"></asp:Label></center>
                <center>
                    <asp:Label ID="lblerror" runat="server" Font-Size="8pt" Font-Names="Arial" Width="237px"></asp:Label>
                </center>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
</asp:Content>
