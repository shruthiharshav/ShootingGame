using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace ShootingGame.Views
{
    public partial class MainWindow : Window
    {
        private readonly string[] _actions = { "Ladda", "Blocka", "Skjuta" };
        private readonly Random _random = new();

        private Player _player = new("Spelare");
        private Player _computer = new("Computer");
        private bool _isGameOver;

        private TextBlock? _playerBulletsText;
        private TextBlock? _computerBulletsText;
        private TextBlock? _resultText;

        public MainWindow()
        {
            InitializeComponent();
            UpdateBullets();
            SetResult("Choose an action to start.");
        }

        private void OnLaddaClicked(object? sender, RoutedEventArgs e) => PlayRound("Ladda");

        private void OnBlockaClicked(object? sender, RoutedEventArgs e) => PlayRound("Blocka");

        private void OnSkjutaClicked(object? sender, RoutedEventArgs e) => PlayRound("Skjuta");

        private void OnResetClicked(object? sender, RoutedEventArgs e)
        {
            _player = new Player("Spelare");
            _computer = new Player("Computer");
            _isGameOver = false;
            UpdateBullets();
            SetResult("Game reset. Choose an action to start.");
        }

        private void PlayRound(string playerAction)
        {
            if (_isGameOver)
            {
                return;
            }

            string computerAction = _actions[_random.Next(_actions.Length)];
            string message = ResolveRound(playerAction, computerAction);
            UpdateBullets();
            SetResult(message);

            if (_isGameOver)
            {
                return;
            }

            if (_player.HasShotgun)
            {
                _isGameOver = true;
                SetResult("Player shotgun! You win!");
            }
            else if (_computer.HasShotgun)
            {
                _isGameOver = true;
                SetResult("Computer shotgun! Computer wins!");
            }
        }

        private string ResolveRound(string playerAction, string computerAction)
        {
            if (playerAction == "Ladda" && computerAction == "Ladda")
            {
                _player.Load();
                _computer.Load();
                return "Both loaded bullets.";
            }

            if (playerAction == "Ladda" && computerAction == "Blocka")
            {
                _player.Load();
                _computer.Block();
                return "You loaded. Computer blocked.";
            }

            if (playerAction == "Blocka" && computerAction == "Blocka")
            {
                _player.Block();
                _computer.Block();
                return "Both blocked.";
            }

            if (playerAction == "Skjuta" && computerAction == "Blocka")
            {
                if (_player.TryShoot())
                {
                    _computer.Block();
                    return "You shot. Computer blocked.";
                }

                _computer.Block();
                return "You tried to shoot without bullets.";
            }

            if (playerAction == "Skjuta" && computerAction == "Skjuta")
            {
                bool playerShot = _player.TryShoot();
                bool computerShot = _computer.TryShoot();

                if (playerShot && computerShot)
                {
                    return "Both shot.";
                }

                if (playerShot && !computerShot)
                {
                    return "You shot. Computer had no bullets.";
                }

                if (!playerShot && computerShot)
                {
                    return "Computer shot. You had no bullets.";
                }

                return "Nobody had bullets.";
            }

            if (playerAction == "Skjuta" && computerAction == "Ladda")
            {
                if (_player.TryShoot())
                {
                    _isGameOver = true;
                    return "You shot while computer loaded. You win!";
                }

                _computer.Load();
                return "You tried to shoot without bullets.";
            }

            if (playerAction == "Ladda" && computerAction == "Skjuta")
            {
                _player.Load();

                if (_computer.TryShoot())
                {
                    _isGameOver = true;
                    return "Computer shot while you loaded. Computer wins!";
                }

                _computer.Load();
                return "Computer had no bullets and had to load.";
            }

            if (playerAction == "Blocka" && computerAction == "Ladda")
            {
                _player.Block();
                _computer.Load();
                return "You blocked while computer loaded.";
            }

            return "Round finished.";
        }

        private void UpdateBullets()
        {
            if (_playerBulletsText != null)
            {
                _playerBulletsText.Text = $"Player bullets: {_player.Bullets}";
            }

            if (_computerBulletsText != null)
            {
                _computerBulletsText.Text = $"Computer bullets: {_computer.Bullets}";
            }
        }

        private void SetResult(string message)
        {
            if (_resultText != null)
            {
                _resultText.Text = message;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _playerBulletsText = this.FindControl<TextBlock>("PlayerBulletsText");
            _computerBulletsText = this.FindControl<TextBlock>("ComputerBulletsText");
            _resultText = this.FindControl<TextBlock>("ResultText");
        }
    }
}