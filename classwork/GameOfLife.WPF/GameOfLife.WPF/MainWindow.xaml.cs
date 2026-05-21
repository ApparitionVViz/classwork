using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using GameOfLide.Core;
namespace GameOfLife.WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _game;
        private Border[,] _cells;
        private DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();

            _game = new Game();
            _cells = new Border[_game.Rows, _game.Cols];
            CreateGameGrid();
            UpdateVisuals();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };
            _timer.Tick += TimerTick;
        }
        private void CreateGameGrid()
        {
            gameGrid.Children.Clear();
            gameGrid.Rows = _game.Rows;
            gameGrid.Columns = _game.Cols;
            for (int i = 0; i < _game.Rows; i++)
            {
                for (int j = 0; j < _game.Cols; j++)
                {
                    Border border = new Border
                    {
                        Background = Brushes.Black,
                        BorderBrush = Brushes.DimGray,
                        BorderThickness = new Thickness(0.3)
                    };

                    int iCaptured = i, jCaptured = j;
                    border.MouseLeftButtonDown += (sender, e) => CellClick(iCaptured, jCaptured);
                    _cells[i, j] = border;
                    gameGrid.Children.Add(border);
                }
            }
            
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _game.NextGeneration();
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            for (int i = 0; i < _game.Rows; i++)
            {
                for(int j = 0; j < _game.Cols; j++)
                {
                    if (_game.Grid[i, j])
                    {
                        _cells[i, j].Background = Brushes.Pink;
                    }
                    else
                    {
                        _cells[i, j].Background = Brushes.Black;
                    }
                }
            }
        }
        private void CellClick(int row, int col)
        {
            if (_timer.IsEnabled) return;

            _game.ToggleCell(row, col);
            UpdateVisuals();
        }

        private void StartClick(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }
        private void StopClick(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }
        private void ResetClick(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _game.Clean();
            UpdateVisuals();
        }
    }
}
