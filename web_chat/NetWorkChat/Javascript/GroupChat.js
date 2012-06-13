var xmlhttpRenew = createAjaxObj();  //在线更新信息xmlhttp对象
var xmlhttpSend = createAjaxObj(); //发送信息xmlhttp对象


var xmls = new Array(2);      //将XmlHttp对象打包至数组
xmls[0] = xmlhttpSend;
xmls[1] = xmlhttpRenew;

var login = 0;
var lastMsgID = 0;            //最近聊天信息更新标记
var userListRenewTime = "00-01-01 00:00:00";    //最近用户列表更新时间
var userID = "";             //当前用户ID
var userName = "ttt";            //当前用户名
var userCount = "1";          //当前在线用户数目
var userListArray = new Array()   //当前用户列表数组
var lines = 0;            //当前聊天窗口信息条数
var friendNameList = new Array();             //用户好友列表
var friendIDList = new Array();
var onlinefriendName = new Array();
var onlineFriendID = new Array();
var outlineFriendName = new Array();
var outlineFriendID = new Array();
var onlineStrangerName = new Array();
var onlineStrangerID = new Array();
//var userListArray = new Array();


function createAjaxObj() {       //XMLhttpRequest 对象创建函数
    var xmlhttp;
    if (window.XMLHttpRequest) {
        //如果为 Mozilla，Safari 等浏览器
        xmlhttp = new XMLHttpRequest();
        if (xmlhttp.overrideMimeType)
            xmlhttp.overrideMimeType('text/xml');
    }
    else if (window.ActiveXObject) {    //如果是Ie浏览器
        try {
            xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (e) { }
        }

    }
    return xmlhttp;
}

function startUp() {    //启动函数  
    initialize();                        //用户登陆初始化函数          
    setInterval("onlineRenew()", 1000);      //启动在线信息更新
    document.getElementById("tbSendMessage").focus();
}

function initialize() {      //用户登陆初始化方法    
    systemMessageShow("正在登陆中...", false);
    userName = document.getElementById("lbUserName").innerText;
    var username = escape(userName);
    fillfriendlist();
    var url = "Chat_Server.aspx?serverType=userlogin"+"&userName=" + username;
    SendCenter(0, url);    //发送XmlHttp连接请求  

}

function onlineRenew() {     //在线信息更新函数         
    var url = "Chat_Server.aspx?serverType=onlinerenew"+"&lastMsgID=" + lastMsgID + "&userListRenewTime=" + userListRenewTime;
    SendCenter(1, url);      //发送XmlHttp连接请求
}

function sendMessage() {     //信息发送函数
    var tbSendMessage = document.getElementById("tbSendMessage");
    var msg = tbSendMessage.value;
    if (msg == "") {
        emptyErrorShow("您不能发送空消息...");
        return;
    }
    else {
        emptyErrorShow("");
    }
    var username = escape(userName);
    msg = escape(msg);
    url = "Chat_Server.aspx?serverType=messagesend" + "&userID=" + userID + "&lastMsgID=" + lastMsgID + "&userName=" + username + "&userMessage=" + msg + "&userListRenewTime=" + userListRenewTime;
    tbSendMessage.value = "";
    SendCenter(0, url);    //发送XmlHttp连接请求

}


function SendCenter(xmlIndex, url) {     //信息发送中心
    if (xmls[xmlIndex].readyState == 4 || xmls[xmlIndex].readyState == 0) {
        var number = Math.random() * 1000;
        url = addUrlArg(url, "ran", number);
        xmls[xmlIndex].open("GET", url, true);
        var handle = new changeHandle(xmlIndex);
        xmls[xmlIndex].onreadystatechange = handle.Listen;
        xmls[xmlIndex].send();
    }
}

