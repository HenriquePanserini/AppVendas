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
using Math = System.Math;

namespace vendas
{
    [Activity(Label = "at_detalheproduto_or_sdesc")]
    public class at_detalheproduto_or_sdesc : Activity
    {



        RadioGroup radiopreco;
        RadioButton preco1;
        RadioButton preco2;
        RadioButton preco3;
        RadioButton preco4;

        EditText unitario;
        EditText quantidade;
        EditText desconto;
        EditText unitariodesc;
        EditText subtotal;
        Button btn_cancelar;
        Button btn_gravar;
        TextView produtodesc;
        Intent intent_temp;


        decimal xquantidadef;
        decimal unitariodescf;
        decimal unitarif;
        decimal subtotalf;
        decimal xdescontof;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.layout_detalheproduto_or_sdesc);


            radiopreco = FindViewById<RadioGroup>(Resource.Id.radioGroup1or);
            preco1 = FindViewById<RadioButton>(Resource.Id.radioButton1or);
            preco2 = FindViewById<RadioButton>(Resource.Id.radioButton2or);
            preco3 = FindViewById<RadioButton>(Resource.Id.radioButton3or);
            preco4 = FindViewById<RadioButton>(Resource.Id.radioButton4or);


            // unitario = FindViewById<EditText>(Resource.Id.unitario);
            quantidade = FindViewById<EditText>(Resource.Id.quantidade);
            //desconto = FindViewById<EditText>(Resource.Id.desconto);
          //  unitariodesc = FindViewById<EditText>(Resource.Id.unitariodesc);
            btn_cancelar = FindViewById<Button>(Resource.Id.btn_cancelar);
            btn_gravar = FindViewById<Button>(Resource.Id.btn_gravar);
            produtodesc = FindViewById<TextView>(Resource.Id.produtodesc);
            subtotal = FindViewById<EditText>(Resource.Id.subtotal);


            preco1.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vars.precoproduto.ToString());
            preco2.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vars.precoproduto2.ToString());
            preco3.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vars.precoproduto3.ToString());
            preco4.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vars.precoproduto4.ToString());


            int xc = 0;

            produtodesc.Text = vars.nomeproduto;
            //unitario.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vars.precoproduto);   //.ToString();
            string xvx = vars.precoproduto.ToString();
            xvx = xvx.Replace(".", ",");

          

           
            subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", xc);

            int x = 1;

           // unitario.BeforeTextChanged += Unitario_BeforeTextChanged;
           
            btn_gravar.Click += Btn_gravar_Click;
            btn_cancelar.Click += Btn_cancelar_Click;



            preco1.Click += Preco1_Click;
            preco2.Click += Preco2_Click;
            preco3.Click += Preco3_Click;
            preco4.Click += Preco4_Click;


        }


        private void Preco4_Click(object sender, EventArgs e)
        {

            if ((quantidade.Text != "") && (quantidade.Text != null))
            {
                string x = vars.precoproduto4.ToString();
                x = x.Replace(".", ",");
                decimal xunitario = Math.Round(Convert.ToDecimal(x), 2);
                decimal xquantidade = decimal.Parse(quantidade.Text);

                unitarif = xunitario = Math.Round(Convert.ToDecimal(x), 2);

                subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", xunitario * xquantidade);

            }
        }

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

            this.Finish();
        }

        
        
        private void Btn_gravar_Click(object sender, EventArgs e)
        {




            if ((unitarif != 0) && (quantidade.Text != null) && (quantidade.Text != ""))
            {

                decimal xquantidade2 = decimal.Parse(quantidade.Text);


                List<SQLparametro> parametro = new List<SQLparametro>();


                            parametro.Add(new SQLparametro("@id_produto", vars.numeroproduto));
                            parametro.Add(new SQLparametro("@descricao", vars.nomeproduto));
                            parametro.Add(new SQLparametro("@quantidade", xquantidade2));
                            parametro.Add(new SQLparametro("@preco", unitarif));
                            parametro.Add(new SQLparametro("@desconto", xdescontof));
                            parametro.Add(new SQLparametro("@precodesconto", unitarif));
                            parametro.Add(new SQLparametro("@total", unitarif * xquantidade2));



                            Cl_gestor.NOM_QUERY(
                                "insert into tmp_vendas_prod_or (id_produto,descricao,quantidade,preco,desconto,precodesconto,total) values ( " +
                                "@id_produto," +
                                "@descricao," +
                                "@quantidade," +
                                "@preco," +
                                "@desconto," +
                                "@precodesconto," +
                                "@total )", parametro);

                            StartActivity(typeof(at_vendas_or_sdesc));
                            this.Finish();


            }
            else
            {

                Toast.MakeText(Application.Context, " Selecione uma quantidade ou preço  ", ToastLength.Long).Show();

            }



        }
        

    }
}