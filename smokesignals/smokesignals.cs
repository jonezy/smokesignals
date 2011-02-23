using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public static class smokesignals {
    /// <summary>
    /// Writes a message to the page (in the messageHolder if provided) and decorates the message box with some nice css based on 
    /// what MessageType you want to show.
    /// </summary>
    public static void SendMessage(this Page page, PlaceHolder messageHolder, MessageType messageType, string message, bool append) {
        Instantiate(page);
       
        PlaceHolder plhMessages = messageHolder == null ? page.FindControl("plhMessages") as PlaceHolder : messageHolder;
        if (plhMessages != null) {
            // build the div to host the message
            HtmlGenericControl messageElement = new HtmlGenericControl("div");
            messageElement.Attributes.Add("class", GetCcss(messageType));
            messageElement.InnerHtml = string.Format("<p>{0}</p>", message);

            if (!append) plhMessages.Controls.Clear(); // clear controls if append is false

            plhMessages.Controls.AddAt(plhMessages.Controls.Count, messageElement);
        }
    }
    
    /// <summary>
    /// Overloaded SendMessage (doesn't require messageHolder or append)
    /// </summary>
    public static void SendMessage(this Page page, MessageType messageType, string message) {
        SendMessage(page, null, messageType, message, false);
    }
    
    /// <summary>
    /// Overloaded SendMessage (doesn't require messageHolder)
    /// </summary>
    public static void SendMessage(this Page page, MessageType messageType, string message, bool append) {
        SendMessage(page, null, messageType, message, append);
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
                break;
        }
    }
    
    /// <summary>
    /// Wriites any embedded resources to the page (css in this case)
    /// </summary>
    private static void Instantiate(Page page) {
        // get the embedded css so we can embed it on the page
        if (!page.ClientScript.IsClientScriptBlockRegistered("messagesCss")) {
            string css_out = string.Format("<link href='{0}' type='text/css' rel='stylesheet' />", page.ClientScript.GetWebResourceUrl(typeof(smokesignals), "smokesignals.messages.css"));
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "messagesCss", css_out, false);
        }
    }
}

/// <summary>
/// enum class to keep track of and identify message types.
/// </summary>
public enum MessageType { Error,Warning,Success,Info }