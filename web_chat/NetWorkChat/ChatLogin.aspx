<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatLogin.aspx.cs" Inherits="NetWorkChat.ChatLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>登录hitachi聊天</title>
    <style type="text/css">
        h1
        {
            text-align: center;
            font-family: @华文隶书;
            font-size: xx-large;
            color: #FF0066;
            width: 291px;
            height: 56px;
        }
        .style2
        {
            height: 243px;
            text-align: center;
            margin: 80px 100px 60px 220px;
            width: 361px;
        }
        .styleBtn
        {
            text-align: center;
            font-family: @华文隶书;
            font-size: small;
            color: #FF0066;
            width: 120px;
            height: 66px;
        }
        body
        {
            text-align: center;
            background-color: White;
            background-position: center center;
            background-repeat: no-repeat;
        }
        .lable
        {
            text-align: right;
            font-family: STLiti;
            font-size: large;
        }
        .txtbox
        {
            width: 128px;
            height: 25px;
        }
        .styletd1
        {
            width: 118px;
            height: 50px;
        }
        .styletd2
        {
            width: 128px;
            height: 50px;
        }
        .style3
        {
            text-align: center;
            width: 150px;
            height: 100px;
            margin: 10px 30px 10px 30px;
        }
        .style4
        {
            width: 144px;
            height: 50px;
        }
        .style5
        {
            width: 312px;
        }
    </style>
</head>
<body background="Images/lOGIIN2.jpg">
    <form id="form1" runat="server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <table class="style2" align="center">
        <tr>
            <td class="style5">
                <h1 align="center" style="font-family: 'Colonna MT'; font-size: 60px;">
                    Log&nbsp; in</h1>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="style5">
                <table class="style3">
                    <tr>
                        <td class="style4">
                            <asp:Label ID="lblUserEmail" runat="server" Font-Names="Colonna MT" ForeColor="#FF0066"
                                Text="UserEmail" Font-Size="X-Large" CssClass="lable"></asp:Label>
                            &nbsp;
                        </td>
                        <td class="styletd2">
                            <asp:TextBox ID="txtUserEmail" runat="server" Height="26px" CssClass="txtbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="lblUserPassword" runat="server" Font-Names="Colonna MT" ForeColor="#FF0066"
                                Text="UserPassword" Font-Size="X-Large" CssClass="lable"></asp:Label>
                        </td>
                        <td class="styletd2">
                            <asp:TextBox ID="txtUserPassword" runat="server" Height="25px" TextMode="Password"
                                CssClass="txtbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Button ID="LoginBtn" runat="server" OnClick="LoginBtn_Click" Text="Log in" ForeColor="#FF0066"
                                Width="94px" text-align="center" CssClass="styleBtn" Height="34px" Font-Names="Colonna MT"
                                Font-Size="X-Large" />
                        </td>
                        <td class="styletd2">
                            <asp:Button ID="RegisterBtn" runat="server" OnClick="RegisterBtn_Click" Text="Register"
                                ForeColor="#FF0066" Width="94px" CssClass="styleBtn" Height="34px" Font-Names="Colonna MT"
                                Font-Size="X-Large" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
