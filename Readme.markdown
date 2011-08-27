= smokesignals

Send messages to the screen in a super simple way in your .net mvc web applications.

== Overview

Setting up is pretty easy

Include the following in the head of your master page

    <link href="@Url.Content("~/Public/css/messages.css")" rel="stylesheet" type="text/css" />
    <script src="@Url.Content("~/Public/js/head.load.min.js")"></script>
    <script>
        head.js('@Url.Content("~/Public/js/jquery-1.6.2.min.js")');
    </script>

