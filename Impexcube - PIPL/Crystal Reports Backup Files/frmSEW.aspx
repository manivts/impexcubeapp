<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmSEW.aspx.cs" Inherits="ImpexCube.frmSEW" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="JobReports" runat="server">
        <ContentTemplate>
            <center>
                <asp:Label ID="Label35" runat="server" Text="SEW - JOB REPORTS" Font-Size="12pt"
                    Font-Names="Verdana" Font-Bold="True"></asp:Label>
            </center>
            <asp:Panel ID="PLDET" runat="server" Width="100%">
                <table style="width: 99%; background-color: white; text-align: center">
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 76px">
                                                        <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                                            Text="PARTY NAME" Width="71px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td colspan="2" style="text-align: left">
                                                        <asp:DropDownList ID="drParty" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                            Font-Size="8pt" Height="20px" OnSelectedIndexChanged="drParty_SelectedIndexChanged"
                                                            Width="228px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 76px">
                                                        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                                            Text="JOB NUMBER" Width="73px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text=":"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="drJOBNO" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Height="20px" Width="146px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Button ID="BtnFind" runat="server" Font-Names="Arial" Font-Size="8pt" Height="30px"
                                                            OnClick="BtnFind_Click" Text="Search" Width="80px"></asp:Button>
                                                        <asp:Button ID="BtnCancel" runat="server" Font-Names="Arial" Font-Size="8pt" Height="30px"
                                                            OnClick="BtnCancel_Click" Text="Cancel" Width="80px"></asp:Button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="width: 100%; height: 355px" class="grid_scroll">
                                            <asp:DataGrid ID="DGDetail1" runat="server" ForeColor="#333333" Font-Size="7pt" Font-Names="Arial"
                                                Width="500px" GridLines="None" CellPadding="1" AutoGenerateColumns="False" OnItemDataBound="DGDetail1_ItemDataBound">
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
                                                            <asp:TextBox ID="txtStage" runat="server" Font-Size="7pt" Height="20px" Width="100px"
                                                                ReadOnly="true" Text='<%# Bind("job_no") %>'></asp:TextBox>
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
                                                            <asp:TextBox ID="txtPO" Font-Size="7pt" Text='<%# Bind("ITC_LOCN") %>' runat="server"
                                                                Height="20px" Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PO ITEM NO.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPOitem" runat="server" Text='<%# Bind("POLICYPARA") %>' Font-Size="7pt"
                                                                Height="20px" Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PART NO.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPart" Font-Size="7pt" runat="server" Height="20px" Width="50px"
                                                                Text='<%# Bind("ITCHS_CODE") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PART DESCRIPTION">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRmks" runat="server" ReadOnly="true" Font-Size="7pt" Height="20px"
                                                                Width="280px" Text='<%# Bind("prod_desc") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="QTY">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" Font-Size="7pt" Height="20px"
                                                                Width="55px" Text='<%# Bind("qty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="PCODE">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtpcode" runat="server" Visible="false" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("prod_code") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="SNO">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtsno" runat="server" Visible="false" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("prod_sn") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="CVD">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcvd" runat="server" Visible="false" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="Additional Duty">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="aDUTY" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("addlDutyAmt") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="DUTY">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="duty" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("totalDUTYamt") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="inv_id" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("inv_id") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="NVD">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="BASEVAL" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="ASSVAL">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ASSVAL" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="BASEDUTY">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="BASEDuty" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("BAS_DUTY") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="CUSDUTY">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="CUSDuty" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("CVD_DUTY") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="MisCUR">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="MisCUR" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="MisPER">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="MisPER" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="MisVAL">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="MisVal" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="FRTCUR">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="frt_cur" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="FRTPER">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="frt_per" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="FRTVAL">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="frt_val" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="INSCUR">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ins_cur" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="INSPER">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ins_per" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="INSVAL">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ins_val" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="PROD_VAL">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="prod_VAL" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("prod_value") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="PROD_VAL">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="prod_amt" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("prodAmt") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="EDUCESS">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="edu_cess" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("EDU_CESS_RATE") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="EDUCESSCustom">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="edu_cess_CUS" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("EDU_CESS_RATE_exc") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="AddDutyPEr">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="AddDutyPer" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("AddlDuty_RATE") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="insON">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="insOn" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("toi") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false" HeaderText="CESS_DUTY">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="cess_duty" Visible="false" runat="server" ReadOnly="true" Font-Size="7pt"
                                                                Height="20px" Width="55px" Text='<%# Bind("cess_duty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <center>
                                                <asp:Label ID="lblMessage" Font-Names="Calibri" runat="server" Font-Size="13pt" ForeColor="#0066FF"></asp:Label>
                                            </center>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" runat="server" Text="Submit"
                                            Font-Size="8pt" Font-Names="Arial" Height="30px" Width="80px"></asp:Button>
                                        &nbsp;
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
