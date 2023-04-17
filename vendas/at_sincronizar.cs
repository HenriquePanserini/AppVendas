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
using MySql.Data.MySqlClient;
using Xamarin.Essentials.Interfaces;
using Acr.UserDialogs;
using Acr.Support.Android;
using AndroidHUD;
using Superpower;
using Java.Sql;

namespace vendas
{
    [Activity(Label = "at_sincronizar")]
    public class at_sincronizar : Activity
    {
        int w = 0;
        Button btn_vend;
        Button btn_clie;
        Button btn_vend_or;
        Button btn_sup;
        Button cadcli;
        Button btn_confsincro;
        Button btn_vend_paraor;
        Button btn_sincro2;
        int statusBarra;
        
        public static int barray = 0;
        public static Android.App.ProgressDialog pbar;
        public static string enderecows = "";
        public static string enderecowsporta = "";
        public static string enderecowslinha = "";
        public static bool x = true;
        public static bool xy = true;
        public static int dataRow1 = 0;
        public static int pedido1 = 0;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            //layout_sincronizar
            // Create your application here
            SetContentView(Resource.Layout.layout_sincronizar);

            btn_vend = FindViewById<Button>(Resource.Id.btn_vend);
            btn_clie = FindViewById<Button>(Resource.Id.btn_clie);
            btn_vend_or = FindViewById<Button>(Resource.Id.btn_vend_or);
            btn_sup = FindViewById<Button>(Resource.Id.btn_sup);
            cadcli = FindViewById<Button>(Resource.Id.cadcli);
            btn_vend_paraor = FindViewById<Button>(Resource.Id.btn_vend_paraor);
            btn_sincro2 = FindViewById<Button>(Resource.Id.sincro2);

            btn_vend.Click += Btn_vend_Click;
            btn_clie.Click += Btn_clie_Click;
            btn_vend_or.Click += Btn_vend_or_Click;
            btn_sup.Click += Btn_sup_Click; 
            cadcli.Click += Cadcli_Click;
            btn_vend_paraor.Click += Btn_vend_paraor_Click;
            btn_sincro2.Click += Btn_sincro2_Click;

            

        }

