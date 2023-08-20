using System.Runtime.InteropServices;

namespace CursorControllerPendulum
{
    public partial class PendulumForm : Form
    {
        private PendulumImage _image;
        private DoublePendulum _pendulum;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);

        public PendulumForm()
        {
            InitializeComponent();

            _pendulum = new DoublePendulum(PendulumPictureBox.Width, PendulumPictureBox.Height);

            PreventScreenSaver(true);
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
            // This doesn't prevent win from going to sleep, I disabled the timer
            Point ScreenPoint = PendulumPictureBox.PointToScreen(_image.P2);
            MouseOperations.SetCursorPosition(ScreenPoint);
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
