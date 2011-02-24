using System.Web.UI;
using System.Web.UI.WebControls;

public static partial class smokesignals_signaller {
    /// <summary>
    /// Writes a message to the control (in the messageHolder if provided) and decorates the message box with some nice css based on 
    /// what MessageType you want to show.
    /// </summary>
    public static void SendMessage(this Control control, PlaceHolder messageHolder, MessageType messageType, string message, bool append) {
        WriteMessage(control.Page, GetPlaceholder(control, messageHolder), messageType, message, append);
    }

    /// <summary>
    /// Overloaded SendMessage (doesn't require messageHolder or append)
    /// </summary>
    public static void SendMessage(this Control control, MessageType messageType, string message) {
        SendMessage(control, null, messageType, message, false);
    }

    /// <summary>
    /// Overloaded SendMessage (doesn't require messageHolder)
    /// </summary>
    public static void SendMessage(this Control control, MessageType messageType, string message, bool append) {
        SendMessage(control, null, messageType, message, append);
    }
}

