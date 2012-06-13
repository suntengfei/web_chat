<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatRegister.aspx.cs" Inherits="NetWorkChat.ChatRegister" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>聊天hitachi注册</title>
    <style type="text/css">
         body
        {
        	background-color:White;
        	background-position:center center;
        	background-repeat:no-repeat;
        }
       .txtbox
       {
       	text-align:left;
       	width:128px;
       	height:22px;
       }
        .style1
        {
            text-align: right;
<%--            font-family: @MS PGothic;--%>
            font-size: medium;
            color: Yellow;
            width: 341px;
            padding:10px 10px 10px 30px;
        }
        .style2
        {
            
            text-align:center;
            width:400px;
            height:300px;
            margin:10px 70px 10px 70px;
        }
        .style3
        {
            width: 144px;
            height: 56px;
        }
       
       
           .style4
           {
           	width:529px;
           	height:400px;
           	text-align:center;
           	margin:80px 150px 60px 120px;
           	}   
       .style5
       {
       	width:220px;
       	height:56px;
       	
       	}
         
              
        </style>
    
</head>
<body background="Images/圣诞树.jpg" bgcolor="#0066ff">
    <form id="form1" runat="server">

    <table class="style4">
        <tr>
            <td >
    
    
       <asp:Label ID="Label3" runat="server" Text="请输入注册信息" Font-Names="STLiti" 
            ForeColor="Yellow" align="center" Font-Size="X-Large"></asp:Label>
               <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table class="style2">
                    <tr> 
                        <td class="style3">
                            <asp:Label ID="Label4" runat="server" Text="用户名" Font-Names="Bauhaus 93" 
                                Height="45px" Width="57px" CssClass="style1"></asp:Label>
                        </td>
                        <td class="style5">
       
      <asp:TextBox ID="txtName" runat="server" MaxLength="20" CssClass="txtbox"></asp:TextBox>
        
                            <br />
        
        <asp:RequiredFieldValidator ID="nameInputvalidator" runat="server" 
            ControlToValidate="txtName" Display="Dynamic" 
            ErrorMessage="*用户名不能为空！" ValidationGroup="group" Font-Size="Small"></asp:RequiredFieldValidator>
                            <br />
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
              ControlToValidate="txtName" 
              ValidationExpression="^[a-zA-Z|\d|\u0391-\uFFE5]{4,20}$" 
              ValidationGroup="group" Font-Size="Small" 
              ErrorMessage="*用户名只能由4-20个字组成，包括字母,数字或汉字。"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" >
                            <asp:Label ID="Label5" runat="server" Text="邮 箱" Font-Names="Bauhaus 93" 
                                Height="29px" Width="53px" CssClass="style1"></asp:Label>
                        </td>
                        <td class="style5">
        
        <asp:TextBox ID="txtEmail" runat="server" CssClass="txtbox" Height="22px" Width="128px"></asp:TextBox>

                            <br />

        <asp:RequiredFieldValidator ID="emailInputvalidator" runat="server" 
            ControlToValidate="txtEmail" Display="Dynamic" 
            ErrorMessage="*邮箱不能为空！" ValidationGroup="group" Font-Size="Small"></asp:RequiredFieldValidator>
                            <br />
        <asp:RegularExpressionValidator ID="emailFormatValidator" runat="server" 
            ControlToValidate="txtEmail" Display="Dynamic" 
            ErrorMessage="*请输入正确的邮箱格式!" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
            ValidationGroup="a" Font-Size="Small"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                         <asp:Label ID="Label6" runat="server" Text="密码" Font-Names="Bauhaus 93"  Height="26px" 
                                Width="51px" CssClass="style1"></asp:Label>
                        </td>
                        <td class="style5">
   
        <asp:TextBox ID="txtPassword1" runat="server"  MaxLength="10" 
            TextMode="Password"  CssClass="txtbox"></asp:TextBox>

                            <br />

        <asp:RequiredFieldValidator ID="password1Inputvalidator" runat="server" 
            ControlToValidate="txtPassword1" Display="Dynamic" 
            ErrorMessage="*密码不能为空！" ValidationGroup="group" Font-Size="Small"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label7" runat="server" Text="&nbsp;确认密码"  
                                Font-Names="Bauhaus 93" Height="39px" 
                                Width="91px" CssClass="style1"></asp:Label>
                        </td>
                        <td class="style5">
        <asp:TextBox ID="txtPassword2" runat="server"  MaxLength="10" 
            TextMode="Password" CssClass="txtbox" ></asp:TextBox>


                            <br />


        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToCompare="txtPassword1" ControlToValidate="txtPassword2" 
            ErrorMessage="*两次密码输入不一致，请重新输入！" Font-Size="Small" ValueToCompare="group"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label8" runat="server" Text="备注" Font-Names="Bauhaus 93" Height="37px" Width="55px" CssClass="style1"></asp:Label>
                        </td>
                        <td>
       <asp:TextBox ID="txtRemark" runat="server" Height="89px" 
            TextMode="MultiLine" Width="220px"></asp:TextBox>
     
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
               
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               
                         <input type="reset" value="重置" style="width: 69px; color: #0000FF; right: 10px;text-align:center"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                         <asp:Button ID="SubmitRegisterBtn" runat="server" Text="确认注册" 
                                onclick="SubmitRegisterBtn_Click" Width="69px" ForeColor="Blue" text-align="center"/>
             </td>
        </tr>
    </table>
      </form> 
    
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
           
</body>
</html>
