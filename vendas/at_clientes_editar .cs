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
    [Activity(Label = "at_clientes_editar")]
    public class at_clientes_editar : Activity
    {
        cl_clientes cliente;
        Button cmd_cancelar_cliente;
        Button cmd_gravar_cliente;
        EditText edit_nome_cliente;
        EditText edit_telefone_cliente;

        int id_cliente = 0 ;
        bool editar = false;
        Intent intent_temp;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);


           
            intent_temp = this.Intent;

            // Create your application here
            SetContentView(Resource.Layout.layout_clientes_editar);

            cmd_cancelar_cliente = FindViewById<Button>(Resource.Id.cmd_cliente_cancelar);
            cmd_gravar_cliente = FindViewById<Button>(Resource.Id.cmd_cliente_gravar);
            edit_nome_cliente = FindViewById<EditText>(Resource.Id.edit_nome_cliente);
            edit_telefone_cliente = FindViewById<EditText>(Resource.Id.edit_telefone_cliente);


            cmd_cancelar_cliente.Click += delegate { this.Finish(); };

            cmd_gravar_cliente.Click += Cmd_gravar_cliente_Click;

            if (this.Intent.GetStringExtra("id_cliente") != null)
            {

                id_cliente = int.Parse(this.Intent.GetStringExtra("id_cliente"));
                ApresentaCliente();
                editar = true;


            }

        }

        private void ApresentaCliente()
        {
            DataTable dados = Cl_gestor.EXE_QUERY("select * from clientes where id_cliente = "+ id_cliente);

            edit_nome_cliente.Text = dados.Rows[0]["nome"].ToString();
            edit_telefone_cliente.Text = dados.Rows[0]["telefone"].ToString();

        }



        private void Cmd_gravar_cliente_Click(object sender, EventArgs e)
        {

            if (edit_nome_cliente.Text == ""  || edit_telefone_cliente.Text == "")
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
                
                parametro.Add(new SQLparametro("@id_cliente", Cl_gestor.ID_DISPONIVEL("clientes", "id_cliente")));

            }
            else
            {
                parametro.Add(new SQLparametro("@id_cliente",id_cliente));
            }

            parametro.Add(new SQLparametro("@nome", edit_nome_cliente.Text));
            parametro.Add(new SQLparametro("@telefone", edit_telefone_cliente.Text));
            parametro.Add(new SQLparametro("@atualizacao", DateTime.Now));


          

            #region novo
            if (!editar)
            { 
                DataTable dados = Cl_gestor.EXE_QUERY("select nome from clientes where nome = @nome", parametro);

                if (dados.Rows.Count != 0)
                {
                    AlertDialog.Builder caixa = new AlertDialog.Builder(this);
                    caixa.SetTitle("erro!");
                    caixa.SetMessage("Ja existe um cliente com o mesmo nome");
                    caixa.SetPositiveButton("OK", delegate { });
                    caixa.Show();
                    return;
                }

                Cl_gestor.NOM_QUERY(
                    "insert into clientes values ("+
                    "@id_cliente,"+
                    "@nome,"+
                    "@telefone,"+
                    "@atualizacao)",parametro);

                //  SetResult(Result.Ok, intent_temp);
                SetResult(Result.Ok, intent_temp);
                this.Finish();

            }
            #endregion

            #region editar
            else
            {

                DataTable dados = Cl_gestor.EXE_QUERY("select nome from clientes where nome = @nome AND id_cliente <> @id_cliente ", parametro);

                if (dados.Rows.Count != 0)
                {
                    AlertDialog.Builder caixa = new AlertDialog.Builder(this);
                    caixa.SetTitle("erro!");
                    caixa.SetMessage("Ja existe um cliente com o mesmo nome");
                    caixa.SetPositiveButton("OK", delegate { });
                    caixa.Show();
                    return;
                }

                Cl_gestor.NOM_QUERY(
                    "UPDATE Clientes SET " +
                    "nome = @nome," +
                    "telefone = @telefone," +
                    "atualizacao = @atualizacao " +
                    "WHERE id_cliente = @id_cliente ", parametro);
                SetResult(Result.Ok, intent_temp);
                this.Finish();

            }
            #endregion

        }
    }
}