using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPW_Project.View;

namespace TPW_Project.ViewModel.Command
{
    public class StartButtonCommand : BasicCommand
    {
        private readonly SimulationViewModel simulationViewModel;
        public StartButtonCommand(SimulationViewModel simulationViewModel) 
        {
            this.simulationViewModel = simulationViewModel;
        }
        public override void Execute(object? parameter)
        {
                if(simulationViewModel.StartButtonText == "Start")
                {
                    simulationViewModel.ProgramStatusText = "Simulation started";
                    simulationViewModel.StartButtonText = "Stop";
                }
                else
                {
                    simulationViewModel.ProgramStatusText = "Simulation stopped";
                    simulationViewModel.StartButtonText = "Start";
                }
            }
        
    }
}
