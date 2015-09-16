<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true"
    CodeBehind="frmUserTemplates.aspx.cs" Inherits="ImpexCube.frmUserTemplates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="container">
        <div class="container-area">
            <div id="col-2ex">
                <div class="d">
                    <div class="c-aEx1">
                        <div class="Col-titleext">
                            Report Templates</div>
                    </div>
                    <div class="c-aEx1">
                        <div class="c-accordion1">
                            <div class="content-work-increase">
                              <%--  <div class="c-s-b1">
                                    Party name</div>--%>
                                <div class="c-s-b2fixeda">
                                    <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="spanddl required" 
                                        Visible="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="content-work-increase">
                                <div class="c-s-b1">
                                    Template Name</div>
                                <div class="c-s-b2fixeda">
                                    <asp:TextBox ID="txtTemplate" runat="server" CssClass="postmsgg23 required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="c-aEx1">
                        <div class="content-work-box1acc">
                            <div class="c-s-b21">
                                Job Field</div>
                        </div>
                        <div class="content-work-box1acc">
                        </div>
                        <div class="content-work-box1acc">
                            <div class="c-s-b21">
                                Custom Field
                            </div>
                        </div>
                    </div>
                    <div class="c-aEx1">
                        <div class="c-accordiontemp1">
                            <div class="content-work-box1acc">
                                <div class="c-s-b2we">
                                    <asp:ListBox ID="lbJobField" runat="server" CssClass="listbox1" SelectionMode="Multiple">
                                    </asp:ListBox>
                                </div>
                            </div>
                            <div class="content-work-box1acc">
                                <div class="c-s-b21">
                                </div>
                                <div class="c-s-b2we">
                                    <p>
                                        <asp:Button ID="btnMove" runat="server" Text=">" CssClass="blue" 
                                            onclick="btnMove_Click">
                                        </asp:Button></p>
                                    <p>
                                        <asp:Button ID="btnMoveAll" runat="server" Text=">>" CssClass="blue" 
                                            onclick="btnMoveAll_Click">
                                        </asp:Button></p>
                                    <p>
                                        <asp:Button ID="btnRemove" runat="server" Text="<" CssClass="blue" 
                                            onclick="btnRemove_Click">
                                        </asp:Button></p>
                                    <p>
                                        <asp:Button ID="btnRemoveAll" runat="server" Text="<<" CssClass="blue" 
                                            onclick="btnRemoveAll_Click">
                                        </asp:Button></p>
                                </div>
                            </div>
                            <div class="content-work1-box1acc">
                                <div class="c-s-b2we">
                                    <asp:ListBox ID="lbCustomField" runat="server" CssClass="listbox1" SelectionMode="Multiple"
                                        AppendDataBoundItems="true"></asp:ListBox>
                                </div>
                                <div class="c-s-b1r">
                                    <p>
                                        <asp:Button ID="btnMoveUp" runat="server" Text="Up" CssClass="blue" 
                                            onclick="btnMoveUp_Click">
                                        </asp:Button></p>
                                    <p>
                                        <asp:Button ID="btnMoveDown" runat="server" Text="Down" CssClass="blue" 
                                            onclick="btnMoveDown_Click">
                                        </asp:Button></p>                                        
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="menu-listacc">
                        <div class="cs2-accbutton">
                            <div style="float: right;">
                                <asp:Button ID="btnSaveTemplate" runat="server" CssClass="orange" OnClick="btnSaveTemplate_Click">
                                </asp:Button>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="orange" OnClick="btnCancel_Click">
                                </asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="d">
                    <div class="grid_scroll-b">
                        <asp:GridView ID="gvTemplateDetails" runat="server" CellPadding="4" GridLines="None"
                            CssClass="table-wrapper" AutoGenerateColumns="False" Font-Size="10pt" Width="700px"
                            OnSelectedIndexChanged="gvTemplateDetails_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <HeaderStyle CssClass="hiddencol"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" CssClass="hiddencol"></ItemStyle>
                                </asp:BoundField>
                              
                                <asp:BoundField DataField="Template" HeaderText="Template Name" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Fields" HeaderText="Custom Fields" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <RowStyle CssClass="table-header light" />
                            <HeaderStyle CssClass="table-row" BackColor="#E7E7FF" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <AlternatingRowStyle BackColor="#E7E7FF" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
