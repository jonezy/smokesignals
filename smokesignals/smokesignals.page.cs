using System.Web.UI;
using System.Web.UI.WebControls;

public static partial class smokesignals_signaller {
    /// <summary>
    /// Writes a message to the page (in the messageHolder if provided) and decorates the message box with some nice css based on what MessageType you want to show.
    /// </summary>
    public static void SendMessage(this Page page, PlaceHolder messageHolder, MessageType messageType, string message, bool append) {
        PlaceHolder messagePlaceHolder = GetPlaceholder(page, messageHolder);
        WriteMessage(page, messagePlaceHolder, messageType, message, append);
    }

    /// <summary>Overloaded SendMessage (doesn't require messageHolder or append)</summary>
    public static void SendMessage(this Page page, MessageType messageType, string message) {
        SendMessage(page, null, messageType, message, false);
    }

    /// <summary>Overloaded SendMessage (doesn't require messageHolder)</summary>
    public static void SendMessage(this Page page, MessageType messageType, string message, bool append) {
        SendMessage(page, null, messageType, message, append);
    }
    
}

