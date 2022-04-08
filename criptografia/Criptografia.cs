using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Bunifu.Framework.UI;

namespace criptografia
{
    class Criptografia
    {
       private MySqlConnection conecter = new MySqlConnection("server=localhost;user id=root;database=soma");
        private MySqlCommand comand;
        private MySqlDataAdapter adapter;
       
       public void criptografados(BunifuCustomDataGrid dt)
        {
            adapter = new MySqlDataAdapter("SELECT id, criptografada FROM frases", conecter);
            DataTable table = new DataTable();
            table.Clear();
            adapter.Fill(table);
            dt.DataSource = table;           
        }
        public void criptografar(BunifuMaterialTextbox txt, BunifuCustomDataGrid dt)
        {
            if (txt.Text.Equals(""))
            {
                throw new ArgumentNullException();
            }
            string texto = txt.Text;
            string criptograf = "";
            char[] sinais = { '.', ',', '!', '?', '+', '-', '*', '/', '"' };
            int X, i, indice;

            string[] txtSeparado = texto.Split(' ');

            for (X = 0; X < txtSeparado.Length; X++)
            {
                for (i = 0; i < sinais.Length; i++)
                {
                    indice = txtSeparado[X].IndexOf(sinais[i]);
                    if (indice != -1)
                    {
                        txtSeparado[X] = txtSeparado[X].Remove(indice, 1);
                    }

                }
            }

            for (X = 0; X < txtSeparado.Length; X++)
            {
                int txtMet = ((txtSeparado[X].Length / 2) + (txtSeparado[X].Length % 2));
                string setes = txtSeparado[X];
                if (setes.Length % 2 != 0)
                {
                    txtMet--;
                }

                for (i = txtMet; i < txtSeparado[X].Length; i++)
                {
                    setes = setes.Remove(i, 1);
                    setes = setes.Insert(i, "?");
                }

                txtSeparado[X] = setes;
                criptograf += txtSeparado[X];
                if (X < txtSeparado.Length - 1) criptograf += " ";
            }
            this.conecter.Open();
           this.comand = new MySqlCommand("INSERT INTO FRASES(frase, criptografada) values(@frase,@cripto)",conecter);
            this.comand.Parameters.AddWithValue("@frase", txt);
            this.comand.Parameters.AddWithValue("@cripto",criptograf);
            this.comand.ExecuteNonQuery();
            this.conecter.Close();
            this.criptografados(dt);
        }
       

    }
}
