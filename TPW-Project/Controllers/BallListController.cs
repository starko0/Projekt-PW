using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using TPW_Project.Model;
using TPW_Project.ViewModel;
using TPW_Project.ViewModelLogic.SpacePartitioning;

namespace TPW_Project.ViewModelLogic
{
    public class BallListController : ViewController
    {
        private ObservableCollection<BallController> balls;
        public ObservableCollection<BallController> Balls
        {
            get { return balls; }
            set
            {
                balls = value;
                OnPropertyChanged(nameof(Balls));
            }
        }

        public BallListController()
        {
            Balls = new ObservableCollection<BallController>();
        }

        public void GenerateBalls(int amount)
        {
            Random random = new Random();

            for (int i = 0; i < amount; i++)
            {
                double r, x, y, speedX, speedY;

                do
                {
                    r = 60 + random.NextDouble() * 15;
                    x = random.NextDouble() * (400 - r); 
                    y = random.NextDouble() * (400 - r); 
                    speedX = -6 + random.NextDouble() * 12; 
                    speedY = -6 + random.NextDouble() * 12; 

                } while (CheckCollisionDuringGeneretingBalls(x, y, r));

                Ball ball = new Ball(i, x, y, speedX, speedY, r / 10, r);
                BallController ballViewModel = new BallController(ball);
                Balls.Add(ballViewModel);
            }
        }

        public bool CheckCollisionDuringGeneretingBalls(double x, double y, double r)
        {
            foreach (var existingBall in Balls)
            {
                double distance = Math.Sqrt(Math.Pow((existingBall.CoordinateX + existingBall.Radius/2) - (x + r/2), 2) 
                    + Math.Pow((existingBall.CoordinateY + existingBall.Radius/2) - (y + r/2), 2));
                if (distance <= (r + existingBall.Radius)/2)
                {
                    return true; // Kolizja
                }
            }
            return false; // Brak kolizji
        }

        public List<List<int>> CheckCollisionBetweenBalls()
        {
            Partitioner partitioner = new Partitioner(Balls);
            Queue<Node> queue = new Queue<Node>();
            queue = partitioner.Divide(4);
            var list = queue.ToList();

            List<List<int>> listOflists = new List<List<int>>();

            foreach (var node in list)
            {
                List<int> pojemnik = new List<int>();

                if (node.Balls.Any() && node.Balls.Count() > 1)
                {
                    foreach (var ball in node.Balls)
                    {
                        for (int i = 0; i < Balls.Count; i++)
                        {
                            if (ball.CoordinateX == Balls[i].CoordinateX && ball.CoordinateY == Balls[i].CoordinateY)
                            {
                                pojemnik.Add(i);
                            }
                        }
                    }


                    for (int i = 0; i < pojemnik.Count; i++)
                    {
                        for (int j = i + 1; j < pojemnik.Count; j++)
                        {
                            double distance = Math.Sqrt(Math.Pow((Balls[pojemnik[i]].CoordinateX + Balls[pojemnik[i]].Radius / 2) - 
                                (Balls[pojemnik[j]].CoordinateX + Balls[pojemnik[j]].Radius / 2), 2) +
                                Math.Pow((Balls[pojemnik[i]].CoordinateY + Balls[pojemnik[i]].Radius / 2) -
                                (Balls[pojemnik[j]].CoordinateY + Balls[pojemnik[j]].Radius / 2), 2));

                            if (distance < (Balls[pojemnik[i]].Radius + Balls[pojemnik[j]].Radius) / 2)
                            {
                                List<int> para = new List<int>();

                                para.Add(Math.Min(pojemnik[i], pojemnik[j]));
                                para.Add(Math.Max(pojemnik[i], pojemnik[j]));


                                bool paraExists = false;
                                foreach (var existingPara in listOflists)
                                {
                                    if (existingPara.SequenceEqual(para))
                                    {
                                        paraExists = true;
                                        break;
                                    }
                                }

                                if (!paraExists)
                                {
                                    listOflists.Add(para);
                                }
                            }
                        }
                    }
                }
            }

            return listOflists;
        }

       
        public void reactionOnCollision(List<int> index)
        {
            // Obliczanie różnicy położeń środków kul
            double dx = (Balls[index[1]].CoordinateX + Balls[index[1]].Radius / 2) - (Balls[index[0]].CoordinateX + Balls[index[0]].Radius / 2);
            double dy = (Balls[index[1]].CoordinateY + Balls[index[1]].Radius / 2) - (Balls[index[0]].CoordinateY + Balls[index[0]].Radius / 2);


            // Obliczanie odległości między środkami kul
            double distance = Math.Sqrt(dx * dx + dy * dy);



            // Obliczanie kąta kolizji
            double angle = Math.Atan2(dy, dx);

            // Obliczanie nowych prędkości po odbiciu
            double v1x = Balls[index[0]].SpeedX * Math.Cos(angle) + Balls[index[0]].SpeedY * Math.Sin(angle);
            double v1y = Balls[index[0]].SpeedY * Math.Cos(angle) - Balls[index[0]].SpeedX * Math.Sin(angle);
            double v2x = Balls[index[1]].SpeedX * Math.Cos(angle) + Balls[index[1]].SpeedY * Math.Sin(angle);
            double v2y = Balls[index[1]].SpeedY * Math.Cos(angle) - Balls[index[1]].SpeedX * Math.Sin(angle);

            // Zamiana prędkości po odbiciu
            double newV1x = ((Balls[index[0]].Weight - Balls[index[1]].Weight) * v1x + 2 * Balls[index[1]].Weight * v2x) / (Balls[index[0]].Weight + Balls[index[1]].Weight);
            double newV2x = ((Balls[index[1]].Weight - Balls[index[0]].Weight) * v2x + 2 * Balls[index[0]].Weight * v1x) / (Balls[index[0]].Weight + Balls[index[1]].Weight);

            // Aktualizacja prędkości po odbiciu
            Balls[index[0]].SpeedX = newV1x * Math.Cos(angle) - v1y * Math.Sin(angle);
            Balls[index[0]].SpeedY = v1y * Math.Cos(angle) + newV1x * Math.Sin(angle);
            Balls[index[1]].SpeedX = newV2x * Math.Cos(angle) - v2y * Math.Sin(angle);
            Balls[index[1]].SpeedY = v2y * Math.Cos(angle) + newV2x * Math.Sin(angle);


            if (distance < (Balls[index[0]].Radius + Balls[index[1]].Radius) / 2)
            {
                double pen_depth = ((Balls[index[0]].Radius + Balls[index[1]].Radius) / 2) - distance;

                double half_pen_depth = pen_depth / 2;
                double half_pen_depthX = half_pen_depth * Math.Cos(angle);
                double half_pen_depthY = half_pen_depth * Math.Sin (angle);

                Balls[index[0]].CoordinateX -= half_pen_depthX;
                Balls[index[0]].CoordinateY -= half_pen_depthY;
                Balls[index[1]].CoordinateX += half_pen_depthX;
                Balls[index[1]].CoordinateY += half_pen_depthY;

                // Dodaj kontrolę kolizji z granicą po przesunięciu kul
                if (Balls[index[0]].CheckCollisionBetweenBallAndBorder())
                {
                    Balls[index[0]].CoordinateX += Balls[index[0]].SpeedX;
                    Balls[index[0]].CoordinateY += Balls[index[0]].SpeedY;
                }
                else if (Balls[index[1]].CheckCollisionBetweenBallAndBorder())
                {
                    Balls[index[1]].CoordinateX += Balls[index[1]].SpeedX;
                    Balls[index[1]].CoordinateY += Balls[index[1]].SpeedY;
                }
            }
        }
        

