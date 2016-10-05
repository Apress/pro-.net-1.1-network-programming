<%@ Page language="C#" %>
<%
HttpCookie cookie = Request.Cookies["MyName"];
if (cookie != null)
   Response.Write("Value for cookie MyName: " + cookie.Value);
else
   Response.Write("Cookie not set");
%>
