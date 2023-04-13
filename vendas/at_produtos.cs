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
    [Activity(Label = "at_produtos")]
    public class at_produtos : Activity
    {
        //Button cmd_adicionar_produto;
        ListView lista_produtos;
        TextView label_numero_produtos;
        List<cl_produtos> PRODUTOSS;
        List<string> NOMES_PRODUTOS; 



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.layout_produtos);

           // cmd_adicionar_produto = FindViewById<Button>(Resource.Id.cmd_adicionar_produto);
            lista_produtos = FindViewById<ListView>(Resource.Id.lista_produtos);
            label_numero_produtos = FindViewById<TextView>(Resource.Id.label_numero_produtos);


            ConstroiListaProdutos();

           // cmd_adicionar_produto.Click += cmd_adicionar_produtos_Click;
            lista_produtos.ItemLongClick += Lista_produtos_ItemLongClick; 

        }

        //protected override void 

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                //at_clientes at = new at_clientes();
                ConstroiListaProdutos();
            }
        }

        private void Lista_produtos_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            cl_produtos produto_selecionado = PRODUTOSS[e.Position];

            AlertDialog.Builder caixa_editar_eliminar = new AlertDialog.Builder(this);
            caixa_editar_eliminar.SetTitle("EDITAR | ELIMINAR");
            caixa_editar_eliminar.SetMessage(produto_selecionado.nome_produto);
           // caixa_editar_eliminar.SetCancelable(false);


            caixa_editar_eliminar.SetPositiveButton("Editar", delegate { EditarProduto(produto_selecionado.id_produto);});
            caixa_editar_eliminar.SetNegativeButton("Eliminar", delegate { EliminarProduto(produto_selecionado.id_produto);});



            caixa_editar_eliminar.Show();
            
        }
        public void EliminarProduto(int id_Produto)
        {

            Cl_gestor.NOM_QUERY("Delete from produtos where id_produto = " + id_Produto);

            ConstroiListaProdutos();

        }


        //----------------------------------editar 
        private void EditarProduto(int id_Produto)
        {
            Intent i = new Intent(this, typeof(at_produtos_editar));
            i.PutExtra("id_produto", id_Produto.ToString());
            StartActivityForResult(i, 0);
        }


        //public void cmd_adicionar_produtos_Click(object sender, EventArgs e)
        //{
        //    //StartActivity(typeof(at_clientes_editar));

        //    Intent i = new Intent(this, typeof(at_produtos_editar));
        //    StartActivityForResult(i, 0);          
        //}

        private void ConstroiListaProdutos()
        {
            PRODUTOSS = new List<cl_produtos>();
            DataTable dados = Cl_gestor.EXE_QUERY("select * from produtos order by descricao asc");
            foreach (DataRow linha  in dados.Rows)
            {
                PRODUTOSS.Add(new cl_produtos()
                {
                    id_produto = Convert.ToInt32(linha["id_produto"]),
                    nome_produto = linha["descricao"].ToString(),
                   // preco = 0 //int.Parse(linha["preco"].ToString())

                });

            }

            NOMES_PRODUTOS = new List<string>();
            foreach (cl_produtos produto in PRODUTOSS)
                NOMES_PRODUTOS.Add(produto.nome_produto);

            ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, NOMES_PRODUTOS);
            lista_produtos.Adapter = adaptador;


            ApresentaTotalProdutos(PRODUTOSS.Count);
        }


        private void ApresentaTotalProdutos(int total_Produtos)
        {
            //int total_Clientes = 0;

            //DataTable dados = Cl_gestor.EXE_QUERY("select id_cliente from clientes");
            //if (dados.Rows.Count != 0)
            //{
            //    total_Clientes = dados.Rows.Count;
            //}

            label_numero_produtos.Text = "Produtos Registrados: " + total_Produtos;
        }
    }
}