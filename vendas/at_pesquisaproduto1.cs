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
   
    [Activity(Label = "at_pesquisaproduto1")]
    public class at_pesquisaproduto1 : Activity
    {
        
        SearchView pesquisaproduto_venda;
        List<cl_produtos1> PRODUTOS1;
        List<string> NOMES;
        ListView lista_produto;
        ArrayAdapter adaptador;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_pesquisayproduto);
            // Create your application here

            pesquisaproduto_venda = FindViewById<SearchView>(Resource.Id.pesquisaprodutos);
            lista_produto = FindViewById<ListView>(Resource.Id.lista_produto);

            pesquisaprodx();

            adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, NOMES);



            lista_produto.Adapter = adaptador;

            pesquisaproduto_venda.QueryTextChange += Pesquisacliente_venda_QueryTextChange;
            lista_produto.ItemClick += Lista_cliente_ItemClick; 


        }

        //private 
        private void Lista_cliente_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ////Toast.MakeText(this, adaptador.GetItem(e.Position).ToString(), ToastLength.Short).Show();

            // cl_produtos produto_selecionado = PRODUTOS1[e.Position];
            ////Toast.MakeText(this, cliente_selecionado.id_clientes.ToString(), ToastLength.Short).Show();
            ////vars.numerocliente = produto_selecionado.id_produto;
            ////StartActivity(typeof(at_vendas));



            //int tam = Convert.ToString(lista_produto.GetItemAtPosition(e.Position)).Length;
            //int tamred = tam - 5;

            //string xnome1 = Convert.ToString(lista_produto.GetItemAtPosition(e.Position)).Substring(tamred, 5);


            ////
            //int codpro1 = Convert.ToInt32(xnome1.TrimStart());

            ////Toast.MakeText(this, TAM.ToString(), ToastLength.Short).Show();
            //DataTable dados = Cl_gestor.EXE_QUERY("select * from produtos where id_produto = " + codpro1);
            //if (dados.Rows.Count != 0)
            //{
            //    vars.numeroproduto = Convert.ToInt32(dados.Rows[0]["id_produto"]);
               
            //    vars.nomeproduto = (dados.Rows[0]["descricao"]).ToString();

            //    vars.precoproduto = Convert.ToDecimal(dados.Rows[0]["preco"]);

            
                
            //    //List<SQLparametro> parametro = new List<SQLparametro>();

            //    ////parametro.Add(new SQLparametro("@id_cliente", Convert.ToInt32(cliente_selecionado.id_clientes)));
            //    //parametro.Add(new SQLparametro("@id_produto", Convert.ToInt32(produto_selecionado.id_produto)));
            //    //parametro.Add(new SQLparametro("@descricao", produto_selecionado.nome_produto));

            //    //Cl_gestor.NOM_QUERY(
            //    //    "insert into tmp_vendas values ( " +
            //    //    "@id_produto," +
            //    //    "@descricao) ", parametro);
            //}
           
            //Intent x = new Intent(this, typeof(at_detalheproduto));

            //x.PutExtra("id_clientes", 0);
            //StartActivityForResult(x, 0);


            this.Finish();


        }


        //protected override void OnRestart()
        //{
        //    this.Finish();
        //}


        private void Pesquisacliente_venda_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            adaptador.Filter.InvokeFilter(e.NewText);
           
        }

        public void pesquisaprodx()
        {


            DataTable dados = Cl_gestor.EXE_QUERY("select * from produtos order by descricao  ");
            PRODUTOS1 = new List<cl_produtos1>();
            foreach (DataRow linha in dados.Rows)
            {
                PRODUTOS1.Add(new cl_produtos1()
                {
                    id_produto = Convert.ToInt32(linha["id_produto"]),
                    nome_produto = linha["descricao"].ToString(),
                    preco = Convert.ToDecimal(linha["preco"]),
                    codbar = linha["codbar"].ToString()

                });

             
            }

            NOMES = new List<string>();
            foreach (cl_produtos1 produto in PRODUTOS1)
                NOMES.Add(produto.nome_produto + "\n  Codigo " + produto.codbar + "\n  Preco " + produto.preco);


        }

    }
}