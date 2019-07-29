<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dendoUpdate.aspx.cs" Inherits="WebApplication2.DendoRank.dendoUpdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            ROWIDは編集しないでください<br />
            <a href="../Default.aspx">メニューに戻る</a>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True" AllowSorting="True" AutoGenerateEditButton="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" PageSize="50" DataKeyNames="ROWID" OnRowDeleting="GridView1_RowDeleting" ViewStateMode="Enabled">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="WebApplication2.App_Code.DataAccess" UpdateMethod="EditDept" DeleteMethod="delete" EnableViewState="False" OnDataBinding="ObjectDataSource1_DataBinding" OnSelecting="ObjectDataSource1_Selecting">
                <DeleteParameters>
                    <asp:Parameter Name="ROWID" Size="100" Type="String" />
                    <asp:Parameter Name="NENDO" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="NENDO" Type="String" />
                    <asp:Parameter Name="GENRE" Type="String" />
                    <asp:Parameter Name="HYOKA_NUM" Type="Int32" />
                    <asp:Parameter Name="KYOKU_NAME" Type="String" />
                    <asp:Parameter Name="AUTHOR_NAME" Type="String" />
                    <asp:Parameter Name="HOUR" Type="Int32" />
                    <asp:Parameter Name="MINUTE" Type="Int32" />
                    <asp:Parameter Name="SECOND" Type="Int32" />
                    <asp:Parameter Name="MEMBER" Type="Int32" />
                    <asp:Parameter Name="FILE_SIZE" Type="Int32" />
                    <asp:Parameter Name="ROWID" Type="String" />
                </UpdateParameters>
            </asp:ObjectDataSource>
        </div>
    </form>
    <p>
        ROWIDは編集しないでください</p>
        <a href="../Default.aspx">メニューに戻る</a>
</body>
</html>
