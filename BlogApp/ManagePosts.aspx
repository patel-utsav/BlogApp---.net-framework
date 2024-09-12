<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagePosts.aspx.cs" Inherits="BlogApp.ManagePosts" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Manage Posts</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Manage Your Blog Posts</h2>

        <asp:GridView ID="gvPosts" runat="server" AutoGenerateColumns="false" OnRowEditing="gvPosts_RowEditing" OnRowDeleting="gvPosts_RowDeleting" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Content" HeaderText="Content" />
                <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
        <br />
        <br />
        <asp:Button ID="HomePageButton" runat="server" Text="Go to Home Page" OnClick="HomePageButton_Click" />
        <br />
        <br />
        <!-- Form to create or update a post -->
        <h3>Create / Update Blog Post</h3>
        <asp:HiddenField ID="hfPostID" runat="server" />
        <label for="txtTitle">Title:</label>
        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br />
        
        <label for="txtContent">Content:</label><br />
        <asp:TextBox ID="txtContent" TextMode="MultiLine" Rows="4" Columns="50" runat="server"></asp:TextBox><br />

        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    </form>
</body>
</html>
