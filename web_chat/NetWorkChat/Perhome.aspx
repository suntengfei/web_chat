<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perhome.aspx.cs" Inherits="NetWorkChat.WebForm3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>个人主页</title>
    <style type="text/css" >
    .font1
    { font-family :@华文新魏;font-size:medium ;font-style:normal ;font-weight:lighter }
    .body1
    {background-color:White     ;background-attachment:fixed ;}
    .font2
    {font-family:@华文琥珀 ;font-size:medium ;font-style: inherit ;font-weight:bold } 
       
    </style>
</head>

<body background="http://t0.gstatic.com/images?q=tbn:EMsM3OVi42AbFM:http://163.17.166.1/xoops/html/web_html/account/account.files/image002.jpg">
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="1000px" Width="300px"  >
    <div class="body1">
    
    <img src="http://webqq.qq.com/images/icon.gif?ver=20091230001"  alt="" height="70" 
            width="70" />
    <asp:Label ID="Label1" runat="server" CssClass="font1" Height="30px" 
        Text="Label" Width="150px"></asp:Label></div>
    <div>
    
    
    
    <asp:Label ID="Label2" runat="server" Height="30px" Text="Label" Width="150px"></asp:Label>
    </div>
        
    <div>
    <asp:Label ID="Label3" runat="server" Height="200px" Text="Label" Width="200px"></asp:Label>
        
    </div>
    </asp:Panel>
   
    </form>
</body>
</html>
