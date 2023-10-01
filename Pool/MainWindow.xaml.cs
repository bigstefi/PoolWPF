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
        Color _canvasBackgroundColor = (Color)ColorConverter.ConvertFromString("#336699");
        private Pen _pen;// = null;
        private Point _lineStart = new Point(0, 0);
        private Point _lineEnd = new Point(100, 100);

        private List<Path> _lines = new List<Path>();
        private List<Path> _circles = new List<Path>();

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

            _canvas.Background = new SolidColorBrush(_canvasBackgroundColor);
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

            DrawLine(_lineStart, _lineEnd);

            int radius = 25;
            DrawCircle(new Point((_lineStart.X + _lineEnd.X)/2, (_lineStart.Y + _lineEnd.Y) / 2), radius);
        }

        private void DrawLine(Point start, Point end)
        {
            //Point p = e.MouseDevice.GetPosition(this.InputHitTest(new Point(0, 0)));
            //EllipseGeometry eg = new EllipseGeometry();

            var lineGeom = new LineGeometry { StartPoint = start, EndPoint = end };

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

            bool contentUpdated = AddLine(linePath);
            if(contentUpdated)
            {
                UpdateCanvasElements();
            }

            //_canvas.InvalidateVisual();
        }

        private void DrawCircle(Point cengter, double radius)
        {
            EllipseGeometry circleGeometry = new EllipseGeometry(cengter, radius, radius);

            Random rand = new Random();

            var circleColor = Color.FromArgb((byte)rand.Next(255),
                (byte)rand.Next(255),
                (byte)rand.Next(255),
                (byte)rand.Next(255));

            Path circlePath = new Path
            {
                Stroke = new SolidColorBrush(circleColor),
                StrokeThickness = rand.Next(1, 10),
                Data = circleGeometry
            };

            bool contentUpdated = AddCircle(circlePath);
            if (contentUpdated)
            {
                UpdateCanvasElements();
            }

            //_canvas.InvalidateVisual();
        }

        private void UpdateCanvasElements()
        {
            _canvas.Children.Clear();
            foreach (Path line in _lines)
            {
                _canvas.Children.Add(line);
            }
            foreach (Path circle in _circles)
            {
                _canvas.Children.Add(circle);
            }
        }

        private bool AddLine(Path line)
        {
            // ToDo: Check if gradient is the same, if yes, update the start and end points (max and min),
            // drop the original element, add the new one and return true (otherwise false)
            if(_lines.Contains(line))
            {
                return false;
            }

            _lines.Add(line);

            return true;
        }

        private bool AddCircle(Path circle)
        {
            // ToDo: Check if gradient is the same, if yes, update the start and end points (max and min),
            // drop the original element, add the new one and return true (otherwise false)
            if (_circles.Contains(circle))
            {
                return false;
            }

            _circles.Add(circle);

            return true;
        }
    }
}
