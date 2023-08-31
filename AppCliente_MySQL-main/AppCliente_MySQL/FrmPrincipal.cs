using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppCliente_MySQL
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            List<Cliente> clientes = cliente.listacliente();
            dgvCliente.DataSource = clientes;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                if (cliente.RegistroRepetido(txtNome.Text, textBox3.Text) == true)
                {
                    MessageBox.Show("Cliente já existe em nossa base de dados!", "Cliente Repetido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNome.Text = "";
                    textBox3.Text = "";
                    return;
                }
                else
                {
                    cliente.Inserir(txtNome.Text, textBox3.Text);
                    MessageBox.Show("Cliente inserido com sucesso!", "Cliente Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<Cliente> clientes = cliente.listacliente();
                    dgvCliente.DataSource = clientes;
                    txtNome.Text = "";
                    textBox3.Text = "";
                    this.txtNome.Focus();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtID.Text.Trim()); //Trim: função que retira os espaços 
                Cliente cliente = new Cliente();
                cliente.Localizar(id);
                txtNome.Text = cliente.nome;
                textBox3.Text = cliente.celular;

            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtID.Text.Trim());
                Cliente cliente = new Cliente();
                cliente.Atualizar(id, txtNome.Text, textBox3.Text);
                MessageBox.Show("Cliente atualizado com sucesso!", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Cliente> clientes = cliente.listacliente();
                dgvCliente.DataSource = clientes;
                txtNome.Text = "";
                textBox3.Text = "";
                txtID.Text = "";
                this.txtNome.Focus();

            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtID.Text.Trim());
                Cliente cliente = new Cliente();
                cliente.Excluir(id);
                MessageBox.Show("Cleinte excluido com sucesso!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Cliente> clientes = cliente.listacliente();
                dgvCliente.DataSource = clientes;
                txtNome.Text = "";
                textBox3.Text = "";
                txtID.Text = "";
                this.txtNome.Focus();
            }
            catch (Exception er)
            {
                 MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) //Se a linha que eu clicar, for >0
            {
                DataGridViewRow row = this.dgvCliente.Rows[e.RowIndex];
                this.dgvCliente.Rows[e.RowIndex].Selected = true; //Qualquer coluna clicada, vai selecionar a linha toda
                txtID.Text = row.Cells[0].Value.ToString(); //ID é a coluna 0
                txtNome.Text = row.Cells[1].Value.ToString();  
                textBox3.Text = row.Cells[2].Value.ToString();  
            }
        }
    }
}
