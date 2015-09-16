using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Net.Mime;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;

/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail
{
    public static string SendMailException
    {
        get;
        set;
    }
    public static void CreateMessageWithAttachment(string filePath,string EmailTo,string EmailFrom,string Subject,string Body)
    {
        // Specify the file to be attached and sent.
        // This example assumes that a file named Data.xls exists in the
        // current working directory.

        // Create a message and set up the recipients.
        //MailMessage message = new MailMessage(EmailFrom,EmailTo,Subject,Body);
        MailMessage message = new MailMessage();
        message.From = new MailAddress(EmailFrom);
        message.Subject = Subject;
        message.Body = Body;
        string mTo =EmailTo;
        if (mTo != "" || mTo != string.Empty)
        {
            string[] strTo = mTo.Split(';');
            foreach (string strThisTo in strTo)
            {
                strThisTo.Trim();
                message.To.Add(strThisTo);
            }
        }

        // Create  the file attachment for this e-mail message.
        Attachment data = new Attachment(filePath, MediaTypeNames.Application.Octet);
        // Add time stamp information for the file.
        ContentDisposition disposition = data.ContentDisposition;
        disposition.CreationDate = System.IO.File.GetCreationTime(filePath);
        disposition.ModificationDate = System.IO.File.GetLastWriteTime(filePath);
        disposition.ReadDate = System.IO.File.GetLastAccessTime(filePath);
        // Add the file attachment to this e-mail message.
        message.Attachments.Add(data);

        //Send the message.
        SmtpClient client = new SmtpClient("smtp.bizmail.yahoo.com", 25);
        // Add credentials if the SMTP server requires them.
        client.Credentials = new System.Net.NetworkCredential("apps@vts.in", "VtsApps");
        try
        {
            client.Send(message);
        }
        catch (Exception ex)
        {
            SendMailException = ex.StackTrace; 
        }
        // Display the values in the ContentDisposition for the attachment.


        data.Dispose();
    }
}
