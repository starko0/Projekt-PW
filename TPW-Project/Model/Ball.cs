using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TPW_Project.Model
{
    public class Ball
    {
        public double Weight { get; set; }
        public double Radius { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public double SpeedX { get; set; }
        public double SpeedY { get; set; }

        public String Color { get; set; } = "Red";

        public Ball(double x, double y, double speedX, double speedY, double weight, double radius)
        {
            CoordinateX = x;
            CoordinateY = y;
            SpeedX = speedX;
            SpeedY = speedY;
            Weight = weight;
            Radius = radius;
        }
    }

}
