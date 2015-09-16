<%@ Page Title="" Language="C#" MasterPageFile="~/ImpexCube.Master" AutoEventWireup="true" CodeBehind="frmImportBond.aspx.cs" Inherits="ImpexCube.frmImportBond" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 601px;
        }
        .style2
        {
            width: 352px;
            text-decoration: underline;
        }
        .style3
        {
            width: 272px;
        }
        .style5
        {
            width: 202px;
        }
        .style7
        {
            width: 355px;
        }
        .style8
        {
            width: 596px;
        }
        .style9
        {
            width: 601px;
            text-decoration: underline;
        }
        .style13
        {
            width: 301px;
        }
        .style14
        {
        }
        .style15
        {
            font-weight: bold;
        }
        .style16
        {
            width: 350px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table>
    <tr>
    <td>
     <asp:Label ID="Label2" runat="server" Text="Job No" CssClass="fontsize"></asp:Label>
    </td>
    <td >
                    <asp:TextBox ID="txtjobNo" runat="server" CssClass="textbox150"></asp:TextBox>                    
                </td> 
                <td>
     <asp:Label ID="Label1" runat="server" Text="Bond" CssClass="fontsize"></asp:Label>
    </td>
                <td >
                     <asp:DropDownList ID="ddlbond" runat="server" CssClass="ddl156">
                         <asp:ListItem>Default</asp:ListItem>
                         <asp:ListItem>P.D.Bond</asp:ListItem>
                         <asp:ListItem>Double Duty Bond</asp:ListItem>
                         <asp:ListItem Value="High Sea Sale Contract">High Sea Sale Contract</asp:ListItem>
                         <asp:ListItem>Agreement Format</asp:ListItem>
                         <asp:ListItem>ReExport Bond</asp:ListItem>
                         <asp:ListItem>RD Bond</asp:ListItem>
                    </asp:DropDownList>
                     
                </td> 
                
                <td>
                 <asp:Button ID="btnGenerate" runat="server" CssClass="stylebutton" 
                        Text="Generate" onclick="btnGenerate_Click" />
                </td>
    </tr>
    <table width="750px">
    <tr><td>
     <asp:Panel ID="pnlDBBond" runat="server">
     <table width="650px">
     <tr>
     <td class="style1" colspan="5">&nbsp;</td>
     </tr>
         <tr>
             <td class="style1" colspan="5">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1" colspan="5">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1" colspan="5">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1" colspan="5">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1" colspan="5">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1" colspan="5">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1" colspan="5">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1" colspan="5">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1" colspan="5">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1" colspan="5">
                 KNOW ALL BY THESE PRESENTS THAT We M/S. SIGMA TEST AND
             </td>
         </tr>
     <tr>
     <td class="style1" colspan="5">RESEARCH CENTRE BA-15, MANGOLPURI INDL. AREA PHASE 2 NEW </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">DELHI Delhi - 110034, INDIAhereinafter referred to as the “Importer” which expression </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">shall unless or excluded by or repugnant to the context include their successors hereby </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">firmly bind ourselves unto to the President of India (hereinafter referred to as the </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">Government) to pay on demand and without demur Rs.420200.70/- the difference </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">between the duty finally assessed under Sub-section 2 of Section 18 of the Customs Act, </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">1962 read with the customs Provisional duty assessment Regulation 1963, by the </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">Commissioner of Customs, JNCH/Mumbai, hereinafter called the "Commissioner" (which </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">expression shall include the person for the time being performing the duties of the </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">Commissioner of Customs, JNCH/Mumbai) and the duty provisionally assessed by the </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">Commissioner in respect of the said goods under Sub-section 1 of Section 18 of the </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">Customs Act, 1962. </td>
     </tr>
     <tr>
     <td class="style1" colspan="5"></td>
     </tr>
     <tr>
     <td class="style1" colspan="5">SEALED with our Seal with this 25-Mar-2014</td>
     </tr>
     <tr>
     <td class="style1" colspan="5"></td>
     </tr>
     <tr>
     <td class="style1" colspan="5">The Dy/Asstt Commissioner of Customs (hereinafter called the proper officer) has agreed </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">to make provisional assessment of the goods described in schedule below imported by the </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">importer pending submission of further documents and furnishing information and/or </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">completion of further inquiries by S.V.B/S.I.I.B and/or chemical or other test and proper </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">officer has agreed to allow clearance of the goods subject to the Importer’s proving to the </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">satisfaction of the proper officer that Open General Licence/Import Licence is valid for </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">and cover the entire goods mentioned in the schedule below and upon Importer agreeing </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">to furnish such bond as is herein contained.</td>
     </tr>
     <tr>
     <td class="style1" colspan="5"></td>
     </tr>
     <tr>
     <td class="style1" colspan="5">NOW THE CONDITIONS OF THE ABOVE BOND ARE SUCH THAT:</td>
     </tr>
     <tr>
     <td class="style1" colspan="5"></td>
     </tr>
     <tr>
     <td class="style1" colspan="5">1.If the importer pays to the President the difference between the duty finally assessed </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">and the duty provisionally assessed in respect of the good mentioned in the schedule </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">and,</td>
     </tr>
     <tr>
     <td class="style1" colspan="5">2.If the importers pays to the President any penalty and any fine that may be adjusted </td></tr>
