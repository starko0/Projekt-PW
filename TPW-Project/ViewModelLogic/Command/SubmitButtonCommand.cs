using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TPW_Project.ViewModel.Command
{
    class SubmitButtonCommand : BasicCommand
    {
        private readonly SimulationViewModel simulationViewModel;
        private object @object;

        public SubmitButtonCommand(SimulationViewModel simulationViewModel)
        {
            this.simulationViewModel = simulationViewModel;
            this.simulationViewModel.PropertyChanged += OnViewModelPropertyChanged;

        }

        public SubmitButtonCommand(object @object)
        {
            this.@object = @object;
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

        private bool hasExecuted = false; // Pole do śledzenia, czy metoda Execute została już wykonana

        public override void Execute(object? parameter)
        {
            if (!hasExecuted) // Sprawdzamy, czy metoda Execute nie została jeszcze wykonana
            {
                if (int.TryParse(simulationViewModel.SubmitInputText, out int amount))
                {
                    simulationViewModel.BallList.GenerateBalls(amount);
                    hasExecuted = true; // Ustawiamy flagę na true po wykonaniu metody Execute
                }
                else
                {
                    // Tutaj będzie komunikat o błędzie
                }
            }
        }
    }
}
