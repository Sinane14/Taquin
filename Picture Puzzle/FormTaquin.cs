using System; // Importe l'espace de noms System

using System.Collections.Generic; // Importe l'espace de noms System.Collections.Generic

using System.Drawing; // Importe l'espace de noms System.Drawing pour manipuler des images

using System.Linq; // Importe l'espace de noms System.Linq pour les opérations LINQ

using System.Text; // Importe l'espace de noms System.Text

using System.Threading.Tasks; // Importe l'espace de noms System.Threading.Tasks

using System.Windows.Forms; // Importe l'espace de noms System.Windows.Forms pour les applications Windows Forms

using System.Collections; // Importe l'espace de noms System.Collections pour les collections non génériques

namespace Picture_Puzzle // Définit un espace de noms appelé Picture_Puzzle
{
    public partial class FormTaquin : Form // Déclare une classe partielle FormTaquin qui hérite de Form
    {
        Point EmptyPoint; // Déclare une variable de type Point pour stocker la position vide.
        ArrayList images = new ArrayList(); // Déclare une collection ArrayList pour stocker les images du puzzle.

        public FormTaquin() // Définit le constructeur de la classe FormTaquin
        {
            EmptyPoint = new Point(180, 180); // Initialise la position de la case vide
            InitializeComponent(); // Initialise le composant du formulaire
        }

        private void FormTaquin_Load(object sender, EventArgs e) // Définit l'événement de chargement du formulaire
        {
            // Méthode vide. Vous pouvez ajouter des initialisations si nécessaire.
        }

        private void button9_Click(object sender, EventArgs e) // Définit l'événement de clic sur le bouton de démarrage du jeu
        {
            foreach (Button b in panel1.Controls.OfType<Button>()) // Parcourt tous les boutons dans le panel1
                b.Enabled = true; // Active tous les boutons

            Image original = Image.FromFile(@"img\img.jpg"); // Charge l'image d'origine à partir d'un fichier.

            cropImageTomages(original, 270, 270); // Découpe l'image en morceaux.

            AddImagesToButtons(images); // Ajoute les morceaux d'image aux boutons.
        }

        private void AddImagesToButtons(ArrayList images) // Définit une méthode pour ajouter des images aux boutons
        {
            int i = 0;
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7 };

            arr = suffle(arr);

            foreach (Button b in panel1.Controls.OfType<Button>()) // Parcourt tous les boutons dans le panel1
            {
                if (i < arr.Length)
                {
                    b.Image = (Image)images[arr[i]]; // Affecte une image à chaque bouton
                    i++;
                }
            }
        }

        private int[] suffle(int[] arr) // Définit une méthode pour mélanger les éléments d'un tableau
        {
            Random rand = new Random(); // Initialise un générateur de nombres aléatoires
            return arr.OrderBy(x => rand.Next()).ToArray(); // Mélange les éléments du tableau et les retourne
        }

        private void cropImageTomages(Image original, int w, int h) // Définit une méthode pour découper une image en morceaux
        {
            Bitmap bmp = new Bitmap(w, h); // Crée un nouveau bitmap avec les dimensions spécifiées

            using (Graphics graphic = Graphics.FromImage(bmp)) // Crée une instance Graphics à partir du bitmap
            {
                graphic.DrawImage(original, 0, 0, w, h); // Dessine l'image d'origine sur le bitmap
            }

            int movr = 0, movd = 0;

            for (int x = 0; x < 8; x++) // Parcourt les morceaux de l'image
            {
                Bitmap piece = new Bitmap(90, 90); // Crée un nouveau bitmap pour chaque morceau

                for (int i = 0; i < 90; i++) // Parcourt les pixels horizontalement
                    for (int j = 0; j < 90; j++) // Parcourt les pixels verticalement
                        piece.SetPixel(i, j, bmp.GetPixel(i + movr, j + movd)); // Copie le pixel de l'image d'origine dans le morceau

                images.Add(piece); // Ajoute le morceau à la collection d'images

                movr += 90;

                if (movr == 270)
                {
                    movr = 0;
                    movd += 90;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) // Définit l'événement de clic sur un bouton du puzzle
        {
            MoveButton((Button)sender); // Appelle la méthode pour déplacer le bouton
        }

        private void MoveButton(Button btn) // Définit une méthode pour déplacer un bouton
        {
            if (((btn.Location.X == EmptyPoint.X - 90 || btn.Location.X == EmptyPoint.X + 90) && btn.Location.Y == EmptyPoint.Y) ||
                (btn.Location.Y == EmptyPoint.Y - 90 || btn.Location.Y == EmptyPoint.Y + 90) && btn.Location.X == EmptyPoint.X)
            {
                Point swap = btn.Location; // Stocke la position du bouton dans une variable temporaire
                btn.Location = EmptyPoint; // Déplace le bouton à la position vide
                EmptyPoint = swap; // Met à jour la position vide avec la position précédente du bouton
            }

            if (EmptyPoint.X == 180 && EmptyPoint.Y == 180) // Vérifie si le puzzle est résolu
                CheckValid(); // Vérifie si le puzzle est valide
        }

        private void CheckValid() // Définit une méthode pour vérifier si le puzzle est résolu
        {
            int count = 0, index;

            foreach (Button btn in panel1.Controls.OfType<Button>()) // Parcourt tous les boutons dans le panel1
            {
                index = (btn.Location.Y / 90) * 3 + btn.Location.X / 90; // Calcule l'index de l'image correspondante

                if (images[index] == btn.Image) // Vérifie si l'image du bouton correspond à l'image attendue
                    count++; // Incrémente le compteur
            }

            if (count == 8) // Vérifie si toutes les images sont à leur place
                MessageBox.Show("Vous avez gagné !"); // Affiche un message de victoire
        }
    }
}
