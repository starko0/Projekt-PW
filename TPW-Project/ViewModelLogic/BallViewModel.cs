using TPW_Project.Model;

namespace TPW_Project.ViewModel
{
    public class BallViewModel : ViewModelBase
    {
        private readonly Ball ball;
        private readonly int canvasWidth = 400;
        private readonly int canvasHeight = 400;

        public BallViewModel(Ball ball)
        {
            this.ball = ball;
        }

        public double CoordinateX
        {
            get => ball.CoordinateX;
            set
            {
                ball.CoordinateX = value;
                OnPropertyChanged(nameof(CoordinateX));
            }
        }

        public double CoordinateY
        {
            get => ball.CoordinateY;
            set
            {
                ball.CoordinateY = value;
                OnPropertyChanged(nameof(CoordinateY));
            }
        }

        public String Color
        {
            get => ball.Color;
            set
            {
                ball.Color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public double SpeedX
        {
            get => ball.SpeedX;
            set => ball.SpeedX = value;
        }

        public double SpeedY
        {
            get => ball.SpeedY;
            set => ball.SpeedY = value;
        }

        public double Weight
        {
            get => ball.Weight;
            set => ball.Weight = value;
        }

        public double Radius
        {
            get => ball.Radius;
            set => ball.Radius = value;
        }


        public void Move()
        {
            if (!CheckCollisionBetweenBallAndBorder())
            {
                CoordinateX += SpeedX;
                CoordinateY += SpeedY;
            }
        }

        public bool CheckCollisionBetweenBallAndBorder()
        {

            // Continuous collision detection

            bool CollisionDetected = false;

            // Kolizja z lewą krawędzią
            if (CoordinateX + SpeedX < 0)
            {
                double Factor = Math.Abs(CoordinateX - 0) / Math.Abs(SpeedX);
                CoordinateX += SpeedX * Factor;
                SpeedX = -SpeedX;
                CollisionDetected = true;
            }
            // Kolizja z prawą krawędzią
            else if (CoordinateX + SpeedX > (canvasWidth - Radius))
            {
                double Factor = Math.Abs((canvasWidth - Radius) - CoordinateX) / Math.Abs(SpeedX);
                CoordinateX += SpeedX * Factor;
                SpeedX = -SpeedX;
                CollisionDetected = true;
            }

            // Kolizja z górną krawędzią
            if (CoordinateY + SpeedY < 0)
            {
                double Factor = Math.Abs(CoordinateY - 0) / Math.Abs(SpeedY);
                CoordinateY += SpeedY * Factor;
                SpeedY = -SpeedY;
                CollisionDetected = true;
            }
            // Kolizja z dolną krawędzią
            else if (CoordinateY + SpeedY > (canvasHeight - Radius))
            {
                double Factor = Math.Abs((canvasHeight - Radius) - CoordinateY) / Math.Abs(SpeedY);
                CoordinateY += SpeedY * Factor;
                SpeedY = -SpeedY; // Odwrócenie kierunku ruchu
                CollisionDetected = true;
            }

            return CollisionDetected;
        }



    }

}

