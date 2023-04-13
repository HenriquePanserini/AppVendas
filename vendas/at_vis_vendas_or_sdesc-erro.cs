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
    [Activity(Label = "at_vis_vendas_or_sdesc")]
    public class at_vis_vendas_or_sdesc : Activity
    {

        ListView lista_pedidos;
        TextView label_numero_Vendas;
        List<pedidos> PEDIDOSX;
        List<string> NOMES;
        Button voltar_v;
        private float[] fi;
        private Bundle subject;
        Intent intent_temp;
        public static Activity fa;
       
        
       
                 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           // Create your application here
            SetContentView(Resource.Layout.layout_vis_vendas_or);
            fa = this;

            lista_pedidos = FindViewById<ListView>(Resource.Id.lista_pedidos);
            label_numero_Vendas = FindViewById<TextView>(Resource.Id.label_numero_vendas);
            voltar_v = FindViewById<Button>(Resource.Id.voltar_v);
            ConstroiListapedidos();

           
            lista_pedidos.ItemLongClick += Lista_pedidos_ItemLongClick;
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
            }
        }

        private void Voltar_v_Click(object sender, EventArgs e)
        {
            this.Finish();
        }


        private void Lista_pedidos_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {

            pedidos pedido_selecionado = PEDIDOSX[e.Position];

            //AlertDialog.Builder caixa_editar_eliminar = new AlertDialog.Builder(this);
            //caixa_editar_eliminar.SetTitle("VISUALIZAR | APAGAR");
            //caixa_editar_eliminar.SetMessage(pedido_selecionado.nome);
            //// caixa_editar_eliminar.SetCancelable(false);
            //vars.nomeclientesel2 = pedido_selecionado.nome;

            //caixa_editar_eliminar.SetPositiveButton("Visualizar", delegate { VisualizaPedido(pedido_selecionado.id_pedido); });
            //caixa_editar_eliminar.SetNegativeButton("Apagar", delegate { EliminarPedido(pedido_selecionado.id_pedido); });



            //caixa_editar_eliminar.Show();
            string handllerNotingButton;
            string orped = "O";
            var dlgAlert = (new AlertDialog.Builder(this)).Create();
            dlgAlert.SetMessage("Selecione uma opção ");
            dlgAlert.SetTitle("Visializar ou Compartilhar");
            dlgAlert.SetButton("Visualizar ", delegate { VisualizaPedido(pedido_selecionado.id_pedido); });
            //dlgAlert.SetButton2("E-mail", delegate { emailx(pedido_selecionado.id_pedido,true,orped); });
            dlgAlert.SetButton2("Whatsap", delegate { gerarpdf1(pedido_selecionado.id_pedido,false); });
          

            dlgAlert.Show();

        }

       
        public void VisualizaPedido(int id_pedido)
        {



            vars.pedidoselecionadodet = id_pedido;

           StartActivity(typeof(at_vis_prod_ped_or));

           
            ConstroiListapedidos();
            fa.Finish();
        }


        public void EliminarPedido(int id_pedido)
        {

            Cl_gestor.NOM_QUERY("Delete from vendas_or where id_pedido = " + id_pedido);
            Cl_gestor.NOM_QUERY("Delete from vendasprd_or where id_pedido = " + id_pedido);

            ConstroiListapedidos();

        }


       


        private void ConstroiListapedidos()
        {



            //     public int id_cliente { get; set; }
            //public string nome { get; set; }
            //public int id_pgto { get; set; }
            //public DateTime data { get; set; }


            PEDIDOSX = new List<pedidos>();
            DataTable dados = Cl_gestor.EXE_QUERY("select * from vendas_or order by id_pedido asc");
            foreach (DataRow linha in dados.Rows)
            {
                PEDIDOSX.Add(new pedidos()
                {
                    id_pedido = Convert.ToInt32(linha["id_pedido"]),
                    nome = linha["nome"].ToString(),
                    total = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Decimal.Parse(linha["total"].ToString())),
                    datasincro = Convert.ToDateTime(linha["datasincro"]),
                    data = Convert.ToDateTime(linha["data"])


                    // data =  Convert.ToDateTime(linha["data"]),
                    //
                    //descricao = linha["descricao"].ToString(),
                    //precodesconto = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Decimal.Parse(linha["precodesconto"].ToString())),
                    //total = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Decimal.Parse(linha["total"].ToString())),
                    //quantidade = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Decimal.Parse(linha["quantidade"].ToString())),

                });

            }

            NOMES = new List<string>();
            foreach (pedidos pedido in PEDIDOSX)
            {
                char pad = '.';
                string xnome1, datax;
                string xnome = pedido.nome.PadRight(25, pad);
                xnome1 = xnome.Substring(0, 18);



                NOMES.Add("Pedido:" + pedido.id_pedido.ToString().PadLeft(5) + " Cliente.: " + xnome1 + "\n" + " Total " + pedido.total.ToString().PadLeft(10) + "\n Dt Sinc    " + pedido.datasincro.ToString() + "\n Dt Emissão " + pedido.data.ToString());
            }
            ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.TestListItem, NOMES);
            lista_pedidos.Adapter = adaptador;


            //ApresentaTotalClientes(CLIENTES.Count);
        }


        private void emailx(int nroped, bool tipo,string orped)
        {

            vars.npedidoemail = nroped;
            vars.pedis = tipo;
            vars.pedidoorcamento = "O";


            StartActivity(typeof(at_email));


        }

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


            string codigo, descricao, precodesconto, total, quantidade, soma, codigox;

            Font fonte2 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 16);
            Font fonte = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14);
            Font fonte3 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9);


            DataTable dados2 = Cl_gestor.EXE_QUERY("select * from vendas_or where id_pedido = " + nroped);


            document.Add(new Paragraph("                                     01 - Orçamento ", fonte2));
            document.Add(new Paragraph("                                      GG PISCINAS ", fonte2));

            document.Add(new Paragraph("_______________________________________________________________________________"));

            document.Add(new Paragraph("Cliente " + dados2.Rows[0]["nome"].ToString(), fonte));
            string nrocli = dados2.Rows[0]["id_cliente"].ToString();


            DataTable dados3 = Cl_gestor.EXE_QUERY("select * from clientes where id_cliente = " + nrocli);


            document.Add(new Paragraph("Endereço..:" + dados3.Rows[0]["endereco"].ToString() + "     " + "Bairro..: " + dados3.Rows[0]["Bairro"].ToString(), fonte));
            document.Add(new Paragraph("Cidade..: " + dados3.Rows[0]["cidade"].ToString() + "     " + "Bairro..: " + dados3.Rows[0]["Bairro"].ToString(), fonte));
            document.Add(new Paragraph("Telefone..: " + dados3.Rows[0]["telefone"].ToString() + "     " + "Contato..: " + dados3.Rows[0]["contato"].ToString(), fonte));


            document.Add(new Paragraph("                  "));
            document.Add(new Paragraph("_______________________________________________________________________________"));

            document.Add(new Paragraph("                  "));


            PdfPTable table = new PdfPTable(6);


            Paragraph Coluna1 = new Paragraph("Item", fonte);
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

            fi = new float[] { 3F, 4F, 15F, 5F, 5F, 5F };
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
            DataTable dados = Cl_gestor.EXE_QUERY("select * from vendasprd_or where id_pedido = " + nroped);  //////+ id_pedido
            foreach (DataRow linha in dados.Rows)
            {

                codigo = linha["id_produto"].ToString();
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

            //**********************************************************************************************************************************

            document.Add(new Paragraph("                                              "));

            PdfPTable tabley = new PdfPTable(2);



            var cell1y = new PdfPCell();
            var cell2y = new PdfPCell();




            table.AddCell(cell7);
            table.AddCell(cell8);



            string totx = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", dados2.Rows[0]["total"]);
            string totxt = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", dados2.Rows[0]["desconto"]);
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






            document.Add(new Paragraph("Cond Pgto.:                         Prazo Medio ", fonte3));
            document.Add(new Paragraph("Documento.:                         Vencimento                                   Valor  ", fonte3));

            document.Add(new Paragraph("OBS  ", fonte3));
            document.Add(new Paragraph("ORCAMENTO SUJEITO A CONFIRNAÇÃO DE PREÇO, PRAZO E ESTOQUE ", fonte3));
            document.Add(new Paragraph("RECEBIDO VIA MOBILE  ", fonte3));


            document.Add(new Paragraph(""));
            document.Add(new Paragraph("_______________________________________________________________________________"));
            document.Add(new Paragraph("ESTOU CIENTE QUE RECEBI A NOTA FISCAL N________ E AS DUPLICATAS REFERNTES A ESTE PEDIDO.", fonte3));
            document.Add(new Paragraph("DATA _____/_____/_______", fonte3));
            document.Add(new Paragraph(""));
            document.Add(new Paragraph("ASSINATURA_________________________________RG:___________________", fonte3));
            document.Add(new Paragraph("_______________________________________________________________________________"));
            document.Add(new Paragraph("NA FALTA DE MERCADORIA, FAVOR RELACIONAR ABAIXO. CASO NAO CONSTYAR, NAO NOS ", fonte3));
            document.Add(new Paragraph("RESPONSABILIZAREMOS PELA ENTREGA DOS PRODUTOS.  ", fonte3));
            document.Add(new Paragraph("__"));
            document.Add(new Paragraph("__"));
            document.Add(new Paragraph("_______________________________________________________________________________"));
            document.Add(new Paragraph("PARA MELHOR VISUALIZACAO DE NOSSOS PRODUTOS, ACESSE O NOSSO SITE: www.ggpiscinas.com.br ", fonte3));
            document.Add(new Paragraph("                                                                  ggpiscinas@gmail.com ", fonte3));






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



    }
              
}