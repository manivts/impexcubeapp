<%@ Page Title="Payment Register" Language="C#" MasterPageFile="~/Accounts/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="PaymentReport.aspx.cs" Inherits="AccountsManagement.PaymentReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .waterText
        {
            font-family: Arial;
            font-size: 8pt;
            color: Fuchsia;
            
            background-color: #FFFFFF;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">&nbsp; &nbsp;&nbsp;
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <table style="width: 76%;" align="center">
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="Label1" runat="server"  
                    Text="Payment Register" CssClass="labeltitle"></asp:Label>
            </td>
        </tr>
             <tr>
            <td style="text-align: left; width:75px;">
                <asp:Label ID="lblFromDate" runat="server" Text="From Date" 
                    CssClass="labelsize" ></asp:Label>
                </td>
                <td style="text-align: left;">
                <asp:TextBox ID="txtFrom" runat="server" MaxLength="10" 
                     CssClass="txtbox100"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:FilteredTextBoxExtender ID="FTEDate" TargetControlID="txtFrom" FilterType="Numbers,Custom"
                    ValidChars="01/01/1999" runat="server">
                </cc1:FilteredTextBoxExtender>
            </td>
            </tr>
             <tr>     
               <td style="text-align: left; width:75px;" >
                   <asp:Label ID="lblToDate" runat="server" Text="To Date" CssClass="labelsize"></asp:Label>
                   </td>
                   <td style="text-align: left;">
                <asp:TextBox ID="txtTo" runat="server"  MaxLength="10" 
                           CssClass="txtbox100"></asp:TextBox>
                <cc1:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                    TargetControlID="txtTo">
                </cc1:CalendarExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtTo"
                    FilterType="Numbers,Custom" ValidChars="01/01/1999" runat="server">
                </cc1:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
        <td style="text-align: left; width:75px;"  class="labelsize">
            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
        </td>
        <td style="text-align: left;">
            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl100">
            <asp:ListItem Value="All" Text="All"></asp:ListItem>
            <asp:ListItem Value="Cash" Text="Cash"></asp:ListItem>
            <asp:ListItem Value="Bank" Text="Bank"></asp:ListItem>
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="Button1" runat="server" Text="GetReport" 
                    OnClick="btnGetReport_Click" CssClass="btn100" />
                &nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" Text="ExportToExcel" 
                    OnClick="btnExcel_Click" CssClass="btn100" />
                &nbsp;&nbsp;
                <asp:Button ID="Button3" runat="server" Text="Exit" Width="50px" 
                    OnClick="btnExit_Click" CssClass="btn70" />
            </td>
        </tr>
        
        <%--<tr>
            <td style="text-align: left; width: 350px">
                <label>
                    Narration &nbsp;&nbsp;&nbsp;
                </label>
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlNarration" runat="server" Width="200px"
                    AppendDataBoundItems="true">
                    <asp:ListItem>--Select--</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="2" style="text-align: left">
                <label>
                    Account Cr&nbsp;&nbsp;</label>
                &nbsp;&nbsp;
                <asp:DropDownList ID="ddlAccountCr" runat="server" Width="200px" AppendDataBoundItems="true">
                    <asp:ListItem>--Select--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>--%>
       
        <tr>
            <td colspan="2" style="text-align: left">
                <asp:GridView ID="gvPaymentReport" runat="server" Style="text-align: left" AutoGenerateColumns="true"
                    BorderColor="Black" BorderStyle="Solid" Font-Names="Arial" BorderWidth="1px"
                    Font-Size="10pt" ForeColor="Black" GridLines="Vertical" ShowFooter="false" ShowHeader="true"
                    Width="100%" OnSelectedIndexChanged="gvPaymentReport_SelectedIndexChanged"
                    AllowSorting="True" 
                    OnPageIndexChanging="gvPaymentReport_PageIndexChanging" 
                    OnSorting="gvPaymentReport_Sorting">
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" Width="350px" />
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="#E7E7FF" />
                    <AlternatingRowStyle BackColor="White" />
                   <%-- <Columns>
                        <asp:CommandField ButtonType="Image" ItemStyle-Font-Bold="false" HeaderText="Print"
                            ItemStyle-Font-Names="Arial" ShowSelectButton="true" SelectImageUrl="~/Image/print-icon.png">
                            <ItemStyle Font-Names="Arial" Font-Bold="False"></ItemStyle>
                        </asp:CommandField>
                    </Columns>--%>
                </asp:GridView>
            </td>
        </tr>
        
        </table>
       

</asp:Content>
