using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TPW_Project.ViewModel;

namespace TPW_Project.ViewModelLogic.SpacePartitioning
{
    public class Partitioner
    {
        public Node Root { get; set; }


        public Partitioner(ObservableCollection<BallViewModel> balls)
        {
            Root = new Node(balls);
        }

        public double SortBallsByCoordinateX(Node root)
        {
            if (root == null || root.Balls == null || root.Balls.Count == 0)
                return 0;

            root.Balls.Sort((ball1, ball2) => ball1.CoordinateX.CompareTo(ball2.CoordinateX));

            double sumX = root.Balls.Sum(ball => ball.CoordinateX);
            double averageX = sumX / root.Balls.Count;

            return averageX;
        }

        public double SortBallsByCoordinateY(Node root)
        {
            if (root == null || root.Balls == null || root.Balls.Count == 0)
                return 0;

            root.Balls.Sort((ball1, ball2) => ball1.CoordinateY.CompareTo(ball2.CoordinateY));

            double sumY = root.Balls.Sum(ball => ball.CoordinateY);
            double averageY = sumY / root.Balls.Count;

            return averageY;
        }



        private bool chechIfFinished(Queue<Node> queue, int depth)
        {
            double amountOfElemnts = Math.Pow(2, depth);

            if (queue.Count != amountOfElemnts)
            {
                return true;
            }
            else { return false; }
        }

        public Queue<Node> Divide(int depth)
        {
            if (Root == null || Root.Balls == null || Root.Balls.Count == 0)
                return null;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(Root);

            Node root;
            Node leftNode;
            Node rightNode;
            //root = Root;


            while (chechIfFinished(queue, depth))
            {
                root = queue.Dequeue();

                int sortByX = root.Depth % 2;

                double avarage;

                if (sortByX == 0)
                {
                    avarage = SortBallsByCoordinateX(root);
                    Console.WriteLine("X");
                }
                else
                {
                    avarage = SortBallsByCoordinateY(root);
                    Console.WriteLine("Y");
                }
                Console.WriteLine(avarage + "Mediana");


                leftNode = new Node(new ObservableCollection<BallViewModel>(), ++root.Depth);
                rightNode = new Node(new ObservableCollection<BallViewModel>(), root.Depth);

                foreach (var ball in root.Balls)
                {
                    if (sortByX == 0)
                    {
                        if (ball.CoordinateX + ball.Radius < avarage)
                        {
                            leftNode.Balls.Add(ball);
                        }
                        else if (ball.CoordinateX > avarage)
                        {
                            rightNode.Balls.Add(ball);
                        }

                        else
                        {
                            leftNode.Balls.Add(ball);
                            rightNode.Balls.Add(ball);
                        }

                    }
                    else
                    {
                        if (ball.CoordinateY + ball.Radius < avarage)
                        {

                            leftNode.Balls.Add(ball);
                        }
                        else if (ball.CoordinateY > avarage)
                        {
                            rightNode.Balls.Add(ball);
                        }

                        else
                        {
                            leftNode.Balls.Add(ball);
                            rightNode.Balls.Add(ball);
                        }
                    }
                }


                foreach (var ball in leftNode.Balls)
                {
                    Console.Write("(" + ball.CoordinateX + " " + ball.CoordinateY + ")  ");
                }
                Console.WriteLine("Lewa");
                foreach (var ball in rightNode.Balls)
                {
                    Console.Write("(" + ball.CoordinateX + " " + ball.CoordinateY + ")  ");
                }
                Console.WriteLine("Prawa");




                Console.WriteLine("Przerwa");

                queue.Enqueue(leftNode);
                queue.Enqueue(rightNode);
            }


            return queue;
        }

    }
}
