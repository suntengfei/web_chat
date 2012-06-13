var xmlHttp =null; 
function createXML(){

 function CreateXmlHttpObject()
{ 
  try
    {
    // Firefox, Opera 8.0+, Safari
    xmlHttp=new XMLHttpRequest();
    }
  catch (e)
    {
    // Internet Explorer
    try
      {
      xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");
      }
    catch (e)
      {
      xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
      }
    }
}
}
function GetXmlHttpObject()
{
    if(xml==null)   
        CreateXmlHttpObject();
    else
        return;
}
function checkSubmit()
{ 
    var msg = document.getElementById("InputID").value;
        if(msg==""||msg==null)
           document.getElementById("InputID").focus();
         else 
           sendmessage(msg);      

}
function sendMessage(msg)
{
    var param = "task=send&msg="+msg;
    ajaxRequest(param);
    document.getElementById("InputID").value = "";
}
function ajaxRequest(param)
{
    var url = "website?work=message&time="+new Date().getTime();
    GetXmlHttpObject();
    xmlHttp.onreadystatechange = refreshMessage;
    xmlHttp.open("POST",url,true);
    xmlHttp.setRequestHeader("Content-type","application/x-www-form-urlencoded;");
    xmlHttp.send(param);
}
function refreshMessage()
{
    if(xmlHttp.readyState==4)
       if(xmlHttp.status==200)
             {
                 document.getElementById("AllchattextboxID").text+=(xmlHttp.ResponseText+"<br/>");
              }
     setTimeout(queryInformation,2000);
}
function queryInformation()
{
     refreshFriendList();
     var param = "task=get";
     ajaxRequest(param);
}
function requestFriendList();
{
     url = "website?work=friendlist";
     GetXmlHttpObject();
     xmlHttp.onreadystatechange = refreshFriendList;
     xmlHttp.open("GET",url,true);
     xmlHttp.send(null);
}
function refreshFriendList()
{
     var friendlist = xmlHttp.responseXML.getElementsByTagName("friend");
     makefriendlist(friendlist);
}
function makefriendlist()
{

}







function directToPrivatechat()
{
    if(window.child && window.child.open && !window.child.closed)  
            {  
                window.child.focus();  
                window.child.callback();  
            }else{  
                window.child=window.open('private.aspx','CIDAS_MAIN');  
            }
}
