using Android.Content;
using Android.Text;
using Android.Text.Method;
using Android.Widget;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;


namespace vendas
{
    public class cl_clientes
    {
        public int id_clientes { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }

    }

    public class cl_clientes2
    {
        public int id_clientes { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string endereco { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string nro { get; set; }
        public string cep { get; set; }
        public string telefone1 { get; set; }
        public string telefone2 { get; set; }
        public string celular { get; set; }
        public string contato { get; set; }
    }


    public class clie
    {
        public int clicod { get; set; }
        public string clinom { get; set; }
        public string cliend { get; set; }
        public string clibai { get; set; }
        public string clicid { get; set; }
        public string numero { get; set; }
        public string clicep { get; set; }
        public string clitel { get; set; }
        public string clitel2 { get; set; }
        public string celular { get; set; }
        public string clicon { get; set; }
        public string email { get; set; }
        public string seq004 { get; set; }

    }
   

    public class cl_produtos
    {
        public int id_produto { get; set; }
        public string nome_produto { get; set; }
        public decimal preco { get; set; }
        public string codbar { get; set; }
  
    }

    public class cl_produtos1
    {
        public int id_produto { get; set; }
        public string nome_produto { get; set; }
        public decimal preco { get; set; }
        public string codbar { get; set; }

    }

    public class tslc001
    {

        public int seqmax { get; set; }

    }

    public class tslv020
    {
        public int seqmax{ get; set; }
      
    }
    public class linped
    {
        public int totmax { get; set; }

    }

    public class tslv0202
    {
        public int seqmax { get; set; }
        public string micro { get; set; }

    }



    public class cl_pagamentos
    {
        public int id_pagamento { get; set; }
        public string descricao { get; set; }
        
    }



    public class cl_vendas
    {
        public int id_produto { get; set; }
        public string descricao { get; set; }
        public decimal quantidade { get; set; }
        public decimal preco { get; set; }
        public decimal desconto { get; set; }
        public decimal precodesconto { get; set; }
        public decimal total { get; set; }
    }

    public class cl_vendasst
    {
        public int id_produto { get; set; }
        public string descricao { get; set; }
        public string quantidade { get; set; }
        public string preco { get; set; }
        public string desconto { get; set; }
        public string precodesconto { get; set; }
        public string total { get; set; }
    }

    public class apresemta_vendas
    {
        public string id_produto { get; set; }
        public string descricao { get; set; }
        public string quantidade { get; set; }
        public string preco { get; set; }
        public string desconto { get; set; }
        public string precodesconto { get; set; }
        public string total { get; set; }
    }


    public class vars
    {
        public static int numerocliente, numeroproduto, nropgto, pedidoselecionado, pedidoselecionadodet,numeropedidosalvo, numeropedidosalvol, numerovendedor, nbarra, npedidoemail,npedidoaltera,numeropedidosalvo_or,parvenda,numeroseqsalvo;
        public static int gerentesn,maxlinhas, parusuario;
        public static string nomeproduto, nomeclientesel, pedidoorcamento;
        public static decimal precoproduto, precoproduto2, precoproduto3, precoproduto4, quantidadeproduto,precoprodutodesc,totalproduto,desconto,descontoprod,descontovendedor;
        public static bool pedis,alterarped,flagmeiopedido,dupliped, web_local;
        public static string nomeemp, enderecoemp, bairroemp, cidadeemp, foneemp,obspedx;
        
    }


    public class Contato
    {
        public string Nome { get; set; }
        public string Status { get; set; }
        public string ImagemUrl { get; set; }
    }


    public class Parametrocli
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Fone { get; set; }

    }
    public class Model
    {
        public int Numero { get; set; }
        public string Nome { get; set; }
        public string Nome2 { get; set; }
    }

                
    public class pedidos
    {
        public int id_pedido { get; set; }
        public int id_cliente { get; set; }
        public string nome { get; set; }
        public int id_pgto { get; set; }
        public string total { get; set; }
        public DateTime data { get; set; }
        public DateTime datasincro { get; set; }
        public string nrovendabco { get; set; }
        public string checar { get; set; }


    }

