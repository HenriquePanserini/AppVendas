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

namespace vendas
{
    [Activity(Label = "at_vendas")]
    public class at_vendas : Activity
    {

        Button btn_clientes;
        Button btn_formapgto;
        Button btn_produtos;
        Button btn_cancelar;
        Button btn_gravar;
        Button btn_tipopgto;


        TextView nome_cliente2;
        TextView nome_forma;
        TextView total;
        TextView totalgeral;
        TextView nome_tipo;
        EditText desconto;
        EditText obsped;

        //TextView nome_produto;
        //EditText desconto;
        //EditText total;
        //EditText qtde;
        //EditText preco;


        ListView listavenda;
        List<cl_vendasst> VENDAS1;   //CLIENTES
        List<string> NOMES1;           //NOMES
        //List<apresemta_vendas> NOMES2;
        List<cl_vendasst> VENDAS2;
        private object lv2;
        List<Model> NOMES2;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.layout_vendas);

            btn_cancelar = FindViewById<Button>(Resource.Id.btn_cancelar);

            btn_gravar = FindViewById<Button>(Resource.Id.btn_gravar);
            btn_clientes = FindViewById<Button>(Resource.Id.btn_clientes);
            btn_produtos = FindViewById<Button>(Resource.Id.btn_produtos);
            btn_tipopgto = FindViewById<Button>(Resource.Id.btn_tipopgto);
            nome_cliente2 = FindViewById<TextView>(Resource.Id.nome_cliente2);
            nome_forma = FindViewById<TextView>(Resource.Id.nome_forma);
            total = FindViewById<TextView>(Resource.Id.total);
            listavenda = FindViewById<ListView>(Resource.Id.listavenda);
            totalgeral = FindViewById<TextView>(Resource.Id.totalgeral);
            desconto = FindViewById<EditText>(Resource.Id.desconto);
            nome_tipo = FindViewById<TextView>(Resource.Id.nome_tipo);
            obsped = FindViewById<EditText>(Resource.Id.obsped);

            decimal xxx = 0;
            desconto.Text = xxx.ToString();//= string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Decimal.Parse(xxx.ToString()));
            //nome_produto = FindViewById<TextView>(Resource.Id.nome_produto);
            //desconto = FindViewById<EditText>(Resource.Id.desconto);
            //total = FindViewById<EditText>(Resource.Id.total);
            //qtde = FindViewById<EditText>(Resource.Id.qtde);
            //preco = FindViewById<EditText>(Resource.Id.preco);



            btn_formapgto = FindViewById<Button>(Resource.Id.btn_formapgto);

            btn_clientes.Click += Btn_clientes_Click;
            btn_formapgto.Click += Btn_formapgto_Click;
            btn_produtos.Click += Btn_produto_Click;
            btn_cancelar.Click += Btn_cancelar_Click;
            btn_gravar.Click += Btn_gravar_Click;
            btn_tipopgto.Click += Btn_tipopgto_Click;
            desconto.TextChanged += Desconto_TextChanged;
            //quantidade.TextChanged += Quantidade_TextChanged;
            listavenda.ItemLongClick += Listavenda_ItemLongClick;
            obsped.TextChanged += Obsped_TextChanged;