<tr>
     <td class="style1" colspan="5">in lieu of confiscation of the said goods for importation of the goods or part thereof </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">without a valid O.G.L./Import Licence.</td>
     </tr>
     <tr>
     <td class="style1" colspan="5">THEN THE ABOVE WRITTEN BOND SHALL BE VOID AND OF NO EFFECT </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">OTHERWISE THE SAME SHALL REMAIN IN FULL FORCE AND VIRTUE</td>
     </tr>
     <tr>
     <td class="style1" colspan="5"></td></tr>
<tr>
     <td class="style1" colspan="5">1.This bond is given under the orders of the Central Government for performance of </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">an act in which the public are interested.</td>
     </tr>
     <tr>
     <td class="style1" colspan="5">2.The President through the Dy./Asstt. Commissioner of Customs, JNCH/Mumbai or </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">other officer may recover the amount due in the manner laid down in Sub-section </td>
     </tr>
     <tr>
     <td class="style1" colspan="5">142 of the Customs Act, 1962, without prejudice to any mode of recovery.</td>
     </tr></table>
     <tr>
     <td class="style1" colspan="1"></td>
     </tr>
     <table>
     <tr>
     <td align="center" class="style2" colspan="2">SCHEDULE OF GOODS</td></tr>
<tr>
     <td class="style3">1.Bill of Entry No. & Date</td><td class="style5">
         <asp:Label ID="lblpdbondbillofentey" runat="server" Text=""></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">2.Name of vessel.</td><td class="style5">
         <asp:Label ID="lblpdbonnameofvessel" runat="server" Text=""></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">3.Description of goods</td><td class="style5">
         <asp:Label ID="lblpdbondescofgoods" runat="server" Text=""></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">4.Country of origin</td><td class="style5">
         <asp:Label ID="lblpdboncountryiforigin" runat="server" Text=""></asp:Label></td></tr>
<tr>
     <td class="style3">5.Quantity</td><td class="style5">
         <asp:Label ID="lblpdbonquantity" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">6.Assessable Value</td><td class="style5">
         <asp:Label ID="lblassesval" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">7.Rate of Duty & amount</td><td class="style5">
         <asp:Label ID="lblpdbonrateofdutyamnt" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">8.Amount of Bond</td><td class="style5">
         <asp:Label ID="lblpdbonamountofbond" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td align="center" colspan="2"> Witness</td>
     </tr>
     <tr>
     <td class="style3" colspan="2">1.</td>
     </tr>
     <tr>
     <td class="style3" colspan="2">2.</td>
     </tr>
     <tr>
     <td class="style3" colspan="2"></td></tr>
<tr>
     <td align="right"  colspan="2">
         (Signature of Importer)
