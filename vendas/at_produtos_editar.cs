using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android;

namespace vendas
{
    [Activity(Label = "at__editar")]
    public class at_produtos_editar : Activity
    {
        cl_produtos produto;
        Button cmd_cancelar_produto;
        Button cmd_produto;
        EditText edit_nome_produto;
        EditText edit_preco_produto;
        Button cmd_gravar_produto;
        int id_produto = 0 ;
        bool editar = false;
        Intent intent_temp;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);


           
            intent_temp = this.Intent;

            // Create your application here
            SetContentView(Resource.Layout.layout_produtos_editar);

            cmd_cancelar_produto = FindViewById<Button>(Resource.Id.cmd_produto_cancelar);
            cmd_gravar_produto = FindViewById<Button>(Resource.Id.cmd_produto_gravar);
            edit_nome_produto = FindViewById<EditText>(Resource.Id.edit_nome_produto);
            edit_preco_produto = FindViewById<EditText>(Resource.Id.edit_preco_unidade);


            cmd_cancelar_produto.Click += delegate { this.Finish(); };

            cmd_gravar_produto.Click += Cmd_gravar_produto_Click;

            if (this.Intent.GetStringExtra("id_produto") != null)
            {

                id_produto = int.Parse(this.Intent.GetStringExtra("id_produto"));
                ApresentaProduto();
                editar = true;


            }

        }

        private void ApresentaProduto()
        {
            DataTable dados = Cl_gestor.EXE_QUERY("select * from produtos where id_produto = "+ id_produto);

            edit_nome_produto.Text = dados.Rows[0]["descricao"].ToString();
            edit_preco_produto.Text = dados.Rows[0]["preco"].ToString();

        }



        private void Cmd_gravar_produto_Click(object sender, EventArgs e)
        {

            if (edit_nome_produto.Text == ""  || edit_preco_produto.Text == "")
            {
                AlertDialog.Builder caixa = new AlertDialog.Builder(this);
                caixa.SetTitle("erro!");
                caixa.SetMessage("Preencha todos os campos");
                caixa.SetPositiveButton("OK", delegate { });
                caixa.Show();


            }



            List<SQLparametro> parametro = new List<SQLparametro>();
            if (!editar)
            {
                
                parametro.Add(new SQLparametro("@id_produto", Cl_gestor.ID_DISPONIVEL("produtos", "id_produto")));

            }
            else
            {
                parametro.Add(new SQLparametro("@id_produto",id_produto));
            }

            parametro.Add(new SQLparametro("@descricao", edit_nome_produto.Text));
            parametro.Add(new SQLparametro("@preco", edit_preco_produto.Text));
            parametro.Add(new SQLparametro("@atualizacao", DateTime.Now));


          

            #region novo
            if (!editar)
            { 
                DataTable dados = Cl_gestor.EXE_QUERY("select descricao from produtos where descricao = @descricao", parametro);

                if (dados.Rows.Count != 0)
                {
                    AlertDialog.Builder caixa = new AlertDialog.Builder(this);
                    caixa.SetTitle("erro!");
                    caixa.SetMessage("Ja existe um produto com o mesmo nome");
                    caixa.SetPositiveButton("OK", delegate { });
                    caixa.Show();
                    return;
                }

                Cl_gestor.NOM_QUERY(
                    "insert into produtos values ("+
                    "@id_produto,"+
                    "@descricao,"+
                    "@preco,"+
                    "@atualizacao)",parametro);

                //  SetResult(Result.Ok, intent_temp);
                SetResult(Result.Ok, intent_temp);
                this.Finish();

            }
            #endregion

            #region editar
            else
            {

                DataTable dados = Cl_gestor.EXE_QUERY("select descricao from produtos where descricao = @descricao AND id_produto <> @id_produto ", parametro);

                if (dados.Rows.Count != 0)
                {
                    AlertDialog.Builder caixa = new AlertDialog.Builder(this);
                    caixa.SetTitle("erro!");
                    caixa.SetMessage("Ja existe um produto com o mesmo nome");
                    caixa.SetPositiveButton("OK", delegate { });
                    caixa.Show();
                    return;
                }

                Cl_gestor.NOM_QUERY(
                    "UPDATE Produtos SET " +
                    "descricao = @descricao," +
                    "preco = @preco," +
                    "atualizacao = @atualizacao " +
                    "WHERE id_produto = @id_produto ", parametro);
                SetResult(Result.Ok, intent_temp);
                this.Finish();

            }
            #endregion

        }
    }
}