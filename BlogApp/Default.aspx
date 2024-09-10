<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BlogApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <div>
            <h1>Welcome to the Blog App</h1>
            
            <asp:Panel ID="AnonymousPanel" runat="server">
                <asp:HyperLink runat="server" NavigateUrl="~/Login.aspx">Log In</asp:HyperLink>
                <asp:HyperLink runat="server" NavigateUrl="~/Signup.aspx">Sign Up</asp:HyperLink>
            </asp:Panel>
            
            <asp:Panel ID="LoggedInPanel" runat="server" Visible="false">
                <h3>Welcome, <asp:Label ID="UsernameLabel" runat="server"></asp:Label></h3>
                <br />

                <asp:HyperLink runat="server" NavigateUrl="~/ManagePosts.aspx">Manage your Posts</asp:HyperLink>
                <asp:Button ID="LogoutButton" runat="server" Text="Logout" OnClick="LogoutButton_Click" />
            </asp:Panel>
             <br />
             <br />
             <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <h2>Recent Posts</h2>
            

             <asp:Repeater ID="PostsRepeater" runat="server" OnItemCommand="PostsRepeater_ItemCommand">
                <ItemTemplate>
                    <div class="post">
                        <h3><%# Eval("Title") %></h3>
                        <p><%# Eval("Content") %></p>
                        <p>Posted on: <%# Eval("CreatedAt", "{0:MM/dd/yyyy}") %></p>
                        <asp:Button ID="CommentButton" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Comment" Text="Comments" />
                        <br /><br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>


        </div>
    </form>
</body>
</html>
