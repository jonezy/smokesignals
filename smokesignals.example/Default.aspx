<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="smokesignals._Default" %>
<!DOCTYPE html> 
<html> 
<head> 
  <meta charset="utf-8" /> 
  <title>Html Starter - Layout</title>

  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">	
      
  <link rel="stylesheet" href="css/site.css" media="screen" />
  <link href="http://fonts.googleapis.com/css?family=Droid+Sans:regular,bold" rel="stylesheet" type="text/css">
</head>    
<body>
    <form id="form1" runat="server">
        <header class="clearfix">
          <a href="index.html" id="logo"><h1>smokesignals</h1></a>
          <nav><ul></ul></nav>
        </header>
        <section>
            <asp:PlaceHolder ID="plhMessages" runat="server" />
        </section>
        <footer></footer>
    </form>
</body>
</html>
