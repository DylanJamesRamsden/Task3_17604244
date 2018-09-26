using System; //Dylan James Ramsden 17604244
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADE6112_POE
{
    class Map
    {
        Random r = new Random();
        public Unit[] UnitsOnMap = new Unit[12]; //Holds the position of units on the map
        public PictureBox[,] guiMAP = new PictureBox[20, 20]; //Holds 20 images to portray the game map

        //All components public because they are used in another class
        public TextBox Info; 
        public Label GameTimeDisplay;
        public Timer gameTimer;
        public Button Start;
        public Button Pause;
        public TextBox NewGame;
        public Button newGameButton;

        Form activeForm;

        public Map(Form active, Timer Gametime) //Map constructor. Generates a new map and displays it, as well as GUI components
        {
            GenerateNew();
            CreateMap(active);
            UserComponentSetup(active, Gametime);
            gameTimer = Gametime;
            activeForm = active;
        }

        public void GenerateNew() //This method assigns the UnitsOnMap array with new Units
        {
            for (int Ranged = 0; Ranged < 6; Ranged++) //Generates ranged troops for each team
            {
               int rX = r.Next(0, 20); //Assigns the Unit with a random X position
               int ry = r.Next(0, 20); //ASsigns the Unit with a random Y position

               if (Ranged % 2 == 0 || Ranged == 0) //These if stamenets are used to determine which teams turn it is for a new unit
               {
                    UnitsOnMap[Ranged] = new RangedUnit(rX, ry, "Blue", "Blue_Arrow");
               }
               else
               {
                    UnitsOnMap[Ranged] = new RangedUnit(rX, ry, "Red", "Red_Arrow");
               }
            }

            for (int Melee = 6; Melee < 12; Melee++) //Generates melee troops for each team
            {
                int rX = r.Next(0, 20);
                int ry = r.Next(0, 20);

                if (Melee % 2 == 0)
                {
                    UnitsOnMap[Melee] = new MeleeUnit(rX, ry, "Blue", "Blue_Sword");
                }
                else
                {
                    UnitsOnMap[Melee] = new MeleeUnit(rX, ry, "Red", "Red_Sword");
                }
            }
        } 

        public void MoveUnit(Unit Soldier, int X,int Y, int pX, int pY) //This method is used to give the Unit a new X and Y position as well as change its position on the guiMAP
        //Parameters: X holds the new x position, Y holds the new Y position, pX holds the old X position and pY holds the old Y position
        //pX and pY are needed to changed the units old position back to grass on the guiMAP
        {
                if (Soldier.GetType() == typeof(MeleeUnit))
                {
                    MeleeUnit convertUnit = (MeleeUnit)Soldier;
                    convertUnit.newPos(X, Y); //ASsigns the converted unit with a new X and Y position

                    //if (Y < 20 && Y >= 0 && X < 20 && X >= 0)
                    //{
                    if (convertUnit.Faction == "Red") //These if statements are used to place the unit on the guiMAP based on their new position, the way they are displayed is based on their team and type
                    {
                        guiMAP[X, Y].Image = Properties.Resources.Red_Sword;
                        guiMAP[pX, pY].Image = Properties.Resources.Grass; //The units old position is visually removed from the guiMAP based on their old position
                    }
                    else
                    {
                        guiMAP[X, Y].Image = Properties.Resources.Blue_Sword;
                        guiMAP[pX, pY].Image = Properties.Resources.Grass;
                    }
                    //}
                }
                else
                {
                    RangedUnit convertUnit = (RangedUnit)Soldier;
                    convertUnit.XPos = X;
                    convertUnit.YPos = Y;

                    //if (Y < 20 && Y >= 0 && X < 20 && X >= 0)
                    //{
                    if (convertUnit.Faction == "Red")
                    {
                        guiMAP[X, Y].Image = Properties.Resources.Red_Arrow;
                        guiMAP[pX, pY].Image = Properties.Resources.Grass;
                    }
                    else
                    {
                        guiMAP[X, Y].Image = Properties.Resources.Blue_Arrow;
                        guiMAP[pX, pY].Image = Properties.Resources.Grass;
                    }
                    //}
                }
        }

        public void updatePos(Unit Soldier, int X, int Y, int pX,int pY) //This method is used to give values to the MoveUnit method
        {
            MoveUnit(Soldier, X, Y, pX, pY);
        }

        public void CreateMap(Form active) //This method is used to add picture boxs to and vusally create the guiMAP
        {
            for (int x = 0; x < 20; x++) //The map is 20x20, this is why there is a loop for the X values and Y values
            {
                for (int y = 0; y < 20; y++)
                {
                    if (x == 0 && y == 0) //This if statement checks if the loop is at 0,0 to assign the first picturebox in the guiMAP with values
                        //This position is handled differently because its location is calculated differently to the others
                    {
                        PictureBox pb = new PictureBox();
                        pb.Size = new System.Drawing.Size(30, 30);
                        pb.Location = new System.Drawing.Point(0, 0);
                        pb.Image = Properties.Resources.Grass; //Added pictures to properties, easier to call and more efficent
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        pb.Visible = true;
                        guiMAP[x, y] = pb;

                        active.Controls.Add(pb); //Adds the component to the active form

                        guiMAP[x, y].Click += new EventHandler(this.picture_Click);
                    }
                    else
                    {
                        PictureBox pb = new PictureBox();
                        pb.Size = new System.Drawing.Size(30, 30);
                        pb.Location = new System.Drawing.Point(12 * (3*x), 12 * (3*y)); 
                        pb.Image = Properties.Resources.Grass;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        pb.Visible = true;
                        guiMAP[x, y] = pb;

                        active.Controls.Add(pb);

                        guiMAP[x, y].Click += new EventHandler(this.picture_Click); //This is the function givent to the pictures on click event
                    }
                }
            }

            for (int i = 0; i < UnitsOnMap.Length; i++) //This loop is used to display the units on the Map based on their current X and Y positions
            {
                if (UnitsOnMap[i].GetType() == typeof(MeleeUnit))
                {
                    MeleeUnit convertUnit = (MeleeUnit)UnitsOnMap[i];
                    if (convertUnit.Faction == "Red")
                    {
                        guiMAP[convertUnit.XPos, convertUnit.YPos].Image = Properties.Resources.Red_Sword;
                    }
                    else guiMAP[convertUnit.XPos, convertUnit.YPos].Image = Properties.Resources.Blue_Sword;
                }
                else
                {
                    RangedUnit convertUnit = (RangedUnit)UnitsOnMap[i];
                    if (convertUnit.Faction == "Red")
                    {
                        guiMAP[convertUnit.XPos, convertUnit.YPos].Image = Properties.Resources.Red_Arrow;
                    }
                    else guiMAP[convertUnit.XPos, convertUnit.YPos].Image = Properties.Resources.Blue_Arrow;
                }
            }
        }

        void picture_Click(object sender, System.EventArgs e)
        {
            int x = ((PictureBox)sender).Location.X / 36; ; //A small culculation to find the X location of the picture box within the guiMAP array
            int y = ((PictureBox)sender).Location.Y / 36; ; //A small culculation to find the Y location of the picture box within the guiMAP array

            if (guiMAP[x, y].Image != Properties.Resources.Grass)//Checks to see if the picturebox clicked on is not grass
            {
                for (int i = 0; i < UnitsOnMap.Length; i++)//A loop to check every unit
                {
                    if (UnitsOnMap[i].GetType() == typeof(MeleeUnit))
                    {
                        MeleeUnit convertUnit = (MeleeUnit)UnitsOnMap[i];
                        if (convertUnit.XPos == x && convertUnit.YPos == y) //This if statement checks if the current units X and Y matches the X and Y of the picturebox clicked on
                        {
                            if (convertUnit.Faction == "Red")
                            {
                                Info.BackColor = System.Drawing.Color.Red; //Changes the textbox displaying the units information to either red or blue based on the units team
                            }
                            else Info.BackColor = System.Drawing.Color.Blue;
                            Info.Text = convertUnit.toString(); //The units information is displayed
                        }
                    }
                    else if (UnitsOnMap[i].GetType() == typeof(RangedUnit))
                    {
                        RangedUnit convertUnit = (RangedUnit)UnitsOnMap[i];
                        if (convertUnit.XPos == x && convertUnit.YPos == y)
                        {
                            if (convertUnit.Faction == "Red")
                            {
                                Info.BackColor = System.Drawing.Color.Red;
                            }
                            else Info.BackColor = System.Drawing.Color.Blue;
                            Info.Text = convertUnit.toString();
                        }
                    }
                }
            }
        }

        public void UserComponentSetup(Form active, Timer GameTime) //This method is used to add components to the active form
        {
            Info = new TextBox(); //Creates a TextBox
            Info.Location = new System.Drawing.Point(726,478);
            Info.Size = new System.Drawing.Size(265, 235);
            Info.Multiline = true;
            Info.ScrollBars = ScrollBars.Vertical;
            Info.Font = new System.Drawing.Font(Info.Font.FontFamily, 12);
            Info.ForeColor = System.Drawing.Color.White;
            Info.BackColor = System.Drawing.Color.Green;
            active.Controls.Add(Info);

            GameTimeDisplay = new Label(); //Creates a Label
            GameTimeDisplay.Location = new System.Drawing.Point(726, 10);
            GameTimeDisplay.Size = new System.Drawing.Size(200, 40);
            GameTimeDisplay.Font = new System.Drawing.Font(Info.Font.FontFamily, 20);
            GameTimeDisplay.ForeColor = System.Drawing.Color.Black;
            GameTimeDisplay.BackColor = System.Drawing.Color.Transparent;
            GameTimeDisplay.Text = "Timer";
            active.Controls.Add(GameTimeDisplay);

            Start = new Button(); //Creates a Button
            Start.Location = new System.Drawing.Point(726, 55);
            Start.Size = new System.Drawing.Size(260, 40);
            Start.Text = "Start Game";
            Start.Font = new System.Drawing.Font(Info.Font.FontFamily, 12);
            Start.Click += new EventHandler(this.Start_Click);
            active.Controls.Add(Start);

            Pause = new Button(); //Creates a Button
            Pause.Location = new System.Drawing.Point(726, 95);
            Pause.Size = new System.Drawing.Size(260, 40);
            Pause.Text = "Pause Game";
            Pause.Font = new System.Drawing.Font(Info.Font.FontFamily, 12);
            Pause.Click += new EventHandler(this.Pause_Click);
            Pause.Enabled = false;
            active.Controls.Add(Pause);

            NewGame = new TextBox(); //Creates a TextBox
            NewGame.Location = new System.Drawing.Point(400, 250);
            NewGame.Size = new System.Drawing.Size(260, 40);
            NewGame.Font = new System.Drawing.Font(Info.Font.FontFamily, 12);
            NewGame.Visible = false;
            NewGame.Enabled = false;
            active.Controls.Add(NewGame);

            newGameButton = new Button(); //Creates a Button
            newGameButton.Location = new System.Drawing.Point(400, 350);
            newGameButton.Size = new System.Drawing.Size(260, 40);
            newGameButton.Text = "Start Game";
            newGameButton.Font = new System.Drawing.Font(Info.Font.FontFamily, 12);
            newGameButton.Visible = false;
            newGameButton.Enabled = false;
            newGameButton.Click += new EventHandler(this.NewGame_Click);
            active.Controls.Add(newGameButton);
        }

        void Pause_Click(object sender, System.EventArgs e) //This method runs when the Pause button has been clicked
        {
            gameTimer.Enabled = false; //The gameTimer is stopped
            Start.Enabled = true; //The Start button is now available
            GameTimeDisplay.Text = "Paused";
            ((Button)sender).Enabled = false; //The Pause button is now disabled
        }

        void Start_Click(object sender, System.EventArgs e) //This method runs when the Pause button has been clicked
        {
            gameTimer.Enabled = true; //The gameTimer is started again
            ((Button)sender).Enabled = false; //The Start button is disabled
            Pause.Enabled = true; //The Pause button is available again
        }

        void NewGame_Click(object sender, System.EventArgs e) //This method runs when the NewGameButton is clicked on
        {
            if (NewGame.Text == "y" || NewGame.Text == "Y") //This if statement checks wether the user wants a new game or not
            {
                GenerateNew();
                Info.Visible = false;
                Start.Visible = false;
                Pause.Visible = false;
                GameTimeDisplay.Visible = false;
                CreateMap(activeForm); //Resets and recreates a new map

                Info.Visible = true;
                Start.Visible = true;
                Start.Enabled = true;
                Pause.Visible = true;
                Pause.Enabled = false;
                GameTimeDisplay.Visible = true;
                GameTimeDisplay.Text = "Timer";
                NewGame.Visible = false;
                NewGame.Enabled = false;
                newGameButton.Visible = false;
                newGameButton.Enabled = false;
            }
            else activeForm.Close(); //The form is closed
        }

        //Picture referencing
        //Royal Blue Sword Clip Art at Clker.com - vector clip art online, royalty free & public domain. 2018. Royal Blue Sword Clip Art at Clker.com - vector clip art online, royalty free & public domain. [ONLINE] Available at: http://www.clker.com/clipart-royal-blue-sword.html. [Accessed 15 August 2018].
        //Red Sword Clip Art at Clker.com - vector clip art online, royalty free & public domain. 2018. Red Sword Clip Art at Clker.com - vector clip art online, royalty free & public domain. [ONLINE] Available at: http://www.clker.com/clipart-red-sword-3.html. [Accessed 15 August 2018].
        //Red Bow And Arrow Clip Art at Clker.com - vector clip art online, royalty free & public domain. 2018. Red Bow And Arrow Clip Art at Clker.com - vector clip art online, royalty free & public domain. [ONLINE] Available at: http://www.clker.com/clipart-red-bow-and-arrow.html. [Accessed 15 August 2018].
        //Royal Blue Bow And Arrow Clip Art at Clker.com - vector clip art online, royalty free & public domain. 2018. Royal Blue Bow And Arrow Clip Art at Clker.com - vector clip art online, royalty free & public domain. [ONLINE] Available at: http://www.clker.com/clipart-royal-blue-bow-and-arrow.html. [Accessed 15 August 2018].
        //OpenGameArt.org. 2018. 30 Grass Textures (Tilable) | OpenGameArt.org. [ONLINE] Available at: https://opengameart.org/content/30-grass-textures-tilable. [Accessed 15 August 2018].
    }
}
