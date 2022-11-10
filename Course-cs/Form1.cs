using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.VisualStyles;
using System.Globalization;

using System.Diagnostics;

namespace Course_cs
{
   
    public partial class Form1 : Form
    {
        DrawGraph graph;
        List<Vertex> VertexList;
        List<Edge> EdgeList;

        int[,] AMatrix; //матрица смежности
        int selectedVertex1; //выбранные вершины, для соединения линиями
        int selectedVertex2;
        public List<string> cycle_matrix;
     
        public Form1()
        {
            InitializeComponent();
            VertexList = new List<Vertex>();
            graph = new DrawGraph(pic.Width, pic.Height);
            EdgeList = new List<Edge>();
            cycle_matrix = new List<string>();
            pic.Image = graph.GetBitmap();
           
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, 25, 25);
            Region rgn = new Region(path);
            drawVertexButton.Region = rgn;
            drawEdgeButton.Region = rgn;
            deleteALLButton.Region = rgn;
            deleteButton.Region = rgn;
            cycleButton.Region = rgn;
            buttonAdj.Region = rgn;
            

        }
        //кнопка - рисовать вершину
        private void drawVertexButton_Click(object sender, EventArgs e)
        {
            drawVertexButton.Enabled = false;

            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            graph.clearPic();
            graph.drawFullGraph(VertexList, EdgeList);
            pic.Image = graph.GetBitmap();
        }

        //кнопка - рисовать ребро
        private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            drawEdgeButton.Enabled = false;

