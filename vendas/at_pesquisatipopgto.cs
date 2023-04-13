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
   
    [Activity(Label = "at_pesquisatipopgto")]
    public class at_pesquisatipopgto : Activity
    {
        
        SearchView pesquisapagamentotipo;
        List<tipopgto> PAGAMENTOS1;
        List<string> NOMES;
        ListView lista_tipopagamento;
        ArrayAdapter adaptador;
        Intent intent_temp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_pesquisatipopgto);
            // Create your application here

            pesquisapagamentotipo = FindViewById<SearchView>(Resource.Id.pesquisapagamentotipo);
            lista_tipopagamento = FindViewById<ListView>(Resource.Id.lista_tipopagamento);

            pesquisapagamentostipo();

            adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, NOMES);



            lista_tipopagamento.Adapter = adaptador;

            pesquisapagamentotipo.QueryTextChange += Pesquisacliente_venda_QueryTextChange;
            lista_tipopagamento.ItemClick += Lista_cliente_ItemClick; 


        }

        private void Lista_cliente_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Toast.MakeText(this, adaptador.GetItem(e.Position).ToString(), ToastLength.Short).Show();

            // cl_pagamentos pagamento_selecionado = PAGAMENTOS1[e.Position];
            //Toast.MakeText(this, cliente_selecionado.id_clientes.ToString(), ToastLength.Short).Show();
            //vars.nropgto = pagamento_selecionado.id_pagamento;

            int tam = Convert.ToString(lista_tipopagamento.GetItemAtPosition(e.Position)).Length;
            int tamred = tam - 5;

            string xnome1 = Convert.ToString(lista_tipopagamento.GetItemAtPosition(e.Position)).Substring(tamred, 5);


            //
            int codpag1x = Convert.ToInt32(xnome1.TrimStart());

            //Toast.MakeText(this, TAM.ToString(), ToastLength.Short).Show();
            DataTable dados = Cl_gestor.EXE_QUERY("select * from  tipopgto where id_tipopgto = " + codpag1x);
            if (dados.Rows.Count != 0)
            {

              

                Cl_gestor.NOM_QUERY("delete from tmptipopgto");


                List<SQLparametro> parametro = new List<SQLparametro>();


                parametro.Add(new SQLparametro("@id_tipopgto", Convert.ToInt32(dados.Rows[0]["id_tipopgto"])));
                parametro.Add(new SQLparametro("@descricao", dados.Rows[0]["descricao"]));

                Cl_gestor.NOM_QUERY(
                    "insert into tmptipopgto values ( " +
                    "@id_tipopgto," +
                    "@descricao) ", parametro);

            } 
            


            SetResult(Result.Ok, intent_temp);
            this.Finish();

        }

        private void Pesquisacliente_venda_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            adaptador.Filter.InvokeFilter(e.NewText);
           
        }

        public void pesquisapagamentostipo()
        {

            
            DataTable dados = Cl_gestor.EXE_QUERY("select * from tipopgto ");
            PAGAMENTOS1 = new List<tipopgto>();
            foreach (DataRow linha in dados.Rows)
            {
                PAGAMENTOS1.Add(new tipopgto()
                {
                    seq017 = Convert.ToInt32(linha["id_tipopgto"]),
                    descricao = linha["descricao"].ToString() + "     " + linha["id_tipopgto"].ToString()

                });

                NOMES = new List<string>();
                foreach (tipopgto pagamento in PAGAMENTOS1)
                    NOMES.Add(pagamento.descricao);

                
            }


        }

    }
}