            //***********************************************************************************************************************************************
            ///alteracao do pedido 
                                    if (vars.alterarped)
                                    {
                                            int xpedc = vars.npedidoaltera;
                                            if (vars.flagmeiopedido)
                                            {

                                            DataTable dadosp = Cl_gestor.EXE_QUERY("select * from vendas where id_pedido = " + xpedc);
                                            int nclip = Convert.ToInt32(dadosp.Rows[0]["id_cliente"]);
                                            vars.desconto = Convert.ToDecimal(dadosp.Rows[0]["desconto"]);
                                            desconto.Text = dadosp.Rows[0]["desconto"].ToString();
                                            obsped.Text = dadosp.Rows[0]["obsped"].ToString();  
                                            DataTable dadosc = Cl_gestor.EXE_QUERY("select * from CLIENTES where id_cliente = " + nclip);
                                            if (dadosc.Rows.Count != 0)
                                            {
                                                vars.numerocliente = Convert.ToInt32(dadosc.Rows[0]["id_cliente"]);

                                                Cl_gestor.NOM_QUERY("delete from  tmp_clientes");

                                                List<SQLparametro> parametrox = new List<SQLparametro>();

                                                parametrox.Add(new SQLparametro("@id_cliente", Convert.ToInt32(dadosc.Rows[0]["id_cliente"])));
                                                parametrox.Add(new SQLparametro("@nome", dadosc.Rows[0]["nome"]));

                                                vars.nomeclientesel = dadosc.Rows[0]["nome"].ToString();

                                                Cl_gestor.NOM_QUERY(
                                                    "insert into tmp_clientes values ( " +
                                                    "@id_cliente," +
                                                    "@nome) ", parametrox);
                                            }


                                                int nforma = Convert.ToInt32(dadosp.Rows[0]["id_pgto"]);


                                                DataTable dadospg = Cl_gestor.EXE_QUERY("select * from  pagamento where id_pagamento = " + nforma);
                                                if (dadospg.Rows.Count != 0)
                                                {

                                                    vars.nropgto = Convert.ToInt32(dadospg.Rows[0]["id_pagamento"]);

                                                    Cl_gestor.NOM_QUERY("delete from  tmp_pagamento");


                                                    List<SQLparametro> parametrow = new List<SQLparametro>();


                                                    parametrow.Add(new SQLparametro("@id_pagamento", Convert.ToInt32(dadospg.Rows[0]["id_pagamento"])));
                                                    parametrow.Add(new SQLparametro("@descricao", dadospg.Rows[0]["descricao"]));
                                                    parametrow.Add(new SQLparametro("@dias",0));

                                                    Cl_gestor.NOM_QUERY(
                                                        "insert into tmp_pagamento (id_pagamento, descricao) values ( " +
                                                        "@id_pagamento," +
                                                        "@descricao); ", parametrow);

                                                }


                                                    int ntipo = Convert.ToInt32(dadosp.Rows[0]["id_formapgto"]);

                                                        DataTable dadost = Cl_gestor.EXE_QUERY("select * from  tipopgto where id_tipopgto = " + ntipo);
                                                        if (dadost.Rows.Count != 0)
                                                        {



                                                            Cl_gestor.NOM_QUERY("delete from tmptipopgto");


                                                            List<SQLparametro> parametrot = new List<SQLparametro>();


                                                            parametrot.Add(new SQLparametro("@id_tipopgto", Convert.ToInt32(dadost.Rows[0]["id_tipopgto"])));
                                                            parametrot.Add(new SQLparametro("@descricao", dadost.Rows[0]["descricao"]));

                                                            Cl_gestor.NOM_QUERY(
                                                                "insert into tmptipopgto values ( " +
                                                                "@id_tipopgto," +
                                                                "@descricao) ", parametrot);

                                                        }


                                                               

                                                                    DataTable dadosprod = Cl_gestor.EXE_QUERY("select * from  vendasprd where id_pedido = " + xpedc);
                                                                    if (dadosprod.Rows.Count != 0)
                                                                    {

                                                                        foreach (DataRow linha in dadosprod.Rows)
                                                                        {




                                                                            List<SQLparametro> parametrok = new List<SQLparametro>();


                                                                            parametrok.Add(new SQLparametro("@id_produto", linha["id_produto"]));
                                                                            parametrok.Add(new SQLparametro("@descricao", linha["descricao"]));
                                                                            parametrok.Add(new SQLparametro("@quantidade", linha["quantidade"]));
                                                                            parametrok.Add(new SQLparametro("@preco", linha["preco"]));
                                                                            parametrok.Add(new SQLparametro("@desconto", linha["desconto"]));
                                                                            parametrok.Add(new SQLparametro("@precodesconto", linha["precodesconto"]));
                                                                            parametrok.Add(new SQLparametro("@total", linha["total"]));
                                                                            parametrok.Add(new SQLparametro("@nrovendabco", linha["nrovendabco"]));
                                                                            parametrok.Add(new SQLparametro("@seq", linha["seq"]));
                                                                            



                                                                            Cl_gestor.NOM_QUERY(
                                                                            "insert into tmp_vendas_prod (id_produto,descricao,quantidade,preco,desconto,precodesconto,total) values ( " +
                                                                            "@id_produto," +
                                                                            "@descricao," +
                                                                            "@quantidade," +
                                                                            "@preco," +
                                                                            "@desconto," +
                                                                            "@precodesconto," +
                                                                            "@total) ", parametrok);
                                                                        }
                                                                    }
                  

                                             }

                                           vars.flagmeiopedido = false;

            }

