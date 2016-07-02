<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CalculatorForm.aspx.cs" Inherits="WebCalculator.CalculatorForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="MainPanel" runat="server">
    <table id="calcTable" style="width:100%; height: 259px;">
        <tr><td colspan="3">
                <asp:TextBox ID="TextResult" Width="162%" Height="50px" runat="server" ReadOnly="True"></asp:TextBox>
            </td></tr>
        <tr>
            <td>
                <asp:Button ID="ButtonMemorySubtration" runat="server" BackColor="#99CCFF" Height="48px" OnClick="ButtonMemoryEdit_Click" TabIndex="1" Text="M-" Width="58px" />
            </td>
            <td>
                <asp:Button ID="ButtonMemoryAddition" runat="server" BackColor="#99CCFF" Height="48px" OnClick="ButtonMemoryEdit_Click" TabIndex="1" Text="M+" Width="58px" />
            </td>
            <td>
                <asp:Button ID="Button18" runat="server" BackColor="#99CCFF" Height="48px" OnClick="ButtonMemoryOut_Click" TabIndex="1" Text="MR" Width="58px" />
            </td>
            <td>
                <asp:Button ID="Button19" runat="server" BackColor="#99CCFF" Height="48px" OnClick="ButtonMemoryReset_Click" TabIndex="1" Text="MC" Width="58px" />
            </td>
            <td colspan="3">
                <asp:Label ID="TextMemory" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 1px">
                <asp:Button ID="Button7" runat="server" Text="7" Height="48px" OnClick="ButtonNumber_Click" Width="58px" />
            </td>
            <td style="width: 1px">
                <asp:Button ID="Button8" runat="server" Text="8" Height="48px" OnClick="ButtonNumber_Click" Width="58px" />
            </td>
            <td style="width: 50px">
                <asp:Button ID="Button9" runat="server" Text="9" Height="48px" OnClick="ButtonNumber_Click" Width="58px" />
            </td>
            <td style="width: 34px">
                <asp:Button ID="Button13" runat="server" Text="D" Height="48px" OnClick="ButtonNumber_Click" Width="58px" />
            </td>
            <td style="width: 1px">
                <asp:Button ID="Button14" runat="server" Text="E" Height="48px" OnClick="ButtonNumber_Click" Width="58px" />
            </td>
            <td style="width: 56px">
                <asp:Button ID="ButtonAddition" runat="server" Text="+" Height="48px" OnClick="ButtonOperation_Click" Width="58px" />
            </td>
            <td>
                <asp:Button ID="ButtonClear" runat="server" Text="C" Height="48px" OnClick="ButtonClear_Click" Width="58px" BackColor="Red" ForeColor="White" />
            </td>
        </tr>
        <tr>
            <td style="width: 1px">
                <asp:Button ID="Button4" runat="server" Text="4" Height="48px" OnClick="ButtonNumber_Click" Width="58px" />
            </td>
            <td style="width: 1px">
                <asp:Button ID="Button5" runat="server" Text="5" Height="48px" OnClick="ButtonNumber_Click" Width="58px" />
            </td>
            <td style="width: 50px">
                <asp:Button ID="Button6" runat="server" Text="6" Height="48px" OnClick="ButtonNumber_Click" Width="58px" />
            </td>
            <td style="width: 34px">
                <asp:Button ID="Button12" runat="server" Text="C" Height="48px" OnClick="ButtonNumber_Click" Width="58px" />
            </td>
            <td style="width: 1px">
                <asp:Button ID="Button15" runat="server" Text="F" Height="48px" OnClick="ButtonNumber_Click" Width="58px" TabIndex="5" />
            </td>
            <td style="width: 56px">
                <asp:Button ID="ButtonSubtraction" runat="server" Text="-" Height="48px" OnClick="ButtonOperation_Click" Width="58px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 1px; height: 50px;">
                <asp:Button ID="Button1" runat="server" Text="1" Height="48px" OnClick="ButtonNumber_Click" Width="58px" TabIndex="2" />
            </td>
            <td style="width: 1px; height: 50px;">
                <asp:Button ID="Button2" runat="server" Text="2" Height="48px" OnClick="ButtonNumber_Click" Width="58px" TabIndex="3" />
            </td>
            <td style="width: 50px; height: 50px;">
                <asp:Button ID="Button3" runat="server" Text="3" Height="48px" OnClick="ButtonNumber_Click" Width="58px" TabIndex="4" />
            </td>
            <td style="width: 34px; height: 50px;">
                <asp:Button ID="Button11" runat="server" Text="B" Height="48px" OnClick="ButtonNumber_Click" Width="58px" />
            </td>
            <td style="height: 50px; width: 1px;">
                &nbsp;</td>
            <td style="width: 56px">
                <asp:Button ID="ButtonMultiplication" runat="server" Text="*" Height="48px" OnClick="ButtonOperation_Click" Width="58px" />
            </td>
            <td>
                <asp:ImageButton ID="ButtonSqrt" runat="server" BackColor="#D8D8D8" Height="48px" ImageUrl="~/Img/square_root2.png" OnClick="buttonSqrt_Click" Width="58px" />
            </td>
        </tr>
        <tr>
            <td style="width: 1px">
                <asp:Button ID="Button0" runat="server" Text="0" Height="48px" OnClick="ButtonNumber_Click" Width="58px" TabIndex="1" />
            </td>
                    <td style="width: 1px">
                <asp:Button ID="ButtonDot" runat="server" Text="," Height="48px" OnClick="buttonDotMode_Click" Width="58px" />
            </td>
                    <td style="width: 50px"></td>
                    <td style="width: 34px">
                <asp:Button ID="Button10" runat="server" Text="A" Height="48px" OnClick="ButtonNumber_Click" Width="58px" />
            </td>
            <td style="width: 1px">
                &nbsp;</td>
            <td style="width: 56px">
                <asp:Button ID="ButtonDivision" runat="server" Text="/" Height="48px" OnClick="ButtonOperation_Click" Width="58px" />
            </td>
            <td>
                <asp:Button ID="ButtonpResult" runat="server" Text="=" Height="48px" OnClick="buttonResult_Click" Width="58px" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
            <asp:Button ID="ButtonPrevNotation" runat="server" Text="-" OnClick="ButtonPrevNotation_Click" />
            <asp:TextBox ID="TextBoxCurrentNotation" runat="server" Width="25px" ReadOnly="True">10</asp:TextBox>
            <asp:Button ID="ButtonNextNotation" runat="server" Text="+" Width="23px" OnClick="ButtonNextNotation_Click" />
        </td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Label ID="RLabel" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
