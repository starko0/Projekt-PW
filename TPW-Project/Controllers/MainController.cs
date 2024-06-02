using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPW_Project.ViewModel
{
    public class MainController : ViewController
    {
         public ViewController CurrentViewModel { get; } 

        public MainController() { 
            CurrentViewModel = new SimualtionController();
        }
    }
}
