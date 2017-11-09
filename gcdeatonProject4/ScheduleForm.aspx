<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleForm.aspx.cs" Inherits="gcdeatonProject4.ScheduleForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblSelectCourse" runat="server" Text="Select Course Number"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddlCourseNumbers" runat="server">
            </asp:DropDownList>
            <br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAddCourse" runat="server" OnClick="btnAddCourse_Click" Text="Add Course" />
            <br />
            <br />
            <asp:Button ID="btnSchedule" runat="server" OnClick="btnSchedule_Click" Text="Schedule" />
        </div>
    </form>
</body>
</html>
