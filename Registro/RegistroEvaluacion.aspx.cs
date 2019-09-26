using BLL;
using Entidades;
using PrimerParcialAp2.Extensores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrimerParcialAp2.Registro
{
    public partial class RegistroEvaluacion : System.Web.UI.Page
    {

        readonly string KeyViewState = "Evaluacion";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState[KeyViewState] = new Evaluaciones();
            }
        }
        private void Limpiar()
        {
            EvaluacionID.Text = 0.ToString();
            FechaTextBox.Text = DateTime.Now.ToString();
            EstudianteTextBox.Text = string.Empty;
            CategoriaTextBox.Text = string.Empty;
            ValorTextBox.Text = 0.ToString();
            LogradoTextBox.Text = 0.ToString();
            TotalPerdidoTextBox.Text = 0.ToString();
            MensajeLB.Visible = false;
            MensajeLB.Text = string.Empty;
            ViewState[KeyViewState] = new Evaluaciones();
            BindGrid();
        }

        private bool Validar()
        {
            bool paso = true;
            if (string.IsNullOrEmpty(EstudianteTextBox.Text))
                paso = false;
            if (string.IsNullOrEmpty(FechaTextBox.Text))
                paso = false;
            if (DetalleGridView.Rows.Count <= 0)
                paso = false;
            return paso;
        }
        private Evaluaciones LlenaClase()
        {
            Evaluaciones evaluaciones = new Evaluaciones();
            DateTime.TryParse(FechaTextBox.Text, out DateTime result);
            evaluaciones = (Evaluaciones)ViewState[KeyViewState];
            evaluaciones.EvaluacionID = EvaluacionID.Text.ToInt();
            evaluaciones.Fecha = result;
            evaluaciones.Estudiante = EstudianteTextBox.Text;
            evaluaciones.TotalPerdido = TotalPerdidoTextBox.Text.ToDecimal();
            return evaluaciones;
        }
        private void LlenaCampo(Evaluaciones evaluaciones)
        {
            EvaluacionID.Text = evaluaciones.EvaluacionID.ToString();
            FechaTextBox.Text = evaluaciones.Fecha.ToString();
            EstudianteTextBox.Text = evaluaciones.Estudiante.ToString();
            TotalPerdidoTextBox.Text = evaluaciones.TotalPerdido.ToString();
            ViewState[KeyViewState] = evaluaciones;
            BindGrid();
        }
        public bool ExisteEnLaBaseDeDatos()
        {
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();
            return repositorio.Buscar(EvaluacionID.Text.ToInt()) != null; ;
        }
        #region "Eventos"
        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            if (!Validar())
                return;
            bool paso = false;
            Evaluaciones evaluacion = LlenaClase();
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();
            if (evaluacion.EvaluacionID == 0)
                paso = repositorio.Guardar(evaluacion);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    return;
                }
                paso = repositorio.Modificar(evaluacion);
            }
            if (paso)
            {
                Limpiar();
                MensajeLB.Text = "Guardado";
                MensajeLB.CssClass = "alert-success";
                MensajeLB.Visible = true;
            }
            else
            {
                MensajeLB.Text = "No guardo";
                MensajeLB.CssClass = "alert-warning";
                MensajeLB.Visible = true;
            }
        }
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();
            Evaluaciones evaluaciones = repositorio.Buscar(EvaluacionID.Text.ToInt());
            if (evaluaciones != null)
            {
                Limpiar();
                LlenaCampo(evaluaciones);
            }
        }
        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();
            int id = EvaluacionID.Text.ToInt();
            if (!ExisteEnLaBaseDeDatos())
            {
                MensajeLB.Visible = true;
                MensajeLB.Text = "No encontrado";
                MensajeLB.CssClass = "alert-danger";
                return;
            }
            else
            {
                if (repositorio.Eliminar(id))
                {
                    Limpiar();
                    MensajeLB.Visible = true;
                    MensajeLB.Text = "Eliminado";
                    MensajeLB.CssClass = "alert-danger";

                }
            }
        }
        protected void AgregarButton_Click(object sender, EventArgs e)
        {
            Evaluaciones evaluaciones = ((Evaluaciones)ViewState[KeyViewState]);
            decimal Valor = ValorTextBox.Text.ToDecimal();
            decimal Logrado = LogradoTextBox.Text.ToDecimal();
            evaluaciones.DetalleEvaluaciones.Add(new DetalleEvaluaciones(0, evaluaciones.EvaluacionID, CategoriaTextBox.Text, Valor, Logrado, (Valor - Logrado)));
            ViewState[KeyViewState] = evaluaciones;
            BindGrid();
            Calcular();
            CategoriaTextBox.Text = string.Empty;
            ValorTextBox.Text = 0.ToString();
            LogradoTextBox.Text = 0.ToString();
        }
        #endregion

        #region "Metodos"
        private void Calcular()
        {
            decimal TotalPerdido = 0;
            Evaluaciones evaluaciones = ((Evaluaciones)ViewState[KeyViewState]);
            foreach (var item in evaluaciones.DetalleEvaluaciones.ToList())
            {
                TotalPerdido += item.Perdido;
            }
            TotalPerdidoTextBox.Text = TotalPerdido.ToString();
        }
        private void BindGrid()
        {
            Evaluaciones evaluaciones = (Evaluaciones)ViewState[KeyViewState];
            DetalleGridView.DataSource = evaluaciones.DetalleEvaluaciones;
            DetalleGridView.DataBind();
        }
        #endregion
    }
}