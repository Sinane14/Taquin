using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BCrypt.Net;

namespace Picture_Puzzle
{
    public partial class FormConnexion : Form
    {
        public FormConnexion()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;database=forum;user=root;password=";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Password FROM User WHERE Nickname=@username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string storedHash = reader.GetString(0);

                        if (BCrypt.Net.BCrypt.Verify(txtPassword.Text, storedHash))
                        {
                            MessageBox.Show("Login successful!");
                            this.Hide();
                            FormTaquin mainForm = new FormTaquin();
                            mainForm.FormClosed += (s, args) => this.Close(); // Fermez le formulaire de connexion lorsque le formulaire principal est fermé
                            mainForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("User not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnid_Click_1(object sender, EventArgs e)
        {
            Connexion1 connexionForm = new Connexion1();
            connexionForm.Show();
        }
    }
}
