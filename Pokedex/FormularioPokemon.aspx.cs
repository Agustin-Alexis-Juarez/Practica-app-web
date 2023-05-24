using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;
namespace Pokedex
{
    public partial class FormularioPokemon : System.Web.UI.Page
    {
        public bool ConfirmarEliminar { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ConfirmarEliminar = false;
            txtId.Enabled = false;
            try
            { 
                //Config inicial de la pantalla.
                if (!IsPostBack)
                {
                    ElementoNegocio negocio = new ElementoNegocio();
                    List<Elemento> lista = negocio.listar();

                    ddlTipo.DataSource = lista;
                    ddlTipo.DataValueField = "Id";
                    ddlTipo.DataTextField = "Descripcion";
                    ddlTipo.DataBind();

                    ddlDebilidad.DataSource = lista;
                    ddlDebilidad.DataValueField = "Id";
                    ddlDebilidad.DataTextField = "Descripcion";
                    ddlDebilidad.DataBind();
                }
                //config si estamos modificando.
                    string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id!= null && !IsPostBack) //para preguntar si tengo un id en la url.
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    //  List<Pokemon> lista = negocio.listar(id);
                    //Pokemon seleccionado = lista[0];
                    Pokemon seleccionado = (negocio.listar(id))[0];

                    //guardo pokemon seleccionado en la session.
                    Session.Add("pokeseleccionado", seleccionado);

                    //precargar los campos.
                    txtId.Text = id;
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtNumero.Text = seleccionado.Numero.ToString();
                    txtImagenUrl.Text = seleccionado.UrlImagen;

                    ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();
                    ddlDebilidad.SelectedValue = seleccionado.Debilidad.Id.ToString();
                    //para forzar el evento y que cargue la imagen
                    txtImagenUrl_TextChanged(sender, e);

                    //config boton inactivar - activar
                    if (!seleccionado.Activo)
                        btnInactivar.Text = "Reactivar";
                }

            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
                throw; //solo por ahora para ver que falla
                //redireccion a pantalla de error
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            try
            {
                Pokemon nuevo = new Pokemon();
                PokemonNegocio negocio = new PokemonNegocio();

                nuevo.Numero = int.Parse(txtNumero.Text);
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.UrlImagen = txtImagenUrl.Text;

                nuevo.Tipo = new Elemento();
                nuevo.Tipo.Id = int.Parse(ddlTipo.SelectedValue);
                nuevo.Debilidad = new Elemento();
                nuevo.Debilidad.Id = int.Parse(ddlDebilidad.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text);
                    negocio.modificarConSP(nuevo);
                }
                else
                    negocio.agregarConSP(nuevo);

                Response.Redirect("ListaPokemon.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                throw;
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgPokemon.ImageUrl = txtImagenUrl.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmarEliminar = true;
        }

        protected void BtnConfirmarEliminacion_Click(object sender, EventArgs e)
        {
           
            try
            {
                if(chkConfirmarEliminacion.Checked )
                {

                    PokemonNegocio negocio = new PokemonNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("ListaPokemon.aspx");
                }
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                Pokemon seleccionado = (Pokemon)Session["pokeSeleccionado"];
                negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);
                Response.Redirect("ListaPokemon.aspx");
            }
            catch (Exception ex)
            {

                Session.Add("Erro", ex);
            }
        }
    }
}