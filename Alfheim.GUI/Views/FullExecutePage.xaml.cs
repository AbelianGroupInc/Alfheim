using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Variables.Services;
using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Alfheim.GUI.Views
{
    public class Point
    {
        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public static Point Create(double x, double y, double z)
        {
            return new Point(x, y, z);
        }
    }
    public partial class FullExecutePage : Page
    {
        float rotatePyramid = 0;
        Page mOwner;
        Point[,] resultsMap = new Point[50, 50];

        public FullExecutePage(Page owner)
        {
            InitializeComponent();
            mOwner = owner;
            Init();
        }

        private void Init()
        {
            var vars = LinguisticVariableService.Instance.InputLinguisticVariables;

            if (vars.Count != 2)
                return;

            for(int x = 0; x < 50; x++)
            {
                for(int y = 0; y < 50; y++)
                {
                    FuzzyLogicQuery flq = new FuzzyLogicQuery
                        (new Dictionary<LinguisticVariable, double> {
                            { vars[0], ((double)vars[0].MaxValue * ((double)x / 50)) + (double)vars[0].MinValue},
                            { vars[1], ((double)vars[1].MaxValue * ((double)y / 50)) + (double)vars[1].MinValue}
                         });

                    resultsMap[x, y] = new Point((double)x / 50, (double)y / 50, flq.Execute());
                }
            }

            NormalizationMap();
        }

        private void NormalizationMap()
        {
            for(int x = 0; x < 50; x++)
            {
                for(int y = 0; y < 50; y++)
                {
                    if (Double.IsNaN(resultsMap[x, y].Z))
                        resultsMap[x, y].Z = 0.0;
                }
            }
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            args.OpenGL.Enable(OpenGL.GL_DEPTH_TEST);
            args.OpenGL.ClearColor(1.0f, 1.0f, 1.0f, 0f);
        }

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Reset the modelview matrix.
            gl.LoadIdentity();

            //  Move the geometry into a fairly central position.
            gl.Translate(0, 0.0f, -2f);

            //  Draw a pyramid. First, rotate the modelview matrix.
            gl.Rotate(-60f, 1.0f, 0.0f, 0.0f);
            gl.Rotate(rotatePyramid, 0.0f, 0.0f, 1.0f);

            //  Start drawing triangles.
            int xSize = 50;
            int ySize = 50;

            //gl.Begin(OpenGL.GL_QUADS);

            float z = 0.001f;
            float k = 0.6f;
            //int counter = 0;

            for (int x = 0; x < xSize - 1; x++)
            {
                for (int y = 0; y < ySize - 1; y++)
                {
                    var c = GetColor(resultsMap[x, y]);
                    var center = Center(Center(resultsMap[x, y], resultsMap[x + 1, y + 1]), Center(resultsMap[x + 1, y], resultsMap[x, y + 1]));

                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);

                    gl.Begin(OpenGL.GL_TRIANGLES);

                    c = GetColor(resultsMap[x, y]);
                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    gl.Vertex(resultsMap[x, y].X - 0.5f, resultsMap[x, y].Y - 0.5f, resultsMap[x, y].Z * k);

                    c = GetColor(resultsMap[x + 1, y]);
                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    gl.Vertex(resultsMap[x + 1, y].X - 0.5f, resultsMap[x + 1, y].Y - 0.5f, resultsMap[x + 1, y].Z * k);

                    c = GetColor(center);
                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    gl.Vertex(center.X - 0.5f, center.Y - 0.5f, center.Z * k);

                    c = GetColor(resultsMap[x + 1, y]);
                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    gl.Vertex(resultsMap[x + 1, y].X - 0.5f, resultsMap[x + 1, y].Y - 0.5f, resultsMap[x + 1, y].Z * k);

                    c = GetColor(resultsMap[x + 1, y + 1]);
                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    gl.Vertex(resultsMap[x + 1, y + 1].X - 0.5f, resultsMap[x + 1, y + 1].Y - 0.5f, resultsMap[x + 1, y + 1].Z * k);

                    c = GetColor(center);
                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    gl.Vertex(center.X - 0.5f, center.Y - 0.5f, center.Z * k);

                    c = GetColor(resultsMap[x + 1, y + 1]);
                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    gl.Vertex(resultsMap[x + 1, y + 1].X - 0.5f, resultsMap[x + 1, y + 1].Y - 0.5f, resultsMap[x + 1, y + 1].Z * k);

                    c = GetColor(resultsMap[x, y + 1]);
                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    gl.Vertex(resultsMap[x, y + 1].X - 0.5f, resultsMap[x, y + 1].Y - 0.5f, resultsMap[x, y + 1].Z * k);

                    c = GetColor(center);
                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    gl.Vertex(center.X - 0.5f, center.Y - 0.5f, center.Z * k);

                    c = GetColor(resultsMap[x, y + 1]);
                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    gl.Vertex(resultsMap[x, y + 1].X - 0.5f, resultsMap[x, y + 1].Y - 0.5f, resultsMap[x, y + 1].Z * k);

                    c = GetColor(resultsMap[x, y]);
                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    gl.Vertex(resultsMap[x, y].X - 0.5f, resultsMap[x, y].Y - 0.5f, resultsMap[x, y].Z * k);

                    c = GetColor(center);
                    gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    gl.Vertex(center.X - 0.5f, center.Y - 0.5f, center.Z * k);

                    gl.End();

                    gl.Color(0.1f, 0.1f, 0.1f);
                    gl.Begin(OpenGL.GL_LINES);
                    gl.Vertex(resultsMap[x, y].X - 0.5f, resultsMap[x, y].Y - 0.5f, resultsMap[x, y].Z * k + z);
                    gl.Vertex(center.X - 0.5f, center.Y - 0.5f, center.Z * k + z);


                    gl.Vertex(resultsMap[x + 1, y].X - 0.5f, resultsMap[x + 1, y].Y - 0.5f, resultsMap[x + 1, y].Z * k + z);
                    gl.Vertex(center.X - 0.5f, center.Y - 0.5f, center.Z * k + z);

                    gl.Vertex(resultsMap[x + 1, y + 1].X - 0.5f, resultsMap[x + 1, y + 1].Y - 0.5f, resultsMap[x + 1, y + 1].Z * k + z);
                    gl.Vertex(center.X - 0.5f, center.Y - 0.5f, center.Z * k + z);

                    gl.Vertex(resultsMap[x, y + 1].X - 0.5f, resultsMap[x, y + 1].Y - 0.5f, resultsMap[x, y + 1].Z * k + z);
                    gl.Vertex(center.X - 0.5f, center.Y - 0.5f, center.Z * k + z);

                    gl.End();
                    //counter = (counter + 1) % 4;

                    //gl.Vertex(resultsMap[x, y].X - 0.5f, resultsMap[x, y].Y - 0.5f, resultsMap[x, y].Z * k);

                    ////c = GetColor(resultsMap[x + 1, y]);
                    ////gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    //gl.Vertex(resultsMap[x + 1, y].X - 0.5f, resultsMap[x + 1, y].Y - 0.5f, resultsMap[x + 1, y].Z * k);

                    ////c = GetColor(resultsMap[x + 1, y + 1]);
                    ////gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    //gl.Vertex(resultsMap[x + 1, y + 1].X - 0.5f, resultsMap[x + 1, y + 1].Y - 0.5f, resultsMap[x + 1, y + 1].Z * k);

                    ////c = GetColor(resultsMap[x, y + 1]);
                    ////gl.Color((float)c.R / 255f, (float)c.G / 255f, (float)c.B / 255f);
                    //gl.Vertex(resultsMap[x, y + 1].X - 0.5f, resultsMap[x, y + 1].Y - 0.5f, resultsMap[x, y + 1].Z * k);
                }
            }

            gl.Color(0.2f, 0.2f, 0.2f);
            gl.Begin(OpenGL.GL_QUADS);
            for (int y = 0; y < ySize - 1; y++)
            {
                gl.Vertex(resultsMap[0, y].X - 0.5f, resultsMap[0, y].Y - 0.5f, resultsMap[0, y].Z * k);
                gl.Vertex(resultsMap[0, y].X - 0.5f, resultsMap[0, y].Y - 0.5f, -0.05);
                gl.Vertex(resultsMap[0, y + 1].X - 0.5f, resultsMap[0, y + 1].Y - 0.5f, -0.05);
                gl.Vertex(resultsMap[0, y + 1].X - 0.5f, resultsMap[0, y + 1].Y - 0.5f, resultsMap[0, y + 1].Z * k);
            }
            gl.End();

            gl.Begin(OpenGL.GL_QUADS);
            for (int y = 0; y < ySize - 1; y++)
            {
                gl.Vertex(resultsMap[xSize - 1, y].X - 0.5f, resultsMap[xSize - 1, y].Y - 0.5f, resultsMap[xSize - 1, y].Z * k);
                gl.Vertex(resultsMap[xSize - 1, y].X - 0.5f, resultsMap[xSize - 1, y].Y - 0.5f, -0.05);
                gl.Vertex(resultsMap[xSize - 1, y + 1].X - 0.5f, resultsMap[xSize - 1, y + 1].Y - 0.5f, -0.05);
                gl.Vertex(resultsMap[xSize - 1, y + 1].X - 0.5f, resultsMap[xSize - 1, y + 1].Y - 0.5f, resultsMap[xSize - 1, y + 1].Z * k);
            }
            gl.End();

            gl.Begin(OpenGL.GL_QUADS);
            for (int x = 0; x < xSize - 1; x++)
            {
                gl.Vertex(resultsMap[x, 0].X - 0.5f, resultsMap[xSize - 1, 0].Y - 0.5f, resultsMap[x, 0].Z * k);
                gl.Vertex(resultsMap[x, 0].X - 0.5f, resultsMap[xSize - 1, 0].Y - 0.5f, -0.05);
                gl.Vertex(resultsMap[x + 1, 0].X - 0.5f, resultsMap[x + 1, 0].Y - 0.5f, -0.05);
                gl.Vertex(resultsMap[x + 1, 0].X - 0.5f, resultsMap[x + 1, 0].Y - 0.5f, resultsMap[x + 1, 0].Z * k);
            }
            gl.End();

            gl.Begin(OpenGL.GL_QUADS);
            for (int x = 0; x < xSize - 1; x++)
            {
                gl.Vertex(resultsMap[x, ySize - 1].X - 0.5f, resultsMap[x, ySize - 1].Y - 0.5f, resultsMap[x, ySize - 1].Z * k);
                gl.Vertex(resultsMap[x, ySize - 1].X - 0.5f, resultsMap[x, ySize - 1].Y - 0.5f, -0.05);
                gl.Vertex(resultsMap[x + 1, ySize - 1].X - 0.5f, resultsMap[x + 1, ySize - 1].Y - 0.5f, -0.05);
                gl.Vertex(resultsMap[x + 1, ySize - 1].X - 0.5f, resultsMap[x + 1, ySize - 1].Y - 0.5f, resultsMap[x + 1, ySize - 1].Z * k);
            }
            gl.End();
            //gl.Color(0.1f, 0.1f, 0.1f);


            //gl.Begin(OpenGL.GL_LINES);
            //for (int x = 0; x < xSize; x++)
            //{
            //    for (int y = 0; y < ySize; y++)
            //    {
            //        if (x + 1 < xSize)
            //        {
            //            gl.Vertex(resultsMap[x, y].X - 0.5f, resultsMap[x, y].Y - 0.5f, resultsMap[x, y].Z * k + z);
            //            gl.Vertex(resultsMap[x + 1, y].X - 0.5f, resultsMap[x + 1, y].Y - 0.5f, resultsMap[x + 1, y].Z * k + z);
            //        }

            //        if (y + 1 < ySize)
            //        {
            //            gl.Vertex(resultsMap[x, y].X - 0.5f, resultsMap[x, y].Y - 0.5f, resultsMap[x, y].Z * k + z);
            //            gl.Vertex(resultsMap[x, y + 1].X - 0.5f, resultsMap[x, y + 1].Y - 0.5f, resultsMap[x, y + 1].Z * k + z);
            //        }
            //    }
            //}

            //gl.End();

            gl.Flush(); 
        }

        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;
            
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            
            gl.Perspective(45.0f, (float)gl.RenderContextProvider.Width /
                (float)gl.RenderContextProvider.Height,
                0.1f, 100.0f);
            
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        public System.Drawing.Color GetColor(Point p)
        {
            var h = p.Z;

            return System.Drawing.Color.FromArgb((byte)(255 * p.Z), (byte)(255 - (255 * p.Z)), 0);
        }

        private Point Center(Point a, Point b)
        {
            return Point.Create((a.X + b.X) / 2, (a.Y + b.Y) / 2, (a.Z + b.Z) / 2);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).OpenPage(mOwner);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            rotatePyramid = (float)e.NewValue;
        }
    }
}
