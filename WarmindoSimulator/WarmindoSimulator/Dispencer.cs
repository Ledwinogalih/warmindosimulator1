using System;
using System.Drawing;
using System.Windows.Forms;

namespace WarmindoSimulator
{
    public class Dispenser
    {
        public PictureBox Box { get; private set; }
        public bool IsFilling { get; private set; } = false;
        private int fillTime = 0;
        public event Action OnMinumanSelesai;

        public Dispenser(Point location, Image spriteDispenser)
        {
            Box = new PictureBox
            {
                Size = new Size(80, 100),
                Location = location,
                Image = spriteDispenser,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            Box.Click += (s, e) => StartFilling();
        }

        public void StartFilling()
        {
            if (!IsFilling)
            {
                IsFilling = true;
                fillTime = 0;
                MessageBox.Show("Mengisi minuman...");
            }
        }

        public void Update()
        {
            if (IsFilling)
            {
                fillTime++;
                if (fillTime >= 20)
                {
                    IsFilling = false;
                    fillTime = 0;
                    OnMinumanSelesai?.Invoke();
                }
            }
        }

        public void Draw(Graphics g)
        {
            if (Box.Image != null)
                g.DrawImage(Box.Image, Box.Location.X, Box.Location.Y, Box.Width, Box.Height);
            else
                g.FillRectangle(Brushes.Gray, Box.Location.X, Box.Location.Y, Box.Width, Box.Height);

            if (IsFilling)
            {
                g.DrawString("Mengisi minuman...", new Font("Arial", 10), Brushes.Blue, 10, 40);
            }
        }
    }
}