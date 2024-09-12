<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="BlogApp.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Sign Up</h2>
            <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <br />
            <asp:Label ID="UsernameLabel" runat="server" Text="Username:"></asp:Label>
            <asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="UsernameRequired" runat="server" 
                ControlToValidate="UsernameTextBox" ErrorMessage="Username is required." 
                ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="EmailLabel" runat="server" Text="Email:"></asp:Label>
            <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                ControlToValidate="EmailTextBox" ErrorMessage="Email is required." 
                ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="EmailFormat" runat="server" 
                ControlToValidate="EmailTextBox" ErrorMessage="Invalid email format." 
                ForeColor="Red" Display="Dynamic" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            <br />
            <asp:Label ID="PasswordLabel" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                ControlToValidate="PasswordTextBox" ErrorMessage="Password is required." 
                ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="ConfirmPasswordLabel" runat="server" Text="Confirm Password:"></asp:Label>
            <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="PasswordCompare" runat="server" 
                ControlToCompare="PasswordTextBox" ControlToValidate="ConfirmPasswordTextBox" 
                ErrorMessage="Passwords do not match." ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
            <br />
            <asp:Button ID="SignUpButton" runat="server" Text="Sign Up" OnClick="SignUpButton_Click" />
        </div>
    </form>
</body>
</html>
