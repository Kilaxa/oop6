using Microsoft.VisualBasic.Devices;
using System;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static OOP6.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace OOP6
{
    public partial class Form1 : Form
    {
        SavedData savedData = new SavedData();
        SaverLoader loader = new SaverLoader();

        List<string> treeData = new List<string>();

        private List<CFigure> figures = new List<CFigure>(); // Лист для хранения всех фигур
        public int objectSize = 20;
        public bool Cntrl;

        Color color = Color.Red;
        Color red = Color.Red;
        Color green = Color.Green;
        Color purple = Color.RebeccaPurple;
        Color black = Color.Black;
        int colorIndex = 0;

        int selectedFigure = 0;

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

                CFigure newfigure = null;
                switch (selectedFigure)
                {
                    case 0:
                        newfigure = new CCircle(e.X, e.Y, objectSize, color);
                        break;
                    case 1:
                        newfigure = new CSquare(e.X, e.Y, objectSize, color);
                        break;
                    case 2:
                        newfigure = new CTriangle(e.X, e.Y, objectSize, color);
                        break;
                    case 3:
                        newfigure = new CSection(e.X, e.Y, objectSize, color);
                        break;
                }
                newfigure.setCondition(true);
                figures.Add(newfigure);
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
            SyncLtoTree();
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
            else if (e.KeyCode == Keys.Delete)
            {
                DelFigures();
            }
            else if (e.KeyCode == Keys.Up)
            {
                foreach (CFigure figure in figures)
                {
                    figure.MoveUp(this);
                }
                Refresh();
            }
            else if (e.KeyCode == Keys.Down)
            {
                foreach (CFigure figure in figures)
                {
                    figure.MoveDown(this);
                }
                Refresh();
            }
            else if (e.KeyCode == Keys.Left)
            {
                foreach (CFigure figure in figures)
                {
                    figure.MoveLeft(this);
                }
                Refresh();
            }
            else if (e.KeyCode == Keys.Right)
            {
                foreach (CFigure figure in figures)
                {
                    figure.MoveRight(this);
                }
                Refresh();
            }

            else if (e.KeyCode == Keys.Oemplus)
            {
                foreach (CFigure figure in figures)
                {
                    figure.GetBigger();
                }
                Refresh();
            }
            else if (e.KeyCode == Keys.OemMinus)
            {
                foreach (CFigure figure in figures)
                {
                    figure.GetSmaller();
                }
                Refresh();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Cntrl = checkBox1.Checked;
            foreach (CFigure figure in figures)
            {
                figure.Cntrled(Cntrl);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (CFigure figure in figures)
            {
                figure.GetBigger();
            }
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (CFigure figure in figures)
            {
                figure.GetSmaller();
            }
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DelFigures();
            SyncLtoTree();
        }

        void DelFigures() // Метод удаления фигур
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
            foreach (CFigure figure in figures) // снятие выделения со всех объектов
            {
                figure.setCondition(false);
            }
            Refresh();
            SyncLtoTree();
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
                    figure.SetColor(color);
            }
            SyncLtoTree();
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
            foreach (CFigure figure in figures) // выделения всех объектов
            {
                figure.setCondition(true);
            }
            SyncLtoTree();
            Refresh();
        }

        void Group()
        {
            CGroup newgroup = new CGroup();
            foreach (CFigure figure in figures)
            {
                if (figure.selected)
                {
                    newgroup.Add(figure);
                }
            }
            newgroup.iAmGroup = true;
            for (int i = 0; i < newgroup.childrens.Count; i++)
            {
                figures.Remove(newgroup.childrens[i]);
            }
            figures.Add(newgroup);

            Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Group();
            SyncLtoTree();
        }

        private void saveMe()
        {
            foreach (CFigure figure in figures)
            {
                figure.SelfSave(savedData);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            saveMe();
            File.Delete("D:\\test.txt");
            loader.Save(savedData, "D:\\test.txt");
        }
        CFigure read(StreamReader sr)
        {
            string line = sr.ReadLine();
            string[] data = line.Split(';');
            switch (data[0])
            {
                case "CGroup":
                    {
                        int count = int.Parse(data[1]);
                        CGroup newfigure = new CGroup();
                        for (int i = 0; i < count; i++)
                        {
                            newfigure.Add(read(sr));
                        }
                        return newfigure;
                    }
                default:
                    {
                        int x = int.Parse(data[2]);
                        int y = int.Parse(data[3]);
                        int rad = int.Parse(data[4]);
                        bool selected = bool.Parse(data[5]);
                        Color color = Color.FromArgb(int.Parse(data[1]));
                        switch (data[0])
                        {
                            case "CCircle":
                                {
                                    CCircle newfigure = new CCircle(x, y, rad, color);
                                    newfigure.setCondition(selected);
                                    return newfigure;
                                }
                            case "CSquare":
                                {
                                    CSquare newfigure = new CSquare(x, y, rad, color);
                                    newfigure.setCondition(selected);
                                    return newfigure;
                                }
                            case "CTriangle":
                                {
                                    CTriangle newfigure = new CTriangle(x, y, rad, color);
                                    newfigure.setCondition(selected);
                                    return newfigure;
                                }
                            case "CSection":
                                {
                                    CSection newfigure = new CSection(x, y, rad, color);
                                    newfigure.setCondition(selected);
                                    return newfigure;
                                }
                        }
                        return null;
                    }
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            foreach (CFigure figure in figures)
            {
                figure.setCondition(true);
            }
            DelFigures();

            StreamReader sr = new StreamReader("D:\\test.txt");

            while (!sr.EndOfStream)
            {
                figures.Add(read(sr));
            }
            sr.Close();
            Refresh();
        }

        TreeNode readdata(StreamReader sr)
        {
            string line = sr.ReadLine();
            string[] data = line.Split(';');
            switch (data[0])
            {
                case "CGroup":
                    {
                        int count = int.Parse(data[1]);
                        TreeNode newnode = new TreeNode();
                        newnode.Text = data[0].ToString();

                        for (int i = 0; i < count; i++)
                        {
                            newnode.Nodes.Add(readdata(sr));
                        }
                        return newnode;
                    }
                default:
                    {
                        Color color = Color.FromArgb(int.Parse(data[1]));
                        TreeNode treeNode = new TreeNode();
                        treeNode.Text = data[0].ToString();
                        if (data[2] == "0")
                        {
                            treeNode.ForeColor = color;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Blue;
                        }
                        return treeNode;
                    }
            }
        }

        public void SyncLtoTree()
        {
            foreach (CFigure figure in figures)
            {
                figure.RetData(treeData);
            }
            File.WriteAllLines("D:\\tree.txt", treeData);
            treeView1.Nodes.Clear();

            StreamReader sr = new StreamReader("D:\\tree.txt");

            while (!sr.EndOfStream)
            {
                treeView1.Nodes.Add(readdata(sr));
            }
            sr.Close();
            treeData.Clear();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            foreach (CFigure figure in figures)
            {
                figure.setCondition(false);
            }

            figures[e.Node.Index].setCondition(true);

            SyncLtoTree();

            e.Node.ForeColor = Color.Blue;
            Refresh();
        }
    }
}

public class CFigure
{
    public Point coords;
    public int rad;
    public bool selected = false;
    public bool fcntrl = false;
    public bool iAmGroup = false;

    public Color colorT = Color.CornflowerBlue;
    public Color mainColor = Color.Purple;
    public virtual void Cntrled(bool pressed)
    {
        fcntrl = pressed;
    }

    public virtual void setCondition(bool cond) // метод переключения выделения
    {
        selected = cond;
    }
    public virtual void SelfDraw(Graphics g) // Метод для отрисовки самого себя
    {

    }

    public virtual void SetColor(Color newcolor)
    {
        mainColor = newcolor;
    }

    public virtual void RetData(List<string> treeData)
    {
        StringBuilder line = new StringBuilder();
        line.Append(ToString()).Append(";");
        line.Append(mainColor.ToArgb()).Append(";");
        if (selected)
        {
            line.Append("1").Append(";");
        }
        else
        {
            line.Append("0").Append(";");
        }
        treeData.Add(line.ToString());
    }

    public virtual void SelfSave(SavedData savedData) // Метод для сохранения самого себя
    {
        StringBuilder line = new StringBuilder();
        line.Append(ToString()).Append(";");
        line.Append(mainColor.ToArgb()).Append(";");
        line.Append(coords.X.ToString()).Append(";");
        line.Append(coords.Y.ToString()).Append(";");
        line.Append(rad.ToString()).Append(";");
        line.Append(selected.ToString()).Append(";");
        savedData.linesToWrite.Add(line.ToString());
    }
    public virtual bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
    {
        return false;
    }

    public virtual void GetSmaller()
    {
        if (selected && rad > 10)
        {
            rad -= 5;
        }
    }
    public virtual void GetBigger()
    {
        if (selected && rad <= 95)
        {
            rad += 5;
        }
    }

    public virtual bool CanMoveUp(Form form)
    {
        if (((coords.Y - rad) > 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual bool CanMoveDown(Form form)
    {
        if ((coords.Y + rad) < (int)form.ClientSize.Height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual bool CanMoveLeft(Form form)
    {
        if ((coords.X - rad) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual bool CanMoveRight(Form form)
    {
        if ((coords.X + rad) < (int)form.ClientSize.Width)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void MoveUp(Form form)
    {
        if (selected && CanMoveUp(form))
        {
            coords.Y -= 1;
        }
    }
    public virtual void MoveDown(Form form)
    {
        if (selected && CanMoveDown(form))
        {
            coords.Y += 1;
        }
    }
    public virtual void MoveLeft(Form form)
    {
        if (selected && CanMoveLeft(form))
        {
            coords.X -= 1;
        }
    }

    public virtual void MoveRight(Form form)
    {
        if (selected && CanMoveRight(form))
        {
            coords.X += 1;
        }
    }

}
public class CCircle : CFigure// класс круга
{
    public CCircle(int x, int y, int radius, Color color) // конструктор по умолчанию
    {
        coords.X = x;
        coords.Y = y;
        rad = radius;
        mainColor = color;
    }
    public override void SelfDraw(Graphics g) // Метод для отрисовки самого себя
    {
        if (selected == true)
            g.DrawEllipse(new Pen(colorT, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);
        else
            g.DrawEllipse(new Pen(mainColor, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);
    }
    public override bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
    {
        if (fcntrl)
        {
            if (Math.Pow(e.X - coords.X, 2) + Math.Pow(e.Y - coords.Y, 2) <= Math.Pow(rad, 2) && !selected)
            {
                setCondition(true);
                return true;
            }
        }
        return false;
    }

}

public class CSquare : CFigure // класс квадрата
{
    public CSquare(int x, int y, int radius, Color color) // конструктор по умолчанию
    {
        coords.X = x;
        coords.Y = y;
        rad = radius;
        mainColor = color;
    }
    public override void SelfDraw(Graphics g) // Метод для отрисовки самого себя
    {
        if (selected == true)
            g.DrawRectangle(new Pen(colorT, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);
        else
            g.DrawRectangle(new Pen(mainColor, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);

    }
    public override bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
    {
        if (fcntrl)
        {
            if (Math.Pow(e.X - coords.X, 2) + Math.Pow(e.Y - coords.Y, 2) <= Math.Pow(rad, 2) && !selected)
            {
                setCondition(true);
                return true;
            }
        }
        return false;
    }
}

public class CTriangle : CFigure // класс треугольника
{
    public CTriangle(int x, int y, int radius, Color color) // конструктор по умолчанию
    {
        coords.X = x;
        coords.Y = y;
        rad = radius;
        mainColor = color;
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
            g.DrawPolygon(new Pen(mainColor, 3), curvePoints);
    }
    public override bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
    {
        if (fcntrl)
        {
            if (Math.Pow(e.X - coords.X, 2) + Math.Pow(e.Y - coords.Y, 2) <= Math.Pow(rad, 2) && !selected)
            {
                setCondition(true);
                return true;
            }
        }
        return false;
    }
}

public class CSection : CFigure // класс отрезка
{
    public CSection(int x, int y, int radius, Color color) // конструктор по умолчанию
    {
        coords.X = x;
        coords.Y = y;
        rad = radius;
        mainColor = color;
    }
    public override void SelfDraw(Graphics g) // Метод для отрисовки самого себя
    {
        Point point1 = new Point(coords.X - rad, coords.Y);
        Point point2 = new Point(coords.X + rad, coords.Y);
        Point[] curvePoints = { point1, point2 };

        if (selected == true)
            g.DrawPolygon(new Pen(colorT, 3), curvePoints);
        else
            g.DrawPolygon(new Pen(mainColor, 3), curvePoints);
    }
    public override bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
    {
        if (fcntrl)
        {
            if (Math.Pow(e.X - coords.X, 2) + Math.Pow(e.Y - coords.Y, 2) <= Math.Pow(rad, 2) && !selected)
            {
                setCondition(true);
                return true;
            }
        }
        return false;
    }
}

public class CGroup : CFigure
{
    public List<CFigure> childrens = new List<CFigure>();

    public CGroup()
    {

    }
    public void Add(CFigure component)
    {
        component.setCondition(false);
        childrens.Add(component);
    }

    public override void Cntrled(bool pressed)
    {
        foreach (CFigure component in childrens)
        {
            component.fcntrl = pressed;
        }
        fcntrl = pressed;
    }

    public override void setCondition(bool cond)
    {
        foreach (CFigure child in childrens)
        {
            child.setCondition(cond);
        }
        selected = cond;
    }

    public override void SetColor(Color newcolor)
    {
        foreach (CFigure child in childrens)
        {
            child.SetColor(newcolor);
        }
        mainColor = newcolor;
    }

    public override void SelfDraw(Graphics g)
    {
        foreach (CFigure child in childrens)
        {
            child.SelfDraw(g);
        }
    }

    public override void RetData(List<string> treeData)
    {
        StringBuilder line = new StringBuilder();
        line.Append(ToString()).Append(";");
        line.Append(childrens.Count.ToString()).Append(";");
        treeData.Add(line.ToString());
        foreach (CFigure child in childrens)
        {
            child.RetData(treeData);
        }
    }

    public override void SelfSave(SavedData savedData)
    {
        StringBuilder tmp = new StringBuilder();
        tmp.Append(ToString()).Append(";");
        tmp.Append(childrens.Count().ToString()).Append(";");
        savedData.linesToWrite.Add(tmp.ToString());
        foreach (CFigure figure in childrens)
        {
            figure.SelfSave(savedData);
        }
    }

    public override bool MouseCheck(MouseEventArgs e)
    {
        foreach (CFigure child in childrens)
        {
            if (child.MouseCheck(e))
            {
                return true;
            }
        }
        return false;
    }

    public override void GetSmaller()
    {
        foreach (CFigure child in childrens)
        {
            child.GetSmaller();
        }
    }
    public override void GetBigger()
    {
        foreach (CFigure child in childrens)
        {
            child.GetBigger();
        }
    }

    public override bool CanMoveUp(Form form)
    {
        foreach (CFigure child in childrens)
        {
            if (!child.CanMoveUp(form))
            {
                return false;
            }
        }
        return true;
    }
    public override bool CanMoveDown(Form form)
    {
        foreach (CFigure child in childrens)
        {
            if (!child.CanMoveDown(form))
            {
                return false;
            }
        }
        return true;
    }
    public override bool CanMoveLeft(Form form)
    {
        foreach (CFigure child in childrens)
        {
            if (!child.CanMoveLeft(form))
            {
                return false;
            }
        }
        return true;
    }
    public override bool CanMoveRight(Form form)
    {
        foreach (CFigure child in childrens)
        {
            if (!child.CanMoveRight(form))
            {
                return false;
            }
        }
        return true;
    }

    public override void MoveUp(Form form)
    {
        if (CanMoveUp(form))
        {
            foreach (CFigure child in childrens)
            {
                child.MoveUp(form);
            }
        }

    }
    public override void MoveDown(Form form)
    {
        if (CanMoveDown(form))
        {
            foreach (CFigure child in childrens)
            {
                child.MoveDown(form);
            }
        }
    }
    public override void MoveLeft(Form form)
    {
        if (CanMoveLeft(form))
        {
            foreach (CFigure child in childrens)
            {
                child.MoveLeft(form);
            }
        }
    }
    public override void MoveRight(Form form)
    {
        if (CanMoveRight(form))
        {
            foreach (CFigure child in childrens)
            {
                child.MoveRight(form);
            }
        }

    }
}

public class line : CFigure
{
    CFigure parent;
    CFigure child;

    public line(CFigure parent, CFigure child)
    {
        this.parent = parent;
        this.child = child;
    }

    public override void MoveUp(Form form)
    {
        
    }
}

public class SavedData
{
    public List<string> linesToWrite = new List<string>();
    public void Add(string line)
    {
        linesToWrite.Add(line);
    }
}

public class SaverLoader
{
    public void Save(SavedData savedData, string way)
    {
        File.WriteAllLines(way, savedData.linesToWrite);
    }
}