function changeHandle(i) {     //xmlhttp 返回信息接收函数
    var index = i;
    this.Listen = function gg() {
        if (xmls[index].readyState == 4 && xmls[index].status == 200) {
            var restext = xmls[index].responseText;
            messageManageCenter(restext);        //提交到客户端信息处理中心                    
        }
    }
}

function showMessage(msg) {
    var tbChatMessage = document.getElementById("tbChatMessage");
    tbChatMessage.value += msg;
}

function messageManageCenter(text) {       //客户端信息处理中心
    var xmldoc = new ActiveXObject("Microsoft.XMLDOM");
    xmldoc.loadXML(text);
    setUserName(xmldoc);                   //设置完整用户名
    renewUserList(xmldoc);                 //更新用户列表
    renewChatMessage(xmldoc);              //更新聊天信息
    checkPrivateChat(xmldoc);
}

function renewChatMessage(xmldoc) {          //更新聊天信息函数
    var chatMessage = xmldoc.getElementsByTagName("chatMessageList")[0];
    if (chatMessage != null) {
        var count = chatMessage.getAttribute("count");
        var cbshowall = document.getElementById("cbShowAllMsg");
        userId = xmldoc.documentElement.getAttribute("userID");
        var childs = chatMessage.getElementsByTagName("message");
        if (childs.length == 0) {
            return;
        }
        lastMsgID = count;
        var str = "";
        var tbChatMessage = document.getElementById("tbChatMessage");
        if (!cbshowall.checked && lines >= 8) {
            tbChatMessage.value = "";
            lines = 0;
        }
        for (var i = 0; i < childs.length; i++) {
            str += childs[i].childNodes[0].nodeValue + "\n";
            lines++;
        }
        str = unescape(str);
        tbChatMessage.value += str;
    }
}


function userListManage(newUserList) {                       //用户列表更新管理函数
    if (userListArray.length != 0) {
        var onlineUserArray = getOutUser(newUserList, userListArray);
        var offlineUserArray = getOutUser(userListArray, newUserList);
        systemMessageManage(onlineUserArray, true);
        systemMessageManage(offlineUserArray, false);
    }
    userListArray = newUserList;
}

function systemMessageManage(userlist, online) {      //系统消息处理函数  
    if (online) {
        systemMessageShow("", true);
    }
    var msg = (online ? "上线了" : "下线了")
    for (var i = 0; i < userlist.length; i++) {
        var str = userlist[i] + " " + msg
        systemMessageShow(str, false);
    }
}

function getOutUser(resArray, objArray) {     //获取多出用户
    var objStr = "";
    var ret = new Array();
    for (var i = 0; i < objArray.length; i++)
        objStr += objArray[i];
    for (var i = 0; i < resArray.length; i++) {
        if (objStr.indexOf(resArray[i]) == -1) {
            ret[ret.length] = resArray[i];
        }
    }
    return ret;
}


function setUserName(xmldoc) {     //设置完整用户名
    if (userID.length != 0) {
        return;
    }
    var root = xmldoc.documentElement;
    if (root != null&&login==0) {
          userID = root.getAttribute("userID");
       // userName = root.getAttribute("userName");
        lbusername = document.getElementById("lbUserName");
        lbusername.innerText = userName + "(" + userID + ")";
        systemMessageShow("登陆成功...", false);
        login++;
    }
}

function addUrlArg(url, name, value) {     //Url参数累加函数
    var connect = (url.indexOf("?") == -1) ? "?" : "&";
    url = url + connect + name + "=" + value;
    return url;
}

function emptyErrorShow(msg) {  //发送空信息警告
    var lbEmptyError = document.getElementById("lbEmptyError");
    lbEmptyError.innerText = msg;
}


function showManager() {     //聊天信息管理    获得更多消息modified by stf  2010.8.19
    var cbshowall = document.getElementById("cbShowAllMsg");
    if (cbshowall.checked) {
        document.getElementById("tbChatMessage").value = "";
        var url = "Chat_Server.aspx?serverType=onlinerenew&userID=" + userID + "&lastMsgID=0" + "&userListRenewTime=" + userListRenewTime;
        SendCenter(0, url);      //发送XmlHttp连接请求        
    }
}

