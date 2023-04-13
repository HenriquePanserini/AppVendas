using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace vendas
{
    [Activity(Label = "Activity1")]
    public class at_servidor : Activity
    {
        Button btn_gravar;
        EditText caminho;
        EditText smtp;
        EditText banco;
        EditText usuario;
        EditText senha;
        EditText vendedor;
        EditText caminholocal;
        RadioGroup Radiogroup1;
        RadioButton botao1;
        public static RadioButton botao2;
        RadioButton botao3;
        RadioButton botao4;
        int descontosel = 0;
        int senha1 = 0;
        internal static bool desc1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_servidor);
            // Create your application here

            btn_gravar = FindViewById<Button>(Resource.Id.btn_gravar);
            caminho = FindViewById<EditText>(Resource.Id.caminho);
            smtp = FindViewById<EditText>(Resource.Id.smtp);
            usuario = FindViewById<EditText>(Resource.Id.usuario);
            senha = FindViewById<EditText>(Resource.Id.senha);
            banco = FindViewById<EditText>(Resource.Id.banco);
            vendedor = FindViewById<EditText>(Resource.Id.vendedor);
            caminholocal = FindViewById<EditText>(Resource.Id.caminholocal);
            Radiogroup1 = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            botao1 = FindViewById<RadioButton>(Resource.Id.radioButton1);
            botao2 = FindViewById<RadioButton>(Resource.Id.radioButton2);
            botao3 = FindViewById<RadioButton>(Resource.Id.radioButton3);
            botao4 = FindViewById<RadioButton>(Resource.Id.radioButton4);



            btn_gravar.Click += Btn_gravar_Click;


            DataTable dados = Cl_gestor.EXE_QUERY("select * from  parametro ");


            if (dados.Rows.Count == 0)
            {

            }
            else
            {
               
                caminho.Text = dados.Rows[0]["endereco"].ToString();
                caminholocal.Text = dados.Rows[0]["caminholocal"].ToString();
                if ((dados.Rows[0]["senha"].ToString() != null) && (dados.Rows[0]["senha"].ToString() != ""))
                {
                    senha.Text = dados.Rows[0]["senha"].ToString();
                }
                if ((dados.Rows[0]["smtp"].ToString() != null) && (dados.Rows[0]["smtp"].ToString() != ""))
                {
                    smtp.Text = dados.Rows[0]["smtp"].ToString();
                }
                usuario.Text = dados.Rows[0]["usuario"].ToString();
                vendedor.Text = dados.Rows[0]["vendedor"].ToString();
                banco.Text = dados.Rows[0]["banco"].ToString();

            }


            botao1.Click += Botao1_Click;
            botao2.Click += Botao2_Click;
            botao3.Click += Botao3_Click;
            botao4.Click += Botao4_Click;
        }

        private void Botao4_Click(object sender, EventArgs e)
        {
            senha1 = 1;
        }

        private void Botao3_Click(object sender, EventArgs e)
        {
            senha1 = 0;
        }

        private void Botao2_Click(object sender, EventArgs e)
        {
            descontosel = 1;
        }

        private void Botao1_Click(object sender, EventArgs e)
        {
            descontosel = 0;
        }

        public void Btn_gravar_Click(object sender, EventArgs e)
        {

            Cl_gestor.NOM_QUERY("delete from usuario");

            Cl_gestor.NOM_QUERY("delete from parametro");

            List<SQLparametro> parametro1 = new List<SQLparametro>();
            string caminho1 = caminho.Text;
            string caminholocal1 = caminholocal.Text;
            string smtp1 =smtp.Text;
            string usuario1 = usuario.Text;
            string senha1 = senha.Text;
            string vendedor1 =vendedor.Text;
            string banco1 = banco.Text;
            string desc1 = descontosel.ToString();


            //parametro.Add(new SQLparametro("@id_cliente", Convert.ToInt32(cliente_selecionado.id_clientes)));
            parametro1.Add(new SQLparametro("@id_parametro", 1));
            parametro1.Add(new SQLparametro("@endereco", caminho1));
            parametro1.Add(new SQLparametro("@usuario", usuario1));
            parametro1.Add(new SQLparametro("@senha", senha1));
            parametro1.Add(new SQLparametro("@smtp", smtp1));
            parametro1.Add(new SQLparametro("@vendedor", vendedor1));
            parametro1.Add(new SQLparametro("@vendasdesc", desc1));
            parametro1.Add(new SQLparametro("@caminholocal", caminholocal1));
            parametro1.Add(new SQLparametro("@banco1", banco1));



            Cl_gestor.NOM_QUERY(
                "insert into parametro  (id_parametro,endereco,usuario,senha,smtp,vendedor,vendasdesc,caminholocal,banco) values ( " +
                "@id_parametro," +
                "@endereco," +
                "@usuario, " +
                "@senha, " +
                "@smtp,"+
                "@vendedor," +
                "@vendasdesc, " +
                "@caminholocal," +
                "@banco1) ", parametro1);

            this.Finish();
        }



    }
}