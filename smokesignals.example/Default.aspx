<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="smokesignals.example.Default" %>
<%@ Register TagPrefix="smokesignals" TagName="UserMessage" Src="~/UserMessage.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:PlaceHolder ID="plhMessages" runat="server" />

    <h3>User message control</h3>
    <smokesignals:UserMessage ID="userMessage" runat="server" />
</asp:Content>
