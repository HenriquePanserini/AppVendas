using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Data;
using vendas;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Threading;
using MySql.Data.MySqlClient;
using static Android.Bluetooth.BluetoothClass;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Threading;


namespace vendas
{
    public static class cl_webservice
    {
        public static string enderecows = "";
        public static string enderecowsporta = "";

        public static string enderecowslinha = "";
        public static bool x = true;
        public static bool xy = true;
        public static int dataRow1 = 0;
        public static int pedido1 = 0;
        public static object Forms { get; private set; }

        public static async Task<bool> InsertVendas()
        {


        
            //throw;

            //DataTable dadosnovo = Cl_gestor.EXE_QUERY("select * from clientes where novo = 1");

            Cl_gestor.NOM_QUERY("delete from nvenda ");

            DataTable dados;
            DataTable dados1;

            DateTime data1 = DateTime.MinValue;
           

            if (vars.web_local)
            {
                dados = Cl_gestor.EXE_QUERY("select * from parametro");
                enderecows = dados.Rows[0]["endereco"].ToString() + "/insert5.php";
                enderecowsporta = dados.Rows[0]["endereco"].ToString();
                enderecowslinha = dados.Rows[0]["endereco"].ToString() + "/insertp5.php";
            }
            else
            {
                dados = Cl_gestor.EXE_QUERY("select * from parametro");
                enderecows = dados.Rows[0]["caminholocal"].ToString() + "/insert5.php";
                enderecowsporta = dados.Rows[0]["caminholocal"].ToString();
                enderecowslinha = dados.Rows[0]["caminholocal"].ToString() + "/insertp5.php";

            }

            
            List<SQLparametro> parametrox = new List<SQLparametro>();
            parametrox.Add(new SQLparametro("@datax", DateTime.MinValue));

            int cv1 = 0;
            // dados = Cl_gestor.EXE_QUERY("select *,clientes.novo from vendas inner join clientes on vendas.id_cliente = clientes.id_cliente where  datasincro == @datax ", parametrox);
            dados = Cl_gestor.EXE_QUERY(" select * from vendas where  datasincro == @datax ", parametrox);

            // at_menu_inicial.mensagem( "Vendas............");

            Console.WriteLine(" linhas header----> " + dados.Rows.Count);

            int cv = dados.Rows.Count;
            int totall = 0;
            string codappx = "";
            int seqx = 0;
                //at_sincronizar at_Sincronizar = new at_sincronizar();


            for (totall = 0; totall < cv; totall++)
            {


                // colocar contador de sincronizacao,
                DataTable dadosyx;
                dadosyx = Cl_gestor.EXE_QUERY(" select sum(id_seq) as id_seqx from sincronizar ");

                // if dadosyx.Rows[0]["id_seqx"].ToString()) == "0"

                List<SQLparametro> parametroxy = new List<SQLparametro>();
                parametroxy.Add(new SQLparametro("@cli", dados.Rows[totall]["id_cliente"]));

                int clientez = Convert.ToInt32(dados.Rows[totall]["id_cliente"]);

                string c = dados.Rows[totall]["nrovendabco"].ToString();

                if (String.IsNullOrEmpty(dados.Rows[totall]["nrovendabco"].ToString()) || c == "0")
                {


                    if (clientez != 0)
                    {
                        ////////inicio pedido


                        /*if (dados.Rows[totall]["id_pedido"].ToString().Trim() == "56")
                        {
                           .

                        }*/

                        codappx = Convert.ToString(dados.Rows[totall]["id_pedido"]);
                        //int count = Convert.ToInt32(dados.Rows[totall][codappx]);

                        FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {
                        
                                                //insert.php
                                                /*new KeyValuePair<String, String>("cupom", vars.numeropedidosalvo.ToString()),
                                                new KeyValuePair<String, String>("data", Convert.ToDateTime(dados.Rows[totall]["data"]).ToString("yyyy-MM-dd")),
                                                new KeyValuePair<String, String>("codcli",clientez.ToString()),
                                                new KeyValuePair<String, String>("bruto", Convert.ToDecimal(dados.Rows[totall]["total"]).ToString().Replace(",",".")),
                                                new KeyValuePair<String, String>("desconto", Convert.ToDecimal(dados.Rows[totall]["desconto"]).ToString().Replace(",",".")),
                                                new KeyValuePair<String, String>("liquido", Convert.ToDecimal(dados.Rows[totall]["total"]).ToString().Replace(",",".")),
                                                new KeyValuePair<String, String>("codven", vars.numerovendedor.ToString()),
                                                new KeyValuePair<String, String>("codform", dados.Rows[totall]["id_pgto"].ToString()),
                                                new KeyValuePair<String, String>("codapp", dados.Rows[totall]["id_pedido"].ToString()),
                                                new KeyValuePair<String, String>("dataincapp", Convert.ToDateTime(dados.Rows[totall]["data"]).ToString("yyyy-MM-dd")),
                                                new KeyValuePair<String, String>("datasincapp",  Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd")),
                                                new KeyValuePair<String, String>("tipotrib", dados.Rows[totall]["tipotrib"].ToString()),
                                                new KeyValuePair<String, String>("parusuario",  vars.parusuario),
                                                new KeyValuePair<String, String>("obsped",  dados.Rows[totall]["obsped"].ToString())*/

                                                //insert(mysql5)
                                                        
                                                        
                                                        new KeyValuePair<String,String>("data", Convert.ToDateTime(dados.Rows[totall]["data"]).ToString("yyyy-MM-dd")),
                                                        new KeyValuePair<String, String>("codcli", clientez.ToString()),
                                                        new KeyValuePair<String,String>("bruto", Convert.ToDecimal(dados.Rows[totall]["total"]).ToString().Replace(",",".")),
                                                        new KeyValuePair<String,String>("desconto", Convert.ToDecimal(dados.Rows[totall]["desconto"]).ToString().Replace(",",".")),
                                                        new KeyValuePair<String, String>("liquido", Convert.ToDecimal(dados.Rows[totall]["total"]).ToString().Replace(",",".")),
                                                        new KeyValuePair<String,String>("codven",vars.numerovendedor.ToString()),
                                                        new KeyValuePair<String,String>("codform",dados.Rows[totall]["id_pgto"].ToString()),
                                                        new KeyValuePair<String,String>("codapp", dados.Rows[totall]["id_pedido"].ToString()),
                                                        new KeyValuePair<String,String>("dataincapp", Convert.ToDateTime(dados.Rows[totall]["data"]).ToString("yyyy-MM-dd")),
                                                        new KeyValuePair<String, String>("datasincapp", DateTime.Now.ToString("yyyy-MM-dd")),
                                                        new KeyValuePair<String, String>("tipopgto", Convert.ToString(dados.Rows[totall]["tipopagto"])),
                                                        new KeyValuePair<String, String>("nome", vars.parusuario.ToString())

                                        }); ;

                        pedido1 = Convert.ToInt32(dados.Rows[totall]["id_pedido"]);

                        Cl_gestor.NOM_QUERY("insert into nvenda  (id_venda)  values ( " + pedido1 + " ) ");
                        Console.WriteLine(" insercao nvenda ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -----------------> " + pedido1);

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

                                    Cl_gestor.NOM_QUERY(" update vendas set nrovendabco = " + vars.numeropedidosalvo + " where id_pedido = " + pedido1);
                                                         

                                }
                                catch (HttpRequestException httpRequestException)
                                {
                                    Users = null;
                                }


                            //  Console.WriteLine(" aqui -----------------> " + vars.numeropedidosalvo);

                            int cc = vars.numeropedidosalvo;

                            dados1 = Cl_gestor.EXE_QUERY("select vendasprd.*,produtos.uni from vendasprd inner join produtos on vendasprd.id_produto =  produtos.id_produto  where id_pedido = " + pedido1);

                            //Console.WriteLine(" linhas---------> " + dados1.Rows.Count);
                            //////////////// pegar numero de itens dados1.Rows.Count
                            ///
                            int nroitenslinha = dados1.Rows.Count;

                            try
                            {


                                for (int ii = 0; ii < dados1.Rows.Count; ii++)
                                {


                                    if (ii == 2)
                                    {
                                        int cccc = 1;
                                    }



                                    if (String.IsNullOrEmpty(dados1.Rows[ii]["nrovendabco"].ToString()) || (dados1.Rows[ii]["nrovendabco"].ToString()) == "0")
                                    {

                                        seqx = Convert.ToInt32(dados1.Rows[ii]["seq"]);

                                        FormUrlEncodedContent param3 = new FormUrlEncodedContent(new[] {
                                                                                                                                     //insertp
                                                                                                                                     /*new KeyValuePair<String, String>("cupom",vars.numeropedidosalvo.ToString()),
                                                                                                                                     new KeyValuePair<String, String>("codprod", dados1.Rows[ii]["id_produto"].ToString()),
                                                                                                                                     new KeyValuePair<String, String>("descricao", dados1.Rows[ii]["descricao"].ToString()),
                                                                                                                                     new KeyValuePair<String, String>("quant", Convert.ToDecimal(dados1.Rows[ii]["quantidade"]).ToString().Replace(",",".")),
                                                                                                                                     new KeyValuePair<String, String>("unit", Convert.ToDecimal(dados1.Rows[ii]["preco"]).ToString().Replace(",",".")),
                                                                                                                                     new KeyValuePair<String, String>("desconto", Convert.ToDecimal(dados1.Rows[ii]["desconto"]).ToString().Replace(",",".")),
                                                                                                                                     new KeyValuePair<String, String>("preco1", Convert.ToDecimal(dados1.Rows[ii]["precodesconto"]).ToString().Replace(",",".")),
                                                                                                                                     new KeyValuePair<String, String>("total", Convert.ToDecimal(dados1.Rows[ii]["total"]).ToString().Replace(",",".")),
                                                                                                                                     new KeyValuePair<String, String>("uni", dados1.Rows[ii]["uni"].ToString()),
                                                                                                                                     new KeyValuePair<String, String>("codapp2", codappx.ToString()),*/


                                                                                                                                     //insertp5
                                                                                                                            
                                                                                                                                     new KeyValuePair<String, String>("cupom", Convert.ToString(vars.numeropedidosalvo)),
                                                                                                                                     new KeyValuePair<String, String>("codprod", Convert.ToInt32(dados1.Rows[ii]["id_produto"]).ToString()),
                                                                                                                                     new KeyValuePair<String, String>("descricao", Convert.ToString(dados1.Rows[ii]["descricao"])),
                                                                                                                                     new KeyValuePair<String, String>("qtd", Convert.ToDecimal(dados1.Rows[ii]["qtd"]).ToString().Replace(",",".")),
                                                                                                                                     new KeyValuePair<String, String>("total", Convert.ToDecimal(dados1.Rows[ii]["total"]).ToString().Replace(",",".")),
                                                                                                                                     new KeyValuePair<String, String>("uni", Convert.ToString(dados1.Rows[ii]["uni"])),
                                                                                                                                     new KeyValuePair<String, String>("desconto", Convert.ToDecimal(dados1.Rows[ii]["desconto"]).ToString().Replace(",",".")),
                                                                                                                                     new KeyValuePair<String, String>("unit", Convert.ToDecimal(dados1.Rows[ii]["preco"]).ToString().Replace(",","."))
                                                                                                                                     
                                                                                                                                     

                                                                //new KeyValuePair<String, String>("preco1", Convert.ToDecimal(dados1.Rows[ii]["precodesconto"]).ToString().Replace(",","."))
                                                                //new KeyValuePair<String, String>("codapp2", codappx),


                                                            }); ;

                                        HttpClient http1 = new HttpClient();
                                        HttpResponseMessage resposta1 = http1.PostAsync(enderecowslinha, param3).GetAwaiter().GetResult();

                                        if (resposta1.StatusCode == HttpStatusCode.OK)
                                        {
                                            int id_prod = Convert.ToInt32(dados1.Rows[ii]["id_produto"]);


                                            List<SQLparametro> parametro1 = new List<SQLparametro>();

                                            parametro1.Add(new SQLparametro("@pedido", pedido1));
                                            parametro1.Add(new SQLparametro("@data", DateTime.Now));

                                            Cl_gestor.NOM_QUERY(" update vendas set datasincro = @data where id_pedido == @pedido ", parametro1);


                                            //  Cl_gestor.NOM_QUERY(" update vendas set datasincro = @data where id_pedido = " + pedido1);
                                            //*******************

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

                                                    Cl_gestor.NOM_QUERY(" update vendasprd set nrovendabco = " + vars.numeropedidosalvol + " where seq = " + seqx);

                                                }
                                                catch (HttpRequestException httpRequestException)
                                                {
                                                    Users = null;
                                                }



                                            //***********************
                                            vars.nbarra += 5;

                                            // aqui muda de pedido
                                            int q = 0;


                                        }

                                    } // fim if se ja existe a linha

                                }

                                //////*********** se nao fez direito sai 
                                ///////verifica se inseriu corretamente 


                                string xpedbco = vars.numeropedidosalvo.ToString();

                                int contalinha = dados1.Rows.Count;

                                using (var client = new HttpClient())
                                    try
                                    {
                                        string url = enderecowsporta + "/selectpedido.php?par=" + xpedbco;
                                        var response = await client.GetAsync(url);
                                        response.EnsureSuccessStatusCode();

                                        var stringResult = await response.Content.ReadAsStringAsync();

                                        linped[] colecaoPegarid = JsonConvert.DeserializeObject<linped[]>(stringResult);


                                        //Console.WriteLine(" aqui --> " + colecaoPegarid[0].seqmax);
                                        vars.maxlinhas = 0;
                                        vars.maxlinhas = Convert.ToInt32(colecaoPegarid[0].totmax);


                                        List<SQLparametro> parametroy = new List<SQLparametro>();
                                        parametroy.Add(new SQLparametro("@datax", DateTime.MinValue));
                                        parametroy.Add(new SQLparametro("@ped1", pedido1));



                                        if (contalinha != vars.maxlinhas)
                                        {


                                            Cl_gestor.NOM_QUERY(" update vendas set  datasincro = @datax,nrovendabco = 0 where id_pedido = @ped1 ", parametroy);
                                            Cl_gestor.NOM_QUERY(" update vendasprd set  nrovendabco = 0 where id_pedido = @ped1 ", parametroy);

                                            string url2 = enderecowsporta + "/deletapedido.php?par=" + xpedbco;
                                            var response2 = await client.GetAsync(url2);
                                            response2.EnsureSuccessStatusCode();

                                            var stringResult2 = await response2.Content.ReadAsStringAsync();



                                        }
                                        else
                                        {
                                            Cl_gestor.NOM_QUERY(" update vendas set  checar = 1 where id_pedido = @ped1 ", parametroy);

                                        }


                                    }
                                    catch (HttpRequestException httpRequestException)
                                    {

                                    }

                                /////acima verifica se fez direito


                                ////*********** se nao fez direito sai 

                                //Cl_gestor.NOM_QUERY("update vendas set id_cliente = 0 where  id_pedido = " + pedido1);                // ************
                                // Cl_gestor.NOM_QUERY("update vendas set datasincro = "+ DateTime.Now + " where id_pedido = " + pedido1);

                            }
                            catch (Exception) // finally
                            {
                                // COMENTEI DIA 12/04
                                /////verificando

                                //int conta = dados1.Rows.Count;



                                //using (var client = new HttpClient())
                                //    try
                                //    {
                                //        string url = enderecowsporta + "/selectcontalinha.php" + "?par=" + vars.numeropedidosalvo;
                                //        var response = await client.GetAsync(url);
                                //        response.EnsureSuccessStatusCode();

                                //        var stringResult = await response.Content.ReadAsStringAsync();

                                //        tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);

                                //        int contagem = 0;
                                //        contagem = Convert.ToInt32(colecaoPegarid[0].seqmax);

                                //        if (conta != contagem)
                                //        {

                                //            string urlx = enderecowsporta + "/exclui.php" + "?par=" + vars.numeropedidosalvo;
                                //            var responsex = await client.GetAsync(urlx);
                                //            responsex.EnsureSuccessStatusCode();

                                //            var stringResultx = await response.Content.ReadAsStringAsync();

                                //            int zero = 0;
                                //            Cl_gestor.NOM_QUERY(" update vendasprd set nrovendabco = " + zero + " where seq = " + seqx);

                                //            List<SQLparametro> parametro1x = new List<SQLparametro>();

                                //            parametro1x.Add(new SQLparametro("@pedido", pedido1));
                                //            parametro1x.Add(new SQLparametro("@data", DateTime.MinValue));

                                //            Cl_gestor.NOM_QUERY(" update vendas set datasincro = @data where id_pedido == @pedido ", parametro1x);



                                //            totall = cv;
                                //        }



                                //    }
                                //    catch (HttpRequestException httpRequestException)
                                //    {
                                //        List<SQLparametro> parametro1xx = new List<SQLparametro>();

                                //        parametro1xx.Add(new SQLparametro("@pedido", pedido1));
                                //        parametro1xx.Add(new SQLparametro("@data", DateTime.MinValue));

                                //        Cl_gestor.NOM_QUERY(" update vendas set datasincro = @data where id_pedido == @pedido ", parametro1xx);

                                //        string urlx = enderecowsporta + "/exclui.php" + "?par=" + vars.numeropedidosalvo;
                                //        var responsex = await client.GetAsync(urlx);
                                //        responsex.EnsureSuccessStatusCode();

                                //        var stringResultx = await responsex.Content.ReadAsStringAsync();

                                //        Users = null;
                                //    }

                                // comentei dia 12/04

                            }
                        }

                    }

                } // fim pergunta se o pedido ja foi incluido
                else
                {

                    //08/06/2022
                    // ////////////////////////
                    //  int ccx = Convert.ToInt32(dados.Rows[totall]["nrovendabco"]);

                    // pedido1 = Convert.ToInt32(dados.Rows[totall]["id_pedido"]);

                    // dados1 = Cl_gestor.EXE_QUERY("select vendasprd.*,produtos.uni from vendasprd inner join produtos on vendasprd.id_produto =  produtos.id_produto  where id_pedido = " + pedido1);

                    //         //Console.WriteLine(" linhas---------> " + dados1.Rows.Count);


                    //         try
                    //         {


                    //             for (int ii = 0; ii < dados1.Rows.Count; ii++)
                    //             {
                    //                     string g = (dados1.Rows[ii]["nrovendabco"].ToString());

                    //                 if ((dados1.Rows[ii]["nrovendabco"].ToString()) == "0" ) //(String.IsNullOrEmpty(dados1.Rows[ii]["nrovendabco"].ToString()) )
                    //                 {

                    //                         seqx = Convert.ToInt32(dados1.Rows[ii]["seq"]);

                    //                         FormUrlEncodedContent param3 = new FormUrlEncodedContent(new[] {

                    //                                                                                                  new KeyValuePair<string, String>("cupom",ccx.ToString()),
                    //                                                                                                  new KeyValuePair<string, String>("codprod", dados1.Rows[ii]["id_produto"].ToString()),
                    //                                                                                                  new KeyValuePair<string, String>("descricao", dados1.Rows[ii]["descricao"].ToString()),
                    //                                                                                                  new KeyValuePair<string, String>("quant", Convert.ToDecimal(dados1.Rows[ii]["quantidade"]).ToString().Replace(",",".")),
                    //                                                                                                  new KeyValuePair<string, String>("unit", Convert.ToDecimal(dados1.Rows[ii]["preco"]).ToString().Replace(",",".")),
                    //                                                                                                  new KeyValuePair<string, String>("desconto", Convert.ToDecimal(dados1.Rows[ii]["desconto"]).ToString().Replace(",",".")),
                    //                                                                                                  new KeyValuePair<string, String>("preco1", Convert.ToDecimal(dados1.Rows[ii]["precodesconto"]).ToString().Replace(",",".")),
                    //                                                                                                  new KeyValuePair<string, String>("total", Convert.ToDecimal(dados1.Rows[ii]["total"]).ToString().Replace(",",".")),
                    //                                                                                                  new KeyValuePair<string, String>("uni", dados1.Rows[ii]["uni"].ToString()),
                    //                                                                                                  new KeyValuePair<string, String>("codapp2", codappx.ToString()),


                    //                                                                                              });

                    //                         HttpClient http1 = new HttpClient();
                    //                         HttpResponseMessage resposta1 = http1.PostAsync(enderecowslinha, param3).GetAwaiter().GetResult();
                    //                         if (resposta1.StatusCode == HttpStatusCode.OK)
                    //                         {
                    //                             int id_prod = Convert.ToInt32(dados1.Rows[ii]["id_produto"]);




                    //                            // Cl_gestor.NOM_QUERY(" update vendas set datasincro = @data where id_pedido = " + pedido1);

                    //                 //*******************

                    //                               using (var client = new HttpClient())
                    //                                 try
                    //                                 {
                    //                                     string url = enderecowsporta + "/selectultpedlinha.php" + "?par=" + ccx;
                    //                                      var response = await client.GetAsync(url);
                    //                                     response.EnsureSuccessStatusCode();

                    //                                     var stringResult = await response.Content.ReadAsStringAsync();

                    //                                     tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);

                    //                                     vars.numeropedidosalvo = 0;
                    //                                     vars.numeropedidosalvo = Convert.ToInt32(colecaoPegarid[0].seqmax);

                    //                                     Cl_gestor.NOM_QUERY(" update vendasprd set nrovendabco = " + vars.numeropedidosalvo + " where seq = " + seqx);
                    //                                    //Cl_gestor.NOM_QUERY("update vendas set datasincro = " + DateTime.Now + " where id_pedido = " + pedido1);


                    //                                     List<SQLparametro> parametro1 = new List<SQLparametro>();

                    //                                         parametro1.Add(new SQLparametro("@pedido", pedido1));
                    //                                         parametro1.Add(new SQLparametro("@data", DateTime.Now));

                    //                                        Cl_gestor.NOM_QUERY(" update vendas set datasincro = @data where id_pedido == @pedido ", parametro1);




                    //                                 }
                    //                     catch (HttpRequestException httpRequestException)
                    //                                 {

                    //                                     throw;
                    //                                 }



                    //                             //***********************
                    //                             vars.nbarra += 5;

                    //                         }

                    //                 } // fim if se ja existe a linha
                    //                 else
                    //                 {
                    //                     List<SQLparametro> parametro1 = new List<SQLparametro>();

                    //                     parametro1.Add(new SQLparametro("@pedido", pedido1));
                    //                     parametro1.Add(new SQLparametro("@data", DateTime.Now));

                    //                     Cl_gestor.NOM_QUERY(" update vendas set datasincro = @data where id_pedido == @pedido ", parametro1);

                    //                 }
                    //             }


                    //             //Cl_gestor.NOM_QUERY("update vendas set id_cliente = 0 where  id_pedido = " + pedido1);                // ************
                    //             // Cl_gestor.NOM_QUERY("update vendas set datasincro = "+ DateTime.Now + " where id_pedido = " + pedido1);

                    //         }
                    //         catch (Exception)
                    //         {

                    //             throw;
                    //         }


                    ///////////////////////////////////
                }

                int e = 1;

                dados1 = Cl_gestor.EXE_QUERY("select * from nvenda ");
                int ped1 = 0, xi = 0;


                //   foreach (DataRow linha in dados1.Rows)
                // {
                //
                //    List<SQLparametro> parametro1 = new List<SQLparametro>();

                //     ped1 = Convert.ToInt32(dados1.Rows[xi]["id_venda"]);
                //
                //    parametro1.Add(new SQLparametro("@pedido", ped1));
                //    parametro1.Add(new SQLparametro("@data", DateTime.Now));


                //    Cl_gestor.NOM_QUERY(" update vendas set datasincro = @data where id_pedido == @pedido ", parametro1);

                //    xi = xi + 1;
                // }
                ///////////////////////////////////////////////////////////////////////
                /// dados relatorio dos pedidos sincronizados



                //////////////////////////////////////////////////////////////////////




                

            }

            return true;
            
        }
     
        

        private static void Finish()
        {
            throw new NotImplementedException();
        }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            public static async System.Threading.Tasks.Task<bool> InsertVendas2d()
            {



                DataTable dados = Cl_gestor.EXE_QUERY("select * from parametro");
                enderecows = dados.Rows[0]["endereco"].ToString();
                enderecowsporta = dados.Rows[0]["endereco"].ToString();
                enderecowslinha = dados.Rows[0]["endereco"].ToString();



                // DataTable dadosc = Cl_gestor.EXE_QUERY("select * from parametro");
                //String servidor = "192.168.0.174"; //dadosc.Rows[0]["endereco"].ToString();
                //String banco =  "max0207";//dadosc.Rows[0]["caminholocal"].ToString();
                //String usuario = "root2"; //dadosc.Rows[0]["usuario"].ToString();
                //String senha =  "12345678";//dadosc.Rows[0]["senha"].ToString();

                String servidor = dados.Rows[0]["smtp"].ToString(); //dadosc.Rows[0]["endereco"].ToString();
                String banco = dados.Rows[0]["banco"].ToString();//dadosc.Rows[0]["caminholocal"].ToString();
                String usuario = dados.Rows[0]["usuario"].ToString(); //dadosc.Rows[0]["usuario"].ToString();
                String senha = dados.Rows[0]["senha"].ToString();//dadosc.Rows[0]["senha"].ToString();
                                                                 //String porta = "330";

                var builder = new MySqlConnectionStringBuilder
                {
                    Server = servidor,
                    Database = banco,
                    UserID = usuario,
                    Password = senha,
                    Port = 3307
                };


                //  Port = 3307

                MySqlConnection conexao = new MySqlConnection(builder.ConnectionString);



                List<SQLparametro> parametrox = new List<SQLparametro>();
                parametrox.Add(new SQLparametro("@datax", DateTime.MinValue));

                int cv1 = 0;
                DataTable dd = Cl_gestor.EXE_QUERY(" select * from vendas where  datasincro == @datax ", parametrox);


                int cv = dd.Rows.Count;
                int totall = 0;
                string codappx = "";
                int seqx = 0;
                int aa = 0;
                string cc = "";
                int sucesso = 0;
                int sucesso2 = 0;
                int sucesso3 = 0;
                int resultx = 1;


                //at_sincronizar at_Sincronizar = new at_sincronizar();

                for (totall = 0; totall < cv; totall++)
                {
                    sucesso = 0;

                    MySqlCommand comandox = new MySqlCommand(" INSERT INTO tslv020web (data,codcli,bruto,desconto,liquido,codven,codform,codapp,dataincapp,datasincapp,tipopgto,micro,obs,nome) VALUES ( " +
                          "@datax,@codclix,@brutox,@descontox,@liquidox,@codvenx,@codformx,@codappx,@dataincappx,@datasincappx,@tipopgtox,@parusuariox,@obspedx,@nomex)", conexao);


                    pedido1 = Convert.ToInt32(dd.Rows[totall]["id_pedido"]);

                    comandox.Parameters.AddWithValue("@datax", DateTime.Today);// &vend2.data
                    comandox.Parameters.AddWithValue("@codclix", dd.Rows[totall]["id_cliente"].ToString());
                    string totalxs = Math.Round(Convert.ToDecimal(dd.Rows[totall]["total"]), 2).ToString().Replace(",", ".");
                    comandox.Parameters.AddWithValue("@brutox", totalxs);
                    string totalxs1 = Math.Round(Convert.ToDecimal(dd.Rows[totall]["desconto"]), 2).ToString().Replace(",", ".");
                    comandox.Parameters.AddWithValue("@descontox", totalxs1);

                    string totalxs1a = Math.Round(Convert.ToDecimal(dd.Rows[totall]["total"]), 2).ToString().Replace(",", ".");
                    comandox.Parameters.AddWithValue("@liquidox", totalxs1a);

                    comandox.Parameters.AddWithValue("@codvenx", vars.numerovendedor.ToString());
                    comandox.Parameters.AddWithValue("@codformx", dd.Rows[totall]["id_pgto"].ToString());
                    comandox.Parameters.AddWithValue("@codappx", dd.Rows[totall]["id_pedido"].ToString());
                    comandox.Parameters.AddWithValue("@dataincappx", DateTime.Today);// &vend2.data
                    comandox.Parameters.AddWithValue("@datasincappx", DateTime.Today);// &vend2.data
                    comandox.Parameters.AddWithValue("@tipopgtox", dd.Rows[totall]["id_formapgto"].ToString());
                    comandox.Parameters.AddWithValue("@parusuariox", aa.ToString());
                    comandox.Parameters.AddWithValue("@obspedx", dd.Rows[totall]["obsped"].ToString());
                    comandox.Parameters.AddWithValue("@nomex", dd.Rows[totall]["nome"].ToString());

                    try
                    {
                        conexao.Open();
                        comandox.ExecuteNonQuery();
                        conexao.Close();
                        sucesso = 1;
                    }
                    catch (System.Exception e)
                    {
                        sucesso = 0;
                        string aaaa = e.Message.ToString();
                    }


                    if (sucesso == resultx)
                    {
                        sucesso2 = 0;
                        MySqlCommand comandoxs = new MySqlCommand(" select max(seq) as seq1 from tslv020web ", conexao);
                        int xxx = 0;

                        try
                        {
                            conexao.Open();
                            xxx = int.Parse(comandoxs.ExecuteScalar() + "");
                            conexao.Close();
                            sucesso2 = 1;
                        }
                        catch (System.Exception e)
                        {
                            string aaaa = e.Message.ToString();
                            sucesso2 = 0;
                        }



                        DataTable dados1 = Cl_gestor.EXE_QUERY("select vendasprd.*,produtos.uni,produtos.codbar,produtos.descricao as dex from vendasprd inner join produtos on vendasprd.id_produto =  produtos.id_produto  where id_pedido = " + pedido1);
                        int nroitenslinha = dados1.Rows.Count;

                        if (xxx > 0)
                        {
                            sucesso3 = 0;
                            try
                            {


                                for (int ii = 0; ii < dados1.Rows.Count; ii++)
                                {

                                    MySqlCommand comandoxa = new MySqlCommand("INSERT INTO tslv021web (cupom,codprod,descricao,quant,unit,desconto,bkppreco,codappl,unidade,preco1,codbar1) VALUES " +
                                      "(@cupomx,@codprodx,@descricx, @quantx,@preco1x,@descontox,@unitx,@codapp2x,@unix,@preco11,@codbar2)", conexao);

                                    //  MySqlCommand comandoxa = new MySqlCommand("INSERT INTO tslv021web (cupom,codprod,descricao) VALUES " +
                                    //"(@cupomx,@codprodx,@descprox)", conexao);


                                    comandoxa.Parameters.AddWithValue("@cupomx", xxx.ToString());
                                    comandoxa.Parameters.AddWithValue("@codprodx", dados1.Rows[ii]["id_produto"].ToString());
                                    comandoxa.Parameters.AddWithValue("@descricx", dados1.Rows[ii]["dex"].ToString());
                                    comandoxa.Parameters.AddWithValue("@codbar2", dados1.Rows[ii]["codbar"].ToString());

                                    string totax1 = Math.Round(Convert.ToDecimal(dados1.Rows[ii]["quantidade"]), 2).ToString().Replace(",", ".");
                                    comandoxa.Parameters.AddWithValue("@quantx", totax1);

                                    string totax12 = Math.Round(Convert.ToDecimal(dados1.Rows[ii]["preco"]), 2).ToString().Replace(",", ".");
                                    comandoxa.Parameters.AddWithValue("@unitx", totax12);
                                    string totax123 = Math.Round(Convert.ToDecimal(dados1.Rows[ii]["desconto"]), 2).ToString().Replace(",", ".");
                                    comandoxa.Parameters.AddWithValue("@descontox", totax123);
                                    string totax1234 = Math.Round(Convert.ToDecimal(dados1.Rows[ii]["precodesconto"]), 2).ToString().Replace(",", ".");
                                    comandoxa.Parameters.AddWithValue("@preco1x", totax1234);
                                    string totax12345 = Math.Round(Convert.ToDecimal(dados1.Rows[ii]["preco"]), 2).ToString().Replace(",", ".");
                                    comandoxa.Parameters.AddWithValue("@preco11", totax12345);


                                    comandoxa.Parameters.AddWithValue("@codapp2x", xxx.ToString());
                                    comandoxa.Parameters.AddWithValue("@unix", dados1.Rows[ii]["uni"].ToString());


                                    try
                                    {
                                        conexao.Open();
                                        comandoxa.ExecuteNonQuery();
                                        conexao.Close();
                                        sucesso3 = 1;
                                    }
                                    catch (System.Exception e)
                                    {
                                        string aaaa = e.Message.ToString();
                                        sucesso3 = 0;
                                    }

                                }

                                if (sucesso == resultx && sucesso2 == resultx && sucesso3 == resultx)
                                {
                                    List<SQLparametro> parametro1 = new List<SQLparametro>();
                                    parametro1.Add(new SQLparametro("@pedido", pedido1));
                                    parametro1.Add(new SQLparametro("@data", DateTime.Now));
                                    parametro1.Add(new SQLparametro("@ped", xxx.ToString()));

                                    Cl_gestor.NOM_QUERY(" update vendas set datasincro = @data,nrovendabco = @ped  where id_pedido == @pedido ", parametro1);
                                }

                            }
                            catch (Exception e)
                            {
                                string aaaa = e.Message.ToString();
                            }

                        }
                    }

                }

                ///checando
                int sus11 = 0;


                List<SQLparametro> parametroxs = new List<SQLparametro>();
                parametroxs.Add(new SQLparametro("@checar1", 0));

                int cv1s = 0;
                DataTable dds = Cl_gestor.EXE_QUERY(" select * from vendas where  checar == @checar1 ", parametroxs);

                int zero = 0;
                int cvs = dds.Rows.Count;
                int pedido2 = 0;
                int sus = 0;

                for (totall = 0; totall < cvs; totall++)
                {

                    pedido2 = Convert.ToInt32(dds.Rows[totall]["nrovendabco"]);


                    MySqlCommand comandoxsa = new MySqlCommand(" select count(seq) as seq1 from tslv020web where seq = @seqw", conexao);

                    comandoxsa.Parameters.AddWithValue("@seqw", pedido2.ToString());
                    int xxxa = 0;
                    int xxxa1 = 0;
                    try
                    {
                        conexao.Open();
                        xxxa = int.Parse(comandoxsa.ExecuteScalar() + "");
                        conexao.Close();

                    }
                    catch (System.Exception e)
                    {
                        string aaaa = e.Message.ToString();
                        xxxa = 0;
                    }

                    if (xxxa > zero)
                    {
                        pedido1 = Convert.ToInt32(dd.Rows[totall]["id_pedido"]);
                        DataTable dados1 = Cl_gestor.EXE_QUERY("select id_pedido from vendasprd where id_pedido = " + pedido1);
                        int nroitenslinha = dados1.Rows.Count;


                        MySqlCommand comand1 = new MySqlCommand(" select count(cupom) as cup1 from tslv021web where cupom = @seqw1", conexao);
                        comand1.Parameters.AddWithValue("@seqw1", pedido2.ToString());

                        try
                        {
                            conexao.Open();
                            xxxa1 = int.Parse(comand1.ExecuteScalar() + "");
                            conexao.Close();

                        }
                        catch (System.Exception e)
                        {
                            string aaaa = e.Message.ToString();
                            xxxa1 = 0;

                        }

                        if (nroitenslinha != xxxa1)
                        {
                            MySqlCommand comand1x = new MySqlCommand(" update tslv021web set exclui = @exclui1 where cupom = @seqw1", conexao);
                            comand1x.Parameters.AddWithValue("@seqw1", pedido2.ToString());
                            comand1x.Parameters.AddWithValue("@exclui1", "S");

                            try
                            {
                                conexao.Open();
                                comand1x.ExecuteNonQuery();
                                conexao.Close();

                            }
                            catch (System.Exception e)
                            {
                                string aaaa = e.Message.ToString();
                            }
                            MySqlCommand comand1x1 = new MySqlCommand(" update tslv020web set exclui = @exclui1 where seq = @seqw1", conexao);
                            comand1x1.Parameters.AddWithValue("@seqw1", pedido2.ToString());
                            comand1x1.Parameters.AddWithValue("@exclui1", "S");

                            try
                            {
                                conexao.Open();
                                comand1x1.ExecuteNonQuery();
                                conexao.Close();
                            }
                            catch (System.Exception e)
                            {
                                string aaaa = e.Message.ToString();
                            }

                            List<SQLparametro> parametro1a = new List<SQLparametro>();
                            parametro1a.Add(new SQLparametro("@pedido", pedido2));
                            parametro1a.Add(new SQLparametro("@zero", 0));

                            Cl_gestor.NOM_QUERY(" update vendas set nrovendabco = @zero,datasincro = '0001-01-01 00:00:00'   where nrovendabco == @pedido ", parametro1a);


                        }
                        else  // tudo certo 
                        {
                            List<SQLparametro> parametro1a = new List<SQLparametro>();
                            parametro1a.Add(new SQLparametro("@pedido", pedido2));
                            parametro1a.Add(new SQLparametro("@chk", 1));

                            Cl_gestor.NOM_QUERY(" update vendas set checar = @chk  where nrovendabco == @pedido ", parametro1a);

                            ///// 
                            ///no orcamento


                            DataTable dadosy;


                            dadosy = Cl_gestor.EXE_QUERY(" select * from vendas where  nrovendabco == @pedido ", parametro1a);


                            if (dadosy.Rows.Count != 0)
                            {

                                //vars.nropgto = Convert.ToInt32(dadospg.Rows[0]["id_pagamento"]);


                                List<SQLparametro> parametro1 = new List<SQLparametro>();
                                parametro1.Add(new SQLparametro("@id_pedido", Cl_gestor.ID_DISPONIVEL("vendas_or", "id_pedido")));
                                parametro1.Add(new SQLparametro("@id_cliente", dadosy.Rows[0]["id_cliente"]));
                                parametro1.Add(new SQLparametro("@nome", dadosy.Rows[0]["nome"]));
                                parametro1.Add(new SQLparametro("@id_pgto", dadosy.Rows[0]["id_pgto"]));
                                parametro1.Add(new SQLparametro("@desconto", dadosy.Rows[0]["desconto"])); //descontox
                                parametro1.Add(new SQLparametro("@total", dadosy.Rows[0]["total"]));
                                parametro1.Add(new SQLparametro("@data", dadosy.Rows[0]["data"]));
                                parametro1.Add(new SQLparametro("@datasincro", dadosy.Rows[0]["datasincro"]));
                                parametro1.Add(new SQLparametro("@id_formapgto", dadosy.Rows[0]["id_formapgto"]));
                                parametro1.Add(new SQLparametro("@tipopagto", dadosy.Rows[0]["tipopagto"]));

                                int iddpedido = Convert.ToInt32((dadosy.Rows[0]["id_pedido"]));


                                Cl_gestor.NOM_QUERY(
                                "insert into vendas_or (id_pedido,id_cliente,nome,id_pgto,desconto,total,data,datasincro,id_formapgto,tipopagto)values ( " +
                                "@id_pedido," +
                                "@id_cliente," +
                                "@nome," +
                                "@id_pgto ," +
                                "@desconto ," +
                                "@total ," +
                                "@data ," +
                                "@datasincro," +
                                "@id_formapgto," +
                                "@tipopagto)", parametro1);

                                DataTable dadosprod;



                                List<SQLparametro> parametro1axx = new List<SQLparametro>();
                                parametro1a.Add(new SQLparametro("@iddpedidox", iddpedido));
                                dadosprod = Cl_gestor.EXE_QUERY(" select * from vendasprd where  id_pedido == @iddpedidox ", parametro1axx);


                                if (dadosprod.Rows.Count != 0)
                                {


                                    if (dadosprod.Rows.Count != 0)
                                    {

                                        foreach (DataRow linha in dadosprod.Rows)
                                        {




                                            List<SQLparametro> parametrok = new List<SQLparametro>();

                                            parametrok.Add(new SQLparametro("@id_pedido", iddpedido));
                                            parametrok.Add(new SQLparametro("@id_produto", linha["id_produto"]));
                                            parametrok.Add(new SQLparametro("@descricao", linha["descricao"]));
                                            parametrok.Add(new SQLparametro("@quantidade", linha["quantidade"]));
                                            parametrok.Add(new SQLparametro("@preco", linha["preco"]));
                                            parametrok.Add(new SQLparametro("@desconto", linha["desconto"]));
                                            parametrok.Add(new SQLparametro("@precodesconto", linha["precodesconto"]));
                                            parametrok.Add(new SQLparametro("@total", linha["total"]));



                                            Cl_gestor.NOM_QUERY(
                                            "insert into tmp_vendas_prod_or (id_pedido,id_produto,descricao,quantidade,preco,desconto,precodesconto,total) values ( " +
                                            "@id_pedido," +
                                            "@id_produto," +
                                            "@descricao," +
                                            "@quantidade," +
                                            "@preco," +
                                            "@desconto," +
                                            "@precodesconto," +
                                            "@total )", parametrok);
                                        }
                                    }




                                }

                                //////------------------------
                            }
                        }
                    }
                    else
                    {
                        // nao achou a cabeca 
                        MySqlCommand comand1x = new MySqlCommand(" update tslv021web set exclui = @exclui1 where cupom = @seqw1", conexao);
                        comand1x.Parameters.AddWithValue("@seqw1", pedido2.ToString());
                        comand1x.Parameters.AddWithValue("@exclui1", "S");

                        try
                        {
                            conexao.Open();
                            comand1x.ExecuteNonQuery();
                            conexao.Close();

                        }
                        catch (System.Exception e)
                        {
                            string aaaa = e.Message.ToString();
                        }


                        List<SQLparametro> parametro1a = new List<SQLparametro>();
                        parametro1a.Add(new SQLparametro("@pedido", pedido2));
                        parametro1a.Add(new SQLparametro("@zero", 0));

                        Cl_gestor.NOM_QUERY(" update vendas set nrovendabco = @zero,datasincro = '0001-01-01 00:00:00'   where nrovendabco == @pedido ", parametro1a);

                    }



                }
                //checando

                return true;
            }


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            /// //////////////////////////////////////////////////////////////////////////////////

            public static async Task<bool> InsertVendas_or()
            {

                Cl_gestor.NOM_QUERY("delete from nvenda_or");

                DataTable dados;
                DataTable dados1;

                DateTime data1 = DateTime.MinValue;

                if (vars.web_local)
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecows = dados.Rows[0]["endereco"].ToString() + "/insert_or.php";
                    enderecowsporta = dados.Rows[0]["endereco"].ToString();
                    enderecowslinha = dados.Rows[0]["endereco"].ToString() + "/insertp_or.php";
                }
                else
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecows = dados.Rows[0]["caminholocal"].ToString() + "/insert_or.php";
                    enderecowsporta = dados.Rows[0]["caminholocal"].ToString();
                    enderecowslinha = dados.Rows[0]["caminholocal"].ToString() + "/insertp_or.php";

                }


                List<SQLparametro> parametrox = new List<SQLparametro>();
                parametrox.Add(new SQLparametro("@datax", DateTime.MinValue));

                int cv1 = 0;
                dados = Cl_gestor.EXE_QUERY("select * from vendas_or where  datasincro == @datax ", parametrox);




                // at_menu_inicial.mensagem( "Vendas............");

                Console.WriteLine(" linhas header----> " + dados.Rows.Count);


                int cv = dados.Rows.Count;
                int totall = 0;

                //at_sincronizar at_Sincronizar = new at_sincronizar();

                for (totall = 0; totall < cv; totall++)
                {

                    at_sincronizar.barray++;

                    if (Convert.ToInt32(dados.Rows[totall]["id_cliente"]) != 0)
                    {

                        FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {

                                                        new KeyValuePair<string, String>("data", Convert.ToDateTime(dados.Rows[totall]["data"]).ToString("yyyy-MM-dd")),
                                                        new KeyValuePair<string, String>("codcli", dados.Rows[totall]["id_cliente"].ToString()),
                                                        new KeyValuePair<string, String>("bruto", Convert.ToDecimal(dados.Rows[totall]["total"]).ToString().Replace(",",".")),
                                                        new KeyValuePair<string, String>("desconto", Convert.ToDecimal(dados.Rows[totall]["desconto"]).ToString().Replace(",",".")),
                                                        new KeyValuePair<string, String>("liquido", Convert.ToDecimal(dados.Rows[totall]["total"]).ToString().Replace(",",".")),
                                                        new KeyValuePair<string, String>("codven", vars.numerovendedor.ToString()),
                                                        new KeyValuePair<string, String>("codform", dados.Rows[totall]["id_pgto"].ToString()),
                                                        new KeyValuePair<string, String>("codapp", dados.Rows[totall]["id_pedido"].ToString()),
                                                        new KeyValuePair<string, String>("dataincapp", Convert.ToDateTime(dados.Rows[totall]["data"]).ToString("yyyy-MM-dd")),
                                                        new KeyValuePair<string, String>("datasincapp",  Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd")),
                                                        new KeyValuePair<string, String>("tipopgto", dados.Rows[totall]["tipopagto"].ToString())

                                                    });

                        pedido1 = Convert.ToInt32(dados.Rows[totall]["id_pedido"]);

                        Cl_gestor.NOM_QUERY("insert into nvenda_or  (id_venda)  values ( " + pedido1 + " ) ");
                        Console.WriteLine(" insercao nvenda ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -----------------> " + pedido1);

                        HttpClient http = new HttpClient();
                        HttpResponseMessage resposta = http.PostAsync(enderecows, param).GetAwaiter().GetResult();

                        if (resposta.StatusCode == HttpStatusCode.OK)
                        {
                            x = true;


                            //**************************************************************************************************
                            //  busca ultimo registro inserido 
                            //pesquisaultimop();


                            List<tslv020> dois;
                            List<tslv020> Users;
                            using (var client = new HttpClient())
                                try
                                {
                                    string url = enderecowsporta + "/selectultped_or.php";
                                    var response = await client.GetAsync(url);
                                    response.EnsureSuccessStatusCode();

                                    var stringResult = await response.Content.ReadAsStringAsync();

                                    tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);


                                    Console.WriteLine(" aqui --> " + colecaoPegarid[0].seqmax);
                                    vars.numeropedidosalvo_or = 0;
                                    vars.numeropedidosalvo_or = Convert.ToInt32(colecaoPegarid[0].seqmax);


                                }
                                catch (HttpRequestException httpRequestException)
                                {
                                    Users = null;
                                }


                            Console.WriteLine(" aqui -----------------> " + vars.numeropedidosalvo);




                            //**************************************************************************************************
                            //insere as linhas

                            //enderecows = dados.Rows[0]["endereco"].ToString() + "/insertp.php";

                            dados1 = Cl_gestor.EXE_QUERY("select * from vendasprd_or where id_pedido = " + pedido1);
                            Console.WriteLine(" linhas---------> " + dados1.Rows.Count);

                            // at_menu_inicial.mensagem( "Vendas............");

                            try
                            {


                                for (int iix = 0; iix < dados1.Rows.Count; iix++)
                                {
                                    FormUrlEncodedContent param1 = new FormUrlEncodedContent(new[] {

                                                                                                                                                                            new KeyValuePair<string, String>("cupom",vars.numeropedidosalvo_or.ToString()),
                                                                                                                                                                            new KeyValuePair<string, String>("codprod", dados1.Rows[iix]["id_produto"].ToString()),
                                                                                                                                                                            new KeyValuePair<string, String>("descricao", dados1.Rows[iix]["descricao"].ToString()),
                                                                                                                                                                            new KeyValuePair<string, String>("quant", Convert.ToDecimal(dados1.Rows[iix]["quantidade"]).ToString().Replace(",",".")),
                                                                                                                                                                            new KeyValuePair<string, String>("unit", Convert.ToDecimal(dados1.Rows[iix]["preco"]).ToString().Replace(",",".")),
                                                                                                                                                                            new KeyValuePair<string, String>("desconto", Convert.ToDecimal(dados1.Rows[iix]["desconto"]).ToString().Replace(",",".")),
                                                                                                                                                                            new KeyValuePair<string, String>("preco1", Convert.ToDecimal(dados1.Rows[iix]["precodesconto"]).ToString().Replace(",",".")),
                                                                                                                                                                            new KeyValuePair<string, String>("total", Convert.ToDecimal(dados1.Rows[iix]["total"]).ToString().Replace(",","."))


                                                                 });

                                    HttpClient http1 = new HttpClient();
                                    HttpResponseMessage resposta1 = http1.PostAsync(enderecowslinha, param1).GetAwaiter().GetResult();
                                    if (resposta1.StatusCode != HttpStatusCode.OK)
                                    {
                                        int id_prod = Convert.ToInt32(dados1.Rows[iix]["id_produto"]);
                                        //Cl_gestor.NOM_QUERY("update vendasprd set id_produto = 0  where  id_pedido = " + pedido1 + " and id_produto = " + id_prod);
                                        //Cl_gestor.NOM_QUERY("delete from vendasprd where id_produto = " + id_prod);
                                        //xy = true;
                                        //return xy;
                                        vars.nbarra += 5;

                                    }



                                }


                                //Cl_gestor.NOM_QUERY("update vendas set id_cliente = 0 where  id_pedido = " + pedido1);                // ************
                                // Cl_gestor.NOM_QUERY("update vendas set datasincro = "+ DateTime.Now + " where id_pedido = " + pedido1);

                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }

                    }


                }

                int e = 1;


                dados1 = Cl_gestor.EXE_QUERY("select * from nvenda_or ");
                int ped1 = 0, xi = 0;


                foreach (DataRow linha in dados1.Rows)
                {

                    List<SQLparametro> parametro1 = new List<SQLparametro>();

                    ped1 = Convert.ToInt32(dados1.Rows[xi]["id_venda"]);

                    parametro1.Add(new SQLparametro("@pedido", ped1));
                    parametro1.Add(new SQLparametro("@data", DateTime.Now));


                    Cl_gestor.NOM_QUERY(" update vendas_or set datasincro = @data where id_pedido == @pedido ", parametro1);

                    xi = xi + 1;
                }
                return true;
            }




            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            public static bool SelectVendas()
            {
                DataTable dados;

                if (vars.web_local)
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["endereco"].ToString();
                    enderecows = dados.Rows[0]["endereco"].ToString() + "/insert.php";
                } else
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["caminholocal"].ToString();
                    enderecows = dados.Rows[0]["caminholocal"].ToString() + "/insert.php";
                }



                //Context contesto =  getApplicationContext();
                //Toast.MakeText(this, dados.Rows[0]["endereco"].ToString(), ToastLength.Short).Show();

                //var URL = enderecows + "/insert.php";


                // at_menu_inicial.mensagem("Vendas............");

                dados = Cl_gestor.EXE_QUERY("select * from vendas");

                FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,String>("nome", dados.Rows[0]["nome"].ToString())

            });

                HttpClient http = new HttpClient();
                HttpResponseMessage resposta = http.PostAsync(enderecows, param).GetAwaiter().GetResult();

                if (resposta.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }

                return false;
            }


            private static HttpClient GetHttp()
            {
                HttpClient http = new HttpClient();

                http.DefaultRequestHeaders.Add("Accept", "application/json");
                http.DefaultRequestHeaders.Add("Connection", "close");

                return http;
            }


            public static async Task<bool> insereclientes()
            {
                ///*************************************************
                ///

                DataTable dados;

                if (vars.web_local)
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["endereco"].ToString();
                    enderecows = dados.Rows[0]["endereco"].ToString() + "/insert.php";
                } else
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["caminholocal"].ToString();
                    enderecows = dados.Rows[0]["caminholocal"].ToString() + "/insert.php";

                }




                List<clie> dois;
                List<clie> Users;
                using (var client = new HttpClient())
                    try
                    {
                        string url = enderecowsporta + "/selectclie.php";



                        var response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();





                        var stringResult = await response.Content.ReadAsStringAsync();
                        //Users = JsonConvert.DeserializeObject<List<nomes>>(stringResult.ToString());

                        //   Users = JsonConvert.DeserializeObject<nomes>(stringResult.ToString());
                        //  var post = JsonConvert.DeserializeObject<nomes>(stringResult.ToString());

                        //at_menu_inicial.mensagem("Clientes............");

                        Cl_gestor.NOM_QUERY("Delete from clientes ");

                        clie[] colecaoPegarid = JsonConvert.DeserializeObject<clie[]>(stringResult);

                        int i = colecaoPegarid.Length - 1;

                        int xcv = 1;

                        for (int j = 0; j <= i; j++)
                        {
                            if (colecaoPegarid[j].clinom != null)
                            {
                                Console.WriteLine(colecaoPegarid[j].clicod + "   -   " + colecaoPegarid[j].clinom);


                                List<SQLparametro> parametro = new List<SQLparametro>();

                                parametro.Add(new SQLparametro("@id_cliente", colecaoPegarid[j].clicod));
                                parametro.Add(new SQLparametro("@nome", colecaoPegarid[j].clinom));
                                parametro.Add(new SQLparametro("@telefone", colecaoPegarid[j].clinom));
                                parametro.Add(new SQLparametro("@atualizacao", DateTime.Now));
                                parametro.Add(new SQLparametro("@novo", 0));



                                Cl_gestor.NOM_QUERY(
                                                    "insert into clientes (id_cliente,nome,telefone,atualizacao,novo) values (" +
                                                    "@id_cliente," +
                                                    "@nome," +
                                                    "@telefone," +
                                                    "@atualizacao," +
                                                    "@novo)", parametro);


                            }
                        }


                        Cl_gestor.NOM_QUERY("CREATE INDEX nome1 ON nome )");



                    }
                    catch (HttpRequestException httpRequestException)
                    {
                        Users = null;
                    }
                return false;
            }




            public static async Task<bool> insereprodutos()
            {
                ///*************************************************
                ///
                DataTable dados;

                if (vars.web_local)
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["endereco"].ToString();
                } else
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["caminholocal"].ToString();
                }

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
                        Cl_gestor.NOM_QUERY("Delete from produtos");


                        // at_menu_inicial.mensagem("Produtos...........");




                        cl_prod[] colecaoPegarid = JsonConvert.DeserializeObject<cl_prod[]>(stringResult);

                        int i = colecaoPegarid.Length - 1;

                        int xcv = 1;
                        int iii = 0;


                        for (iii = 0; iii <= i; iii++)
                        {
                        if (colecaoPegarid[iii].descpro != null)
                        {


                            List<SQLparametro> parametro = new List<SQLparametro>();


                            string xy = colecaoPegarid[iii].preco1.ToString();
                            xy = xy.Replace(",", ".");
                            Console.WriteLine(colecaoPegarid[iii].descpro + " " + xy);

                            at_sincronizar.barray++;

                            parametro.Add(new SQLparametro("@id_produto", colecaoPegarid[iii].codigo));
                            parametro.Add(new SQLparametro("@descricao", colecaoPegarid[iii].descpro));
                            parametro.Add(new SQLparametro("@preco", Convert.ToDecimal(colecaoPegarid[iii].preco1.ToString())));
                            parametro.Add(new SQLparametro("@atualizacao", DateTime.Now));
                            parametro.Add(new SQLparametro("@codbar", colecaoPegarid[iii].codbar));
                            parametro.Add(new SQLparametro("@preco2", Convert.ToDecimal(colecaoPegarid[iii].preco2.ToString())));
                            parametro.Add(new SQLparametro("@preco3", Convert.ToDecimal(colecaoPegarid[iii].preco3.ToString())));
                            parametro.Add(new SQLparametro("@preco4", Convert.ToDecimal(colecaoPegarid[iii].preco4.ToString())));
                            parametro.Add(new SQLparametro("@preco4", Convert.ToDecimal(colecaoPegarid[iii].preco4.ToString())));
                            parametro.Add(new SQLparametro("@descapp", Convert.ToDecimal(colecaoPegarid[iii].descapp.ToString())));
                            parametro.Add(new SQLparametro("@uni", Convert.ToDecimal(colecaoPegarid[iii].uni.ToString())));
                            parametro.Add(new SQLparametro("@qtd", colecaoPegarid[iii].qtd.ToString()));
                            parametro.Add(new SQLparametro("@custo", colecaoPegarid[iii].custo.ToString()));
                           





                            Cl_gestor.NOM_QUERY(
                                                "insert into produtos (id_produto,descricao,preco,atualizacao,codbar,preco2,preco3,preco4,descapp,uni,qtd,custo) values (" +
                                                "@id_produto," +
                                                "@descricao," +
                                                "@preco," +
                                                "@atualizacao," +
                                                "@codbar," +
                                                "@preco2," +
                                                "@preco3," +
                                                "@preco4," +
                                                "@descapp," +
                                                "@uni, " +
                                                "@qtd, " +
                                                "@custo) ", parametro);
                                                
                                                
                                                    


                                // Toast.MakeText(Application.Context, " Sincronização  "+ (colecaoPegarid[at_sincronizar.barray].descpro), ToastLength.Long).Show();


                            }
                        }


                        // Cl_gestor.NOM_QUERY("CREATE INDEX nome1 ON nome )");



                    }
                    catch (HttpRequestException httpRequestException)
                    {
                        Users = null;
                    }
                return false;
            }

            public static async Task<bool> inserepgto()
            {
                ///*************************************************
                ///
                DataTable dados;

                //dados = Cl_gestor.EXE_QUERY("select * from parametro");
                //enderecowsporta = dados.Rows[0]["endereco"].ToString();

                if (vars.web_local)
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["endereco"].ToString();
                }
                else
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["caminholocal"].ToString();
                }




                List<cl_prod> dois;
                List<cl_prod> Users;
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

                        int i = colecaoPegarid.Length - 1;

                        int xcv = 1;

                        for (int j = 0; j <= i; j++)
                        {
                            if (colecaoPegarid[j].descricao != null)
                            {


                                List<SQLparametro> parametro = new List<SQLparametro>();

                                Console.WriteLine(colecaoPegarid[j].descricao);

                                parametro.Add(new SQLparametro("@id_pagamento", colecaoPegarid[j].codigo));
                                parametro.Add(new SQLparametro("@descricao", colecaoPegarid[j].descricao));
                                parametro.Add(new SQLparametro("@dias", 0));




                                Cl_gestor.NOM_QUERY(
                                                    "insert into  pagamento (id_pagamento, descricao) values (" +
                                                    "@id_pagamento," +
                                                    "@descricao);", parametro);


                            }
                        }


                        // Cl_gestor.NOM_QUERY("CREATE INDEX nome1 ON nome )");
                        // at_menu_inicial.mensagem("Fim Sincronização.....");


                    }
                    catch (HttpRequestException httpRequestException)
                    {
                        Users = null;
                    }
                return false;
            }





            public static async Task<bool> insereusu()
            {
                ///*************************************************
                DataTable dados;

                //dados = Cl_gestor.EXE_QUERY("select * from parametro");
                //enderecowsporta = dados.Rows[0]["endereco"].ToString() ;
                ///


                if (vars.web_local)
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["endereco"].ToString();
                }
                else
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["caminholocal"].ToString();
                }

                List<cl_usu> dois;
                List<cl_usu> Users;
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

                        int i = colecaoPegarid.Length - 1;

                        int xcv = 1;

                        for (int j = 0; j <= i; j++)
                        {
                            if (colecaoPegarid[j].nome != null)
                            {


                                List<SQLparametro> parametro = new List<SQLparametro>();

                                Console.WriteLine(colecaoPegarid[j].nome);

                                parametro.Add(new SQLparametro("@id_usuario", colecaoPegarid[j].codigo));
                                parametro.Add(new SQLparametro("@nome", colecaoPegarid[j].nome));
                                parametro.Add(new SQLparametro("@senha", colecaoPegarid[j].senha));
                                parametro.Add(new SQLparametro("@descvendas", colecaoPegarid[j].descvendas));
                                parametro.Add(new SQLparametro("@gerente", colecaoPegarid[j].gerente));

                                Cl_gestor.NOM_QUERY(
                                                    "insert into  usuario values (" +
                                                    "@id_usuario," +
                                                    "@nome," +
                                                    "@senha," +
                                                    "@descvendas)", parametro);


                                int c = 1;
                            }
                        }


                        // Cl_gestor.NOM_QUERY("CREATE INDEX nome1 ON nome )");
                        // at_menu_inicial.mensagem("Fim Sincronização.....");


                    }
                    catch (HttpRequestException httpRequestException)
                    {
                        Users = null;
                    }
                return false;
            }

            //********************//*****************************************************************************




            //**********************************************************************************************************

            public static void limpavenda()
            {

                Cl_gestor.NOM_QUERY("Delete from vendas ");

                Cl_gestor.NOM_QUERY("Delete from vendasprd ");


            }

            public static async Task confere_pedidosAsync()
            {

                DataTable dados;

                if (vars.web_local)
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecows = dados.Rows[0]["endereco"].ToString() + "/insert5.php";
                    enderecowsporta = dados.Rows[0]["endereco"].ToString();
                    enderecowslinha = dados.Rows[0]["endereco"].ToString() + "/insertp5.php";
                }
                else
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecows = dados.Rows[0]["caminholocal"].ToString() + "/insert5.php";
                    enderecowsporta = dados.Rows[0]["caminholocal"].ToString();
                    enderecowslinha = dados.Rows[0]["caminholocal"].ToString() + "/insertp5.php";

                }

                int ch = 0;

                DataTable dadosped = Cl_gestor.EXE_QUERY(" select * from  vendas where checar = 0  ");


                int contador = dadosped.Rows.Count;

                if (dadosped.Rows.Count != 0)
                {
                    foreach (DataRow ped in dadosped.Rows)
                    {

                        string xped = ped["id_pedido"].ToString();
                        string xpedbco = ped["nrovendabco"].ToString();

                        if (xpedbco != "")
                        {

                            DataTable dadoslinha = Cl_gestor.EXE_QUERY(" select id_pedido from  vendasprd where id_pedido =  " + xped);

                            int contalinha = dadoslinha.Rows.Count;

                            using (var client = new HttpClient())
                                try
                                {
                                    string url = enderecowsporta + "/selectpedido.php?par=" + xpedbco;
                                    var response = await client.GetAsync(url);
                                    response.EnsureSuccessStatusCode();

                                    var stringResult = await response.Content.ReadAsStringAsync();

                                    linped[] colecaoPegarid = JsonConvert.DeserializeObject<linped[]>(stringResult);


                                    //Console.WriteLine(" aqui --> " + colecaoPegarid[0].seqmax);
                                    vars.maxlinhas = 0;
                                    vars.maxlinhas = Convert.ToInt32(colecaoPegarid[0].totmax);


                                    List<SQLparametro> parametrox = new List<SQLparametro>();
                                    parametrox.Add(new SQLparametro("@datax", DateTime.MinValue));
                                    parametrox.Add(new SQLparametro("@ped1", xped));



                                    if (contalinha != vars.maxlinhas)
                                    {


                                        Cl_gestor.NOM_QUERY(" update vendas set  datasincro = @datax,nrovendabco = 0 where id_pedido = @ped1 ", parametrox);
                                        Cl_gestor.NOM_QUERY(" update vendasprd set  nrovendabco = 0 where id_pedido = @ped1 ", parametrox);

                                        string url2 = enderecowsporta + "/deletapedido.php?par=" + xpedbco;
                                        var response2 = await client.GetAsync(url2);
                                        response2.EnsureSuccessStatusCode();

                                        var stringResult2 = await response2.Content.ReadAsStringAsync();



                                    }
                                    else
                                    {
                                        Cl_gestor.NOM_QUERY(" update vendas set  checar = 1 where id_pedido = @ped1 ", parametrox);

                                    }


                                }
                                catch (HttpRequestException httpRequestException)
                                {

                                }
                        }

                    }

                }

            }

            public static async Task<bool> pesquisaultimop()
            {
                ///*************************************************
                ///
                DataTable dados;

                //dados = Cl_gestor.EXE_QUERY("select * from parametro");
                //enderecowsporta = dados.Rows[0]["endereco"].ToString() + "/insert.php";

                if (vars.web_local)
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["endereco"].ToString() + "/insert.php";
                }
                else
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["caminholocal"].ToString() + "/insert.php";
                }




                List<tslv020> dois;
                List<tslv020> Users;
                using (var client = new HttpClient())
                    try
                    {
                        string url = enderecowsporta + "/selectultped.php";
                        var response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        var stringResult = await response.Content.ReadAsStringAsync();

                        tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);


                        Console.WriteLine(" aqui --> " + colecaoPegarid[0].seqmax);
                        vars.numeropedidosalvo = 0;
                        vars.numeropedidosalvo = Convert.ToInt32(colecaoPegarid[0].seqmax);

                        //****************************************************











                        //*************************************************

                    }
                    catch (HttpRequestException httpRequestException)
                    {
                        Users = null;
                    }
                return true;
            }


            public static async Task<bool> pesquiprod()
            {
                ///*************************************************
                ///
                DataTable dados;


                if (vars.web_local)
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["endereco"].ToString() + "/insert.php";
                }
                else
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["caminholocal"].ToString() + "/insert.php";
                }






                List<tslv020> dois;
                List<tslv020> Users;
                using (var client = new HttpClient())
                    try
                    {
                        string url = enderecowsporta + "/selectultped.php";
                        var response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        var stringResult = await response.Content.ReadAsStringAsync();

                        tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);


                        Console.WriteLine(" aqui --> " + colecaoPegarid[0].seqmax);
                        vars.numeropedidosalvo = 0;
                        vars.numeropedidosalvo = Convert.ToInt32(colecaoPegarid[0].seqmax);

                        //************************************************








                        //*************************************************
                    }
                    catch (HttpRequestException httpRequestException)
                    {
                        Users = null;
                    }
                return true;
            }


            ////////////////////////////////

            public static async Task<bool> pesquisadados()
            {
                ///*************************************************
                ///

                DataTable dados;



                if (vars.web_local)
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["endereco"].ToString();
                    enderecows = dados.Rows[0]["endereco"].ToString() + "/parametroclie.php";
                }
                else
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["caminholocal"].ToString();
                    enderecows = dados.Rows[0]["caminholocal"].ToString() + "/parametroclie.php";

                }




                List<Parametrocli> dois;
                List<Parametrocli> Users;
                using (var client = new HttpClient())
                    try
                    {


                        ///   Cl_gestor.NOM_QUERY("delete from parametroclie ");



                        string url = enderecowsporta + "/parametroclie.php";
                        var response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        var stringResult = await response.Content.ReadAsStringAsync();
                        //Users = JsonConvert.DeserializeObject<List<nomes>>(stringResult.ToString());

                        //   Users = JsonConvert.DeserializeObject<nomes>(stringResult.ToString());
                        //  var post = JsonConvert.DeserializeObject<nomes>(stringResult.ToString());

                        //at_menu_inicial.mensagem("Clientes............");

                        // Cl_gestor.NOM_QUERY("Delete from clientes ");

                        Parametrocli[] colecaoPegarid = JsonConvert.DeserializeObject<Parametrocli[]>(stringResult);

                        int i = colecaoPegarid.Length - 1;

                        int xcv = 1;

                        for (int j = 0; j <= i; j++)
                        {
                            if (colecaoPegarid[j].Nome != null)
                            {

                                Console.WriteLine(" insercao nvenda ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -----------------> " + colecaoPegarid[j].Nome);


                                List<SQLparametro> parametro = new List<SQLparametro>();

                                parametro.Add(new SQLparametro("@nome", colecaoPegarid[j].Nome));
                                parametro.Add(new SQLparametro("@endereco", colecaoPegarid[j].Endereco));
                                parametro.Add(new SQLparametro("@bairro", colecaoPegarid[j].Bairro));
                                parametro.Add(new SQLparametro("@cidade", colecaoPegarid[j].Cidade));
                                parametro.Add(new SQLparametro("@telefone", colecaoPegarid[j].Fone));


                                Cl_gestor.NOM_QUERY(
                                                    "insert into parametrocli values (" +
                                                    "@nome," +
                                                    "@endereco," +
                                                    "@bairro," +
                                                    "@cidade," +
                                                    "@telefone)", parametro);


                            }
                        }



                    }
                    catch (HttpRequestException httpRequestException)
                    {
                        Users = null;
                    }
                return false;
            }

            /////////////////////////////////
            public static async Task<bool> carrega_fina()
            {

                DataTable dados;

                if (vars.web_local)
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["endereco"].ToString() + "/receber.php";
                }
                else
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecowsporta = dados.Rows[0]["caminholocal"].ToString() + "/receber.php";
                }



                List<tslv020> dois;
                List<tslv020> Users;
                using (var client = new HttpClient())
                    try
                    {
                        string url = enderecowsporta; //+ "/mesas.php";
                        var response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        var stringResult = await response.Content.ReadAsStringAsync();

                        cl_receber[] colecaoPegarid = JsonConvert.DeserializeObject<cl_receber[]>(stringResult);

                        int i = colecaoPegarid.Length - 1;


                        int xcv = 1;

                        for (int j = 0; j <= i; j++)
                        {
                            if (colecaoPegarid[j].titulo != null)
                            {


                                List<SQLparametro> parametro = new List<SQLparametro>();


                                parametro.Add(new SQLparametro("@titulo", colecaoPegarid[j].titulo));
                                parametro.Add(new SQLparametro("@nome", colecaoPegarid[j].nome));
                                parametro.Add(new SQLparametro("@emissao", colecaoPegarid[j].emissao));
                                parametro.Add(new SQLparametro("@vencimento", colecaoPegarid[j].vencimento));
                                parametro.Add(new SQLparametro("@valor", colecaoPegarid[j].valor));



                                Cl_gestor.NOM_QUERY(
                            "insert into receber (titulo,nome,emissao,vencimento,valor) values ( " +
                            "@titulo," +
                            "@nome," +
                            "@emissao," +
                            "@vencimento," +
                            "@valor)", parametro);

                            }
                        }


                    }
                    catch (HttpRequestException httpRequestException)
                    {

                        Users = null;
                    }

                return true;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///*//////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////


            public static async Task<bool> Inserinsereclientesapp()
            {



                DataTable dados;
                DataTable dados1;

                DateTime data1 = DateTime.MinValue;

                string caminhox = "";
                if (vars.web_local)
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecows = dados.Rows[0]["endereco"].ToString() + "/insertclie.php";
                    caminhox = dados.Rows[0]["endereco"].ToString();
                }
                else
                {
                    dados = Cl_gestor.EXE_QUERY("select * from parametro");
                    enderecows = dados.Rows[0]["caminholocal"].ToString() + "/insertclie.php";
                    caminhox = dados.Rows[0]["endereco"].ToString();
                }


                List<SQLparametro> parametrox = new List<SQLparametro>();

                parametrox.Add(new SQLparametro("@novox", 1));


                int cv1 = 0;
                dados = Cl_gestor.EXE_QUERY("select * from clientes where  novo == @novox ", parametrox);



                // at_menu_inicial.mensagem( "Vendas............");

                Console.WriteLine(" linhas header----> " + dados.Rows.Count);


                int cv = dados.Rows.Count;
                int totall = 0;

                //at_sincronizar at_Sincronizar = new at_sincronizar();

                for (totall = 0; totall < cv; totall++)
                {
                    at_sincronizar.barray++;

                    if (Convert.ToInt32(dados.Rows[totall]["novo"]) == 1)
                    {

                        FormUrlEncodedContent paramx = new FormUrlEncodedContent(new[] {



                        new KeyValuePair<string, String>("nome1", dados.Rows[totall]["nome"].ToString()),
                        new KeyValuePair<string, String>("telefone1",dados.Rows[totall]["telefone"].ToString()),
                        new KeyValuePair<string, String>("endereco1",dados.Rows[totall]["endereco"].ToString()),
                        new KeyValuePair<string, String>("bairro1",dados.Rows[totall]["bairro"].ToString()),
                        new KeyValuePair<string, String>("cidade1",dados.Rows[totall]["cidade"].ToString()),
                        new KeyValuePair<string, String>("numero1",dados.Rows[totall]["numero"].ToString()),
                        new KeyValuePair<string, String>("cep1",dados.Rows[totall]["cep"].ToString()),
                        new KeyValuePair<string, String>("telefone11",dados.Rows[totall]["telefone1"].ToString()),
                        new KeyValuePair<string, String>("telefone21",dados.Rows[totall]["telefone2"].ToString()),
                        new KeyValuePair<string, String>("celular1",dados.Rows[totall]["celular"].ToString()),
                        new KeyValuePair<string, String>("contato1",dados.Rows[totall]["contato"].ToString()),
                        new KeyValuePair<string, String>("ndereco1",dados.Rows[totall]["endereco"].ToString()),
                        new KeyValuePair<string, String>("email1",dados.Rows[totall]["email"].ToString()),
                        new KeyValuePair<string, String>("cnpj1",dados.Rows[totall]["cnpj"].ToString()),
                        new KeyValuePair<string, String>("ie1",dados.Rows[totall]["ie"].ToString()),
                        new KeyValuePair<string, String>("cpf1",dados.Rows[totall]["cpf"].ToString()),
                        new KeyValuePair<string, String>("rg1",dados.Rows[totall]["rg"].ToString()),
                        new KeyValuePair<string, String>("fantasia1",dados.Rows[totall]["fantasia"].ToString()),
                        new KeyValuePair<string, String>("vendedor1",vars.numerovendedor.ToString())




                    });


                        // Console.WriteLine(" insercao Cliente ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -----------------> " + pedido1);

                        HttpClient http = new HttpClient();
                        HttpResponseMessage resposta = http.PostAsync(enderecows, paramx).GetAwaiter().GetResult();

                        if (resposta.StatusCode == HttpStatusCode.OK)
                        {
                            x = true;


                            //**************************************************************************************************
                            //  busca ultimo registro inserido para atualizar base interna antes de importar pedido
                            //pesquisaultimop();


                            List<tslv020> dois;
                            List<tslv020> Users;
                            using (var client = new HttpClient())
                                try
                                {
                                    string url = caminhox + "/selectultclie.php";
                                    var response = await client.GetAsync(url);
                                    response.EnsureSuccessStatusCode();

                                    var stringResult = await response.Content.ReadAsStringAsync();

                                    tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);


                                    Console.WriteLine(" aqui --> " + colecaoPegarid[0].seqmax);
                                    vars.numeropedidosalvo = 0;
                                    vars.numeropedidosalvo = Convert.ToInt32(colecaoPegarid[0].seqmax);

                                    List<SQLparametro> parametro1 = new List<SQLparametro>();
                                    parametro1.Add(new SQLparametro("@nrocli", vars.numeropedidosalvo));
                                    parametro1.Add(new SQLparametro("@idcliente", dados.Rows[totall]["id_cliente"].ToString()));



                                    Cl_gestor.NOM_QUERY(" update clientes set novo = @nrocli where id_cliente == @idcliente ", parametro1);



                                }
                                catch (HttpRequestException httpRequestException)
                                {
                                    Users = null;
                                }


                            Console.WriteLine(" aqui -----------------> " + vars.numeropedidosalvo);






                        }
                        else
                        {
                            int ff = 1;
                        }

                    }


                }

                //int e = 1;


                //dados1 = Cl_gestor.EXE_QUERY("select * from nvenda ");
                //int ped1 = 0, xi = 0;


                //foreach (DataRow linha in dados1.Rows)
                //{

                //    List<SQLparametro> parametro1 = new List<SQLparametro>();

                //    ped1 = Convert.ToInt32(dados1.Rows[xi]["id_venda"]);

                //    parametro1.Add(new SQLparametro("@pedido", ped1));
                //    parametro1.Add(new SQLparametro("@data", DateTime.Now));


                //    Cl_gestor.NOM_QUERY(" update vendas set datasincro = @data where id_pedido == @pedido ", parametro1);

                //    xi = xi + 1;
                //}
                return true;
            }






            /////////////////////////////////////
            ///*///////////////////////////////////
            /////////////////////////////////////
        }

      
    }
