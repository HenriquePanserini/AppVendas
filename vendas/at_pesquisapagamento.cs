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
   
    [Activity(Label = "at_pesquisapagamento")]
    public class at_pesquisapagamento : Activity
    {
        
        SearchView pesquisapagamento_venda;
        List<cl_pagamentos> PAGAMENTOS1;
        List<string> NOMES;
        ListView lista_pagamento;
        ArrayAdapter adaptador;
        Intent intent_temp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_pesquisapagamento);
            // Create your application here

            pesquisapagamento_venda = FindViewById<SearchView>(Resource.Id.pesquisapagamento);
            lista_pagamento = FindViewById<ListView>(Resource.Id.lista_pagamento);

            pesquisapagamentos();

            adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, NOMES);


                       
            lista_pagamento.Adapter = adaptador;

            pesquisapagamento_venda.QueryTextChange += Pesquisacliente_venda_QueryTextChange;
            lista_pagamento.ItemClick += Lista_cliente_ItemClick; 


        }

        private void Lista_cliente_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Toast.MakeText(this, adaptador.GetItem(e.Position).ToString(), ToastLength.Short).Show();

            // cl_pagamentos pagamento_selecionado = PAGAMENTOS1[e.Position];
            //Toast.MakeText(this, cliente_selecionado.id_clientes.ToString(), ToastLength.Short).Show();
            //vars.nropgto = pagamento_selecionado.id_pagamento;

            int tam = Convert.ToString(lista_pagamento.GetItemAtPosition(e.Position)).Length;
            int tamred = tam - 5;

            string xnome1 = Convert.ToString(lista_pagamento.GetItemAtPosition(e.Position)).Substring(tamred, 5);


            //
            int codpag1 = Convert.ToInt32(xnome1.TrimStart());

            //Toast.MakeText(this, TAM.ToString(), ToastLength.Short).Show();
            DataTable dados = Cl_gestor.EXE_QUERY("select * from  pagamento where id_pagamento = " + codpag1);
            if (dados.Rows.Count != 0)
            {

                vars.nropgto = Convert.ToInt32(dados.Rows[0]["id_pagamento"]);

                Cl_gestor.NOM_QUERY("delete from  tmp_pagamento");


                List<SQLparametro> parametro = new List<SQLparametro>();


                parametro.Add(new SQLparametro("@id_pagamento", Convert.ToInt32(dados.Rows[0]["id_pagamento"])));
                parametro.Add(new SQLparametro("@descricao", dados.Rows[0]["descricao"]));
                parametro.Add(new SQLparametro("@dias",0));

                Cl_gestor.NOM_QUERY(
                    "insert into tmp_pagamento (id_pagamento, descricao) values ( " +
                    "@id_pagamento," +
                    "@descricao);", parametro);

            } 
            


            SetResult(Result.Ok, intent_temp);
            this.Finish();

        }

        private void Pesquisacliente_venda_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            adaptador.Filter.InvokeFilter(e.NewText);
           
        }

        public void pesquisapagamentos()
        {

            
            DataTable dados = Cl_gestor.EXE_QUERY("select * from pagamento ");
            PAGAMENTOS1 = new List<cl_pagamentos>();
            foreach (DataRow linha in dados.Rows)
            {
                PAGAMENTOS1.Add(new cl_pagamentos()
                {
                    id_pagamento = Convert.ToInt32(linha["id_pagamento"]),
                    descricao = linha["descricao"].ToString() + "     " + linha["id_pagamento"]

                });

                NOMES = new List<string>();
                foreach (cl_pagamentos pagamento in PAGAMENTOS1)
                    NOMES.Add(pagamento.descricao);

                
            }


        }

    }
}