        public void reactionOnCollisionBetter(List<int> index)
        {
            // Obliczanie różnicy położeń środków kul
            double dx = (Balls[index[0]].CoordinateX + Balls[index[0]].Radius / 2) - (Balls[index[1]].CoordinateX + Balls[index[1]].Radius / 2);
            double dy = (Balls[index[0]].CoordinateY + Balls[index[0]].Radius / 2) - (Balls[index[1]].CoordinateY + Balls[index[1]].Radius / 2);

            // Obliczanie odległości między środkami kul
            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance < (Balls[index[0]].Radius + Balls[index[1]].Radius) / 2)
            {
                double pen_depth = ((Balls[index[0]].Radius + Balls[index[1]].Radius) / 2) - distance;

                // Przesunięcie kul z powrotem o głębokość penetracji
                double penetrationX = (dx / distance) * (pen_depth / 2);
                double penetrationY = (dy / distance) * (pen_depth / 2);

                Balls[index[0]].CoordinateX += penetrationX;
                Balls[index[0]].CoordinateY += penetrationY;
                Balls[index[1]].CoordinateX -= penetrationX;
                Balls[index[1]].CoordinateY -= penetrationY;

                // Dodaj kontrolę kolizji z granicą po przesunięciu kul
                if (Balls[index[0]].CheckCollisionBetweenBallAndBorder())
                {
                    Balls[index[0]].CoordinateX += Balls[index[0]].SpeedX;
                    Balls[index[0]].CoordinateY += Balls[index[0]].SpeedY;
                }
                else if (Balls[index[1]].CheckCollisionBetweenBallAndBorder())
                {
                    Balls[index[1]].CoordinateX += Balls[index[1]].SpeedX;
                    Balls[index[1]].CoordinateY += Balls[index[1]].SpeedY;
                }
            }

            // Obliczanie kąta kolizji
            double angle = Math.Atan2(dy, dx);

            // Obliczanie nowych prędkości po odbiciu
            double v1x = Balls[index[0]].SpeedX * Math.Cos(angle) + Balls[index[0]].SpeedY * Math.Sin(angle);
            double v1y = Balls[index[0]].SpeedY * Math.Cos(angle) - Balls[index[0]].SpeedX * Math.Sin(angle);
            double v2x = Balls[index[1]].SpeedX * Math.Cos(angle) + Balls[index[1]].SpeedY * Math.Sin(angle);
            double v2y = Balls[index[1]].SpeedY * Math.Cos(angle) - Balls[index[1]].SpeedX * Math.Sin(angle);

            // Zamiana prędkości po odbiciu
            double newV1x = ((Balls[index[0]].Weight - Balls[index[1]].Weight) * v1x + 2 * Balls[index[1]].Weight * v2x) / (Balls[index[0]].Weight + Balls[index[1]].Weight);
            double newV2x = ((Balls[index[1]].Weight - Balls[index[0]].Weight) * v2x + 2 * Balls[index[0]].Weight * v1x) / (Balls[index[0]].Weight + Balls[index[1]].Weight);

            // Aktualizacja prędkości po odbiciu
            Balls[index[0]].SpeedX = newV1x * Math.Cos(angle) - v1y * Math.Sin(angle);
            Balls[index[0]].SpeedY = v1y * Math.Cos(angle) + newV1x * Math.Sin(angle);
            Balls[index[1]].SpeedX = newV2x * Math.Cos(angle) - v2y * Math.Sin(angle);
            Balls[index[1]].SpeedY = v2y * Math.Cos(angle) + newV2x * Math.Sin(angle);
            
        }


    }
}


