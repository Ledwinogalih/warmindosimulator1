using System;
using System.Drawing;
using System.Windows.Forms;

namespace WarmindoSimulator
{
    public class Kompor
    {
        public PictureBox Box { get; private set; }
        public bool IsCooking { get; private set; } = false;
        private int cookTime = 0;
        public event Action OnMasakSelesai;

        public Kompor(Point location, Image image)
        {
            Box = new PictureBox
            {
                Size = new Size(100, 100),
                Location = location,
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Box.Click += (s, e) => StartCooking();
        }

        public void StartCooking()
        {
            if (!IsCooking)
            {
                IsCooking = true;
                cookTime = 0;
                MessageBox.Show("Memasak dimulai!");
            }
        }

        public void Update()
        {
            if (IsCooking)
            {
                cookTime++;
                if (cookTime >= 30)
                {
                    IsCooking = false;
                    cookTime = 0;
                    OnMasakSelesai?.Invoke();
                }
            }
        }

        public void Draw(Graphics g)
        {
            if (Box.Image != null)
                g.DrawImage(Box.Image, Box.Location.X, Box.Location.Y, Box.Width, Box.Height);
            else
                g.FillRectangle(Brushes.Gray, Box.Location.X, Box.Location.Y, Box.Width, Box.Height);

            if (IsCooking)
            {
                g.DrawString("Memasak...", new Font("Arial", 14), Brushes.Red, 10, 10);
            }
        }
    }
}