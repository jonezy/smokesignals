using System;

namespace smokesignals.example {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            this.SendMessage(MessageType.Error, "This is an error message set from the aspx page.", true);
            this.SendMessage(MessageType.Warning, "This is a warning message", true);
            this.SendMessage(MessageType.Success, "This is a success message", true);
            this.SendMessage(MessageType.Info, "This is an information message", true);
        }
    }
}