            drawVertexButton.Enabled = true;
            deleteButton.Enabled = true;
            graph.clearPic();
            graph.drawFullGraph(VertexList, EdgeList);
            pic.Image = graph.GetBitmap();
            selectedVertex1 = -1;
            selectedVertex2 = -1;
        }

        //кнопка - удалить элемент
        private void deleteButton_Click(object sender, EventArgs e)
        {
            deleteButton.Enabled = false;

            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            graph.clearPic();
            graph.drawFullGraph(VertexList, EdgeList);
            pic.Image = graph.GetBitmap();
        }

        //кнопка - удалить граф
        private void deleteALLButton_Click(object sender, EventArgs e)
        {

            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            const string message = "Вы действительно хотите полностью удалить граф?";
            const string caption = "Удаление";
            var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MBSave == DialogResult.Yes)
            {
                VertexList.Clear();
                EdgeList.Clear();
                graph.clearPic();
                pic.Image = graph.GetBitmap();
            }
        }

        //кнопка - матрица смежности
        private void buttonAdj_Click(object sender, EventArgs e)
        {
            CreateMatrix();
        }

        Pen Bp = new Pen(Color.Black, 3);
        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            //нажата кнопка "рисовать вершину"
            if (drawVertexButton.Enabled == false)
            {
                if (graph.InTheRangeVertex(VertexList, e.X, e.Y)) return;
             if(VertexList.Contains(new Vertex(e.X, e.Y))) { }
                    else
                    {
                    VertexList.Add(new Vertex(e.X, e.Y));
                }
                
              
                graph.drawVertex(Bp, e.X, e.Y, VertexList.Count.ToString());
                pic.Image = graph.GetBitmap();
            }
            //нажата кнопка "рисовать ребро"
            if (drawEdgeButton.Enabled == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int i = 0; i < VertexList.Count; i++)
                    {
                        if (Math.Pow((VertexList[i].x - e.X), 2) + Math.Pow((VertexList[i].y - e.Y), 2) <= graph.R * graph.R / 5)
                        {
                            if (selectedVertex1 == -1)
                            {
                                graph.drawSelectedVertex(VertexList[i].x, VertexList[i].y, (i + 1).ToString());
                                selectedVertex1 = i;
                                pic.Image = graph.GetBitmap();
                                break;
                            }
                            if (selectedVertex2 == -1)
                            {
                                graph.drawSelectedVertex(VertexList[i].x, VertexList[i].y, (i + 1).ToString());
                                selectedVertex2 = i;
                                EdgeList.Add(new Edge(selectedVertex1, selectedVertex2));
                                graph.drawEdge(VertexList[selectedVertex1], VertexList[selectedVertex2], EdgeList[EdgeList.Count - 1], EdgeList.Count - 1);
                                selectedVertex1 = -1;
                                selectedVertex2 = -1;
                                pic.Image = graph.GetBitmap();
                                break;
                            }
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if ((selectedVertex1 != -1) &&
                        (Math.Pow((VertexList[selectedVertex1].x - e.X), 2) + Math.Pow((VertexList[selectedVertex1].y - e.Y), 2) <= graph.R * graph.R))
                    {
                        graph.drawVertex(Bp, VertexList[selectedVertex1].x, VertexList[selectedVertex1].y, (selectedVertex1 + 1).ToString());
                        selectedVertex1 = -1;
                        pic.Image = graph.GetBitmap();
                    }
                }
            }
            //нажата кнопка "удалить элемент"
            if (deleteButton.Enabled == false)
            {
                bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                //ищем, возможно была нажата вершина
                for (int i = 0; i < VertexList.Count; i++)
                {
                    if (Math.Abs(e.X - VertexList[i].x) <= graph.R / 2 && (Math.Abs(e.Y - VertexList[i].y) <= graph.R / 2))
                    {
                        for (int j = 0; j < EdgeList.Count; j++)
                        {
                            if ((EdgeList[j].VStart == i) || (EdgeList[j].VEnd == i))
                            {
                                EdgeList.RemoveAt(j);
                                j--;
                            }
                            else
                            {
                                if (EdgeList[j].VStart > i) EdgeList[j].VStart--;
                                if (EdgeList[j].VEnd > i) EdgeList[j].VEnd--;
                            }
                        }
                        VertexList.RemoveAt(i);
                        flag = true;
                        break;
                    }
                }
           
                if (!flag)
                {
                    for (int i = 0; i < EdgeList.Count; i++)
                    {
                        if (EdgeList[i].VStart == EdgeList[i].VEnd) 
                        {
                            if ((Math.Pow((VertexList[EdgeList[i].VStart].x - graph.R / 2 - e.X), 2) + (Math.Pow((VertexList[EdgeList[i].VEnd].y - graph.R / 2 - e.Y), 2))) <= (Math.Pow(graph.R / 2 + 4, 2)) &&
                               (Math.Pow((VertexList[EdgeList[i].VStart].x - graph.R / 2 - e.X), 2) + Math.Pow((VertexList[EdgeList[i].VEnd].y - graph.R / 2 - e.Y), 2) >= Math.Pow(graph.R / 2 - 4, 2)))
                            {
                                EdgeList.RemoveAt(i);
                                flag = true;
                                break;
                            }
                        }
                        else 
                        {
                            if (((e.X - VertexList[EdgeList[i].VStart].x) * (VertexList[EdgeList[i].VEnd].y - VertexList[EdgeList[i].VStart].y) / (VertexList[EdgeList[i].VEnd].x - VertexList[EdgeList[i].VStart].x) + VertexList[EdgeList[i].VStart].y) <= (e.Y + 4) &&
                                ((e.X - VertexList[EdgeList[i].VStart].x) * (VertexList[EdgeList[i].VEnd].y - VertexList[EdgeList[i].VStart].y) / (VertexList[EdgeList[i].VEnd].x - VertexList[EdgeList[i].VStart].x) + VertexList[EdgeList[i].VStart].y) >= (e.Y - 4))
                            {
                                if ((VertexList[EdgeList[i].VStart].x <= VertexList[EdgeList[i].VEnd].x && VertexList[EdgeList[i].VStart].x <= e.X && e.X <= VertexList[EdgeList[i].VEnd].x) ||
                                    (VertexList[EdgeList[i].VStart].x >= VertexList[EdgeList[i].VEnd].x && VertexList[EdgeList[i].VStart].x >= e.X && e.X >= VertexList[EdgeList[i].VEnd].x))
                                {
                                    EdgeList.RemoveAt(i);
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (flag)
                {
                    graph.clearPic();
                    graph.drawFullGraph(VertexList, EdgeList);
                    pic.Image = graph.GetBitmap();
                }
            }
        }

        //создание матрицы смежности 
        private void CreateMatrix()
        {
            AMatrix = new int[VertexList.Count, VertexList.Count];
            graph.GenMatrix(VertexList.Count, EdgeList, AMatrix);
            try
            {
                if (VertexList.Count == 0) { throw new Exception("Графа не существует. Добавьте вершины"); }
                dataGridView1.Rows.Clear();
                dataGridView1.Font = new Font("Comic Sans MS", 9);
                dataGridView1.RowCount = VertexList.Count;
                dataGridView1.ColumnCount = VertexList.Count;
                dataGridView1.RowHeadersWidth = 50;
                dataGridView1.ColumnHeadersHeight = 50;

                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                for (int i = 0; i < VertexList.Count; i++)
                {

                    for (int j = 0; j < VertexList.Count; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = AMatrix[i, j];
                        dataGridView1.Columns[j].Width = 30;
                    dataGridView1.Rows[i].Height = 30;
                    dataGridView1.Rows[i].HeaderCell.Value = $"V{i + 1}";
                        dataGridView1.Columns[j].HeaderCell.Value = $"V{j + 1}";
                       
                    }
                }
        }
            catch (Exception error) { MessageBox.Show(error.Message, "Ошибка"); }
}

     

        private void cycleButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            cycle_matrix.Clear();

            int[] color = new int[VertexList.Count];
            for (int i = 0; i < VertexList.Count; i++)
            {
                for (int k = 0; k < VertexList.Count; k++)
                {
                    color[k] = 1;
          
                }
                List<int> cycle = new List<int>();
                cycle.Add(i + 1); //недостает последней вершины
                DFScycle(i, i, EdgeList, color, -1, cycle, cycle_matrix);
            }
                if (cycle_matrix.Count == 0) { { MessageBox.Show("В данном графе нет гамильтонова цикла.\n Добавьте недостающие ребра", "Ошибка"); } }
             
            for (int i = 0; i < cycle_matrix.Count; i++)
            {
                listBox1.Items.Add(cycle_matrix[i]);

            }
            dr = true;
        }

        private void DFScycle(int posit, int endV, List<Edge> E, int[] color, int unavailableEdge, List<int> cycle, List<string> cycle_matrix)
        {
            if (posit != endV)
            {
                color[posit] = 2;
            }
            else
            {
                for (int i = 0; i < EdgeList.Count; i++)
                {
                    if (EdgeList[i].VStart == EdgeList[i].VEnd)
                    {

                        E.RemoveAt(i);
                    }
                }
                if (cycle.Count == VertexList.Count + 1)
                {
                    cycle.Reverse();

                    string s = cycle[0].ToString();
                    for (int D = 1; D < cycle.Count; D++)
                    {
                        s += "-" + cycle[D].ToString();

                    }
                    cycle_matrix.Add(s);

                    return;
                }
            }
            for (int h = 0; h < E.Count; h++)
            {
                if (h == unavailableEdge)
                    continue;
                if (color[E[h].VEnd] == 1 && E[h].VStart == posit)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(E[h].VEnd + 1);
                    DFScycle(E[h].VEnd, endV, E, color, h, cycleNEW, cycle_matrix);
                    color[E[h].VEnd] = 1;
                }
                else if (color[E[h].VStart] == 1 && E[h].VEnd == posit)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(E[h].VStart + 1);
                    DFScycle(E[h].VStart, endV, E, color, h, cycleNEW, cycle_matrix);
                    color[E[h].VStart] = 1;

                }
               

            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            graph.clearPic();
            graph.drawFullGraph(VertexList, EdgeList);
            try
            {
                if (listBox1.Items.Count == 0) return;
                if (listBox1 == null) { graph.drawFullGraph(VertexList, EdgeList); listBox1.ClearSelected(); return; };
                if (dr == true && listBox1.SelectedItem == null) throw new Exception("Не выбран цикл!");
                var temp = listBox1.SelectedItem.ToString().Split('-');

                List<Edge> buffer = new List<Edge>();

                for (int i = 0; i < temp.Length - 1; i++)
                {
                    buffer.Add(new Edge(int.Parse(temp[i].ToString()), int.Parse(temp[i + 1].ToString())));
                }
     
                graph.DrawGamilton(VertexList, buffer);

                pic.Image = graph.GetBitmap();
            }
            catch (Exception exc) { MessageBox.Show("Элемент не выбран\n" + exc.Message, "Ошибка"); }
            dr = false;

        }
        public static bool dr = false;
      
        private void Info_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
