using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System;

public static partial class smokesignals_signaller {

    private static void WriteMessage(Page page, PlaceHolder plhMessages, MessageType messageType, string message, bool append) {
        if (plhMessages != null) {
            Instantiate(page);

            string uniqueId = Guid.NewGuid().ToString();
            // build the close button
            HtmlGenericControl closeButton = new HtmlGenericControl("div");
            closeButton.Attributes.Add("class", "close");
            closeButton.InnerText = "x";
            closeButton.Attributes.Add("onclick", "javascript:document.getElementById('" + uniqueId + "').style.display='none';");
            //closeButton.HRef = "javascript:document.getElementById('" + uniqueId + "').style.display='none';";
            
            // build the div to host the message
            HtmlGenericControl messageElement = new HtmlGenericControl("div");
            messageElement.Attributes.Add("id", uniqueId);
            messageElement.Attributes.Add("class", GetCcss(messageType));
            messageElement.InnerHtml = string.Format("<p>{0}</p>", message);
            messageElement.Controls.Add(closeButton); // add last or the innerhtml call will replace it.

            if (!append) plhMessages.Controls.Clear(); // clear controls if append is false

            plhMessages.Controls.AddAt(plhMessages.Controls.Count, messageElement);
        }
    }

    private static PlaceHolder GetPlaceholder(Page page, PlaceHolder messageHolder) {
        if (messageHolder != null)
            return messageHolder;

        return FindContentPlaceholder(page.Form.Controls);
    }

    private static PlaceHolder GetPlaceholder(MasterPage masterPage, PlaceHolder messageHolder) {
        if (messageHolder != null)
            return messageHolder;

        foreach (Control control in masterPage.Controls) {
            if (control is HtmlForm) {
                foreach (Control formControl in control.Controls) {
                    if (formControl is PlaceHolder) {
                        return formControl as PlaceHolder;
                    }
                }
            }
        }

        return null;
    }

    private static PlaceHolder GetPlaceholder(Control control, PlaceHolder messageHolder) {
        if (messageHolder != null)
            return messageHolder;

        foreach (Control childControl in control.Controls) {
            if (childControl is PlaceHolder) {
                return childControl as PlaceHolder;
            }
        }
        
        return null;
    }
   
    private static PlaceHolder FindContentPlaceholder(ControlCollection controls) {
        foreach (Control control in controls) {
            if (control is ContentPlaceHolder) {
                return control.FindControl("plhMessages") as PlaceHolder;
            }
        }
        return null;
    }

    /// <summary>
    /// Determines what css class to use based on the MessageType enum
    /// </summary>
    private static string GetCcss(MessageType messageType) {
        switch (messageType) {
            case MessageType.Error:
                return "Message_Error";
            case MessageType.Warning:
                return "Message_Warning";
            case MessageType.Success:
                return "Message_Success";
            case MessageType.Info:
                return "Message_Info";
            default:
                return "Message_Info";
        }
    }
    
    /// <summary>
    /// Wriites any embedded resources to the page (css in this case)
    /// </summary>
    private static void Instantiate(Page page) {
        // get the embedded css so we can embed it on the page
        if (!page.ClientScript.IsClientScriptBlockRegistered("messagesCss")) {
            string css_out = string.Format("<link href='{0}' type='text/css' rel='stylesheet' />", page.ClientScript.GetWebResourceUrl(typeof(smokesignals_signaller), "smokesignals.messages.css"));
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "messagesCss", css_out, false);
        }
    }
}

/// <summary>
/// enum class to keep track of and identify message types.
/// </summary>
public enum MessageType { Error,Warning,Success,Info }