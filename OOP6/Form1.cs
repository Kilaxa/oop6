using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static OOP6.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace OOP6
{
    public partial class Form1 : Form
    {
        private List<CFigure> figures = new List<CFigure>(); // Лист для хранения всех фигур
        public int objectSize = 10;
        public bool Cntrl;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Paint(object sender, PaintEventArgs e) // Метод отрисовки кругов
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Сглаживание
            foreach (CFigure figure in figures)
            {
                figure.SelfDraw(e.Graphics); // Метод круга для отрисовки самого себя
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Cntrl)
            {
                foreach (CFigure figure in figures) // снятие выделения со всех объектов
                {
                    figure.setCondition(false);
                }

                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        CCircle newcircle = new CCircle(e.X, e.Y, objectSize);
                        newcircle.setCondition(true);
                        figures.Add(newcircle);
                        break;
                    case 1:
                        CSquare newsquare = new CSquare(e.X, e.Y, objectSize);
                        newsquare.setCondition(true);
                        figures.Add((newsquare));
                        break;
                    case 2:
                        CTriangle newtriangle = new CTriangle(e.X, e.Y, objectSize);
                        newtriangle.setCondition(true);
                        figures.Add((newtriangle));
                        break;
                    case 3:
                        CSection newsection = new CSection(e.X, e.Y, objectSize);
                        newsection.setCondition(true);
                        figures.Add((newsection));
                        break;
                }
                Refresh();
            }
            else if (Cntrl) // Выделение кругов, если зажат cntrl
            {
                foreach (CFigure figure in figures)
                {
                    if (figure.MouseCheck(e))
                    {
                        figure.setCondition(true);
                        break;
                    }
                }
                Refresh();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            objectSize = trackBar1.Value;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) // Отжатие кнопки
        {
            checkBox1.Checked = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) // Нажатие кнопок delete и cntrl
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                checkBox1.Checked = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Cntrl = checkBox1.Checked;
            foreach (CFigure figure in figures)
            {
                figure.fcntrl = Cntrl;
            }
        }
    }
}

public class CFigure
{
    public Point coords;
    public int rad;
    public bool selected = false;
    public bool fcntrl = false;

    public Color colorT = Color.CornflowerBlue;
    public Color colorF = Color.Purple;

    public void setCondition(bool cond) // метод переключения выделения
    {
        selected = cond;
    }
    public virtual void SelfDraw(Graphics g) // Метод для отрисовки самого себя
    {

    }
    public virtual bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
    {
        return false;
    }

}
public class CCircle : CFigure// класс круга
{
    public CCircle(int x, int y, int radius) // конструктор по умолчанию
    {
        coords.X = x;
        coords.Y = y;
        rad = radius;
    }
    public override void SelfDraw(Graphics g) // Метод для отрисовки самого себя
    {
        if (selected == true)
            g.DrawEllipse(new Pen(colorT, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);
        else
            g.DrawEllipse(new Pen(colorF, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);
    }
    public override bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
    {
        if (fcntrl)
        {
            if (Math.Pow(e.X - coords.X, 2) + Math.Pow(e.Y - coords.Y, 2) <= Math.Pow(rad, 2) && !selected)
            {
                selected = true;
                return true;
            }
        }
        return false;
    }

}

public class CSquare : CFigure // класс квадрата
{
    public CSquare(int x, int y, int radius) // конструктор по умолчанию
    {
        coords.X = x;
        coords.Y = y;
        rad = radius;
    }
    public override void SelfDraw(Graphics g) // Метод для отрисовки самого себя
    {
        if (selected == true)
            g.DrawRectangle(new Pen(colorT, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);
        else
            g.DrawRectangle(new Pen(colorF, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);

    }
    public override bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
    {
        if (fcntrl)
        {
            if (Math.Pow(e.X - coords.X, 2) + Math.Pow(e.Y - coords.Y, 2) <= Math.Pow(rad, 2) && !selected)
            {
                selected = true;
                return true;
            }
        }
        return false;
    }
}

public class CTriangle : CFigure // класс треугольника
{
    public CTriangle(int x, int y, int radius) // конструктор по умолчанию
    {
        coords.X = x;
        coords.Y = y;
        rad = radius;
    }
    public override void SelfDraw(Graphics g) // Метод для отрисовки самого себя
    {
        Point point1 = new Point(coords.X, coords.Y - rad);
        Point point2 = new Point(coords.X + rad, coords.Y + rad);
        Point point3 = new Point(coords.X - rad, coords.Y + rad);
        Point[] curvePoints = { point1, point2, point3 };

        if (selected == true)
            g.DrawPolygon(new Pen(colorT, 3), curvePoints);
        else
            g.DrawPolygon(new Pen(colorF, 3), curvePoints);
    }
    public override bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
    {
        if (fcntrl)
        {
            if (Math.Pow(e.X - coords.X, 2) + Math.Pow(e.Y - coords.Y, 2) <= Math.Pow(rad, 2) && !selected)
            {
                selected = true;
                return true;
            }
        }
        return false;
    }
}

public class CSection : CFigure // класс отрезка
{
    public CSection(int x, int y, int radius) // конструктор по умолчанию
    {
        coords.X = x;
        coords.Y = y;
        rad = radius;
    }
    public override void SelfDraw(Graphics g) // Метод для отрисовки самого себя
    {
        Point point1 = new Point(coords.X - rad, coords.Y);
        Point point2 = new Point(coords.X + rad, coords.Y);
        Point[] curvePoints = { point1, point2 };

        if (selected == true)
            g.DrawPolygon(new Pen(colorT, 3), curvePoints);
        else
            g.DrawPolygon(new Pen(colorF, 3), curvePoints);
    }
    public override bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
    {
        if (fcntrl)
        {
            if (Math.Pow(e.X - coords.X, 2) + Math.Pow(e.Y - coords.Y, 2) <= Math.Pow(rad, 2) && !selected)
            {
                selected = true;
                return true;
            }
        }
        return false;
    }
}