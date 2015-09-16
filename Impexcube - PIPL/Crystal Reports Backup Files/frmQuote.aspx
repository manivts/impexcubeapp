<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmQuote.aspx.cs" Inherits="ImpexCube.CRM.frmQuote1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%">
        <table style="width: 69%" align="center">
            <tr>
                <td colspan="7" style="text-align: center">
                    Quote
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    Customer Name&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlCusName" runat="server" AppendDataBoundItems="True" 
                        AutoPostBack="True" Width="150px">
                        <asp:ListItem>~Select~</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="center" colspan="7">
                    <strong>20 FEET</strong>
                </td>
            </tr>
            <tr>
                <td class="center">
                    <%--<asp:Panel ID="Panel1" runat="server" GroupingText="20 FEET" 
        style="font-weight: 700; font-size: small">
    </asp:Panel>--%>
                    Descripiton
                </td>
                <td class="center">
                    Unit
                </td>
                <td class="center">
                    At Actual
                </td>
                <td class="center">
                    Minimum
                </td>
                <td class="center">
                    Variable
                </td>
                <td class="center">
                    Maximum
                </td>
                <td class="center">
                    Fixed
                </td>
            </tr>
            <tr>
                <td class="left">
                    <asp:DropDownList ID="ddl20FeetCharges" runat="server" Width="200px" 
                        AppendDataBoundItems="True" AutoPostBack="True" 
                        onselectedindexchanged="ddl20FeetCharges_SelectedIndexChanged">
                        <asp:ListItem>~Select~</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:DropDownList ID="ddl20FeetUnit" runat="server" Width="100px">
                        <asp:ListItem>~Select~</asp:ListItem>
                        <asp:ListItem>PER B/E</asp:ListItem>
                        <asp:ListItem>PER shipment</asp:ListItem>
                        <asp:ListItem>PER Flat Rate</asp:ListItem>
                        <asp:ListItem>PER Kg</asp:ListItem>
                        <asp:ListItem>PER Contr</asp:ListItem>
                        <asp:ListItem>PER TON</asp:ListItem>
                        <asp:ListItem>PER PKG</asp:ListItem>
                        <asp:ListItem>PER LICENSE</asp:ListItem>
                        <asp:ListItem>PER TRA</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt20FeetAtActual" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt20feetMinimum" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt20feetVariable" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt20feetMaximum" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt20feetFixed" runat="server" Width="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="center" colspan="7">
                    <strong>40 FEET</strong>
                </td>
            </tr>
            <tr>
                <td class="center">
                    <%--<asp:Panel ID="Panel1" runat="server" GroupingText="20 FEET" 
        style="font-weight: 700; font-size: small">
    </asp:Panel>--%>
                    Descripiton
                </td>
                <td class="center">
                    Unit
                </td>
                <td class="center">
                    At Actual
                </td>
                <td class="center">
                    Minimum
                </td>
                <td class="center">
                    Variable
                </td>
                <td class="center">
                    Maximum
                </td>
                <td class="center">
                    Fixed
                </td>
            </tr>
            <tr>
                <td class="left">
                    <asp:DropDownList ID="ddl40FeetCharges" runat="server" Width="200px" 
                        AppendDataBoundItems="True" AutoPostBack="True" Enabled="False">
                        <asp:ListItem>~Select~</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:DropDownList ID="ddl40feetUnit" runat="server" Width="100px">
                        <asp:ListItem>~Select~</asp:ListItem>
                        <asp:ListItem>PER B/E</asp:ListItem>
                        <asp:ListItem>PER shipment</asp:ListItem>
                        <asp:ListItem>PER Flat Rate</asp:ListItem>
                        <asp:ListItem>PER Kg</asp:ListItem>
                        <asp:ListItem>PER Contr</asp:ListItem>
                        <asp:ListItem>PER TON</asp:ListItem>
                        <asp:ListItem>PER PKG</asp:ListItem>
                        <asp:ListItem>PER LICENSE</asp:ListItem>
                        <asp:ListItem>PER TRA</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt40FeetAtActual" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt40FeetMinimum" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt40FeetVariable" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt40FeetMaximum" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txt40FeetFixed" runat="server" Width="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="center" colspan="7">
                    <strong>LCL</strong>
                </td>
            </tr>
            <tr>
                <td class="center">
                    <%--<asp:Panel ID="Panel1" runat="server" GroupingText="20 FEET" 
        style="font-weight: 700; font-size: small">
    </asp:Panel>--%>
                    Descripiton
                </td>
                <td class="center">
                    Unit
                </td>
                <td class="center">
                    At Actual
                </td>
                <td class="center">
                    Minimum
                </td>
                <td class="center">
                    Variable
                </td>
                <td class="center">
                    Maximum
                </td>
                <td class="center">
                    Fixed
                </td>
            </tr>
            <tr>
                <td class="left">
                    <asp:DropDownList ID="ddlLCLCharges" runat="server" Width="200px" 
                        AppendDataBoundItems="True" AutoPostBack="True" Enabled="False">
                        <asp:ListItem>~Select~</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:DropDownList ID="ddlLCLUnit" runat="server" Width="100px">
                        <asp:ListItem>~Select~</asp:ListItem>
                        <asp:ListItem>PER B/E</asp:ListItem>
                        <asp:ListItem>PER shipment</asp:ListItem>
                        <asp:ListItem>PER Flat Rate</asp:ListItem>
                        <asp:ListItem>PER Kg</asp:ListItem>
                        <asp:ListItem>PER Contr</asp:ListItem>
                        <asp:ListItem>PER TON</asp:ListItem>
                        <asp:ListItem>PER PKG</asp:ListItem>
                        <asp:ListItem>PER LICENSE</asp:ListItem>
                        <asp:ListItem>PER TRA</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtLCLAtActual" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtLCLMinimum" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtLCLVariable" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtLCLMaximum" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtLCLFixed" runat="server" Width="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="center" colspan="7">
                    <strong>AIR</strong>
                </td>
            </tr>
            <tr>
                <td class="center">
                    <%--<asp:Panel ID="Panel1" runat="server" GroupingText="20 FEET" 
        style="font-weight: 700; font-size: small">
    </asp:Panel>--%>
                    Descripiton
                </td>
                <td class="center">
                    Unit
                </td>
                <td class="center">
                    At Actual
                </td>
                <td class="center">
                    Minimum
                </td>
                <td class="center">
                    Variable
                </td>
                <td class="center">
                    Maximum
                </td>
                <td class="center">
                    Fixed
                </td>
            </tr>
            <tr>
                <td class="left">
                    <asp:DropDownList ID="ddlAIRCharges" runat="server" Width="200px" 
                        AppendDataBoundItems="True" AutoPostBack="True" Enabled="False">
                        <asp:ListItem>~Select~</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:DropDownList ID="ddlAirUnit" runat="server" Width="100px">
                        <asp:ListItem>~Select~</asp:ListItem>
                        <asp:ListItem>PER B/E</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>PER shipment</asp:ListItem>
                        <asp:ListItem>PER Flat Rate</asp:ListItem>
                        <asp:ListItem>PER Kg</asp:ListItem>
                        <asp:ListItem>PER Contr</asp:ListItem>
                        <asp:ListItem>PER TON</asp:ListItem>
                        <asp:ListItem>PER PKG</asp:ListItem>
                        <asp:ListItem>PER LICENSE</asp:ListItem>
                        <asp:ListItem>PER TRA</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtairAtAcutal" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtairminimum" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtairVariable" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtairMaximum" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="left">
                    <asp:TextBox ID="txtairFixed" runat="server" Width="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left" align="right">
                   <asp:Button ID="btnSave" runat="server" Text="Add" onclick="btnSave_Click" 
                        Width="80px" />
                </td>
                <td class="left">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                        onclick="btnUpdate_Click" Visible="False" Width="70px" />
                </td>
                <td class="left">
                    <asp:Button ID="Button2" runat="server" Text="Cancel" onclick="Button2_Click" />
                </td>
                <td class="left">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" onclick="btnPrint_Click" 
                        Width="62px" />
                </td>
                <td class="left">
                    &nbsp;
                </td>
                <td class="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:GridView ID="GvQuote" runat="server" AutoGenerateSelectButton="True" 
                        onselectedindexchanged="GvQuote_SelectedIndexChanged">
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
