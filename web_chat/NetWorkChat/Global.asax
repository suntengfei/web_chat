<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        Logic.XmlDataManager xdm = new Logic.XmlDataManager();
        Application["chatMessageList"] = xdm.InitialChatMessageXml; 
        Application["userList"] = xdm.InitialUserListXml;
    }
   
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e) 
    {
        Application.Remove(Session["UserID"].ToString());
    }
       
</script>