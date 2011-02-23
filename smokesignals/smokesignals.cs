using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace smokesignals {
    public static partial class smokesignals {

        public static void SendMessage(this Page page, MessageType messageType, string message) {
            SendMessage(page, messageType, message, false);
        }

        public static void SendMessage(this Page page, MessageType messageType, string message, bool append) {
            Instantiate(page);

            // for now we will assume that the control we'll add our messages to is called plhMessage
            PlaceHolder plhMessages = page.FindControl("plhMessages") as PlaceHolder;

            if (plhMessages != null) {
                // build the div to host the message
                HtmlGenericControl messageElement = new HtmlGenericControl("div");
                messageElement.Attributes.Add("class", GetCcss(messageType));
                messageElement.InnerHtml = string.Format("<p>{0}</p>", message);

                if (!append)
                    plhMessages.Controls.Clear();

                plhMessages.Controls.AddAt(plhMessages.Controls.Count, messageElement);
            }
        }

        /// <summary>
        /// Determines what css class to use based on the MessageType enum
        /// </summary>
        /// <param name="messageType">MessageType enum</param>
        /// <returns>a string containing a class name</returns>
        private static string GetCcss(MessageType messageType) {
            string cssClass = string.Empty;
            switch (messageType) {
                case MessageType.Error:
                    cssClass = "Message_Error";
                    break;
                case MessageType.Warning:
                    cssClass = "Message_Warning";
                    break;
                case MessageType.Success:
                    cssClass = "Message_Success";
                    break;
                case MessageType.Info:
                    cssClass = "Message_Info";
                    break;
                default:
                    break;
            }
            return cssClass;
        }

        /// <summary>
        /// Wriites any embedded resources to the page (css in this case)
        /// </summary>
        /// <param name="page"></param>
        private static void Instantiate(Page page) {
            // get the embedded css so we can embed it on the page
            if (!page.ClientScript.IsClientScriptBlockRegistered("messagesCss")) {
                string css_url = page.ClientScript.GetWebResourceUrl(typeof(smokesignals), "smokesignals.messages.css");
                string css_out = string.Format("<link href='{0}' type='text/css' rel='stylesheet' />", css_url);

                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "messagesCss", css_out, false);
            }
        }
    }
    
    /// <summary>
    /// enum class to keep track of message types.
    /// </summary>
    public enum MessageType {
        Error,
        Warning,
        Success,
        Info
    }
}
