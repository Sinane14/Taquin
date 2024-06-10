using System; // Importe l'espace de noms System

using System.Windows.Forms; // Importe l'espace de noms Windows Forms

using MySql.Data.MySqlClient; // Importe l'espace de noms MySQLClient pour la connexion à MySQL

using BCrypt.Net; // Importe l'espace de noms BCrypt.Net pour le hachage de mot de passe

namespace Picture_Puzzle // Définit un espace de noms appelé Picture_Puzzle
{
    public partial class FormConnexion : Form // Déclare une classe partielle FormConnexion qui hérite de Form
    {
        public FormConnexion() // Définit le constructeur de la classe FormConnexion
        {
            InitializeComponent(); // Initialise le composant du formulaire
        }

        private void btnLogin_Click(object sender, EventArgs e) // Définit l'événement de clic sur le bouton de connexion
        {
            string connectionString = "server=localhost;database=forum;user=root;password="; // Définit la chaîne de connexion MySQL

            using (MySqlConnection conn = new MySqlConnection(connectionString)) // Crée une connexion MySQL avec la chaîne de connexion spécifiée
            {
                try // Démarre un bloc try-catch pour gérer les exceptions
                {
                    conn.Open(); // Ouvre la connexion à la base de données MySQL

                    string query = "SELECT Password FROM User WHERE Nickname=@username"; // Définit la requête SQL pour récupérer le mot de passe de l'utilisateur

                    MySqlCommand cmd = new MySqlCommand(query, conn); // Crée une nouvelle commande MySQL

                    cmd.Parameters.AddWithValue("@username", txtUsername.Text); // Ajoute un paramètre à la commande pour le nom d'utilisateur

                    MySqlDataReader reader = cmd.ExecuteReader(); // Exécute la commande et lit les données résultantes

                    if (reader.Read()) // Vérifie s'il y a des données lues
                    {
                        string storedHash = reader.GetString(0); // Récupère le hachage du mot de passe stocké dans la base de données

                        if (BCrypt.Net.BCrypt.Verify(txtPassword.Text, storedHash)) // Vérifie si le mot de passe saisi correspond au hachage stocké
                        {
                            MessageBox.Show("Login successful!"); // Affiche un message de succès
                            this.Hide(); // Cache le formulaire de connexion actuel
                            FormTaquin mainForm = new FormTaquin(); // Crée une nouvelle instance du formulaire principal du jeu
                            mainForm.FormClosed += (s, args) => this.Close(); // Ferme le formulaire de connexion lorsque le formulaire principal est fermé
                            mainForm.Show(); // Affiche le formulaire principal du jeu
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password."); // Affiche un message d'erreur si le nom d'utilisateur ou le mot de passe est incorrect
                        }
                    }
                    else
                    {
                        MessageBox.Show("User not found."); // Affiche un message d'erreur si l'utilisateur n'est pas trouvé dans la base de données
                    }
                }
                catch (Exception ex) // Attrape toute exception
                {
                    MessageBox.Show("Error: " + ex.Message); // Affiche un message d'erreur avec le détail de l'exception
                }
            }
        }

        private void btnid_Click_1(object sender, EventArgs e) // Définit l'événement de clic sur le bouton d'inscription
        {
            Connexion1 connexionForm = new Connexion1(); // Crée une nouvelle instance du formulaire d'inscription
            connexionForm.Show(); // Affiche le formulaire d'inscription
        }
    }
}
