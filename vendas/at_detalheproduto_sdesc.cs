using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using iTextSharp.text;
using Math = System.Math;

namespace vendas
{
    [Activity(Label = "at_detalheproduto_sdesc")]
    public class at_detalheproduto_sdesc : Activity
    {
        RadioGroup radiopreco;
        RadioButton preco1;
        RadioButton preco2;
        RadioButton preco3;
        //RadioButton preco4;


        
        EditText unitario;
        EditText quantidade;
        EditText subtotal;
        Button btn_cancelar;
        Button btn_gravar;
        TextView produtodesc;
        Intent intent_temp;
        List precos;


        decimal xquantidadef;
        decimal unitariodescf;
        decimal unitarif;
        decimal subtotalf;
        decimal xdescontof;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.layout_detalheproduto_sdesc);

            radiopreco = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            preco1 = FindViewById<RadioButton>(Resource.Id.radioButton1);
            preco2 = FindViewById<RadioButton>(Resource.Id.radioButton2);
            preco3 = FindViewById<RadioButton>(Resource.Id.radioButton3);
            //preco4 = FindViewById<RadioButton>(Resource.Id.radioButton4);

            quantidade = FindViewById<EditText>(Resource.Id.quantidade);
            btn_cancelar = FindViewById<Button>(Resource.Id.btn_cancelar);
            btn_gravar = FindViewById<Button>(Resource.Id.btn_gravar);
            produtodesc = FindViewById<TextView>(Resource.Id.produtodesc);
            subtotal = FindViewById<EditText>(Resource.Id.subtotal);

            preco1.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vars.precoproduto.ToString());
            preco2.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vars.precoproduto2.ToString());
            preco3.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vars.precoproduto3.ToString());
            //preco4.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vars.precoproduto4.ToString());

            int xc = 0;

            produtodesc.Text = vars.nomeproduto;
            //unitario.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vars.precoproduto);   //.ToString();
            string xvx = vars.precoproduto.ToString();
            xvx = xvx.Replace(".", ",");

            //unitario.Text = xvx;//Math.Round(Convert.ToDecimal(xvx), 2).ToString();

            subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", xc);

            int x = 1;

            //unitario.BeforeTextChanged += Unitario_BeforeTextChanged;
            //unitario.TextChanged += Unitario_TextChanged;
            //unitario.BeforeTextChanged += Unitario_BeforeTextChanged;
            //quantidade.BeforeTextChanged += Quantidade_BeforeTextChanged;
            btn_gravar.Click += Btn_gravar_Click;
           
            //unitario.Click += Unitario_Click;
            btn_cancelar.Click += Btn_cancelar_Click;
            preco1.Click += Preco1_Click;
            preco2.Click += Preco2_Click;
            preco3.Click += Preco3_Click;
            //preco4.Click += Preco4_Click;
        }

        //private void Preco4_Click(object sender, EventArgs e)
        //{

        //    if ((quantidade.Text != "") && (quantidade.Text != null))
        //    {
        //        string x = vars.precoproduto4.ToString();
        //        x = x.Replace(".", ",");
        //        decimal xunitario = Math.Round(Convert.ToDecimal(x), 2);
        //        decimal xquantidade = decimal.Parse(quantidade.Text);

        //        unitarif = xunitario = Math.Round(Convert.ToDecimal(x), 2);

        //        subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", xunitario * xquantidade);

        //    }
        //}

        private void Preco3_Click(object sender, EventArgs e)
        {

            if ((quantidade.Text != "") && (quantidade.Text != null))
            {
                string x = vars.precoproduto3.ToString();
                x = x.Replace(".", ",");
                decimal xunitario = Math.Round(Convert.ToDecimal(x), 2);
                decimal xquantidade = decimal.Parse(quantidade.Text);

                unitarif = xunitario = Math.Round(Convert.ToDecimal(x), 2);

                subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", xunitario * xquantidade);

            }


        }

        private void Preco2_Click(object sender, EventArgs e)
        {

            if ((quantidade.Text != "") && (quantidade.Text != null))
            {

                string x = vars.precoproduto2.ToString();
                x = x.Replace(".", ",");
                decimal xunitario = Math.Round(Convert.ToDecimal(x), 2);
                decimal xquantidade = decimal.Parse(quantidade.Text);

                unitarif = xunitario = Math.Round(Convert.ToDecimal(x), 2);

                subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", xunitario * xquantidade);

            }
        }

        private void Preco1_Click(object sender, EventArgs e)
        {


            if ((quantidade.Text != "") && (quantidade.Text != null))
            {
                string x = vars.precoproduto.ToString();
                x = x.Replace(".", ",");
                decimal xunitario = Math.Round(Convert.ToDecimal(x), 2);
                decimal xquantidade = decimal.Parse(quantidade.Text);

                unitarif = xunitario = Math.Round(Convert.ToDecimal(x), 2);

                subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", xunitario * xquantidade);
            }
        }

        private void Btn_cancelar_Click(object sender, EventArgs e)
        {

            StartActivity(typeof(at_vendas_sdesc));
            this.Finish();
        }

       
        private void Btn_gravar_Click(object sender, EventArgs e)
        {


           

            if ((unitarif != 0) &&  (quantidade.Text != null) && (quantidade.Text != ""))
            {

                List<SQLparametro> parametro = new List<SQLparametro>();


                xdescontof = 0;
                unitariodescf = 0;
               

                     decimal xquantidade2 = decimal.Parse(quantidade.Text);

                parametro.Add(new SQLparametro("@id_produto", vars.numeroproduto));
                parametro.Add(new SQLparametro("@descricao", vars.nomeproduto));
                parametro.Add(new SQLparametro("@quantidade", xquantidade2));
                parametro.Add(new SQLparametro("@preco", unitarif));
                parametro.Add(new SQLparametro("@desconto", xdescontof));
                parametro.Add(new SQLparametro("@precodesconto", unitarif));
                parametro.Add(new SQLparametro("@total", unitarif * xquantidade2));



                Cl_gestor.NOM_QUERY(
                    "insert into tmp_vendas_prod (id_produto, descricao,quantidade,preco, desconto ,precodesconto, total) values ( " +
                    "@id_produto," +
                    "@descricao," +
                    "@quantidade," +
                    "@preco," +
                    "@desconto," +
                    "@precodesconto," +
                    "@total )", parametro);

                StartActivity(typeof(at_vendas_sdesc));
                this.Finish();

            }
            else
            {

                Toast.MakeText(Application.Context, " Selecione uma quantidade ou preço  ", ToastLength.Long).Show();

            }


        }

        

         


              


    }
}