</td>
     </tr>
     </table>
    
     
     
    </asp:Panel></td></tr>
    <tr><td>
     <asp:Panel ID="pnlDoubleDutyBond" runat="server">
     <table width="650px">
     <tr>
     <td class="style1">&nbsp;</td>
     </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                BOND UNDER SECTION 59(1) OF THE CUSTOMS ACT,1962
             </td>
         </tr>
     <tr>
     <td class="style1">We, SIGMA TEST AND RESEARCH CENTRE are jointly and severally bound to </td>
     </tr>
     <tr>
     <td class="style1">the President of India in the sum of Rs.__________/- </td>
     </tr>
     <tr>
     <td class="style1">severally bind ourselves and our legal representatives by these presents. </td>
     </tr>
     <tr>
     <td class="style1">The above bounden SIGMA TEST AND RESEARCH CENTRE having applied to  </td>
     </tr>
     <tr>
     <td class="style1">the Dy./Assistant Commissioner of Customs, for and obtained and orders permitting the  </td>
     </tr>
     <tr>
     <td class="style1">deposit in Customs Bonded Warehouse situated at ________________ for a period of  </td>
     </tr>
     <tr>
     <td class="style1">one year of the said permission, the goods described in the Schedule below, that is to say:-</td>
     </tr>
     <tr>
     <td class="style1">As per Public Notice No. 36/95.</td>
     </tr>
     <tr>
     <td class="style1"> </td>
     </tr>
     <tr>
     <td class="style1">The condition of this bond is that- </td>
     </tr>
     <tr>
     <td class="style1">If the above bounden SIGMA TEST AND RESEARCH CENTRE or their legal </td>
     </tr>
     <tr>
     <td class="style1">representative, </td>
     </tr>
     <tr>
     <td class="style1"> a).Shall observe all the provisions of the Customs Act (53 of 1962) (hereinafter called the </td>
     </tr>
     <tr>
     <td class="style1">said Act) and the Rules and Regulation in respect of the above mentioned goods:</td>
     </tr>
     <tr>
     <td class="style1">Sealed with our Seal this:</td>
     </tr>
     <tr>
     <td class="style1"> b) Shall pay Custom House port of _________ on or before the date specified in the  </td>
     </tr>
     <tr>
     <td class="style1">notice of demand all duties, rent and charge claimable to account of the above  </td>
     </tr>
     <tr>
     <td class="style1">mentioned goods under the said Act together with interest of the same for the date so  </td>
     </tr>
     <tr>
     <td class="style1">specified at the rate of fifteen percent per annum or such other rate as the time being </td>
     </tr>
     <tr>
     <td class="style1">fixed by the Central Board of Excise and Customs constituted under the Central Board </td>
     </tr>
     <tr>
     <td class="style1">of Revenue Act 1963 (hereinafter called the Board Act): and </td>
     </tr>
     <tr>
     <td class="style1"> c) To discharge all penalties incurred for violation of the (provisions of the said Act and </td>
     </tr>
     <tr>
     <td class="style1">Rules and Regulations in respect of the such goods:</td>
     </tr>
     <tr>
     <td class="style1"></td>
     </tr>
     <tr>
     <td class="style1">And if, within a period so fixed or extended, the said Goods, or any portion thereof </td>
     </tr>
     <tr>
     <td class="style1">having been removed from the said warehouse for Home consumption, or re-</td>
     </tr>
     <tr>
     <td class="style1">exportation by Sea, the full amount of all duties, rent and other charges-claimable as  </td>
     </tr>
     <tr>
     <td class="style1">aforesaid and the said interest, and penalties shall been first paid and discharges on the  </td>
     </tr>
     <tr>
     <td class="style1">whole of the said goods then this obligation shall be void and of no effect.</td>
     </tr>
     <tr>
     <td class="style1">But, otherwise, and no breach or failure in the performance of any part of condition, the </td></tr>
<tr>
     <td class="style1">same shall be and remain in full force; effect and virtue. It is hereby agreed that amount that  </td>
     </tr>
     <tr>
     <td class="style1">is due from me/us under his Bond may be recovered in the manner laid down in sub </td>
     </tr>
     <tr>
     <td class="style1">section (10 of section 142 of the _____________ Custom Act (52 of 1962) </td>
     </tr>
     <tr>
     <td class="style1"></td>
     </tr>
     <tr>
     <td class="style1"></td></tr>
     
<tr>
     <td class="style1" align="center" colspan="2">SCHEDULE OF ABOVE REFERRED TO</td>
     </tr>
     <tr>
     <td class="style1">1. B/E NO. & DATE
</td><td class="style5">
         <asp:Label ID="lblDDBBEnodate" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style1">2. IGM/ITEM NO. & DATE </td><td class="style5">
         <asp:Label ID="lblDDBigmnodate" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style1">3. B/L.NO.</td><td class="style5">
         <asp:Label ID="lblDDBblno" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style1">4. NAME OF VESSEL</td><td class="style5">
         <asp:Label ID="lblDDBnameofvess" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style1" colspan="1">5. MARKS & NOS.</td><td class="style5">
         <asp:Label ID="lblDDBmarksnos" runat="server" Text="Label"></asp:Label></td>
     </tr>
     
     <tr>
     <td >6. PACKAGES</td><td class="style5">
         <asp:Label ID="lblDDBpacks" runat="server" Text="Label"></asp:Label></td></tr>
<tr>
     <td class="style3">7. DESCRIPTION OF GOODS</td><td class="style5">
         <asp:Label ID="lblDDBdescofgoods" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">8. QUANTITY WEIGHT OR MEASURE</td><td class="style5">
         <asp:Label ID="lblDDBquanweigtmeaser" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">9. CIF. VALUE</td><td class="style5">
         <asp:Label ID="lblDDBcif" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">10. EXCHANGE RATE</td><td class="style5">
         <asp:Label ID="lblDDBexchrate" runat="server" Text="Label"></asp:Label></td></tr>
<tr>
     <td class="style3">11. VALUE FOR DUTY (ASS. VALUE)</td><td class="style5">
         <asp:Label ID="lblDDBvalforduty" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">12. RATE OF CUSTOMS DUTY</td><td class="style5">
         <asp:Label ID="lblDDBrateodcustoms" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">13. DUTY VALUE</td><td class="style5">
         <asp:Label ID="lblDDBdutyval" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">14. BOND AMOUNT</td><td class="style5">
         <asp:Label ID="lblDDBbondamnt" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td> 15. PORT OF SHIPMENT</td><td class="style5">
         <asp:Label ID="lblDDBportofship" runat="server" Text="Label"></asp:Label></td>
     </tr></table>
     <table style="width: 644px">
     <tr>
     <td class="style3" colspan="2">PLACE:</td><td>_______________________</td>
     </tr>
     <tr>
     <td class="style3" colspan="2">DATE: .</td><td>Importer Stamp & Signature</td>
     </tr>
     <tr>
     <td class="style3" colspan="2" align="left"> WITNESS:</td></tr>
<tr>
     <td align="center"  colspan="2">
         IDENTIFIED BY ME
</td>
     </tr>
     <tr>
     <td>1)_____________________</td>
     </tr>
     <tr>
     <td>2)_____________________</td>
     </tr>
     </table>
    </asp:Panel></td></tr>
    <tr><td>
     <asp:Panel ID="pnlHighSeaSaleContract" runat="server">
     <table width="650px">
     <tr>
     <td class="style8" colspan="4">&nbsp;</td>
     </tr>
         <tr>
             <td class="style8" colspan="4">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style8" colspan="4">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style8" colspan="4">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style8" colspan="4">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style8" colspan="4">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style8" colspan="4">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style8" colspan="4">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style8" colspan="4">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style8" colspan="4">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style8" >
                 CONTRACT NO.
             </td>
             <td class="style8">
                 <asp:Label ID="lblHSSCcontractno" runat="server" Text="Label"></asp:Label></td>
             <td class="style8">
                Date :</td>
             <td class="style8">
                <asp:Label ID="lblHSSCdate" runat="server" Text="Label"></asp:Label></td>
             <td> </td>
         </tr>
     <tr>
     <td  colspan="5" align="center">HIGH SEAS SALE AGREEMENT</td>
     </tr>
     <tr>
     <td align="center" class="style8" colspan="4" >(Transfer of Ownership)</td>
     </tr>
     <tr>
     <td class="style8" colspan="4">This agreement made Between M/s.(hereinafter referred to as the Seller") which  </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">expression in so far as the context admits shall include their successors, nominees and </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">assigns) and M/s.SIGMA TEST AND RESEARCH CENTRE (hereinafter called to as  </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">the "Buyer" which expression in so far as the context admits shall includes their  </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">successors,nominees and assigns) for High sea sale of KGS on the following terms and </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">conditions: </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">Custom Duty & other charges </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">In view of disposal of the goods on High Sea Sale basis, the Buyer shall arrange clearance  </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">of the goods from customs at their sole risk and responsibility.customs duty shall be paid </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">by the buyer and any changes in the rates of customs duty and other Government Levies of  </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">whatsoever nature, during the pendency of this contract will be on buyer's account</td>
     </tr>
     <tr>
     <td class="style8" colspan="4">Payment of cost of Documents</td>
     </tr>
     <tr>
     <td class="style8" colspan="4">Full payment of cost of document comprising of CIF value,L/C opening/Amendment </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">charges, Bank interest,commission and other charges for retirement of documents,to be  </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">made by actual users/buyers to us</td>
     </tr>
     <tr>
     <td class="style8" colspan="4">Delivery & customs clearance</td>
     </tr>
     <tr>
     <td class="style8" colspan="4">All rights and title of the above goods will be transferred by us to the Actual Users/buyers </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">shall arrange clearance of the goods from customs at his role risk and responsibility.The  </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">entire clearing expenses viz.customs duties,clearing charges,demurrages,octroi etc. will be  </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">born by the Buyer and paid directly to the customs and/or clearing & forwarding Agents. </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">Sales Tax:</td>
     </tr>
     <tr>
     <td class="style8" colspan="4">No Sales tax will charges as above goods are being sold to the actual user/buyer on high </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">sea sale basis,but if at a later dates sales tax authorities assess sales tax on this sale ,the </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">same shall be paid by the Buyer to us on demand.</td>
     </tr>
     <tr>
     <td class="style8" colspan="4">Insurance claims:</td>
     </tr>
     <tr>
     <td class="style8" colspan="4">As regards any loss/shortage and/or any other claim to the consignment,we shall nominate  </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">and subrogate our rights to recover the amount from insurances copmany/steamer agents </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">and/or customs authorities to enable you to deal directly with the concerned.</td></tr>
<tr>
     <td class="style8" colspan="4"></td>
     </tr>
     <tr>
     <td class="style8" colspan="4">In witness thereof the seller and the actual user buyer here to have set their respective </td>
     </tr>
     <tr>
     <td class="style8" colspan="4">hands on the date mentioned above. </td>
     </tr>
         <tr>
             <td class="style8" colspan="4">
                 &nbsp;</td>
         </tr>
     <table>
     <tr>
     <td class="style7" >Name & address of seller :</td><td class="style5">
         <asp:Label ID="lblHSSCbnameadressseller" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style7" >Name & address of Actual Buyer :</td><td class="style5">
         <asp:Label ID="lblHSSCnameaddrbuyer" runat="server" Text="Label"></asp:Label></td></tr>
<tr>
     <td class="style7" >Description of goods :</td><td class="style5">
         <asp:Label ID="lblHSSCdescofgoods" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style7" >Qty. :</td><td class="style5">
         <asp:Label ID="lblHSSCqty" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style7" >Name of Foreign/supplier country : </td><td class="style5">
         <asp:Label ID="lblHSSCnameofforeignsupply" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style7" >Invoice No. & Date : </td><td class="style5">
         <asp:Label ID="lblHSSCinvnodate" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style7" >Name of vessel :</td><td class="style5">
         <asp:Label ID="lblHSSCnameodvess" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style7" colspan="1">B/L No. & Date :</td><td class="style5">
         <asp:Label ID="lblHSSCblnodate" runat="server" Text="Label"></asp:Label></td>
     </tr>
     
<tr>
     <td class="style7">Import licence No. & Date :</td><td class="style5">
         <asp:Label ID="lblHSSCimplicesnodate" runat="server" Text="Label"></asp:Label></td>
     </tr>
     
     <tr>
     <td class="style7">Consideration :</td><td class="style5">
         <asp:Label ID="lblHSSCconsider" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style7" >&nbsp;</td>        
     </tr>
         <tr>
             <td class="style7">
                 For SIGMA TEST AND RESEARCH CENTRE</td>
         </tr>
     <tr>
     <td class="style7">&nbsp;</td><td class="style5">
         &nbsp;</td></tr>
         <tr>
             <td class="style7">
                 Authorised signatory</td>
             <td class="style5">
                 Authorised signatory
             </td>
         </tr>
<tr>
     <td  colspan="2">&nbsp;</td>
     </tr>
         <tr>
             <td colspan="2">
                 Please return to us the duplicate copy of this contract duly sealed and signed.</td>
         </tr>
     <tr>
     <td class="style7"></td>
        
     </tr>
     <tr>
     <td class="style7" >We confirm the above and accept all terms and condition.</td>
     </tr>
     </table>
     </table>
    </asp:Panel></td></tr>
    <tr>
    <td>
   <asp:Panel ID="pnlagreementformat" runat="server">
     <table width="650px">
     <tr>
     <td class="style1">&nbsp;</td>
     </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style9" align="center">
                 <strong>BOND UNDER SECTION 59 (1) OF THE CUSTOM ACT, 1962. </strong>
             </td>
         </tr>
     <tr>
     <td class="style1">&nbsp;</td>
     </tr>
     <tr>
     <td class="style1">We SIGMA TEST AND RESEARCH CENTRE BA-15, MANGOLPURI INDL. </td>
     </tr>
     <tr>
     <td class="style1">AREA PHASE 2 NEW DELHI Delhi - 110034, INDIA are jointly &amp; 
         severally bound to  </td>
     </tr>
     <tr>
     <td class="style1">the President of India in the sum of Rs. 420200.70 well and truly 
         to be paid to the  </td>
     </tr>
     <tr>
     <td class="style1">President of India, for which payment we jointly &amp; severally bind 
         ourselves and our legal </td>
     </tr>
     <tr>
     <td class="style1"> representatives by these presents. </td>
     </tr>
     <tr>
     <td class="style1"> </td>
     </tr>
     <tr>
     <td class="style1"> Sealed with our Seal this day of 25-Mar-2014 </td>
     </tr>
     <tr>
     <td class="style1"> </td>
     </tr>
     <tr>
     <td class="style1">The above bounden SIGMA TEST AND RESEARCH CENTRE BA-15,  </td>
     </tr>
     <tr>
     <td class="style1">MANGOLPURI INDL. AREA PHASE 2 NEW DELHI Delhi - 110034, INDIA 
         having  </td>
     </tr>
     <tr>
     <td class="style1">applied to the Dy. Commissioner of Customs, for and obtained an 
         orders permitting the </td>
     </tr>
     <tr>
     <td class="style1">deposit in Customs Bonded Warehouse, situated at for a period of 
         one year from the date </td>
     </tr>
     <tr>
     <td class="style1">of the said permission, the goods described in the schedule 
         below, that is to say :- As per </td>
     </tr>
     <tr>
     <td class="style1">Public Notice No. 36/95.</td>
     </tr>
     <tr>
     <td class="style1"></td>
     </tr>
     <tr>
     <td class="style1">The condition of this Bond is that :-</td>
     </tr>
     <tr>
     <td class="style1"> If the above – bounden : M/s. ABG Crane Pvt. Ltd. or their legal 
         representative, </td>
     </tr>
     <tr>
     <td class="style1"> </td>
     </tr>
     <tr>
     <td class="style1">a)a)Shall observe all the provisions of the Customs Act (52 of 
         1962) (hereinafter called </td>
     </tr>
     <tr>
     <td class="style1">the ‘said Act) and the rules and regulations in respect of the 
         above mentioned goods;</td>
     </tr>
     <tr>
     <td class="style1"> </td>
     </tr>
     <tr>
     <td class="style1">b)a)Shall pay to the officer-in-charge of the Customs House at 
         the port of Nhava Sheva </td>
     </tr>
     <tr>
     <td class="style1">on or before the date satisfied in the notice of demand all 
         duties, rent &amp; charges claimable </td>
     </tr>
     <tr>
     <td class="style1">to account of the above-mentioned goods under the said Act 
         together with interest of the </td>
     </tr>
     <tr>
     <td class="style1">same from the date of specified at the rate of 15% P. annum or 
         such other rate as is for the </td>
     </tr>
     <tr>
     <td class="style1">time being fixed by the Central Board of Excise &amp; Customs 
         constituted under the Central </td>
     </tr>
     <tr>
     <td class="style1"> Boards of Revenue Act, 1963 (hereinafter called the Board); and </td>
     </tr>
     <tr>
     <td class="style1"> </td>
     </tr>
     <tr>
     <td class="style1">c)To discharge all penalties incurred for violation of the 
         provisions of this Act and the rules  </td></tr>
