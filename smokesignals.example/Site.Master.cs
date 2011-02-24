using System;

namespace smokesignals.example {
    public partial class Site : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            this.SendMessage(MessageType.Info, "This one is set from the master page", true);
        }
    }
}
