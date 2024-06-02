using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPW_Project.ViewModel;

namespace TPW_Project.ViewModelLogic.SpacePartitioning
{
    public class Node
    {
        public List<BallController> Balls { get; set; }

        public int Depth { get; set; }

        public Node(ObservableCollection<BallController> balls, int depth = 0)
        {
            this.Balls = balls.ToList();
            this.Depth = depth;
        }


    }
}
