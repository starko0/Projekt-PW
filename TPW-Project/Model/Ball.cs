using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPW_Project.Model
{
    public class Ball
    {

        private int coordinateX;

        public int CoordinateX
        {
            get { return coordinateX; }
            set { coordinateX = value; }
        }

        private int coordinateY;

        public int CoordinateY
        {
            get { return coordinateY; }
            set { coordinateY = value; }
        }

        private int speedX;

        public int SpeedX
        {
            get { return speedX; }
            set { speedX = value; }
        }

        private int speedY;

        public int SpeedY
        {
            get { return speedY; }
            set { speedY = value; }
        }


        public Ball(int x, int y, int speedX, int speedY)
        {
            this.coordinateX = x;
            this.coordinateY = y;
            this.speedX = speedX;
            this.speedY = speedY;
        }
    }
}
