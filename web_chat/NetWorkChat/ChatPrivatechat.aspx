<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatPrivatechat.aspx.cs" Inherits="NetWorkChat.ChatPrivatechat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>hitachi 私人聊天室</title>
<script language="javascript" type="text/javascript"src = "Javascript/PrivateChat.js" ></script>
    <script language="javascript" type="text/javascript">
        document.onkeypress = function() {  //发送快捷键关联    Ctrl+Enter
            if (event.keyCode == 10) {
                sendMessage();
            }
            else if (event.keyCode == 11) {
                ClearScreen();
            }
            document.getElementById("tbSendMessage").focus();
        }

        function ClearScreen() {     //清屏方法
            document.getElementById("tbChatMessage").value = "";
            document.getElementById("tbSendMessage").focus();
        }
    </script>
    <style type="text/css">
        #text1
        {
            width: 730px;
            margin-bottom: 169px;
        }
        #P28
        {
            height: 27px;
            width: 168px;
        }
        .styletable
        {
            width: 160px;
            height:250px;
            text-align:center;
        }
        #P34
        {
            height: 27px;
            width: 168px;
        }
        body
        {
        	text-align:center;
        	background-color:White;
        	background-position:center center;
        	background-repeat:no-repeat;
        	}
        	.txtbox
        	{
        		text-align:left;
        		width:419px;
        	}
        	.lable
        	{
        		font-family:@隶书;
        		Color:Blue;
                Font-Size:X-Large;
                text-align:center;
        	}
    </style>
</head>
<body background="Images/蓝蝶1.jpg" onload="startUp()" onunload="closeIt()">
    <form id="form1" runat="server">
        <div style="text-align:center">
            <table width="200" border="0" style="margin-top: 1px" align="center";>
              <tr>
                <td colspan="2" >&nbsp;
                <asp:Label ID="Label1" runat="server" Font-Size="XX-Large"
                        Text="hitachi私人聊聊" Font-Names="STXingkai" ForeColor="#FF33CC"></asp:Label></td>
              </tr>
              <tr>
                <td>
                        <asp:Label ID="Label2" runat="server" ForeColor="#33CCFF" Text="聊天信息" 
                            Font-Names="STXingkai" Font-Size="X-Large"></asp:Label>&nbsp;</td>
                <td style="width: 217px">
                        <asp:Label ID="lbOnlines" runat="server" ForeColor="#00CCFF" Text="当前在线用户" 
                            Font-Names="STXingkai" Font-Size="X-Large"></asp:Label>&nbsp;</td>
              </tr>
              <tr>
                <td style="height: 340px">&nbsp;
                  <asp:TextBox ID="tbChatMessage" runat="server" Height="320px" 
                        TextMode="MultiLine" Width="350px"  ReadOnly="true" BorderStyle="Groove" 
                        BackColor="#FFFFCC" ></asp:TextBox>
                </td>
                <td style="width: 217px; height: 340px"><div id="ListBox1" 
                        style="border-style: solid; border-color: inherit; border-width: 1px; width:161px; height:275px;  margin-bottom: 1px;"></div>
                    <label>
                    </label><br />
                  <asp:Label ID="Label4" runat="server" ForeColor="Coral" Text="系统消息"></asp:Label></td>
              </tr>
              <tr>
                <td style="height: 100px">&nbsp;<asp:TextBox ID="tbSendMessage" runat="server" 
                        Height="116px" TextMode="MultiLine"
                            Width="350px" BackColor="#FFFFCC"></asp:TextBox></td>
                <td valign="top" style="width: 217px">&nbsp;
                  
                      <asp:TextBox ID="tbSystemMessage" runat="server" Height="96px" 
                        TextMode="MultiLine" Width="180px" ReadOnly="True" BackColor="#FFFFCC"></asp:TextBox>                </td>
              </tr>
              <tr>
                <td valign="top" style="height: 46px">
                                            <asp:Label ID="currentfriendID" runat="server" Height="29px" Width="126px"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      
                      <input name="btSend" type="button" id="btSend" value="发送Ctrl+Enter" 
                          onclick="sendMessage()" 
                          style="width: 95px; font-family: STXingkai; height: 29px;"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <br />
                                            <br />
                      &nbsp;
                                           <input name="btClear" type="button" id="btClear" value="清屏Ctrl+K" 
                          onclick ="ClearScreen()" 
                          style="color: #000000; font-family: STXingkai; height: 29px; width: 95px;"/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Button ID="BtnReturn" runat="server" Font-Names="STXingkai" Height="29px" 
                          onclick="BtnReturn_Click" Text="返回个人主页" Width="95px" />
                      <br />
                      <asp:Label ID="lbEmptyError" runat="server" ForeColor="Red" Font-Size="Small"></asp:Label>
                </td>
                <td align="left" valign="top" style="width: 217px; height: 46px;"><span >您的昵称：<asp:Label ID="lbUserName" runat="server" ForeColor="DeepPink"
                        Text="游客"></asp:Label></span></td>
              </tr>
        </table>
    </div>
    <div id = "Label5" runat ="server" style=" display:none"></div>
    </form>
</body>
</html>
