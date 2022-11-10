using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using System.Windows.Forms.VisualStyles;

namespace Course_cs
{
        class Vertex
        {
            public int x { get; set; }
            public int y { get; set; }
            public Vertex()
            { }
            public Vertex(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        class Edge
        {
            public int VStart { get; set; }
            public int VEnd { get; set; }

            public Edge(int VSt, int VEn)
            {
                this.VStart = VSt;
                this.VEnd = VEn;
            }
        }

        class DrawGraph
        {
            Bitmap bitmap;
            Pen blackPen;
            Pen SelectPen;
            Pen LinePen;
            Graphics gr;
            Font fo;
        Pen EndvPen;
        Brush en = Brushes.Chocolate;
       Brush st = Brushes.BurlyWood;
        PointF point;
            public int R = 40; 
Pen[] PenColor;
    
        public DrawGraph(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            gr = Graphics.FromImage(bitmap);
            clearPic();
            blackPen = new Pen(Color.IndianRed);
            blackPen.Width = 4;
            SelectPen = new Pen(Color.BurlyWood);
            SelectPen.Width = 4;
            LinePen = new Pen(Color.Gray);
            LinePen.Width = 4;
            fo = new Font("Comic Sans MS", 9);
            EndvPen = new Pen(Color.Chocolate, 4);
           
          PenColor = new Pen[]
            {
            new Pen( Color.Violet,4),
            new Pen(Color.Plum, 4),
             new Pen(Color.Bisque, 4),
            new Pen(Color.Aquamarine, 4),
             new Pen(Color.HotPink, 4),
            new Pen(Color.DeepPink, 4),
          new Pen(Color.PeachPuff,4),
           new Pen(Color.Purple, 4),
           new Pen(Color.SpringGreen, 4),
             new Pen(Color.Green, 4),
           new Pen(Color.Red, 4),
            new Pen(Color.DarkCyan, 4),
            new Pen(Color.Gold, 4),
          new Pen(Color.Salmon, 4)   ,
            new Pen(Color.SeaGreen, 4),
            new Pen(Color.SteelBlue, 4),
            new Pen(Color.Sienna, 4),
            new Pen(Color.Khaki, 4),
            new Pen(Color.BlueViolet, 4),
            new Pen(Color.Thistle, 4),
            new Pen(Color.Moccasin, 4),
            new Pen(Color.Olive, 4),
            new Pen(Color.Orange,4)
        };
        }
    
            public Bitmap GetBitmap()
            {
                return bitmap;
            }

            public void clearPic()
        {
                gr.Clear(Color.White);
            }
       
        public void DrawGamilton(List <Vertex> VertexList, List <Edge> EdgeList)
        {
            clearPic();
            Random rnd = new Random();
            Pen pen = PenColor[rnd.Next(PenColor.Length)];
         
            for (int i = 0; i < EdgeList.Count; i++)
            {
                if  (EdgeList[i].VStart == EdgeList[i].VEnd)
                {gr.DrawEllipse(pen, VertexList[EdgeList[i].VStart-1].x -  R/2, 
                    VertexList[EdgeList[i].VEnd - 1].y - R ,  R, R);

            drawVertex(pen, VertexList[EdgeList[i].VStart - 1].x, VertexList[EdgeList[i].VStart - 1].y, (EdgeList[i].VStart).ToString());
        }
                else
                {
                    gr.DrawLine(pen, VertexList[EdgeList[i].VStart - 1].x, VertexList[EdgeList[i].VStart - 1].y,
                    VertexList[EdgeList[i].VEnd - 1].x, VertexList[EdgeList[i].VEnd - 1].y);

                    gr.DrawLine(pen, VertexList[EdgeList[i].VStart - 1].x, VertexList[EdgeList[i].VStart - 1].y,
                     (VertexList[EdgeList[i].VStart - 1].x + VertexList[EdgeList[i].VEnd - 1].x) / 2, (VertexList[EdgeList[i].VStart - 1].y + VertexList[EdgeList[i].VEnd - 1].y) / 2);

                
                    for (int d = 0; d < VertexList.Count; d++)
                    {
                        drawVertex(pen,VertexList[d].x, VertexList[d].y, (d + 1).ToString());
                    }
                    int k = VertexList.IndexOf(VertexList[EdgeList[0].VStart - 1]) + 1;
                    int io = VertexList.IndexOf(VertexList[EdgeList[EdgeList.Count - 1].VStart - 1])+1;

                    point = new PointF(VertexList[EdgeList[EdgeList.Count - 1].VStart - 1].x - 5, VertexList[EdgeList[EdgeList.Count - 1].VStart - 1].y - 9);
                  
                    gr.DrawEllipse(EndvPen, VertexList[EdgeList[EdgeList.Count-1].VStart - 1].x - 9, VertexList[EdgeList[EdgeList.Count-1].VStart - 1].y - 9, R / 2, R / 2);
                    gr.FillEllipse(en, VertexList[EdgeList[EdgeList.Count - 1].VStart - 1].x - 9, VertexList[EdgeList[EdgeList.Count - 1].VStart - 1].y - 9, R / 2, R / 2);
                    gr.DrawString(io.ToString(), fo, Brushes.White, point);

                    gr.DrawEllipse(SelectPen, VertexList[EdgeList[0].VStart - 1].x - 9, VertexList[EdgeList[0].VStart - 1].y - 9, R / 2, R / 2);
                    gr.FillEllipse(st, VertexList[EdgeList[0].VStart - 1].x-9, VertexList[EdgeList[0].VStart - 1].y-9, R / 2, R / 2);
                    point = new PointF(VertexList[EdgeList[0].VStart - 1].x - 5, VertexList[EdgeList[0].VStart - 1].y - 9);
                    gr.DrawString(k.ToString(), fo, Brushes.White, point);
                }
            }
  

        }
        public void drawVertex(Pen p, int x, int y, string number)
        {
            Brush bru = default;

         if (p.Color == Color.Violet) { bru = Brushes.Violet; }
         else if (p.Color == Color.Plum) { bru = Brushes.Plum; }
         else if (p.Color == Color.Bisque) { bru = Brushes.Bisque; }
         else if (p.Color == Color.Aquamarine) { bru = Brushes.Aquamarine; }
         else if (p.Color == Color.HotPink) { bru = Brushes.HotPink; }
         else if (p.Color == Color.DeepPink) { bru = Brushes.DeepPink; }
         else if (p.Color == Color.PeachPuff) { bru = Brushes.PeachPuff; }
         else if (p.Color == Color.Purple) { bru = Brushes.Purple; }
         else if (p.Color == Color.SpringGreen) { bru = Brushes.SpringGreen; }
         else if (p.Color == Color.Green) { bru = Brushes.Green; }
         else if (p.Color == Color.Red) { bru = Brushes.Red; }
         else if (p.Color == Color.DarkCyan) { bru = Brushes.DarkCyan; }
         else if (p.Color == Color.Gold) { bru = Brushes.Gold; }
         else if (p.Color == Color.Salmon) { bru = Brushes.Salmon; }
         else if (p.Color == Color.SeaGreen) { bru = Brushes.SeaGreen; }
         else if (p.Color == Color.SteelBlue) { bru = Brushes.SteelBlue; }
         else if (p.Color == Color.Sienna) { bru = Brushes.Sienna; }
         else if (p.Color == Color.Khaki) { bru = Brushes.Khaki; }
         else if (p.Color == Color.BlueViolet) { bru = Brushes.BlueViolet; }
         else if (p.Color == Color.Thistle) { bru = Brushes.Thistle; }
         else if (p.Color == Color.Moccasin) { bru = Brushes.Moccasin; }
         else if (p.Color == Color.Olive) { bru = Brushes.Olive; }
         else if (p.Color == Color.Orange) { bru = Brushes.Orange; }
         else { bru = Brushes.IndianRed; p.Color = Color.IndianRed; }
                
            
                gr.FillEllipse(bru, (x - 9), (y - 9), R / 2, R / 2);
                gr.DrawEllipse(p, (x - 9), (y - 9), R / 2, R / 2);
                point = new PointF(x - 5, y - 9);
                gr.DrawString(number, fo, Brushes.White, point);
            

        }
            public void drawSelectedVertex(int x, int y, string number)
            {
            gr.FillEllipse(st, (x - 9), (y - 9), R / 2, R / 2);
            gr.DrawEllipse(SelectPen, (x - 9), (y - 9), R/2, R/2);
            point = new PointF(x - 5, y - 9);
            gr.DrawString(number, fo, Brushes.White, point);
        }
        public bool InTheRangeVertex(List<Vertex> vertex, int x, int y)
        {
            foreach (var item in vertex)
                if ((Math.Abs(x - item.x) <= R*1.5 ) && (Math.Abs(y - item.y) <= R*1.5)) return true;
            return false;
        }
        public void drawEdge(Vertex V1, Vertex V2, Edge E, int numberE)
            {
                if (E.VStart == E.VEnd)
                {
                    gr.DrawEllipse(LinePen, (V1.x - R/2), (V1.y - R), R,  R);
              

                drawVertex(blackPen, V1.x, V1.y, (E.VStart + 1).ToString());
                }
                else
                {
                    gr.DrawLine(LinePen, V1.x , V1.y, V2.x, V2.y);
          

                drawVertex(blackPen, V1.x, V1.y, (E.VStart + 1).ToString());
                    drawVertex(blackPen, V2.x, V2.y, (E.VEnd + 1).ToString());
                }
            }

            public void drawFullGraph(List<Vertex> V, List<Edge> E)
            {
                //рисуем ребра
                for (int i = 0; i < E.Count; i++)
                {
                    if (E[i].VStart == E[i].VEnd)
                    {
                        gr.DrawEllipse(LinePen, (V[E[i].VStart].x -R/2), (V[E[i].VStart].y - R), R, R);
                      
                      
                    }
                    else
                { 
                       gr.DrawLine(LinePen, V[E[i].VStart].x, V[E[i].VStart].y , V[E[i].VEnd].x, V[E[i].VEnd].y);
          
                       
                    }
                }
           
                for (int i = 0; i < V.Count; i++)
                {
                    drawVertex(blackPen, V[i].x, V[i].y, (i + 1).ToString());
                }
            }

         
            public void GenMatrix(int numberV, List<Edge> E, int[,] matrix)
            {
                for (int i = 0; i < numberV; i++)
                    for (int j = 0; j < numberV; j++)
                        matrix[i, j] = 0;
                for (int i = 0; i < E.Count; i++)
                {
                    matrix[E[i].VStart, E[i].VEnd] = 1;
                    matrix[E[i].VEnd, E[i].VStart] = 1;
                }
            }
    }
    }

