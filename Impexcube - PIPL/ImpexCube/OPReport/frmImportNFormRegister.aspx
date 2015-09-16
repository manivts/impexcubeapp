<%@ Page Title="" Language="C#" MasterPageFile="~/OPReport/OperationReport.Master" AutoEventWireup="true" CodeBehind="frmImportNFormRegister.aspx.cs" Inherits="ImpexCube.OPReport.frmImportNFormRegister" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlImportNForm" runat="server">
    <table style="width: 689px">
    <tr>
    <td>
        </td>
    <td>
        </td>
    <td>
        </td>
    <td>
    
        </td>
    </tr>
        <tr>
            <td class="fontsize">
                JobNo</td>
            <td>
                <asp:DropDownList ID="ddlJobNo" runat="server" CssClass="ddl150">
                    <asp:ListItem>~Select~</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnGenerate" runat="server" CssClass="stylebutton" 
                    onclick="btnGenerate_Click" Text="Generate" />
            </td>
            <td>
                </td>
        </tr>
        <tr>
            <td class="fontsize">
                N-Form No/Date</td>
            <td>
                <asp:TextBox ID="txtNFormNo" runat="server" CssClass="textbox150" Width="80px"></asp:TextBox>
                <asp:TextBox ID="txtNFormDate" runat="server" CssClass="textbox150" 
                    Width="80px"></asp:TextBox>
            </td>
            <td class="fontsize" width="80px">
                No Of Pkgs</td>
            <td class="fontsize">
                <asp:TextBox ID="txtNoOfPkgs" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                Imported By</td>
            <td>
                <asp:TextBox ID="txtImportedBy" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td class="fontsize">
                Landed At</td>
            <td>
                <asp:TextBox ID="txtLandedAt" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="fontsize">
                From</td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td class="fontsize">
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td class="fontsize">
                Place Of Export</td>
            <td>
                <asp:TextBox ID="txtPaceOfExport" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td class="fontsize">
                Per</td>
            <td>
                <asp:TextBox ID="txtPer" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="fontsize">
                Consignee</td>
            <td>
                <asp:TextBox ID="txtConsignee" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td class="fontsize">
                Destination Port</td>
            <td>
                <asp:TextBox ID="txtDestination" runat="server" CssClass="textbox150"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="fontsize">
                Transporter</td>
            <td>
                <asp:DropDownList ID="ddlTransporter" runat="server" CssClass="ddl150">
                    <asp:ListItem>~Select~</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="fontsize">
                Octroi Rate</td>
            <td class="fontsize">
                <asp:TextBox ID="txtOctroiRate" runat="server" CssClass="textbox150"></asp:TextBox>
                %</td>
        </tr>
    <tr>
    <td class="fontsize">
        Supervisor</td>
    <td class="fontsize">
    
        <asp:TextBox ID="txtSupervisor" runat="server" CssClass="textbox150"></asp:TextBox>
    
        </td>
    <td class="fontsize">
        &nbsp;</td>
    <td class="fontsize">
    
        &nbsp;</td>
    </tr>
        <tr>
            <td class="fontsize">
                Importer/Agent</td>
            <td class="fontsize">
                <asp:TextBox ID="txtImporter" runat="server" CssClass="textbox150"></asp:TextBox>
            </td>
            <td class="fontsize">
                Signatory</td>
            <td class="fontsize">
                <asp:TextBox ID="txtSignature" runat="server" CssClass="textbox150">Authorised Signatory</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fontsize">
                &nbsp;</td>
            <td class="fontsize">
                &nbsp;</td>
            <td class="fontsize">
                &nbsp;</td>
            <td class="fontsize">
                &nbsp;</td>
        </tr>
    <tr>
    <td colspan="4" align="right">
   
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="stylebutton" />
       
  
            <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" 
                CssClass="stylebutton" />
        </td>
    </tr>
    </table>
    <div>
    <table>
    
    <tr>
    <td>
        <asp:GridView ID="gvImporterJob" runat="server" AutoGenerateColumns="False" CssClass="table-wrapper">
        <Columns>
       <asp:BoundField DataField="JobNo" HeaderText="Job No"  />
       <asp:BoundField DataField="N-FormNo" HeaderText="N-Form No"  />
       <asp:BoundField DataField="N-FormDate" HeaderText="N-Form Date" />
       <asp:BoundField DataField="NoofPkgs" HeaderText="No of Pkgs"  />
       <asp:BoundField DataField="Passed" HeaderText="Passed"  />
       <asp:BoundField DataField="Vessel/Freight(From)" HeaderText="Vessel/Freight(From)" />
       <asp:BoundField DataField="Vessel/Freight(To)" HeaderText="Vessel/Freight(To)"  />
       <asp:BoundField DataField="PlaceofExport" HeaderText="Place of Export"  />
       <asp:BoundField DataField="Per" HeaderText="Per"  />
       <asp:BoundField DataField="Transporter" HeaderText="Transporter"  />
        <asp:BoundField DataField="DestinationPort" HeaderText="Destination Port"  />
         <asp:BoundField DataField="ImporterName" HeaderText="Name of Importer"  />
          <asp:BoundField DataField="Supervisor" HeaderText="Supervisor"  />
        </Columns>
        </asp:GridView>
    </td>
    </tr>
    </table>
    </div>
    </asp:Panel>
</asp:Content>
