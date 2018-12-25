using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //tetris variables  
        Tetris Game = new Tetris();

        //music variables
        MP3Player M = new MP3Player();
        int iVolume = 0;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int WM_APPCOMMAND = 0x319;
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
        IntPtr wParam, IntPtr lParam);
        public Form1()
        {
            InitializeComponent();
        }

        public class MP3Player
        {
            private string _command;
            private bool isOpen;
            [DllImport("winmm.dll")]

            private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

            public void Close()
            {
                _command = "close MediaFile";
                mciSendString(_command, null, 0, IntPtr.Zero);
                isOpen = false;
            }

            public void Open(string sFileName)
            {
                _command = "open \"" + sFileName + "\" type mpegvideo alias MediaFile";
                mciSendString(_command, null, 0, IntPtr.Zero);
                isOpen = true;
            }

            public void Play(bool loop)
            {
                if (isOpen)
                {
                    _command = "play MediaFile";
                    if (loop)
                        _command += " REPEAT";
                    mciSendString(_command, null, 0, IntPtr.Zero);
                }
            }
        }

        public class Tetris
        {
            //you lose when this adds up to 19; it checks if there is atleast one 1x1 block at each row
            protected int Lose = 0; 
            //finds out what block it is, there are 7 total
            protected int Block;
            //represents the block after the one that is falling down, so it can be shown in the top left
            protected int NextBlock;
            //score
            protected int Score = 0;
            //High Score
            protected int HighScore = 0;
            //determines how large the block is to fit an a square array that lets it rotate
            protected int ArraySize;
            // x - coordinate of the moving block (takes the top left)
            protected int x;
            //y - coordinate of the moving block (takes the top left)
            protected int y;
            protected Color Colour;
            //moving block array
            protected int[,] BlockArray = new int[10, 20];
            //the board array which includes the fallen blocks
            protected int[,] BoardArray = new int[10, 20];

            //tetris constructor
            public Tetris()
            {
                Colour = Color.White;
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 20; j++)
                        BlockArray[i, j] = 0;
            }

            //getters
            public int GetLose()
            {
                return Lose;
            }

            public int GetBlockArray(int x, int y)
            {
                return BlockArray[x, y];
            }
            public int GetBoardArray(int x, int y)
            {
                return BoardArray[x, y];
            }
            public Color GetColour()
            {
                return Colour;
            }
            public int GetScore()
            {
                return Score;
            }
            public int GetHighScore()
            {
                return HighScore;
            }

            //setters
            public void SetBlock(int n)//sets the next block
            {
                //line
                int i;
                if (n == 0)
                {
                    ArraySize = 4;
                    x = 4;
                    y = 0;
                    for (i = 0; i < 4; i++)
                        BlockArray[4 + i, 0] = 1;
                    Colour = Color.Aqua;
                }
                //L left
                else if (n == 1)
                {
                    ArraySize = 3;
                    x = 3;
                    y = 0;
                    BlockArray[3, 1] = 1;
                    BlockArray[4, 1] = 1;
                    BlockArray[5, 1] = 1;
                    BlockArray[3, 0] = 1;
                    Colour = Color.MediumBlue;
                }
                //L right
                else if (n == 2)
                {
                    ArraySize = 3;
                    x = 3;
                    y = 0;
                    BlockArray[3, 1] = 1;
                    BlockArray[4, 1] = 1;
                    BlockArray[5, 1] = 1;
                    BlockArray[5, 0] = 1;
                    Colour = Color.FromArgb(255, 153, 51);
                }
                //square 2x2
                else if (n == 3)
                {
                    ArraySize = 2;
                    x = 3;
                    y = 0;
                    BlockArray[3, 0] = 1;
                    BlockArray[4, 0] = 1;
                    BlockArray[3, 1] = 1;
                    BlockArray[4, 1] = 1;
                    Colour = Color.Yellow;
                }
                //Z 
                else if (n == 4)
                {
                    ArraySize = 3;
                    x = 3;
                    y = 0;
                    BlockArray[3, 0] = 1;
                    BlockArray[4, 0] = 1;
                    BlockArray[5, 1] = 1;
                    BlockArray[4, 1] = 1;
                    Colour = Color.Lime;
                }
                //S 
                else if (n == 5)
                {
                    ArraySize = 3;
                    x = 3;
                    y = 0;
                    BlockArray[4, 0] = 1;
                    BlockArray[5, 0] = 1;
                    BlockArray[3, 1] = 1;
                    BlockArray[4, 1] = 1;
                    Colour = Color.Red;
                }
                //T 
                else if (n == 6)
                {
                    ArraySize = 3;
                    x = 3;
                    y = 0;
                    BlockArray[3, 1] = 1;
                    BlockArray[4, 1] = 1;
                    BlockArray[5, 1] = 1;
                    BlockArray[4, 0] = 1;
                    Colour = Color.FromArgb(102, 0, 204);
                }
            }
            public void SetNextBlock(int n)//sets next block to drop
            {
                NextBlock = n;
            }
            public void SetLose()//resets lose to 0
            {
                Lose = 0;
            }
            public void SetScore()//resets score to 0
            {
                Score = 0;
            }
            public void SetHighScore(int n)//updates highscore
            {
                HighScore = n;
            }

            //copy block array to board array
            public void BlockToBoardArray()//when a block lies down, it transfers the block to the board
            {
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 20; j++)
                    {
                        if (BlockArray[i, j] == 1 && BoardArray[i, j] == 0)
                            BoardArray[i, j] = BlockArray[i, j];
                    }
            }

            //Checks if you lose
            public void CheckLose()//adds one to "Lose" if there is atleast one or more blocks in a row
            {
                int i, j;
                for (j = 0; j < 20; j++)
                {
                    for (i = 0; i < 10; i++)
                    {
                        if (BoardArray[i, j] == 1)
                        {
                            Lose++;
                            break;
                        }
                    }
                }
            }

            //checks if a row is full, then clears it
            public void ClearRow()
            {
                int i, j, i1, j2, icheck = 0;
                for (j = 0; j < 20; j++)
                {
                    for (i = 0; i < 10; i++)
                    {
                        if (BoardArray[i, j] == 1)
                            icheck++;
                    }

                    if (icheck == 10)//checks if there is 10 in a row
                    {

                        for (j2 = j; j2 > 0; j2--)
                            for (i1 = 0; i1 < 10; i1++)
                            {
                                BoardArray[i1, j2] = BoardArray[i1, j2 - 1];
                            }
                        Score++;//score is increased
                    }
                    icheck = 0;//resets to find other full rows
                }
            }

            //clear block array
            public void ClearBlockArray()//clears the block array so a new block(moving) can be used
            {
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 20; j++)
                        BlockArray[i, j] = 0;
            }

            //clear board array
            public void ClearBoardArray()//clears board for new game
            {
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 20; j++)
                        BoardArray[i, j] = 0;
            }

            //move block left
            public void MoveBlockLeft()
            {
                int[,] iTempBlock = new int[10, 20];
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 20; j++)
                    {
                        if (BlockArray[0, j] == 1)
                            return;

                        if (BlockArray[i, j] == 1 && BoardArray[i - 1, j] == 1)
                            return;

                        if (i != 0)
                            iTempBlock[i - 1, j] = BlockArray[i, j];
                    }

                x--;

                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 20; j++)
                        BlockArray[i, j] = iTempBlock[i, j];
            }

            //move block right
            public void MoveBlockRight()
            {
                int[,] iTempBlock = new int[10, 20];
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 20; j++)
                    {
                        if (BlockArray[9, j] == 1)
                            return;

                        if (BlockArray[i, j] == 1 && BoardArray[i + 1, j] == 1)
                            return;

                        if (i < 9)
                            iTempBlock[i + 1, j] = BlockArray[i, j];

                    }
                x++;
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 20; j++)
                    {
                        BlockArray[i, j] = iTempBlock[i, j];
                    }
            }

            //move block down
            public void MoveBlockDown()
            {
                Lose = 0;
                int i, j;
                Random rnd = new Random();
                int[,] iTempBlock = new int[10, 20];      
                for (i = 0; i < 10; i++)
                {
                    for (j = 0; j < 20; j++)
                    {
                        if (BlockArray[i, j] == 1 && BoardArray[i, j + 1] == 1)
                        {//if there is a block below
                            BlockToBoardArray();//transfers block to board
                            ClearBlockArray();//clears block
                            SetBlock(Block);//sets a new block
                            ClearRow();//checks if a row is full
                            CheckLose();//checks if the board
                            NextBlock = rnd.Next(0, 7);//sets the next block
                            return;
                        }
                        if (BlockArray[i, 19] == 1)
                        {//if the bottom is below
                            BlockToBoardArray();//transfers block to board
                            ClearBlockArray();//clears block
                            SetBlock(Block);// sets the new block
                            ClearRow();// checks if a row is gull
                            CheckLose();//checks if the board is full
                            NextBlock = rnd.Next(0, 7);//sets the next block
                            return;
                        }

                        if (j > 0)
                            iTempBlock[i, j] = BlockArray[i, j - 1];
                    }
                }

                y++;              
                Block = NextBlock;

                for (i = 0; i < 10; i++)
                    for (j = 0; j < 20; j++)
                    {
                        BlockArray[i, j] = iTempBlock[i, j];
                    }
            }

            //rotate
            public void RotateBlock()
            {
                int i, j, tmp, n;
                n = ArraySize;

                if (n == 2)//no point rotating a square
                    return;

                if (x < 0 || x > 10 - n || y > 20 - n)//checks for edge of board
                    return;

                //this is for the 1X4 block; the way it would rotate in a 4x4 block is undesireable (didn't want it to move left and right then go up and down to rotate)
                //thus the manual rotation
                if (n == 4)
                {
                    if (BlockArray[x + 1, y] == 1)
                    {
                        for (i = 1; i < 4; i++)
                        {
                            if (BoardArray[x, y + i] == 1)
                                return;
                        }
                        for (i = 1; i < 4; i++)
                        {
                            BlockArray[x, y + i] = 1;
                            BlockArray[x + i, y] = 0;
                        }
                        return;
                    }
                    if (BlockArray[x, y + 1] == 1)
                    {
                        for (i = 1; i < 4; i++)
                        {
                            if (BoardArray[x + i, y] == 1)
                                return;
                        }
                        for (i = 1; i < 4; i++)
                        {
                            BlockArray[x, y + i] = 0;
                            BlockArray[x + i, y] = 1;
                        }
                        return;
                    }
                }

                for (i = 0; i < n; i++)
                    for (j = 0; j < n; j++)
                        if (BoardArray[x + i, y + j] == 1)
                            return;

                //the rotate array code was taken and modified from http://stackoverflow.com/questions/42519/how-do-you-rotate-a-two-dimensional-array
                //↓↓↓↓↓This is the original code↓↓↓↓↓
                //int a[4][4];
                //int n=4;
                //int tmp;
                //   for (int i=0; i<n/2; i++){
                //for (int j=i; j<n-i-1; j++){
                //tmp=a[i][j];
                //a[i][j]=a[j][n-i-1];
                //a[j][n-i-1]=a[n-i-1][n-j-1];
                //a[n-i-1][n-j-1]=a[n-j-1][i];
                //a[n-j-1][i]=tmp;
                //    }
                //}
                //↑↑↑↑↑This is the original code↑↑↑↑

                //↓↓↓↓modified code is below↓↓↓↓↓            
                for (i = 0; i < n / 2; i++)
                    for (j = i; j < n - i - 1; j++)
                    {
                        tmp = BlockArray[i + x, j + y];
                        BlockArray[i + x, j + y] = BlockArray[j + x, n - i - 1 + y];
                        BlockArray[j + x, n - i - 1 + y] = BlockArray[n - i - 1 + x, n - j - 1 + y];
                        BlockArray[n - i - 1 + x, n - j - 1 + y] = BlockArray[n - j - 1 + x, i + y];
                        BlockArray[n - j - 1 + x, i + y] = tmp;
                    }
            }

            //draws board and falling shape
            public void DrawTetris(Graphics g)
            {
                int i, j;
                Pen pencil = new Pen(Color.FromArgb(90, 90, 90), 1);
                Pen pencil2 = new Pen(Color.LightSkyBlue, 1);
                SolidBrush brush = new SolidBrush(Colour);

                //draws grid
                for (i = 0; i <= 500; i = i + 25)
                {
                    g.DrawLine(pencil2, 0, i, 251, i);
                    if (i >= 0 && i <= 500)
                    {
                        g.DrawLine(pencil2, i, 0, i, 500);
                    }
                }


                for (i = 0; i < 10; i++)
                    for (j = 0; j < 20; j++)
                    {
                        //draws moving Block
                        if (BlockArray[i, j] == 1)
                        {
                            g.FillRectangle(brush, i * 25, j * 25, 25, 25);
                            g.DrawRectangle(pencil, i * 25 + 5, j * 25 + 5, 15, 15);
                            g.DrawRectangle(pencil, i * 25, j * 25, 25, 25);
                        }
                        //draws resting blocks
                        if (BoardArray[i, j] == 1)
                        {
                            g.FillRectangle(brush, i * 25, j * 25, 25, 25);
                            g.DrawRectangle(pencil, i * 25 + 5, j * 25 + 5, 15, 15);
                            g.DrawRectangle(pencil, i * 25, j * 25, 25, 25);
                        }
                    }
            }

            //draws the preview for the next block
            public void DrawNextBlock(Graphics g)
            {
                int i, j, n;
                int[,] iTempArray = new int[4, 4];

                n = NextBlock;

                Pen pencil = new Pen(Color.Black, 1);
                SolidBrush brush = new SolidBrush(Color.White);
                if (n == 0)
                {
                    for (i = 0; i < 4; i++)
                        iTempArray[i, 0] = 1;
                }
                //L left
                else if (n == 1)
                {
                    iTempArray[0, 1] = 1;
                    iTempArray[1, 1] = 1;
                    iTempArray[2, 1] = 1;
                    iTempArray[0, 0] = 1;
                }
                //L right
                else if (n == 2)
                {
                    iTempArray[0, 1] = 1;
                    iTempArray[1, 1] = 1;
                    iTempArray[2, 1] = 1;
                    iTempArray[2, 0] = 1;
                }
                //square 2x2
                else if (n == 3)
                {
                    iTempArray[0, 0] = 1;
                    iTempArray[1, 0] = 1;
                    iTempArray[1, 1] = 1;
                    iTempArray[0, 1] = 1;
                }
                //Z 
                else if (n == 4)
                {
                    iTempArray[0, 0] = 1;
                    iTempArray[1, 0] = 1;
                    iTempArray[1, 1] = 1;
                    iTempArray[2, 1] = 1;
                }
                //S 
                else if (n == 5)
                {
                    iTempArray[0, 1] = 1;
                    iTempArray[1, 1] = 1;
                    iTempArray[1, 0] = 1;
                    iTempArray[2, 0] = 1;
                }
                //T 
                else if (n == 6)
                {
                    iTempArray[0, 1] = 1;
                    iTempArray[1, 1] = 1;
                    iTempArray[2, 1] = 1;
                    iTempArray[1, 0] = 1;
                }

                for (i = 0; i < 4; i++)
                    for (j = 0; j < 4; j++)
                    {
                        if (iTempArray[i, j] == 1)
                        {
                            g.FillRectangle(brush, (i) * 25, j * 25, 25, 25);
                            g.DrawRectangle(pencil, (i) * 25 + 5, j * 25 + 5, 15, 15);
                            g.DrawRectangle(pencil, (i) * 25, j * 25, 25, 25);
                        }
                    }
            }

        }

            //keyboard controls: rotate, ↓, →, ↑.
            private void Form1_KeyPress(object sender, KeyPressEventArgs e)
            {
                //tetris controls
                int iLose, iScore;
                iScore = Game.GetScore();
                iLose = Game.GetLose();
                //w -rotate
                if (e.KeyChar == 119)
                {
                    Game.RotateBlock();
                    pBox1.Invalidate();
                    pBox2.Invalidate();
                }
                //a -left
                if (e.KeyChar == 97)
                {
                    Game.MoveBlockLeft();
                    pBox1.Invalidate();
                    pBox2.Invalidate();
                }
                //s -down
                if (e.KeyChar == 115)
                {
                    if (iScore > Game.GetHighScore())
                    {
                        Game.SetHighScore(iScore);
                        lblHighScore.Text = Game.GetHighScore().ToString();
                    }
                    Game.MoveBlockDown();
                    lblScore.Text = iScore.ToString();
                    pBox1.Invalidate();
                    pBox2.Invalidate();
                }
                //d - right
                else if (e.KeyChar == 100)
                {
                    Game.MoveBlockRight();
                    pBox1.Invalidate();
                    pBox2.Invalidate();
                }

                //music controls
                if (e.KeyChar == 49)
                {
                    M.Close();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Music\Tetris Remix.mp3");
                    M.Open(path);
                    M.Play(true);
                }
                if (e.KeyChar == 50)
                {
                    M.Close();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Music\Mount and Blade.mp3");
                    M.Open(path);
                    M.Play(true);
                }
                if (e.KeyChar == 51)
                {
                    M.Close();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Music\Rules of Nature.mp3");
                    M.Open(path);
                    M.Play(true);
                }
                if (e.KeyChar == 52)
                {
                    M.Close();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Music\Smooth Criminal.mp3");
                    M.Open(path);
                    M.Play(true);
                }
                if (e.KeyChar == 53)
                {
                    M.Close();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Music\This is Our Time.mp3");
                    M.Open(path);
                    M.Play(true);
                }
                if (e.KeyChar == 54)
                {
                    M.Close();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Music\Ding Dong Song.mp3");
                    M.Open(path);
                    M.Play(true);
                }
                if (e.KeyChar == 55)
                {
                    M.Close();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Music\Gaben.mp3");
                    M.Open(path);
                    M.Play(true);
                }
                if (e.KeyChar == 56)
                {
                    M.Close();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Music\Final Countdown.mp3");
                    M.Open(path);
                    M.Play(true);
                }
                if (e.KeyChar == 57)
                {
                    M.Close();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Music\Yee.mp3");
                    M.Open(path);
                    M.Play(true);
                }
                if (e.KeyChar == 48)
                {
                    M.Close();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Music\Tetris.mp3");
                    M.Open(path);
                    M.Play(true);
                }
            }

            //timer moves block down once per tick
            private void Timer_Tick(object sender, EventArgs e)
            {
                int iLose, iScore;
                iScore = Game.GetScore();
                iLose = Game.GetLose();
                if (iLose >= 19)
                {
                    Game.SetLose();
                    Timer.Stop();
                    Game.ClearBlockArray();
                    Game.ClearBoardArray();
                    MessageBox.Show("Game Over");
                    iLose = 0;
                    Game.SetScore();
                    return;
                }
                Game.MoveBlockDown();
                if (iScore > Game.GetHighScore())
                {
                    Game.SetHighScore(iScore);
                    lblHighScore.Text = Game.GetHighScore().ToString();
                }
                lblScore.Text = iScore.ToString();
                pBox2.Invalidate();
                pBox1.Invalidate();
            }
            //turns vol up
            private void btnVolUp_Click(object sender, EventArgs e)
            {
                SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
                (IntPtr)APPCOMMAND_VOLUME_UP);
                iVolume++;
                if (iVolume == 6)
                {
                    M.Close();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Music\Metal.mp3");
                    M.Open(path);
                    M.Play(true);
                    iVolume++;
                }
            }
            //turns vol up (even though it says down)
            private void btnVolDown_Click(object sender, EventArgs e)
            {
                SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
                (IntPtr)APPCOMMAND_VOLUME_UP);
                iVolume++;
                if (iVolume == 6)
                {
                    M.Close();
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Music\Metal.mp3");
                    M.Open(path);
                    M.Play(true);
                    iVolume++;
                }
            }
            //draws the falling block and the fallen peices
            private void pBox1_Paint(object sender, PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                Game.DrawTetris(g);
            }
            //draw preview of next block
            private void pBox2_Paint(object sender, PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                Game.DrawNextBlock(g);
            }
            //starts the game
            private void btnStart_Click(object sender, EventArgs e)
            {
                Random rnd = new Random();
                int iRandom;
                Timer.Start();
                Game.ClearBoardArray();
                Game.ClearBlockArray();
                Game.SetScore();
                iRandom = rnd.Next(0, 7);
                Game.SetNextBlock(iRandom);
                Game.SetBlock(rnd.Next(0, 7));
                pBox1.Invalidate();
                pBox2.Invalidate();
            }
        }
    }