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
   
    [Activity(Label = "at_pesquisacliente1")]
    public class at_pesquisacliente1 : Activity
    {
        
        SearchView pesquisacliente_venda;
        List<cl_clientes2> CLIENTES1;
        List<string> NOMES;
        ListView lista_cliente;
        ArrayAdapter adaptador;
        Intent intent_temp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_pesquisaxcliente);
            // Create your application here

            pesquisacliente_venda = FindViewById<SearchView>(Resource.Id.pesquisa);
            lista_cliente = FindViewById<ListView>(Resource.Id.lista_cliente);

            pesquisaclientes();

           // int w = 1;

            adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1,NOMES);
           
            lista_cliente.Adapter = adaptador;

            pesquisacliente_venda.QueryTextChange += Pesquisacliente_venda_QueryTextChange;
            lista_cliente.ItemClick += Lista_cliente_ItemClick; 


        }

        private void Lista_cliente_ItemClick(object sender, AdapterView.ItemClickEventArgs e)

        {
                ////Toast.MakeText(this, adaptador.GetItem(e.Position).ToString(), ToastLength.Short).Show();

                //// cl_clientes cliente_selecionado = CLIENTES1[e.Position];
                //// cl_clientes cliente_selecionado = CLIENTES1[index: e.Position];

                //int tam = Convert.ToString(lista_cliente.GetItemAtPosition(e.Position)).Length;
                //int tamred = tam - 5;

                //string xnome1 = Convert.ToString(lista_cliente.GetItemAtPosition(e.Position)).Substring(tamred,5);


                ////
                //int codcli1 = Convert.ToInt32(xnome1.TrimStart());

                ////Toast.MakeText(this, TAM.ToString(), ToastLength.Short).Show();
                //DataTable dados = Cl_gestor.EXE_QUERY("select * from CLIENTES where id_cliente = "+ codcli1);
                //if (dados.Rows.Count != 0)
                //{
                //    vars.numerocliente = Convert.ToInt32(dados.Rows[0]["id_cliente"]);

                //    Cl_gestor.NOM_QUERY("delete from  tmp_clientes");

                //    List<SQLparametro> parametro = new List<SQLparametro>();


                //    parametro.Add(new SQLparametro("@id_cliente", Convert.ToInt32(dados.Rows[0]["id_cliente"])));
                //    parametro.Add(new SQLparametro("@nome", dados.Rows[0]["nome"]));

                //    vars.nomeclientesel = dados.Rows[0]["nome"].ToString();

                //    Cl_gestor.NOM_QUERY(
                //        "insert into tmp_clientes values ( " +
                //        "@id_cliente," +
                //        "@nome) ", parametro);


                //}



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
            CLIENTES1 = new List<cl_clientes2>();
            foreach (DataRow linha in dados.Rows)
            {
                CLIENTES1.Add(new cl_clientes2()
                {
                    id_clientes = Convert.ToInt32(linha["id_cliente"]),
                    nome = linha["nome"].ToString() ,
                    endereco = linha["endereco"].ToString() + ", "+ linha["numero"].ToString(),
                    bairro = linha["bairro"].ToString(), 
                    cidade =  linha["cidade"].ToString() + " " + linha["cep"].ToString(),
                    telefone1 =  linha["telefone1"].ToString() + "  " + linha["telefone2"].ToString(),
                    celular  =  linha["celular"].ToString() + "  " + linha["contato"].ToString() ,
                    telefone = ""
                });

                                
                NOMES = new List<string>();
                foreach (cl_clientes2 cliente in CLIENTES1)
                    NOMES.Add(" Codigo:   "+cliente.id_clientes+"\n Cliente:   "+cliente.nome +"\n Endereço: "+ cliente.endereco + "\n Bairro:      " + cliente.bairro + "\n Cidade:     " + cliente.cidade + "\n Telefones: " + cliente.telefone1 + "\n" + cliente.celular);              

                //+ "-" + cliente.id_clientes.ToString().PadLeft(5)

            }


        }

    }
}