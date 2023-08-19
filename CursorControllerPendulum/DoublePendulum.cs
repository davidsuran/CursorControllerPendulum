using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursorControllerPendulum
{
    internal class DoublePendulum
    {
        public readonly double Radius1 = 105;
        public readonly double Radius2 = 175;
        public double Angle1 = 0;
        public double Angle2 = 0;
        public double AngleVelocity1 = 0;
        public double AngleVelocity2 = 0;
        static readonly double G = 9.8 / (60);

        public int BallRadius1 => (int)MassToRadius(_mass1);
        public int BallRadius2 => (int)MassToRadius(_mass2);

        private readonly double _mass1 = 100;
        private readonly double _mass2 = 10;

        static double Px2 = -1;
        static double Py2 = -1;
        public readonly double Cx, Cy;
        public double X1 = 0;
        public double Y1 = 0;
        public double X2 = 0;
        public double Y2 = 0;

        private double _frameWidth;
        private double _frameHeight;

        public DoublePendulum(double width, double height)
        {
            //double width = 450;
            Angle1 = Math.PI / 2;
            Angle2 = Math.PI / 1;
            Cx = width / 2;
            Cy = height / 4;

            _frameWidth = width;
            _frameHeight = height;
        }

        public void Update()
        {
            // https://www.myphysicslab.com/pendulum/double-pendulum-en.html

            double num1 = -G * (2 * _mass1 + _mass2) * Math.Sin(Angle1);
            double num2 = -_mass2 * G * Math.Sin(Angle1 - 2 * Angle2);
            double num3 = -2 * Math.Sin(Angle1 - Angle2) * _mass2;
            double num4 = AngleVelocity2 * AngleVelocity2 * Radius2 + AngleVelocity1 * AngleVelocity1 * Radius1 * Math.Cos(Angle1 - Angle2);
            double den = Radius1 * (2 * _mass1 + _mass2 - _mass2 * Math.Cos(2 * Angle1 - 2 * Angle2));
            double angleAcceleration1 = (num1 + num2 + num3 * num4) / den;

            num1 = 2 * Math.Sin(Angle1 - Angle2);
            num2 = (AngleVelocity1 * AngleVelocity1 * Radius1 * (_mass1 + _mass2));
            num3 = G * (_mass1 + _mass2) * Math.Cos(Angle1);
            num4 = AngleVelocity2 * AngleVelocity2 * Radius2 * _mass2 * Math.Cos(Angle1 - Angle2);
            den = Radius2 * (2 * _mass1 + _mass2 - _mass2 * Math.Cos(2 * Angle1 - 2 * Angle2));
            double angleAcceleration2 = (num1 * (num2 + num3 + num4)) / den;

            X1 = Radius1 * Math.Sin(Angle1);
            Y1 = Radius1 * Math.Cos(Angle1);

            X2 = X1 + Radius2 * Math.Sin(Angle2);
            Y2 = Y1 + Radius2 * Math.Cos(Angle2);

            AngleVelocity1 += angleAcceleration1;
            AngleVelocity2 += angleAcceleration2;
            Angle1 += AngleVelocity1;
            Angle2 += AngleVelocity2;

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
