using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Xamarin;
using System.Threading.Tasks;
using System.Threading;
using static Android.Widget.GridLayout;
using System.Net;

namespace vendas
{
    [Activity(Label = "at_cadcli")]
    public class at_cadcli : Activity
    {
            Button btngravar;
            Button btncancelar;
            EditText txtnome;
            EditText txtfant;
            EditText txtcnpj;
            EditText txtie;
            EditText txtcpf;
            EditText txtrg;
            EditText txtender;
            EditText txtnro;
            EditText txtbairro;
            EditText txtcida;
            EditText txtcep;
            EditText txttel;
            EditText txtemail;
            EditText txtcont;
            EditText vendedor;

            public static int barray = 0;
            public static ProgressDialog pbar;
            public static string enderecows = "";
            public static string enderecowsporta = "";
            public static bool x = true;
            public static bool xy = true;
            public static int dataRow1 = 0;
            public static int pedido1 = 0;
            public static int clientex;
            public static Int32 xnovo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.layout_cadcli);
            // Create your application here
            btncancelar = FindViewById<Button>(Resource.Id.btncancelar);
            btngravar = FindViewById<Button>(Resource.Id.btngravar);
            txtnome = FindViewById<EditText>(Resource.Id.txtnome);
            txtfant = FindViewById<EditText>(Resource.Id.txtfant);
            txtcnpj = FindViewById<EditText>(Resource.Id.txtcnpj);
            txtie = FindViewById<EditText>(Resource.Id.txtie);
            txtcpf = FindViewById<EditText>(Resource.Id.txtcpf);
            txtrg = FindViewById<EditText>(Resource.Id.txtrg);
            txtender = FindViewById<EditText>(Resource.Id.txtender);
            txtnro = FindViewById<EditText>(Resource.Id.txtnro);
            txtbairro = FindViewById<EditText>(Resource.Id.txtbairro);
            txtcida = FindViewById<EditText>(Resource.Id.txtcida);
            txtcep = FindViewById<EditText>(Resource.Id.txtcep);
            txttel = FindViewById<EditText>(Resource.Id.txttel);
            txtemail = FindViewById<EditText>(Resource.Id.txtemail);
            txtcont = FindViewById<EditText>(Resource.Id.txtconta);
            vendedor = FindViewById<EditText>(Resource.Id.vendedor);


