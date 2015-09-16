<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="frmGeneralLedger.aspx.cs" Inherits="AccountsManagement.frmGeneralLedger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        input[type=], textarea
        {
            border: 1px solid #ccc;
        }
        input[type=text]:focus, textarea:focus
        {
            background-color: #F0F0F0;
            border: 1px solid #ccc;
        }
        .hiddenid
        {
            display: none;
        }
        .grid_scroll-GenMaster
        {
            height: 191px;
            overflow: auto;
        }
        .alignment
        {
            text-align:left;
        }
        .Column1
        {
           padding-left:402px;
            width:150px;
        }
        .Column2
        {
            height:30px;
            width:400px;
            padding-left:511px;
            margin-top:-15px;
        }
        
       
        
    </style>
<script type="text/javascript" language="javascript">
    function validation() {
        var LedgerName = document.getElementById('<%=txtLedgerName.ClientID%>').value;
        if (LedgerName == "") {
            alert("Please fill LedgerName ");
            return false;
            document.getElementById("txtLedgerName").focus();
        }
        var Fromdate = document.getElementById('<%=txtFrom.ClientID%>').value;

        if (Fromdate == "") {
            alert("Please Fill From Date");
            return false;
            document.getElementById("txtFrom").focus();
        }
        var Todate = document.getElementById('<%=txtTo.ClientID%>').value;

        if (Fromdate == "") {
            alert("Please Fill To Date");
            return false;
            document.getElementById("txtTo").focus();
        }
        }
    
