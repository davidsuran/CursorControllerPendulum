namespace CursorControllerPendulum
{
    public partial class Form1 : Form
    {
        private PendulumImage _image;
        private DoublePendulum _pendulum;
        private double _timeStep;

        public Form1()
        {
            InitializeComponent();

            _pendulum = new DoublePendulum(pictureBox1.Width, pictureBox1.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pImage = new PendulumImage(pictureBox1.Width, pictureBox1.Height);
            //p = new PendulumParameters();
            //L1Scroll.Value = int.Parse(L1TextBox.Text);
            //L2Scroll.Value = int.Parse(L2TextBox.Text);
            //θ1Scroll.Value = int.Parse(θ1TextBox.Text);
            //θ2Scroll.Value = int.Parse(θ2TextBox.Text);
            //double interval = timer.Interval / 1000d;
            //timeStep = interval / iterationsPerFrame;
            //recording = new Recording(timeStep);

            _image = new PendulumImage(pictureBox1.Width, pictureBox1.Height, _pendulum.BallRadius1, _pendulum.BallRadius2);
            PendulumTimer.Interval = 16;
            //double interval = 16;//timer.Interval / 1000d;
            //timeStep = interval / iterationsPerFrame;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            _pendulum.Update();

            pictureBox1.Image = _image.GetImage(_pendulum);
        }

        private void MouseTimer_Tick(object sender, EventArgs e)
        {

        }
    }
}