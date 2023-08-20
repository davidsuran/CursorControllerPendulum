namespace CursorControllerPendulum
{
    internal class DoublePendulum
    {
        public readonly double Radius1 = 105;
        public readonly double Radius2 = 175;

        private readonly double _mass1 = 100;
        private readonly double _mass2 = 10;

        public double Angle1 => _angle1;
        private double _angle1 = 0;

        public double Angle2 => _angle2;
        private double _angle2 = 0;

        public double AngleVelocity1 => _angleVelocity1;
        private double _angleVelocity1 = 0;

        public double AngleVelocity2 => _angleVelocity2;
        private double _angleVelocity2 = 0;

        static readonly double G = 9.8 / (60);

        public int BallRadius1 => (int)MassToRadius(_mass1);
        public int BallRadius2 => (int)MassToRadius(_mass2);


        static double Px2 = -1;
        static double Py2 = -1;

        public readonly double X0;
        public readonly double Y0;

        public double X1 => _x1;
        private double _x1;

        public double Y1 => _y1;
        private double _y1;

        public double X2 => _x2;
        private double _x2;

        public double Y2 => _y2;
        private double _y2;

        private readonly double _frameWidth;
        private readonly double _frameHeight;

        public DoublePendulum(double width, double height)
        {
            _angle1 = Math.PI / 2;
            _angle2 = Math.PI / 1;
            X0 = width / 2;
            Y0 = height / 4;

            _frameWidth = width;
            _frameHeight = height;
        }

        public void Update()
        {
            // https://www.myphysicslab.com/pendulum/double-pendulum-en.html

            double num1 = -G * (2 * _mass1 + _mass2) * Math.Sin(_angle1);
            double num2 = -_mass2 * G * Math.Sin(_angle1 - 2 * _angle2);
            double num3 = -2 * Math.Sin(_angle1 - _angle2) * _mass2;
            double num4 = AngleVelocity2 * AngleVelocity2 * Radius2 + AngleVelocity1 * AngleVelocity1 * Radius1 * Math.Cos(_angle1 - _angle2);
            double den = Radius1 * (2 * _mass1 + _mass2 - _mass2 * Math.Cos(2 * _angle1 - 2 * _angle2));
            double angleAcceleration1 = (num1 + num2 + num3 * num4) / den;

            num1 = 2 * Math.Sin(_angle1 - _angle2);
            num2 = (AngleVelocity1 * AngleVelocity1 * Radius1 * (_mass1 + _mass2));
            num3 = G * (_mass1 + _mass2) * Math.Cos(_angle1);
            num4 = AngleVelocity2 * AngleVelocity2 * Radius2 * _mass2 * Math.Cos(_angle1 - _angle2);
            den = Radius2 * (2 * _mass1 + _mass2 - _mass2 * Math.Cos(2 * _angle1 - 2 * _angle2));
            double angleAcceleration2 = (num1 * (num2 + num3 + num4)) / den;

            _x1 = Radius1 * Math.Sin(_angle1);
            _y1 = Radius1 * Math.Cos(_angle1);

            _x2 = X1 + Radius2 * Math.Sin(_angle2);
            _y2 = Y1 + Radius2 * Math.Cos(_angle2);

            _angleVelocity1 += angleAcceleration1;
            _angleVelocity2 += angleAcceleration2;
            _angle1 += _angleVelocity1;
            _angle2 += _angleVelocity2;

            Px2 = X2;
            Py2 = Y2;
        }

        private double MassToRadius(double x)
        {
            if (_frameWidth > _frameHeight)
            {
                return Math.Sqrt(Normalize(x)) * (_frameWidth / 10);
            }

            return Math.Sqrt(Normalize(x)) * (_frameHeight / 10);
        }

        private double Normalize(double x)
        {
            double xMax, xMin;

            if (_frameWidth > _frameHeight)
            {
                xMax = _frameWidth;
                xMin = 0;
            }
            else
            {
                xMax = _frameHeight;
                xMin = 0;
            }

            return (x - xMin) / (xMax - xMin);
        }
    }
}
