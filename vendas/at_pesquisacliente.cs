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

namespace vendas
{
   
    [Activity(Label = "at_pesquisacliente")]
    public class at_pesquisacliente : Activity
    {
        
        SearchView pesquisacliente_venda;
        List<cl_clientes> CLIENTES1;
        List<string> NOMES;
        ListView lista_cliente;
        ArrayAdapter adaptador;
        Intent intent_temp;
        Button voltar;
        Button gravar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_pesquisacliente);
            // Create your application here

            pesquisacliente_venda = FindViewById<SearchView>(Resource.Id.pesquisa);
            lista_cliente = FindViewById<ListView>(Resource.Id.lista_cliente);

            pesquisaclientes();

           // int w = 1;

            adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, NOMES);
           
            lista_cliente.Adapter = adaptador;

            pesquisacliente_venda.QueryTextChange += Pesquisacliente_venda_QueryTextChange;
            lista_cliente.ItemClick += Lista_cliente_ItemClick;
            //voltar.Click += Voltar_Click;
            //gravar.Click += Gravar_Click;
        }

        //private void Gravar_Click(object sender, EventArgs e)
        //{
        //    StartActivity(typeof(at_cadcli));
        //    this.Finish();

        //}

        //private void Voltar_Click(object sender, EventArgs e)
        //{
        //    this.Finish();
        //}

        private void Lista_cliente_ItemClick(object sender, AdapterView.ItemClickEventArgs e)

        {
            //Toast.MakeText(this, adaptador.GetItem(e.Position).ToString(), ToastLength.Short).Show();

            // cl_clientes cliente_selecionado = CLIENTES1[e.Position];
            // cl_clientes cliente_selecionado = CLIENTES1[index: e.Position];

            int tam = Convert.ToString(lista_cliente.GetItemAtPosition(e.Position)).Length;
            int tamred = tam - 5;

            string xnome1 = Convert.ToString(lista_cliente.GetItemAtPosition(e.Position)).Substring(tamred,5);


            //
            int codcli1 = Convert.ToInt32(xnome1.TrimStart());

            //Toast.MakeText(this, TAM.ToString(), ToastLength.Short).Show();
            DataTable dados = Cl_gestor.EXE_QUERY("select * from CLIENTES where id_cliente = "+ codcli1);
            if (dados.Rows.Count != 0)
            {
                vars.numerocliente = Convert.ToInt32(dados.Rows[0]["id_cliente"]);

                Cl_gestor.NOM_QUERY("delete from  tmp_clientes");

                List<SQLparametro> parametro = new List<SQLparametro>();


                parametro.Add(new SQLparametro("@id_cliente", Convert.ToInt32(dados.Rows[0]["id_cliente"])));
                parametro.Add(new SQLparametro("@nome", dados.Rows[0]["nome"]));

                vars.nomeclientesel = dados.Rows[0]["nome"].ToString();

                Cl_gestor.NOM_QUERY(
                    "insert into tmp_clientes values ( " +
                    "@id_cliente," +
                    "@nome) ", parametro);


            }



            SetResult(Result.Ok, intent_temp);

            this.Finish();

        }

        private void Pesquisacliente_venda_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            adaptador.Filter.InvokeFilter(e.NewText);
           
        }

        public void pesquisaclientes()
        {

            
            DataTable dados = Cl_gestor.EXE_QUERY("select * from clientes order by nome asc ");
            CLIENTES1 = new List<cl_clientes>();
            foreach (DataRow linha in dados.Rows)
            {
                CLIENTES1.Add(new cl_clientes()
                {
                    id_clientes = Convert.ToInt32(linha["id_cliente"]),
                    nome = linha["nome"].ToString()+"     "+ linha["id_cliente"]

                });

                                
                NOMES = new List<string>();
                foreach (cl_clientes cliente in CLIENTES1)
                   NOMES.Add(cliente.nome);


                //+ "-" + cliente.id_clientes.ToString().PadLeft(5)

            }


        }

    }
}