using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace Pool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pen _pen;// = null;
        private Point _lineStart = new Point(0, 0);
        private Point _lineEnd = new Point(100, 100);

        public MainWindow()
        {
            InitializeComponent();
            //this.OnMouseMove += OnMouseMove1;

            _pen = new Pen();

            _canvas.MouseLeftButtonDown += Canvas_MouseLeftButtonDown;
            _canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;

            this.MouseDown += MainWindow_OnMouseDown;
            this.MouseUp += MainWindow_OnMouseUp;
            //_canvas.ActualHeight = 300;
            //_canvas.ActualWidth = 300;
        }

        private void MainWindow_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Canvas_MouseLeftButtonUp(sender, e);
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Canvas_MouseLeftButtonDown(sender, e);
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _lineStart = e.MouseDevice.GetPosition(this);
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _lineEnd = e.MouseDevice.GetPosition(this);

            _canvas.Children.Clear();
            var bgColor = (Color)ColorConverter.ConvertFromString("#003366");
            _canvas.Background = new SolidColorBrush(bgColor);

            Point p = e.MouseDevice.GetPosition(this.InputHitTest(new Point(0, 0)));
            EllipseGeometry eg= new EllipseGeometry();

            var lineGeom = new LineGeometry { StartPoint = _lineStart, EndPoint = _lineEnd };

            //_lineGeometry = lineGeom;

            Random rand = new Random();

            var lineColor = Color.FromArgb((byte)rand.Next(255),
                (byte)rand.Next(255),
                (byte)rand.Next(255),
                (byte)rand.Next(255));

            Path linePath = new Path
            {
                Stroke = new SolidColorBrush(lineColor),
                StrokeThickness = rand.Next(1, 10),
                Data = lineGeom
            };

            // add this line to the canvas
            _canvas.Children.Add(linePath);

            _canvas.InvalidateVisual();
        }

        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    _canvas.Children.Clear();
        //    var bgColor = (Color)ColorConverter.ConvertFromString("#003366");
        //    _canvas.Background = new SolidColorBrush(bgColor);

        //    Point p = e.MouseDevice.GetPosition(this.InputHitTest(new Point(0, 0)));

        //    var lineGeom = new LineGeometry { StartPoint = _lineStart, EndPoint = _lineEnd };

        //    Random rand = new Random();

        //    var lineColor = Color.FromArgb((byte)rand.Next(255),
        //        (byte)rand.Next(255),
        //        (byte)rand.Next(255),
        //        (byte)rand.Next(255));

        //    Path linePath = new Path
        //    {
        //        Stroke = new SolidColorBrush(lineColor),
        //        StrokeThickness = rand.Next(1, 10),
        //        Data = lineGeom
        //    };

        //    // add this line to the canvas
        //    _canvas.Children.Add(linePath);

        //    Ellipse myEllipse = new Ellipse();
        //    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
        //    mySolidColorBrush.Color = Colors.Black;
        //    myEllipse.Fill = mySolidColorBrush;
        //    myEllipse.StrokeThickness = 2;
        //    myEllipse.Stroke = Brushes.Black;

        //    // Set the width and height of the Ellipse.
        //    myEllipse.Width = 100;
        //    myEllipse.Height = 100;

        //    // How to set center of ellipse???
        //    //this.AddChild(myEllipse);

        //    LineGeometry myLineGeometry = new LineGeometry();

        //    myLineGeometry.StartPoint = _lineStart;
        //    myLineGeometry.EndPoint = _lineEnd;

        //    Line line = new Line();
        //    line.X1= _lineStart.X;
        //    line.Y1 = _lineStart.Y;

        //    line.X2 = _lineEnd.X;
        //    line.Y2 = _lineEnd.Y;
        //    line.Stroke = SystemColors.WindowFrameBrush;
        //    line.StrokeThickness = 2;

        //    Path myPath = new Path();
        //    myPath.Stroke = Brushes.Black;
        //    myPath.StrokeThickness = 1;
        //    myPath.Data = myLineGeometry;
        //    myPath.Fill = mySolidColorBrush; 
        //    myPath.StrokeThickness = 1;

        //    this.InvalidateVisual();

        //    _canvas.Children.Add(myEllipse);
        //    _canvas.Children.Add(line);
        //    _canvas.Children.Add(myPath);

        //    _canvas.InvalidateVisual();

        //    e.Handled = true;
        //}
    }
}
