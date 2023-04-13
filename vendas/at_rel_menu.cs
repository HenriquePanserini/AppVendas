using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Print;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Karumi.Dexter;
using Com.Karumi.Dexter.Listener;
using Com.Karumi.Dexter.Listener.Single;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Java.Security.Acl;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin;
using static Android.App.DatePickerDialog;

namespace vendas
{
    [Activity(Label = "Relatorios", MainLauncher = false, Icon = "@drawable/icon")]

    public class at_rel_menu : AppCompatActivity, Android.App.DatePickerDialog.IOnDateSetListener
    {
        Button btn_gerar;
        Button btn_dt1;
        Button btn_dt2;
        TextView edit1;
        TextView edit2;
        private int year, month, date;
        private int getDatePickerId;
        public static string enderecows = "";
        public static string enderecowsporta = "";
        public static string enderecowslinha = "";
        DataTable dados;

        public static string fileName = "Ordem.pdf";


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_rel_menu);

            btn_dt1 = FindViewById<Button>(Resource.Id.bt1);
            btn_dt2 = FindViewById<Button>(Resource.Id.bt2);
            btn_gerar = FindViewById<Button>(Resource.Id.btn_gerar);
            edit1 = FindViewById<TextView>(Resource.Id.tx_edit1);
            edit2 = FindViewById<TextView>(Resource.Id.tx_edit2);

