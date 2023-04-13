using System;
using Mono.Data.Sqlite;
namespace vendas
{
    public class SQLparametro
    {
        public string nome { get; set; }
        public object valor  { get; set; }

        public SQLparametro(string parametro,object valor)
        {
            this.nome = parametro;
            this.valor = valor;
            
        }

        
    }
}