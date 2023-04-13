using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace vendas
{
    [Activity(Label = "at_email")]
    public class at_email : Activity
    {



        Button enviar;
        Button cancelar;
        TextView email;
        TextView assunto;
        TextView mensagem1;
        TextView mensagem2;
        TextView mensagem3;
        TextView mensagem4;
        private float[] fi;
        private float[] fii;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

                        

            SetContentView(Resource.Layout.layout_email);

          
            enviar = FindViewById<Button>(Resource.Id.enviar);
            cancelar = FindViewById<Button>(Resource.Id.cancelar);

            email = FindViewById<TextView>(Resource.Id.email);
            assunto = FindViewById<TextView>(Resource.Id.assunto);
            mensagem1= FindViewById<TextView>(Resource.Id.mensagem1);
            mensagem2 = FindViewById<TextView>(Resource.Id.mensagem2);
            mensagem3 = FindViewById<TextView>(Resource.Id.mensagem3);
            mensagem4 = FindViewById<TextView>(Resource.Id.mensagem4);

            cancelar.Click += Cancelar_Click;
            enviar.Click += Enviar_Click;

            int nropedx2;

            nropedx2 = vars.npedidoemail;
            DataTable dados1 = Cl_gestor.EXE_QUERY("select id_cliente  from  vendas where id_pedido = " + nropedx2);

            int ncli;
            ncli = 0;
            if (dados1.Rows.Count > 0)
                ncli = Convert.ToInt32(dados1.Rows[0]["id_cliente"]);


            DataTable dados2 = Cl_gestor.EXE_QUERY("select email from  clientes where id_cliente = " + ncli);

            string emailx;
            emailx = "";
            if (dados2.Rows.Count > 0)
                emailx = dados2.Rows[0]["email"].ToString();
                email.Text = emailx;

           



        }

        private void Enviar_Click(object sender, EventArgs e)
        {

            Toast.MakeText(Application.Context, "Aguarde.....  Enviando Email ", ToastLength.Short).Show();

            int nroped;
            bool tipo;
            string orped;

            nroped = vars.npedidoemail ;
            tipo = vars.pedis ;
            orped = vars.pedidoorcamento;




            gerarpdf12(nroped, tipo,orped);



        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
           

            this.Finish();
        }


        public void gerarpdf12(int nroped, bool tipo,string orped)
        {

            string pasta_pdf;

            pasta_pdf = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "pdf");

            if (!Directory.Exists(pasta_pdf))
            {
                Directory.CreateDirectory(pasta_pdf);
            }

            FileStream fs = new FileStream(pasta_pdf + @"/documento1.pdf", FileMode.Create);

            //*************************************************************************************************************************************



            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);


            document.Open();


            string codigo, descricao, precodesconto, total, quantidade, soma, codigox;

            Font fonte2 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 16);
            Font fonte = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14);
            Font fonte3 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9);

            DataTable dados2;

            if (orped == "P")
            {
                dados2 = Cl_gestor.EXE_QUERY("select * from vendas where id_pedido = " + nroped);
            }
            else
            {
                dados2 = Cl_gestor.EXE_QUERY("select * from vendas_or where id_pedido = " + nroped);
            }

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


            //
            //S = new List<cl_vendasst>();
            DataTable dados;
            if (orped == "P")
            {
                dados = Cl_gestor.EXE_QUERY("select * from where id_pedido = " + nroped);  //////+ id_pedido
            }
            else
            {
                dados = Cl_gestor.EXE_QUERY("select * from vendasprd_or where id_pedido = " + nroped);  //////+ id_pedido
            }

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



            //*************************************************************************************************************************************

            document.Close();
            // Close the writer instance

            writer.Close();
            // Always close open file handles explicitly

            fs.Close();



            int nro = nroped;
                       
            enviaremail(nro);
           
        }


        private void enviaremail(int nro)
        {

            



            DataTable dados = Cl_gestor.EXE_QUERY("select * from  parametro ");

            string username, password,smtp1;
            username = "tieteonlineinfo@gmail.com";
            password = "t1eteoli";
            smtp1 = "smtp.gmail.com";

            if (dados.Rows.Count > 0)
             smtp1 = dados.Rows[0]["smtp"].ToString();
             username = dados.Rows[0]["usuario"].ToString();
             password = dados.Rows[0]["senha"].ToString();


           

            MailMessage maile = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtp1);
             maile.From = new MailAddress(username);
            maile.To.Add(email.Text);
            maile.Subject = assunto.Text;
            maile.Body = mensagem1.Text + mensagem2.Text + mensagem3.Text + mensagem4.Text;

        


            string myDir1 = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);

            string fileName = "documento1.pdf";

            //Java.IO.File myDir = new Java.IO.File(myDir1 + @"/pdf/");
            //Java.IO.File file = new Java.IO.File(myDir, fileName);

            string file = myDir1 + @"/pdf/" + fileName;

            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            // Add the file attachment to this email message.
            maile.Attachments.Add(data);


            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(username, password);
            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object senderx, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) {
                return true;
            };
            SmtpServer.Send(maile);
            Toast.MakeText(Application.Context, "Enviado com Sucesso", ToastLength.Short).Show();

            string pasta_pdf, pasta_pdfx;
            pasta_pdf = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "pdf");
                       
            pasta_pdfx = pasta_pdf +@"/documento1.pdf";
            
            File.Delete(pasta_pdfx);



            this.Finish();
        }


    }
}