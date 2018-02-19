using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Markup;


public class MouseHelper
{
    private FrameworkElement _eventSource;
    private Point _position;

    private Transform3DGroup _transformGroup;
    private TranslateTransform3D _translate
        = new TranslateTransform3D();

    public MouseHelper( Transform3DGroup transformGroup )
    {
        _transformGroup = transformGroup;
        _transformGroup.Children.Add(_translate);
    }


    public FrameworkElement EventSource
    {
        get { return _eventSource; }

        set
        {
            if (_eventSource != null)
            {
                _eventSource.MouseDown -= this.OnMouseDown;
                _eventSource.MouseUp -= this.OnMouseUp;
                _eventSource.MouseMove -= this.OnMouseMove;
            }

            _eventSource = value;

            _eventSource.MouseDown += this.OnMouseDown;
            _eventSource.MouseUp += this.OnMouseUp;
            _eventSource.MouseMove += this.OnMouseMove;
        }
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
        Mouse.Capture(EventSource, CaptureMode.Element);
        _position = e.GetPosition(EventSource);
    }

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
        Mouse.Capture(EventSource, CaptureMode.None);
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        Point currentPosition = e.GetPosition(EventSource);

        if (e.LeftButton == MouseButtonState.Pressed)
        {
            _translate.OffsetX += (_position.X - currentPosition.X) * 0.1;
            _translate.OffsetY -= (_position.Y - currentPosition.Y) * 0.1;
        }
        else if (e.RightButton == MouseButtonState.Pressed)
        {
            _translate.OffsetZ += (_position.Y - currentPosition.Y) * 0.1;
        }

        _position = currentPosition;
    }
}