using Microsoft.VisualBasic.Devices;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static OOP6.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace OOP6
{
    public partial class Form1 : Form
    {
        private List<CFigure> figures = new List<CFigure>(); // ���� ��� �������� ���� �����
        public int objectSize = 10;
        public bool Cntrl;

        Color color = Color.Red;
        Color red = Color.Red;
        Color green = Color.Green;
        Color purple = Color.RebeccaPurple;
        Color black = Color.Black;
        int colorIndex = 0;

        int selectedFigure = 0;

        int allgindex = 1;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Paint(object sender, PaintEventArgs e) // ����� ��������� ������
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // �����������
            foreach (CFigure figure in figures)
            {
                figure.SelfDraw(e.Graphics); // ����� ����� ��� ��������� ������ ����
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Cntrl)
            {
                foreach (CFigure figure in figures) // ������ ��������� �� ���� ��������
                {
                    figure.setCondition(false);
                }

                switch (selectedFigure)
                {
                    case 0:
                        CCircle newcircle = new CCircle(e.X, e.Y, objectSize, color);
                        newcircle.setCondition(true);
                        figures.Add(newcircle);
                        break;
                    case 1:
                        CSquare newsquare = new CSquare(e.X, e.Y, objectSize, color);
                        newsquare.setCondition(true);
                        figures.Add((newsquare));
                        break;
                    case 2:
                        CTriangle newtriangle = new CTriangle(e.X, e.Y, objectSize, color);
                        newtriangle.setCondition(true);
                        figures.Add((newtriangle));
                        break;
                    case 3:
                        CSection newsection = new CSection(e.X, e.Y, objectSize, color);
                        newsection.setCondition(true);
                        figures.Add((newsection));
                        break;
                }
                Refresh();
            }
            else if (Cntrl) // ��������� ������, ���� ����� cntrl
            {
                foreach (CFigure figure in figures)
                {
                    if (figure.MouseCheck(e))
                    {
                        if (figure.gindex != 0)
                        {
                            foreach (CFigure cFigure in figures)
                            {
                                if (cFigure.gindex == figure.gindex)
                                {
                                    cFigure.setCondition(true);
                                }
                            }
                        }
                        else
                            figure.setCondition(true);
                        break;
                    }
                }
                foreach(CFigure figure in figures)
                {
                    if(figure.MouseCheck(e) && figure.gindex != 0)
                    {
                        foreach(CFigure cFigure in figures)
                        {
                            if(cFigure.gindex == figure.gindex)
                            {
                                cFigure.setCondition(true);
                            }
                        }
                    }
                }
                Refresh();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            objectSize = trackBar1.Value;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) // ������� ������
        {
            checkBox1.Checked = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) // ������� ������ delete � cntrl
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                checkBox1.Checked = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DelFigures();
            }
            else if (e.KeyCode == Keys.Up)
            {
                foreach (CFigure figure in figures)
                {
                    if (figure.selected && ((figure.coords.Y - figure.rad) > 0))
                    {
                        figure.coords.Y -= 3;
                    }
                }
                Refresh();
            }
            else if (e.KeyCode == Keys.Down)
            {
                foreach (CFigure figure in figures)
                {
                    if (figure.selected && ((figure.coords.Y + figure.rad) < (int)this.ClientSize.Height))
                    {
                        figure.coords.Y += 3;
                    }
                }
                Refresh();
            }
            else if (e.KeyCode == Keys.Left)
            {
                foreach (CFigure figure in figures)
                {
                    if (figure.selected && ((figure.coords.X - figure.rad) > 0))
                    {
                        figure.coords.X -= 3;
                    }
                }
                Refresh();
            }
            else if (e.KeyCode == Keys.Right)
            {
                foreach (CFigure figure in figures)
                {
                    if (figure.selected && ((figure.coords.X + figure.rad) < (int)this.ClientSize.Width))
                    {
                        figure.coords.X += 3;
                    }
                }
                Refresh();
            }

            else if (e.KeyCode == Keys.Oemplus)
            {
                GetBigger();
            }
            else if (e.KeyCode == Keys.OemMinus)
            {
                GetSmaller();
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

        private void button1_Click(object sender, EventArgs e)
        {
            GetBigger();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetSmaller();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DelFigures();
        }

        private void GetSmaller()
        {
            foreach (CFigure figure in figures)
            {
                if (figure.selected && figure.rad > 10)
                {
                    figure.rad -= 5;
                }
            }
            Refresh();
        }
        private void GetBigger()
        {
            foreach (CFigure figure in figures)
            {
                if (figure.selected && figure.rad <= 95)
                {
                    figure.rad += 5;
                }
            }
            Refresh();
        }
        void DelFigures() // ����� �������� �����
        {
            for (int i = 0; i < figures.Count; i++)
            {
                if (figures[i].selected == true)
                {
                    figures.Remove(figures[i]);
                    i--;
                }
            }
            Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (CFigure figure in figures) // ������ ��������� �� ���� ��������
            {
                figure.setCondition(false);
            }
            Refresh();
        }

        private void button_circle_Click(object sender, EventArgs e)
        {
            selectedFigure = 0;
        }

        private void button_square_Click(object sender, EventArgs e)
        {
            selectedFigure = 1;
        }

        private void button_triangle_Click(object sender, EventArgs e)
        {
            selectedFigure = 2;
        }

        private void button_section_Click(object sender, EventArgs e)
        {
            selectedFigure = 3;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (colorIndex < 3)
                colorIndex++;
            else
                colorIndex = 0;
            switch (colorIndex)
            {
                case 0:
                    color = red;
                    break;
                case 1:
                    color = green;
                    break;
                case 2:
                    color = purple;
                    break;
                case 3:
                    color = black;
                    break;
            }
            button5.BackColor = color;
            foreach (CFigure figure in figures)
            {
                if (figure.selected)
                    figure.colorF = color;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown += new PreviewKeyDownEventHandler(control_PreviewKeyDown);
            }
        }

        void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (CFigure figure in figures) // ��������� ���� ��������
            {
                figure.setCondition(true);
            }
            Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CGroup newgroup = new CGroup();
            foreach(CFigure figure in figures)
            {
                if(figure.selected)
                    newgroup.Add(figure, allgindex);
            }
            figures.Add(newgroup);
            allgindex++;
        }
    }
}