function systemMessageShow(msg, clear) {    //系统消息显示函数
    var tbSystemMessage = document.getElementById("tbSystemMessage");
    if (clear) {
        tbSystemMessage.value = "";
    }
    if (msg == "") {
        return;
    }
    var now = new Date();
    var time = now.getHours() + ":" + now.getMinutes() + ":" + now.getSeconds();
    msg = msg + "\n" + time + "\n";
    tbSystemMessage.value += msg;
}



        function renewUserList(xmldoc){                                               //刷新好友列表
            var userList = xmldoc.getElementsByTagName("userList")[0];
            if(userList !=null){
                 userListRenewTime = userList.getAttribute("listRenewTime");  //更新用户列表更新时间        
                 userCount = userList.getAttribute("count");
                 listBoxClear("ListBox1");//     清楚所有陌生人
                 //listBoxClear("ListBox2");//     清楚所有好友
                 var lbuserCount = document.getElementById("lbOnlines");
                 lbuserCount.innerText = "当前在线用户" + userCount + "人";
                 var newPeopleName = new Array();
                 var newPeopleID = new Array();
                 var newuserList = new Array();
                 var childs = userList.childNodes;
                 for(var i=0;i<childs.length;i++){
                      var name = childs[i].getAttribute("name");
                      var id = childs[i].getAttribute("id");
                      var username = "(" + id + ") " + name;
                      newuserList[newuserList.length] = username;
                      newPeopleName[newPeopleName.length]=name;
                      newPeopleID[newPeopleID.length]=id;
                 }
//                 addListBoxItem("ListBox1",newPeopleID,newPeopleName,0);
                 friendListFactory(newPeopleName,newPeopleID);
                 addListBoxItem("ListBox1",onlineStrangerID,onlineStrangerName,0);  //添加所有在线陌生人
                 addListBoxItem("ListBox1",onlineFriendID,onlinefriendName,1);    //添加所有在线好友
                 addListBoxItem("ListBox1",outlineFriendID,outlineFriendName,2);  //添加所有离线好友
                 userListManage(newuserList);
            }     
            
        }
        
        function friendListFactory(newPeopleName,newPeopleID){                          //好友列表工厂
             if(newPeopleID.length !=0){
                  outlineFriendID = getOutPeopleID(friendIDList,newPeopleID);
                  outlineFriendName =getOutPeopleName(friendIDList,friendNameList,newPeopleID);
                  onlineFriendID = getOutPeopleID(friendIDList,outlineFriendID);
                  onlinefriendName = getOutPeopleName(friendIDList,friendNameList,outlineFriendID);
                  onlineStrangerID = getOutPeopleID(newPeopleID,friendIDList);
                  onlineStrangerName = getOutPeopleName(newPeopleID,newPeopleName,friendIDList);
             }
        }
        
        function getOutPeopleID(resArray, objArray) {     //获取多出用户ID
             var objStr = "";
             var ret = new Array();
             for (var i = 0; i < objArray.length; i++)
                    objStr += objArray[i];
             for (var i = 0; i < resArray.length; i++) {
                    if (objStr.indexOf(resArray[i]) == -1) {
                         ret[ret.length] = resArray[i];
                    }
             }
             return ret;
        }
        
        function getOutPeopleName(IDArray,nameArray,objArray) {     //获取多出用户Name
             var objStr = "";
             var idlist = new Array();
             var namelist = new Array();
             for (var i = 0; i < objArray.length; i++)
                  objStr += objArray[i];
                 for (var i = 0; i < IDArray.length; i++) {
                      if (objStr.indexOf(IDArray[i]) == -1) {
                            namelist[namelist.length] = nameArray[i];
                 }
             }
             return namelist;
        }


        function addListBoxItem(listbox,ID,name,index){                          //好友列表排版
             var listbox =  document.getElementById(listbox); 
             for(var i=0;i<ID.length;i++){
             var row = document.createElement("p");
             row.setAttribute("id",ID[i]);
             row.setAttribute("align","Left");
             var cell1 = document.createElement("img");
             cell1.setAttribute("alt","图");
             if(index == 0)
             {      
                cell1.setAttribute("src","Images/mphoto.jpg");
             }//陌生人头像
             if(index == 1)
             {
                cell1.setAttribute("src","Images/lphoto.jpg");
             }
             if(index == 2)
             {
                cell1.setAttribute("src","Images/wphoto.jpg");
             }
             var cell2 = document.createElement("b"); 
             cell2.setAttribute("style","color:black");
//             cell2.setAttribute("innerText",name[i]+"("+ID[i]+")");
             cell2.setAttribute("innerText",name[i]);
             row.appendChild(cell1);
             row.appendChild(cell2);
             if(index != 0){
             if(index !=2){
             row.onmousemove = function() { mover(this); }
             row.onmouseout = function() { mout(this); }
             }
             row.ondblclick= function(){directToPrivatechat();}
             }
             listbox.appendChild(row);
             }
       } 
  
        
       function listBoxClear(id) {       //列表框清空函数  
             var obj = document.getElementById(id);
             var objitem = obj.getElementsByTagName("p")
             var objcount = objitem.length;
             for(var i=0;i<objcount;i++)
                   obj.removeChild(obj.firstChild);   
        }         
       function mover(im){
        // if(document.getElementById(im.id).firstChild.src.toString()!="NewFolder1/shandong.gif")
     {
         document.getElementById(im.id).firstChild.src = "Images/llphoto.jpg";
         document.getElementById(im.id).style.color="Red";}
 }
 function mout(im){
      //if(document.getElementById(im.id).firstChild.src.toString()!="NewFolder1/shandong.gif")
      {document.getElementById(im.id).firstChild.src="Images/lphoto.jpg";
      document.getElementById(im.id).style.color="Black";}
 }
 function directToPrivatechat()
{
    if(window.child && window.child.open && !window.child.closed)  
            {  
                window.child.focus();  
                //window.child.callback();  
            }else{  
                window.child=window.open('ChatPrivatechat.aspx','CIDAS_MAIN');  
            }
}