            btncancelar.Click += Btncancelar_Click;
            btngravar.Click += Btngravar_Click;

        }

        private async void Btngravar_Click(object sender, EventArgs e)
        {

            DataTable dados1;
            DataTable dados2;

            DateTime data1 = DateTime.MinValue;

            if (vars.web_local)
            {
                dados1 = Cl_gestor.EXE_QUERY("select*from parametro");
                enderecows = dados1.Rows[0]["endereco"].ToString() + "/insertClie.php";
                enderecowsporta = dados1.Rows[0]["endereco"].ToString();

            }
            else
            {
                dados1 = Cl_gestor.EXE_QUERY("select*from parametro");
                enderecows = dados1.Rows[0]["caminholocal"].ToString() + "/insertClie.php";
                enderecowsporta = dados1.Rows[0]["caminholocal"].ToString();
            }

            List<SQLparametro> parametro1 = new List<SQLparametro>();
            parametro1.Add(new SQLparametro("@data1", data1));
            
            try
            {
              
                if (txtnome.Text.ToString() != string.Empty)
                {

                    FormUrlEncodedContent parametrowe = new FormUrlEncodedContent(new[] {

                        new KeyValuePair<String, String>("nome", txtnome.Text.ToString()),
                        new KeyValuePair<String, String>("fants", txtfant.Text.ToString()),
                        new KeyValuePair<String, String>("cnpj", txtcnpj.Text.ToString()),
                        new KeyValuePair<String, String>("cpf", txtcpf.Text.ToString()),
                        new KeyValuePair<String, String>("ie", txtie.Text.ToString()),
                        new KeyValuePair<String, String>("rg", txtrg.Text.ToString()),
                        new KeyValuePair<String, String>("end", txtender.Text.ToString()),
                        new KeyValuePair<String, String>("nro", txtnro.Text.ToString()),
                        new KeyValuePair<String, String>("bairro", txtbairro.Text.ToString()),
                        new KeyValuePair<String, String>("cida", txtcida.Text.ToString()),
                        new KeyValuePair<String, String>("cep", txtcep.Text.ToString()),
                        new KeyValuePair<String, String>("tel", txttel.Text.ToString()),
                        new KeyValuePair<String, String>("email", txtemail.Text.ToString()),
                        new KeyValuePair<String, String>("cont", txtcont.Text.ToString()),
                        new KeyValuePair<String, String>("vend", vars.numerovendedor.ToString())

                    });                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        

                    HttpClient http = new HttpClient();
                    HttpResponseMessage resposta = http.PostAsync(enderecows, parametrowe).GetAwaiter().GetResult();

                    if(resposta.StatusCode == HttpStatusCode.OK)
                    {
                        x = true;

                        List<tslc001> dois;
                        List<tslc001> users;

                        using (var client = new HttpClient())
                        try
                        {

                            string url = enderecowsporta + "/selectultped.php" + "?par=" + vars.numerovendedor.ToString();
                            var response = await client.GetAsync(url);
                            response.EnsureSuccessStatusCode();

                                var stringResult = await response.Content.ReadAsStringAsync();

                                tslc001[] colecaoPegarid = JsonConvert.DeserializeObject<tslc001[]>(stringResult);

                                vars.numeropedidosalvo = 0;
                                vars.numeropedidosalvo = Convert.ToInt32(colecaoPegarid[0].seqmax);

                                Cl_gestor.NOM_QUERY(" update CLIENTES set numero = " + vars.numeropedidosalvo + "where id_cliente = " + vars.numerocliente);

                                 

                        }catch(HttpRequestException ex)
                        {

                                users = null;

                        }

                    }
                        
                }


            }
            catch (Exception ex)
            {
                
            }
            finally {

                List<SQLparametro> parametro = new List<SQLparametro>();

                //parametro.Add(new SQLparametro("@id_cliente", Cl_gestor.ID_ULTIMO("CLIENTES", "id_cliente")));
                parametro.Add(new SQLparametro("@nome", txtnome.Text.ToString()));
                parametro.Add(new SQLparametro("@fantasia", txtfant.Text.ToString()));
                parametro.Add(new SQLparametro("@cnpj", txtcnpj.Text.ToString()));
                parametro.Add(new SQLparametro("@ie", txtie.Text.ToString()));
                parametro.Add(new SQLparametro("@cpf", txtcpf.Text.ToString()));
                parametro.Add(new SQLparametro("@rg", txtrg.Text.ToString()));
                parametro.Add(new SQLparametro("@ender", txtender.Text.ToString()));
                parametro.Add(new SQLparametro("@nro", txtnro.Text.ToString()));
                parametro.Add(new SQLparametro("@bairro", txtbairro.Text.ToString()));
                parametro.Add(new SQLparametro("@cida", txtcida.Text.ToString()));
                parametro.Add(new SQLparametro("@cep", txtcep.Text.ToString()));
                parametro.Add(new SQLparametro("@tel", txttel.Text.ToString()));
                parametro.Add(new SQLparametro("@email", txtemail.Text.ToString()));
                parametro.Add(new SQLparametro("@cont", txtcont.Text.ToString()));
                parametro.Add(new SQLparametro("@vend", vars.numerovendedor.ToString()));
                parametro.Add(new SQLparametro("@novo", 1));


                Cl_gestor.NOM_QUERY(
                     " insert into CLIENTES (nome,fantasia,cnpj,ie,cpf,rg,endereco,numero,bairro,cidade, " +
                     " cep,telefone1,email,contato,vend,novo) values ( " +
                     "@nome," +
                     "@fantasia," +
                     "@cnpj," +
                     "@ie," +
                     "@cpf," +
                     "@rg," +
                     "@ender," +
                     "@nro," +
                     "@bairro," +
                     "@cida, " +
                     "@cep," +
                     "@tel," +
                     "@email," +
                     "@cont," +
                     "@vend," +
                     "@novo)", parametro);


                this.Finish();
            }
           
        }

                   
    private void Btncancelar_Click(object sender, EventArgs e)
        {
            this.Finish();
        }
    }
}