# smokesignals

Send messages to the screen in a super simple way in your .net mvc web applications.

## Overview

Setting up is pretty easy

Install the package from nuget.org (Install-Package smokesignals, in the package manager console in visual studio 2010).

Include the following in the head of your master page

    <link href="@Url.Content("~/Public/css/messages.css")" rel="stylesheet" type="text/css" />
    <script src="@Url.Content("~/Public/js/head.load.min.js")"></script>
    <script>
        head.js('@Url.Content("~/Public/js/jquery-1.6.2.min.js")');
    </script>

After that, right after the opening body tag in your layout add this call

    @Html.Send()
    
Then in one of your controllers, if you want to send a message to the screen do this

    this.Receive(MessageTye.Success, "Thing was saved yo!");
    
And when your page reloads the message will appear anchored to the top of the browser viewport.

word.
