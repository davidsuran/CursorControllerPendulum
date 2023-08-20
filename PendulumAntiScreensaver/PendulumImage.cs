namespace CursorControllerPendulum
{
    internal class PendulumImage
    {
        private int _x1, _y1, _x2, _y2;
        Bitmap bitmap;
        private Graphics _graphics;
        private readonly Brush blackBrush = Brushes.Black;
        
        private readonly Brush redBrush = Brushes.Red;
        private readonly Brush redDarkBrush = Brushes.DarkRed;
        private readonly Brush blueBrush = Brushes.DarkBlue;

        private readonly Color _bgColor = Color.Wheat;

        Pen blackPen = new Pen(Color.Black);
        private double _armLengthMultiplier = 2.4;

        private readonly int _ballRadius0;
        private readonly int _ballRadius1;
        private readonly int _ballRadius2;


        private Point _p2 = new Point();
        public Point P2 => _p2;


        public PendulumImage(int width, int height, int ballRadius1, int ballRadius2)
        {
            bitmap = new Bitmap(width, height);
            _graphics = Graphics.FromImage(bitmap);

            _ballRadius0 = 5;
            _ballRadius1 = ballRadius1;
            _ballRadius2 = ballRadius2;
        }

        public Bitmap GetImage(DoublePendulum pendulum)
        {
            _x1 = (int)(pendulum.X0 + (int)(pendulum.Radius1 * _armLengthMultiplier * Math.Sin(pendulum.Angle1)));
            _y1 = (int)(pendulum.Y0 + (int)(pendulum.Radius1 * _armLengthMultiplier * Math.Cos(pendulum.Angle1)));
            _x2 = _x1 + (int)(pendulum.Radius2 * _armLengthMultiplier * Math.Sin(pendulum.Angle2));
            _y2 = _y1 + (int)(pendulum.Radius2 * _armLengthMultiplier * Math.Cos(pendulum.Angle2));

            _x1 = Math.Clamp(_x1, _ballRadius1, bitmap.Width - _ballRadius1);
            _y1 = Math.Clamp(_y1, _ballRadius1, bitmap.Height - _ballRadius1);
            _x2 = Math.Clamp(_x2, _ballRadius2, bitmap.Width - _ballRadius2);
            _y2 = Math.Clamp(_y2, _ballRadius2, bitmap.Height - _ballRadius2);

            _graphics.Clear(_bgColor);
            _graphics.DrawLine(blackPen, (int)pendulum.X0, (int)pendulum.Y0, _x1, _y1);
            _graphics.DrawLine(blackPen, _x1, _y1, _x2, _y2);

            _graphics.FillEllipse(blackBrush, (int)pendulum.X0 - 5, (int)pendulum.Y0 - _ballRadius0, _ballRadius0 * 2, _ballRadius0 * 2);
            _graphics.FillEllipse(redBrush, _x1 - _ballRadius1, _y1 - _ballRadius1, _ballRadius1 * 2, _ballRadius1 * 2);

            _graphics.FillEllipse(redDarkBrush,
                _x1 - ((_ballRadius1 * 2)/ 3), _y1 - ((_ballRadius1 * 2) / 3),
                ((_ballRadius1 * 4) / 3), ((_ballRadius1 * 4) / 3));
            
            _graphics.FillEllipse(blueBrush, _x2 - _ballRadius2, _y2 - _ballRadius2, _ballRadius2 * 2, _ballRadius2 * 2);

            _p2 = new Point(_x2, _y2);

            return bitmap;
        }       
    }
}
