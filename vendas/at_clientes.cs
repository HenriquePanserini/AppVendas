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
    [Activity(Label = "at_clientes")]
    public class at_clientes : Activity
    {
        Button voltar;
        Button cadastrar;
        ListView lista_clientes;
        TextView label_numero_clientes;
        List<cl_clientes> CLIENTES;
        List<string> NOMES; 



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.layout_clientes);

            voltar = FindViewById<Button>(Resource.Id.voltar);
            cadastrar = FindViewById<Button>(Resource.Id.cadclix);

            lista_clientes = FindViewById<ListView>(Resource.Id.lista_clientes);
            label_numero_clientes = FindViewById<TextView>(Resource.Id.label_numero_clientes);


            ConstroiListaClientes();

          //  cmd_adicionar_cliente.Click += Cmd_adicionar_cliente_Click;
            lista_clientes.ItemLongClick += Lista_clientes_ItemLongClick;
            cadastrar.Click += Cadastrar_Click;
            voltar.Click += Voltar_Click;
        }

        private void Voltar_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void Cadastrar_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(at_cadcli));
            this.Finish();
        }

        //protected override void 

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                //at_clientes at = new at_clientes();
                ConstroiListaClientes();
            }
        }

        private void Lista_clientes_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
           cl_clientes cliente_selecionado = CLIENTES[e.Position];

            AlertDialog.Builder caixa_editar_eliminar = new AlertDialog.Builder(this);
            caixa_editar_eliminar.SetTitle("EDITAR | ELIMINAR");
            caixa_editar_eliminar.SetMessage(cliente_selecionado.nome);
           // caixa_editar_eliminar.SetCancelable(false);


            caixa_editar_eliminar.SetPositiveButton("Editar", delegate { EditarCliente(cliente_selecionado.id_clientes);});
            caixa_editar_eliminar.SetNegativeButton("Eliminar", delegate { EliminarCliente(cliente_selecionado.id_clientes);});



            caixa_editar_eliminar.Show();
            
        }
        public void EliminarCliente(int id_Cliente)
        {

            Cl_gestor.NOM_QUERY("Delete from clientes where id_cliente = " + id_Cliente);

            ConstroiListaClientes();

        }


        //----------------------------------editar 
        private void EditarCliente(int id_Cliente)
        {
            Intent i = new Intent(this, typeof(at_clientes_editar));
            i.PutExtra("id_cliente", id_Cliente.ToString());
            StartActivityForResult(i, 0);
        }


        //public void Cmd_adicionar_cliente_Click(object sender, EventArgs e)
        //{
        //    //StartActivity(typeof(at_clientes_editar));

        //    Intent i = new Intent(this, typeof(at_clientes_editar));
        //    StartActivityForResult(i, 0);          
        //}

        private void ConstroiListaClientes()
        {
            CLIENTES = new List<cl_clientes>();
            DataTable dados = Cl_gestor.EXE_QUERY("select * from clientes order by nome asc");
            foreach (DataRow linha  in dados.Rows)
            {
                CLIENTES.Add(new cl_clientes()
                {
                    id_clientes = Convert.ToInt32(linha["id_cliente"]),
                    nome = linha["nome"].ToString(),
                    telefone = linha["telefone"].ToString()

                });

            }

            NOMES = new List<string>();
            foreach (cl_clientes cliente in CLIENTES)
                NOMES.Add(cliente.nome);

            ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, NOMES);
            lista_clientes.Adapter = adaptador;

          
            ApresentaTotalClientes(CLIENTES.Count);
        }


        private void ApresentaTotalClientes(int total_Clientes)
        {
            //int total_Clientes = 0;

            //DataTable dados = Cl_gestor.EXE_QUERY("select id_cliente from clientes");
            //if (dados.Rows.Count != 0)
            //{
            //    total_Clientes = dados.Rows.Count;
            //}

            label_numero_clientes.Text = "Clientes Registrados: " + total_Clientes;
        }
    }
}