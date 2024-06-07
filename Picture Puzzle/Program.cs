using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Picture_Puzzle
{
    static class Program
    {
        /// <summary>
        /// Le point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Utilisez le bon formulaire pour le login
            FormConnexion loginForm = new FormConnexion();

            // Ajoutez un message de débogage pour vérifier que le formulaire de connexion est affiché
            Console.WriteLine("Affichage du formulaire de connexion");

            // Affichez le formulaire de connexion
            DialogResult result = loginForm.ShowDialog();

            // Ajoutez un message de débogage pour vérifier le résultat du formulaire de connexion
            Console.WriteLine($"Résultat du formulaire de connexion : {result}");

            if (result == DialogResult.OK)
            {
                // Si la connexion est réussie, lancez le jeu
                MessageBox.Show("Connexion réussie, lancement du jeu");
                Application.Run(new FormTaquin());
            }
            else
            {
                // Si la connexion échoue, fermez l'application
                MessageBox.Show("Connexion échouée, fermeture de l'application");
                Application.Exit();
            }
        }
    }
}