        public async void Btn_sincro2_Click(object sender, EventArgs e)
        {


            //LEIA A DOCUMENTAÇÃO E ADICIONE COMENTARIOS 

            // Atribui valor de query a variavel dbpbar
            DataTable dbpbar = Cl_gestor.EXE_QUERY("SELECT * FROM vendas");
            //Le a quantidade de linhas dentro da tabela na variavel dbpbar
            int qtdProgress = dbpbar.Rows.Count;

            //Indicadore display de barra de progresso que funciona com carregamento em design Spin 
            pbar = new Android.App.ProgressDialog(this);
            pbar.SetMessage("sincronizando...");
            pbar.SetCancelable(false);
            pbar.SetProgressStyle(ProgressDialogStyle.Horizontal);
            pbar.Progress = 0;
            pbar.Max = qtdProgress;
            pbar.Show();

            //indicador de barra de status que incia em 0
            int statusBarr = 0;

            /*Query que executa um delete na tabela numero de vendas para iniciar uma nova adição de 
             Valores a ela, e exclusao de tabela do ultimo backup para a realização do backup de uma nova venda*/
            Cl_gestor.NOM_QUERY("DELETE FROM nvenda");
            Cl_gestor.NOM_QUERY("DELETE FROM vendasprd_BKP");
            Cl_gestor.NOM_QUERY("DELETE FROM vendas_BKP");

            DataTable dados;
            DataTable dados1;

            DateTime data1 = DateTime.MinValue;

            try {

                if (vars.web_local)
                {
                    /*Atribui a enderecows o caminho para comunicação com a API SOAP em PHP*/
                    dados = Cl_gestor.EXE_QUERY("SELECT * FROM parametro");
                    enderecows = dados.Rows[0]["endereco"].ToString() + "/insert5.php";
                    enderecowsporta = dados.Rows[0]["endereco"].ToString();
                    enderecowslinha = dados.Rows[0]["endereco"].ToString() + "/insertp5.php";
                }
                else
                {
                    
                    dados = Cl_gestor.EXE_QUERY("SELECT * FROM parametro");
                    enderecows = dados.Rows[0]["caminholocal"].ToString() + "/insert5.php";
                    enderecowsporta = dados.Rows[0]["caminholocal"].ToString();
                    enderecowslinha = dados.Rows[0]["caminholocal"].ToString() + "/insertp5.php";
                }

            } catch (SQLException sqlex)
            {
                Toast.MakeText(this, "Erro ao conectar com servidor", ToastLength.Long);
            }

            /* Atribui a uma variavel do tipo list a os dados da Classe SQLparametros*/
            List<SQLparametro> parametrox = new List<SQLparametro>();
            parametrox.Add(new SQLparametro("@data", DateTime.MinValue));

            int cv1 = 0;

            /* Execução de Query que seleciona os dados da tabela venda e atribui a uma variavel do tipo DataTable*/
            dados = Cl_gestor.EXE_QUERY("SELECT * FROM vendas WHERE datasincro == @data", parametrox);

            Console.WriteLine(" linhas header ------> " + dados.Rows.Count);

            int cv = dados.Rows.Count;
            int totall = 0;
            string codappx = "";
            int seqx = 0;

            for (totall = 0; totall < cv; totall++)
            {

                try
                {

                    DataTable dadosyx;
                    dadosyx = Cl_gestor.EXE_QUERY("SELECT SUM(id_seq) AS id_seqx FROM sincronizar");

                    int clientez = 0;
                    Int32 nnvovo = 1;

                    List<SQLparametro> parametroxy = new List<SQLparametro>();
                    parametroxy.Add(new SQLparametro("@cli", dados.Rows[totall]["id_cliente"]));

                    clientez = Convert.ToInt32(dados.Rows[totall]["id_cliente"]);

                    string c = dados.Rows[totall]["nrovendabco"].ToString();

                    statusBarra += 1;
                    pbar.Progress += statusBarra;
                    Thread.Sleep(1000);

                    if (string.IsNullOrEmpty(dados.Rows[totall]["nrovendabco"].ToString()) || c == "0")
                    {

                        if (clientez != 0)
                        {

                            codappx = Convert.ToString(dados.Rows[totall]["id_pedido"]);

                            FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {

                                new KeyValuePair<String, String>("data", Convert.ToDateTime(dados.Rows[totall]["data"]).ToString("yyyy-MM-dd")),
                                new KeyValuePair<String, String>("codcli", clientez.ToString()),
                                new KeyValuePair<String, String>("bruto", Convert.ToDecimal(dados.Rows[totall]["total"]).ToString().Replace(",",".")),
                                new KeyValuePair<String, String>("desconto", Convert.ToDecimal(dados.Rows[totall]["desconto"]).ToString().Replace(",",".")),
                                new KeyValuePair<String, String>("liquido", Convert.ToDecimal(dados.Rows[totall]["total"]).ToString().Replace(",",".")),
                                new KeyValuePair<String, String>("codven", vars.numerovendedor.ToString()),
                                new KeyValuePair<String, String>("codform", dados.Rows[totall]["id_pgto"].ToString()),
                                new KeyValuePair<String, String>("codapp", dados.Rows[totall]["id_pedido"].ToString()),
                                new KeyValuePair<String, String>("dataincapp", Convert.ToDateTime(dados.Rows[totall]["data"]).ToString("yyyy-MM-dd")),
                                new KeyValuePair<String, String>("datasincapp", DateTime.Now.ToString("yyyy-MM-dd")),
                                new KeyValuePair<String, String>("tipopgto", Convert.ToString(dados.Rows[totall]["tipopagto"])),
                                new KeyValuePair<String, String>("nome", Convert.ToString(dados.Rows[totall]["nome"]))

                    });

                            int pedido = Convert.ToInt32(dados.Rows[totall]["id_pedido"]);

                            Cl_gestor.NOM_QUERY("INSERT INTO nvenda (id_venda) VALUES ( " + pedido + ")");
                            Console.WriteLine("Vendas inseridas -----------------------> " + pedido);

                            HttpClient http = new HttpClient();
                            HttpResponseMessage resposta = http.PostAsync(enderecows, param).GetAwaiter().GetResult();

                            if (resposta.StatusCode == HttpStatusCode.OK)
                            {
                                x = true;

                                List<tslv020> dois;
                                List<tslv020> Users;

                                using (var client = new HttpClient())
                                    try
                                    {

                                        string url = enderecowsporta + "/selectultped.php" + "?par=" + vars.numerovendedor.ToString();

                                        var response = await client.GetAsync(url);
                                        response.EnsureSuccessStatusCode();

                                        var stringResult = await response.Content.ReadAsStringAsync();

                                        tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);

                                        vars.numeropedidosalvo = 0;
                                        vars.numeropedidosalvo = Convert.ToInt32(colecaoPegarid[0].seqmax);

                                        Cl_gestor.NOM_QUERY("UPDATE vendas SET nrovendabco = " + vars.numeropedidosalvo + " WHERE id_pedido = " + pedido);

                                    }
                                    catch (Exception ex)
                                    {

                                        Users = null;
                                    }

                                DataTable dados2 = Cl_gestor.EXE_QUERY("SELECT * FROM vendasprd WHERE id_pedido = " + pedido);
                                DataTable dados3 = Cl_gestor.EXE_QUERY("SELECT * FROM Produtos");

                                int count = Convert.ToInt32(dados2.Rows.Count);

                                try
                                {

                                    for (int ii = 0; ii < count; ii++)
                                    {

                                        if (string.IsNullOrEmpty(dados2.Rows[ii]["nrovendabco"].ToString()) || dados2.Rows[ii]["nrovendabco"].ToString() == "0")
                                        {

                                            FormUrlEncodedContent param3 = new FormUrlEncodedContent(new[] {

                                             new KeyValuePair<String, String>("cupom", vars.numeropedidosalvo.ToString()),
                                             new KeyValuePair<String, String>("codprod", Convert.ToInt32(dados2.Rows[ii]["id_produto"]).ToString()),
                                             new KeyValuePair<String, String>("descricao", Convert.ToString(dados2.Rows[ii]["descricao"])),
                                             new KeyValuePair<String, String>("quant", Convert.ToDecimal(dados2.Rows[ii]["quantidade"]).ToString().Replace(",",".")),
                                             new KeyValuePair<String, String>("total", Convert.ToDecimal(dados2.Rows[ii]["total"]).ToString().Replace(",",".")),
                                             new KeyValuePair<String, String>("desconto", Convert.ToDecimal(dados2.Rows[ii]["desconto"]).ToString().Replace(",",".")),
                                             new KeyValuePair<String, String>("uni", dados3.Rows[ii]["uni"].ToString()),
                                             new KeyValuePair<String, String>("unit", Convert.ToDecimal(dados2.Rows[ii]["precodesconto"]).ToString().Replace(",",".")),
                                             new KeyValuePair<String, String>("preco1",Convert.ToDecimal(dados2.Rows[ii]["preco"]).ToString().Replace(",",".")),
                                             new KeyValuePair<String, String>("codappp1",dados3.Rows[ii]["codbar"].ToString())

                                        });

                                            int seqx1 = Convert.ToInt32(dados2.Rows[ii]["id_pedido"]);

                                            HttpClient http1 = new HttpClient();
                                            HttpResponseMessage response1 = http1.PostAsync(enderecowslinha, param3).GetAwaiter().GetResult();

                                            if (response1.StatusCode == HttpStatusCode.OK)
                                            {

                                                int id_prod = Convert.ToInt32(dados2.Rows[ii]["id_produto"]);

                                                List<SQLparametro> parametro1 = new List<SQLparametro>();

                                                parametro1.Add(new SQLparametro("@pedido", pedido));
                                                parametro1.Add(new SQLparametro("@data", DateTime.Now));

                                                Cl_gestor.NOM_QUERY("UPDATE vendas SET datasincro = @data WHERE id_pedido == @pedido", parametro1);

                                                using (var client = new HttpClient())
                                                    try
                                                    {

                                                        string url = enderecowsporta + "/selectultpedlinha.php" + "?par=" + vars.numeropedidosalvo;
                                                        var response = await client.GetAsync(url);
                                                        response.EnsureSuccessStatusCode();

                                                        var stringResult = await response.Content.ReadAsStringAsync();

                                                        tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);

                                                        vars.numeropedidosalvol = 0;
                                                        vars.numeropedidosalvol = Convert.ToInt32(colecaoPegarid[0].seqmax);

                                                        Cl_gestor.NOM_QUERY("UPDATE vendasprd SET nrovendabco = " + vars.numeropedidosalvol + " WHERE seq = " + seqx1);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Toast.MakeText(this, "Erro na verificação da linha de pedidos", ToastLength.Long).Show();
                                                        StartActivity(typeof(at_menu_inicial));
                                                        Users = null;
                                                    }

                                                vars.nbarra += 5;
                                                int q = 1;

                                            }


                                        }
                                    }

                                    string xpedbco = vars.numeropedidosalvo.ToString();

                                    int contalinha = dados2.Rows.Count;

                                    using (var client = new HttpClient())
                                        try
                                        {

                                            string url = enderecowsporta + "/selectpedido.php" + "?par=" + xpedbco;
                                            var response = await client.GetAsync(url);
                                            response.EnsureSuccessStatusCode();

                                            var stringResult = await response.Content.ReadAsStringAsync();

                                            linped[] colecaoPegarid = JsonConvert.DeserializeObject<linped[]>(stringResult);

                                            vars.maxlinhas = 0;
                                            vars.maxlinhas = Convert.ToInt32(colecaoPegarid[0].totmax);

                                            List<SQLparametro> parametroy = new List<SQLparametro>();
                                            parametroy.Add(new SQLparametro("@datax", DateTime.MinValue));
                                            parametroy.Add(new SQLparametro("@ped1", pedido));

                                            if (contalinha != vars.maxlinhas)
                                            {

                                                Cl_gestor.NOM_QUERY("UPDATE vendas SET datasincro == @datax, nrovendabco = 0 WHERE id_pedido = @ped1", parametroy);
                                                Cl_gestor.NOM_QUERY("UPDATE vendasprd SET nrovendabco = 0 WHERE id_pedido = @ped1", parametroy);

                                                string url2 = enderecowsporta + "/deletapedido.php?par=" + xpedbco;
                                                var response2 = await client.GetAsync(url2);
                                                response2.EnsureSuccessStatusCode();

                                                var stringResult2 = await response2.Content.ReadAsStringAsync();


                                            }
                                            else
                                            {

                                                Cl_gestor.NOM_QUERY("UPDATE vendas SET checar = 1 WHERE id_pedido = @ped1", parametroy);

                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            Toast.MakeText(this, "Erro na conferencia de pedidos" + ex.Message, ToastLength.Long).Show();
                                            Users = null;
                                            StartActivity(typeof(at_menu_inicial));
                                        }


                                }
                                catch (Exception ex)
                                {

                                    int conta = dados2.Rows.Count;

                                    using (var client = new HttpClient())
                                        try
                                        {

                                            string url = enderecowsporta + "/selectcontalinha.php" + "?par=" + vars.numeropedidosalvo;
                                            var response = await client.GetAsync(url);
                                            response.EnsureSuccessStatusCode();

                                            var stringResult = await response.Content.ReadAsStringAsync();
                                            tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);

                                            var contagem = 0;
                                            contagem = Convert.ToInt32(colecaoPegarid[0].seqmax);

                                            if (conta != contagem)
                                            {
                                                string urlx = enderecowsporta + "/excluir" + "?par=" + vars.numeropedidosalvo;
                                                var responsex = await client.GetAsync(urlx);
                                                responsex.EnsureSuccessStatusCode();

                                                var stringResultx = await responsex.Content.ReadAsStringAsync();

                                                var zero = 0;
                                                Cl_gestor.NOM_QUERY("UPDATE vendasprd SET nrovendabco = " + zero + " WHERE seq = " + seqx);

                                                List<SQLparametro> parametro1x = new List<SQLparametro>();

                                                parametro1x.Add(new SQLparametro("@pedido", pedido1));
                                                parametro1x.Add(new SQLparametro("@data", DateTime.MinValue));

                                                Cl_gestor.NOM_QUERY("UPDATE vendas SET datasincro = @data WHERE id_pedido == @pedido", parametro1x);

                                                totall = cv;

                                            }


                                        }
                                        catch (HttpRequestException httpRequestExcpetion)
                                        {

                                            List<SQLparametro> parametro1xx = new List<SQLparametro>();

                                            parametro1xx.Add(new SQLparametro("@pedido", pedido1));
                                            parametro1xx.Add(new SQLparametro("@data", DateTime.MinValue));

                                            Cl_gestor.NOM_QUERY("UPDATEA vendas SET datasincro = @data WHERE id_pedido == @pedido", parametro1xx);

                                            string urlx = enderecowsporta + "/exlcuir.php" + "?par=" + vars.numeropedidosalvo;
                                            var responsex = await client.GetAsync(urlx);
                                            responsex.EnsureSuccessStatusCode();

                                            var stringResultx = await responsex.Content.ReadAsStringAsync();

                                            Toast.MakeText(this, "Erro ao enviar o pedido", ToastLength.Long).Show();
                                            StartActivity(typeof(at_menu_inicial));
                                        }

                                }
                            }

                        }

                        dados1 = Cl_gestor.EXE_QUERY("select * from nvenda ");
                        int ped1 = 0, xi = 0;

                    }

                }
                catch (Exception ex)
                {

                    DataTable dados2 = Cl_gestor.EXE_QUERY("SELECT * FROM vendasprd");
                    int conta = dados2.Rows.Count;

                    using (var client = new HttpClient())
                        try
                        {

                            string url = enderecowsporta + "/selectcontalinha.php" + "?par=" + vars.numeropedidosalvo;
                            var response = await client.GetAsync(url);
                            response.EnsureSuccessStatusCode();

                            var stringResult = await response.Content.ReadAsStringAsync();

                            tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);

                            int contagem = 0;
                            contagem = Convert.ToInt32(colecaoPegarid[0].seqmax);

                            if (conta != contagem)
                            {

                                string urlx = enderecowsporta + "/exclui.php" + "?par=" + vars.numeropedidosalvo;
                                var responsex = await client.GetAsync(urlx);
                                responsex.EnsureSuccessStatusCode();

                                var stringResultx = await response.Content.ReadAsStringAsync();

                                int zero = 0;
                                Cl_gestor.NOM_QUERY("UPDATE vendasprd SET nrovendabco = " + zero + " WHERE seq = " + seqx);

                                List<SQLparametro> parametro1x = new List<SQLparametro>();

                                parametro1x.Add(new SQLparametro("@pedido", pedido1));
                                parametro1x.Add(new SQLparametro("@data", DateTime.MinValue));

                                Cl_gestor.NOM_QUERY("UDPDATE vendas SET  datasincro = @data WHERE id_pedido == @pedido", parametro1x);

                                totall = cv;
                            }

                        }
                        catch (HttpRequestException httpRequestException)
                        {

                            List<SQLparametro> parametro1xx = new List<SQLparametro>();

                            parametro1xx.Add(new SQLparametro("@pedido", pedido1));
                            parametro1xx.Add(new SQLparametro("@data", DateTime.MinValue));

                            Cl_gestor.NOM_QUERY("UPDATE vendas SET datasincro = @data WHERE id_pedido == @pedido", parametro1xx);

                            string urlx = enderecowsporta + "/exclui.php" + "?par=" + vars.numeropedidosalvo;
                            var responsex = await client.GetAsync(urlx);
                            responsex.EnsureSuccessStatusCode();

                            var stringresultx = await responsex.Content.ReadAsStringAsync();

                            Toast.MakeText(this, "Erro ao enviar os pedidos", ToastLength.Long).Show();
                            StartActivity(typeof(at_menu_inicial));


                        }
                }



            }

            pbar.Dismiss();

            }

        
        private void Btn_vend_paraor_Click(object sender, EventArgs e)
        {
            //// copia da vemda para orcamento
            ///

            var zero = 0;
            DataTable dados = Cl_gestor.EXE_QUERY("update vendas set datasincro = '0001-01-01 00:00:00'   ");
            DataTable dadosy = Cl_gestor.EXE_QUERY("update vendas set  nrovendabco = " + zero);
            DataTable dadosx = Cl_gestor.EXE_QUERY("update vendasprd set nrovendabco = " + zero);

        }

        private void Btn_confsincro_Click(object sender, EventArgs e)
        {

            try
            {
               // Toast.MakeText(this, " Aguarde Conferência ", ToastLength.Long).Show();

            cl_webservice.confere_pedidosAsync();

            int maximo = 5000;



            vars.nbarra = 0;

            vars.nbarra = 0;

                // cl_webservice.insereclientes();
                //  cl_webservice.insereprodutos();
#pragma warning disable CS0618 // O tipo ou membro é obsoleto
            Android.App.ProgressDialog pbar = new Android.App.ProgressDialog(this);
#pragma warning restore CS0618 // O tipo ou membro é obsoleto
            pbar.SetCancelable(true);
            pbar.SetMessage("Conferindo Sincronizado....");
            pbar.SetProgressStyle(ProgressDialogStyle.Horizontal);
            pbar.Progress = 0;
            pbar.Max = 3000;
            pbar.Show();
            statusBarra = 0;
            new Thread(new ThreadStart(delegate
            {
                while (barray < maximo)
                {
                    barray += 1;
                    pbar.Progress += barray;
                    //Thread.Sleep(800);
                }
                RunOnUiThread(() => { pbar.SetMessage("Sincronizado..."); });
                RunOnUiThread(() => { Toast.MakeText(this, "Sincronizado com sucesso.", ToastLength.Long).Show(); });
                this.Finish();
            })).Start();

        }
            catch (Exception)
            {
                Toast.MakeText(this, "Erro de Sincronização ", ToastLength.Long).Show();
                //throw;

            }

}

        private void Cadcli_Click(object sender, EventArgs e)
        {

            StartActivity(typeof(at_cadcli));

        }

        private void Btn_sup_Click(object sender, EventArgs e)
        {

            DataTable dados3 = null;
            DataTable dados4 = null;

            AlertDialog.Builder build = new AlertDialog.Builder(this);
            AlertDialog alert = build.Create();

            alert.SetTitle("ALERTA!");
            alert.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            alert.SetMessage("Deseja realmente limpar a lista de vendas?");

            alert.SetButton("Ok", (s, ev) =>
            {

                DataTable dados1 = Cl_gestor.EXE_QUERY("delete from vendasprd ");
                DataTable dados2 = Cl_gestor.EXE_QUERY("delete from vendas ");

                dados3 = Cl_gestor.EXE_QUERY("CREATE TRIGGER IF NOT EXISTS vendasprd_BKP_log AFTER DELETE ON vendasprd" +
                        "                                   BEGIN" +
                        "                                           INSERT INTO vendasprd_BKP VALUES(OLD.id_pedido,OLD.id_produto,OLD.descricao,OLD.quantidade,OLD.preco,OLD.desconto,OLD.precodesconto,OLD.total,OLD.nrovendabco,OLD.seq); " +
                        "                                   END");
                dados4 = Cl_gestor.EXE_QUERY("CREATE TRIGGER  IF NOT EXISTS vendas_BKP_log AFTER DELETE ON vendas" +
                    "                                   BEGIN" +
                    "                                           INSERT INTO vendas_BKP VALUES(OLD.id_pedido,OLD.id_cliente,OLD.nome,OLD.id_pgto,OLD.desconto,OLD.total,OLD.data,OLD.datasincro,OLD.id_formapgto,OLD.tipopagto,OLD.nrovendabco,OLD.checar,OLD.obsped);" +
                    "                                   END");

                Toast.MakeText(this, "Lista de vendas limpa...", ToastLength.Short).Show();

            });

            alert.SetButton2("Cancel", (s, ev) =>
            {

                Toast.MakeText(this, "Cancelado...", ToastLength.Short).Show();

            });

            alert.Show();
        }


        //private void Btn_teste_Click(object sender, EventArgs e)
        //{
        //    sincronizarCadastros();
        //    //sincronizarCliente();

        //}

        private void Btn_clie_Click(object sender, EventArgs e)
        {

            
            sincronizarCadastros();
         

        }


        private void Btn_vend_Click(object sender, EventArgs e)
        {

           
            try
            {

                DataTable dados = Cl_gestor.EXE_QUERY("select id_pedido from vendas");

                


                int maximo = dados.Rows.Count ;



                vars.nbarra = 0;


                //*************************************

                cl_webservice.InsertVendas2d();

               
                vars.nbarra = 0;

                // cl_webservice.insereclientes();
                //  cl_webservice.insereprodutos();
#pragma warning disable CS0618 // O tipo ou membro é obsoleto
                Android.App.ProgressDialog pbar = new Android.App.ProgressDialog(this);
#pragma warning restore CS0618 // O tipo ou membro é obsoleto
                pbar.SetCancelable(true);
                pbar.SetMessage("Sincronizado....");
                pbar.SetProgressStyle(ProgressDialogStyle.Horizontal);
                pbar.Progress = 0;
                pbar.Max = 3000;
                pbar.Show();
                statusBarra = 0;
                new Thread(new ThreadStart(delegate
                {
                    while (barray < maximo)
                    {
                        barray += 1;
                        pbar.Progress += barray;
                        //Thread.Sleep(800);
                    }
                    RunOnUiThread(() => { pbar.SetMessage("Sincronizado..."); });
                    RunOnUiThread(() => { Toast.MakeText(this, "Sincronizado com sucesso.", ToastLength.Long).Show(); });
                    this.Finish();
                })).Start();

            }
            catch (Exception)
            {
                Toast.MakeText(this, "Erro de Sincronização ", ToastLength.Long).Show();
                //throw;

            }

            //DataTable dadosnovo = Cl_gestor.EXE_QUERY("select * from clientes where novo > 0");


            //if (dadosnovo.Rows.Count > 0)
            //{
            //    sincronizarCadastros();
            //}

            
        }



        /// <summary>
        /// ////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>


        private void Btn_vend_or_Click(object sender, EventArgs e)
        {


            try
            {


                DataTable dados = Cl_gestor.EXE_QUERY("select id_pedido from vendas_or");



                int maximo = dados.Rows.Count;



                vars.nbarra = 0;


                //*************************************


                cl_webservice.InsertVendas_or();

                vars.nbarra = 0;

                // cl_webservice.insereclientes();
                //  cl_webservice.insereprodutos();
#pragma warning disable CS0618 // O tipo ou membro é obsoleto
                Android.App.ProgressDialog pbar = new Android.App.ProgressDialog(this);
#pragma warning restore CS0618 // O tipo ou membro é obsoleto
                pbar.SetCancelable(true);
                pbar.SetMessage("Sincronizado....");
                pbar.SetProgressStyle(ProgressDialogStyle.Horizontal);
                pbar.Progress = 0;
                pbar.Max = 3000;
                pbar.Show();
                statusBarra = 0;
                new Thread(new ThreadStart(delegate
                {
                    while (barray < maximo)
                    {
                        barray += 1;
                        pbar.Progress += barray;
                        Thread.Sleep(400);
                    }
                    RunOnUiThread(() => { pbar.SetMessage("Sincronizado..."); });
                    RunOnUiThread(() => { Toast.MakeText(this, "Sincronizado com sucesso.", ToastLength.Long).Show(); });
                    this.Finish();
                })).Start();

            }
            catch (Exception)
            {
                Toast.MakeText(this, "Erro de Sincronização ", ToastLength.Long).Show();
                //throw;

            }


        }
