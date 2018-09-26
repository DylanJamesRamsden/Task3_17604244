using System; //Dylan James Ramsden 17604244
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADE6112_POE
{
    class GameEngine
    {
        Map M;
        Unit[] UnitsOnMap;
        PictureBox[,] visualMap;
        Form Active;
        Timer gameTimer;

        int seconds = 0; //Stores the gameTime's seconds
        int minutes = 0;//Stores the gameTime's minutes

        public GameEngine(Form act, Timer gameTime)
        {
            M = new Map(act, gameTime); //Creates a new Map object, this creates a new map and new units
            Active = act;
            UnitsOnMap = M.UnitsOnMap;
            visualMap = M.guiMAP;
            gameTimer = gameTime;
        }

        public void UnitAction() //This controls each units action throughout the simulation as well as the game time
        {
            if (seconds < 59) //These if statments are used to store the simulations time
            //Calculates the seconds up to 59
            {
                seconds++;
            }
            else
            //Adds a minute when the second is greater than or equal to 59 and resets seconds to 0
            {
                minutes++;
                seconds = 0;
            }
            if (seconds < 10) //This if statement is purely for visual aestetic and ensures the correct time format is displayed
            {
                M.GameTimeDisplay.Text = "Time: " + minutes.ToString() + ":0" + seconds.ToString();
            }
            else M.GameTimeDisplay.Text = "Time: " + minutes.ToString() + ":" + seconds.ToString();

            for (int i = 0; i < 12; i++) //This for loops runs through every Unit within the current simulation
            {
                if (UnitsOnMap[i].GetType() == typeof(MeleeUnit)) //This if statements is used to determine which type the Unit is
                {
                    MeleeUnit soldier = (MeleeUnit)UnitsOnMap[i]; //This casts the Unit as a MeleeUnit, this allows us to access its properties000000000000000000000000000000000000000
                    //soldier.closestUnit(UnitsOnMap);
                    if (soldier.IsAlive == true) //This if statements checks if the current Unit is alive or not
                    {
                        if (soldier.Health > 25) //This set of if statements chcks wether the soldier has more than 25 health or if they have less than or equal to 25 health, and runs methods based on the condition met
                        {
                            if (soldier.closestUnit(UnitsOnMap).GetType() == typeof(MeleeUnit))
                            {
                                MeleeUnit enemy = (MeleeUnit)soldier.closestUnit(UnitsOnMap);
                                if (soldier.inRange(soldier.closestUnit(UnitsOnMap)) == false) //This if statement checks whether the current Unit is in range of its target, if it is then there is no point in moving
                                {
                                    if (soldier.XPos < enemy.XPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                    }
                                    else if (soldier.XPos > enemy.XPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                    }
                                    else if (soldier.YPos < enemy.YPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                    }
                                    else if (soldier.YPos > enemy.YPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                    }
                                }
                                else soldier.combatWithEnemy(enemy); //If the Unit is in range, a combat action is inititated
                            }
                            else
                            {
                                RangedUnit enemy = (RangedUnit)soldier.closestUnit(UnitsOnMap);
                                if (soldier.inRange(soldier.closestUnit(UnitsOnMap)) == false)
                                {
                                    if (soldier.XPos < enemy.XPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos);
                                    }
                                    else if (soldier.XPos > enemy.XPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos);
                                    }
                                    else if (soldier.YPos < enemy.YPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos);
                                    }
                                    else if (soldier.YPos > enemy.YPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);
                                    }
                                }
                                else soldier.combatWithEnemy(enemy);
                            }
                        }
                        else if (soldier.Health <= 25)
                        {
                            Random r = new Random();
                            int randomDirection = r.Next(0, 5);
                            moveInRandom(UnitsOnMap[i], randomDirection); //If the Units health is less than or equal to 25, the Unit will run in any random direction
                        }
                    }
                    else  M.guiMAP[soldier.XPos, soldier.YPos].Image = Properties.Resources.Grass; //If the soldier is dead, their visual representation is removed from the guiMAP
                }
                else
                {
                   RangedUnit soldier = (RangedUnit)UnitsOnMap[i];
                    //soldier.closestUnit(UnitsOnMap);
                    if (soldier.IsAlive == true)
                    {
                        if (soldier.Health > 25)
                        {
                            if (soldier.closestUnit(UnitsOnMap).GetType() == typeof(MeleeUnit))
                            {
                                MeleeUnit enemy = (MeleeUnit)soldier.closestUnit(UnitsOnMap);
                                if (soldier.inRange(soldier.closestUnit(UnitsOnMap)) == false)
                                {
                                    if (soldier.XPos < enemy.XPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos);
                                    }
                                    else if (soldier.XPos > enemy.XPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos);
                                    }
                                    else if (soldier.YPos < enemy.YPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos);
                                    }
                                    else if (soldier.YPos > enemy.YPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);
                                    }
                                }
                                else soldier.combatWithEnemy(enemy);
                            }
                            else
                            {
                                RangedUnit enemy = (RangedUnit)soldier.closestUnit(UnitsOnMap);
                                if (soldier.inRange(soldier.closestUnit(UnitsOnMap)) == false)
                                {
                                    if (soldier.XPos < enemy.XPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos);
                                    }
                                    else if (soldier.XPos > enemy.XPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos);
                                    }
                                    else if (soldier.YPos < enemy.YPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos);
                                    }
                                    else if (soldier.YPos > enemy.YPos)
                                    {
                                        M.updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);
                                    }
                                }
                                else soldier.combatWithEnemy(enemy);
                            }
                        }
                        else if (soldier.Health <= 25)
                        {
                            Random r = new Random();
                            int randomDirection = r.Next(0, 5);
                            moveInRandom(UnitsOnMap[i], randomDirection);
                        }
                    }
                    else M.guiMAP[soldier.XPos, soldier.YPos].Image = Properties.Resources.Grass;
                }
            }
            FindWinner(); //This method is used to check wether there is a winner or not
        }

        public void moveInRandom(Unit soldier, int r) //This method is used to move a Unit in a random direction
        {
            if (soldier.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit scaredSoldier = (MeleeUnit)soldier;
                switch (r) //It bases the random direction decision on a random int variable 
                {
                    case 0:
                        if (scaredSoldier.XPos + scaredSoldier.Speed > 20) //This if statement checks if the Unit will move out of bounds or not of the map, this prevents an outOfbounds error
                        {
                            scaredSoldier.IsAlive = false; //If the Unit moves off the map they are killed
                            M.guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass; //If the Unit is dead its visual position on the guiMAP is truned to grass
                        }
                        else
                        {
                            M.updatePos(soldier, scaredSoldier.XPos + scaredSoldier.Speed, scaredSoldier.YPos, scaredSoldier.XPos, scaredSoldier.YPos); //The units movement is multiplied by its movement speed because it is running 
                        }
                        break;
                    case 1:
                        if (scaredSoldier.XPos - scaredSoldier.Speed < 0)
                        {
                            scaredSoldier.IsAlive = false;
                            M.guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            M.updatePos(soldier, scaredSoldier.XPos - scaredSoldier.Speed, scaredSoldier.YPos, scaredSoldier.XPos, scaredSoldier.YPos);
                        }
                        break;
                    case 2:
                        if (scaredSoldier.YPos + scaredSoldier.Speed > 20)
                        {
                            scaredSoldier.IsAlive = false;
                            M.guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            M.updatePos(soldier, scaredSoldier.XPos, scaredSoldier.YPos + scaredSoldier.Speed, scaredSoldier.XPos, scaredSoldier.YPos);
                        }
                        break;
                    case 3:
                        if (scaredSoldier.YPos - scaredSoldier.Speed < 0)
                        {
                            scaredSoldier.IsAlive = false;
                            M.guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            M.updatePos(soldier, scaredSoldier.XPos, scaredSoldier.YPos - scaredSoldier.Speed, scaredSoldier.XPos, scaredSoldier.YPos);
                        }
                        break;
                }

            }
            else
            {
                RangedUnit scaredSoldier = (RangedUnit)soldier;
                switch (r)
                {
                    case 0:
                        if (scaredSoldier.XPos + scaredSoldier.Speed > 20)
                        {
                            scaredSoldier.IsAlive = false;
                            M.guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            M.updatePos(soldier, scaredSoldier.XPos + scaredSoldier.Speed, scaredSoldier.YPos, scaredSoldier.XPos, scaredSoldier.YPos);
                        }
                        break;
                    case 1:
                        if (scaredSoldier.XPos - scaredSoldier.Speed < 0)
                        {
                            scaredSoldier.IsAlive = false;
                            M.guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            M.updatePos(soldier, scaredSoldier.XPos - scaredSoldier.Speed, scaredSoldier.YPos, scaredSoldier.XPos, scaredSoldier.YPos);
                        }
                        break;
                    case 2:
                        if (scaredSoldier.YPos + scaredSoldier.Speed > 20)
                        {
                            scaredSoldier.IsAlive = false;
                            M.guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            M.updatePos(soldier, scaredSoldier.XPos, scaredSoldier.YPos + scaredSoldier.Speed, scaredSoldier.XPos, scaredSoldier.YPos);
                        }
                        break;
                    case 3:
                        if (scaredSoldier.YPos - scaredSoldier.Speed < 0)
                        {
                            scaredSoldier.IsAlive = false;
                            M.guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            M.updatePos(soldier, scaredSoldier.XPos, scaredSoldier.YPos - scaredSoldier.Speed, scaredSoldier.XPos, scaredSoldier.YPos);
                        }
                        break;
                }
            }
        }

        public void FindWinner() //This method is used to see if there is a winner
        {
            int Healthy = 0; //This variables holds the number of Units that have more than 25 HP
            int redCounter = 0; //This variable holds the number of red Units still alive
            int blueCounter = 0; //This variable holds the number of blue Units still alive

            for (int i= 0; i < UnitsOnMap.Length; i++) //This for loop runs through all the units, checking if they are alive or not, and if they have more than 25 HP
            {
                if (UnitsOnMap[i].GetType() == typeof(MeleeUnit))
                {
                    MeleeUnit soldier = (MeleeUnit)UnitsOnMap[i];
                    if (soldier.IsAlive == true) //This if statement checks if the current Unit is alive
                    {
                        if (soldier.Health > 25) //This if statement checks if the current Unit has less than 25 HP
                        {
                            Healthy++;
                        }
                        if (soldier.Faction == "Red") //This if statement checks if the current Unit is on the Red team
                        {
                            redCounter++; //Increases the Red team counter
                        }
                        else blueCounter++; //Increases the Blue team counter
                    }
                }
                else
                {
                    RangedUnit soldier = (RangedUnit)UnitsOnMap[i];
                    if (soldier.IsAlive == true)
                    {
                        if (soldier.Health > 25)
                        {
                            Healthy++;
                        }
                        if (soldier.Faction == "Red")
                        {
                            redCounter++;
                        }
                        else blueCounter++;
                    }
                }
            }

            if (redCounter==0) //This if statement checks if the redCounter is 0 (if the red team wins)
            {
                gameTimer.Enabled = false;
                MessageBox.Show("Blue Team wins!!!!");
                newGame();
            }
            else if (blueCounter == 0) //This if statement checks if the blueCounter is 0 (if the Blue team wins)
            {
                gameTimer.Enabled = false;
                MessageBox.Show("Red Team wins!!!!");
                newGame();
            }
            else if (Healthy == 0) //If no team wins and all units on the guiMAP are below or equal to 25 HP then the battle is a tie
            { 
                gameTimer.Enabled = false;
                MessageBox.Show("Tie");
                newGame();
            }
        }

        public void newGame() //This method is used to ask the user if they would like to start a new game
        {
            for (int x = 0; x < 20; x++)
            {
               for (int y = 0; y < 20; y++)
                {
                    M.guiMAP[x, y].Visible = false;
                }
            }
            M.Info.Visible = false;
            M.Start.Visible = false;
            M.Pause.Visible = false;
            M.GameTimeDisplay.Visible = false;
            M.NewGame.Visible = true;
            M.NewGame.Enabled = true;
            M.newGameButton.Visible = true;
            M.newGameButton.Enabled = true;
            seconds = 0;
            minutes = 0;         
        }
    }
}
