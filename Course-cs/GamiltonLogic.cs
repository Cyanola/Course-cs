//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Course_cs
//{
//    public class GamiltonLogic
//    {
//        DrawGraph gr;
//        Form1 form;
//        Graphics g;
//        public List<Edge> Edg;
//        public List<Vertex> Verts;
//        public string DrawGamilton(List<Vertex> VertexList, List<Edge> EdgeList)
//        {
//            gr.clearPic();
//            Random rnd = new Random();
//            Pen pen = gr.PenColor[rnd.Next(gr.PenColor.Length)];
//            string redHex = ColorTranslator.ToHtml(pen.Color);
//            for (int i = 0; i < EdgeList.Count; i++)
//            {
//                if (EdgeList[i].VStart == EdgeList[i].VEnd)
//                {
//                    g.DrawEllipse(pen, VertexList[EdgeList[i].VStart - 1].x - gr.R / 2,
//                    VertexList[EdgeList[i].VEnd - 1].y - gr.R, gr.R, gr.R);

//                    gr.drawVertex(pen, VertexList[EdgeList[i].VStart - 1].x, VertexList[EdgeList[i].VStart - 1].y, (EdgeList[i].VStart).ToString());
//                }
//                else
//                {
//                    g.DrawLine(pen, VertexList[EdgeList[i].VStart - 1].x, VertexList[EdgeList[i].VStart - 1].y,
//                    VertexList[EdgeList[i].VEnd - 1].x, VertexList[EdgeList[i].VEnd - 1].y);

//                    g.DrawLine(pen, VertexList[EdgeList[i].VStart - 1].x, VertexList[EdgeList[i].VStart - 1].y,
//                     (VertexList[EdgeList[i].VStart - 1].x + VertexList[EdgeList[i].VEnd - 1].x) / 2, (VertexList[EdgeList[i].VStart - 1].y + VertexList[EdgeList[i].VEnd - 1].y) / 2);


//                    for (int d = 0; d < VertexList.Count; d++)
//                    {
//                        gr.drawVertex(pen, VertexList[d].x, VertexList[d].y, (d + 1).ToString());
//                    }
//                    int k = VertexList.IndexOf(VertexList[EdgeList[0].VStart - 1]) + 1;
//                    int io = VertexList.IndexOf(VertexList[EdgeList[EdgeList.Count - 1].VStart - 1]) + 1;

//                    gr.point = new PointF(VertexList[EdgeList[EdgeList.Count - 1].VStart - 1].x - 5, VertexList[EdgeList[EdgeList.Count - 1].VStart - 1].y - 9);

//                    g.DrawEllipse(gr.EndvPen, VertexList[EdgeList[EdgeList.Count - 1].VStart - 1].x - 9, VertexList[EdgeList[EdgeList.Count - 1].VStart - 1].y - 9, gr.R / 2, gr.R / 2);
//                    g.FillEllipse(gr.en, VertexList[EdgeList[EdgeList.Count - 1].VStart - 1].x - 9, VertexList[EdgeList[EdgeList.Count - 1].VStart - 1].y - 9, gr.R / 2, gr.R / 2);
//                    g.DrawString(io.ToString(), gr.fo, Brushes.White, gr.point);

//                    g.DrawEllipse(SelectPen, VertexList[EdgeList[0].VStart - 1].x - 9, VertexList[EdgeList[0].VStart - 1].y - 9, gr.R / 2, gr.R / 2);
//                    g.FillEllipse(st, VertexList[EdgeList[0].VStart - 1].x - 9, VertexList[EdgeList[0].VStart - 1].y - 9, gr.R / 2, gr.R / 2);
//                    gr.point = new PointF(VertexList[EdgeList[0].VStart - 1].x - 5, VertexList[EdgeList[0].VStart - 1].y - 9);
//                    g.DrawString(k.ToString(), gr.fo, Brushes.White, gr.point);
//                }
//            }
//            return redHex;

//        }
//        public void DFScycle(int posit, int endV, List<Edge> E, int[] color, int unavailableEdge, List<int> cycle, List<string> cycle_matrix)
//        {
//            if (posit != endV)
//            {
//                color[posit] = 2;
//            }
//            else
//            {
//                for (int i = 0; i < Edg.Count; i++)
//                {
//                    if (Edg[i].VStart == Edg[i].VEnd)
//                    {

//                        E.RemoveAt(i);
//                    }
//                }
//                if (cycle.Count == Verts.Count + 1)
//                {
//                    cycle.Reverse();

//                    string s = cycle[0].ToString();
//                    for (int D = 1; D < cycle.Count; D++)
//                    {
//                        s += "-" + cycle[D].ToString();

//                    }
//                    cycle_matrix.Add(s);

//                    return;
//                }
//            }
//            for (int h = 0; h < E.Count; h++)
//            {
//                if (h == unavailableEdge)
//                    continue;
//                if (color[E[h].VEnd] == 1 && E[h].VStart == posit)
//                {
//                    List<int> cycleNEW = new List<int>(cycle);
//                    cycleNEW.Add(E[h].VEnd + 1);
//                    DFScycle(E[h].VEnd, endV, E, color, h, cycleNEW, cycle_matrix);
//                    color[E[h].VEnd] = 1;
//                }
//                else if (color[E[h].VStart] == 1 && E[h].VEnd == posit)
//                {
//                    List<int> cycleNEW = new List<int>(cycle);
//                    cycleNEW.Add(E[h].VStart + 1);
//                    DFScycle(E[h].VStart, endV, E, color, h, cycleNEW, cycle_matrix);
//                    color[E[h].VStart] = 1;

//                }


//            }
//        }
//    }
//}