// 

//////////////////////////////////////////////////////

        public async System.Threading.Tasks.Task  sincronizarCadastros()
        {
            
            
            DataTable dados;
#pragma warning disable CS0618 // O tipo ou membro é obsoleto
            Android.App.ProgressDialog pbar = new Android.App.ProgressDialog(this);
#pragma warning restore CS0618 // O tipo ou membro é obsoleto

            Cl_gestor.InicioAplicacaosync();
            pbar.SetCancelable(false);
            pbar.SetMessage("Sincronizado Cadastros...");
            pbar.SetProgressStyle(ProgressDialogStyle.Spinner);
            pbar.Progress = 0;
            pbar.Show();
     
            dados = Cl_gestor.EXE_QUERY("select * from parametro");
            string enderecowsporta = dados.Rows[0]["endereco"].ToString();

            int aaaa = 0;

            try
            {
                List<cl_prod> dois;
                List<cl_prod> Users;
                using (var client = new HttpClient())
                    try
                    {
                        string url = enderecowsporta + "/selectpro.php";
                        var response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        var stringResult = await response.Content.ReadAsStringAsync();
                        //Users = JsonConvert.DeserializeObject<List<nomes>>(stringResult.ToString());

                        //   Users = JsonConvert.DeserializeObject<nomes>(stringResult.ToString());
                        //  var post = JsonConvert.DeserializeObject<nomes>(stringResult.ToString());
                        Cl_gestor.NOM_QUERY("Delete from produtos ");

                        // at_menu_inicial.mensagem("Produtos...........");

                        cl_prod[] colecaoPegarid = JsonConvert.DeserializeObject<cl_prod[]>(stringResult);

                        int i = colecaoPegarid.Length;

                        int xcvt = 5000;

                        for (int xj = 0; xj <= xcvt; xj++)
                        {
                            if (xj < i)
                            {

                                if (colecaoPegarid[xj].descpro != null)
                                {

                                    List<SQLparametro> parametro = new List<SQLparametro>();

                                    string xy = colecaoPegarid[xj].preco1.ToString();
                                    xy = xy.Replace(",", ".");
                                    Console.WriteLine(colecaoPegarid[xj].descpro + " " + xy);
                                    Console.WriteLine(colecaoPegarid[xj].codbar);

                                    parametro.Add(new SQLparametro("@id_produto", colecaoPegarid[xj].codigo));
                                    parametro.Add(new SQLparametro("@descricao", colecaoPegarid[xj].descpro));
                                    parametro.Add(new SQLparametro("@preco", Convert.ToDecimal(colecaoPegarid[xj].preco1.ToString())));
                                    parametro.Add(new SQLparametro("@atualizacao", DateTime.Now));
                                    parametro.Add(new SQLparametro("@codbar", colecaoPegarid[xj].codbar));
                                    parametro.Add(new SQLparametro("@preco2", colecaoPegarid[xj].preco2));
                                    parametro.Add(new SQLparametro("@preco3", colecaoPegarid[xj].preco3));
                                    parametro.Add(new SQLparametro("@preco4", colecaoPegarid[xj].preco4));
                                    parametro.Add(new SQLparametro("@descapp", colecaoPegarid[xj].descapp));
                                    parametro.Add(new SQLparametro("@uni", colecaoPegarid[xj].uni));
                                    parametro.Add(new SQLparametro("@qtd", colecaoPegarid[xj].qtd));
                                    parametro.Add(new SQLparametro("@custo", colecaoPegarid[xj].custo));

                                    //parametro.Add(new SQLparametro("@codbar", Convert.ToDecimal(colecaoPegarid[xj].codbar.ToString())));

                                    int h = 1;

                                    Cl_gestor.NOM_QUERY("insert into produtos (id_produto, descricao, preco, atualizacao, codbar, preco2, preco3, preco4, descapp, uni, qtd, custo) values (" +
                                                        "@id_produto," +
                                                        "@descricao," +
                                                        "@preco," +
                                                        "@atualizacao," +
                                                        "@codbar," +
                                                        "@preco2," +
                                                        "@preco3," +
                                                        "@preco4," +
                                                        "@descapp," +
                                                        "@uni," +
                                                        "@qtd, " +
                                                        "@custo) ", parametro);

                                    // Toast.MakeText(Application.Context, " Sincronização  "+ (colecaoPegarid[at_sincronizar.barray].descpro), ToastLength.Long).Show(

                                }

                                pbar.Progress = xj;

                            }


                            //RunOnUiThread(() => { pbar.SetMessage("Sincronizado..."); });
                            //RunOnUiThread(() => { Toast.MakeText(this, "Sincronizado com sucesso.",ToastLength.Short).Show(); });
                            //this.Finish();
                            pbar.SetMessage("Sincronizando...");

                        }
                    }
                    catch (HttpRequestException httpRequestException)
                    {
                        Users = null;
                    }
                 

                    List<clie> doisc;
                    List<clie> Usersc;
               
                    using (var client = new HttpClient())
                        try
                        {

                           string nrovenx;

                            nrovenx = "?vendedor="+vars.numerovendedor.ToString().Trim();

                            string url = enderecowsporta + "/selectclie.php?par="+ nrovenx;
                            var response = await client.GetAsync(url);
                            response.EnsureSuccessStatusCode();

                            var stringResult = await response.Content.ReadAsStringAsync();


                        //  Cl_gestor.NOM_QUERY("Delete from clientes where novo != 1 ");
                         Cl_gestor.NOM_QUERY("Delete from clientes ");

                        clie[] colecaoPegarid = JsonConvert.DeserializeObject<clie[]>(stringResult);

                            int i = colecaoPegarid.Length - 1;

                            int xcv = 1;
                            string vendx;
                            vendx = "";


                        DataTable dadosx = Cl_gestor.EXE_QUERY("select * from parametro ");  //////+ id_pedido


                        if (dadosx.Rows.Count != 0)
                        {
                            vendx = dadosx.Rows[0]["vendedor"].ToString();
                                                   }
                        else
                        {
                            vendx = "9999999";

                        }

                            if (vars.gerentesn == 0)  /////gerente carregand apenas seus  clientes
                        {

                                                  
                                                        for (int j = 0; j <= i; j++)
                                                        {


                                                            //insert into  usuario values(" +
                                                            //                        "@id_usuario," +

                                                            int cvx = 0;
                                                            string zero = "0";
                                                            if ((colecaoPegarid[j].clinom != null) && ((colecaoPegarid[j].seq004 == vendx) || (vendx == zero) ))
                                                            {
                                                                Console.WriteLine(colecaoPegarid[j].clicod + "   -   " + colecaoPegarid[j].clinom);


                                                                List<SQLparametro> parametro = new List<SQLparametro>();

                                                                parametro.Add(new SQLparametro("@id_cliente", colecaoPegarid[j].clicod));
                                                                parametro.Add(new SQLparametro("@nome", colecaoPegarid[j].clinom));
                                                                parametro.Add(new SQLparametro("@telefone", colecaoPegarid[j].clitel));
                                                                parametro.Add(new SQLparametro("@atualizacao", DateTime.Now));
                                                                parametro.Add(new SQLparametro("@endereco", colecaoPegarid[j].cliend));
                                                                parametro.Add(new SQLparametro("@bairro", colecaoPegarid[j].clibai));
                                                                parametro.Add(new SQLparametro("@cidade", colecaoPegarid[j].clicid));
                                                                parametro.Add(new SQLparametro("@nro", colecaoPegarid[j].numero));
                                                                parametro.Add(new SQLparametro("@cep", colecaoPegarid[j].clicep));
                                                                parametro.Add(new SQLparametro("@telefone1", colecaoPegarid[j].clitel2));
                                                                parametro.Add(new SQLparametro("@telefone2", colecaoPegarid[j].clitel2));
                                                                parametro.Add(new SQLparametro("@celular", colecaoPegarid[j].celular));
                                                                parametro.Add(new SQLparametro("@contato", colecaoPegarid[j].clicon));
                                                                parametro.Add(new SQLparametro("@email", colecaoPegarid[j].email));
                                                                parametro.Add(new SQLparametro("@novo1", cvx));



                                                                Cl_gestor.NOM_QUERY(
                                                                                    "insert into clientes (id_cliente,nome,telefone,atualizacao,bairro,cidade,numero,cep,telefone1,telefone2,celular,contato,endereco,email,novo) values (" +
                                                                                    "@id_cliente," +
                                                                                    "@nome," +
                                                                                    "@telefone," +
                                                                                    "@atualizacao," +
                                                                                    "@bairro," +
                                                                                    "@cidade," +
                                                                                    "@nro," +
                                                                                    "@cep," +
                                                                                    "@telefone1," +
                                                                                    "@telefone2," +
                                                                                    "@celular," +
                                                                                    "@contato," +
                                                                                    "@endereco," +
                                                                                    "@email," +
                                                                                    "@novo1)", parametro);

                                                            }
                                                        }
                                                 
                            }
                            else    /////gerente carrehand todos os clientes
                            {
                                      
                                            for (int j = 0; j <= i; j++)
                                            {


                                                //insert into  usuario values(" +
                                                //                        "@id_usuario," +

                                                int cvx = 0;

                                                if ((colecaoPegarid[j].clinom != null))
                                                {
                                                    Console.WriteLine(colecaoPegarid[j].clicod + "   -   " + colecaoPegarid[j].clinom);


                                                    List<SQLparametro> parametro = new List<SQLparametro>();

                                                    parametro.Add(new SQLparametro("@id_cliente", colecaoPegarid[j].clicod));
                                                    parametro.Add(new SQLparametro("@nome", colecaoPegarid[j].clinom));
                                                    parametro.Add(new SQLparametro("@telefone", colecaoPegarid[j].clitel));
                                                    parametro.Add(new SQLparametro("@atualizacao", DateTime.Now));
                                                    parametro.Add(new SQLparametro("@endereco", colecaoPegarid[j].cliend));
                                                    parametro.Add(new SQLparametro("@bairro", colecaoPegarid[j].clibai));
                                                    parametro.Add(new SQLparametro("@cidade", colecaoPegarid[j].clicid));
                                                    parametro.Add(new SQLparametro("@nro", colecaoPegarid[j].numero));
                                                    parametro.Add(new SQLparametro("@cep", colecaoPegarid[j].clicep));
                                                    parametro.Add(new SQLparametro("@telefone1", colecaoPegarid[j].clitel2));
                                                    parametro.Add(new SQLparametro("@telefone2", colecaoPegarid[j].clitel2));
                                                    parametro.Add(new SQLparametro("@celular", colecaoPegarid[j].celular));
                                                    parametro.Add(new SQLparametro("@contato", colecaoPegarid[j].clicon));
                                                    parametro.Add(new SQLparametro("@email", colecaoPegarid[j].email));
                                                    parametro.Add(new SQLparametro("@novo1", cvx));



                                                    Cl_gestor.NOM_QUERY(
                                                                        "insert into clientes (id_cliente,nome,telefone,atualizacao,bairro,cidade,numero,cep,telefone1,telefone2,celular,contato,endereco,email,novo) values (" +
                                                                        "@id_cliente," +
                                                                        "@nome," +
                                                                        "@telefone," +
                                                                        "@atualizacao," +
                                                                        "@bairro," +
                                                                        "@cidade," +
                                                                        "@nro," +
                                                                        "@cep," +
                                                                        "@telefone1," +
                                                                        "@telefone2," +
                                                                        "@celular," +
                                                                        "@contato," +
                                                                        "@endereco," +
                                                                        "@email," +
                                                                        "@novo1)", parametro);

                                                }
                                            }
                                       
                            }

                        }
                        catch (HttpRequestException httpRequestException)
                        {
                            Usersc = null;
                        }

                    List<clie> doispg;
                    List<clie> Userspg;
                    using (var client = new HttpClient())
                        try
                        {
                            string url = enderecowsporta + "/selectpgto.php";
                            var response = await client.GetAsync(url);
                            response.EnsureSuccessStatusCode();

                            var stringResult = await response.Content.ReadAsStringAsync();
                            //Users = JsonConvert.DeserializeObject<List<nomes>>(stringResult.ToString());

                            //   Users = JsonConvert.DeserializeObject<nomes>(stringResult.ToString());
                            //  var post = JsonConvert.DeserializeObject<nomes>(stringResult.ToString());
                            Cl_gestor.NOM_QUERY("Delete from pagamento ");

                            //at_menu_inicial.mensagem("Formas de Pamentos............");


                            cl_pgto[] colecaoPegarid = JsonConvert.DeserializeObject<cl_pgto[]>(stringResult);

                            int id = colecaoPegarid.Length - 1;

                            int xcv = 1;


                           
                                for (int jd = 0; jd <= id; jd++)
                                {
                                    if (colecaoPegarid[jd].descricao != null)
                                    {


                                        List<SQLparametro> parametro = new List<SQLparametro>();

                                        Console.WriteLine(colecaoPegarid[jd].descricao);

                                        parametro.Add(new SQLparametro("@id_pagamento", colecaoPegarid[jd].codigo));
                                        parametro.Add(new SQLparametro("@descricao", colecaoPegarid[jd].descricao));
                                        parametro.Add(new SQLparametro("@dias", 0));


                                        Cl_gestor.NOM_QUERY(
                                                            "insert into pagamento (id_pagamento, descricao) values (" +
                                                            "@id_pagamento," +
                                                            "@descricao);", parametro);
                    }
                                }
                            

                        }
                        catch (HttpRequestException httpRequestException)
                        {
                            Userspg = null;
                        }

                List<clie> doisusu;
                    List<clie> Usersusu;
                    using (var client = new HttpClient())
                        try
                        {
                            int x = 0;

                            string url = enderecowsporta + "/selectusu.php";
                            var response = await client.GetAsync(url);
                            response.EnsureSuccessStatusCode();

                            var stringResult = await response.Content.ReadAsStringAsync();
                            //Users = JsonConvert.DeserializeObject<List<nomes>>(stringResult.ToString());

                            //   Users = JsonConvert.DeserializeObject<nomes>(stringResult.ToString());
                            //  var post = JsonConvert.DeserializeObject<nomes>(stringResult.ToString());
                            Cl_gestor.NOM_QUERY("Delete from usuario ");

                            //at_menu_inicial.mensagem("Formas de Pamentos............");


                            cl_usu[] colecaoPegarid = JsonConvert.DeserializeObject<cl_usu[]>(stringResult);

                            int ix = colecaoPegarid.Length - 1;

                            int xcv = 1;

                        
                            for (int jx = 0; jx <= ix; jx++)
                            {
                                if (colecaoPegarid[jx].nome != null)
                                {


                                    List<SQLparametro> parametro = new List<SQLparametro>();

                                    Console.WriteLine(colecaoPegarid[jx].nome);

                                    parametro.Add(new SQLparametro("@id_usuario", colecaoPegarid[jx].codigo));
                                    parametro.Add(new SQLparametro("@nome", colecaoPegarid[jx].nome));
                                    parametro.Add(new SQLparametro("@senha", colecaoPegarid[jx].senha));
                                    parametro.Add(new SQLparametro("@descvendas", colecaoPegarid[jx].descvendas));
                                    parametro.Add(new SQLparametro("@gerente", colecaoPegarid[jx].gerente));

                                    Cl_gestor.NOM_QUERY(
                                                        "insert into  usuario (id_usuario,nome,senha,descvendas,gerente) values (" +
                                                        "@id_usuario," +
                                                        "@nome," +
                                                        "@senha," +
                                                        "@descvendas," +
                                                        "@gerente)", parametro);

                                }
                            }

                            //RunOnUiThread(() => { pbar.SetMessage("Sincronizado..."); });
                            //RunOnUiThread(() => { Toast.MakeText(this, "Sincronizado com sucesso.", ToastLength.Long).Show(); });
                            //this.Finish();


                        }
                        catch (HttpRequestException httpRequestException)
                        {
                            Usersusu = null;
                        }


                //********************************************************************************************/

                   using (var client = new HttpClient())
                    try
                    {
                        int x = 0;

                        string url = enderecowsporta + "/selecttipo.php";
                        var response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        var stringResult = await response.Content.ReadAsStringAsync();
                        //Users = JsonConvert.DeserializeObject<List<nomes>>(stringResult.ToString());

                        //   Users = JsonConvert.DeserializeObject<nomes>(stringResult.ToString());
                        //  var post = JsonConvert.DeserializeObject<nomes>(stringResult.ToString());
                        Cl_gestor.NOM_QUERY("Delete from tipopgto ");

                        //at_menu_inicial.mensagem("Formas de Pamentos............");


                        tipopgto[] colecaoPegarid = JsonConvert.DeserializeObject<tipopgto[]>(stringResult);

                        int ixf = colecaoPegarid.Length - 1;
                        if (ixf > 0)
                        {
                            int xcv = 1;

                            
                                for (int jxh = 0; jxh <= ixf; jxh++)
                                {
                                    if (colecaoPegarid[jxh].descricao != null)
                                    {


                                        List<SQLparametro> parametro = new List<SQLparametro>();

                                        Console.WriteLine(colecaoPegarid[jxh].descricao);

                                        parametro.Add(new SQLparametro("@id_tipopgto", colecaoPegarid[jxh].seq017));
                                        parametro.Add(new SQLparametro("@descricao", colecaoPegarid[jxh].descricao));


                                        Cl_gestor.NOM_QUERY(
                                                            "insert into  tipopgto values (" +
                                                            "@id_tipopgto," +
                                                            "@descricao)", parametro);


                                    }
                                }

                                //RunOnUiThread(() => { pbar.SetMessage("Sincronizado..."); });
                                //RunOnUiThread(() => { Toast.MakeText(this, "Sincronizado com sucesso.", ToastLength.Long).Show(); });
                                //this.Finish();
                           
                        }
                        else
                        {
                            List<SQLparametro> parametro = new List<SQLparametro>();

                            parametro.Add(new SQLparametro("@id_tipopgto", 1));
                            parametro.Add(new SQLparametro("@descricao", "A Vista"));


                            Cl_gestor.NOM_QUERY(
                                                "insert into  tipopgto values (" +
                                                "@id_tipopgto," +
                                                "@descricao)", parametro);


                        }

                    }
                    catch (HttpRequestException httpRequestException)
                    {
                        Usersusu = null;
                    }



                //*********************************************************************************************/


                 }
                catch (HttpRequestException httpRequestException)
                {

                    throw;
                }

            
            pbar.Dismiss();

        }


        // copia da vemda para orcamento

             
    }
}      