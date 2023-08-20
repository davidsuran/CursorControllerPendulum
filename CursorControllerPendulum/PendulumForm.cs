namespace CursorControllerPendulum
{
    public partial class PendulumForm : Form
    {
        private PendulumImage _image;
        private DoublePendulum _pendulum;

        public PendulumForm()
        {
            InitializeComponent();

            _pendulum = new DoublePendulum(PendulumPictureBox.Width, PendulumPictureBox.Height);
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

        int i = 0;
        private void MouseTimerTick(object sender, EventArgs e)
        {
            i++;
            if (i >= 10000)
            {
                return;
            }

            Point ScreenPoint = PendulumPictureBox.PointToScreen(_image.P2);
            MouseOperations.SetCursorPosition(ScreenPoint);
        }

    }
}