using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TPW_Project.Controllers;
using TPW_Project.Model;
using TPW_Project.ViewModel.Command;
using TPW_Project.ViewModelLogic;

namespace TPW_Project.ViewModel
{
    public class SimualtionController : ViewController
    {
        private DispatcherTimer timer;

        public SimualtionController()
        {
            StartButton = new StartButtonCommand(this);
            SubmitButton = new SubmitButtonCommand(this);

            BallList = new BallListController();
            logController = new LoggingController(BallList);

            programStatusText = string.Empty;
            submitInputText = string.Empty;
            startButtonText = "Start";
            submitBasicTextVisibility = Visibility.Visible;
        }

        public bool IsSimulationRunning { get; set; }

        public async Task MoveBallsAsync()
        {
            while (IsSimulationRunning)
            {
                await Task.Run(() =>
                {
                    Parallel.ForEach(BallList.Balls, ball =>
                    {
                            ball.Move();
                    });
                });

                await Task.Run(() =>
                {
                    logController.LogBallList(BallList);
                });

                await Task.Run(() =>
                {
                    var result = BallList.CheckCollisionBetweenBalls();

                    Parallel.ForEach(result, innerList =>
                    {
                        lock (innerList)
                        {
                            BallList.reactionOnCollision(innerList);
                        }
                    });
                });

                await Task.Delay(25); // Oczekiwanie asynchroniczne na kolejną iterację
            }
        }
        public BallListController BallList { get; }

        public LoggingController logController;

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

        //Widoczność tekstu w tle przy wprowadzaniu liczby kulek
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

        public void StartMovingBalls()
        {
            timer.Start();
        }

        public void StopMovingBalls()
        {
            if (timer != null && timer.IsEnabled)
            {
                timer.Stop();
            }
        }
    }
}