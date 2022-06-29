using LivroAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsumirAPI
{
    public partial class _Default : Page
    {
        private HttpClient client;
        private Uri usuarioUri;

        public _Default()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44327");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getAll();
            }
        }

        private void getAll()
        {
            //chamando a API pela URl
            HttpResponseMessage response = client.GetAsync("api/usuario/").Result;

            if (response.IsSuccessStatusCode)
            {
                usuarioUri = response.Headers.Location;
                var usuarios = response.Content.ReadAsAsync<IEnumerable<Usuario>>().Result;

                Gridview1.DataSource = usuarios;
                Gridview1.DataBind();
            }
            else
            {
                Response.Write(response.StatusCode.ToString() + "-" + response.ReasonPhrase);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Atualizar")
            {
                int _index = int.Parse((string)e.CommandArgument);
                string _chave = Gridview1.DataKeys[_index]["Id"].ToString();
                string _nome = Server.HtmlDecode(Gridview1.Rows[_index].Cells[1].Text);

                HdId.Value = _chave;
                txtNome.Text = _nome;
                txtNome.Focus();
            }

            if (e.CommandName == "Excluir")
            {
                //int _index = int.Parse((string)e.CommandArgument);
                //string _chave = Gridview1.DataKeys[_index]["Id"].ToString();
                //Delete(int.Parse(_chave));

                int _index = Convert.ToInt32(e.CommandArgument);

                Delete(_index);
            }
        }

        private void Delete(int Id)
        {
            HttpResponseMessage response = client.GetAsync("api/usuario/" + Id).Result;
            response = client.DeleteAsync("api/usuario/" + Id).Result;

            if (response.IsSuccessStatusCode)
            {
                usuarioUri = response.Headers.Location;
                Response.Write("Usuario deletado.");

                //string script = "alert(\"Usuário excluido com sucesso!\");";
                //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            else
            {
                Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString());
            }
            getAll();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response = client.GetAsync("api/usuario/?name=" + txtNome.Text).Result;
            if (response.IsSuccessStatusCode)
            {
                usuarioUri = response.Headers.Location;
                var usuarios = response.Content.ReadAsAsync<IEnumerable<Usuario>>().Result;

                Gridview1.DataSource = usuarios;
                Gridview1.DataBind();
            }
            else
            {
                Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase);
            }
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            Update(int.Parse(HdId.Value), txtNome.Text);
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            Enviar(txtNome.Text);
            txtNome.Text = "";
            txtNome.Focus();
        }

        private string Enviar(string name)
        {
            HttpResponseMessage response = client.GetAsync("api/usuario/").Result;

            object data = new
            {
                Nome = name,
            };

            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            response = client.PostAsync("api/usuario/", stringContent).Result;
            string resultContet = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                usuarioUri = response.Headers.Location;
            }
            else
            {
                Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString());
            }
            getAll();
            return resultContet;
        }

        private void Update(int _id, string _nome)
        {
            var usuarios = new Usuario() { Id = _id, Nome = _nome };
            HttpResponseMessage response = client.GetAsync("api/usuario/").Result;
            response = client.PutAsJsonAsync("api/usuario/" + _id, usuarios).Result;

            if (response.IsSuccessStatusCode)
            {
                usuarioUri = response.Headers.Location;
            }
            else
            {
                Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString());
            }
            getAll();
        }

        protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton l = (LinkButton)e.Row.FindControl("LinkButton1");
                l.Attributes.Add("onclick", "javascript:return " +
                "confirm('Tem certeza que deseja Excluir o Usuário? " +
                DataBinder.Eval(e.Row.DataItem, "Id") + "')");
            }
        }
    }
}