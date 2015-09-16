<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPIPLinvoice.aspx.cs"
    Inherits="ImpexCube.Billing.frmPIPLinvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%; height: 400px; border: 1px;">
            <tr>
                <td class="style1">
                    <table style="width: 100%;">
                        <tr>
                            <td align="right">
                                <asp:LinkButton ID="Print" runat="server" CausesValidation="False" OnClick="Print_Click"
                                    Font-Names="Arial" Font-Size="8pt">Print All</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 90%; vertical-align: top; background-color: white; width: 755px;">
                                <asp:Panel ID="Panel2" runat="server" Height="1200px" Width="750px" BackColor="white">
                                    <center>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 679px;">
                                                        <tbody>
                                                            <tr style="border-color: #2461BF; border-style: solid; border-width: 1px;">
                                                                <td rowspan="2" style="width: 77px">
                                                                    <asp:Image ID="Image1" runat="server" Height="122px" ImageUrl="image/plogo.jpg"
                                                                        Width="80px" />
                                                                </td>
                                                                <td align="center" style="width: 586px">
                                                                    <asp:Label ID="lblCompName" runat="server" Text="" Font-Size="18pt" Font-Names="Verdana"></asp:Label><br />
                                                                    <asp:Label ID="lblCHANO" runat="server" Text="" Font-Size="8pt"></asp:Label><br />
                                                                    <asp:Label ID="lblSTRegno" runat="server" Text="" Font-Size="8pt"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 586px; height: 52px;">
                                                                    <hr style="border-right: #2461bf thin solid; border-top: #2461bf thin solid; border-left: #2461bf thin solid;
                                                                        border-bottom: #2461bf thin solid; background-color: #2461bf" />
                                                                    <asp:Label ID="lbladdress" runat="server" Font-Names="Verdana" Font-Size="8pt" Text=""></asp:Label><br />
                                                                    <asp:Label ID="lbladdress1" runat="server" Font-Names="Arial" Font-Size="8pt" Text=""></asp:Label><br />
                                                                    <asp:Label ID="lblTele" runat="server" Font-Names="Arial" Font-Size="8pt" Text=""></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 679px;">
                                                        <tbody>
                                                            <tr>
                                                                <td align="center" style="width: 610px; height: 16px">
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Label ID="lblIName" runat="server" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 679px;">
                                                        <tbody>
                                                            <tr style="border-style: solid; border-width: 1px; border-color: #2461BF;">
                                                                <td id="a1" style="width: 321px; height: 17px;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 198px; height: 17px;">
                                                                    <asp:Label ID="Label42" runat="server" Text="Date :" Font-Names="Arial" Font-Size="9pt"
                                                                        Font-Bold="True"></asp:Label>
                                                                    <asp:Label ID="lblDAte" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                                        Width="127px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 679px;">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 56px; height: 26px;">
                                                                    <asp:Label ID="Label6" runat="server" Text="Job No" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="height: 26px; width: 278px;">
                                                                    <asp:Label ID="lblBillno" runat="server" Width="150px" Font-Names="arial" Font-Size="8pt"></asp:Label>
                                                                </td>
                                                                <td style="width: 89px; height: 26px;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 26px; width: 138px;">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 56px; height: 26px;">
                                                                    <asp:Label ID="Label8" runat="server" Text="Name" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="height: 26px; width: 278px;">
                                                                    <asp:TextBox ID="txtCompName" runat="server" Width="250px" Font-Names="Arial" Font-Size="8pt"
                                                                        Height="20px"></asp:TextBox>&nbsp;
                                                                </td>
                                                                <td style="width: 89px; height: 26px;">
                                                                    <asp:Label ID="Label14" runat="server" Text="Job No." Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="height: 26px; width: 138px;">
                                                                    <asp:TextBox ID="txtJobNo" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 56px">
                                                                    <asp:Label ID="Label9" runat="server" Text="Sub Party" Font-Names="Arial" Font-Size="10pt"
                                                                        Width="61px"></asp:Label>
                                                                </td>
                                                                <td style="width: 278px">
                                                                    <asp:TextBox ID="txtSubParty" runat="server" Width="250px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 89px">
                                                                    <asp:Label ID="Label15" runat="server" Text="AWB / BL No." Font-Names="Arial" Font-Size="10pt"
                                                                        Width="91px"></asp:Label>
                                                                </td>
                                                                <td style="width: 138px">
                                                                    <asp:TextBox ID="txtBLNo" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td rowspan="3" style="vertical-align: top; width: 56px;">
                                                                    <asp:Label ID="Label10" runat="server" Text="Address" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="width: 278px">
                                                                    <asp:TextBox ID="txtAddr" runat="server" Width="250px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 89px">
                                                                    <asp:Label ID="Label16" runat="server" Text="BE NO./DT." Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="width: 138px">
                                                                    <asp:TextBox ID="txtBENo" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 278px">
                                                                    <asp:TextBox ID="txtAdd1" runat="server" Width="250px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 89px">
                                                                    <asp:Label ID="Label17" runat="server" Text="Item Imported" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="width: 138px">
                                                                    <asp:TextBox ID="txtImpotItem" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 278px">
                                                                    <asp:TextBox ID="txtCity" runat="server" Width="250px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 89px">
                                                                    <asp:Label ID="Label18" runat="server" Text="Quantity" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="width: 138px">
                                                                    <asp:TextBox ID="txtQty" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 56px; height: 26px;">
                                                                    <asp:Label ID="Label11" runat="server" Text="State" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="width: 278px; height: 26px;">
                                                                    <asp:TextBox ID="txtAdd2" runat="server" Width="250px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 89px; height: 26px;">
                                                                    <asp:Label ID="Label19" runat="server" Text="Ass. Value" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="height: 26px; width: 138px;">
                                                                    <asp:TextBox ID="txtAssValue" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 56px; height: 26px;">
                                                                    <asp:Label ID="Label12" runat="server" Text="Phone" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="width: 278px; height: 26px;">
                                                                    <asp:TextBox ID="txtPhone" runat="server" Width="250px" MaxLength="15" Font-Names="Arial"
                                                                        Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 89px; height: 26px;">
                                                                    <asp:Label ID="Label20" runat="server" Text="No.of CNTR" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="width: 138px; height: 26px;">
                                                                    <asp:TextBox ID="ContNo" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 56px; height: 20px;">
                                                                    <asp:Label ID="Label13" runat="server" Text="Party Ref" Font-Names="Arial" Font-Size="10pt"
                                                                        Width="62px"></asp:Label>
                                                                </td>
                                                                <td style="width: 278px; height: 20px;">
                                                                    <asp:TextBox ID="txtParty_Reff" runat="server" Width="250px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 89px; height: 20px;">
                                                                    <asp:Label ID="Label21" runat="server" Text="Custom Duty" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="height: 20px; width: 138px;">
                                                                    <asp:TextBox ID="txtCustomDuty" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 679px;">
                                                        <tbody>
                                                            <tr id="rHEAD" runat="server">
                                                                <td style="height: 8px; vertical-align: top; width: 49px;">
                                                                    <asp:Label ID="Label22" runat="server" Text="S.No" Font-Size="8pt" Font-Names="Arial"
                                                                        BackColor="White" Font-Bold="False"></asp:Label>
                                                                </td>
                                                                <td style="width: 228px; vertical-align: top;">
                                                                    <asp:Label ID="Label23" runat="server" Text="PARTICULARS" Font-Size="8pt" Font-Names="Arial"
                                                                        BackColor="White" Font-Bold="False" Width="273px"></asp:Label>
                                                                </td>
                                                                <td align="left" style="vertical-align: top; width: 389px;">
                                                                    <asp:Label ID="Label24" runat="server" Text="RECEIPT DETAILS" Font-Size="8pt" Font-Names="Arial"
                                                                        BackColor="White" Font-Bold="False" Width="101px"></asp:Label>
                                                                </td>
                                                                <td style="vertical-align: top; width: 270px;">
                                                                    <asp:Label ID="Label25" runat="server" Text="PAID BY PARTY" Font-Size="8pt" Font-Names="Arial"
                                                                        BackColor="White" Font-Bold="False"></asp:Label>
                                                                </td>
                                                                <td align="right" style="vertical-align: top; width: 206px;">
                                                                    <asp:Label ID="Label26" runat="server" Text="AMOUNT Rs." Font-Size="8pt" Font-Names="Arial"
                                                                        BackColor="White" Font-Bold="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 679px;
                                                        height: 256px;">
                                                        <tbody>
                                                            <tr style="vertical-align: top;">
                                                                <td>
                                                                    <asp:GridView ID="GridView1" runat="server" Font-Names="Arial" Font-Size="8pt" BackColor="White"
                                                                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                                                        GridLines="Vertical" AutoGenerateColumns="False" Font-Overline="False" OnRowDataBound="GridView1_RowDataBound">
                                                                        <FooterStyle BackColor="#CCCC99" />
                                                                        <RowStyle BackColor="#F7F7DE" />
                                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="sno" HeaderText="SNO" SortExpression="Desc">
                                                                                <ItemStyle HorizontalAlign="left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="charge_desc" HeaderText="DESCRIPTION" SortExpression="Desc">
                                                                                <ItemStyle HorizontalAlign="left" Width="550px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT RS." SortExpression="Desc">
                                                                                <ItemStyle HorizontalAlign="RIGHT" Width="100PX" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <table id="TblMST" runat="server" style="border-color: #2461BF; border-style: solid;
                                                                        border-width: 1px; width: 669px; height: 256px;">
                                                                        <tbody>
                                                                            <tr style="vertical-align: top;">
                                                                                <td align="center" style="vertical-align: middle;">
                                                                                    <asp:Label ID="sno1" runat="server" Text="1" BackColor="#FFE0C0" Font-Names="Verdana"
                                                                                        Font-Size="10pt"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 238px">
                                                                                    <asp:TextBox ID="Detail1" runat="server" Width="279px" BorderColor="White" BorderStyle="Solid"
                                                                                        BackColor="#FFE0C0"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 239px">
                                                                                    <asp:TextBox ID="Rcpt1" runat="server" Width="169px" BorderColor="White" BorderStyle="Solid"
                                                                                        BackColor="#FFE0C0"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 80px">
                                                                                    <asp:TextBox ID="ppaid1" runat="server" Width="82px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right" BackColor="#FFE0C0">0</asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="amt1" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right" BackColor="#FFE0C0">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="vertical-align: top;">
                                                                                <td align="center" style="vertical-align: middle;">
                                                                                    <asp:Label ID="sno2" runat="server" Text="2" BackColor="#FFE0C0" Font-Names="Verdana"
                                                                                        Font-Size="10pt"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 238px; height: 26px;">
                                                                                    <asp:TextBox ID="Detail2" runat="server" Width="279px" BorderColor="White" BorderStyle="Solid"
                                                                                        BackColor="#FFE0C0"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px; width: 239px;">
                                                                                    <asp:TextBox ID="Rcpt2" runat="server" Width="169px" BorderColor="White" BorderStyle="Solid"
                                                                                        BackColor="#FFE0C0"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 80px; height: 26px;">
                                                                                    <asp:TextBox ID="ppaid2" runat="server" Width="82px" BorderColor="White" BorderStyle="Solid"
                                                                                        BackColor="#FFE0C0" Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="amt2" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        BackColor="#FFE0C0" Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="vertical-align: top;">
                                                                                <td align="center" style="vertical-align: middle;">
                                                                                    <asp:Label ID="sno3" runat="server" Text="3" BackColor="#FFE0C0" Font-Names="Verdana"
                                                                                        Font-Size="10pt"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 238px">
                                                                                    <asp:TextBox ID="Detail3" runat="server" Width="279px" BorderColor="White" BorderStyle="Solid"
                                                                                        BackColor="#FFE0C0"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 239px">
                                                                                    <asp:TextBox ID="Rcpt3" runat="server" Width="169px" BorderColor="White" BorderStyle="Solid"
                                                                                        BackColor="#FFE0C0"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 80px">
                                                                                    <asp:TextBox ID="ppaid3" runat="server" Width="82px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right" BackColor="#FFE0C0">0</asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="amt3" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        BackColor="#FFE0C0" Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="vertical-align: top;">
                                                                                <td align="center" style="vertical-align: middle;">
                                                                                    <asp:Label ID="sno4" runat="server" Text="4" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 238px; height: 26px;">
                                                                                    <asp:TextBox ID="Detail4" runat="server" Width="279px" BorderColor="White" BorderStyle="Solid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px; width: 239px;">
                                                                                    <asp:TextBox ID="Rcpt4" runat="server" Width="169px" BorderColor="White" BorderStyle="Solid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 80px; height: 26px;">
                                                                                    <asp:TextBox ID="ppaid4" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="amt4" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="vertical-align: top;">
                                                                                <td align="center" style="vertical-align: middle;">
                                                                                    <asp:Label ID="sno5" runat="server" Text="5" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 238px; height: 26px;">
                                                                                    <asp:TextBox ID="Detail5" runat="server" Width="279px" BorderColor="White" BorderStyle="Solid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 26px; width: 239px;">
                                                                                    <asp:TextBox ID="Rcpt5" runat="server" Width="169px" BorderColor="White" BorderStyle="Solid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 80px; height: 26px;">
                                                                                    <asp:TextBox ID="ppaid5" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="amt5" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="vertical-align: top;">
                                                                                <td align="center" style="vertical-align: middle;">
                                                                                    <asp:Label ID="sno6" runat="server" Text="6" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 238px">
                                                                                    <asp:TextBox ID="Detail6" runat="server" Width="279px" BorderColor="White" BorderStyle="Solid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 239px">
                                                                                    <asp:TextBox ID="Rcpt6" runat="server" Width="169px" BorderColor="White" BorderStyle="Solid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 80px">
                                                                                    <asp:TextBox ID="ppaid6" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="amt6" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="vertical-align: top;">
                                                                                <td align="center" style="vertical-align: middle;">
                                                                                    <asp:Label ID="sno7" runat="server" Text="7" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 238px">
                                                                                    <asp:TextBox ID="Detail7" runat="server" Width="279px" BorderColor="White" BorderStyle="Solid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 239px">
                                                                                    <asp:TextBox ID="Rcpt7" runat="server" Width="169px" BorderColor="White" BorderStyle="Solid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 80px">
                                                                                    <asp:TextBox ID="ppaid7" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="amt7" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="vertical-align: top;">
                                                                                <td align="center" style="vertical-align: middle;">
                                                                                    <asp:Label ID="sno8" runat="server" Text="8" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 238px">
                                                                                    <asp:TextBox ID="Detail8" runat="server" Width="279px" BorderColor="White" BorderStyle="Solid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 239px">
                                                                                    <asp:TextBox ID="Rcpt8" runat="server" Width="169px" BorderColor="White" BorderStyle="Solid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 80px">
                                                                                    <asp:TextBox ID="ppaid8" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="amt8" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="vertical-align: top;">
                                                                                <td align="center" style="vertical-align: middle;">
                                                                                    <asp:Label ID="sno9" runat="server" Text="9" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 238px; height: 15px;">
                                                                                    <asp:TextBox ID="Detail9" runat="server" Width="279px" BorderColor="White" BorderStyle="Solid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="height: 15px; width: 239px;">
                                                                                    <asp:TextBox ID="Rcpt9" runat="server" Width="169px" BorderColor="White" BorderStyle="Solid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 80px; height: 15px;">
                                                                                    <asp:TextBox ID="ppaid9" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="amt9" runat="server" Width="80px" BorderColor="White" BorderStyle="Solid"
                                                                                        Style="text-align: right">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 679px;">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 739px">
                                                                    <asp:LinkButton ID="LKRupees" runat="server" Font-Names="Verdana" Font-Overline="False"
                                                                        Font-Size="9pt">Rupees(In Word)</asp:LinkButton>
                                                                </td>
                                                                <td align="right" style="width: 187px">
                                                                    <asp:Label ID="Label28" runat="server" Text="Sub Total" Font-Bold="True" Font-Names="Arial"
                                                                        Font-Size="10pt" Width="62px"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 8px">
                                                                    <asp:TextBox ID="subPaidTotal" runat="server" Width="86px" Style="text-align: right">0</asp:TextBox>
                                                                </td>
                                                                <td style="width: 93px">
                                                                    <asp:TextBox ID="SubTotal" runat="server" Width="80px" Style="text-align: right">0</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td rowspan="3" style="width: 739px; vertical-align: top;">
                                                                    <asp:TextBox ID="txtRupees" runat="server" Width="353px" Font-Names="Verdana" Font-Size="8pt"
                                                                        Height="39px" TextMode="MultiLine"></asp:TextBox><br />
                                                                    <br />
                                                                    <asp:Label ID="lblnote" runat="server"></asp:Label>
                                                                </td>
                                                                <td align="right" colspan="2">
                                                                    &nbsp;<asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                        Font-Size="8pt" Text="Service Tax "></asp:Label>
                                                                    <asp:TextBox ID="ServiceTax" runat="server" Width="34px"></asp:TextBox>
                                                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                                        Text="%"></asp:Label>
                                                                </td>
                                                                <td style="width: 93px">
                                                                    <asp:TextBox ID="sTax" runat="server" Style="text-align: right" Width="80px">0</asp:TextBox>&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="2">
                                                                    <asp:Label ID="Label30" runat="server" Text="Educational Cess @2.00%" Font-Names="Arial"
                                                                        Font-Size="8pt" Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td style="width: 93px">
                                                                    <asp:TextBox ID="EdCess" runat="server" Width="80px" Style="text-align: right">0</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="2">
                                                                    <asp:Label ID="Label31" runat="server" Text="Secondary & Higher Ed. Cess @ 1.00%"
                                                                        Font-Names="Arial" Font-Size="8pt" Font-Bold="True" Width="210px"></asp:Label>
                                                                </td>
                                                                <td style="width: 93px">
                                                                    <asp:TextBox ID="SHCess" runat="server" Width="80px" Style="text-align: right">0</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 739px">
                                                                    <asp:Label ID="lblintrmks" runat="server" Width="300px" Font-Names="Arial" Font-Size="8pt"
                                                                        Text="Label"></asp:Label>
                                                                </td>
                                                                <td align="right" colspan="2">
                                                                    <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                                        Text="Total"></asp:Label>
                                                                </td>
                                                                <td style="width: 93px">
                                                                    <asp:TextBox ID="Totals" runat="server" Width="80px" Style="text-align: right">0</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 739px; height: 20px;">
                                                                    <asp:Label ID="lblResult" runat="server"></asp:Label>
                                                                </td>
                                                                <td align="right" colspan="2" style="height: 20px">
                                                                    <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                                        Text="Less Advance Recd."></asp:Label>
                                                                </td>
                                                                <td style="width: 93px; height: 20px;">
                                                                    <asp:TextBox ID="LessAd" runat="server" Width="80px" Style="text-align: right">0</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="border-color: #2461BF; border-style: solid; border-width: 1px; width: 679px;
                                                        border-bottom-color: #2461BF;">
                                                        <tbody>
                                                            <tr>
                                                                <td align="right" style="width: 588px; vertical-align: top;">
                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                                                    <asp:Label ID="Label34" runat="server" Text="Balance Due" Font-Bold="True" Font-Names="Arial"
                                                                        Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td style="vertical-align: top; width: 88px;">
                                                                    <asp:TextBox ID="BalanceDue" runat="server" Width="81px" Style="text-align: right">0</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="width: 679px;">
                                                        <tbody>
                                                            <tr>
                                                                <td style="height: 67px">
                                                                    <asp:Label ID="Label35" runat="server" Text="E.& O.E." Font-Names="Arial" Font-Size="7pt"></asp:Label><br />
                                                                    <asp:Label ID="Label36" runat="server" Text="1.In case of any dispute on this bill, the same mustbereported to 1.us within 07 days from the receipt of bill "
                                                                        Font-Names="Arial" Font-Size="7pt" Width="500px"></asp:Label><br />
                                                                    <asp:Label ID="Label37" runat="server" Font-Names="Arial" Font-Size="7pt"></asp:Label>
                                                                    <asp:Label ID="lblBranchName" runat="server" Text="" Font-Names="Arial" Font-Size="7pt"></asp:Label><br />
                                                                    <asp:Label ID="Label38" runat="server" Text="2.Any notice of demand under Section 28(1) of CA 1962 for the short levy of duty shall be payable by you"
                                                                        Font-Names="Arial" Font-Size="7pt"></asp:Label><br />
                                                                    <asp:Label ID="Label39" runat="server" Font-Names="Arial" Font-Size="7pt" Text="3.All delayed payments will attract interest @18 p.a."></asp:Label>
                                                                </td>
                                                                <td align="right" style="vertical-align: top; height: 67px;">
                                                                    <asp:Label ID="Label40" runat="server" Text="For" Font-Bold="True" Font-Names="Arial"
                                                                        Font-Size="9pt"></asp:Label>
                                                                    <asp:Label ID="lblCompName1" runat="server" Text="" Font-Bold="True" Font-Names="Arial"
                                                                        Font-Size="9pt"></asp:Label>
                                                                    &nbsp;<br />
                                                                    <br />
                                                                    <br />
                                                                    <asp:Label ID="Label41" runat="server" Text="Authorised Signatory" Font-Names="Arial"
                                                                        Font-Size="8pt"></asp:Label>
                                                                    &nbsp; &nbsp;
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <asp:TextBox ID="balance1" runat="server" BackColor="White" BorderStyle="Solid" ForeColor="White"
                                                        Width="67px">0</asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </center>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
