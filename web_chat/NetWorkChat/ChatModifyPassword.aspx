<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatModifyPassword.aspx.cs" Inherits="NetWorkChat.ChatModifyPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Modify the password</title>
    <style type="text/css">
    body
    {
    	margin:5px;
    	background-position:center center;
    	background-repeat:no-repeat;
    	background-color:White;
    }
        .txtbox
        {
            width: 128px;
            height:24px;
            text-align:left;
            
        }
        
        .style1
        {   font-family:Arial;
            font-size:medium;
            color:#FF0066;
        	text-align:right;
            width: 300px;
            height: 39px;
        }
       
    </style>
</head>

<body background="Images/Password.jpg">
    <form id="form1" runat="server">
    <h1 align="center" style="color: #FF0066">&nbsp;</h1>
    
    <p align="center" style="color: #FF00FF; font-family: 'Vladimir Script';">
        <asp:Label ID="Label1" runat="server" Font-Names="STXingkai" 
            Font-Size="XX-Large" ForeColor="#FF0066" Text="修改密码"></asp:Label>
    </p>
    <p align="center" style="color: #0000FF">&nbsp;</p>
    <table class="style1"align="center">
        <tr>
            <td class="style1">原密码</td>
           
            <td class="txtbox">
                <asp:TextBox ID="txtOPassword" runat="server" Height="24px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="*密码不能为空！" Font-Size="Small" ValidationGroup="group" 
                    ForeColor="#0066FF" ControlToValidate="txtOPassword"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style1">新密码</td>
            <td class="txtbox">
            <asp:TextBox ID="txtNPassword1" runat="server" Height="24px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="*新密码不能为空！" Font-Size="Small" ValidationGroup="group" 
                    ForeColor="#0066FF" ControlToValidate="txtNPassword1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style1">确认新密码</td>
            <td class="txtbox">
                <asp:TextBox ID="txtNPassword2" runat="server" Height="24px" TextMode="Password"></asp:TextBox>
         <asp:CompareValidator ID="Comparepassword" runat="server" 
                    ControlToCompare="txtNPassword1" ControlToValidate="txtNpassword2" 
                    ErrorMessage="*两次密码不一致，请重新输入！" Font-Size="Small" ValidationGroup="group" 
                    ForeColor="#0066FF"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            
            <td>
            <input type="reset"  value="重置" text-align="right";
                    style="width: 67px; color: #FF0066;"/>
            </td>
            <td><asp:Button ID="SubmitBtn" runat="server" ForeColor="#FF0066" 
                    onclick="SubmitBtn_Click" Text="确认提交" Width="67px" 
                    ValidationGroup="group" text-align="center" /></td>
        </tr>
    </table>
    </form>
</body>
</html>
