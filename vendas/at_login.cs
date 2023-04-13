using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace vendas
{
   // [Activity(Label = "at_login")]
    [Activity(Label = "Vendas TOL", MainLauncher = true, Icon = "@drawable/Icon", Theme = "@android:style/Theme.DeviceDefault.NoActionBar.Fullscreen")]

   
    public class at_login : Activity
    {

        public static string recebeUsuario;

        private async System.Threading.Tasks.Task CheckAppPermissionsAsync()
        {
            if ((int)Build.VERSION.SdkInt < 19)
            {
                Toast.MakeText(this, "Versão do Android não suportada", ToastLength.Short).Show();
                return;
            }
            else
            {

                if (PackageManager.CheckPermission(Manifest.Permission.ReadExternalStorage, PackageName) == Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.WriteExternalStorage, PackageName) == Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.AccessCoarseLocation, PackageName) == Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.AccessFineLocation, PackageName) == Permission.Granted)
                   
                {
                    await Cl_gestor.InicioAplicacaosync();
                   // inicio();
                }
                else
                {
                    var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage, Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation };
                    RequestPermissions(permissions, 77);

                    if (PackageManager.CheckPermission(Manifest.Permission.ReadExternalStorage, PackageName) == Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.WriteExternalStorage, PackageName) == Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.AccessCoarseLocation, PackageName) == Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.AccessFineLocation, PackageName) == Permission.Granted)
                    {
                      await  Cl_gestor.InicioAplicacaosync();
                       // inicio();

                    }
                    else
                    {
                        this.Recreate();                                          
                    }                   

                }
            }
        }

        Button btn_ok;
        Button btn_cancelar;
        EditText usuario;
        EditText senha;
        RadioButton web;
        RadioButton local;

        string nomez ="";
        string senhaz = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {

            CheckAppPermissionsAsync();

            base.OnCreate(savedInstanceState);
             //Cl_gestor.InicioAplicacao();

            SetContentView(Resource.Layout.layout_login);
            btn_ok = FindViewById<Button>(Resource.Id.btn_ok);
            btn_cancelar = FindViewById<Button>(Resource.Id.btn_cancelar);
            usuario = FindViewById<EditText>(Resource.Id.usuario);
            senha = FindViewById<EditText>(Resource.Id.senha);
            web = FindViewById<RadioButton>(Resource.Id.web);
            local = FindViewById<RadioButton>(Resource.Id.local);


            btn_ok.Click += Btn_ok_Click;
            btn_cancelar.Click += Btn_cancelar_Click;
            web.Click += Web_Click;
            local.Click += Local_Click;
            // Create your application here

            // inicio();


           // web.Checked = false;
            //local.Checked = false;

           

        }

        private void inicio()
        {

                    
            int logx = 0;
            DataTable dadoscone;

            dadoscone = Cl_gestor.EXE_QUERY("select * from parametro");

            logx = Convert.ToInt32(dadoscone.Rows[0]["log"]);

            if (logx == 1)
            {
                web.Checked = true;
                local.Checked = false;
            }
            else
            {
                web.Checked = false;
                local.Checked = true;
            }
        }

        private void Local_Click(object sender, EventArgs e)
        {
            web.Checked = false;
            vars.web_local = true;
        }

        private void Web_Click(object sender, EventArgs e)
        {
            local.Checked = false;
            vars.web_local = false;
        }




        private void Btn_cancelar_Click(object sender, EventArgs e)
        { 
            this.Finish();
        }

        private void Btn_ok_Click(object sender, EventArgs e)
        {
            //    if (CrossConnectivity.Current.IsConnected == true)
            //    {
            //        Toast.MakeText(this, "Conectado", ToastLength.Short).Show();
            //    }
            //    else
            //    {
            //        Toast.MakeText(this, "Disconectado", ToastLength.Short).Show();
            //    }
            // Cl_gestor.NOM_QUERY("delete from parametrocli");
            //preenche a tabela com os dadso da empresa
            Cl_gestor.EXE_QUERY("delete from parametrocli");
                        
            DataTable dadosempx;

            dadosempx = Cl_gestor.EXE_QUERY("select * from parametrocli ");

            if (dadosempx.Rows.Count == 0)
            {

                cl_webservice.pesquisadados();

            }

            //***************** carregando dados empresa

            // nomeemp, enderecoemp, bairroemp, cidadeemp, foneemp;
            DataTable dadosemp;

            dadosemp = Cl_gestor.EXE_QUERY("select * from parametrocli ");

            if (dadosemp.Rows.Count == 0)
            {
                vars.nomeemp = "";
                vars.enderecoemp = "";
                vars.bairroemp = "";
                vars.cidadeemp = "";
                vars.foneemp = "";
            }
            else
            {
                vars.nomeemp = dadosemp.Rows[0]["nome"].ToString().Trim();
                vars.enderecoemp = dadosemp.Rows[0]["endereco"].ToString().Trim();
                vars.bairroemp = dadosemp.Rows[0]["bairro"].ToString().Trim();
                vars.cidadeemp = dadosemp.Rows[0]["cidade"].ToString().Trim();
                vars.foneemp = dadosemp.Rows[0]["fone"].ToString().Trim();

            }
            Toast.MakeText(this, vars.nomeemp, ToastLength.Short).Show();


            DataTable dados;

            dados = Cl_gestor.EXE_QUERY("select * from usuario ");


            if (dados.Rows.Count == 0)
            {
                cl_webservice.insereusu();
                Toast.MakeText(this, "Inserindo pela primeira vez os usuarios ", ToastLength.Short).Show();


            }

            nomez = usuario.Text.ToString().Trim();
            senhaz = senha.Text.ToString().Trim();

            DataTable dados1 = Cl_gestor.EXE_QUERY("select * from parametro ");

            // if (dados1.Rows[0]["login"].ToString() == "0")
            //if (dados1.Rows.Count == 0)
            if (nomez == "toll")
            {





                List<SQLparametro> parametro1 = new List<SQLparametro>();
            parametro1.Add(new SQLparametro("@nome1", nomez));
            //parametro1.Add(new SQLparametro("@senhaz", senhaz));

         

                dados = Cl_gestor.EXE_QUERY("select * from usuario ");


                if ((nomez == "toll") && (senhaz == "toll"))
                {

                    StartActivity(typeof(at_servidor));
                    nomez = "";
                }

                if (dados.Rows.Count > 0)
                {
                    try
                    {

                        int xc = Convert.ToInt32(dados.Rows.Count) - 1;

                        for (int j = 0; j <= xc; j++)
                        {


                            string nome = dados.Rows[j]["nome"].ToString().Trim().ToLower();
                            string senha = dados.Rows[j]["senha"].ToString().Trim().ToLower();
                          
                            int df = 0;


                            // Toast.MakeText(this, nome, ToastLength.Short).Show();

                            if ((nome == nomez.Trim()) && (senha == senhaz.Trim()))
                            {

                                DataTable dadosinicio;
                                dadosinicio = Cl_gestor.EXE_QUERY("select * from usuario where nome =="+ nomez.Trim());
                                vars.descontovendedor = Convert.ToDecimal(dadosinicio.Rows[0]["descvendas"]);
                               
                                int dfk = 0;

                                DataTable dadosvend;
                                dadosvend = Cl_gestor.EXE_QUERY("select * from parametro ");

                                vars.numerovendedor = Convert.ToInt32(dadosvend.Rows[0]["vendedor"]);
                                StartActivity(typeof(at_menu_inicial));
                                this.Finish();

                            }




                        }


                    }
                    catch (Exception)
                    {

                        //  Toast.MakeText(this, "Senha errada ", ToastLength.Long).Show();
                    }

                }



           

            }
            else
            {
               
                // Cl_gestor.NOM_QUERY("update parametro set log = @log1 ",par);

               // Cl_gestor.NOM_QUERY("update parametro set log = "+web1 );


                DataTable dadosvendx;
                dadosvendx = Cl_gestor.EXE_QUERY("select * from parametro ");

                vars.numerovendedor = Convert.ToInt32(dadosvendx.Rows[0]["vendedor"]);

                recebeUsuario = Convert.ToString(vars.parusuario);
                recebeUsuario = dadosvendx.Rows[0]["usuario"].ToString();

                DataTable dadosinicio;
                dadosinicio = Cl_gestor.EXE_QUERY("select * from usuario where id_usuario ==" + vars.numerovendedor);
                //vars.descontovendedor = Convert.ToDecimal(dadosinicio.Rows[0]["descvendas"]);

                decimal xxt = 0;
                xxt = 99;
                vars.descontovendedor = xxt;
                DataTable dadosy;
                dadosy = Cl_gestor.EXE_QUERY("select * from  usuario ");

                if (dadosy.Columns.Contains("gerente"))
                {
                    DataTable gere;
                    gere = Cl_gestor.EXE_QUERY("select * from usuario where id_usuario ==" + vars.numerovendedor);
                    if (gere.Rows.Count > 0 && gere.Rows[0]["gerente"].ToString() != "" )
                    {
                        vars.gerentesn = Convert.ToInt32(gere.Rows[0]["gerente"]);
                    }
                    else
                    {
                        vars.gerentesn = 0;
                    }
                }
                else
                {
                    vars.gerentesn = 0; 
                }


              
                


                int ddd = 1;
                // vars.numerovendedor = 4;
                StartActivity(typeof(at_menu_inicial));
                this.Finish();


            }
                
        }
    }
}