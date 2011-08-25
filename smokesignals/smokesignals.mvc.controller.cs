using System;
using System.Text;
using System.Web.Mvc;

public partial class smokesignals_signaller {
    public static void StoreSuccess(this Controller controller, string message) {
        controller.TempData["success"] = message;
    }

    public static void StoreInfo(this Controller controller, string message) {
        controller.TempData["info"] = message;
    }

    public static void StoreWarning(this Controller controller, string message) {
        controller.TempData["warning"] = message;
    }

    public static void StoreError(this Controller controller, string message) {
        controller.TempData["error"] = message;
    }

    public static string Notify(this HtmlHelper helper) {
        Instantiate(helper.ViewContext.View as WebFormView);

        var message = "";
        var className = "";

        if (helper.ViewContext.TempData["success"] != null) {
            message = helper.ViewContext.TempData["success"].ToString();
            className = "Message_Success Message_Flash";
        } else if (helper.ViewContext.TempData["info"] != null) {
            message = helper.ViewContext.TempData["info"].ToString();
            className = "Message_Info Message_Flash";
        } else if (helper.ViewContext.TempData["warning"] != null) {
            message = helper.ViewContext.TempData["warning"].ToString();
            className = "Message_Warning Message_Flash";
        } else if (helper.ViewContext.TempData["error"] != null) {
            message = helper.ViewContext.TempData["error"].ToString();
            className = "Message_Error Message_Flash";
        }

        var sb = new StringBuilder();
        if (!String.IsNullOrEmpty(message)) {
            sb.AppendLine("<script>");
            sb.AppendLine("head.ready(function() {");
            sb.AppendFormat("$('#flash').html('{0}');", message);
            sb.AppendFormat("$('#flash').toggleClass('{0}');", className);
            sb.AppendLine("$('#flash').slideDown('med');");
            sb.AppendLine("$('#flash').click(function(){$('#flash').toggle('highlight')});");
            sb.AppendLine("});");
            sb.AppendLine("</script>");
        }

        return sb.ToString();
    }
}

