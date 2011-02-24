using System.Web.UI;
using System.Web.UI.WebControls;

public static partial class smokesignals_signaller {
    /// <summary>
    /// Writes a message to the master page (in the messageHolder if provided) and decorates the message box with some nice css based on 
    /// what MessageType you want to show.
    /// </summary>
    public static void SendMessage(this MasterPage masterPage, PlaceHolder messageHolder, MessageType messageType, string message, bool append) {
        WriteMessage(masterPage.Page, GetPlaceholder(masterPage, messageHolder), messageType, message, append);
    }

    /// <summary>
    /// Overloaded SendMessage (doesn't require messageHolder or append)
    /// </summary>
    public static void SendMessage(this MasterPage masterPage, MessageType messageType, string message) {
        SendMessage(masterPage, null, messageType, message, false);
    }

    /// <summary>
    /// Overloaded SendMessage (doesn't require messageHolder)
    /// </summary>
    public static void SendMessage(this MasterPage masterPage, MessageType messageType, string message, bool append) {
        SendMessage(masterPage, null, messageType, message, append);
    }
}

