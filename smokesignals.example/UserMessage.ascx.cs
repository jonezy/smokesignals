using System;

namespace smokesignals.example {
    public partial class UserMessage : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            this.SendMessage(MessageType.Warning, "I've been set from inside this user control");
        }
    }
}