</script>

    <style type="text/css">
        .waterText
        {
            font-family: Arial;
            font-size: 8pt;
            color: Fuchsia;
            overflow: auto;
            background-color: #FFFFFF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

        <div>
          <asp:Label ID="Label1" runat="server" Text="General Ledger Report" CssClass="labeltitle"></asp:Label>
        </div>

        <br />

        <div> 
           <div class="alignment labelsize Column1">
                <label> Ledger Name </label>       
           </div>

           <div class="alignment labelsize Column2">
               <asp:TextBox ID="txtLedgerName" runat="server" CssClass="txtbox300"></asp:TextBox>
           </div>
        </div>

        <div>
           <div class="alignment labelsize Column1">
             <label> From Date </label>
           </div>

           <div class="alignment labelsize Column2">
             <asp:TextBox ID="txtFrom" runat="server" MaxLength="10" CssClass="txtbox100"></asp:TextBox>
             <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
             </cc1:CalendarExtender>
             <cc1:FilteredTextBoxExtender ID="FTEDate" runat="server" FilterType="Numbers,Custom" TargetControlID="txtFrom" ValidChars="01/01/1999">
             </cc1:FilteredTextBoxExtender>
           </div>
        </div>

        <div>
           <div class="alignment labelsize Column1">
             <label> To Date </label>
           </div>

           <div class="alignment labelsize Column2">
              <asp:TextBox ID="txtTo" runat="server" MaxLength="10" CssClass="txtbox100"></asp:TextBox>
              <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
              </cc1:CalendarExtender>
              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom" TargetControlID="txtTo" ValidChars="01/01/1999">
              </cc1:FilteredTextBoxExtender>
           </div>
        </div>

        <div>
           
        </div>

        <div>
           <asp:Button ID="btnGetReport" runat="server" OnClick="btnGetReport_Click" 
                          OnClientClick="javascript:return validation();"  Text="GetReport" CssClass="btn100" Height="26px" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnExcel" runat="server" Text="ExportToExcel" 
                            onclick="btnExcel_Click" CssClass="btn100" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnExit" runat="server" Text="Exit" Width="50px" 
                            onclick="btnExit_Click" CssClass="btn70" />
        </div>

        
         <div>
             <asp:Label ID="lblOBalance" runat="server" Text="0"></asp:Label>
         </div>
        
           <div>
           <asp:GridView ID="gvGeneralLedgerReport" runat="server" AllowPaging="True" AllowSorting="True"
                             BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="Arial" Font-Size="10pt" ForeColor="Black" GridLines="Vertical" ShowFooter="false"
                            ShowHeader="true" Style="text-align: left" Width="100%" 
                            AutoGenerateColumns="false" 
                            onpageindexchanging="gvGeneralLedgerReport_PageIndexChanging" 
                            onselectedindexchanged="gvGeneralLedgerReport_SelectedIndexChanged">
                            <FooterStyle BackColor="#C6C3C6" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                ForeColor="Black" Width="350px" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderText="Print" ItemStyle-Font-Bold="false"
                                    ItemStyle-Font-Names="Arial" SelectImageUrl="~/Accounts/AccImages/print-icon.png" ShowSelectButton="true">
                                    <ItemStyle Font-Bold="False" Font-Names="Arial" />
                                </asp:CommandField>
                                <asp:BoundField HeaderText="Inv.No" DataField="InvoiceNo" />
                                 <asp:BoundField HeaderText="Inv.Date" DataField="Invoice Date" />
                                  <asp:BoundField HeaderText="Inv.Type" DataField="InvoiceType" />
                                   <asp:BoundField HeaderText="Opening" DataField="opening" />
                                    <asp:BoundField HeaderText="DR/CR-open" DataField="Dr/Cr-op" />
                                     <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                      <asp:BoundField HeaderText="DR/CR-cur" DataField="drcr" />
                                       <asp:BoundField HeaderText="Closing" DataField="closing" />
                                        <asp:BoundField HeaderText="DR/CR-Close" DataField="Dr/Cr-cl" />
                            </Columns>
                        </asp:GridView>
        </div>

        <div>
          <asp:Label ID="lblClosing" runat="server" Text="0" ></asp:Label>
        </div>

            <%--<table align="center" style="width:75%;">
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Label ID="Label1" runat="server" 
                            Text="General Ledger Report" CssClass="labeltitle"></asp:Label>
                    </td>
                </tr>
                <tr>
                <td colspan="2"></td>
                </tr>
                <tr>
                    <td style="text-align: left" class="labelsize" >
                        <label>
                            Ledger Name
                        </label>
                        &nbsp;&nbsp;
                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" class="labelsize">
                        <label>
                            From Date&nbsp;&nbsp;
                        </label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtFrom" runat="server" MaxLength="10" CssClass="txtbox100"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                        </cc1:CalendarExtender>
                        <cc1:FilteredTextBoxExtender ID="FTEDate" runat="server" FilterType="Numbers,Custom"
                            TargetControlID="txtFrom" ValidChars="01/01/1999">
                        </cc1:FilteredTextBoxExtender>
                 </td>
                 </tr>
                 <tr>
                 <td style="text-align: left;" class="labelsize">
                        <labe>
                            To Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         </label>

                        <asp:TextBox ID="txtTo" runat="server" MaxLength="10" CssClass="txtbox100"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
                        </cc1:CalendarExtender>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                            TargetControlID="txtTo" ValidChars="01/01/1999">
                        </cc1:FilteredTextBoxExtender>
                        
                 </td>
                    
                </tr>
                <tr>
                <td></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnGetReport" runat="server" OnClick="btnGetReport_Click" 
                          OnClientClick="javascript:return validation();"  Text="GetReport" CssClass="btn100" Height="26px" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnExcel" runat="server" Text="ExportToExcel" 
                            onclick="btnExcel_Click" CssClass="btn100" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnExit" runat="server" Text="Exit" Width="50px" 
                            onclick="btnExit_Click" CssClass="btn70" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:right;">                        &nbsp;</td>                    
                </tr>
                <tr>
                <td colspan="2" align="right">
                    <asp:Label ID="lblOBalance" runat="server" Text="0"></asp:Label>
                </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:GridView ID="gvGeneralLedgerReport" runat="server" AllowPaging="True" AllowSorting="True"
                             BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="Arial" Font-Size="10pt" ForeColor="Black" GridLines="Vertical" ShowFooter="false"
                            ShowHeader="true" Style="text-align: left" Width="100%" 
                            AutoGenerateColumns="false" 
                            onpageindexchanging="gvGeneralLedgerReport_PageIndexChanging" 
                            onselectedindexchanged="gvGeneralLedgerReport_SelectedIndexChanged">
                            <FooterStyle BackColor="#C6C3C6" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                ForeColor="Black" Width="350px" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderText="Print" ItemStyle-Font-Bold="false"
                                    ItemStyle-Font-Names="Arial" SelectImageUrl="~/Accounts/AccImages/print-icon.png" ShowSelectButton="true">
                                    <ItemStyle Font-Bold="False" Font-Names="Arial" />
                                </asp:CommandField>
                                <asp:BoundField HeaderText="Inv.No" DataField="InvoiceNo" />
                                 <asp:BoundField HeaderText="Inv.Date" DataField="Invoice Date" />
                                  <asp:BoundField HeaderText="Inv.Type" DataField="InvoiceType" />
                                   <asp:BoundField HeaderText="Opening" DataField="opening" />
                                    <asp:BoundField HeaderText="DR/CR-open" DataField="Dr/Cr-op" />
                                     <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                      <asp:BoundField HeaderText="DR/CR-cur" DataField="drcr" />
                                       <asp:BoundField HeaderText="Closing" DataField="closing" />
                                        <asp:BoundField HeaderText="DR/CR-Close" DataField="Dr/Cr-cl" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                </tr>

                <tr>
                    <td colspan="2" align="right">  
                        <asp:Label ID="lblClosing" runat="server" Text="0" ></asp:Label>
                    </td>                    
                    <tr>
                        <td colspan="2" style="text-align:right;">
                            &nbsp;</td>
                    </tr>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
