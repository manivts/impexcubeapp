using System;
namespace CSharp
{

	/// <summary>
	/// A simple class that allows you to add an email to MS Outlook Outbox
	/// </summary>
public class OutlookMail
{

   private Microsoft.Office.Interop.Outlook.Application oApp;
   private Microsoft.Office.Interop.Outlook._NameSpace oNameSpace;
   private Microsoft.Office.Interop.Outlook.MAPIFolder oSentItems;
      
   /// <summary>
   /// Constructor
   /// </summary>
   public OutlookMail()
   {
      //Return a reference to the MAPI layer
      oApp = new Microsoft.Office.Interop.Outlook.Application();
      oNameSpace= oApp.GetNamespace("MAPI");

      /***********************************************************************
          * Logs on the user
          * Profile: Set to null if using the currently logged on user, or set 
          *    to an empty string ("") if you wish to use the default Outlook Profile. 
          * Password: Set to null if  using the currently logged on user, or set 
          *    to an empty string ("") if you wish to use the default Outlook Profile
          *    password. 
          * ShowDialog: Set to True to display the Outlook Profile dialog box. 
          * NewSession: Set to True to start a new session. Set to False to 
          *    use the current session. 
          ***********************************************************************/
      oNameSpace.Logon(null,null,true,true);

      //gets defaultfolder for my Outlook Outbox
      oSentItems = oNameSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderSentMail);
   }



   /// <summary>
   /// Adds an email to MS Outlook Outbox
   /// </summary>
   /// <param name="toValue">Email address of recipient</param>
   /// <param name="subjectValue">Email subject</param>
   /// <param name="bodyValue">Email body</param>
   public void addToOutBox(string toValue, string subjectValue, string bodyValue, string cc, string attachment)
   {
      //creates a new MailItem object
      Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);      
         
      oMailItem.To = toValue;
      oMailItem.Subject = subjectValue;
      oMailItem.Body = bodyValue;
      oMailItem.CC = cc;
      if (attachment != "")
      {
          String sDisplayName = "Job Status Details";
          int iPosition = (int)oMailItem.Body.Length + 1;
          int iAttachType = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;

          Microsoft.Office.Interop.Outlook.Attachment attachtype = oMailItem.Attachments.Add(attachment, iAttachType, iPosition, sDisplayName); 
      }      

      oMailItem.SaveSentMessageFolder = oSentItems;
         
      //uncomment this to also save this in your draft
      //oMailItem.Save();

      //adds it to the outbox
      oMailItem.Send();

   }



}
}