<tr>
     <td class="style1">&amp; regulations in respect of such goods; And if, within the period 
         so fixed or extended, the  </td>
     </tr>
     <tr>
     <td class="style1">said goods, or any portion thereof having been removed from the 
         said Warehouse for </td>
     </tr>
     <tr>
     <td class="style1">Home consumption, or re-exportation by sea, the full amount of 
         all duties, rent and other </td>
     </tr>
     <tr>
     <td class="style1">charges claimable as aforesaid and the said interest, and 
         penalties shall have been first paid </td>
     </tr>
     <tr>
     <td class="style1">&amp; discharged on the whole of the said goods then this obligation 
         shall be void and of no </td></tr>
<tr>
     <td class="style1">effect, but otherwise, and no breach or failure in the 
         performance of any part of this </td>
     </tr>
     <tr>
     <td class="style1">condition, the same shall be and remain in full force; effect &amp; 
         virtue </td>
     </tr>
     <tr>
     <td class="style1"> </td>
     </tr>
     <tr>
     <td class="style1">It is hereby agreed that any amount that is due from us under 
         this Bond may be recovered  </td>
     </tr>
     <tr>
     <td class="style1">in the manner laid down in sub-section (1) of Section 142 of the 
         Customs Act. (52 of </td>
     </tr>
         <tr>
             <td class="style1">
                 1962).</td>
         </tr>
       </table>
     <tr>
     <td class="style1" colspan="1"></td>
     </tr>
     <table>
     <tr>
     <td align="center" class="style2" colspan="2"><strong>SCHEDULE ABOVE REFERRED TO</strong></td></tr>
