using System; //Dylan James Ramsden 17604244
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GADE6112_POE
{
    [Serializable]
    class GameEngine
    {
        Map M;
        Form Active;

        int overAllgameTime = 0;

        public PictureBox[,] guiMAP = new PictureBox[20, 20]; //Holds 20 images to portray the game map

        public TextBox Info;
        public Label GameTimeDisplay;
        public Label RedResourceCounter;
        public Label BlueResourceCounter;
        public Timer gameTimer;
        public Button Start;
        public Button Pause;
        public TextBox NewGame;
        public Button Save;
        public Button Load;

        public GameEngine(Form act, Timer gameTime)
        {
            M = new Map(); //Creates a new Map object, this creates a new map with new units and buildings
            Active = act;
            gameTimer = gameTime;
            CreateMap(Active);

            UserComponentSetup(act);
        }

        public void UnitAction() //This controls each units action throughout the simulation as well as the game time
        {
            if (M.Seconds < 59) //These if statments are used to store the simulations time
            //Calculates the seconds up to 59
            {
                M.Seconds++;
                overAllgameTime++;
            }
            else
            //Adds a minute when the second is greater than or equal to 59 and resets seconds to 0
            {
                M.Minutes++;
                M.Seconds = 0;
            }
            if (M.Seconds < 10) //This if statement is purely for visual aestetic and ensures the correct time format is displayed
            {
                GameTimeDisplay.Text = "Time: " + M.Minutes.ToString() + ":0" + M.Seconds.ToString();
            }
            else GameTimeDisplay.Text = "Time: " + M.Minutes.ToString() + ":" + M.Seconds.ToString();


            for (int i = 0; i < M.UnitsOnMap.Length; i++) //This for loops runs through every Unit within the current simulation
                {
                if (M.UnitsOnMap[i] != null)
                {
                    if (M.UnitsOnMap[i].GetType() == typeof(MeleeUnit)) //This if statements is used to determine which type the Unit is
                    {
                        MeleeUnit soldier = (MeleeUnit)M.UnitsOnMap[i]; //This casts the Unit as a MeleeUnit, this allows us to access its properties000000000000000000000000000000000000000
                                                                        //soldier.closestUnit(UnitsOnMap);
                        if (soldier.IsAlive == true) //This if statements checks if the current Unit is alive or not
                        {
                            if (soldier.Health > 25) //This set of if statements chcks wether the soldier has more than 25 health or if they have less than or equal to 25 health, and runs methods based on the condition met
                            {
                                if (soldier.closestUnit(M.UnitsOnMap).GetType() == typeof(MeleeUnit))
                                {
                                    MeleeUnit enemy = (MeleeUnit)soldier.closestUnit(M.UnitsOnMap);
                                    if (soldier.inRange(soldier.closestUnit(M.UnitsOnMap)) == false) //This if statement checks whether the current Unit is in range of its target, if it is then there is no point in moving
                                    {
                                        if (soldier.XPos < enemy.XPos && soldier.XPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                        }
                                        else if (soldier.XPos > enemy.XPos && soldier.XPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                        }
                                        else if (soldier.YPos < enemy.YPos && soldier.YPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                        }
                                        else if (soldier.YPos > enemy.YPos && soldier.YPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                        }
                                    }
                                    else soldier.combatWithEnemy(enemy); //If the Unit is in range, a combat action is inititated
                                }
                                else if (soldier.closestUnit(M.UnitsOnMap).GetType() == typeof(RangedUnit))
                                {
                                    RangedUnit enemy = (RangedUnit)soldier.closestUnit(M.UnitsOnMap);
                                    if (soldier.inRange(soldier.closestUnit(M.UnitsOnMap)) == false)
                                    {
                                        if (soldier.XPos < enemy.XPos && soldier.XPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                        }
                                        else if (soldier.XPos > enemy.XPos && soldier.XPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                        }
                                        else if (soldier.YPos < enemy.YPos && soldier.YPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                        }
                                        else if (soldier.YPos > enemy.YPos && soldier.YPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                        }
                                    }
                                    else soldier.combatWithEnemy(enemy);
                                }
                                else if (soldier.closestUnit(M.UnitsOnMap).GetType() == typeof(BarbarianMelee))
                                {
                                    BarbarianMelee enemy = (BarbarianMelee)soldier.closestUnit(M.UnitsOnMap);
                                    if (soldier.inRange(soldier.closestUnit(M.UnitsOnMap)) == false)
                                    {
                                        if (soldier.XPos < enemy.XPos && soldier.XPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                        }
                                        else if (soldier.XPos > enemy.XPos && soldier.XPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                        }
                                        else if (soldier.YPos < enemy.YPos && soldier.YPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                        }
                                        else if (soldier.YPos > enemy.YPos && soldier.YPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                        }
                                    }
                                    else soldier.combatWithEnemy(enemy);
                                }
                                else
                                {
                                    BarbarianRanged enemy = (BarbarianRanged)soldier.closestUnit(M.UnitsOnMap);
                                    if (soldier.inRange(soldier.closestUnit(M.UnitsOnMap)) == false)
                                    {
                                        if (soldier.XPos < enemy.XPos && soldier.XPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                        }
                                        else if (soldier.XPos > enemy.XPos && soldier.XPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                        }
                                        else if (soldier.YPos < enemy.YPos && soldier.YPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                        }
                                        else if (soldier.YPos > enemy.YPos && soldier.YPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                        }
                                    }
                                    else soldier.combatWithEnemy(enemy);
                                }
                            }
                            else if (soldier.Health <= 25)
                            {
                                Random r = new Random();
                                int randomDirection = r.Next(0, 6); //Random number to affect the movement of the Unit more dynamically

                                if (randomDirection < 5) //Moves the unit in a random direction
                                {
                                    moveInRandom(M.UnitsOnMap[i], randomDirection);
                                }
                                else //Changes the units faction due to fear or healing
                                {
                                    if (soldier.HasTurned == false)
                                    {
                                        if (soldier.Faction == "Red")
                                        {
                                            soldier.Faction = "Blue";
                                            soldier.Name = "Healed " + soldier.Name;
                                            soldier.HasTurned = true;
                                        }
                                        else
                                        {
                                            soldier.Faction = "Red";
                                            soldier.Name = "Turned " + soldier.Name;
                                            soldier.HasTurned = true;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            guiMAP[soldier.XPos, soldier.YPos].Image = Properties.Resources.Grass; //If the soldier is dead, their visual representation is removed from the guiMAP
                            M.UnitsOnMap[i] = null;
                        }
                    }
                    else if (M.UnitsOnMap[i].GetType() == typeof(RangedUnit))
                    {
                        RangedUnit soldier = (RangedUnit)M.UnitsOnMap[i];
                        //soldier.closestUnit(UnitsOnMap);
                        if (soldier.IsAlive == true)
                        {
                            if (soldier.Health > 25)
                            {
                                if (soldier.closestUnit(M.UnitsOnMap).GetType() == typeof(MeleeUnit))
                                {
                                    MeleeUnit enemy = (MeleeUnit)soldier.closestUnit(M.UnitsOnMap);
                                    if (soldier.inRange(soldier.closestUnit(M.UnitsOnMap)) == false) //This if statement checks whether the current Unit is in range of its target, if it is then there is no point in moving
                                    {
                                        if (soldier.XPos < enemy.XPos && soldier.XPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                        }
                                        else if (soldier.XPos > enemy.XPos && soldier.XPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                        }
                                        else if (soldier.YPos < enemy.YPos && soldier.YPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                        }
                                        else if (soldier.YPos > enemy.YPos && soldier.YPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                        }
                                    }
                                    else soldier.combatWithEnemy(enemy); //If the Unit is in range, a combat action is inititated
                                }
                                else if (soldier.closestUnit(M.UnitsOnMap).GetType() == typeof(RangedUnit))
                                {
                                    RangedUnit enemy = (RangedUnit)soldier.closestUnit(M.UnitsOnMap);
                                    if (soldier.inRange(soldier.closestUnit(M.UnitsOnMap)) == false)
                                    {
                                        if (soldier.XPos < enemy.XPos && soldier.XPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                        }
                                        else if (soldier.XPos > enemy.XPos && soldier.XPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                        }
                                        else if (soldier.YPos < enemy.YPos && soldier.YPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                        }
                                        else if (soldier.YPos > enemy.YPos && soldier.YPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                        }
                                    }
                                    else soldier.combatWithEnemy(enemy);
                                }
                                else if (soldier.closestUnit(M.UnitsOnMap).GetType() == typeof(BarbarianMelee))
                                {
                                    BarbarianMelee enemy = (BarbarianMelee)soldier.closestUnit(M.UnitsOnMap);
                                    if (soldier.inRange(soldier.closestUnit(M.UnitsOnMap)) == false)
                                    {
                                        if (soldier.XPos < enemy.XPos && soldier.XPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                        }
                                        else if (soldier.XPos > enemy.XPos && soldier.XPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                        }
                                        else if (soldier.YPos < enemy.YPos && soldier.YPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                        }
                                        else if (soldier.YPos > enemy.YPos && soldier.YPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                        }
                                    }
                                    else soldier.combatWithEnemy(enemy);
                                }
                                else
                                {
                                    BarbarianRanged enemy = (BarbarianRanged)soldier.closestUnit(M.UnitsOnMap);
                                    if (soldier.inRange(soldier.closestUnit(M.UnitsOnMap)) == false)
                                    {
                                        if (soldier.XPos < enemy.XPos && soldier.XPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                        }
                                        else if (soldier.XPos > enemy.XPos && soldier.XPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                        }
                                        else if (soldier.YPos < enemy.YPos && soldier.YPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                        }
                                        else if (soldier.YPos > enemy.YPos && soldier.YPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                        }
                                    }
                                    else soldier.combatWithEnemy(enemy);
                                }
                            }
                            else if (soldier.Health <= 25)
                            {
                                Random r = new Random();
                                int randomDirection = r.Next(0, 6);

                                if (randomDirection < 5)
                                {
                                    moveInRandom(M.UnitsOnMap[i], randomDirection);
                                }
                                else
                                {
                                    if (soldier.HasTurned == false)
                                    {
                                        if (soldier.Faction == "Red")
                                        {
                                            soldier.Faction = "Blue";
                                            soldier.Name = "Healed " + soldier.Name;
                                            soldier.HasTurned = true;
                                        }
                                        else
                                        {
                                            soldier.Faction = "Red";
                                            soldier.Name = "Turned " + soldier.Name;
                                            soldier.HasTurned = true;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            guiMAP[soldier.XPos, soldier.YPos].Image = Properties.Resources.Grass; //If the soldier is dead, their visual representation is removed from the guiMAP
                            M.UnitsOnMap[i] = null;
                        }
                    }
                    else if (M.UnitsOnMap[i].GetType() == typeof(BarbarianMelee)) //This if statements is used to determine which type the Unit is
                    {
                        BarbarianMelee soldier = (BarbarianMelee)M.UnitsOnMap[i]; //This casts the Unit as a BarbarianMeleeUnit, this allows us to access its properties000000000000000000000000000000000000000
                                                                                  //soldier.closestUnit(UnitsOnMap);
                        if (soldier.IsAlive == true) //This if statements checks if the current Unit is alive or not
                        {
                                if (soldier.closestUnit(M.UnitsOnMap).GetType() == typeof(MeleeUnit))
                                {
                                    MeleeUnit enemy = (MeleeUnit)soldier.closestUnit(M.UnitsOnMap);
                                    if (soldier.inRange(soldier.closestUnit(M.UnitsOnMap)) == false) //This if statement checks whether the current Unit is in range of its target, if it is then there is no point in moving
                                        {
                                        if (soldier.XPos < enemy.XPos && soldier.XPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                        }
                                        else if (soldier.XPos > enemy.XPos && soldier.XPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                        }
                                        else if (soldier.YPos < enemy.YPos && soldier.YPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                        }
                                        else if (soldier.YPos > enemy.YPos && soldier.YPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                        }
                                    }
                                    else soldier.combatWithEnemy(enemy); //If the Unit is in range, a combat action is inititated
                                }
                                else if (soldier.closestUnit(M.UnitsOnMap).GetType() == typeof(RangedUnit))
                                {
                                    RangedUnit enemy = (RangedUnit)soldier.closestUnit(M.UnitsOnMap);
                                    if (soldier.inRange(soldier.closestUnit(M.UnitsOnMap)) == false)
                                    {
                                        if (soldier.XPos < enemy.XPos && soldier.XPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                        }
                                        else if (soldier.XPos > enemy.XPos && soldier.XPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                        }
                                        else if (soldier.YPos < enemy.YPos && soldier.YPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                        }
                                        else if (soldier.YPos > enemy.YPos && soldier.YPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                        }
                                    }
                                    else soldier.combatWithEnemy(enemy);
                                }
                        }
                        else
                        {
                            guiMAP[soldier.XPos, soldier.YPos].Image = Properties.Resources.Grass; //If the soldier is dead, their visual representation is removed from the guiMAP
                            M.UnitsOnMap[i] = null;
                        }
                    }
                    else if (M.UnitsOnMap[i].GetType() == typeof(BarbarianRanged)) //This if statements is used to determine which type the Unit is
                    {
                        BarbarianRanged soldier = (BarbarianRanged)M.UnitsOnMap[i]; //This casts the Unit as a BarbarianRangedUnit, this allows us to access its properties000000000000000000000000000000000000000
                                                                        //soldier.closestUnit(UnitsOnMap);
                        if (soldier.IsAlive == true) //This if statements checks if the current Unit is alive or not
                        {
                                if (soldier.closestUnit(M.UnitsOnMap).GetType() == typeof(MeleeUnit))
                                {
                                    MeleeUnit enemy = (MeleeUnit)soldier.closestUnit(M.UnitsOnMap);
                                    if (soldier.inRange(soldier.closestUnit(M.UnitsOnMap)) == false) //This if statement checks whether the current Unit is in range of its target, if it is then there is no point in moving
                                    {
                                        if (soldier.XPos < enemy.XPos && soldier.XPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                        }
                                        else if (soldier.XPos > enemy.XPos && soldier.XPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                        }
                                        else if (soldier.YPos < enemy.YPos && soldier.YPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                        }
                                        else if (soldier.YPos > enemy.YPos && soldier.YPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                        }
                                    }
                                    else soldier.combatWithEnemy(enemy); //If the Unit is in range, a combat action is inititated
                                }
                                else if (soldier.closestUnit(M.UnitsOnMap).GetType() == typeof(RangedUnit))
                            {
                                    RangedUnit enemy = (RangedUnit)soldier.closestUnit(M.UnitsOnMap);
                                    if (soldier.inRange(soldier.closestUnit(M.UnitsOnMap)) == false)
                                    {
                                        if (soldier.XPos < enemy.XPos && soldier.XPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos + 1, soldier.YPos, soldier.XPos, soldier.YPos); //Increases the Units X position by 1
                                        }
                                        else if (soldier.XPos > enemy.XPos && soldier.XPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos - 1, soldier.YPos, soldier.XPos, soldier.YPos); //Decreases the Units X position by 1
                                        }
                                        else if (soldier.YPos < enemy.YPos && soldier.YPos < 19)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos + 1, soldier.XPos, soldier.YPos); //Increases the Units Y position by 1
                                        }
                                        else if (soldier.YPos > enemy.YPos && soldier.YPos > 0)
                                        {
                                            updatePos(soldier, soldier.XPos, soldier.YPos - 1, soldier.XPos, soldier.YPos);//Decreases the Units Y position by 1
                                        }
                                    }
                                    else soldier.combatWithEnemy(enemy);
                                }
                        }
                        else
                        {
                            guiMAP[soldier.XPos, soldier.YPos].Image = Properties.Resources.Grass; //If the soldier is dead, their visual representation is removed from the guiMAP
                            M.UnitsOnMap[i] = null;
                        }
                    }
                }
            }

            for (int b = 0; b < M.BuildingsOnMap.Length; b++) //This loop runs through all the buildings in the game and carries out their specific tasks
            {
                if (M.BuildingsOnMap[b].GetType() == typeof(ResourceBuilding))
                {
                    ResourceBuilding newResource = (ResourceBuilding)M.BuildingsOnMap[b];
                    if (newResource.isDead() == false) //Checks to see if the building is dead
                    {
                        if (newResource.Faction == "Red")
                        {
                            guiMAP[newResource.XPos, newResource.YPos].Image = Properties.Resources.Red_Mine;
                            M.RedResources = M.RedResources + newResource.GenerateResources();
                            RedResourceCounter.Text = "Red Resources: " + M.RedResources.ToString();
                        }
                        else
                        {
                            guiMAP[newResource.XPos, newResource.YPos].Image = Properties.Resources.Blue_Mine;
                            M.BlueResources = M.BlueResources + newResource.GenerateResources();
                            BlueResourceCounter.Text = "Blue resources: " + M.BlueResources.ToString();
                        }
                    }
                    else
                    {
                         if (newResource.Faction == "Red")
                         {
                             guiMAP[newResource.XPos, newResource.YPos].Image = Properties.Resources.Red_Mine_No_Resources;
                         }
                         else guiMAP[newResource.XPos, newResource.YPos].Image = Properties.Resources.Blue_Mine_No_Resources;
                    }
                }
                else
                {
                    if (M.getUnits() < 18) //Checks to see if there is space to spawn a new unit
                    {
                        FactoryBuilding newFactory = (FactoryBuilding)M.BuildingsOnMap[b];
                        if (newFactory.Faction == "Red")
                        {
                            guiMAP[newFactory.XPos, newFactory.YPos].Image = Properties.Resources.Red_Factory;
                            if (newFactory.SpawnUnit(overAllgameTime, M.RedResources) != null)
                            {
                                for (int s = 0; s < 18; s++)
                                {
                                    if (M.UnitsOnMap[s] == null)
                                    {
                                        M.UnitsOnMap[s] = newFactory.SpawnUnit(overAllgameTime, M.RedResources);
                                        if (M.UnitsOnMap[s] != null) 
                                        {
                                            if (M.UnitsOnMap[s].GetType() == typeof(MeleeUnit))
                                            {
                                                MeleeUnit newMelee = (MeleeUnit)M.UnitsOnMap[s];
                                                M.RedResources = M.RedResources - newMelee.UnitCost;
                                            }
                                            else
                                            {
                                                RangedUnit newRanged = (RangedUnit)M.UnitsOnMap[s];
                                                M.RedResources = M.RedResources - newRanged.UnitCost;
                                            }
                                        }
                                        break; //breaks the loop to ensure only one unit is spawned per a building
                                            
                                    }
                                }
                            }
                        }
                        else
                        {
                            guiMAP[newFactory.XPos, newFactory.YPos].Image = Properties.Resources.Blue_Factory;
                            if (newFactory.SpawnUnit(overAllgameTime, M.BlueResources) != null)
                            {
                                for (int s = 0; s < 12; s++)
                                {
                                    if (M.UnitsOnMap[s] == null)
                                    {
                                        M.UnitsOnMap[s] = newFactory.SpawnUnit(overAllgameTime, M.BlueResources);
                                            if (M.UnitsOnMap[s].GetType() == typeof(MeleeUnit))
                                            {
                                                MeleeUnit newMelee = (MeleeUnit)M.UnitsOnMap[s];
                                                M.BlueResources = M.BlueResources - newMelee.UnitCost;
                                            }
                                            else
                                            {
                                                RangedUnit newRanged = (RangedUnit)M.UnitsOnMap[s];
                                                M.BlueResources = M.BlueResources - newRanged.UnitCost;
                                            }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            endGameCondition();
        }
        
        public void moveInRandom(Unit soldier, int r) //This method is used to move a Unit in a random direction
        {
            if (soldier.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit scaredSoldier = (MeleeUnit)soldier;
                switch (r) //It bases the random direction decision on a random int variable 
                {
                    case 0:
                        if (scaredSoldier.XPos + scaredSoldier.Speed > 19) //This if statement checks if the Unit will move out of bounds or not of the map, this prevents an outOfbounds error
                        {
                            M.UnitsOnMap[r] = null; //If the Unit moves off the map they are killed
                            guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass; //If the Unit is dead its visual position on the guiMAP is truned to grass
                        }
                        else
                        {
                            updatePos(soldier, scaredSoldier.XPos + scaredSoldier.Speed, scaredSoldier.YPos, scaredSoldier.XPos, scaredSoldier.YPos); //The units movement is multiplied by its movement speed because it is running 
                            //scaredSoldier.Health = scaredSoldier.Health - 5; //The soldier eventually bleeds out
                        }
                        break;
                    case 1:
                        if (scaredSoldier.XPos - scaredSoldier.Speed < 0)
                        {
                            M.UnitsOnMap[r] = null;
                            guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            updatePos(soldier, scaredSoldier.XPos - scaredSoldier.Speed, scaredSoldier.YPos, scaredSoldier.XPos, scaredSoldier.YPos);
                            //scaredSoldier.Health = scaredSoldier.Health - 5; //The soldier eventually bleeds out
                        }
                        break;
                    case 2:
                        if (scaredSoldier.YPos + scaredSoldier.Speed > 19)
                        {
                            M.UnitsOnMap[r] = null;
                            guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            updatePos(soldier, scaredSoldier.XPos, scaredSoldier.YPos + scaredSoldier.Speed, scaredSoldier.XPos, scaredSoldier.YPos);
                            //scaredSoldier.Health = scaredSoldier.Health - 5; //The soldier eventually bleeds out
                        }
                        break;
                    case 3:
                        if (scaredSoldier.YPos - scaredSoldier.Speed < 0)
                        {
                            M.UnitsOnMap[r] = null;
                            guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            updatePos(soldier, scaredSoldier.XPos, scaredSoldier.YPos - scaredSoldier.Speed, scaredSoldier.XPos, scaredSoldier.YPos);
                            //scaredSoldier.Health = scaredSoldier.Health - 5; //The soldier eventually bleeds out
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
                        if (scaredSoldier.XPos + scaredSoldier.Speed > 19) //This if statement checks if the Unit will move out of bounds or not of the map, this prevents an outOfbounds error
                        {
                            M.UnitsOnMap[r] = null; //If the Unit moves off the map they are killed
                            guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass; //If the Unit is dead its visual position on the guiMAP is truned to grass
                        }
                        else
                        {
                            updatePos(soldier, scaredSoldier.XPos + scaredSoldier.Speed, scaredSoldier.YPos, scaredSoldier.XPos, scaredSoldier.YPos); //The units movement is multiplied by its movement speed because it is running 
                            //scaredSoldier.Health = scaredSoldier.Health - 5; //The soldier eventually bleeds out
                        }
                        break;
                    case 1:
                        if (scaredSoldier.XPos - scaredSoldier.Speed < 0)
                        {
                            M.UnitsOnMap[r] = null;
                            guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            updatePos(soldier, scaredSoldier.XPos - scaredSoldier.Speed, scaredSoldier.YPos, scaredSoldier.XPos, scaredSoldier.YPos);
                            //scaredSoldier.Health = scaredSoldier.Health - 5; //The soldier eventually bleeds out
                        }
                        break;
                    case 2:
                        if (scaredSoldier.YPos + scaredSoldier.Speed > 19)
                        {
                            M.UnitsOnMap[r] = null;
                            guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            updatePos(soldier, scaredSoldier.XPos, scaredSoldier.YPos + scaredSoldier.Speed, scaredSoldier.XPos, scaredSoldier.YPos);
                            //scaredSoldier.Health = scaredSoldier.Health - 5; //The soldier eventually bleeds out
                        }
                        break;
                    case 3:
                        if (scaredSoldier.YPos - scaredSoldier.Speed < 0)
                        {
                            M.UnitsOnMap[r] = null;
                            guiMAP[scaredSoldier.XPos, scaredSoldier.YPos].Image = Properties.Resources.Grass;
                        }
                        else
                        {
                            updatePos(soldier, scaredSoldier.XPos, scaredSoldier.YPos - scaredSoldier.Speed, scaredSoldier.XPos, scaredSoldier.YPos);
                            //scaredSoldier.Health = scaredSoldier.Health - 5; //The soldier eventually bleeds out
                        }
                        break;
                }
            }
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
                        pb.Location = new System.Drawing.Point(12 * (3 * x), 12 * (3 * y));
                        pb.Image = Properties.Resources.Grass;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        pb.Visible = true;
                        guiMAP[x, y] = pb;

                        active.Controls.Add(pb);

                        guiMAP[x, y].Click += new EventHandler(this.picture_Click); //This is the function givent to the pictures on click event
                    }
                }
            }

            if (M.getUnits() > 0)
            {
                for (int i = 0; i < M.UnitsOnMap.Length; i++) //This loop is used to display the units on the Map based on their current X and Y positions
                {
                    if (M.UnitsOnMap[i] != null)
                    {
                        if (M.UnitsOnMap[i].GetType() == typeof(MeleeUnit))
                        {
                            MeleeUnit convertUnit = (MeleeUnit)M.UnitsOnMap[i];
                            if (convertUnit.Faction == "Red")
                            {
                                guiMAP[convertUnit.XPos, convertUnit.YPos].Image = Properties.Resources.Red_Sword;
                            }
                            else guiMAP[convertUnit.XPos, convertUnit.YPos].Image = Properties.Resources.Blue_Sword;
                        }
                        else if (M.UnitsOnMap[i].GetType() == typeof(RangedUnit))
                        {
                            RangedUnit convertUnit = (RangedUnit)M.UnitsOnMap[i];
                            if (convertUnit.Faction == "Red")
                            {
                                guiMAP[convertUnit.XPos, convertUnit.YPos].Image = Properties.Resources.Red_Arrow;
                            }
                            else guiMAP[convertUnit.XPos, convertUnit.YPos].Image = Properties.Resources.Blue_Arrow;
                        }
                        else if (M.UnitsOnMap[i].GetType() == typeof(BarbarianMelee))
                        {
                            BarbarianMelee convertUnit = (BarbarianMelee)M.UnitsOnMap[i];
                            guiMAP[convertUnit.XPos, convertUnit.YPos].Image = Properties.Resources.Barbarian_Sword;
                        }
                        else
                        {
                            BarbarianRanged convertUnit = (BarbarianRanged)M.UnitsOnMap[i];
                            guiMAP[convertUnit.XPos, convertUnit.YPos].Image = Properties.Resources.Barbarian_Arrow;
                        }
                    }
                }
            }

            for (int i = 0; i < 6; i++) //This loop is used to display the units on the Map based on their current X and Y positions
            {
                if (M.BuildingsOnMap[i].GetType() == typeof(ResourceBuilding))
                {
                    ResourceBuilding convertBuilding = (ResourceBuilding)M.BuildingsOnMap[i];
                    if (convertBuilding.Faction == "Red")
                    {
                        guiMAP[convertBuilding.XPos, convertBuilding.YPos].Image = Properties.Resources.Red_Mine;
                    }
                    else guiMAP[convertBuilding.XPos, convertBuilding.YPos].Image = Properties.Resources.Blue_Mine;
                }
                else
                {
                    FactoryBuilding convertBuilding = (FactoryBuilding)M.BuildingsOnMap[i];
                    if (convertBuilding.Faction == "Red")
                    {
                        guiMAP[convertBuilding.XPos, convertBuilding.YPos].Image = Properties.Resources.Red_Factory;
                    }
                    else guiMAP[convertBuilding.XPos, convertBuilding.YPos].Image = Properties.Resources.Blue_Factory;
                }
            }
        }

        void picture_Click(object sender, System.EventArgs e) //This is the onclick event for the picture boxs portraying the map
        {
            int x = ((PictureBox)sender).Location.X / 36; ; //A small culculation to find the X location of the picture box within the guiMAP array
            int y = ((PictureBox)sender).Location.Y / 36; ; //A small culculation to find the Y location of the picture box within the guiMAP array

            if (guiMAP[x, y].Image != Properties.Resources.Grass)//Checks to see if the picturebox clicked on is not grass
            {
                for (int i = 0; i < M.UnitsOnMap.Length; i++)//A loop to check every unit
                {
                    if (M.UnitsOnMap[i] != null)
                    {
                        if (M.UnitsOnMap[i].GetType() == typeof(MeleeUnit))
                        {
                            MeleeUnit convertUnit = (MeleeUnit)M.UnitsOnMap[i];
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
                        else if (M.UnitsOnMap[i].GetType() == typeof(RangedUnit))
                        {
                            RangedUnit convertUnit = (RangedUnit)M.UnitsOnMap[i];
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
                        else if (M.UnitsOnMap[i].GetType() == typeof(BarbarianMelee))
                        {
                            BarbarianMelee convertUnit = (BarbarianMelee)M.UnitsOnMap[i];
                            if (convertUnit.XPos == x && convertUnit.YPos == y)
                            {
                                Info.BackColor = System.Drawing.Color.Black;
                                Info.Text = convertUnit.toString();
                            }
                        }
                        else
                        {
                            BarbarianRanged convertUnit = (BarbarianRanged)M.UnitsOnMap[i];
                            if (convertUnit.XPos == x && convertUnit.YPos == y)
                            {
                                Info.BackColor = System.Drawing.Color.Black;
                                Info.Text = convertUnit.toString();
                            }
                        }
                    }
                }

                for (int b = 0; b < M.BuildingsOnMap.Length; b++)
                {
                    if (M.BuildingsOnMap[b].GetType() == typeof(ResourceBuilding))
                    {
                        ResourceBuilding newResource = (ResourceBuilding)M.BuildingsOnMap[b];
                        if (newResource.XPos == x && newResource.YPos == y)
                        {
                            if (newResource.Faction == "Red")
                            {
                                Info.BackColor = System.Drawing.Color.Red;
                            }
                            else Info.BackColor = System.Drawing.Color.Blue;
                            Info.Text = newResource.toString();
                        }
                    }
                    else
                    {
                        if (M.BuildingsOnMap[b].GetType() == typeof(FactoryBuilding))
                        {
                            FactoryBuilding newFactory = (FactoryBuilding)M.BuildingsOnMap[b];
                            if (newFactory.XPos == x && newFactory.YPos == y)
                            {
                                if (newFactory.Faction == "Red")
                                {
                                    Info.BackColor = System.Drawing.Color.Red;
                                }
                                else Info.BackColor = System.Drawing.Color.Blue;
                                Info.Text = newFactory.toString();
                            }
                        }
                    }
                }
            }
        }

        public void UserComponentSetup(Form active) //This method is used to add components to the active form
        {
            Info = new TextBox(); //Creates a TextBox
            Info.Location = new System.Drawing.Point(726, 478);
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

            RedResourceCounter = new Label(); //Creates a Label
            RedResourceCounter.Location = new System.Drawing.Point(726, 140);
            RedResourceCounter.Size = new System.Drawing.Size(200, 40);
            RedResourceCounter.Font = new System.Drawing.Font(Info.Font.FontFamily, 14);
            RedResourceCounter.ForeColor = System.Drawing.Color.Black;
            RedResourceCounter.BackColor = System.Drawing.Color.Transparent;
            RedResourceCounter.Text = "Red Resources: 0";
            active.Controls.Add(RedResourceCounter);

            BlueResourceCounter = new Label(); //Creates a Label
            BlueResourceCounter.Location = new System.Drawing.Point(726, 175);
            BlueResourceCounter.Size = new System.Drawing.Size(200, 40);
            BlueResourceCounter.Font = new System.Drawing.Font(Info.Font.FontFamily, 14);
            BlueResourceCounter.ForeColor = System.Drawing.Color.Black;
            BlueResourceCounter.BackColor = System.Drawing.Color.Transparent;
            BlueResourceCounter.Text = "Blue Resources: 0";
            active.Controls.Add(BlueResourceCounter);

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

            Save = new Button(); //Creates a Button
            Save.Location = new System.Drawing.Point(726, 215);
            Save.Size = new System.Drawing.Size(260, 40);
            Save.Text = "Save Game";
            Save.Font = new System.Drawing.Font(Info.Font.FontFamily, 12);
            Save.Click += new EventHandler(this.Save_Click);
            active.Controls.Add(Save);

            Load = new Button(); //Creates a Button
            Load.Location = new System.Drawing.Point(726, 255);
            Load.Size = new System.Drawing.Size(260, 40);
            Load.Text = "Load Game";
            Load.Font = new System.Drawing.Font(Info.Font.FontFamily, 12);
            Load.Click += new EventHandler(this.Load_Click);
            active.Controls.Add(Load);

            NewGame = new TextBox(); //Creates a TextBox
            NewGame.Location = new System.Drawing.Point(400, 250);
            NewGame.Size = new System.Drawing.Size(260, 40);
            NewGame.Font = new System.Drawing.Font(Info.Font.FontFamily, 12);
            NewGame.Visible = false;
            NewGame.Enabled = false;
            active.Controls.Add(NewGame);

            //newGameButton = new Button(); //Creates a Button
            //newGameButton.Location = new System.Drawing.Point(400, 350);
            //newGameButton.Size = new System.Drawing.Size(260, 40);
            //newGameButton.Text = "Start Game";
            //newGameButton.Font = new System.Drawing.Font(Info.Font.FontFamily, 12);
            //newGameButton.Visible = false;
            //newGameButton.Enabled = false;
            //newGameButton.Click += new EventHandler(this.NewGame_Click);
            //active.Controls.Add(newGameButton);
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

        void Save_Click(object sender, System.EventArgs e) //This is the onclick event for the save button
        {
            try
            {
                BinaryFormatter b = new BinaryFormatter();
                FileStream fsOUT = new FileStream("GameEngine.dat", FileMode.Create, FileAccess.Write, FileShare.None);

                using (fsOUT)
                {
                    b.Serialize(fsOUT, M);
                    MessageBox.Show("Gameplay database saved...");
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error Occured" + ex.Message);
            }
            catch (UnauthorizedAccessException exc)
            {
                MessageBox.Show("File inaccessible, make sure it isnt read-only:" + exc.Message);
            }
        }

        void Load_Click(object sender, System.EventArgs e) //This is the onclick event for the load button
        {
            try
            {
                BinaryFormatter b = new BinaryFormatter();
                FileStream fsIN = new FileStream("GameEngine.dat", FileMode.Open, FileAccess.Read, FileShare.None);

                using (fsIN)
                {
                    Map TempClass = (Map)b.Deserialize(fsIN);
                    M = TempClass;

                    Active.Controls.Clear();
                    CreateMap(Active);
                    UserComponentSetup(Active);

                    if (M.Seconds < 10)
                    {
                        GameTimeDisplay.Text = "Time: " + M.Minutes.ToString() + ":0" + M.Seconds.ToString();
                    }
                    else GameTimeDisplay.Text = "Time: " + M.Minutes.ToString() + ":" + M.Seconds.ToString();
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error Occured, no file:" + ex.Message);
            }
        }

        public void MoveUnit(Unit Soldier, int X, int Y, int pX, int pY) //This method is used to give the Unit a new X and Y position as well as change its position on the guiMAP
        //Parameters: X holds the new x position, Y holds the new Y position, pX holds the old X position and pY holds the old Y position
        //pX and pY are needed to changed the units old position back to grass on the guiMAP
        {
            if (Soldier.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit convertUnit = (MeleeUnit)Soldier;
                convertUnit.newPos(X, Y); //Assigns the converted unit with a new X and Y position

                if (Y < 20 && Y >= 0 && X < 20 && X >= 0)
                {
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
                }
            }
            else if (Soldier.GetType() == typeof(RangedUnit))
            {
                RangedUnit convertUnit = (RangedUnit)Soldier;
                convertUnit.newPos(X, Y); //Assigns the converted unit with a new X and Y position

                if (Y < 20 && Y >= 0 && X < 20 && X >= 0)
                {
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
                }
            }
            else if (Soldier.GetType() == typeof(BarbarianMelee))
            {
                BarbarianMelee convertUnit = (BarbarianMelee)Soldier;
                convertUnit.newPos(X, Y); //Assigns the converted unit with a new X and Y position

                if (Y < 20 && Y >= 0 && X < 20 && X >= 0)
                {
                    guiMAP[X, Y].Image = Properties.Resources.Barbarian_Sword;
                    guiMAP[pX, pY].Image = Properties.Resources.Grass;
                }
            }
            else
            {
                BarbarianRanged convertUnit = (BarbarianRanged)Soldier;
                convertUnit.newPos(X, Y); //Assigns the converted unit with a new X and Y position

                if (Y < 20 && Y >= 0 && X < 20 && X >= 0)
                {
                    guiMAP[X, Y].Image = Properties.Resources.Barbarian_Arrow;
                    guiMAP[pX, pY].Image = Properties.Resources.Grass;
                }
            }
        }

        public void updatePos(Unit Soldier, int X, int Y, int pX, int pY) //This method is used to give values to the MoveUnit method
        {
            MoveUnit(Soldier, X, Y, pX, pY);
        }

        public void endGameCondition()
        {
            int RedCounter = 0;
            int BlueCounter = 0;

            for (int i = 0; i < M.UnitsOnMap.Length; i++)
            {
                if (M.UnitsOnMap[i] != null)
                {
                    if (M.UnitsOnMap[i].GetType() == typeof(MeleeUnit))
                    {
                        MeleeUnit temp = (MeleeUnit)M.UnitsOnMap[i];
                        if (temp.Faction == "Red")
                        {
                            RedCounter++;
                        }
                        else BlueCounter++;
                    }
                    else if (M.UnitsOnMap[i].GetType() == typeof(RangedUnit))
                    {
                        RangedUnit temp = (RangedUnit)M.UnitsOnMap[i];
                        if (temp.Faction == "Red")
                        {
                            RedCounter++;
                        }
                        else BlueCounter++;
                    }
                }
            }

            //if (RedCounter == 0 && M.BuildingsOnMap[0])
            ResourceBuilding tempRed = (ResourceBuilding)M.BuildingsOnMap[0];
            ResourceBuilding tempBlue = (ResourceBuilding)M.BuildingsOnMap[3];

            if (RedCounter == 0 && tempRed.ResourceRemaining == 0)
            {
                gameTimer.Enabled = false;
                MessageBox.Show("The Elves are victorious");
                MessageBox.Show("Quit  and re-open to refresh the simulator");

                Pause.Enabled = false;
                Start.Enabled = false;
                Save.Enabled = false;
            }
            else if (BlueCounter == 0 && tempBlue.ResourceRemaining == 0)
            {
                gameTimer.Enabled = false;
                MessageBox.Show("The Orcs are victorious");
                MessageBox.Show("Quit  and re-open to refresh the simulator");

                Pause.Enabled = false;
                Start.Enabled = false;
                Save.Enabled = false;
            }
        }
    }
}
