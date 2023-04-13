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
    [Activity(Label = "at_detalheproduto_or")]
    public class at_detalheproduto_or : Activity
    {

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

            SetContentView(Resource.Layout.layout_detalheproduto_or);



            
            unitario = FindViewById<EditText>(Resource.Id.unitario);
            quantidade = FindViewById<EditText>(Resource.Id.quantidade);
            desconto = FindViewById<EditText>(Resource.Id.desconto);
            unitariodesc = FindViewById<EditText>(Resource.Id.unitariodesc);
            btn_cancelar = FindViewById<Button>(Resource.Id.btn_cancelar);
            btn_gravar = FindViewById<Button>(Resource.Id.btn_gravar);
            produtodesc = FindViewById<TextView>(Resource.Id.produtodesc);
            subtotal = FindViewById<EditText>(Resource.Id.subtotal);


           

            int xc = 0;

            produtodesc.Text = vars.nomeproduto;
            //unitario.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vars.precoproduto);   //.ToString();
            string xvx = vars.precoproduto.ToString();
            xvx = xvx.Replace(".", ",");

            unitario.Text = xvx;//Math.Round(Convert.ToDecimal(xvx), 2).ToString();

            unitariodesc.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", vars.precoproduto);   //.ToString();
            subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", xc);

            int x = 1;

           // unitario.BeforeTextChanged += Unitario_BeforeTextChanged;
            unitario.TextChanged += Unitario_TextChanged;
            unitario.BeforeTextChanged += Unitario_BeforeTextChanged;
            quantidade.BeforeTextChanged += Quantidade_BeforeTextChanged;
            desconto.TextChanged += Desconto_TextChanged;
            btn_gravar.Click += Btn_gravar_Click;
            quantidade.TextChanged += Quantidade_TextChanged;
            unitario.Click += Unitario_Click;
            btn_cancelar.Click += Btn_cancelar_Click;
        }

        private void Btn_cancelar_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(at_vendas_or));
            this.Finish();
   
        }

        private void Unitario_Click(object sender, EventArgs e)
        {
            unitario.Text = "";
        }

        private void Btn_gravar_Click(object sender, EventArgs e)
        {

            //vars.precoproduto = Convert.ToDouble(unitariodesc);
            //vars.quantidadeproduto = Convert.ToDouble(quantidade);
            //vars.precoprodutodesc = Convert.ToDouble(produtodesc);
            //vars.totalproduto = Convert.ToDouble(subtotal);
            //vars.desconto = Convert.ToDouble(desconto);

            //"id_produto                               integer," +
            //   "descricao                                nvarchar(30)," +
            //   "quantidade                               DECIMAL(10,2)," +
            //   "preco                                    DECIMAL(10,2)," +
            //   "desconto                                 DECIMAL(10,2)," +
            //   "precodesconto                            DECIMAL(10,2)," +
            //   "total                                    DECIMAL(10,2)";

            List<SQLparametro> parametro = new List<SQLparametro>();


            parametro.Add(new SQLparametro("@id_produto", vars.numeroproduto));
            parametro.Add(new SQLparametro("@descricao", vars.nomeproduto));
            parametro.Add(new SQLparametro("@quantidade", xquantidadef));
            parametro.Add(new SQLparametro("@preco", unitarif));
            parametro.Add(new SQLparametro("@desconto", xdescontof));
            parametro.Add(new SQLparametro("@precodesconto", unitariodescf));
            parametro.Add(new SQLparametro("@total", subtotalf));



            Cl_gestor.NOM_QUERY(
                "insert into tmp_vendas_prod_or (id_produto,descricao,quantidade,preco,desconto,precodesconto,total) values ( " +
                "@id_produto," +
                "@descricao," +
                "@quantidade," +
                "@preco," +
                "@desconto," +
                "@precodesconto," +
                "@total )", parametro);

            StartActivity(typeof(at_vendas_or));
            this.Finish();


        }

        private void Desconto_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {

            string xy1 = "";
            //if (unitario.Text.ToString().Substring(1, unitario.Text.Length ) != null)
            //{
            //    xy1 = unitario.Text.ToString().Substring(1, unitario.Text.Length );
            //}
            //else
            //{
            //    xy1 = "";
            //}

            xy1 = unitario.Text.ToString(); //.Substring(1, unitario.Text.Length );

            if ((xy1 != "") && (unitario.Text != "") && (xy1 != null) && (unitario.Text != null) && (desconto.Text != null) && (desconto.Text !="") && (quantidade.Text != null) && (quantidade.Text != ""))
            {
                decimal xquantidade = decimal.Parse(quantidade.Text);

                //string x = unitario.Text.ToString().Substring(3, unitario.Text.Length - 4);
                //double xunitario = double.Parse(x);  // double.Parse(unitario.Text);
                //double xunitario = double.Parse(unitario.Text);
                string x = unitario.Text.ToString(); //.Substring(1, unitario.Text.Length );
                x = x.Replace(".", ",");
                decimal xunitario = Math.Round(Convert.ToDecimal(x), 2);

                string xy = desconto.Text.ToString();   //.Substring(3, desconto.Text.Length - 3);
                xy = xy.Replace(".", ",");
                decimal xdesconto = Math.Round(Convert.ToDecimal(xy), 2);

                // decimal xdesconto = decimal.Parse(desconto.Text);
                decimal calculo;
                calculo = xunitario - ((xunitario * xdesconto) / 100);

                unitariodesc.Text = calculo.ToString();
                unitariodesc.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", calculo);
                subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", calculo * xquantidade);

                // subtotal.Text = (calculo * xquantidade).ToString();

                xquantidadef = xquantidade;
                xdescontof = xdesconto;
                unitariodescf = calculo;
                unitarif = xunitario;
                subtotalf = calculo * xquantidade;

            }
        }

        private void Quantidade_BeforeTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {

            string xy2 = "";
            //  if (unitario.Text.ToString().Substring(1, unitario.Text.Length ) != null)
            //  {
            xy2 = unitario.Text.ToString(); //.Substring(3, unitario.Text.Length );
                                            // }
                                            // else
                                            // {
                                            //    xy2 = "";
                                            // }

            xy2 = unitario.Text.ToString();//.Substring(1, unitario.Text.Length );

            if ((xy2 != "") && (quantidade.Text != "") && (xy2 != null) && (quantidade.Text != null))
            {
                // double xunitario = double.Parse(unitario.Text);
                //string x = unitario.Text.ToString().Substring(3, unitario.Text.Length - 4);
                string x = unitario.Text.ToString(); //.Substring(1, unitario.Text.Length);
                x = x.Replace(".", ",");
                decimal xunitario = Math.Round(Convert.ToDecimal(x), 2);
                decimal xquantidade = decimal.Parse(quantidade.Text);


                //subtotal.Text = (xunitario * xquantidade).ToString();

                subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", xunitario * xquantidade);

                xquantidadef = xquantidade;
                xdescontof = 0;
                unitariodescf = xunitario;
                unitarif = xunitario;
                subtotalf = xunitario * xquantidade;


            }

        }

        private void Quantidade_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {



            string xy3 = "";
            //if (unitario.Text.ToString().Substring(3, unitario.Text.Length - 3) != null)
            //{
            xy3 = unitario.Text.ToString(); //.Substring(3, unitario.Text.Length - 3);
            //}
            //else
            //{
            //    xy3 = "";
            //}


            
            if ((xy3 != "") && (quantidade.Text != "") && (xy3 != null) && (quantidade.Text != null))
            {
                string x = unitario.Text.ToString(); //.Substring(1, unitario.Text.Length );
                x = x.Replace(".", ",");
                decimal xunitario = Math.Round(Convert.ToDecimal(x), 2);

                decimal xquantidade = decimal.Parse(quantidade.Text);





                //subtotal.Text = (xunitario * xquantidade).ToString();

                subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", xunitario * xquantidade);

                xquantidadef = xquantidade;
                xdescontof = 0;
                unitariodescf = xunitario;
                unitarif = xunitario;
                subtotalf = xunitario * xquantidade;

            }
            int xx = 0;
        }



        private void Unitario_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
           
            string xy4 = "";
            if ((unitario.Text.ToString().Trim() != null) && (unitario.Text.ToString().Trim() != "R$ ")  && (unitario.Text.ToString().Trim() != "R$") && (unitario.Text.ToString().Trim() != "R") && (unitario.Text.ToString().Trim() != "") && (quantidade.Text != ""))
            {
                
                /////xy4 = unitario.Text.ToString().Substring(3, unitario.Text.Length - 3);
                //if ((unitario.Text.Length > 3) && (unitario.Text == "R$ "))
                //{
                //    xy4 = unitario.Text.ToString().Substring(3, unitario.Text.Length - 3);
                //}
                //if ((unitario.Text.Length == 3) && (unitario.Text != "R$ "))
                //{
                //    xy4 = unitario.Text.ToString().Substring(2, unitario.Text.Length - 2);
                //}
                //if ((unitario.Text.Length == 2) && (unitario.Text != "R$"))
                //{
                //    xy4 = unitario.Text.ToString();
                //}
                //if ((unitario.Text.Length == 1) && (unitario.Text != "R"))
                //{
                //    xy4 = unitario.Text.ToString();
                //}
                //if ((unitario.Text.Length > 1) && (unitario.Text.ToString().Substring(1, 1) != "R"))
                //{
                    xy4 = unitario.Text.ToString();
                //}

            }
            else
            {
                xy4 = "";
            }


            if ((xy4 != "") && (quantidade.Text != "") && (xy4 != null) && (quantidade.Text != null))
            {
                //double xunitario = double.Parse(unitario.Text);
                //string x = unitario.Text.ToString().Substring(4, unitario.Text.Length - 4);
                //double xunitario = double.Parse(x);  // double.Parse(unitario.Text);

                string x = xy4; //unitario.Text.ToString().Substring(3, unitario.Text.Length - 3);
                x = x.Replace(".", ",");

               // Toast.MakeText(this, x, ToastLength.Long).Show();

                decimal xunitario = Math.Round(Convert.ToDecimal(x), 2);
                decimal xquantidade = decimal.Parse(quantidade.Text);

                //unitariodesc.Text = unitario.Text;
                unitariodesc.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Convert.ToDecimal(x));


                xquantidadef = xquantidade;
                xdescontof = 0;
                unitariodescf = xunitario;
                unitarif = xunitario;
                subtotalf = xunitario * xquantidade;

                //Toast.MakeText(this, x, ToastLength.Long).Show();
                // subtotal.Text = (xunitario * xquantidade).ToString();

                subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", xunitario * xquantidade);

            }
        }


        private void Unitario_BeforeTextChanged(object sender, TextChangedEventArgs e)
        {

           
            string xy5 = "";
            //if (unitario.Text.ToString().Substring(3, unitario.Text.Length - 3) != null)
            if ((unitario.Text.ToString().Trim() != null) && (unitario.Text.ToString().Trim() != "R$ ") && (unitario.Text.ToString().Trim() != "R$") && (unitario.Text.ToString().Trim() != "R") && (unitario.Text.ToString().Trim() != "R") && (unitario.Text.ToString().Trim() != "") && (quantidade.Text != ""))
            {

                //if ((unitario.Text.Length > 3) && (unitario.Text.ToString().Substring(3, unitario.Text.Length - 3) == "R$ "))
                //{
                //    xy5 = unitario.Text.ToString().Substring(3, unitario.Text.Length - 3);
                //    Toast.MakeText(this, '1', ToastLength.Long).Show();
                //}
                //if ((unitario.Text.Length == 3) && (unitario.Text != "R$ "))
                //{
                //    xy5 = unitario.Text.ToString().Substring(2, unitario.Text.Length - 2);
                //    Toast.MakeText(this, '2', ToastLength.Long).Show();
                //}
                //if ((unitario.Text.Length == 2) && (unitario.Text != "R$"))
                //{
                //    xy5 = unitario.Text.ToString();
                //    Toast.MakeText(this, '3', ToastLength.Long).Show();
                //}
                //if ((unitario.Text.Length == 1) && (unitario.Text != "R"))
                //{
                //    xy5 = unitario.Text.ToString();
                //    Toast.MakeText(this, '4', ToastLength.Long).Show();
                //}
               
                //if ((unitario.Text.Length > 1) && (unitario.Text.ToString().Substring(1,1) != "R"))
                //{
                    xy5 = unitario.Text.ToString();
                  
                //}

            }
            else
            {
                xy5 = "";
            }
            

            if ((xy5 != "") && (quantidade.Text != "") && (xy5 != null) && (quantidade.Text != null))
            {
                //double xunitario = double.Parse(unitario.Text);
                //string x = unitario.Text.ToString().Substring(4, unitario.Text.Length - 4);
                //double xunitario = double.Parse(x);  // double.Parse(unitario.Text);

                string x = xy5; //unitario.Text.ToString().Substring(3, unitario.Text.Length - 3);
               x = x.Replace(",",".");

               // Toast.MakeText(this, x, ToastLength.Long).Show();

                decimal xunitario = Math.Round(Convert.ToDecimal(x), 2);
                decimal xquantidade = decimal.Parse(quantidade.Text);

                //unitariodesc.Text = unitario.Text;

                unitariodesc.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Convert.ToDecimal(x));

                // Toast.MakeText(this, xunitario.ToString(), ToastLength.Long).Show();

                xquantidadef = xquantidade;
                xdescontof = 0;
                unitariodescf = xunitario;
                unitarif = xunitario;
                subtotalf = xunitario * xquantidade;

                // subtotal.Text = (xunitario * xquantidade).ToString();

                subtotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", xunitario * xquantidade);

            }
        }
                              

    }
}