    public class pedidosst
    {
        public int id_pedido { get; set; }
        public int id_cliente { get; set; }
        public string nome { get; set; }
        public int id_pgto { get; set; }
        public double total { get; set; }
        public DateTime data { get; set; }

    }

    public class pedidositens
    {
        public int id_produto { get; set; }
        public string descricao { get; set; }
        public double quantidade { get; set; }
        public double preco { get; set; }
        public double desconto { get; set; }
        public double precodesconto { get; set; }
        public double total { get; set; }
      

    }

    public class rel_receber
    {
        public int seq { get; set; }
        public DateTime data { get; set; }
        public int id_cliente { get; set; }
        public int codform { get; set; }
        public int cupom { get; set; }
        public int quant { get; set; }
        public double total { get; set; }
        public string descricao { get; set; }
        public double desconto { get; set; }
        public double preco { get; set; }

    }
   
    public class cl_prod
    {
        
        public int codigo { get; set; }
        public string descpro { get; set; }
        public decimal preco1 { get; set; }
        public string codbar { get; set; }
        public decimal preco2 { get; set; }
        public decimal preco3 { get; set; }
        public decimal preco4 { get; set; }
        public decimal descapp { get; set; }
        public string uni { get; set; }
        public string qtd { get; set; }
        public string custo { get; set; }

    }

    public class cl_pgto
    {

        public int codigo { get; set; }
        public string descricao { get; set; }
      
    }
    public class cl_receber
    {
        public string titulo { get; set; }
        public string nome { get; set; }
        public DateTime emissao { get; set; }
        public DateTime vencimento { get; set; }
        public decimal valor { get; set; }

    }

    public class tipopgto
    {
        public int seq017 { get; set; }
        public string descricao { get; set; }
    }

    
       
    public class cl_usu
    {

        public int codigo { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public string descvendas { get; set; }
        public string gerente { get; set; }

    }

    public class testepg
    {

        public int codigo { get; set; }
        public string descricao { get; set; }
       

    }

    class DecimalFilter : Java.Lang.Object, IInputFilter
    {
        //Pattern mPattern;
        String regex = "[0-9]+((\\.[0-9]{0," + (2 - 1) + "})?)||(\\.)?";
        public DecimalFilter(int digitsAfterZero)
        {
            //mPattern = Pattern.compile("[0-9]+((\\.[0-9]{0," + (digitsAfterZero - 1) + "})?)||(\\.)?");
            regex = "[0-9]+((\\.[0-9]{0," + (digitsAfterZero - 1) + "})?)||(\\.)?";
        }

        public Java.Lang.ICharSequence FilterFormatted(Java.Lang.ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(dest.ToString(), regex))
            {
                return new Java.Lang.String(string.Empty);
            }
            return null;
        }
    }

    public interface IShare
    {
        Task Show(string title, string message, string filePath);
    }

    class ShareAndroid : IShare
    {
        private readonly Android.Content.Context context;
        public ShareAndroid()
        {
            context = Android.App.Application.Context;
        }
        public Task Show(string title, string message, string filePath)
        {
            var uri = Android.Net.Uri.Parse("file://" + filePath);
            var contentType = "application/pdf";
            var intent = new Intent(Intent.ActionSend);
            intent.PutExtra(Intent.ExtraStream, uri);
            intent.PutExtra(Intent.ExtraText, string.Empty);
            intent.PutExtra(Intent.ExtraSubject, message ?? string.Empty);
            intent.SetType(contentType);
            var chooserIntent = Intent.CreateChooser(intent, title ?? string.Empty);
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            context.StartActivity(chooserIntent);

            return Task.FromResult(true);
        }
    }


}