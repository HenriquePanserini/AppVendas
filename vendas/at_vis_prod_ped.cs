using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Data;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
//using Plugin.Share;

namespace vendas
{
    [Activity(Label = "at_vis_prod_ped")]
    public class at_vis_prod_ped : Activity
    {
        
        ListView lista_prod_ped;
        TextView label_numero_produtos;
        TextView label;
        TextView label2;
        TextView label3;
        List<cl_vendasst> PRODUTOSS;
        List<string> NOMES_PRODUTOS; 
        int xpedido = vars.pedidoselecionadodet;
        Button voltar_vis;
        Button excluirped;
        Button alterarped;
        Button duplicarped;
        private float[] fi;
        Intent intent_temp;
        private int descontosel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.layout_vis_prod_ped);

            lista_prod_ped = FindViewById<ListView>(Resource.Id.lista_prod_ped);
            label_numero_produtos = FindViewById<TextView>(Resource.Id.label_numero_produtos);
            label = FindViewById<TextView>(Resource.Id.label);
            label2 = FindViewById<TextView>(Resource.Id.label2);
            label3 = FindViewById<TextView>(Resource.Id.label3);

            voltar_vis = FindViewById<Button>(Resource.Id.voltar_vis);
            excluirped = FindViewById<Button>(Resource.Id.excluirped);
            alterarped = FindViewById<Button>(Resource.Id.alterarped);
            duplicarped = FindViewById<Button>(Resource.Id.duplicarped);




            // label_numero_produtos.Text = vars.nomeclientesel2;
            label.Text = "Pedido " + vars.pedidoselecionadodet.ToString() ;

            //////


            int ped2 = vars.pedidoselecionadodet;

            DataTable dados1 = Cl_gestor.EXE_QUERY("select * from vendas where id_pedido =="+ ped2);
            int codclix = Convert.ToInt32(dados1.Rows[0]["id_cliente"]);

            DataTable dados11 = Cl_gestor.EXE_QUERY("select * from clientes where id_cliente ==" + codclix);

            if (dados11.Rows.Count != 0)
            {
                label2.Text = "Cliente " + dados11.Rows[0]["nome"].ToString();
            }
            else
            {
                label2.Text = "Erro no Cliente ........... ";
            }


            int pgto = Convert.ToInt32(dados1.Rows[0]["id_pgto"]);
            DataTable dados2 = Cl_gestor.EXE_QUERY("select * from pagamento where id_pagamento ==" + pgto);

           


            if (dados2.Rows.Count != 0)
            {
                label3.Text = "Pgto " + dados2.Rows[0]["descricao"].ToString() + "  Data " + dados1.Rows[0]["data"].ToString();
            }
            else
            {
                label3.Text = "Pgto     "  ;
            }


            //////
            ///
           
            voltar_vis.Click += Voltar_vis_Click;
            excluirped.Click += Excluirped_Click;
            alterarped.Click += Alterarped_Click;
            duplicarped.Click += Duplicarped_Click;
            ConstroiListaProdutos();

         
           

        }

        private void Duplicarped_Click(object sender, EventArgs e)
        {
            at_vis_vendas at_Vis_Vendas = new at_vis_vendas();

            

            int ped2x = vars.pedidoselecionadodet;
            vars.alterarped = true;
            vars.npedidoaltera = ped2x;
            vars.flagmeiopedido = true;
            Cl_gestor.NOM_QUERY("delete from tmp_vendas_prod ");
            vars.dupliped = false;
            FinishActivity(1003);
            StartActivity(typeof(at_vendas));
            this.Finish();
          


        }

        private void Alterarped_Click(object sender, EventArgs e)
        {

            int ped2x = vars.pedidoselecionadodet;
            vars.alterarped = true;
            vars.npedidoaltera = ped2x;
            vars.flagmeiopedido = true;
            vars.dupliped = true;
            Cl_gestor.NOM_QUERY("delete from tmp_vendas_prod ");
            FinishActivity(1003);

            StartActivity(typeof(at_vendas));
                
            this.Finish();
            FinishActivity(1003);

        }

        private void Excluirped_Click(object sender, EventArgs e)
        {

            int ped2x = vars.pedidoselecionadodet;
            Cl_gestor.NOM_QUERY("Delete from vendas where id_pedido = " + ped2x);
            Cl_gestor.NOM_QUERY("Delete from vendasprd where id_pedido = " + ped2x);
            Toast.MakeText(Application.Context, "Excluido", ToastLength.Short).Show();



            Intent i = new Intent(this, typeof(at_vis_vendas));

            i.PutExtra("id_produto", 0);
            StartActivityForResult(i, 0);
            this.Finish();



        }

        private void Voltar_vis_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        //protected override void 

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                //at_clientes at = new at_clientes();
                ConstroiListaProdutos();
            }
        }





        private void ConstroiListaProdutos()
        {
            PRODUTOSS = new List<cl_vendasst>();
            DataTable dados = Cl_gestor.EXE_QUERY("select * from vendasprd where id_pedido = " + xpedido);
            foreach (DataRow linha in dados.Rows)
            {
                PRODUTOSS.Add(new cl_vendasst()
                {
                    //id_produto = Convert.ToInt32(linha["id_produto"]),
                    //descricao = linha["descricao"].ToString(),
                    //precodesconto = Double.Parse(linha["precodesconto"].ToString()),
                    //total = Double.Parse(linha["total"].ToString()),
                    //quantidade = Decimal.Parse(linha["quantidade"].ToString()),

                    id_produto = Convert.ToInt32(linha["id_produto"]),
                    descricao = linha["descricao"].ToString(),
                    precodesconto = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Decimal.Parse(linha["precodesconto"].ToString())),
                    total = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Decimal.Parse(linha["total"].ToString())),
                    quantidade = linha["quantidade"].ToString(),


                });

            }




            NOMES_PRODUTOS = new List<string>();
            foreach (cl_vendasst produto in PRODUTOSS)
            {
                NOMES_PRODUTOS.Add("Prod.:" + produto.descricao + "\n QTD " + produto.quantidade + " UNIT " + produto.precodesconto + " TOTAL " + produto.total);

            }


            ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.TestListItem, NOMES_PRODUTOS);
            lista_prod_ped.Adapter = adaptador;





        }
        
    }




}