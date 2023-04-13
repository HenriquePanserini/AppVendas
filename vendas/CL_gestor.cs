using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.Content.PM;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mono.Data.Sqlite;
using System.Threading.Tasks;
using vendas;
using System.Data;

namespace vendas
{ 
    public static  class Cl_gestor
    {
        static string pasta_dados;
        static string base_dados;
       
        //public static void InicioAplicacao()
        public static async Task InicioAplicacaosync()
        {

            pasta_dados = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Vendas");

            if (!Directory.Exists(pasta_dados))
            {
                Directory.CreateDirectory(pasta_dados);
            }
            else
            {

                ////base_dados = pasta_dados + @"/dados.db";
                ////SqliteConnection ligacao = new SqliteConnection("Data Source = " + base_dados);
                ////ligacao.Open();


                ////SqliteCommand comando = new SqliteCommand();
                ////comando.Connection = ligacao;

                ////Cl_gestor.NOM_QUERY("delete from parametro");

                //comando.CommandText =
                //   "DROP TABLE parametro";
                //comando.ExecuteNonQuery();

                //comando.CommandText =
                //   "CREATE TABLE parametro(" +
                //   "id_parametro                               integer not null, " +
                //   "endereco                                     NVARCHAR(200))";
                //comando.ExecuteNonQuery();

                //List<SQLparametro> parametro1 = new List<SQLparametro>();

                ////parametro.Add(new SQLparametro("@id_cliente", Convert.ToInt32(cliente_selecionado.id_clientes)));
                //parametro1.Add(new SQLparametro("@id_parametro", 1));
                //parametro1.Add(new SQLparametro("@endereco", "http://191.253.238.178:1024"));

                //Cl_gestor.NOM_QUERY(
                //    "insert into parametro values ( " +
                //    "@id_parametro," +
                //    "@endereco) ", parametro1);
            }

            base_dados = pasta_dados + @"/dados.db";

            if (!File.Exists(base_dados))
            {

                SqliteConnection.CreateFile(base_dados);

                SqliteConnection ligacao = new SqliteConnection("Data Source=" + base_dados);
                ligacao.Open();


                SqliteCommand comando = new SqliteCommand();
                comando.Connection = ligacao;

                ////fina
                ///

                comando.CommandText =
                   "CREATE TABLE receber(" +
                   "titulo                                   NVARCHAR(10), " +
                   "emissao                                  DATETIME," +
                   "vencimento                               DATETIME," +
                   "valor                                    DECIMAL(10,2)," +
                   "nome                                     NVARCHAR(20))";
                comando.ExecuteNonQuery();


                //clientes
                comando.CommandText =
                    "CREATE TABLE CLIENTES(" +
                    "id_cliente                               integer not null Primary Key, " +
                    "nome                                     NVARCHAR(50)," +
                    "telefone                                 NVARCHAR(20)," +
                    "atualizacao                              DATETIME" +
                    "endereco                                 NVARCHAR(100)," +
                    "bairro                                   NVARCHAR(50)," +
                    "cidade                                   NVARCHAR(50)," +
                    "numero                                   NVARCHAR(10)," +
                    "cep                                      NVARCHAR(10)," +
                    "telefone1                                NVARCHAR(20)," +
                    "telefone2                                NVARCHAR(20)," +
                    "celular                                  NVARCHAR(20)," +
                    "contato                                  NVARCHAR(20)," +
                    "cnpj                                     NVARCHAR(20)," +
                    "ie                                       NVARCHAR(20)," +
                    "cpf                                      NVARCHAR(20)," +
                    "rg                                       NVARCHAR(20)," +
                    "novo                                     NVARCHAR(20)," +
                    "fantasia                                 NVARCHAR(20)," +
                    "email                                    NVARCHAR(20))";


                comando.ExecuteNonQuery();


                comando.CommandText =
                 "CREATE TABLE  IF NOT EXISTS parametrocli(" +
                 "nome                               NVARCHAR(80), " +
                 "endereco                           NVARCHAR(80), " +
                 "bairro                             NVARCHAR(80), " +
                 "cidade                             NVARCHAR(60), " +
                 "fone                               NVARCHAR(60))";
                comando.ExecuteNonQuery();


                //produtos
                comando.CommandText =
                    "Create Table Produtos(" +
                    "id_produto                               integer not null Primary Key, " +
                    "descricao                                nvarchar(100)," +
                    "preco                                    DECIMAL(10,2)," +
                    "preco2                                   DECIMAL(10,2)," +
                    "preco3                                   DECIMAL(10,2)," +
                    "preco4                                   DECIMAL(10,2)," +
                    "atualizacao                              DATETIME)";
                comando.ExecuteNonQuery();

                comando.CommandText =
                  "Create Table pagamento(" +
                   "id_pagamento                             integer not null Primary Key, " +
                   "descricao                                nvarchar(30))";
                comando.ExecuteNonQuery();

                comando.CommandText =
                  "Create Table usuario(" +
                   "id_usuario                             integer not null Primary Key, " +
                   "nome                                 nvarchar(30)," +
                   "senha                                nvarchar(15))";
                comando.ExecuteNonQuery();


                ////vendas
                //comando.CommandText =
                //    "Create Table vendas(" +
                //    "id_venda                                 integer not null Primary Key, " +
                //    "id_cliente                               integer," +
                //    "id_produto                               integer," +
                //    "quantidade                               integer," +
                //    "preco                                    integer," +
                //    "atualizacao                              DATETIME)";

                // comando.ExecuteNonQuery();


                comando.CommandText =
                    "CREATE TABLE tmp_clientes(" +
                    "id_cliente                               integer not null, " +
                    "nome                                     NVARCHAR(50))";
                comando.ExecuteNonQuery();


                comando.CommandText =
                   "CREATE TABLE parametro(" +
                   "id_parametro                               integer not null, " +
                   "endereco                                     NVARCHAR(200))";
                comando.ExecuteNonQuery();


                comando.CommandText =
                 "Create Table tmp_pagamento(" +
                  "id_pagamento                             integer not null Primary Key, " +
                  "descricao                                nvarchar(30))";
                comando.ExecuteNonQuery();



                comando.CommandText =
               "Create Table tmp_vendas_prod(" +
               "id_produto                               integer," +
               "descricao                                nvarchar(30)," +
               "quantidade                               DECIMAL(10,2)," +
               "preco                                    DECIMAL(10,2)," +
               "desconto                                 DECIMAL(10,2)," +
               "precodesconto                            DECIMAL(10,2)," +
               "total                                    DECIMAL(10,2))";

                comando.ExecuteNonQuery();


                comando.CommandText =
              "Create Table vendasprd(" +
              "id_pedido                                interger," +
              "id_produto                               integer," +
              "descricao                                nvarchar(70)," +
              "quantidade                               DECIMAL(10,2)," +
              "preco                                    DECIMAL(10,2)," +
              "desconto                                 DECIMAL(10,2)," +
              "precodesconto                            DECIMAL(10,2)," +
              "total                                    DECIMAL(10,2))";
                comando.ExecuteNonQuery();

                comando.CommandText =
                "Create Table vendas(" +
                "id_pedido                                interger," +
                "id_cliente                               integer," +
                "nome                                     nvarchar(70)," +
                "id_pgto                                  integer," +
                "desconto                                 DECIMAL(10,2)," +
                "total                                    DECIMAL(10,2)," +
                "data                                     DATETIME)";
                comando.ExecuteNonQuery();

                comando.CommandText =
                 "Create Table vendasprd_BKP(" +
                 "id_pedido                                integer," +
                 "id_produto                               integer," +
                 "descricao                                nvarchar(30)," +
                 "quantidade                               DECIMAL(10,2)," +
                 "preco                                    DECIMAL(10,2)," +
                 "desconto                                 DECIMAL(10,2)," +
                 "precodesconto                            DECIMAL(10,2)," +
                 "total                                    DECIMAL(10,2))";
                comando.ExecuteNonQuery();

                comando.CommandText =
                  "Create Table vendas_BKP(" +
                  "id_pedido                                integer," +
                  "id_cliente                               integer," +
                  "nome                                     nvarchar(30)," +
                  "id_pgto                                  integer," +
                  "desconto                                 DECIMAL(10,2)," +
                  "total                                    DECIMAL(10,2)," +
                  "data                                     DATETIME)";
                comando.ExecuteNonQuery();

                comando.CommandText =
                "Create Table sincronizar(" +
                "id_seq                              integer not nul default 0," +
                "data                                     DATETIME)";
                comando.ExecuteNonQuery();

                List<SQLparametro> parametro1 = new List<SQLparametro>();

                //parametro.Add(new SQLparametro("@id_cliente", Convert.ToInt32(cliente_selecionado.id_clientes)));
                parametro1.Add(new SQLparametro("@id_pagamento", 2));
                parametro1.Add(new SQLparametro("@descricao", "30 dias"));
                parametro1.Add(new SQLparametro("@dias", 0));

                Cl_gestor.NOM_QUERY(
                    "insert into pagamento (id_pagamento, descricao) values ( " +
                    "@id_pagamento," +
                    "@descricao);", parametro1);



                ligacao.Clone();
                ligacao.Dispose();
            }
            else
            {

                SqliteConnection ligacao = new SqliteConnection("Data Source=" + base_dados);
                ligacao.Open();


                SqliteCommand comando = new SqliteCommand();
                comando.Connection = ligacao;

                DataTable dados;

                dados = Cl_gestor.EXE_QUERY("select * from  parametro ");

                comando.CommandText =
                  "Create Table if not exists vendasprd_BKP(" +
                  "id_pedido                                integer," +
                  "id_produto                               integer," +
                  "descricao                                nvarchar(30)," +
                  "quantidade                               DECIMAL(10,2)," +
                  "preco                                    DECIMAL(10,2)," +
                  "desconto                                 DECIMAL(10,2)," +
                  "precodesconto                            DECIMAL(10,2)," +
                  "total                                    DECIMAL(10,2))";
                comando.ExecuteNonQuery();

                comando.CommandText =
                  "Create Table if not exists vendas_BKP(" +
                  "id_pedido                                integer," +
                  "id_cliente                               integer," +
                  "nome                                     nvarchar(30)," +
                  "id_pgto                                  integer," +
                  "desconto                                 DECIMAL(10,2)," +
                  "total                                    DECIMAL(10,2)," +
                  "data                                     DATETIME)";
                comando.ExecuteNonQuery();

                if (dados.Columns.Contains("usuario"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE parametro ADD usuario NVARCHAR(100) ";
                    comando.ExecuteNonQuery();
                }


                if (dados.Columns.Contains("senha"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE parametro ADD senha NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }

                if (dados.Columns.Contains("smtp"))
                {

                }
                else
                {
                    comando.CommandText = " ALTER TABLE parametro ADD smtp NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }
                if (dados.Columns.Contains("vendedor"))
                {

                }
                else
                {
                    comando.CommandText = " ALTER TABLE parametro ADD vendedor NVARCHAR(12) ";
                    comando.ExecuteNonQuery();
                }
                if (dados.Columns.Contains("caminholocal"))
                {

                }
                else
                {
                    comando.CommandText = " ALTER TABLE parametro ADD caminholocal NVARCHAR(40) ";
                    comando.ExecuteNonQuery();
                }

                if (dados.Columns.Contains("vendasdesc"))
                {

                }
                else
                {
                    comando.CommandText = " ALTER TABLE parametro ADD vendasdesc integer ";
                    comando.ExecuteNonQuery();
                }

                if (dados.Columns.Contains("login"))
                {

                }
                else
                {
                    comando.CommandText = " ALTER TABLE parametro ADD login integer ";
                    comando.ExecuteNonQuery();
                }

                if (dados.Columns.Contains("banco"))
                {

                }
                else
                {
                    comando.CommandText = " ALTER TABLE parametro ADD banco NVARCHAR(40) ";
                    comando.ExecuteNonQuery();
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////
                comando.CommandText =
                  "Create Table IF NOT EXISTS usuario(" +
                   "id_usuario                             integer not null Primary Key, " +
                   "nome                                 nvarchar(30)," +
                   "senha                                nvarchar(15))";
                comando.ExecuteNonQuery();



                dados = Cl_gestor.EXE_QUERY("select * from CLIENTES ");

                if (dados.Columns.Contains("endereco"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD endereco NVARCHAR(100) ";
                    comando.ExecuteNonQuery();
                }

                if (dados.Columns.Contains("bairro"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD bairro NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }

                if (dados.Columns.Contains("cidade"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD cidade NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }

                if (dados.Columns.Contains("numero"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD numero NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }
                if (dados.Columns.Contains("cep"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD cep NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }

                if (dados.Columns.Contains("telefone1"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD telefone1 NVARCHAR(20) ";
                    comando.ExecuteNonQuery();
                }

                if (dados.Columns.Contains("telefone2"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD telefone2 NVARCHAR(20) ";
                    comando.ExecuteNonQuery();
                }

                if (dados.Columns.Contains("celular"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD celular NVARCHAR(20) ";
                    comando.ExecuteNonQuery();
                }
                if (dados.Columns.Contains("contato"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD contato NVARCHAR(20) ";
                    comando.ExecuteNonQuery();
                }
                if (dados.Columns.Contains("email"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD email NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }
                if (dados.Columns.Contains("cnpj"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD cnpj NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }
                if (dados.Columns.Contains("ie"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD ie NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }
                if (dados.Columns.Contains("cpf"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD cpf NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }
                if (dados.Columns.Contains("rg"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD rg NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }
                if (dados.Columns.Contains("novo"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD novo integer ";
                    comando.ExecuteNonQuery();
                }
                if (dados.Columns.Contains("fantasia"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD fantasia NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }
                if (dados.Columns.Contains("email"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD email NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }

                if (dados.Columns.Contains("estado"))
                {

                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD estado NVARCHAR(30) ";
                    comando.ExecuteNonQuery();
                }

                if(dados.Columns.Contains("vend"))
                {

                }
                else
                {
                    comando.CommandText = " ALTER TABLE CLIENTES ADD vend NVARCHAR(30) ";
                    comando.ExecuteNonQuery();
                }

                DataTable dados1;
                dados1 = Cl_gestor.EXE_QUERY("select * from Produtos ");

                if (dados1.Columns.Contains("codbar"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE Produtos ADD codbar NVARCHAR(16) ";
                    comando.ExecuteNonQuery();
                }

                if (dados1.Columns.Contains("preco2"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE Produtos ADD preco2 DECIMAL(10,2) ";
                    comando.ExecuteNonQuery();
                }

                if (dados1.Columns.Contains("preco3"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE Produtos ADD preco3 DECIMAL(10,2) ";
                    comando.ExecuteNonQuery();
                }

                if (dados1.Columns.Contains("preco4"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE Produtos ADD preco4 DECIMAL(10,2) ";
                    comando.ExecuteNonQuery();
                }

                if (dados1.Columns.Contains("descapp"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE Produtos ADD descapp DECIMAL(5,2) ";
                    comando.ExecuteNonQuery();
                }

                if (dados1.Columns.Contains("uni"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE Produtos ADD uni NVARCHAR(4) ";
                    comando.ExecuteNonQuery();
                }

                if (dados1.Columns.Contains("qtd"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE Produtos ADD qtd NVARCHAR(5) ";
                    comando.ExecuteNonQuery();
                }


                if (dados1.Columns.Contains("custo"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE Produtos ADD custo DECIMAL(10,2) ";
                    comando.ExecuteNonQuery();
                }


                //Cl_gestor.NOM_QUERY(" delete from vendas ");

                DataTable dados2zx;
                dados2zx = Cl_gestor.EXE_QUERY("select * from vendasprd ");

                if (dados2zx.Columns.Contains("nrovendabco"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendasprd ADD nrovendabco integer  ";
                    comando.ExecuteNonQuery();
                }

                DataTable dados2z;
                dados2z = Cl_gestor.EXE_QUERY("select * from vendas ");

                if (dados2z.Columns.Contains("datasincro"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas ADD datasincro DATETIME ";
                    comando.ExecuteNonQuery();
                }

                if (dados2z.Columns.Contains("id_formapgto"))
                {
                }
                else
                { 
                    comando.CommandText = " ALTER TABLE vendas ADD id_formapgto integer  ";
                    comando.ExecuteNonQuery();
                }

                if (dados2z.Columns.Contains("tipopagto"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas ADD tipopagto NVARCHAR(15)  ";
                    comando.ExecuteNonQuery();
                }

                if (dados2z.Columns.Contains("nrovendabco"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas ADD nrovendabco integer ";
                    comando.ExecuteNonQuery();
                }

                if (dados2z.Columns.Contains("checar"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas ADD checar integer";
                    comando.ExecuteNonQuery();
                }

                if (dados2z.Columns.Contains("obsped"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas ADD obsped NVARCHAR(70)  ";
                    comando.ExecuteNonQuery();
                }

                DataTable dados12x;
                dados12x = Cl_gestor.EXE_QUERY("select * from usuario ");

                if (dados12x.Columns.Contains("gerente"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE usuario ADD gerente integer ";
                    comando.ExecuteNonQuery();
                }

                if (dados12x.Columns.Contains("descvendas"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE usuario ADD descvendas DECIMAL(5,2) ";
                    comando.ExecuteNonQuery();
                }

                ////// adiuconando campos em vendas
                ///

                DataTable dados2x1;
                dados2x1 = Cl_gestor.EXE_QUERY("select * from vendas ");

                if (dados2x1.Columns.Contains("datasincro"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas ADD datasincro DATETIME ";
                    comando.ExecuteNonQuery();
                }

                DataTable dados2bkp;
                dados2bkp = Cl_gestor.EXE_QUERY("select * from vendasprd_BKP ");

                if (dados2bkp.Columns.Contains("nrovendabco"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendasprd_BKP ADD nrovendabco integer  ";
                    comando.ExecuteNonQuery();
                }

                if (dados2bkp.Columns.Contains("seq"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendasprd_BKP ADD seq integer ";
                    comando.ExecuteNonQuery();
                }

                DataTable dados2bkp2;
                dados2bkp2 = Cl_gestor.EXE_QUERY("select * from vendas_BKP ");

                if (dados2bkp2.Columns.Contains("datasincro"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas_BKP ADD datasincro DATETIME ";
                    comando.ExecuteNonQuery();
                }

                if (dados2bkp2.Columns.Contains("id_formapgto"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas_BKP ADD id_formapgto integer  ";
                    comando.ExecuteNonQuery();
                }


                if (dados2bkp2.Columns.Contains("tipopagto"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas_BKP ADD tipopagto NVARCHAR(15)  ";
                    comando.ExecuteNonQuery();
                }

                if (dados2bkp2.Columns.Contains("nrovendabco"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas_BKP ADD nrovendabco integer ";
                    comando.ExecuteNonQuery();
                }

                if (dados2bkp2.Columns.Contains("checar"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas_BKP ADD checar integer not null default 0 ";
                    comando.ExecuteNonQuery();
                }

                if (dados2bkp2.Columns.Contains("obsped"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas_BKP ADD obsped NVARCHAR(50) ";
                    comando.ExecuteNonQuery();
                }

                comando.CommandText =
                   "CREATE TABLE  IF NOT EXISTS  sincronizar(" +
                   "id_seq                             integer not null default 0," +
                   "data                               DATETIME)";
                comando.ExecuteNonQuery();


                comando.CommandText =
                   "CREATE TABLE  IF NOT EXISTS nvenda(" +
                   "id_venda                           integer not null default 0, " +
                   "data                               DATETIME)";

                comando.ExecuteNonQuery();

                comando.CommandText =
                  "CREATE TABLE  IF NOT EXISTS nvenda_or(" +
                  "id_venda                           integer not null default 0, " +
                   "data                               DATETIME)";

                comando.ExecuteNonQuery();


                comando.CommandText =
                   "CREATE TABLE  IF NOT EXISTS tipopgto(" +
                   "id_tipopgto                               integer not null, " +
                   "descricao                                 NVARCHAR(50))";
                comando.ExecuteNonQuery();


                comando.CommandText =
                  "CREATE TABLE  IF NOT EXISTS tmptipopgto(" +
                  "id_tipopgto                               integer not null, " +
                  "descricao                                 NVARCHAR(50))";
                comando.ExecuteNonQuery();


                comando.CommandText =
                   "CREATE TABLE  IF NOT EXISTS parametrocli(" +
                   "nome                               NVARCHAR(80), " +
                   "endereco                           NVARCHAR(80), " +
                   "bairro                             NVARCHAR(80), " +
                   "cidade                             NVARCHAR(60), " +
                   "fone                               NVARCHAR(60))";
                comando.ExecuteNonQuery();

                //////////////////////////////////////
                ///


                comando.CommandText =
               "Create Table IF NOT EXISTS tmp_vendas_prod(" +
               "id_produto                               integer," +
               "descricao                                nvarchar(30)," +
               "quantidade                               DECIMAL(10,2)," +
               "preco                                    DECIMAL(10,2)," +
               "desconto                                 DECIMAL(10,2)," +
               "precodesconto                            DECIMAL(10,2)," +
               "total                                    DECIMAL(10,2))";

                comando.ExecuteNonQuery();

                comando.CommandText =
                "CREATE TABLE  IF NOT EXISTS  tmp_vendas_prod_or(" +
                "id_produto                               integer," +
                "descricao                                nvarchar(30)," +
                "quantidade                               DECIMAL(10,2)," +
                "preco                                    DECIMAL(10,2)," +
                "desconto                                 DECIMAL(10,2)," +
                "precodesconto                            DECIMAL(10,2)," +
                "total                                    DECIMAL(10,2))";
                comando.ExecuteNonQuery();


                comando.CommandText =
               "CREATE TABLE  IF NOT EXISTS  vendasprd_or(" +
               "id_pedido                                integer," +
               "id_produto                               integer," +
               "descricao                                nvarchar(30)," +
               "quantidade                               DECIMAL(10,2)," +
               "preco                                    DECIMAL(10,2)," +
               "desconto                                 DECIMAL(10,2)," +
               "precodesconto                            DECIMAL(10,2)," +
               "total                                    DECIMAL(10,2))";
                comando.ExecuteNonQuery();

                comando.CommandText =
                "CREATE TABLE  IF NOT EXISTS  vendas_or(" +
                "id_pedido                                integer," +
                "id_cliente                               integer," +
                "nome                                     nvarchar(30)," +
                "id_pgto                                  integer," +
                "desconto                                 DECIMAL(10,2)," +
                "total                                    DECIMAL(10,2)," +
                "data                                     DATETIME)";
                comando.ExecuteNonQuery();

                comando.CommandText =
                  "CREATE TABLE  IF NOT EXISTS  receber(" +
                  "titulo                                   NVARCHAR(10), " +
                  "emissao                                  DATETIME," +
                  "vencimento                               DATETIME," +
                  "valor                                    DECIMAL(10,2)," +
                  "nome                                     NVARCHAR(20))";
                comando.ExecuteNonQuery();

                comando.CommandText =
                  "Create Table if not exists vendasprd_BKP(" +
                  "id_pedido                                integer," +
                  "id_produto                               integer," +
                  "descricao                                nvarchar(30)," +
                  "quantidade                               DECIMAL(10,2)," +
                  "preco                                    DECIMAL(10,2)," +
                  "desconto                                 DECIMAL(10,2)," +
                  "precodesconto                            DECIMAL(10,2)," +
                  "total                                    DECIMAL(10,2))";
                comando.ExecuteNonQuery();

                comando.CommandText =
                  "Create Table if not exists vendas_BKP(" +
                  "id_pedido                                integer," +
                  "id_cliente                               integer," +
                  "nome                                     nvarchar(30)," +
                  "id_pgto                                  integer," +
                  "desconto                                 DECIMAL(10,2)," +
                  "total                                    DECIMAL(10,2)," +
                  "data                                     DATETIME)";
                comando.ExecuteNonQuery();

                comando.CommandText =
                  "Create Table if not exists rel_vendas(" +
                  "codigo                                   integer," +
                  "id_cliente                               integer," +
                  "descricao                                nvarchar(30)," +
                  "cupom                                    integer, " +
                  "total                                    DECIMAL(10,2)," +
                  "data                                     DATETIME ," +
                  "preco                                    DECIMAL(10,2)," +
                  "quantidade                               DECIMAL(10,2), " +
                  "codform                                  integer) ";
                comando.ExecuteNonQuery();

                DataTable dados21;
                dados21 = Cl_gestor.EXE_QUERY("select * from vendas_or ");

                if (dados21.Columns.Contains("datasincro"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER " +
                        "TABLE vendas_or ADD datasincro DATETIME ";
                    comando.ExecuteNonQuery();
                }

                if (dados21.Columns.Contains("id_formapgto"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas_or ADD id_formapgto integer  ";
                    comando.ExecuteNonQuery();
                }


                if (dados21.Columns.Contains("tipopagto"))
                {
                }
                else
                {
                    comando.CommandText = " ALTER TABLE vendas_or ADD tipopagto NVARCHAR(15)  ";
                    comando.ExecuteNonQuery();
                }


                //////////////////////////////////////////


                ////apagar pedido maior 5 dias
                ///

                DateTime data = DateTime.Now.AddDays(-5);
                DateTime data1 = DateTime.MinValue;

                List<SQLparametro> parametroxc = new List<SQLparametro>();
                parametroxc.Add(new SQLparametro("@datasinc", DateTime.Now.AddDays(-30)));
                parametroxc.Add(new SQLparametro("@datasincx", DateTime.MinValue));

                int cv1 = 0;

                //AddDays(-5);                
                dados1 = Cl_gestor.EXE_QUERY("select * from vendas where datasincro < @datasinc  and  datasincro > @datasincx ", parametroxc);
                int xi = 0, id_pedido;


                foreach (DataRow linha in dados1.Rows)
                {

                    id_pedido = Convert.ToInt32(dados1.Rows[xi]["id_pedido"]);

                    Cl_gestor.NOM_QUERY("Delete from vendas where id_pedido = " + id_pedido);
                    Cl_gestor.NOM_QUERY("Delete from vendasprd where id_pedido = " + id_pedido);
                    xi = xi + 1;

                }

                //List<SQLparametro> parametroxc1 = new List<SQLparametro>();
                //parametroxc1.Add(new SQLparametro("@data1", DateTime.Now.AddDays(-8)));
                //parametroxc1.Add(new SQLparametro("@pedido", 4));
                //Cl_gestor.NOM_QUERY(" update vendas set datasincro = @data1 where id_pedido == @pedido ", parametroxc1);



                //tirar
                // Cl_gestor.NOM_QUERY("delete  from vendas ");

                DataTable dados22;
                dados22 = Cl_gestor.EXE_QUERY("select * from parametro ");

                if (dados22.Columns.Contains("vendasdesc"))
                {

                }
                else
                {
                    comando.CommandText = " ALTER TABLE parametro ADD vendasdesc integer ";
                    comando.ExecuteNonQuery();
                }

                if (dados22.Rows.Count != 0)
                {

                    if (!DBNull.Value.Equals(dados22.Rows[0]["vendasdesc"]))
                    {
                        vars.parvenda = Convert.ToInt32(dados22.Rows[0]["vendasdesc"]);
                    }
                    else
                    {
                        vars.parvenda = 0;
                    }
                }
                // Toast.MakeText(Application.Context, " Selecione uma quantidade ou preço  " + vars.parvenda.ToString(), ToastLength.Long).Show();
            }

        }

        public static void NOM_QUERY(string query, List<SQLparametro> parametros)        
        {
            SqliteConnection ligacao = new SqliteConnection("Data source = " + base_dados);
            ligacao.Open();

            SqliteCommand comando = new SqliteCommand(query, ligacao);

            foreach (SQLparametro parametro in parametros)
            {
                comando.Parameters.Add(new SqliteParameter(parametro.nome, parametro.valor));
            }
            comando.ExecuteNonQuery();

            comando.Dispose();
            ligacao.Clone();
            ligacao.Dispose();
        }

        public static void NOM_QUERY(string query)
        {
            SqliteConnection ligacao = new SqliteConnection("Data source = " + base_dados);
            ligacao.Open();

            SqliteCommand comando = new SqliteCommand(query, ligacao);

            comando.ExecuteNonQuery();

            comando.Dispose();
            ligacao.Clone();    
            ligacao.Dispose();
        }


        public static DataTable EXE_QUERY(string query, List<SQLparametro> parametros)
        {

            SqliteConnection ligacao = new SqliteConnection("Data source = " + base_dados);
            ligacao.Open();

            SqliteDataAdapter adaptador = new SqliteDataAdapter(query, ligacao);


            SqliteCommand comando = new SqliteCommand(query, ligacao);

            foreach (SQLparametro parametro in parametros)
            {

                adaptador.SelectCommand.Parameters.Add(new SqliteParameter(parametro.nome, parametro.valor));
            }

            DataTable dados = new DataTable();
            adaptador.Fill(dados);
            return dados;


        }

        public static DataTable EXE_QUERY(string query)
        {
            
            SqliteConnection ligacao = new SqliteConnection("Data source=" + base_dados);
            ligacao.Open();

            SqliteDataAdapter adaptador = new SqliteDataAdapter(query, ligacao);

            SqliteCommand comando = new SqliteCommand(query, ligacao);

            DataTable dados = new DataTable();
            adaptador.Fill(dados);
            return dados;


        }

        public static int ID_DISPONIVEL(string tabela, string coluna)
        {

            int valor = 0;

            SqliteConnection ligacao = new SqliteConnection("Data source = " + base_dados);
            ligacao.Open();

            string query = "select max(" + coluna + ") as maxid from " + tabela;

            DataTable dados = new DataTable();

            SqliteDataAdapter adaptador = new SqliteDataAdapter(query,ligacao);

            adaptador.Fill(dados);

            if (dados.Rows.Count != 0)
            {

                if (!DBNull.Value.Equals(dados.Rows[0][0]))
                {
                    valor = Convert.ToInt32(dados.Rows[0][0]) + 1;
                }

            }
                       
            ligacao.Close();
            ligacao.Dispose();

          

           
            return valor;
                                 
        }


        public static int ID_ULTIMO(string tabela, string coluna)
        {

            int valor = 0;

            SqliteConnection ligacao = new SqliteConnection("Data source = " + base_dados);
            ligacao.Open();

            string query = "select max(" + coluna + ") as maxid from " + tabela;

            DataTable dados = new DataTable();

            SqliteDataAdapter adaptador = new SqliteDataAdapter(query, ligacao);

            adaptador.Fill(dados);


            if (dados.Rows.Count != 0)
            {

                if (!DBNull.Value.Equals(dados.Rows[0][0]))
                    valor = Convert.ToInt32(dados.Rows[0][0]) ;

            }

            ligacao.Close();
            ligacao.Dispose();

            return valor;

        }




    }
}