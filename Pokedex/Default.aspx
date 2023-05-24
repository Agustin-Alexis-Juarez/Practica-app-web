<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Pokedex.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <h1>Hola!</h1>
    <p>Llegaste a Pokedex Web,tu lugar Pokemon...</p>
    <div class="row row-cols-1 row-cols-md-3 g-4">

        <%--   <%
            foreach (dominio.Pokemon poke in ListaPokemon)
            {
        %>

             <div class="col">
                <div class="card">
                    <img src="<%:poke.UrlImagen %>" class="card-img-top" alt="...">
                 <div class="card-body">
                      <h5 class="card-title"><%:poke.Nombre%></h5>
                      <p class="card-text"><%:poke.Descripcion %></p>
                     <a href="Detalles.aspx?id=<%:poke.Id %>" style="color: aqua;">Ver mas</a>
                    </div>
                </div>
             </div>

        <%  } %>--%>


        <asp:Repeater ID="repRepetidor" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <img src="<%#Eval("UrlImagen") %>" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")%></h5>
                            <p class="card-text"><%#Eval("Descripcion") %></p>
                            <a href="Detalles.aspx?id=<%#Eval("Id")%>" style="color: aqua;">Ver mas</a>
                            <asp:Button Text="Ejemplo" CssClass="btn btn-primary" ID="btnEjemplo" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="pokemonId" OnClick="btnEjemplo_Click" />
                            </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>


    </div>
</asp:Content>
