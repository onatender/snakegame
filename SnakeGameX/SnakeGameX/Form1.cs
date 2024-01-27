using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGameX
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Panel[]> Pixels;
        List<Coordinate> SnakePixels;
        char Direction = 'X';
        Color snakeColor = Color.FromArgb(255, 0, 255, 0);
        Color appleColor = Color.FromArgb(255, 255, 0, 0);
        Color bgColor = Color.Black;

        void SetColor(Coordinate cd, Color clr)
        {
            Pixels[cd.x][cd.y].BackColor = clr;
        }

        void DrawAllSnakePixels()
        {
            foreach (Coordinate coordinate in SnakePixels)
            {
                SetColor(coordinate, snakeColor);
            }
        }

        void ClearAllSnakePixels(List<Coordinate> lst)
        {
            foreach (Coordinate coordinate in lst)
            {
                SetColor(coordinate, bgColor);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Focus();
            SnakePixels = new List<Coordinate>();
            Pixels = new List<Panel[]>();
            int xpay = 40;
            int ypay = 40;
            int pixel_shape_x = 20;
            int pixel_shape_y = 20;
            for (int x = 0; x < 20; x++)
            {
                Pixels.Add(new Panel[20]);
                for (int y = 0; y < 20; y++)
                {
                    Pixels[x][y] = new Panel();
                    Pixels[x][y].Size = new Size(pixel_shape_x, pixel_shape_y);
                    Pixels[x][y].BorderStyle = BorderStyle.FixedSingle;
                    Pixels[x][y].Location = new Point(y*pixel_shape_y+ypay, x*pixel_shape_x+xpay);
                    Pixels[x][y].BackColor = bgColor;
                    this.Controls.Add(Pixels[x][y]);
                }
            }

            SnakePixels.Add(new Coordinate(10, 10));
            DrawAllSnakePixels();
            timer1.Start();
            RandomAppleSpawn();
        }
        int skor = 0;
        bool isAppleEaten = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if ("UDRL".IndexOf(Direction) == -1) return;
            int formerX = SnakePixels[0].x;
            int formerY = SnakePixels[0].y;
            List<Coordinate> backup = new List<Coordinate>();
            foreach (Coordinate cd in SnakePixels)
            {
                backup.Add(new Coordinate(cd.x, cd.y));
            }
            switch (Direction)
            {
                case 'U':
                    SnakePixels[0].x--;
                    break;
                case 'D':
                    SnakePixels[0].x++;
                    break;
                case 'R':
                    SnakePixels[0].y++;
                    break;
                case 'L':
                    SnakePixels[0].y--;
                    break;
            }
            if (SnakePixels[0].x > 19 || SnakePixels[0].x < 0 || SnakePixels[0].y < 0 || SnakePixels[0].y > 19)
            {
                GameOver();
                return;
            }
            isAppleEaten = false;
            if (Pixels[SnakePixels[0].x][SnakePixels[0].y].BackColor == appleColor)
            {
                isAppleEaten = true;
            }
            else if (Pixels[SnakePixels[0].x][SnakePixels[0].y].BackColor == snakeColor)
            {
                if (SnakePixels[0].x == SnakePixels[1].x && SnakePixels[0].y == SnakePixels[1].y)
                {
                    Direction = formerDirection;
                    SnakePixels[0].x = backup[0].x;
                    SnakePixels[0].y = backup[0].y;
                    timer1_Tick(sender,e);
                    return;
                }
                else
                    GameOver();
            }
            if (!isAppleEaten) BodyMove(formerX, formerY);
            else
            {
                SnakePixels.Insert(1, new Coordinate(formerX, formerY));
            }
            ClearAllSnakePixels(backup);
            DrawAllSnakePixels();
            if (isAppleEaten)
            {
                skor++;
                label1.Text = $"Skor:{skor}";
                RandomAppleSpawn();
            }

        }
        void GameOver()
        {
            Direction = 'X';
            timer1.Stop();
            MessageBox.Show("ÖLDÜN! Skor:"+skor);
            NewGame();
        }
        void NewGame()
        {
            skor = 0; label1.Text = $"Skor:{skor}";
            foreach (Panel[] Pixelarr in Pixels)
            {
                foreach (var Pixel in Pixelarr)
                    Pixel.BackColor = bgColor;
            }
            SnakePixels = new List<Coordinate>();
            SnakePixels.Add(new Coordinate(10, 10));
            DrawAllSnakePixels();
            timer1.Start();
            RandomAppleSpawn();
        }
        void BodyMove(int headx, int heady)
        {
            for (int i = SnakePixels.Count-1; i > 0; i--)
            {
                if (i == 1)
                {
                    SnakePixels[i].x = headx;
                    SnakePixels[i].y = heady;
                    continue;
                }
                SnakePixels[i].x = SnakePixels[i-1].x;
                SnakePixels[i].y = SnakePixels[i-1].y;
            }
        }
        // elma yenildiyse hareket yönünde yine hareket et ama sadece baştaki eleman hareket etsin yeni eleman eklenerek 00XX00 -> 00XXX0
        static Random rnd = new Random();
        void RandomAppleSpawn()
        {
            int rndx = rnd.Next(20);
            int rndy = rnd.Next(20);

            if (Pixels[rndx][rndy].BackColor == bgColor)
                Pixels[rndx][rndy].BackColor = appleColor;
            else RandomAppleSpawn();
        }
        char formerDirection = 'X';
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            char fd = Direction;
            switch (e.KeyCode)
            {
                case Keys.Down:
                    Direction = 'D';
                    break;
                case Keys.Up:
                    Direction = 'U';
                    break;
                case Keys.Left:
                    Direction = 'L';
                    break;
                case Keys.Right:
                    Direction = 'R';
                    break;
            }
            if (Direction != formerDirection) formerDirection =fd;
            if (fd == 'X') fd = Direction;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x;
        public int y;
    }
}
