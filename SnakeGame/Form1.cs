using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private List<Circle>Snake = new List<Circle>();


        private Circle food = new Circle();

        public Form1()
        {
            InitializeComponent();
            //Set settings to default
            new Settings();
             new attribute();

            //Set game speed and start timer
            gameTimer.Interval = 1000/ attribute.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            //Start a new game
            StartGame();

        }

       

        private void StartGame()
        {
            labelGame.Visible = false;
            //set settings to default
          //  new Settings();
            new attribute();

            //Create new player object
            Snake.Clear();
            Circle head = new Circle();
            head.X = 10;
            head.Y = 5;
            Snake.Add(head);

            label2.Text = attribute.Score.ToString();
            GenerateFood();


        }
        private void GenerateFood()
        {
            int maxXPos = pbBox.Size.Width/Settings.Width;
            int maxYPos = pbBox.Size.Height/Settings.Height;

            Random random = new Random();
            food = new Circle();
            food.X = random.Next(0, maxXPos);
            food.Y = random.Next(0, maxYPos);
        }
        private void  UpdateScreen(object sender, EventArgs e)
        {
            //Chech for Game Over
            if (attribute.GameOver == true)
            {
                //check if enter is pressed
                if (Input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if (Input.KeyPressed(Keys.Right) && attribute.direction != Direction.Left)
                    attribute.direction = Direction.Right;
                else if (Input.KeyPressed(Keys.Left) && attribute.direction != Direction.Right)
                    attribute.direction = Direction.Left;
                else if (Input.KeyPressed(Keys.Up) && attribute.direction != Direction.Down)
                    attribute.direction = Direction.Up;
                else if (Input.KeyPressed(Keys.Down) && attribute.direction != Direction.Up)
                    attribute.direction = Direction.Down;

                 MovePlayer();



            }

            pbBox.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                  
            
        }

        private void pbBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if (!attribute.GameOver)
            {
                //set colour  of snake 
                Brush snakeColour;

                //Draw snake
                for (int i = 0; i <Snake.Count; i++)
                {
                    if (i == 0)
                    
                        snakeColour = Brushes.Black; //Draw head kafası

                    else
                        snakeColour = Brushes.Green; //rest of body vucudun geri kalanı

                    //Draw snake
                    canvas.FillEllipse(snakeColour,
                        new Rectangle(Snake[i].X * Settings.Width,
                                      Snake[i].Y * Settings.Height,
                                      Settings.Width, Settings.Height));


                    //Draw food
                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Settings.Width,
                                      food.Y * Settings.Height,
                                      Settings.Width, Settings.Height));



                }
            }
            else
            {

                labelGame.Text = "Game Over!";
                labelGame.Visible = true;
            }
        }

        private void MovePlayer()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                //Move head
                if (i == 0)
                {
                    switch (attribute.direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                    }
                    //Get maximum x and y pos
                    int maxXPos = pbBox.Width / Settings.Width;
                    int maxYPos = pbBox.Height / Settings.Height;

                    //detect collission with game borders

                    if (Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X >= maxXPos || Snake[i].Y >= maxYPos)
                    {
                        Die();
                    }

                    //detection collision with body
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        Eat();
                    }
                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }
        private void Eat()
        {
            //Add to circle to body
            Circle food = new Circle();
            food.X = Snake[Snake.Count - 1].X;
            food.Y = Snake[Snake.Count - 1].Y;
            Snake.Add(food);
            //Update Score
            attribute.Score += attribute.Points;
            label2.Text = attribute.Score.ToString();
            GenerateFood();

        }
        private void Die()
        {
            attribute.GameOver = true;
        }
        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void lblGameOver_Click(object sender, EventArgs e)
        {

        }

        private void labelGame_Click(object sender, EventArgs e)
        {

        }
    }
}
