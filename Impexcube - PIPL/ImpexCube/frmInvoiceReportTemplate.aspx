<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmInvoiceReportTemplate.aspx.cs" Inherits="ImpexCube.frmInvoiceReportTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.hide
{
    display:none;
    }
</style>

<script type="text/javascript">
    function checkAll(objRef) {

        var GridView = objRef.parentNode.parentNode.parentNode;

        var inputList = GridView.getElementsByTagName("input");

        for (var i = 0; i < inputList.length; i++) {

            //Get the Cell To find out ColumnIndex

            var row = inputList[i].parentNode.parentNode;

            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                if (objRef.checked) {

                    inputList[i].checked = true;

                }

                else {

                 
                    inputList[i].checked = false;

                }

            }

        }

    }
    function checkAll1(objRef) {

        var GridView = objRef.parentNode.parentNode.parentNode;

        var inputList = GridView.getElementsByTagName("input");

        for (var i = 0; i < inputList.length; i++) {

            //Get the Cell To find out ColumnIndex

            var row = inputList[i].parentNode.parentNode;

            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                if (objRef.checked) {

                    inputList[i].checked = true;

                }

                else {


                    inputList[i].checked = false;

                }

            }

        }

    }
    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: left">
        <table style="width: 31%" align="center">
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="Invoice Report Template"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Template Name"></asp:Label>               
                    <asp:TextBox ID="txtTemplateName" runat="server"></asp:TextBox>                
                </td>
            </tr>                        
            <tr>
                <td>
                <div class="grid_scroll">
                    <asp:GridView ID="gvFieldName" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CellPadding="4" Font-Names="Arial" Font-Size="8pt" border="4" GridLines="Horizontal" ShowHeader="true" 
                        Width="378px" Height="224px">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" ForeColor="#333333"   />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#0099FF" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkheader" runat="server" onclick="javascript:return checkAll(this)" />
                            </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkField" runat="server" AutoPostBack="false" Font-Names="arial"
                                        Font-Size="7px" OnCheckedChanged="chkField_CheckedChanged" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:BoundField DataField="ReportName" HeaderText="Select The Field Name" >
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                              <asp:BoundField DataField="TableField" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                          
                        </Columns>
                    </asp:GridView>
                    </div>
                </td>
                <td>
                <div class="grid_scroll">
                    <asp:GridView ID="gvChargelist" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CellPadding="4" Font-Names="Arial" Font-Size="8pt" border="4" GridLines="Horizontal" 
                        ShowHeader="true"
                        Width="456px" Height="212px">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#0099FF" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkheader1" runat="server" onclick="javascript:return checkAll1(this)" />
                            </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkcharge" runat="server" AutoPostBack="false" Font-Names="arial"
                                        Font-Size="7px" OnCheckedChanged="chkcharge_CheckedChanged" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:BoundField DataField="Charge_desc" HeaderText="Select The Charges Description">
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
                </td>
                <td>
                    <asp:Button ID="tnCancel" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
