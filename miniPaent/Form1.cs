using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace miniPaent
{
    public delegate void DrawHandler(Point point);

    public partial class Form1 : Form
    {
        Graphics gr;

        DrawHandler FigureDel;

        // переменная, которая равна true, когда у нас появляется разрешение на рисование фигуры при движении мышью
        bool canDraw = false;

        Pen userPen = new Pen(Color.Black, 5);

        List<Figura> history = new List<Figura>(); // сюда будем закидывать фигуры

        // настройка, позволяющая newton json работать со всеми типами данных, при инициализации формы записываем туда эти настройки
        JsonSerializerSettings settings;

        public Form1()
        {
            InitializeComponent();

            FigureDel = DrawBrush;

            settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

        }
        private void Mouse_down(object sender, MouseEventArgs e)
        {
            canDraw = true;
            FigureDel(e.Location);
        }
        private void Mouse_move(object sender, MouseEventArgs e)
        {
            if (canDraw)
            {
                history[history.Count - 1].PointAction(e.Location);
                Refresh();
            }
        }
        private void Mouse_up(object sender, MouseEventArgs e)
        {
            canDraw = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            userPen.Width = trackBar1.Value;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            history.Clear();
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            gr = e.Graphics;
            DrawAll();
        }

        private void DrawAll()
        {
            foreach (Figura f in history) 
                f.Draw(gr);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (history.Count > 0)
            {
                history.RemoveAt(history.Count - 1);
                Refresh();
            }
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                userPen.Color = colorDialog1.Color;
                buttonColor.BackColor = colorDialog1.Color;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {            
            string json, path;
            DialogResult result = saveFileDialog1.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
                json = JsonConvert.SerializeObject(history, settings);
                File.WriteAllText(path, json);
                Text = Path.GetFileNameWithoutExtension(path);
            }
        }
        private void loadButton_Click(object sender, EventArgs e)
        {
            string json, path;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                json = File.ReadAllText(path);

                try
                {
                    Text = Path.GetFileNameWithoutExtension(path);
                    history = JsonConvert.DeserializeObject<List<Figura>>(json, settings);
                    Refresh();
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить файл", "Ошибка");
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show(
                "Сохранить?",
                "Изменения не сохранены",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);

            if (res == DialogResult.Yes) saveButton_Click(null, null);
            if (res == DialogResult.Cancel) e.Cancel = true;
        }

        private void DrawBrush(Point point)
        {
            Stack<Point> points = new Stack<Point>();
            points.Push(point);
            Figura fig = new FigBrush(userPen.Width, userPen.Color, points);
            history.Add(fig);
        }

        private void DrawRect(Point point)
        {
            Figura fig = new FigRect(userPen.Width, userPen.Color, point, point);
            history.Add(fig);
        }

        private void DrawCircle(Point point)
        {
            Figura fig = new FigCircle(userPen.Width, userPen.Color, point, point);
            history.Add(fig);
        }

        private void DrawLine(Point point)
        {
            Figura fig = new FigLine(userPen.Width, userPen.Color, point, point);
            history.Add(fig);
        }

        private void chooseBrush(object sender, EventArgs e)
        {
            FigureDel = DrawBrush;
        }

        private void chooseLine(object sender, EventArgs e)
        {
            FigureDel = DrawLine;
        }

        private void chooseRect(object sender, EventArgs e)
        {
            FigureDel = DrawRect;
        }

        private void chooseCircle(object sender, EventArgs e)
        {
            FigureDel = DrawCircle;
        }
    }
}
