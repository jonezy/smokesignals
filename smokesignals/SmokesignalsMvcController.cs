using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

public static class SmokesignalsMvcController {
    public static void Receive(this Controller controller, MessageType messageType, string message) {
        StoreError(controller, messageType, message);
    }

    public static MvcHtmlString Send(this HtmlHelper helper, int heightOffset) {
        StringBuilder sb = new StringBuilder();
        List<SmokesignalError> errors = helper.ViewContext.TempData["SMOKESIGNALERRORS"] as List<SmokesignalError> ?? null;

        if (errors != null) {
            StringBuilder script = new StringBuilder();
            StringBuilder html = new StringBuilder();

            for (int i = 0; i < errors.Count; i++) {
                SmokesignalError error = errors[i];
                string signalId = string.Format("flash_{0}", i);

                script.AppendFormat("$('#{0}').html('<span>{1}</span>');", signalId, error.Message);
                script.AppendFormat("$('#{0}').toggleClass('{1}').slideDown('med');", signalId, error.ErrorType.ToCss());
                script.Append("$('#" + signalId + "').click(function(){$('#" + signalId + "').toggle('highlight')});");
                
                html.AppendFormat("<div id=\"{0}\" style=\"top:{1}px\"></div>", signalId, i * heightOffset);
            }

            sb.AppendLine("<script>");
            sb.AppendLine("head.ready(function() {");
            sb.AppendLine(script.ToString());
            sb.AppendLine("});");
            sb.AppendLine("</script>");
            sb.AppendLine(html.ToString());
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

    #region Deprecated methods
    public static void StoreSuccess(this Controller controller, string message) {
        Receive(controller, MessageType.Success, message);
    }

    public static void StoreInfo(this Controller controller, string message) {
        Receive(controller, MessageType.Info, message);
    }

    public static void StoreWarning(this Controller controller, string message) {
        Receive(controller, MessageType.Warning, message);
    }

    public static void StoreError(this Controller controller, string message) {
        Receive(controller, MessageType.Error, message);
    }

    public static string Notify(this HtmlHelper helper) {
        return Send(helper, 0).ToString();
    }
    #endregion
}

public enum MessageType { Error, Warning, Success, Info }

class SmokesignalError {
    public MessageType ErrorType { get; set; }
    public string Message { get; set; }
}