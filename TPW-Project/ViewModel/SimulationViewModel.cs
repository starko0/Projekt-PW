using System.Windows;
using System.Windows.Input;
using TPW_Project.ViewModel.Command;

namespace TPW_Project.ViewModel
{
    public class SimulationViewModel : ViewModelBase
    {
        public SimulationViewModel()
        {
           

            StartButton = new StartButtonCommand(this);
            SubmitButton = new SubmitButtonCommand(this);

            programStatusText = string.Empty;
            submitInputText = string.Empty;
            startButtonText = "Start";
            submitBasicTextVisibility = Visibility.Visible;
        }

        //Komenda definiująca co ma robić guzik
        public ICommand StartButton { get; }

        //komenda guzika submit
        public ICommand SubmitButton { get; }

        // Tekst na guziki Start/Stop
        private string startButtonText;
        public string StartButtonText
        {
            get { return startButtonText; }
            set
            {
                startButtonText = value;
                OnPropertyChanged(nameof(StartButtonText));
            }
        }

        //Tekst wyświetlany na dole w zależności od klinięcia guzika
        private string programStatusText;
        public string ProgramStatusText
        {
            get { return programStatusText; }
            set
            {
                programStatusText = value;
                OnPropertyChanged(nameof(ProgramStatusText));
            }
        }

        //Wprowadzana liczba kulek do zatwierdzenia
        private string submitInputText;
        public string SubmitInputText
        {
            get { return submitInputText; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    submitInputText = string.Empty;
                    OnPropertyChanged(nameof(SubmitInputText));
                    SubmitBasicTextVisibility = Visibility.Visible;
                    return;
                }

                if (int.TryParse(value, out int number) && number >= 1 && number <= 10)
                {
                    submitInputText = value;
                    OnPropertyChanged(nameof(SubmitInputText));
                    SubmitBasicTextVisibility = Visibility.Collapsed;
                }
                else
                {
                    submitInputText = string.Empty;
                    OnPropertyChanged(nameof(SubmitInputText));
                    SubmitBasicTextVisibility = Visibility.Visible;
                }
            }
        }


        private Visibility submitBasicTextVisibility;
        public Visibility SubmitBasicTextVisibility
        {
            get { return submitBasicTextVisibility; }
            set
            {
                submitBasicTextVisibility = value;
                OnPropertyChanged(nameof(SubmitBasicTextVisibility));
            }
        }

    }
}
