using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using System.Data;
using MySql.Data.MySqlClient;
using Com.Karumi.Dexter;
using Android.Support.V7.App;
using Com.Karumi.Dexter.Listener.Single;
using Com.Karumi.Dexter.Listener;
using static Android.Provider.DocumentsContract;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Android.Util;
using Android.Print;


namespace vendas
{
    [Activity(Label = "Vendas TOL")]
    // [Activity(Label = "Vendas TOL", MainLauncher = true, Icon = "@drawable/Icon")]
    public class at_menu_inicial : AppCompatActivity
    {
        //private void CheckAppPermissions()
        //{
        //    if ((int)Build.VERSION.SdkInt < 19)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        if (PackageManager.CheckPermission(Manifest.Permission.ReadExternalStorage, PackageName) != Permission.Granted
        //            && PackageManager.CheckPermission(Manifest.Permission.WriteExternalStorage, PackageName) != Permission.Granted)
        //        {
        //            var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };
        //            RequestPermissions(permissions, 77);
        //        }
        //    }
        //}


        Button cmd_clientes;
        Button cmd_produtos;
        Button cmd_vendas;
        Button cmd_estatistica;
        Button cmd_sincronizar;
        Button cmd_sair;
        Button cmd_orcamento;
        Button cmd_estatistica_or;
        Button cmd_rela;
       
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            //CheckAppPermissions();

            base.OnCreate(savedInstanceState);


            //Cl_gestor.InicioAplicacao();

            // Create your application here
            SetContentView(Resource.Layout.layout_menu_inicial);
            


            cmd_clientes = FindViewById<Button>(Resource.Id.cmd_clientes);
            cmd_produtos = FindViewById<Button>(Resource.Id.cmd_produtos);
            cmd_vendas = FindViewById<Button>(Resource.Id.cmd_vendas);
            cmd_estatistica = FindViewById<Button>(Resource.Id.cmd_estatistica);
            cmd_sincronizar = FindViewById<Button>(Resource.Id.cmd_sincronizar);
            cmd_sair = FindViewById<Button>(Resource.Id.cmd_sair);
            cmd_orcamento = FindViewById<Button>(Resource.Id.cmd_orcamento);
            cmd_estatistica_or = FindViewById<Button>(Resource.Id.cmd_estatistica_or);
            cmd_rela = FindViewById<Button>(Resource.Id.cmd_rela);
         
            cmd_clientes.Click += Cmd_clientes_Click;
            cmd_vendas.Click += Cmd_vendas_Click;
            cmd_produtos.Click += Cmd_produtos_Click;
            cmd_estatistica.Click += Cmd_estatistica_Click;
            cmd_sincronizar.Click += Cmd_sincronizar_Click;
            cmd_sair.Click += Cmd_sair_Click;
            cmd_orcamento.Click += Cmd_orcamento_Click;
            cmd_estatistica_or.Click += Cmd_estatistica_or_Click;
            cmd_rela.Click += Cmd_rela_Click;
        }

        
        private void Cmd_rela_Click(object sender, EventArgs e)
        {


            StartActivity(typeof(at_rel_menu));

            //// Cl_gestor.NOM_QUERY("Delete from receber ");


            //// DataTable dadosx = Cl_gestor.EXE_QUERY("select * from  parametro ");

            ////vars.parvenda = Int32.Parse(dadosx.Rows[0]["vendasdesc"].ToString());

            ////if (vars.parvenda == 0)
            ////{

            /////QUERYAsync();
            //cl_webservice.carrega_fina();

            //cl_webservice.carrega_fina();
            ///DataTable dadosx1 = Cl_gestor.EXE_QUERY("select * from  receber ");

            //StartActivity(typeof(at_rel_receber));

            ////}

        }

        private void Cmd_estatistica_or_Click(object sender, EventArgs e)
        {


            Cl_gestor.NOM_QUERY("Delete from tmp_vendas_prod_or");


            Intent i = new Intent(this, typeof(at_vis_vendas_or));

            //i.PutExtra("id_pagamento", 0);
            //StartActivityForResult(i, 0);

            StartActivityForResult(i, 1004);


        }

        private void Cmd_orcamento_Click(object sender, EventArgs e)
        {

            vars.alterarped = false;
            vars.dupliped = false;
            vars.flagmeiopedido = true;



            DataTable dadosx = Cl_gestor.EXE_QUERY("select * from  parametro ");

            vars.parvenda = Int32.Parse(dadosx.Rows[0]["vendasdesc"].ToString());

            if (vars.parvenda == 0)
            {
                StartActivity(typeof(at_vendas_or));
            }
            else
            {
                StartActivity(typeof(at_vendas_or_sdesc));
            }


            //  StartActivity(typeof(at_vendas_or));

        }

        private void Cmd_sair_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void Cmd_sincronizar_Click(object sender, EventArgs e)
        {
            //DataTable dados = Cl_gestor.EXE_QUERY("select * from parametro");
            //enderecows = dados.Rows[0]["endereco"].ToString() + "/insert.php";

            // Toast.MakeText(this, dados.Rows[0]["endereco"].ToString(), ToastLength.Short).Show();

            // dados = Cl_gestor.EXE_QUERY("select * from vendas");

            // Toast.MakeText(this, dados.Rows.Count.ToString(), ToastLength.Short).Show();

            StartActivity(typeof(at_sincronizar));


        }
        ///   visualizar vendas 
        private void Cmd_estatistica_Click(object sender, EventArgs e)
        {

            Cl_gestor.NOM_QUERY("Delete from tmp_vendas_prod ");


            Intent i = new Intent(this, typeof(at_vis_vendas));

            //i.PutExtra("id_pagamento", 0);
            //StartActivityForResult(i, 0);

            StartActivityForResult(i, 1003);

            //StartActivity(typeof(at_vis_vendas));
        }

        private void Cmd_clientes_Click(object sender, EventArgs e)
        {
            //StartActivity(typeof(at_clientes));
            StartActivity(typeof(at_pesquisacliente1));

        }

        private void Cmd_vendas_Click(object sender, EventArgs e)
        {
            vars.alterarped = false;
            vars.dupliped = false;
            vars.flagmeiopedido = true;

            if (vars.parvenda == 0)
            {
                StartActivity(typeof(at_vendas));
            }
            else
            {
                StartActivity(typeof(at_vendas_sdesc));
            }
        }

        private void Cmd_produtos_Click(object sender, EventArgs e)
        {


            StartActivity(typeof(at_pesquisaproduto1));

        }


        public static void mensagem(string mensagem)
        {

            Toast.MakeText(Android.App.Application.Context, mensagem, ToastLength.Short).Show();

        }

        public static async Task QUERYAsync()
        {

            await cl_webservice.carrega_fina();

            

        }



     
    }
 }