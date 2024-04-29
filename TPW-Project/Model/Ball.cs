using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPW_Project.Model
{
    public class Ball
    {

        public int CoordinateX {  get; set; }
        public int CoordinateY {  get; set; }
        public int SpeedX {  get; set; }
        public int SpeedY {  get; set; }
      
        public Ball(int x, int y, int speedX, int speedY)
        {
            CoordinateX = x;
            CoordinateY = y;
            SpeedX = speedX;
            SpeedY = speedY;
        }
    }
}
