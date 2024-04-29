using TPW_Project.Model;

namespace TPW_Project.ViewModel
{
    public class BallViewModel : ViewModelBase
    {
        private readonly Ball ball;
        private readonly int canvasWidth = 370;
        private readonly int canvasHeight = 370;

        public BallViewModel(Ball ball)
        {
            this.ball = ball;
        }

        public int CoordinateX
        {
            get => ball.CoordinateX;
            set
            {
                ball.CoordinateX = value;
                OnPropertyChanged(nameof(CoordinateX));
            }
        }

        public int CoordinateY
        {
            get => ball.CoordinateY;
            set
            {
                ball.CoordinateY = value;
                OnPropertyChanged(nameof(CoordinateY));
            }
        }

        public int SpeedX
        {
            get => ball.SpeedX;
            set => ball.SpeedX = value;
        }

        public int SpeedY
        {
            get => ball.SpeedY;
            set => ball.SpeedY = value;
        }

        public void Move()
        {
            // Update ball position
            CoordinateX += SpeedX;
            CoordinateY += SpeedY;

            // Check if the ball hits the canvas boundaries
            if (CoordinateX < 0 || CoordinateX > canvasWidth)
            {
                // If the ball hits the left or right boundary, reverse its horizontal direction
                SpeedX = -SpeedX;
            }
            if (CoordinateY < 0 || CoordinateY > canvasHeight)
            {
                // If the ball hits the top or bottom boundary, reverse its vertical direction
                SpeedY = -SpeedY;
            }
        }
    }

}

