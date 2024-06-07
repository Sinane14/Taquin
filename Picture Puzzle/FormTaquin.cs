using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Picture_Puzzle
{
    public partial class FormTaquin : Form
    {
        Point EmptyPoint; // Déclare une variable de type Point pour stocker la position vide.
        ArrayList images = new ArrayList(); // Déclare une collection ArrayList pour stocker les images du puzzle.

        public FormTaquin()
        {
            EmptyPoint = new Point(180, 180); // Initialise la position de la case vide
            InitializeComponent();
        }

        private void FormTaquin_Load(object sender, EventArgs e)
        {
            // Méthode vide. Vous pouvez ajouter des initialisations si nécessaire.
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (Button b in panel1.Controls.OfType<Button>())
                b.Enabled = true;

            Image original = Image.FromFile(@"img\img.jpg"); // Charge l'image d'origine à partir d'un fichier.

            cropImageTomages(original, 270, 270); // Découpe l'image en morceaux.

            AddImagesToButtons(images); // Ajoute les morceaux d'image aux boutons.
        }

        private void AddImagesToButtons(ArrayList images)
        {
            int i = 0;
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7 };

            arr = suffle(arr);

            foreach (Button b in panel1.Controls.OfType<Button>())
            {
                if (i < arr.Length)
                {
                    b.Image = (Image)images[arr[i]];
                    i++;
                }
            }
        }

        private int[] suffle(int[] arr)
        {
            Random rand = new Random();
            return arr.OrderBy(x => rand.Next()).ToArray();
        }

        private void cropImageTomages(Image original, int w, int h)
        {
            Bitmap bmp = new Bitmap(w, h);

            using (Graphics graphic = Graphics.FromImage(bmp))
            {
                graphic.DrawImage(original, 0, 0, w, h);
            }

            int movr = 0, movd = 0;

            for (int x = 0; x < 8; x++)
            {
                Bitmap piece = new Bitmap(90, 90);

                for (int i = 0; i < 90; i++)
                    for (int j = 0; j < 90; j++)
                        piece.SetPixel(i, j, bmp.GetPixel(i + movr, j + movd));

                images.Add(piece);

                movr += 90;

                if (movr == 270)
                {
                    movr = 0;
                    movd += 90;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoveButton((Button)sender);
        }

        private void MoveButton(Button btn)
        {
            if (((btn.Location.X == EmptyPoint.X - 90 || btn.Location.X == EmptyPoint.X + 90) && btn.Location.Y == EmptyPoint.Y) ||
                (btn.Location.Y == EmptyPoint.Y - 90 || btn.Location.Y == EmptyPoint.Y + 90) && btn.Location.X == EmptyPoint.X)
            {
                Point swap = btn.Location;
                btn.Location = EmptyPoint;
                EmptyPoint = swap;
            }

            if (EmptyPoint.X == 180 && EmptyPoint.Y == 180)
                CheckValid();
        }

        private void CheckValid()
        {
            int count = 0, index;

            foreach (Button btn in panel1.Controls.OfType<Button>())
            {
                index = (btn.Location.Y / 90) * 3 + btn.Location.X / 90;

                if (images[index] == btn.Image)
                    count++;
            }

            if (count == 8)
                MessageBox.Show("Vous avez gagné !");
        }
    }
}