public class CFigure
{
    public Point coords;
    public int rad;
    public bool selected = false;
    public bool fcntrl = false;
    public int gindex = 0;

    public Color colorT = Color.CornflowerBlue;
    public Color colorF = Color.Purple;

    public virtual void setCondition(bool cond) // ����� ������������ ���������
    {
        selected = cond;
    }
    public virtual void SelfDraw(Graphics g) // ����� ��� ��������� ������ ����
    {

    }
    public virtual bool MouseCheck(MouseEventArgs e) // �������� ������� �� ��������� � ���� �������
    {
        return false;
    }

}
public class CCircle : CFigure// ����� �����
{
    public CCircle(int x, int y, int radius, Color color) // ����������� �� ���������
    {
        coords.X = x;
        coords.Y = y;
        rad = radius;
        colorF = color;
    }
    public override void SelfDraw(Graphics g) // ����� ��� ��������� ������ ����
    {
        if (selected == true)
            g.DrawEllipse(new Pen(colorT, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);
        else
            g.DrawEllipse(new Pen(colorF, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);
    }
    public override bool MouseCheck(MouseEventArgs e) // �������� ������� �� ��������� � ���� �������
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

public class CSquare : CFigure // ����� ��������
{
    public CSquare(int x, int y, int radius, Color color) // ����������� �� ���������
    {
        coords.X = x;
        coords.Y = y;
        rad = radius;
        colorF = color;
    }
    public override void SelfDraw(Graphics g) // ����� ��� ��������� ������ ����
    {
        if (selected == true)
            g.DrawRectangle(new Pen(colorT, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);
        else
            g.DrawRectangle(new Pen(colorF, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);

    }
    public override bool MouseCheck(MouseEventArgs e) // �������� ������� �� ��������� � ���� �������
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

public class CTriangle : CFigure // ����� ������������
{
    public CTriangle(int x, int y, int radius, Color color) // ����������� �� ���������
    {
        coords.X = x;
        coords.Y = y;
        rad = radius;
        colorF = color;
    }
    public override void SelfDraw(Graphics g) // ����� ��� ��������� ������ ����
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
    public override bool MouseCheck(MouseEventArgs e) // �������� ������� �� ��������� � ���� �������
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

public class CSection : CFigure // ����� �������
{
    public CSection(int x, int y, int radius, Color color) // ����������� �� ���������
    {
        coords.X = x;
        coords.Y = y;
        rad = radius;
        colorF = color;
    }
    public override void SelfDraw(Graphics g) // ����� ��� ��������� ������ ����
    {
        Point point1 = new Point(coords.X - rad, coords.Y);
        Point point2 = new Point(coords.X + rad, coords.Y);
        Point[] curvePoints = { point1, point2 };

        if (selected == true)
            g.DrawPolygon(new Pen(colorT, 3), curvePoints);
        else
            g.DrawPolygon(new Pen(colorF, 3), curvePoints);
    }
    public override bool MouseCheck(MouseEventArgs e) // �������� ������� �� ��������� � ���� �������
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

class CGroup : CFigure
{
    public List<CFigure> children = new List<CFigure>();

    public void Add(CFigure component, int index)
    {
        component.gindex = index;
        component.colorF = Color.DarkCyan;
        children.Add(component);
    }

    public void Remove(CFigure component)
    {
        children.Remove(component);
    }

    //public override void setCondition(bool cond) // ����� ������������ ���������
    //{
    //    foreach (CFigure child in children)
    //    {
    //        child.setCondition(cond);
    //    }
    //}

    //public override bool MouseCheck(MouseEventArgs e)
    //{
    //    foreach(CFigure child in children)
    //    {
    //        if(child.MouseCheck(e))
    //            return true;
    //    }
    //    return false;
    //}

}