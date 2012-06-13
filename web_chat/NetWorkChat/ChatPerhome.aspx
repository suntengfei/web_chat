<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatPerhome.aspx.cs" Inherits="NetWorkChat.Perhome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server" >
<title>我的主页</title>
    
    <meta http-equiv="Page-Enter" content="revealTrans(duration=５.０, transition=２０)" /> 
    <meta http-equiv="Page-Exit" content="revealTrans(duration=５.０, transition=２０)" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />

    
    <style type="text/css" >
      body{
      
      background-repeat :no-repeat ;
      border:1px solid none;
      background-position :center ;	
      margin-left:7%;
      margin-right :6%;
      position:fixed;
      background-attachment:scroll;
      
      }
      
      
      h1
      { float :right;
      	font-family:@ST ;
      	font-size :60px;
      	font-weight :bold ;
      	color:#66FF33;
      	letter-spacing :7px;
      	text-align:center ;
      	width:400px;
      	clear:left;
      	margin-top:-5px;
      	margin-right:50px;} 
      	#div1
      	{
        float:right;
        margin-top:50px;
      	font-size:23px;
      	cursor:hand;
        height: 30px;
        width: 25%;
        color:Black ;
        
            
        } 
      #div2
      {
       
      float:right ;
      margin-top :20px;
      margin-right :10px;
      width:25%;
      height:250px;
      clear:right;
      
      	}
      	#div3
      	{float:right;
      	 clear:right;	
      	
      	margin-top:20px;
      	margin-right:10px;
      	
      	width:25%;}
      	#div4
      	{
        float:right; 
        font-size:30px;
        font-style :normal;
        font-family :Lucida Bright  ;
        font-weight :bolder ;
        width:40%;
       text-align:center ;
       margin-top:20px;
       letter-spacing :5px;
        
          		}
         #div5
         {
         float:left;
         margin-left:20px;
         margin-top:70px;
         font-size:15px;
         
         border:0px solid ;
         width:25%;}
      	#div6
      	{
      	float:left;
      	
      	margin-top:20px;
      	font-size:15px;
      	clear:left;
      	
      	}
      .style1
      {
      	float :left ;
      font-size:20px;
      clear:both;
      
      }
      
      	
     .style2
     {
     	cursor:hand;}
     .font1
     {
     	font-size:20px;
     	line-height:8mm;
     	padding-top:20px;
     	color:#999999;
     	font-variant :small-caps ;
     	font-weight :100;
     	} 	
   
     
    </style>
    
</head>
<body background="Images/主页背景.jpg" >
    <form id="form1" runat="server">
    
     <div id ="div1">
       <asp:Button ID="Button1" runat="server"
           Text="更改信息" Font-Size="20px" Height="25px" Width="80px" 
             onclick="Button1_Click" BackColor="#CCFF99" BorderWidth="0px" />
    <asp:Button ID="Button10" runat="server" 
             onclick="Button10_Click" 
             Text="退出" style="height: 26px" Font-Size="20px" BackColor="#CCFF99" 
             BorderWidth="0px" />
    </div>
   
   <h1 style="font-family: 方正舒体; color: #00FF00;">个人主页</h1>
   
    
    <div id ="div2"> 
        <asp:Label ID="Label1" runat="server" Text="我的昵称:" Font-Bold="False" 
            Font-Size="18px" ForeColor="#333333" Height="20px" CssClass="style1"></asp:Label><br/>
    <asp:Label ID="text1" runat="server"  CssClass="style1" 
            Font-Size="20px" ForeColor="Blue" ></asp:Label>  
         <asp:Label ID="Label2" runat="server" Text="我的账号:" Font-Bold="False" 
            Font-Size="18px" ForeColor="#333333" CssClass="style1" ></asp:Label> 
            
        <asp:Label ID="text2" runat="server" Font-Italic="False" Font-Size="20px" 
            CssClass="style1" ForeColor="Red"></asp:Label>
         <asp:Label ID="Label33" runat="server" Text="我的备注:" Font-Bold="False" 
            Font-Size="18px" ForeColor="#333333" CssClass="style1" ></asp:Label>   
        <asp:Label ID="text3" ForeColor="Gray" runat="server"  CssClass="style1" Font-Size="20px" ></asp:Label>
      </div>
      <div id="div3">
    <asp:Button ID="Button5" runat="server" Text="查找好友" 
               BackColor="#CCFF99" onclick="Button5_Click1"  
              Font-Size="20px" Height="30px" Width="85px"/><br/><br/>
               <asp:Label ID="Label5" runat="server" Text="  请输入查找的账号" 
               Visible="False"  ForeColor="#333333" Font-Size="20px"></asp:Label><br/>
              <asp:TextBox ID="TextBox1" runat="server"  
               Visible="False"></asp:TextBox>
           <br/><br/>
           
           <asp:ImageButton 
            ID="ImageButton1" runat="server" 
            ImageUrl="~/Images/搜索.gif" 
            Visible="False" onclick="ImageButton1_Click" Height="26px" Width="77px"   />
    </div>
     <div id="div5">
          <asp:Button ID="Button6" runat="server" Text="查看离线消息" BackColor="#CCFFCC" 
              BorderWidth="0px"  cursor="hand" CssClass="style2" 
              onclick="Button6_Click" Visible="False" Font-Size="20px"/>
              <br/><br/>
         
          <asp:TextBox ID="TextBox3" runat="server" Height="253px" 
              style="margin-top: 0px" TextMode="MultiLine" Visible="False" Width="186px"></asp:TextBox>
         
    <div id="div6" enableviewstate="True">
        <asp:Button ID="Button7" runat="server" Text="谁要加我？" BackColor="#CCFFCC" 
            BorderWidth="0px"   Font-Bold="True" 
            Font-Size="20px" onclick="Button7_Click" CssClass="style2" 
            Visible="False" /><br/>
        <asp:DropDownList ID="DropDownList1" runat="server" Height="25px" Width="156px" 
            Visible="False"> 
           </asp:DropDownList>  
            
        
        <br/>
        
        <asp:Button ID="Button8" runat="server"
                Text="同意" style="float:left ;margin:10px 0 10px 5px;" 
            BackColor="#CCFFFF" Width="50px" 
            onclick="Button8_Click" Visible="False" Height="26px" Font-Size="20px" />
        <asp:Button ID="Button9" runat="server" 
            Text="拒绝" style="float:none;margin:10px 10px  10px auto; " 
            BackColor="#CCFFFF"  onclick="Button9_Click" Visible="False" 
            Font-Size="20px" Height="26px" Width="50px" />
            
            </div>
         
         </div>
   
    <div id="div4">
    
    <p class ="font1">凡是遥远的地方<br/>对于我们<br/>都有一种诱惑<br/>不是诱惑于美丽<br/>就是诱惑于<br/>传<a href="ChatGroupchat.aspx" style="color:Blue;  ">enter群聊</a>说<br/>
    即便<br/>远方的风景<br/>并不尽如人意<br/>我们也<br/>无需在乎<br/>因为<br/>这实在是一个<br/>迷人的错<br/><a href="ChatPrivatechat.aspx">enter私聊</a><br/>到远方去<br/>到远方去
    <br/>熟悉的地方<br/>没有风景</p>
 </div>
      
      
     </form>
       
    

    
        
    </body>
</html>

