using System.Web;
using System.Web.Compilation;
using System.Web.UI;

public class smokesignalshandler : IHttpHandler {

    public bool IsReusable {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context) {
        HttpRequest request = context.Request;
        HttpResponse response = context.Response;
        
        Page page = BuildManager.CreateInstanceFromVirtualPath(context.Request.AppRelativeCurrentExecutionFilePath, typeof(Page)) as Page;

        if (page != null) {
            // get the embedded css so we can embed it on the page
            if (!page.ClientScript.IsClientScriptBlockRegistered("messagesCss")) {
                string css = string.Format("<link href='{0}' type='text/css' rel='stylesheet' />", page.ClientScript.GetWebResourceUrl(typeof(smokesignals_signaller), "smokesignals.messages.css"));
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "messagesCss", css, false);
            }

            IHttpHandler handler = page;
            handler.ProcessRequest(context);
        }

        return;
    }
}