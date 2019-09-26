<%@ Page Title="Registro de Evaluaciones"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="RegistroEvaluacion.aspx.cs"
    Inherits="PrimerParcialAp2.Registro.RegistroEvaluacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="card text-center bg-light">
            <div class="card-header"><%:Page.Title %></div>

            <div>
                <label for="EvaluacionID" runat="server">ID</label>
                <asp:TextBox ID="EvaluacionID" runat="server" TextMode="Number" placeHolder="ID"></asp:TextBox>
                <asp:Button ID="BuscarButton" class="btn btn-info btn-lg" Text="Buscar" OnClick="BuscarButton_Click" runat="server" />
            </div>
            <div>
                <label for="FechaTextBox" runat="server">Fecha</label>
                <asp:TextBox ID="FechaTextBox" runat="server" TextMode="Date"></asp:TextBox>
            </div>
            <div>
                <label for="EstudianteTextBox" runat="server">Estudiante</label>
                <asp:TextBox ID="EstudianteTextBox" runat="server" placeHolder="Nombre"></asp:TextBox>
            </div>
            <div>
                <label for="CategoriaTextBox" runat="server">Categoria</label>
                <asp:TextBox ID="CategoriaTextBox" runat="server" placeHolder="Categoria"></asp:TextBox>
            </div>
            <div>
                <label for="ValorTextBox" runat="server">Valor</label>
                <asp:TextBox ID="ValorTextBox" runat="server" placeHolder="Valor"></asp:TextBox>
            </div>
            <div>
                <label for="LogradoTextBox" runat="server">Logrado</label>
                <asp:TextBox ID="LogradoTextBox" runat="server" placeHolder="Logrado"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="AgregarButton" Text="Agregar" runat="server" OnClick="AgregarButton_Click" />
            </div>
            <div>
                <div class="row">
                    <div class="table table-responsive col-md-12">
                        <asp:GridView ID="DetalleGridView" runat="server"
                            CssClass="table table-condensed table-bordered table-responsive"
                            GridLines="None" CellPadding="4" ForeColor="#333333" 
                            AllowPaging="true" PageSize="5">
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div>
                <label for="TotalPerdidoTextBox" runat="server">Total Perdido</label>
                <asp:TextBox ID="TotalPerdidoTextBox" AutoPostBack="true" runat="server" ReadOnly="true" placeHolder="Total Perdido"></asp:TextBox>
            </div>
            <div class="panel-footer">
                <div class="text-center">
                    <div class="form-group" display: inline-block>
                        <asp:Button Text="Nuevo" class="btn btn-warning btn-lg" runat="server" ID="NuevoButton" OnClick="NuevoButton_Click" />
                        <asp:Button Text="Guardar" class="btn btn-success btn-lg" runat="server" ID="GuadarButton" OnClick="GuadarButton_Click" />
                        <asp:Button Text="Eliminar" class="btn btn-danger btn-lg" runat="server" ID="EliminarButton" OnClick="EliminarButton_Click" />

                    </div>
                </div>
            </div>
            <asp:Label ID="MensajeLB" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
    </div>

</asp:Content>
