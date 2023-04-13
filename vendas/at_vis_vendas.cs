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
using System.Collections;
using Android.Support.V4.Content;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mime;
using Android.Util;
using Java.IO;

namespace vendas
{
    [Activity(Label = "at_vis_vendas")]
    public class at_vis_vendas : Activity
    {

        ListView lista_pedidos;
        ListView lista_pedidosn;
        TextView label_numero_Vendas;
        List<pedidos> PEDIDOSX;
        List<pedidos> PEDIDOSX1;
        List<string> NOMES;
        List<string> NOMES1;
        Button voltar_v;
        private float[] fi;
        private Bundle subject;
        Intent intent_temp;
        public static Activity fa;
        public decimal Total;
        public decimal TotalSDesconto;
        public decimal TotalDesc;
        public decimal CountDesc;
        public decimal CountSDesc;
        public int qtd;
       
            
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           // Create your application here
            SetContentView(Resource.Layout.layout_vis_vendas);
            fa = this;

            lista_pedidos = FindViewById<ListView>(Resource.Id.lista_pedidos);
            lista_pedidosn = FindViewById<ListView>(Resource.Id.lista_pedidosn);

            label_numero_Vendas = FindViewById<TextView>(Resource.Id.label_numero_vendas);
            voltar_v = FindViewById<Button>(Resource.Id.voltar_v);
            ConstroiListapedidos();
            ConstroiListapedidos2();


