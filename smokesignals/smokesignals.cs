using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Mvc;

public static partial class smokesignals_signaller {
    /// <summary>
    /// Builds the close button and the message container elements and adds/appends (based on append) it to the placeholder.
    /// </summary>
    private static void WriteMessage(Page page, PlaceHolder plhMessages, MessageType messageType, string message, bool append) {
        if (plhMessages != null) {
            Instantiate(page);

            HtmlGenericControl messageElement = BuildMessageControl(messageType, message);

            if (!append) plhMessages.Controls.Clear(); // clear controls if append is false

            plhMessages.Controls.AddAt(plhMessages.Controls.Count, messageElement);
        }
    }

    /// <summary>
    /// Builds a div with the class being derived from messageType (via GetCss(), adds the close button and message text
    /// </summary>
    private static HtmlGenericControl BuildMessageControl(MessageType messageType, string message) {
        string uniqueId = Guid.NewGuid().ToString(); // make each of these unique

        HtmlImage closeImage = new HtmlImage(); // build the close image
        closeImage.Src = "";
        closeImage.Border = 0;
        closeImage.Attributes.Add("title", "close");

        HtmlGenericControl closeButton = new HtmlGenericControl("div"); // build the close button
        closeButton.Attributes.Add("class", "close");
        closeButton.Attributes.Add("onclick", "javascript:document.getElementById('" + uniqueId + "').style.display='none';");
        closeButton.InnerText = "x";

        HtmlGenericControl messageElement = new HtmlGenericControl("div"); // build the div to host the message
        messageElement.Attributes.Add("id", uniqueId);
        messageElement.Attributes.Add("class", GetCcss(messageType));
        messageElement.InnerHtml = string.Format("<p>{0}</p>", message);
        messageElement.Controls.Add(closeButton); // add last or the innerhtml call will replace it.

        return messageElement;
    }

    /// <summary>
    /// If messageHolder is not null it is returned (we are writing to a user specified placeholder
    /// Otherwise the page is searched for the default plhMessage placeholder
    /// </summary>
    private static PlaceHolder GetPlaceholder(Page page, PlaceHolder messageHolder) {
        if (messageHolder != null) return messageHolder;

        foreach (Control control in page.Form.Controls)
            if (control is ContentPlaceHolder) return control.FindControl("plhMessages") as PlaceHolder;

        return page.FindControl("plhMessages") as PlaceHolder;
    }

    /// <summary>
    /// If messageHolder is not null it is returned (we are writing to a user specified placeholder
    /// Otherwise the nasterPage is searched for the default plhMessage placeholder
    /// </summary>
    private static PlaceHolder GetPlaceholder(MasterPage masterPage, PlaceHolder messageHolder) {
        if (messageHolder != null) return messageHolder;

        foreach (Control control in masterPage.Controls) {
            if (control is HtmlForm) {
                foreach (Control formControl in control.Controls) {
                    if (formControl is PlaceHolder) return formControl as PlaceHolder;
                }
            }
        }

        return masterPage.FindControl("plhMessages") as PlaceHolder;
    }

    /// <summary>
    /// If messageHolder is not null it is returned (we are writing to a user specified placeholder
    /// Otherwise the coontrol is searched for the default plhMessage placeholder
    /// </summary>
    private static PlaceHolder GetPlaceholder(Control control, PlaceHolder messageHolder) {
        if (messageHolder != null) return messageHolder;

        foreach (Control childControl in control.Controls) {
            if (childControl is PlaceHolder) return childControl as PlaceHolder;
        }
        
        return control.FindControl("plhMessages") as PlaceHolder;
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

    /// <summary>
    /// Wriites any embedded resources to the page (css in this case)
    /// </summary>
    private static void Instantiate(ViewMasterPage page) {
        // get the embedded css so we can embed it on the page
        if (!page.Page.ClientScript.IsClientScriptBlockRegistered("messagesCss")) {
            string css_out = string.Format("<link href='{0}' type='text/css' rel='stylesheet' />", page.Page.ClientScript.GetWebResourceUrl(typeof(smokesignals_signaller), "smokesignals.messages.css"));
            page.Page.ClientScript.RegisterClientScriptBlock(page.GetType(), "messagesCss", css_out, false);
        }
    }
}
