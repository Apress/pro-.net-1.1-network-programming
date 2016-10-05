<html>
<script language="C#" runat="server">
  public void Page_Load(Object sender, EventArgs E)
  {
	lblHello.Text = "Some information!";
  }
</script>

<body style="font: 10pt verdana" bgcolor="ffffcc">
  <form runat="server">
  <h3>Hacking!</h3>
  <asp:Label id="lblHello" Text="Default text" runat="server"/>
  </form>
</body>
</html>