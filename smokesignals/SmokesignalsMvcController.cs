using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

public static class SmokesignalsMvcController {
    public static void Store(this Controller controller, MessageType messageType, string message) {
        StoreError(controller, messageType, message);
    }

    #region Deprecated methods
    public static void StoreSuccess(this Controller controller, string message) {
        Store(controller, MessageType.Success, message);
    }

    public static void StoreInfo(this Controller controller, string message) {
        Store(controller, MessageType.Info, message);
    }

    public static void StoreWarning(this Controller controller, string message) {
        Store(controller, MessageType.Warning, message);
    }

    public static void StoreError(this Controller controller, string message) {
        Store(controller, MessageType.Error, message);
    } 
    #endregion

    public static MvcHtmlString Notify(this HtmlHelper helper) {
        StringBuilder sb = new StringBuilder();
        List<SmokesignalError> errors = helper.ViewContext.TempData["SMOKESIGNALERRORS"] as List<SmokesignalError> ?? null;

        if (errors != null) {
            foreach (var error in errors) {
                sb.AppendLine("<script>");
                sb.AppendLine("head.ready(function() {");
                sb.AppendFormat("$('#flash').html('{0}');", error.Message);
                sb.AppendFormat("$('#flash').toggleClass('{0}');", error.ErrorType.ToCss());
                sb.AppendLine("$('#flash').slideDown('med');");
                sb.AppendLine("$('#flash').click(function(){$('#flash').toggle('highlight')});");
                sb.AppendLine("});");
                sb.AppendLine("</script>");
            }
            sb.AppendLine("<div id=\"flash\"></div>");
        }

        return MvcHtmlString.Create(sb.ToString());
    }
    
    static void StoreError(Controller controller, MessageType messageType, string message) {
        List<SmokesignalError> errors = controller.TempData["SMOKESIGNALERRORS"] as List<SmokesignalError> ?? new List<SmokesignalError>();
        errors.Add(new SmokesignalError() { ErrorType = messageType, Message = message });
        controller.TempData["SMOKESIGNALERRORS"] = errors;
    }

    static string ToCss(this MessageType messageType) {
        switch (messageType) {
            case MessageType.Error:
                return "Message_Error Message_Flash";
            case MessageType.Warning:
                return "Message_Warning Message_Flash";
            case MessageType.Success:
                return "Message_Success Message_Flash";
            case MessageType.Info:
                return "Message_Info Message_Flash";
            default:
                return string.Empty;
        }
    }
}


