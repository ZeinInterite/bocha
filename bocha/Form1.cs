using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bocha
{
    public partial class Form1 : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Form1()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);

            // Вычисляем размер и положение формы, чтобы она полностью соответствовала звезде
            int n = 5;               // число вершин
            double R = 23, r = 60;   // радиусы
            double alpha = 45.0;        // поворот

            // Вычисляем диаметр звезды
            double diameter = 2 * r;
            int formSize = (int)Math.Ceiling(diameter);

            // Вычисляем положение центра формы
            int x = this.ClientSize.Width / 2 - formSize / 2;
            int y = this.ClientSize.Height / 2 - formSize / 2;

            // Устанавливаем размер и положение формы
            this.ClientSize = new Size(formSize, formSize);
            this.Location = new Point(x, y);

            this.Text = "Фиолетовый круг";

            SetFormToStar();
            this.Paint += new PaintEventHandler(MainForm_Paint);
        }

        private void SetFormToStar()
        {
            int n = 5;               // число вершин
            double R = 23, r = 60;   // радиусы
            double alpha = 45.0;        // поворот

            // Вычисляем координаты вершин звезды
            PointF[] points = new PointF[2 * n + 1];

            double a = alpha, da = Math.PI / n, l;
            for (int k = 0; k < 2 * n + 1; k++)
            {
                l = k % 2 == 0 ? r : R;
                // Вычисляем координаты вершин относительно центра формы
                points[k] = new PointF((float)(this.ClientSize.Width / 2 + l * Math.Cos(a)+8), (float)(this.ClientSize.Height / 2 + l * Math.Sin(a)+30));
                a += da;
            }

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(points);

            // Устанавливаем форму формы
            this.Region = new Region(path);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int n = 5;               // число вершин
            double R = 23, r = 60;   // радиусы
            double alpha = 45.0;        // поворот

            // Вычисляем координаты вершин звезды
            PointF[] points = new PointF[2 * n + 1];

            double a = alpha, da = Math.PI / n, l;
            for (int k = 0; k < 2 * n + 1; k++)
            {
                l = k % 2 == 0 ? r : R;
                // Вычисляем координаты вершин относительно центра формы
                points[k] = new PointF((float)(this.ClientSize.Width / 2 + l * Math.Cos(a)), (float)(this.ClientSize.Height / 2 + l * Math.Sin(a)));
                a += da;
            }

            // Заполняем звезду фиолетовым цветом
            g.FillPolygon(new SolidBrush(Color.Violet), points);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
