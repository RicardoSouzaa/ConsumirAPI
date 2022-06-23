<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ConsumirAPI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>

        <div class="jumbotron">
            <asp:TextBox ID="txtNome" runat="server" Height="23px" Width="172px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
            &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAtualizar" runat="server" Text="Atualizar" OnClick="btnAtualizar_Click" />
            &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnTodos" runat="server" Text="Todos" />
            &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
            &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" />
            &nbsp;&nbsp;
        <asp:HiddenField ID="HdId" runat="server" />
        </div>

        <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand" Width="668px">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                <asp:ButtonField ButtonType="Link" CommandName="Excluir" Text="Excluir" HeaderText="Excluir" />
                <asp:ButtonField ButtonType="Link" CommandName="Atualizar" Text="Atualizar" HeaderText="Atualizar" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>