            //***********************************************************************************************************************************************

            ConstroiListavendas();
            ConstroiListaProdutosVendas();
            xdesconto();
            obsped.Text = vars.obspedx;

        }

        private void Obsped_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            vars.obspedx = obsped.Text;
        }

        private void Btn_tipopgto_Click(object sender, EventArgs e)
        {

            Intent i = new Intent(this, typeof(at_pesquisatipopgto));

            i.PutExtra("id_pagamento", 0);
            StartActivityForResult(i, 0);

        }

        private void Listavenda_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {

            cl_vendasst item_selecionado = VENDAS1[e.Position];

            AlertDialog.Builder caixa_editar_eliminar = new AlertDialog.Builder(this);
            caixa_editar_eliminar.SetTitle("EDITAR | ELIMINAR");
            caixa_editar_eliminar.SetMessage(item_selecionado.descricao);
            // caixa_editar_eliminar.SetCancelable(false);

            caixa_editar_eliminar.SetPositiveButton("Editar", delegate { });
            caixa_editar_eliminar.SetNegativeButton("Eliminar", delegate { EliminarIitem(item_selecionado.id_produto); });

            caixa_editar_eliminar.Show();

            
        }

        public void EliminarIitem(int id_produto)
        {

            Cl_gestor.NOM_QUERY("Delete from tmp_vendas_prod where id_produto = " + id_produto);

            ConstroiListaProdutosVendas();

        }



        private void Desconto_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {

            DataTable dados1 = Cl_gestor.EXE_QUERY("select sum(total) as tot  from  tmp_vendas_prod ");
            
            if (dados1.Rows.Count != 0)
            {
                xdesconto();
            }           
        }

        private void Btn_gravar_Click(object sender, EventArgs e)
        {
            /////////////////////////gravar nha tabela de pedidos
            ///
            if (vars.numerocliente > 0)
            { 
                    int pgto = 0;
                    string tipopgto = "";
                    DataTable dadosx = Cl_gestor.EXE_QUERY("select * from tmptipopgto ");
                    if (dadosx.Rows.Count != 0)
                    {
                        pgto = Convert.ToInt32(dadosx.Rows[0]["id_tipopgto"]);
                        tipopgto = dadosx.Rows[0]["descricao"].ToString();
                    }

                        DataTable dados = Cl_gestor.EXE_QUERY("select * from tmp_vendas_prod ");
                if (dados.Rows.Count != 0)
                {

                    DataTable dados1 = Cl_gestor.EXE_QUERY("select sum(total) as tot  from  tmp_vendas_prod ");
                    decimal varx1 = 0;
                    if (dados1.Rows.Count != 0)
                    {
                        decimal descontox1 = 0;


                        string xt2 = desconto.Text.ToString(); //.Substring(3, desconto.Text.Length - 3);
                        xt2 = xt2.Replace(".", ",");
                        if (xt2 == String.Empty)
                        {
                            xt2 = "0";
                        }

                        decimal xtv2 = Math.Round(Convert.ToDecimal(xt2), 2);

                        if (xtv2 > 0)
                        {
                            descontox1 = xtv2;
                        }

                        varx1 = Convert.ToDecimal(dados1.Rows[0]["tot"]) - descontox1;
                    }
                    else
                    {
                        varx1 = 0;
                    }

                    string xt2z = desconto.Text.ToString();   //.Substring(3, desconto.Text.Length - 3);
                    xt2z = xt2z.Replace(",", ".");


                    if (xt2z == String.Empty)
                    {
                        xt2z = "0";
                    }

                    decimal xtv21 = Math.Round(Convert.ToDecimal(xt2z), 2);

                    decimal descontox = 0;
                    if (xtv21 > 0)
                    {
                        descontox = xtv21;
                    }
                    else
                    {
                        descontox = 0;
                    }


                    string obspedz = obsped.Text.ToString();

                    DBNull xd = null;
                    //string xd = "0000/00/00";
                    //DateTime data1 = DateTime.Now.AddDays();

                    List<SQLparametro> parametro1 = new List<SQLparametro>();
                    parametro1.Add(new SQLparametro("@id_pedido", Cl_gestor.ID_DISPONIVEL("vendas", "id_pedido")));
                    parametro1.Add(new SQLparametro("@id_cliente", vars.numerocliente));
                    parametro1.Add(new SQLparametro("@nome", vars.nomeclientesel));
                    parametro1.Add(new SQLparametro("@id_pgto", vars.nropgto));
                    parametro1.Add(new SQLparametro("@desconto", vars.desconto)); //descontox
                    parametro1.Add(new SQLparametro("@total", varx1));
                    parametro1.Add(new SQLparametro("@data", DateTime.Now));
                    parametro1.Add(new SQLparametro("@datasincro", DateTime.MinValue));
                    parametro1.Add(new SQLparametro("@id_formapgto", pgto));
                    parametro1.Add(new SQLparametro("@tipopagto", tipopgto));
                    parametro1.Add(new SQLparametro("@obsped", obspedz));
                    
                    if ((vars.alterarped) && (vars.dupliped))
                    {

                        int xpedc = vars.npedidoaltera;
                        parametro1.Add(new SQLparametro("@xpedc", xpedc));
                        Cl_gestor.NOM_QUERY(
                       "update  vendas set id_cliente = @id_cliente,nome = @nome,id_pgto = @id_pgto,desconto = @desconto, total = @total, " +
                       " data = @data,datasincro = @datasincro,id_formapgto = @id_formapgto,tipopagto = @tipopagto where id_pedido = @xpedc ", parametro1);

                    }
                    else
                    {
                        Cl_gestor.NOM_QUERY(
                        "insert into vendas (id_pedido,id_cliente,nome,id_pgto,desconto,total,data,datasincro,id_formapgto,tipopagto,obsped)values ( " +
                        "@id_pedido," +
                        "@id_cliente," +
                        "@nome," +
                        "@id_pgto ," +
                        "@desconto ," +
                        "@total ," +
                        "@data ," +
                        "@datasincro," +
                        "@id_formapgto," +
                        "@tipopagto," +
                        "@obsped)", parametro1);
                    }




                    if (vars.alterarped)
                    {
                        int xpedc = vars.npedidoaltera;
                        Cl_gestor.NOM_QUERY("delete from vendasprd where id_pedido = " + xpedc);

                    }


                    DataTable dadosnew = Cl_gestor.EXE_QUERY("select * from tmp_vendas_prod ");
                    foreach (DataRow linha in dadosnew.Rows)
                    {



                        List<SQLparametro> parametro = new List<SQLparametro>();

                        if ((vars.alterarped) && (vars.dupliped))
                        {
                            int xpedc = vars.npedidoaltera;
                            parametro.Add(new SQLparametro("@id_pedido", xpedc));
                        }
                        else
                        {
                            parametro.Add(new SQLparametro("@id_pedido", Cl_gestor.ID_ULTIMO("vendas", "id_pedido")));
                        }

                        int numerador = Cl_gestor.ID_ULTIMO("vendasprd", "seq") + 1;
                        int zero = 0;
                        parametro.Add(new SQLparametro("@id_produto", linha["id_produto"]));
                        parametro.Add(new SQLparametro("@descricao", linha["descricao"]));
                        parametro.Add(new SQLparametro("@quantidade", linha["quantidade"]));
                        parametro.Add(new SQLparametro("@preco", linha["preco"]));
                        parametro.Add(new SQLparametro("@desconto", linha["desconto"]));
                        parametro.Add(new SQLparametro("@precodesconto", linha["precodesconto"]));
                        parametro.Add(new SQLparametro("@total", linha["total"]));
                        parametro.Add(new SQLparametro("@nrovendabco", zero));
                        parametro.Add(new SQLparametro("@seq", numerador));
                        


                        Cl_gestor.NOM_QUERY(
                            "insert into vendasprd (id_pedido, id_produto, descricao, quantidade, preco, desconto, precodesconto, total, nrovendabco, seq) values ( " +
                            "@id_pedido," +
                            "@id_produto," +
                            "@descricao," +
                            "@quantidade," +
                            "@preco," +
                            "@desconto," +
                            "@precodesconto," +
                            "@total ," +
                            "@nrovendabco ," +
                            "@seq) ", parametro);


                    }


                    Cl_gestor.NOM_QUERY("delete from tmp_vendas_prod");
                    Cl_gestor.NOM_QUERY("delete from tmp_clientes");
                    Cl_gestor.NOM_QUERY("delete from tmp_pagamento");
                    Cl_gestor.NOM_QUERY("delete from tmptipopgto");



                    vars.numerocliente = 0;
                    vars.numeroproduto = 0;
                    vars.nomeproduto = "";
                    vars.precoproduto = 0;
                    vars.quantidadeproduto = 0;
                    vars.precoprodutodesc = 0;
                    vars.totalproduto = 0;
                    vars.desconto = 0;
                    vars.numerocliente = 0;
                    vars.dupliped = false;
                    StartActivity(typeof(at_menu_inicial));

                    Toast.MakeText(this, "Pedido Gravado", ToastLength.Short).Show();
                    vars.obspedx = "";
                    this.Finish();

                }
            }
            else
            {
                Toast.MakeText(this, "Problemas com o cliente, selecione novamente ", ToastLength.Short).Show();
            }

            
        }

        private void Btn_cancelar_Click(object sender, EventArgs e)
        {
              Cl_gestor.NOM_QUERY("delete from  tmp_vendas_prod");
              Cl_gestor.NOM_QUERY("delete from  tmp_clientes");
              Cl_gestor.NOM_QUERY("delete from  tmp_pagamento");
              Cl_gestor.NOM_QUERY("delete from  tmptipopgto");
              Cl_gestor.NOM_QUERY("delete from  tmptipopgto");

                vars.numerocliente = 0;
                vars.numeroproduto = 0;
                vars.nomeproduto = "";
                vars.precoproduto = 0;
                vars.quantidadeproduto = 0;
                vars.precoprodutodesc = 0;
                vars.totalproduto = 0;
                vars.desconto = 0;
                vars.numerocliente = 0;
            Toast.MakeText(this,"Pedido Cancelado...", ToastLength.Short).Show();
            StartActivity(typeof(at_menu_inicial));
            this.Finish();
        }

        private void Btn_produto_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(at_pesquisaproduto));

            i.PutExtra("id_produto", 0);
            StartActivityForResult(i, 0);
            this.Finish();
        }

        private void Btn_formapgto_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(at_pesquisapagamento));

            i.PutExtra("id_pagamento", 0);
            StartActivityForResult(i, 0);
        }

        private void Btn_clientes_Click(object sender, EventArgs e)
        {
            // StartActivity(typeof(at_pesquisacliente));

            Intent i = new Intent(this, typeof(at_pesquisacliente));

            i.PutExtra("id_clientes", 0);
            StartActivityForResult(i, 0);


        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {

                //DataTable dados = Cl_gestor.EXE_QUERY(" select * from tmp_clientes ");
                // nome_cliente2.Text = dados.Rows[0]["nome"].ToString();

                ConstroiListavendas();


            }
        }


        public void ConstroiListavendas()
        {

            DataTable dados = Cl_gestor.EXE_QUERY(" select * from tmp_clientes ");

            if (dados.Rows.Count != 0)
            {
                nome_cliente2.Text = dados.Rows[0]["nome"].ToString();
                vars.numerocliente =Convert.ToInt32(dados.Rows[0]["id_cliente"]);
            }
            else
            {
                nome_cliente2.Text = "Selecione um cliente";
            }

            DataTable dados1 = Cl_gestor.EXE_QUERY(" select * from tmp_pagamento ");

            if (dados1.Rows.Count != 0)
            {
                nome_forma.Text = dados1.Rows[0]["descricao"].ToString();
                vars.nropgto = Convert.ToInt32(dados1.Rows[0]["id_pagamento"]);
            }
            else
            {
                nome_forma.Text = "Selecione uma forma de pagamento";
                                                
            }


            DataTable dadosx = Cl_gestor.EXE_QUERY(" select * from tmptipopgto ");

            if (dadosx.Rows.Count != 0)
            {
                nome_tipo.Text = dadosx.Rows[0]["descricao"].ToString();
               
            }
            else
            {
                nome_forma.Text = "Selecione um tipo de pagamento";

            }



            if (vars.desconto > 0)
            {
                //desconto.Text = vars.desconto.ToString();
                desconto.Text = vars.desconto.ToString(); // string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Decimal.Parse(vars.desconto.ToString()));
                xdesconto();

                
            }
            else
            {
            
                //DataTable dados2 = Cl_gestor.EXE_QUERY("select sum(total) as tot  from  tmp_vendas_prod ");
                //if (dados2.Rows.Count != 0) 
                //{

                //  decimal varx10 = Convert.ToDecimal(dados2.Rows[0]["tot"].ToString());

                //    totalgeral.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", (varx10));


                //}
            
            }
        }

        private void ConstroiListaProdutosVendas()
        {
            VENDAS1 = new List<cl_vendasst>();
            DataTable dados = Cl_gestor.EXE_QUERY("select * from  tmp_vendas_prod order by descricao asc");
            foreach (DataRow linha in dados.Rows)
            {
                VENDAS1.Add(new cl_vendasst()
                {
                    id_produto = Convert.ToInt32(linha["id_produto"]),
                    descricao = linha["descricao"].ToString(),
                    precodesconto = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}",Decimal.Parse(linha["precodesconto"].ToString())),
                    total = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Decimal.Parse(linha["total"].ToString())),
                    quantidade = linha["quantidade"].ToString(),

                    // quantidade = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Decimal.Parse(linha["quantidade"].ToString())),


                });

            }

            int x = 0;

            NOMES1 = new List<string>();
            foreach (cl_vendasst produto in VENDAS1)
            {
                NOMES1.Add("Prod.:" + produto.descricao + "\n QTD " + produto.quantidade + " UN." + produto.precodesconto + " TOTAL " + produto.total);
                
            }



            ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.TestListItem, NOMES1);
            

            listavenda.Adapter = adaptador;


            DataTable dados1 = Cl_gestor.EXE_QUERY("select sum(total) as tot  from  tmp_vendas_prod ");

            if (dados1.Rows.Count != 0)
            {
               // total.Text = dados1.Rows[0]["tot"].ToString();
                total.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", dados1.Rows[0]["tot"]);
                totalgeral.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", dados1.Rows[0]["tot"]);
            }
            else
            {
                //total.Text = "0,00";
                decimal vc = 0;
                total.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vc.ToString());
            }


                ////ApresentaTotalProdutos(PRODUTOSS.Count);
                ///

            
        }


        private void xdesconto()
        {

            string xtz = desconto.Text.ToString(); //  .Substring(3, desconto.Text.Length - 3);
            if ((xtz != "") && (xtz != null))
            {

                DataTable dados1 = Cl_gestor.EXE_QUERY("select sum(total) as tot  from  tmp_vendas_prod ");
                decimal varx1 = 0;
                decimal varx2 = 0;
                string x = dados1.Rows[0]["tot"].ToString();

                if (x != string.Empty)
                {
                    varx1 = Convert.ToDecimal(dados1.Rows[0]["tot"]);
                }
                else
                {
                    varx1 = 0;
                }

                decimal desc1 = 0;


                string xt = desconto.Text.ToString();  //.Substring(3, desconto.Text.Length - 3);
                xt = xt.Replace(".", ",");
                decimal xtv = Math.Round(Convert.ToDecimal(xt), 2);

                if (xtv > 0)
                {
                    desc1 = xtv;
                }
                else
                {
                    desc1 = 0;
                }



                if ((varx1 > 0) && (desc1 > 0))
                {

                    string xte = desconto.Text.ToString();  //.Substring(3, desconto.Text.Length - 3);
                    xte = xte.Replace(".", ",");
                    decimal xtve = Math.Round(Convert.ToDecimal(xte), 2);

                    varx2 = varx1 - xtve;


                    totalgeral.Text =  string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", (varx2));
                    //totalgeral.Text = varx2.ToString();
                    vars.desconto = Math.Round(Convert.ToDecimal(xte), 2);  //varx2
                }
                else
                {
                    totalgeral.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", (varx1));
                    // totalgeral.Text = varx1.ToString();
                }

            }
        }



    }
       
}