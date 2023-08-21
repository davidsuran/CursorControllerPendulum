using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CursorControllerPendulum
{
    public partial class PendulumForm : Form
    {
        private PendulumImage _image;
        private DoublePendulum _pendulum;
        private int _mouseMovementTriggerInSeconds = 3 * 60;
        private int _mouseMovementStopAfterTriggerInSeconds = 10;
        private Stopwatch _mouseStopwatch;
        private Random _random;
        private bool _isLeftMouseButtonPressed;
        private bool _atLeastOneClickConfirmed;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);

        public PendulumForm()
        {
            InitializeComponent();

            _pendulum = new DoublePendulum(PendulumPictureBox.Width, PendulumPictureBox.Height);

            PreventScreenSaver(true);

            _mouseStopwatch = new Stopwatch();
            _random = new Random();

            PendulumPictureBox.MouseClick += new MouseEventHandler(ChangeBackground);
        }

        private void ChangeBackground(object? sender, MouseEventArgs e)
        {
            _image.NextBackgroundColor();
        }

        private void PendulumFormLoad(object sender, EventArgs e)
        {
            _image = new PendulumImage(PendulumPictureBox.Width, PendulumPictureBox.Height, _pendulum.BallRadius1, _pendulum.BallRadius2);
            PendulumTimer.Interval = 16;
        }

        private void PendulumTimerTick(object sender, EventArgs e)
        {
            _pendulum.Update();
            PendulumPictureBox.Image = _image.GetImage(_pendulum);
        }

        private void MouseTimerTick(object sender, EventArgs e)
        {
            if (!_mouseStopwatch.IsRunning)
            {
                _mouseStopwatch.Restart();
            }

            if (_isLeftMouseButtonPressed)
            {
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            }

            if (_mouseStopwatch.Elapsed.TotalSeconds >= _mouseMovementTriggerInSeconds
                && _mouseStopwatch.Elapsed.TotalSeconds < _mouseMovementTriggerInSeconds + _mouseMovementStopAfterTriggerInSeconds)
            {
                Point ScreenPoint = PendulumPictureBox.PointToScreen(_image.P2);
                MouseOperations.SetCursorPosition(ScreenPoint);

                if (_random.Next(1, 300) >= 295)
                {
                    _isLeftMouseButtonPressed = true;
                    _atLeastOneClickConfirmed = true;
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);

                }
            }
            else if (_mouseStopwatch.Elapsed.TotalSeconds >= _mouseMovementTriggerInSeconds + _mouseMovementStopAfterTriggerInSeconds)
            {
                if (_atLeastOneClickConfirmed)
                {
                    _atLeastOneClickConfirmed = false;
                }
                else
                {
                    _isLeftMouseButtonPressed = true;
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                }

                _mouseStopwatch.Stop();
            }
        }

        private void PreventScreenSaver(bool sw)
        {
            if (sw)
            {
                SetThreadExecutionState(ExecutionState.ES_DISPLAY_REQUIRED | ExecutionState.ES_CONTINUOUS);
            }
            else
            {
                SetThreadExecutionState(ExecutionState.ES_CONTINUOUS);
            }
        }
    }
}
