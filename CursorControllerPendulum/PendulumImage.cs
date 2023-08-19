using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursorControllerPendulum
{
    internal class PendulumImage
    {
        public static int x0, y0;
        int x1, y1, x2, y2;
        Bitmap bitmap;
        private Graphics _graphics;
        Brush blackBrush = Brushes.Black;
        Brush redBrush = Brushes.Red;
        Brush blueBrush = Brushes.Blue;
        Pen blackPen = new Pen(Color.Black);
        private double _armLengthMultiplier = 2.4;

        private readonly int _ballRadius0;
        private readonly int _ballRadius1;
        private readonly int _ballRadius2;

        public PendulumImage(int width, int height, int ballRadius1, int ballRadius2)
        {
            bitmap = new Bitmap(width, height);
            _graphics = Graphics.FromImage(bitmap);
            x0 = width / 2;
            y0 = height / 3;

            _ballRadius0 = 5;
            _ballRadius1 = ballRadius1;
            _ballRadius2 = ballRadius2;
        }

        public Bitmap GetImage(DoublePendulum pendulum)
        {

            x1 = (int)(pendulum.Cx + (int)(pendulum.Radius1 * _armLengthMultiplier * Math.Sin(pendulum.Angle1)));
            y1 = (int)(pendulum.Cy + (int)(pendulum.Radius1 * _armLengthMultiplier * Math.Cos(pendulum.Angle1)));
            x2 = x1 + (int)(pendulum.Radius2 * _armLengthMultiplier * Math.Sin(pendulum.Angle2));
            y2 = y1 + (int)(pendulum.Radius2 * _armLengthMultiplier * Math.Cos(pendulum.Angle2));

            x1 = Math.Clamp(x1, _ballRadius1, bitmap.Width - _ballRadius1);
            y1 = Math.Clamp(y1, _ballRadius1, bitmap.Height - _ballRadius1);
            x2 = Math.Clamp(x2, _ballRadius2, bitmap.Width - _ballRadius2);
            y2 = Math.Clamp(y2, _ballRadius2, bitmap.Height - _ballRadius2);

            _graphics.Clear(Color.White);
            _graphics.DrawLine(blackPen, (int)pendulum.Cx, (int)pendulum.Cy, x1, y1);
            _graphics.DrawLine(blackPen, x1, y1, x2, y2);

            _graphics.FillEllipse(blackBrush, (int)pendulum.Cx - 5, (int)pendulum.Cy - _ballRadius0, _ballRadius0 * 2, _ballRadius0 * 2);
            _graphics.FillEllipse(redBrush, x1 - _ballRadius1, y1 - _ballRadius1, _ballRadius1 * 2, _ballRadius1 * 2);
            _graphics.FillEllipse(blueBrush, x2 - _ballRadius2, y2 - _ballRadius2, _ballRadius2 * 2, _ballRadius2 * 2);
            
            return bitmap;
        }
    }
}
