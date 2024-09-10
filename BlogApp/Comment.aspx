<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comment.aspx.cs" Inherits="BlogApp.Comment" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Post Comments</title>
</head>
<body>
    <form id="form2" runat="server">
        <div>
            <h1>Comment</h1>
            <!-- Blog Post Section -->
            <h1><asp:Label ID="PostTitleLabel" runat="server"></asp:Label></h1>
            <p><asp:Label ID="PostContentLabel" runat="server"></asp:Label></p>
            <p>Posted on: <asp:Label ID="PostDateLabel" runat="server"></asp:Label></p>
            <br />
            <br />
            <asp:Button ID="HomePageButton" runat="server" Text="Go to Home Page" OnClick="HomePageButton_Click" />
            <br />
            <br />
            <!-- Comments Section -->
            <h2>Comments</h2>
            <asp:Repeater ID="CommentsRepeater" runat="server">
                <ItemTemplate>
                    <div class="comment">
                        <p><strong><%# Eval("Username") %>:</strong></p>
                        <p><%# Eval("Comment") %></p>
                        <p><i>Posted on: <%# Eval("CreatedAt", "{0:MM/dd/yyyy HH:mm}") %></i></p>
                    </div>
                    <hr />
                </ItemTemplate>
            </asp:Repeater>

            <!-- Leave a Comment Section -->
            <h2>Leave a Comment</h2>
            <asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
            <asp:Button ID="SubmitCommentButton" runat="server" Text="Submit Comment" OnClick="SubmitCommentButton_Click" />
        </div>
    </form>
</body>
</html>
