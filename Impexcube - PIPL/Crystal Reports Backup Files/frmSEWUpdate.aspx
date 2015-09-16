<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmSEWUpdate.aspx.cs" Inherits="ImpexCube.frmSEWUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="JobReports" runat="server">
        <ContentTemplate>
            <center>
                <asp:Label ID="Label35" runat="server" Text="SEW - JOB REPORTS UPDATE" Font-Size="12pt"
                    Font-Names="Verdana" Font-Bold="True"></asp:Label>
            </center>
            <asp:Panel ID="PLDET" runat="server" Width="100%">
                <table style="width: 99%; background-color: white">
                    <tr>
                        <td colspan="2">
                            <table style="width:100%">
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 76px">
                                                    <asp:Label ID="Label3" runat="server" Text="PARTY NAME" Font-Size="8pt" Font-Names="Arial"
                                                        Width="71px" Font-Overline="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text=":"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="drParty" runat="server" Font-Size="8pt" Font-Names="Arial"
                                                        Height="20px" Width="228px" OnSelectedIndexChanged="drParty_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 76px">
                                                    <asp:Label ID="Label1" runat="server" Text="JOB NUMBER" Font-Size="8pt" Font-Names="Arial"
                                                        Width="73px" Font-Overline="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text=":"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="drJOBNO" runat="server" Font-Size="8pt" Font-Names="Arial"
                                                        Height="20px" Width="146px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnFind" OnClick="BtnFind_Click" runat="server" Text="Search" Font-Size="8pt"
                                                        Font-Names="Arial" Height="30px" Width="80px"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="width: 100%; height: 355px" class="grid_scroll">
                                            <asp:DataGrid ID="DataGrid1" runat="server" ForeColor="#333333" Font-Size="7pt" Font-Names="Arial"
                                                Width="500px" GridLines="None" CellPadding="1" AutoGenerateColumns="False">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditItemStyle BackColor="#2461BF" Font-Names="Arial" Font-Size="7pt" />
                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Font-Names="Arial"
                                                    Font-Size="8pt" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Names="Arial"
                                                    Font-Size="8pt" />
                                                <AlternatingItemStyle BackColor="#D1DDF1" Font-Names="Arial" Font-Size="7pt" />
                                                <ItemStyle BackColor="#D1DDF1" Font-Names="Arial" Font-Size="7pt" />
                                                <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Arial"
                                                    Font-Size="7pt" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="Job No">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtStage" runat="server" Font-Size="7pt" Height="20px" ReadOnly="true"
                                                                Text='<%# Bind("job_no") %>' Width="100px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="INV NO">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtINV" runat="server" Font-Size="7pt" Height="20px" Width="50px"
                                                                ReadOnly="true" Text='<%# Bind("inv_no") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PO NO.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPO" runat="server" Font-Size="7pt" Height="20px" Width="50px"
                                                                Text='<%# Bind("pur_ordno") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PO ITEM NO.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPOitem" runat="server" Font-Size="7pt" Height="20px" Text='<%# Bind("PO_ItemNO") %>'
                                                                Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PART NO.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPart" runat="server" Font-Size="7pt" Height="20px" Text='<%# Bind("model") %>'
                                                                Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PART DESCRIPTION">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRmks" runat="server" Font-Size="7pt" Height="20px" ReadOnly="true"
                                                                Text='<%# Bind("prod_desc") %>' Width="280px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="QTY">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDate" runat="server" Font-Size="7pt" Height="20px" ReadOnly="true"
                                                                Text='<%# Bind("qty") %>' Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="PCODE">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtpcode" runat="server" Font-Size="7pt" Height="20px" ReadOnly="true"
                                                                Text='<%# Bind("prod_code") %>' Visible="false" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="SNO">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtsno" runat="server" Font-Size="7pt" Height="20px" ReadOnly="true"
                                                                Text='<%# Bind("prod_sn") %>' Visible="false" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="CVD">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcvd" runat="server" Font-Size="7pt" Height="20px" ReadOnly="true"
                                                                Text='<%# Bind("TotalCVDAmt") %>' Visible="false" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="Additional Duty">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="aDUTY" runat="server" Font-Size="7pt" Height="20px" ReadOnly="true"
                                                                Text='<%# Bind("addlDutyAmt") %>' Visible="false" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="DUTY">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="duty" runat="server" Font-Size="7pt" Height="20px" ReadOnly="true"
                                                                Text='<%# Bind("totalDUTYamt") %>' Visible="false" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="inv_id" runat="server" Font-Size="7pt" Height="20px" ReadOnly="true"
                                                                Text='<%# Bind("inv_id") %>' Visible="false" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="NVD">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="nvd" runat="server" Font-Size="7pt" Height="20px" ReadOnly="true"
                                                                Text='<%# Bind("NVD") %>' Visible="false" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" runat="server" Text="Submit"
                                            Font-Size="8pt" Font-Names="Arial" Height="30px" Width="80px"></asp:Button>
                                        <asp:Button ID="BtnCancel" OnClick="BtnCancel_Click" runat="server" Text="Cancel"
                                            Font-Size="8pt" Font-Names="Arial" Height="30px" Width="80px"></asp:Button>&nbsp;
                                    </td>
                                </tr>
                            </table>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnFind" />
            <asp:PostBackTrigger ControlID="BtnSubmit" />
            <asp:PostBackTrigger ControlID="BtnCancel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
