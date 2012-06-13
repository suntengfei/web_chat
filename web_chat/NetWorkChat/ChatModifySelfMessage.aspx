<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatModifySelfMessage.aspx.cs" Inherits="NetWorkChat.ChatModifySelfMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
            width: 471px;
            height: 39px;
        }
       
        .style2
        {
            font-family: Arial;
            font-size: medium;
            color: #FF0066;
            text-align: right;
            width: 116px;
            height: 39px;
        }
        .style3
        {
            width: 116px;
        }
       
        .style4
        {
            width: 188px;
            height: 24px;
            text-align: left;
        }
        .style5
        {
            width: 188px;
        }
       
        .style8
        {
            width: 181px;
            height: 40px;
            text-align: left;
        }
        .style9
        {
            width: 181px;
            height: 40px;
        }
        .style10
        {
            width: 181px;
        }
               
    </style>
</head>

<body background="Images/Password.jpg">
    <form id="form1" runat="server">
    <h1 align="center" style="color: #FF0066">&nbsp;</h1>
    <h1 align="center" style="color: #FF0066">
        <asp:Label ID="Label1" runat="server" Font-Names="STXingkai" 
            Font-Size="XX-Large" ForeColor="#FF0066" Text="修改个人信息"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </h1>
    <p align="center" style="color: #FF0066">
        </p>
    <table class="style1"align="center">
    <tr VALIGN="top">
      <td class="style2">用户名</td>
      <td class="style8">
       <asp:TextBox ID="txtUserName" runat="server" MaxLength="20" Height="24px"
              Width="128px"></asp:TextBox>
      </td>
      <td class="style9">
          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
              ControlToValidate="txtUserName" 
              ForeColor="#0066FF" 
              ValidationExpression="^[a-zA-Z|\d|\u0391-\uFFE5]{4,20}$" 
              ValidationGroup="group" Font-Size="Small" 
              ErrorMessage="*用户名只能由4-20个字组成，包括字母,数字或汉字。"></asp:RegularExpressionValidator>
      </td>
    </tr>
        <tr VALIGN="top">
            <td class="style2">原密码</td>
            <td class="style4">
                <asp:TextBox ID="txtOPassword" runat="server"  Width="128px" Height="24px" TextMode="Password"></asp:TextBox>
            </td>
            <td class="style10">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="*密码不能为空！" Font-Size="Small" ValidationGroup="group" 
                    ForeColor="#0066FF" ControlToValidate="txtOPassword"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr VALIGN="top">
            <td class="style2">新密码</td>
            <td class="style4">
            <asp:TextBox ID="txtNPassword1" runat="server"  Width="128px" Height="24px" TextMode="Password"></asp:TextBox>
            </td>
            <td class="style10">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="*新密码不能为空！" Font-Size="Small" ValidationGroup="group" 
                    ForeColor="#0066FF" ControlToValidate="txtNPassword1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr VALIGN="top">
            <td class="style2">确认新密码</td>
            <td class="style4">
                <asp:TextBox ID="txtNPassword2" runat="server"  Width="128px" Height="24px" TextMode="Password"></asp:TextBox>
            </td>
            <td class="style10">
         <asp:CompareValidator ID="Comparepassword" runat="server" 
                    ControlToCompare="txtNPassword1" ControlToValidate="txtNpassword2" 
                    ErrorMessage="*两次密码不一致，请重新输入！" Font-Size="Small" ValidationGroup="group" 
                    ForeColor="#0066FF"></asp:CompareValidator>
            </td>
        </tr>
        <tr VALIGN="top">
        <td class="style2">备注</td>
        <td class="style4"><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" 
                Height="69px"></asp:TextBox></td>
        </tr>
        <tr>
            
            <td class="style3">
            <input type="reset"  value="重置"
                    style="width: 67px; color: #FF0066;"/>
            </td>
            <td class="style5"><asp:Button ID="SubmitBtn" runat="server" ForeColor="#FF0066" 
                    onclick="SubmitBtn_Click" Text="确认提交" Width="67px" 
                    ValidationGroup="group" text-align="center" /></td>
        </tr>
    </table>
    </form>
</body>
</html>
