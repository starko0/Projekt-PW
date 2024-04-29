using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TPW_Project.Model;
using TPW_Project.ViewModel.Command;

namespace TPW_Project.ViewModel
{
    public class SimulationViewModel : ViewModelBase
    {
        private DispatcherTimer timer;
        public SimulationViewModel()
        {
            StartButton = new StartButtonCommand(this);
            SubmitButton = new SubmitButtonCommand(this);

            balls = new ObservableCollection<BallViewModel>();

            programStatusText = string.Empty;
            submitInputText = string.Empty;
            startButtonText = "Start";
            submitBasicTextVisibility = Visibility.Visible;
            timer = new DispatcherTimer();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += (sender, e) =>
            {
                foreach (var ball in Balls)
                {
                    ball.Move();
                }
            };
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

        //Lista kulek
        private ObservableCollection<BallViewModel> balls;
        public ObservableCollection<BallViewModel> Balls
        {
            get { return balls; }
            set
            {
                balls = value;
                OnPropertyChanged(nameof(Balls));
            }
        }

        //Tworzenie kulek
        public void GenerateBalls(int amount)
        {
            Random random = new Random();

            for (int i = 0; i < amount; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(0, 370);
                    y = random.Next(0, 370);
                } while (CheckCollision(x, y));

                int speedX = random.Next(-5, 6);
                int speedY = random.Next(-5, 6);

                Ball ball = new Ball(x, y, speedX, speedY);
                BallViewModel ballViewModel = new BallViewModel(ball);
                Balls.Add(ballViewModel);
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


        public bool CheckCollision(int x, int y)
        {
            foreach (var existingBall in Balls)
            {
                double distance = Math.Sqrt(Math.Pow(existingBall.CoordinateX - x, 2) + Math.Pow(existingBall.CoordinateY - y, 2));
                if (distance < 30)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
