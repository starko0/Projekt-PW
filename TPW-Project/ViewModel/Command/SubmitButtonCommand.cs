using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPW_Project.ViewModel.Command
{
    class SubmitButtonCommand : BasicCommand
    {
        private readonly SimulationViewModel simulationViewModel;
        public SubmitButtonCommand(SimulationViewModel simulationViewModel)
        {
            this.simulationViewModel = simulationViewModel;
            this.simulationViewModel.PropertyChanged += OnViewModelPropertyChanged;

        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SimulationViewModel.SubmitInputText)) 
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(simulationViewModel.SubmitInputText) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (int.TryParse(simulationViewModel.SubmitInputText, out int amount))
            {
                simulationViewModel.GenerateBalls(amount);
            }
            else
            {
                // Tutaj bedzie komunikat o błedzie
            }
        }
    }
}
