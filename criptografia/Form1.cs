using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace criptografia
{
    public partial class Palavras : Form
    {
        Criptografia crip = new Criptografia();
        public Palavras()
        {
            InitializeComponent();
        }

        private void Palavras_Load(object sender, EventArgs e)
        {
            crip.criptografados(datagrid);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
            crip.criptografar(txtPalavre, datagrid);
            } catch(ArgumentNullException){
                MessageBox.Show("Caixa de texto vazia, introduza algum texto para criptografar.", "Campo vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
    }
}