            btn_dt1.Click += Btn_dt1_Click;
            btn_dt2.Click += Btn_dt2_Click;
            btn_gerar.Click += Btn_gerar_Click1;


        }

        private async void Btn_gerar_Click1(object sender, EventArgs e)
        {
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

                    string url = enderecowsporta + "/selectRelVendas.php";
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var StringResult = await response.Content.ReadAsStringAsync();

                    Cl_gestor.EXE_QUERY("DELETE FROM rel_vendas");

                    rel_receber[] colecaoPegarid = JsonConvert.DeserializeObject<rel_receber[]>(StringResult);

                    int cont = colecaoPegarid.Length - 1;

                    for (int i = 0; i < cont; i++)
                    {
                        if (colecaoPegarid[i].descricao != null)
                        {
                            List<SQLparametro> parametro = new List<SQLparametro>();

                            string total = colecaoPegarid[i].total.ToString();
                            total = total.Replace(",", ".");
                            Console.WriteLine(colecaoPegarid[i].descricao + " " + total);
                            Console.WriteLine(colecaoPegarid[i].id_cliente);

                            parametro.Add(new SQLparametro("@seq", colecaoPegarid[i].seq));
                            parametro.Add(new SQLparametro("@data", colecaoPegarid[i].data));
                            parametro.Add(new SQLparametro("@id_cliente", colecaoPegarid[i].id_cliente));
                            parametro.Add(new SQLparametro("@codform", colecaoPegarid[i].codform));
                            parametro.Add(new SQLparametro("@cupom", colecaoPegarid[i].cupom));
                            parametro.Add(new SQLparametro("@quant", colecaoPegarid[i].quant));
                            parametro.Add(new SQLparametro("@total", colecaoPegarid[i].total));
                            parametro.Add(new SQLparametro("@descricao", colecaoPegarid[i].descricao));
                            parametro.Add(new SQLparametro("@desconto", colecaoPegarid[i].desconto));
                            parametro.Add(new SQLparametro("@preco", colecaoPegarid[i].preco));

                            Cl_gestor.NOM_QUERY("INSERT INTO rel_vendas(codigo, id_cliente, descricao, desconto, cupom,  total, data, preco, quantidade, codform) VALUES (" +
                                                "@seq," +
                                                "@id_cliente," +
                                                "@descricao, " +
                                                "@desconto, " +
                                                "@cupom, " +
                                                "@total," +
                                                "@data, " +
                                                "@preco," +
                                                "@quant," +
                                                "@codform)", parametro);
                        }
                    }

                    //contas_a_receber();

                }
                catch (Exception ex)
                {
                    Toast.MakeText(BaseContext, ex.Message, ToastLength.Long).Show();
                }
        }

        

        private void Btn_dt1_Click(object sender, EventArgs e)
        {

            getDatePickerId = 1;
            ShowDialog(getDatePickerId);

        }

        private void Btn_dt2_Click(object sender, EventArgs e)
        {

            getDatePickerId = 2;
            ShowDialog(getDatePickerId);

        }

        protected override Dialog OnCreateDialog(int id)
        {


            if (id == 1)
            {

                return new Android.App.DatePickerDialog(this, this, year, month, date);

            } else if (id == 2)
            {

                return new Android.App.DatePickerDialog(this, this, year, month, date);

            }
            return null;

            
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            try{
                
                this.year = year;
                this.month = month + 1;
                this.date = dayOfMonth;

                if(getDatePickerId == 1)
                {  
                    edit1.Text = string.Format("%02d", date) + "/" + string.Format("%02d", month) + "/" + year;
                }    
                else if(getDatePickerId == 2)
                { 
                    edit2.Text = string.Format("%02d",date) + "/" + string.Format("%02d",month) + "/" + year;
                }
                
            }
            catch(Exception ex)
            {

                Toast.MakeText(BaseContext, "Erro ao inserir a data " + ex.Message, ToastLength.Short);

            }

        }



        public void contas_a_receber()
        {

            string pdf_dir = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "pdf");

            if (!Directory.Exists(pdf_dir))
            {
                Directory.CreateDirectory(pdf_dir);
            }

            FileStream files = new FileStream(pdf_dir + @"/documento.pdf", FileMode.Create);

            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter WriterPdf = PdfWriter.GetInstance(document, files);

            document.Open();

            Font fonte1 = FontFactory.GetFont(BaseFont.HELVETICA, 16);
            Font fonte2 = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 10,Color.BLACK);
            Font fonte3 = FontFactory.GetFont(BaseFont.HELVETICA, 8);

            string nomeLoja = "";

            DataTable dados1 = Cl_gestor.EXE_QUERY(" SELECT nome FROM parametrocli ");

            if (dados1.Rows.Count > 0)
            {
                nomeLoja = dados1.Rows[0]["nome"].ToString();
            }

            document.Add(new Paragraph("Listagem - recebimento de contas", fonte1));
            document.Add(new Paragraph("" + nomeLoja.Trim(), fonte2));

            int nroped = 0; 
            bool tipo = false;

            DataTable dados2 = Cl_gestor.EXE_QUERY(" SELECT * FROM vendas A " +
                "                                    INNER JOIN clientes B ON A.id_cliente = B.id_cliente " +
                "                                    WHERE A.id_cliente >= " + nroped.ToString());

            for (int i = 0; i < dados2.Rows.Count; i++)
            {

                string ncliente = dados2.Rows[i]["id_cliente"].ToString();
                string nome = dados2.Rows[i]["nome"].ToString();
                string desconto = dados2.Rows[i]["desconto"].ToString();
                string total = dados2.Rows[i]["total"].ToString();
                string data = dados2.Rows[i]["data"].ToString();
                string tipopagto = dados2.Rows[i]["tipopagto"].ToString();
                string nrobanco = dados2.Rows[i]["nrovendabco"].ToString();
                string id_pgto = dados2.Rows[i]["id_pgto"].ToString();
                string obsped = dados2.Rows[i]["obsped"].ToString();

                document.Add(new Paragraph("______________________________________________________________________________"));


                PdfPTable table = new PdfPTable(6);

                Paragraph Coluna1 = new Paragraph("Codigo", fonte2);
                Coluna1.SetAlignment("middle");
                Paragraph Coluna2 = new Paragraph("Nome", fonte2);
                Coluna2.SetAlignment("middle");
                Paragraph Coluna3 = new Paragraph("Data", fonte2);
                Coluna3.SetAlignment("middle");
                Paragraph Coluna4 = new Paragraph("Tipo de Pagamento", fonte2);
                Coluna4.SetAlignment("middle");
                Paragraph Coluna5 = new Paragraph("Desconto", fonte2);
                Coluna4.SetAlignment("middle");
                Paragraph Coluna6 = new Paragraph("Total", fonte2);
                Coluna4.SetAlignment("middle");

                var cell1 = new PdfPCell();
                var cell2 = new PdfPCell();
                var cell3 = new PdfPCell();
                var cell4 = new PdfPCell();
                var cell5 = new PdfPCell();
                var cell6 = new PdfPCell();

                var fi = new float[] { 6F, 6F, 6F, 15F, 8F, 6F };
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

                DataTable dados5 = Cl_gestor.EXE_QUERY(" SELECT * FROM vendas WHERE id_cliente >= " + ncliente);
                foreach (DataRow linha in dados5.Rows)
                {

                    var codigo = linha["id_cliente"].ToString();
                    var nomeven = linha["nome"].ToString();
                    var dataven = linha["data"].ToString();
                    var tipopagtoven = linha["tipopagto"].ToString();
                    var descontoven = linha["desconto"].ToString();
                    var totalven = linha["total"].ToString();

                    Phrase A = new Phrase(codigo, fonte3);
                    PdfPCell cella = new PdfPCell(A);
                    table.AddCell(A);


                    Phrase B = new Phrase(nomeven, fonte3);
                    PdfPCell cellb = new PdfPCell(B);
                    table.AddCell(B);


                    Phrase C = new Phrase(dataven, fonte3);
                    PdfPCell cellc = new PdfPCell(C);
                    table.AddCell(C);


                    Phrase D = new Phrase(tipopagtoven, fonte3);
                    PdfPCell celld = new PdfPCell(D);
                    table.AddCell(D);


                    Phrase E = new Phrase(descontoven, fonte3);
                    PdfPCell celle = new PdfPCell(E);
                    table.AddCell(E);


                    Phrase F = new Phrase(totalven, fonte3);
                    PdfPCell cellf = new PdfPCell(F);
                    table.AddCell(F);


                }

                document.Add(table);

            }

            document.Close();
            WriterPdf.Close();

            Toast.MakeText(Application.Context, "Gerando relatorio...", ToastLength.Long);

            if (tipo == true)
            {

            }
            else
            {
                compartilhar();
            }

        }

        public void compartilhar()
        {

            Intent email = new Intent(Intent.ActionSend);
            var mkDir = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);

            string fileName1 = @"/" + "documento.pdf";
            string fileName2 = mkDir + @"/documento.pdf";

            Java.IO.File myDir1 = new Java.IO.File(mkDir + @"/pdf/");
            Java.IO.File myDir2 = new Java.IO.File(myDir1, fileName1);

            var uri = Android.Net.Uri.Parse(myDir2.AbsolutePath);

            email.PutExtra(Intent.ExtraSubject, uri);
            email.PutExtra(Intent.ExtraStream, uri);

            email.SetType("application/pdf");

            StartActivity(Intent.CreateChooser(email, "Enviar..."));

        }

     
    }
}