            lista_pedidos.ItemLongClick += Lista_pedidos_ItemLongClick;
            lista_pedidosn.ItemLongClick += Lista_pedidosn_ItemLongClick;
            voltar_v.Click += Voltar_v_Click;
            //  lista_clientes.ItemLongClick += Lista_clientes_ItemLongClick; 
             

        }

      
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                //at_clientes at = new at_clientes();
                ConstroiListapedidos();
                ConstroiListapedidos2();
            }
        }

        private void Voltar_v_Click(object sender, EventArgs e)
        {
            this.Finish();
        }


        private void Lista_pedidos_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {

            pedidos pedido_selecionado = PEDIDOSX[e.Position];

            //caixa_editar_eliminar.Show();
            string handllerNotingButton;
            string orped = "P";
            var dlgAlert = (new AlertDialog.Builder(this)).Create();
            dlgAlert.SetMessage("Selecione uma opção ");
            dlgAlert.SetTitle("Visializar ou Compartilhar");
            dlgAlert.SetButton("Visualizar ", delegate { VisualizaPedido(pedido_selecionado.id_pedido); });
            dlgAlert.SetButton2("Gerar Relatorio sincronização ", delegate { gerarpdf2(pedido_selecionado.id_pedido, false); });
            dlgAlert.SetButton3("Whatsap", delegate { gerarpdf1(pedido_selecionado.id_pedido,false); });
           
            dlgAlert.Show();

            

        }

        private void Lista_pedidosn_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {

            pedidos pedido_selecionado = PEDIDOSX1[e.Position];

               //caixa_editar_eliminar.Show();
            string handllerNotingButton;
            string orped = "P";
            var dlgAlert = (new AlertDialog.Builder(this)).Create();
            dlgAlert.SetMessage("Selecione uma opção ");
            dlgAlert.SetTitle("Visializar ou Compartilhar");
            dlgAlert.SetButton("Visualizar ", delegate { VisualizaPedido(pedido_selecionado.id_pedido); });
            dlgAlert.SetButton2("Liberar Sincronização", delegate { liberarpedido(pedido_selecionado.id_pedido); });
            dlgAlert.SetButton3("WhatsApp", delegate { gerarpdf1(pedido_selecionado.id_pedido, false); });
            
            dlgAlert.Show();

        }


        public void VisualizaPedido(int id_pedido)
        {



            vars.pedidoselecionadodet = id_pedido;

           StartActivity(typeof(at_vis_prod_ped));

           
           ConstroiListapedidos();
           ConstroiListapedidos2();

            fa.Finish();
        }


        public void EliminarPedido(int id_pedido)
        {

            Cl_gestor.NOM_QUERY("Delete from vendas where id_pedido = " + id_pedido);
            Cl_gestor.NOM_QUERY("Delete from vendasprd where id_pedido = " + id_pedido);

            ConstroiListapedidos();
            ConstroiListapedidos2();

        }

        private void ConstroiListapedidos()
        {


            List<SQLparametro> parametrox = new List<SQLparametro>();
            parametrox.Add(new SQLparametro("@datax", DateTime.MinValue));

            int cv1 = 0;

            PEDIDOSX = new List<pedidos>();
          

                PEDIDOSX = new List<pedidos>();
            //DataTable dados = Cl_gestor.EXE_QUERY("select * from vendas order by id_pedido asc");
            DataTable dados = Cl_gestor.EXE_QUERY("select * from vendas  where datasincro != @datax order by id_pedido asc ", parametrox);
            foreach (DataRow linha in dados.Rows)
            {
                PEDIDOSX.Add(new pedidos()
                {

                    id_pedido = Convert.ToInt32(linha["id_pedido"]),
                    nome = linha["nome"].ToString(),
                    nrovendabco = linha["nrovendabco"].ToString(),
                    total = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Decimal.Parse(linha["total"].ToString())),
                    datasincro = Convert.ToDateTime(linha["datasincro"]),
                    data = Convert.ToDateTime(linha["data"]),
                    checar = linha["checar"].ToString()

                });

            }

            NOMES = new List<string>();
            foreach (pedidos pedido in PEDIDOSX)
            {
                char pad = '.';
                string xnome1, datax,checkx;
                string xnome = pedido.nome.PadRight(25, pad);
                xnome1 = xnome.Substring(0, 18);
                checkx = pedido.checar;


                NOMES.Add("Ped App.:" + pedido.id_pedido.ToString().PadLeft(5) + " Cliente.:" + xnome1.Trim() + "\n" + "Ped Servidor:" + pedido.nrovendabco.ToString().PadLeft(5) + " Total " + pedido.total.ToString().PadLeft(10) + "\n Dt Sinc    " + pedido.datasincro.ToString() + "\n Dt Emissão " + pedido.data.ToString()+ "  CK." + checkx.Trim());
            }
            // ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.TestListItem, NOMES);
            ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemActivated1, NOMES);
            lista_pedidos.Adapter = adaptador;

            

        }

        private void ConstroiListapedidos2()
        {

            List<SQLparametro> parametrox = new List<SQLparametro>();
            parametrox.Add(new SQLparametro("@datax", DateTime.MinValue));

            int cv1 = 0;
          
            PEDIDOSX1 = new List<pedidos>();
            DataTable dadosy = Cl_gestor.EXE_QUERY("select * from vendas  where  datasincro == @datax order by id_pedido asc ", parametrox);
            foreach (DataRow linha in dadosy.Rows)
            {
                PEDIDOSX1.Add(new pedidos()
                {

                    id_pedido = Convert.ToInt32(linha["id_pedido"]),
                    nome = linha["nome"].ToString(),
                    nrovendabco = linha["nrovendabco"].ToString(),
                    total = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Decimal.Parse(linha["total"].ToString())),
                    datasincro = Convert.ToDateTime(linha["datasincro"]),
                    data = Convert.ToDateTime(linha["data"])


                });

            }

            NOMES1 = new List<string>();
            foreach (pedidos pedido in PEDIDOSX1)
            {
                char pad = '.';
                string xnome1, datax;
                string xnome = pedido.nome.PadRight(25, pad);
                xnome1 = xnome.Substring(0, 18);

                NOMES1.Add("Ped App:" + pedido.id_pedido.ToString().PadLeft(5) + " Cliente.:" + xnome1.Trim() + "\n" + "Ped. Servidor:" + pedido.nrovendabco.ToString().PadLeft(5) + " Total " + pedido.total.ToString().PadLeft(10) + "\n Dt Sinc    " + pedido.datasincro.ToString() + "\n Dt Emissão " + pedido.data.ToString());
            }
            // ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.TestListItem, NOMES);
            ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemActivated1, NOMES1);
            
            lista_pedidosn.Adapter = adaptador;

        }



        private void liberarpedido(int nroped)
        {

            

            string handllerNotingButton;
            string orped = "P";
            var dlgAlert = (new AlertDialog.Builder(this)).Create();
            dlgAlert.SetMessage("Selecione uma opção ");
            dlgAlert.SetTitle("Liberar Sincronização");
            dlgAlert.SetButton("Liberar apenas este  ", delegate { liberarpedido1(nroped); });

            
            //dlgAlert.SetButton2("Liberar todos acima deste ", delegate { liberarpedido2(nroped); });
            

            dlgAlert.Show();



        }


        private void liberarpedido1(int nroped)
        {
            int zero = 0;
            DataTable dados = Cl_gestor.EXE_QUERY("update vendas set datasincro = '0001-01-01 00:00:00' where id_pedido  = " + nroped);
            DataTable dadosy = Cl_gestor.EXE_QUERY("update vendas set  nrovendabco = "+ zero + "  where id_pedido  = " + nroped);
            DataTable dadosx = Cl_gestor.EXE_QUERY("update vendasprd set nrovendabco = " + zero + "  where id_pedido  = " + nroped);

            ConstroiListapedidos();
        }
        private void liberarpedido2(int nroped)
        {

            DataTable dados = Cl_gestor.EXE_QUERY("update vendas set datasincro = '0001-01-01 00:00:00' where id_pedido  >= " + nroped);

            ConstroiListapedidos();
        }





        private void emailx(int nroped, bool tipo, string orped)
        {

            vars.npedidoemail = nroped;
            vars.pedis = tipo;
            vars.pedidoorcamento = "P";


            StartActivity(typeof(at_email));


        }



        // esse e onde envia wats app de verdade
        public  void gerarpdf1(int nroped,bool tipo)
        {


            

            string pasta_pdf;

            pasta_pdf = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "pdf");

            if (!Directory.Exists(pasta_pdf))
            {
                Directory.CreateDirectory(pasta_pdf);
            }

            FileStream fs = new FileStream(pasta_pdf + @"/documento1.pdf", FileMode.Create);


            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);


            document.Open();


            string codigo, descricao, precodesconto, preco, ctotal, quantidade, soma, codigox;

            Font fonte2 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 16);
            Font fonte = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12);
            Font fonte3 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9);

            string nome1x ="";
            string endereco1x = "";
            string bairro1x = "";
            string cidade1x = "";
            string fone1x = "";

            DataTable dados21 = Cl_gestor.EXE_QUERY("select * from parametrocli ");
            if (dados21.Rows.Count > 0)
            {
                 nome1x = dados21.Rows[0]["nome"].ToString();
                 endereco1x = dados21.Rows[0]["endereco"].ToString();
                 bairro1x = dados21.Rows[0]["bairro"].ToString();
                 cidade1x = dados21.Rows[0]["cidade"].ToString();
                 fone1x = dados21.Rows[0]["fone"].ToString();
            }


            document.Add(new Paragraph("01 - Pedido de Vendas Mobile ", fonte2));
            document.Add(new Paragraph(""+ nome1x.Trim(), fonte2));
            document.Add(new Paragraph(" Endereço...: " + endereco1x.Trim(), fonte3));
            document.Add(new Paragraph(" Bairro.....: " + bairro1x.Trim()+ " - "+ cidade1x.Trim(), fonte3));
            document.Add(new Paragraph(" Fone.......: " + fone1x.Trim(), fonte3));

            document.Add(new Paragraph("_______________________________________________________________________________"));

            DataTable dados2 = Cl_gestor.EXE_QUERY("select * from vendas where id_pedido = " + nroped);


            string nrocli = dados2.Rows[0]["id_cliente"].ToString();
            string nrovend = dados2.Rows[0]["id_pgto"].ToString();
            string obsped1 = dados2.Rows[0]["obsped"].ToString();
            string nrobanco = dados2.Rows[0]["id_pedido"].ToString();

            DataTable dados3 = Cl_gestor.EXE_QUERY("select * from clientes where id_cliente = " + nrocli);

            document.Add(new Paragraph("Cliente....:" + dados3.Rows[0]["nome"].ToString() + "    Nro Pedido  ......  " + nrobanco.ToString(), fonte3));
            document.Add(new Paragraph("Endereço...:" + dados3.Rows[0]["endereco"].ToString() + "     " + "Bairro..: " + dados3.Rows[0]["Bairro"].ToString(), fonte3));
            document.Add(new Paragraph("Cidade.....: " + dados3.Rows[0]["cidade"].ToString(), fonte3));
            document.Add(new Paragraph("Telefone...: " + dados3.Rows[0]["telefone"].ToString() + "     " + "Contato..: " + dados3.Rows[0]["contato"].ToString(), fonte3));

            DataTable dados4 = Cl_gestor.EXE_QUERY("select * from pagamento where id_pagamento = " + nrovend);
            document.Add(new Paragraph("Form Pgto..: " + dados4.Rows[0]["descricao"].ToString(), fonte3));
            document.Add(new Paragraph("Observação.: " + obsped1.ToString(), fonte3));

            document.Add(new Paragraph("                  "));

            document.Add(new Paragraph("_______________________________________________________________________________"));

            document.Add(new Paragraph("                  "));


            PdfPTable table = new PdfPTable(6);


            Paragraph Coluna1 = new Paragraph("Item", fonte);
            Paragraph Coluna2 = new Paragraph("Codigo", fonte);
            Paragraph Coluna3 = new Paragraph("Descricao", fonte);
            Paragraph Coluna4 = new Paragraph("Preco", fonte);
            Paragraph Coluna5 = new Paragraph("Quant", fonte);
            Paragraph Coluna6 = new Paragraph("Preço Desconto", fonte);
            Paragraph Coluna7 = new Paragraph("Total", fonte);


            var cell1 = new PdfPCell();
            var cell2 = new PdfPCell();
            var cell3 = new PdfPCell();
            var cell4 = new PdfPCell();
            var cell5 = new PdfPCell();
            var cell6 = new PdfPCell();
            var cell7 = new PdfPCell();
            var cell8 = new PdfPCell();
            var cell9 = new PdfPCell();

            fi = new float[] { 3F, 12F, 12F, 4F, 5F, 5F, 5F };
            table = new PdfPTable(fi);

            cell1.AddElement(Coluna1);
            cell2.AddElement(Coluna2);
            cell3.AddElement(Coluna3);
            cell4.AddElement(Coluna4);
            cell5.AddElement(Coluna5);
            cell6.AddElement(Coluna6);
            cell7.AddElement(Coluna7);

            table.AddCell(cell1);
            table.AddCell(cell2);
            table.AddCell(cell3);
            table.AddCell(cell4);
            table.AddCell(cell5);
            table.AddCell(cell6);
            table.AddCell(cell7);


            //
            //S = new List<cl_vendasst>();
            DataTable dados = Cl_gestor.EXE_QUERY("select * from vendasprd A inner join Produtos B on A.id_produto = B.id_produto where id_pedido = " + nroped);  //////+  
            foreach (DataRow linha in dados.Rows)
            {


                codigo = linha["id_produto"].ToString(); 
                Phrase aa = new Phrase(codigo, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                PdfPCell cellxa = new PdfPCell(aa);
                table.AddCell(cellxa);

                codigo = linha["codbar"].ToString();
                Phrase a = new Phrase(codigo, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                PdfPCell cellx = new PdfPCell(a);
                table.AddCell(cellx);

                descricao = linha["descricao"].ToString();
                Phrase b = new Phrase(descricao, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                PdfPCell cellx1 = new PdfPCell(b);
                table.AddCell(cellx1);

                preco = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha["preco"]); 
                Phrase c = new Phrase(preco, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                PdfPCell cellx2 = new PdfPCell(c);
                cellx2.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cellx2);

                quantidade = linha["quantidade"].ToString();
                Phrase d = new Phrase(quantidade, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                PdfPCell cellx3 = new PdfPCell(d);
                cellx3.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cellx3);

                precodesconto = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha["precodesconto"]);
                Phrase e = new Phrase(precodesconto, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                PdfPCell cellx4 = new PdfPCell(e);
                cellx2.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cellx4);


                ctotal = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha["total"]);
                Phrase f = new Phrase(ctotal, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                PdfPCell cellx5 = new PdfPCell(f);
                cellx4.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cellx5);
                                                                                    

            }

            int count = Convert.ToInt32(dados.Rows.Count);

            TotalDesc = 0;
            Total = 0;
            CountDesc = 0;
            TotalSDesconto = 0;
            CountSDesc = 0;

            for (var i = 0; i < count; i++)
            {

                
                Total = Total + Convert.ToDecimal(dados.Rows[i]["total"]);
                CountSDesc = Convert.ToDecimal(dados.Rows[i]["preco"])*Convert.ToInt32(dados.Rows[i]["quantidade"]);
                TotalSDesconto += CountSDesc;
                CountDesc = (Convert.ToDecimal(dados.Rows[i]["preco"]) - Convert.ToDecimal(dados.Rows[i]["precodesconto"]));
                TotalDesc += CountDesc;

            }

            var recebe = Total;

            document.Add(table);

            //**********************************************************************************************************************************

            document.Add(new Paragraph("                                              "));

            PdfPTable tabley = new PdfPTable(2);

            var cell1y = new PdfPCell();
            var cell2y = new PdfPCell();   

            table.AddCell(cell7);
            table.AddCell(cell8);

            document.Add(new Paragraph("                       "));
            document.Add(new Paragraph("_______________________________________________________________________________"));
            document.Add(new Paragraph("                       "));

            string totx = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", TotalSDesconto);
            string totxt = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", (TotalSDesconto - Convert.ToDecimal(dados2.Rows[0]["total"]))*-1);
            string totxtu = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", dados2.Rows[0]["total"]);
            
            string ttd = "Sub-Total:";
            Phrase axd = new Phrase(ttd, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
            PdfPCell cellxxd = new PdfPCell(axd);
            cellxxd.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabley.AddCell(cellxxd);

            string tt = totx.ToString();
            Phrase ax = new Phrase(tt, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
            PdfPCell cellxx = new PdfPCell(ax);
            cellxx.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabley.AddCell(cellxx);

            string ttdx = "Desconto:";
            Phrase axdx = new Phrase(ttdx, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
            PdfPCell cellxxdx = new PdfPCell(axdx);
            cellxxdx.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabley.AddCell(cellxxdx);

            string ttv = totxt.ToString();
            Phrase axv = new Phrase(ttv, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
            PdfPCell cellxxv = new PdfPCell(axv);
            cellxxv.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabley.AddCell(cellxxv);

            string ttdxd = "Total:";
            Phrase axdxd = new Phrase(ttdxd, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
            PdfPCell cellxxdxd = new PdfPCell(axdxd);
            cellxxdxd.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabley.AddCell(cellxxdxd);

            string ttvd = totxtu.ToString();
            Phrase axvd = new Phrase(ttvd, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
            PdfPCell cellxxvd = new PdfPCell(axvd);
            cellxxvd.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabley.AddCell(cellxxvd);


            document.Add(tabley);






            //document.Add(new Paragraph("Cond Pgto.:                         Prazo Medio " , fonte3));
           // document.Add(new Paragraph("Documento.:                         Vencimento                                   Valor  ", fonte3));

            document.Add(new Paragraph("OBS  ", fonte3));
            document.Add(new Paragraph("ORCAMENTO SUJEITO A CONFIRNAÇÃO DE PREÇO, PRAZO E ESTOQUE ", fonte3));
            document.Add(new Paragraph("RECEBIDO VIA MOBILE  ", fonte3));


            //document.Add(new Paragraph(""));
            //document.Add(new Paragraph("_______________________________________________________________________________"));
            //document.Add(new Paragraph("ESTOU CIENTE QUE RECEBI A NOTA FISCAL N________ E AS DUPLICATAS REFERNTES A ESTE PEDIDO.", fonte3));
            //document.Add(new Paragraph("DATA _____/_____/_______", fonte3));
            //document.Add(new Paragraph(""));
            //document.Add(new Paragraph("ASSINATURA_________________________________RG:___________________", fonte3));
            //document.Add(new Paragraph("_______________________________________________________________________________"));
            //document.Add(new Paragraph("NA FALTA DE MERCADORIA, FAVOR RELACIONAR ABAIXO. CASO NAO CONSTYAR, NAO NOS ", fonte3));
            //document.Add(new Paragraph("RESPONSABILIZAREMOS PELA ENTREGA DOS PRODUTOS.  ", fonte3));
            //document.Add(new Paragraph("__"));
            //document.Add(new Paragraph("__"));
            //document.Add(new Paragraph("_______________________________________________________________________________"));
            //document.Add(new Paragraph("PARA MELHOR VISUALIZACAO DE NOSSOS PRODUTOS, ACESSE O NOSSO SITE: www.ggpiscinas.com.br ", fonte3));
            //document.Add(new Paragraph("                                                                  ggpiscinas@gmail.com ", fonte3));






            document.Close();
            

            writer.Close();

            fs.Close();









            Toast.MakeText(Application.Context, "Aguarde..... ", ToastLength.Short).Show();

            if (tipo)
            {
                                              
               
            } 
            else
            {
                compartilhar();
            }
          
        }


        /// <summary>
        /// ////////////////////////////////////////////
        /// </summary>

        public void gerarpdf2(int nroped, bool tipo)
        {




            string pasta_pdf;

            pasta_pdf = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "pdf");

            if (!Directory.Exists(pasta_pdf))
            {
                Directory.CreateDirectory(pasta_pdf);
            }

            FileStream fs = new FileStream(pasta_pdf + @"/documento1.pdf", FileMode.Create);


            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);


            document.Open();


            string codigo, descricao, precodesconto, total, quantidade, soma, codigox;

            Font fonte2 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 16);
            Font fonte = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12);
            Font fonte3 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9);


            string nome1x = "";
            string endereco1x = "";
            string bairro1x = "";
            string cidade1x = "";
            string fone1x = "";



            DataTable dados21 = Cl_gestor.EXE_QUERY("select * from parametrocli ");
            if (dados21.Rows.Count > 0)
            {
                 nome1x = dados21.Rows[0]["nome"].ToString();
                 endereco1x = dados21.Rows[0]["endereco"].ToString();
                 bairro1x = dados21.Rows[0]["bairro"].ToString();
                 cidade1x = dados21.Rows[0]["cidade"].ToString();
                 fone1x = dados21.Rows[0]["fone"].ToString();
            }

            document.Add(new Paragraph("Listagem de - Pedido de Vendas Mobile ", fonte2));
            document.Add(new Paragraph("" + nome1x.Trim(), fonte2));

            DataTable dados2 = Cl_gestor.EXE_QUERY("select * from vendas where id_pedido >= " + nroped);

            for (int ii = 0; ii < dados2.Rows.Count; ii++)
            {

                string nropedido = dados2.Rows[ii]["id_pedido"].ToString();
                string nrocli = dados2.Rows[ii]["id_cliente"].ToString();
                string nrovend = dados2.Rows[ii]["id_pgto"].ToString();
                string obsped1 = dados2.Rows[ii]["obsped"].ToString();
                string nrobanco = dados2.Rows[ii]["nrovendabco"].ToString();

                DataTable dados3 = Cl_gestor.EXE_QUERY("select * from clientes where id_cliente = " + nrocli);
                document.Add(new Paragraph("_______________________________________________________________________________"));

                if (dados3.Rows.Count != 0)
                {
                    document.Add(new Paragraph("Cliente....:" + dados3.Rows[0]["nome"].ToString()+"    Nro Pedido  ......  "+ nrobanco.ToString(), fonte));
                    document.Add(new Paragraph("Endereço...:" + dados3.Rows[0]["endereco"].ToString() + "     " + "Bairro..: " + dados3.Rows[0]["Bairro"].ToString(), fonte3));
                    document.Add(new Paragraph("Cidade.....: " + dados3.Rows[0]["cidade"].ToString(), fonte3));
                    document.Add(new Paragraph("Telefone...: " + dados3.Rows[0]["telefone"].ToString() + "     " + "Contato..: " + dados3.Rows[0]["contato"].ToString(), fonte3));
                }
                else
                {
                    document.Add(new Paragraph("Cliente....:" + " Verifique Cliente ", fonte));
                }
                DataTable dados4 = Cl_gestor.EXE_QUERY("select * from pagamento where id_pagamento = " + nrovend);
                document.Add(new Paragraph("Form Pgto..: " + dados4.Rows[0]["descricao"].ToString(), fonte3));
                document.Add(new Paragraph("Observação.: " + obsped1.ToString(), fonte3));
                document.Add(new Paragraph("                  "));



                PdfPTable table = new PdfPTable(6);


                Paragraph Coluna1 = new Paragraph("Pedido", fonte);
                Paragraph Coluna2 = new Paragraph("Codigo", fonte);
                Paragraph Coluna3 = new Paragraph("Descricao", fonte);
                Paragraph Coluna4 = new Paragraph("Preco", fonte);
                Paragraph Coluna5 = new Paragraph("Quant", fonte);
                Paragraph Coluna6 = new Paragraph("Total", fonte);


                var cell1 = new PdfPCell();
                var cell2 = new PdfPCell();
                var cell3 = new PdfPCell();
                var cell4 = new PdfPCell();
                var cell5 = new PdfPCell();
                var cell6 = new PdfPCell();
                var cell7 = new PdfPCell();
                var cell8 = new PdfPCell();

                fi = new float[] { 4F, 4F, 15F, 5F, 5F, 5F };
                table = new PdfPTable(fi);

                cell1.AddElement(Coluna1);
                cell2.AddElement(Coluna2);
                cell3.AddElement(Coluna3);
                cell4.AddElement(Coluna4);
                cell5.AddElement(Coluna5);
                cell6.AddElement(Coluna6);

                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);
                table.AddCell(cell4);
                table.AddCell(cell5);
                table.AddCell(cell6);

                //PRODUTOSS = new List<cl_vendasst>();
                DataTable dados = Cl_gestor.EXE_QUERY("select * from vendasprd where id_pedido == " + nropedido);  //////+ id_pedido
                foreach (DataRow linha in dados.Rows)
                {

                    codigo = linha["id_pedido"].ToString();
                    Phrase aa = new Phrase(codigo, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                    PdfPCell cellxa = new PdfPCell(aa);
                    table.AddCell(cellxa);

                    codigo = linha["id_produto"].ToString();
                    Phrase a = new Phrase(codigo, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                    PdfPCell cellx = new PdfPCell(a);
                    table.AddCell(cellx);

                    descricao = linha["descricao"].ToString();
                    Phrase b = new Phrase(descricao, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                    PdfPCell cellx1 = new PdfPCell(b);
                    table.AddCell(cellx1);

                    precodesconto = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha["precodesconto"]);
                    Phrase c = new Phrase(precodesconto, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                    PdfPCell cellx2 = new PdfPCell(c);
                    cellx2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cellx2);

                    quantidade = linha["quantidade"].ToString();
                    Phrase d = new Phrase(quantidade, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                    PdfPCell cellx3 = new PdfPCell(d);
                    cellx3.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cellx3);


                    total = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha["total"]);
                    Phrase f = new Phrase(total, new Font(Font.HELVETICA, 8f, Font.NORMAL, Color.BLACK));
                    PdfPCell cellx4 = new PdfPCell(f);
                    cellx4.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cellx4);

                }
                                              
                document.Add(table);
            }
            //**********************************************************************************************************************************

            document.Add(new Paragraph("                                              "));

            PdfPTable tabley = new PdfPTable(2);



            var cell1y = new PdfPCell();
            var cell2y = new PdfPCell();


            
            document.Close();


            writer.Close();

            fs.Close();





            Toast.MakeText(Application.Context, "Aguarde..... ", ToastLength.Short).Show();

            if (tipo)
            {


            }
            else
            {
                compartilhar();
            }

        }

        /////////////////////////////////
        ///

        private void compartilhar()
        {


            Intent email = new Intent(Intent.ActionSend);

            var myDir1 = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);

            string fileName = @"/" + "documento1.pdf";
            string fileNamex = myDir1 + @"/documento1.pdf";

            Java.IO.File myDir = new Java.IO.File(myDir1 + @"/pdf/");
            Java.IO.File file = new Java.IO.File(myDir, fileName);

            var uri = Android.Net.Uri.Parse(file.AbsolutePath);

            email.PutExtra(Android.Content.Intent.ExtraSubject, uri);

            email.PutExtra(Intent.ExtraStream, uri);

            email.SetType("application/pdf");

            StartActivity(Intent.CreateChooser(email, "Enviar ...."));
        }
/////////////////////////////////////////////////////////////


    }
              
}