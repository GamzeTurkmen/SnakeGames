using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };
    public class attribute:Settings
    {
        public static int Score { get; set; }
        public static int Points { get; set; }
        public static bool GameOver { get; set; }
        public static Direction direction { get; set; }

        public attribute()
        {
            Score = 0;
            Points = 100;
            GameOver = false;
            direction = Direction.Down;
        }

    }
}
