<%@ Page Language="C#"%>

<%
    //Response.Write("abc");    
    string version = Request.QueryString["version"];
    if (version==null)
    {
        Response.Write("error");
        Response.End();
    }
    else if (version == "")
    {
        Response.Write("error");
        Response.End();
    }
    //else if (version == "1.2.5.32")
    //{
    //    Response.Write(@"http://192.168.0.230:6767/setup.exe");
    //    Response.End();
    //}
    else if(version != "2.2.0.2")
    {
				Response.Write(@"http://192.168.0.145/setup.exe");
        Response.End();
    }
    else
    {
        Response.Write("new");
        Response.End();
    }

%>