function fillfriendlist(){                               //created by suntengfei 2010.8.18
    if(document.getElementById("tbFriendList") != null)
    {
        for(var ii=0;ii<document.getElementById("tbFriendList").childNodes.length;ii++)
        {
             for(var iii=0;iii<document.getElementById("tbFriendList").childNodes[ii].childNodes.length;iii++)
             {
             friendIDList[friendIDList.length] = document.getElementById("tbFriendList").childNodes[ii].childNodes[iii].firstChild.innerText;
             friendNameList[friendNameList.length] = document.getElementById("tbFriendList").childNodes[ii].childNodes[iii].lastChild.innerText;
             }
        }
    }
}

  function checkPrivateChat(xmldoc){
              var privateChatMessage = xmldoc.getElementsByTagName("privateChatList")[0];
              if(privateChatMessage !=null){
                 if(privateChatMessage.getAttribute("loginPri")=="false")
                  {
                  var childs = privateChatMessage.childNodes;
                  if(childs.length == 0)
                   {
                       return;
                    } 
                  var listbox =  document.getElementById("Listbox1");
                  var listboxc = listbox.childNodes;
                  for(var i=0;i<childs.length;i++){
                       var id = childs[i].getAttribute("senderID"); 
                           for(var j=0;j<listboxc.length;j++)
                                if(id == listboxc[j].getAttribute("ID"))
                                       listboxc[j].firstChild.src = "Images/shandong.gif";  
                  }
                  }                
              }
      }                                                                       
//by SunTengfei   2010.8.16