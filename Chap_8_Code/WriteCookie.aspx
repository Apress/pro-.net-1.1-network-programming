<%@ Page language="C#" %>
<%
string username = "Vinod";

HttpCookie cookie = new HttpCookie("username", username);
Response.Cookies.Add(cookie);
Response.Write("Hello, " + username);
%>

