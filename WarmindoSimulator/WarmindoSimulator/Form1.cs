using System;
using System.Drawing;
using System.Windows.Forms;

namespace WarmindoSimulator
{
    public partial class Form1 : Form
    {
        private Player player;
        private Kompor kompor;
        private Dispenser dispenser;
        private Order currentOrder;

        private Timer timerGame;
        private int orderTick = 0;

        private bool isMieReady = false;
        private bool isMinumReady = false;

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.Width = 800;
            this.Height = 600;
            this.Text = "Warmindo Simulator 2D";
            this.KeyPreview = true;

            // Sementara belum ada gambar
            Image karakterImg = null;
            Image komporImg = null;
            Image dispenserImg = null;

            // Inisialisasi objek
            player = new Player(100, 100, karakterImg);
            kompor = new Kompor(new Point(300, 200), komporImg);
            dispenser = new Dispenser(new Point(450, 200), dispenserImg);

            kompor.OnMasakSelesai += () =>
            {
                isMieReady = true;
                MessageBox.Show("Mie instan selesai dimasak!");
                CheckOrderComplete();
            };

            dispenser.OnMinumanSelesai += () =>
            {
                isMinumReady = true;
                MessageBox.Show("Minuman selesai dibuat!");
                CheckOrderComplete();
            };

            this.Controls.Add(kompor.Box);
            this.Controls.Add(dispenser.Box);

            GenerateNewOrder();

            timerGame = new Timer { Interval = 100 };
            timerGame.Tick += TimerGame_Tick;
            timerGame.Start();

            this.KeyDown += Form1_KeyDown;
            this.Paint += Form1_Paint;
        }

        private void TimerGame_Tick(object sender, EventArgs e)
        {
            kompor.Update();
            dispenser.Update();

            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            player.Draw(g);
            kompor.Draw(g);
            dispenser.Draw(g);
            DrawOrderPaper(g);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            player.Move(e.KeyCode, 5);
            this.Invalidate();
        }

        private void GenerateNewOrder()
        {
            currentOrder = new Order();
            isMieReady = false;
            isMinumReady = false;
        }

        private void CheckOrderComplete()
        {
            if (isMieReady && isMinumReady)
            {
                MessageBox.Show("Pesanan sudah lengkap, kirim ke customer!");
                GenerateNewOrder();
            }
        }

        private void DrawOrderPaper(Graphics g)
        {
            Rectangle paperRect = new Rectangle(600, 50, 180, 140);
            g.FillRectangle(Brushes.WhiteSmoke, paperRect);
            g.DrawRectangle(Pens.Black, paperRect);

            g.DrawString("Pesanan:", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, paperRect.X + 10, paperRect.Y + 10);
            g.DrawString(currentOrder.GetOrderSummary(), new Font("Arial", 10), Brushes.DarkBlue, paperRect.X + 10, paperRect.Y + 30);

            g.DrawString("Minuman:", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, paperRect.X + 10, paperRect.Y + 85);
            g.DrawString(currentOrder.GetDrinkSummary(), new Font("Arial", 10), Brushes.DarkGreen, paperRect.X + 10, paperRect.Y + 105);
        }
    }
}