<tr>
     <td class="style3">B/E IGM/ITEM NO. & DATE</td><td class="style5">
         <asp:Label ID="lblAFbeigmitemnodate" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">Name of Vessel</td><td class="style5">
         <asp:Label ID="lblAFnameofvess" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">Marks & No.</td><td class="style5">
         <asp:Label ID="lblAFmarksnos" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">packages</td><td class="style5">
         <asp:Label ID="lblAFpack" runat="server" Text="Label"></asp:Label></td></tr>
<tr>
     <td class="style3">Description of goods</td><td class="style5">
         <asp:Label ID="lblAFdescofgoods" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">Qty.</td><td class="style5">
         <asp:Label ID="lblAFqty" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">Value</td><td class="style5">
         <asp:Label ID="lblAFvalue" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style3">Exchange Rate</td><td class="style5">
         <asp:Label ID="lblAFexchrate" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td > Value for duty</td><td class="style5">
         <asp:Label ID="lblAFvalforduty" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td >Rate of custom duty</td><td class="style5">
         <asp:Label ID="lblAFrateofcustomduty" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td >Amount of custom duty</td><td class="style5">
         <asp:Label ID="lblAFamntofcusduty" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td >Signed, sealed and delivered by the within named in the presence of</td></tr>
<tr>
     <td align="right"  colspan="2">
         &nbsp;</td>
     </tr>
         <tr>
             <td align="right" colspan="2">
                 Importer
             </td>
         </tr>
     <tr>
     <td align="right"  colspan="2">
         &nbsp;</td>
     </tr>
         <tr>
             <td align="right" colspan="2">
                 Authorized Signatory
             </td>
         </tr>
     </table>
    
     
     
    </asp:Panel></td>
    </tr>
    <tr><td>
    <asp:Panel ID="pnlReexportBond" runat="server">
     <table width="650px">
     <tr>
     <td class="style1">&nbsp;</td>
     </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style1">
                 <strong>ReExport Bond</strong>
             </td>
         </tr>
     <tr>
     <td class="style1">&nbsp;</td>
     </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
         </tr>
     <tr>
     <td class="style1">To,</td>
     </tr>
     <tr>
     <td class="style1">The President of India</td>
     </tr>
     <tr>
     <td class="style1">Through Commissioner of Customs</td>
     </tr>
     <tr>
     <td class="style1">Mundra </td>
     </tr>
     <tr>
     <td class="style1"> </td>
     </tr>
     <tr>
     <td class="style1">KNOW ALL MEN BY THESE present that, we,SIGMA TEST AND RESEARCH 
         CENTRE , BA-15, MANGOLPURI INDL. AREA PHASE 2 NEW DELHI Delhi -  </td>
     </tr>
     <tr>
     <td class="style1">110034, INDIA,hereinafter called the importer (which expression 
         shall where the context to admits include their respective heirs, </td>
     </tr>
     <tr>
     <td class="style1">executors,administrators, and legal representatives / successors 
         and permitted assigns are held and firmly bound jointly and  </td>
     </tr>
     <tr>
     <td class="style1">severally unto the President of India to pay the President of 
         India through Commissioner of Customs,Mundra, for the time being on  </td>
     </tr>
     <tr>
     <td class="style1">demand and without demur the sum of Rs.420200.70 
         __________________________________________________ for which payment well and </td>
     </tr>
     <tr>
     <td class="style1">truly to be made we bind ourselves firmly by these presents. </td>
     </tr>
     <tr>
     <td class="style1"> </td>
     </tr>
     <tr>
     <td class="style1">Whereas,the Commissioner of Customs,Mundra, hereinafter referred 
         to as the said Commissioner of Customs, which expression shall </td>
     </tr>
     <tr>
     <td class="style1">include the person for the time being performing the duties of 
         Commissioner of Customs, Mundra, has permitted clearance of Re-</td>
     </tr>
     <tr>
     <td class="style1">Usable,_______________________________________________ 
         Construction fully described in the Schedule hereunder written imported in </td>
     </tr>
     <tr>
     <td class="style1">India by the Importer under Re-export purpose. </td>
     </tr>
     <tr>
     <td class="style1"></td>
     </tr>
     <tr>
     <td class="style1">Now the conditions of the above written bond are such that:</td>
     </tr>
     <tr>
     <td class="style1">1. If the said Importer shall re-export the above outside India 
         within a period of Six months from the date of importation of the  </td>
     </tr>
     <tr>
     <td class="style1">said goods and whereas the Commissioner of Customs, has allowed 
         clearance of the said goods without payment of customs duty amount </td>
     </tr>
     <tr>
     <td class="style1">wide Custom Notification No. 104/94, we also undertake to pay 
         duty amount in the event of failure to re-export within six month or </td>
     </tr>
     <tr>
     <td class="style1">written such extended period the Commissioner of Customs may 
         allow. </td>
     </tr>
     <tr>
     <td class="style1"> </td>
     </tr>
     <tr>
     <td class="style1">2. If the said Importer shall produce or cause to be produced 
         evidence before the Commissioner of Customs within one month from the </td>
     </tr>
     <tr>
     <td class="style1">date of expiry of the aforesaid period of six months to show that 
         the said goods have been exported outside India within a period of </td>
     </tr>
     <tr>
     <td class="style1">six months from the date of import, then the above written bond 
         shall be void and of no effect, otherwise the same shall remain in </td>
     </tr>
     <tr>
     <td class="style1"> full force and virtue and it is hereby agreed and declared 
         between the parties as follows:</td>
     </tr>
     <tr>
     <td class="style1"> </td>
     </tr>
     <tr>
     <td class="style1">a. That the above written bond shall remain in full force till 
         the _____________ from the date of importation, and that if any claim </td></tr>
<tr>
     <td class="style1">accrues or arise by virtue of this bond, the same will be 
         enforceable till ______________.</td>
     </tr>
     <tr>
     <td class="style1">b. Any forbearance, act or omission on the part of President of 
         India to enforce the bond against the Importer (whether with or </td>
     </tr>
     <tr>
     <td class="style1">without the knowledge or consent of surety) shall not relieve the 
         importer from the liability under the bond.</td>
     </tr>
     <tr>
     <td class="style1">c. That the bond is entered in to under the orders of the Central 
         Government for the performance of an Act in which the public are </td>
     </tr>
     <tr>
     <td class="style1">interested.</td></tr>
<tr>
     <td class="style1">d. Any breach of the terms and conditions of this bond will 
         render the Importer liable to penalties as are provided under Section </td>
     </tr>
     <tr>
     <td class="style1">142(C)of the Customs Act, 1962, over and above their liability 
         for payment of the amount of the bond. </td>
     </tr>
         <tr>
             <td class="style1">
             </td>
         </tr>
     <table>
     <tr>
     <td class="style1" colspan="2" align="center">THE SCHEDULE ABOVE REFERRED TO</td>
     </tr>
     <tr>
     <td class="style13" >1.Importer Name & Address </td><td class="style5">
         <asp:Label ID="lblREBimportnameaddr" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style13" >2. Description of goods</td><td class="style5">
         <asp:Label ID="lblREBdescofgoods" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style13" colspan="1">3. Vessel’s Name</td><td class="style5">
         <asp:Label ID="lblREBvessname" runat="server" Text="Label"></asp:Label></td>
     </tr>     
<tr>
     <td class="style13">4. IGM / Item No</td><td class="style5">
         <asp:Label ID="lblREBigmitemno" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style13">5. B/E No  </td><td class="style5">
         <asp:Label ID="lblREBbeno" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style13">6. Country of Origin </td><td class="style5">
         <asp:Label ID="lblREBcountryorign" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style13">7. B/L No. &Date </td><td class="style5">
         <asp:Label ID="lblREBblnodate" runat="server" Text="Label"></asp:Label></td></tr>
<tr>
     <td class="style13">8. CIF Value</td><td class="style5">
         <asp:Label ID="lblREBcifval" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style13">9. Assessable Value  </td><td class="style5">
         <asp:Label ID="lblREBasscessval" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style13">10.Normal Rate of Duty
(10%+12%+2%+1%+4%)</td><td class="style5">
         <asp:Label ID="lblREBnormalrateofduty" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style13">11.Concessional Rate of Duty</td><td class="style5">
         <asp:Label ID="lblREBconcessrateofduty" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style13"  > 12.Bond Value </td><td class="style5">
         <asp:Label ID="lblREBbondval" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td class="style13" >13.Address of Factory </td><td class="style5">
         <asp:Label ID="lblREBadroffact" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td colspan="2">In witness whereas the parties hear to have duly executed this undertaking on</td>
     </tr>
     <tr>
     <td  colspan="2">SIGNED AND DELIVERED BY</td></tr>
<tr>
     <td   colspan="2">
         FOR AND ON BEHALF OF THE IMPORTERS:
</td></tr>  
         <tr>
             <td colspan="2">
                 &nbsp;</td>
         </tr>
<tr>
     <td class="style3" colspan="2">Witness:</td></tr>
         <tr>
             <td class="style3" colspan="2">
                 &nbsp;</td>
         </tr>
     <tr>
     <td class="style3" colspan="2">Name</td></tr>
     <tr>
     <td class="style3" colspan="2">Address</td></tr>
     <tr>
     <td class="style3" colspan="2">Signature</td></tr>
     <tr>
     <td class="style3" colspan="2">Name</td></tr>
     <tr>
     <td class="style3" colspan="2">Address</td></tr>
     <tr>
     <td class="style3" colspan="2">Signature</td></tr>
     </table>
     </table>
    
    </asp:Panel></td></tr>
    <tr><td>
     <asp:Panel ID="pnlRDBond" runat="server">
     <table style="width: 603px">
     <tr>
     <td colspan="4"><strong>RD Bond</strong></td>
     </tr>
     <tr>
     <td colspan="4"></td>
     </tr>
     <tr>
     <td colspan="4"><strong>OFFICE OF THE COMMISIONER OF CUSTOMS</strong></td>
     </tr>
     <tr>
     <td align="center" colspan="4"><asp:Label ID="lblRRBCommcustomsaddress" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td align="center">Taluka:-</td><td><asp:Label ID="lblRBBtaluk" runat="server" Text="Label"></asp:Label></td><td>Dist:-</td><td><asp:Label ID="lblRBBdist" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>
     <td colspan="4"></td>
     </tr>
     <tr>
     <td align="right" colspan="4">DATE :<asp:Label ID="lblRBBdate" runat="server" Text="Label"></asp:Label>
         </td>
     </tr>
     <tr>
     <td colspan="4"></td>
     </tr>
     <tr>
     <td colspan="4" align="center"> Challan for the payment of Government, Dues etc. in The Custom House</td>
     </tr>
     </table>
     <table style="width: 601px">
     <tr>
     <td class="style14" colspan="2"></td>
     </tr>
     <tr>
     <td class="style16">1.FULL NAME OF THE PARTY MAKING PAYMENT</td> <td ><asp:Label ID="lblRBBfullnameofparty" runat="server" Text="Label"></asp:Label>
         </td>
     </tr>
     <tr>
     <td class="style16">2.B/E THOKA NO & DATE.</td> <td ><asp:Label ID="lblRBBbethokano" runat="server" Text="Label"></asp:Label>
         </td>
     </tr>
     <tr>
     <td class="style16">3.IGM /ITEM NO & DATE</td> <td ><asp:Label ID="lblRBBigmitemno" runat="server" Text="Label"></asp:Label>
         </td>
     </tr>
     <tr>
     <td class="style16">4.VESSEL NAME</td> <td ><asp:Label ID="lblRBBvessname" runat="server" Text="Label"></asp:Label>
         </td>
     </tr>
     <tr>
     <td class="style16">5.NAME OF THE C.H.A.</td> <td ><asp:Label ID="lblRBBnameofcha" runat="server" Text="Label"></asp:Label>
         </td>
     </tr>
     <tr>
     <td class="style16">6.FILE NO.</td> <td ><asp:Label ID="lblRBBfileno" runat="server" Text="Label"></asp:Label>
         </td>
     </tr>
     <tr>
     <td class="style16">7.ASSESSABLE VALUE</td> <td ><asp:Label ID="lblRBBasscessval" runat="server" Text="Label"></asp:Label>
         </td>
     </tr>
     <tr>
     <td class="style16">8.AMOUNT OF R.D @ 1% HEAD OF THE ACCOUNT UNDER</td> <td ><asp:Label ID="lblRBBamntofrd" runat="server" Text="Label"></asp:Label>
         </td>
     </tr>
     <tr>
     <td class="style16">9.WHICH THE AMOUNT SHOULD BE CREDITIED</td> <td ><asp:Label ID="lblRBBamountcredited" runat="server" Text="Label"></asp:Label>
         </td>
     </tr>
     <tr>
     <td class="style16">10.DEPARTMENT OF CUSTOM HOUSE</td> <td ><asp:Label ID="lblRBBdeptofcusthouse" runat="server" Text="Label"></asp:Label>
         </td>
     </tr>
     <tr>
     <td align="center" class="style14" colspan="2">&nbsp;</td>
     </tr>
         <tr>
             <td align="center" class="style14" colspan="2">
                 <strong>For&nbsp;&nbsp; </strong>
                 <asp:Label ID="lblRBBbottomCustomname" runat="server" Text="Label" 
                     style="font-weight: 700"></asp:Label>
             </td>
         </tr>
     <tr>
     <td align="center" class="style16"  ><asp:Label ID="lblRBBbottomCustomaddress" 
             runat="server" Text="Label" CssClass="style15"></asp:Label>
         </td>
     </tr>
     <tr>
     <td align="center" class="style16" ><asp:Label ID="Label5" runat="server" 
             Text="Label" CssClass="style15"></asp:Label>
         </td>
     </tr>
     
     </table>
     </asp:Panel></td></tr>
    </table>
   </table>
</asp:Content>
