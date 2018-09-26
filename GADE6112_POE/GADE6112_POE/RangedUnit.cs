using System; //Dylan James Ramsden 17604244
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADE6112_POE
{
    class RangedUnit : Unit
    {
        public int XPos
        {
            get { return base.xpos; }
            set { base.xpos = value; }
        }

        public int YPos
        {
            get { return base.ypos; }
            set { base.ypos = value; }
        }

        public int Speed
        {
            get { return base.speed; }
            set { base.speed = value; }
        }

        public int Health
        {
            get { return base.health; }
            set { base.health = value; }
        }

        public int Attack
        {
            get { return base.attack; }
            set { base.attack = value; }
        }

        public int AttackRange
        {
            get { return base.attackRange; }
            set { base.attackRange = value; }
        }

        public bool IsAlive
        {
            get { return base.isalive; }
            set { base.isalive = value; }
        }

        public string Faction
        {
            get { return base.faction; }
            set { base.faction = value; }
        }

        public string Symbol
        {
            get { return base.symbol; }
            set { base.symbol = value; }
        }

        int Distance = 0;

        public RangedUnit(int xP, int yP, String unitFaction, string unitSymbol) //Constructor to set the Melee units stats
        {
            XPos = xP;
            YPos = yP;
            Health = 100;
            Speed = 3;
            Attack = 50;
            AttackRange = 3; //This can change
            Faction = unitFaction;
            Symbol = unitSymbol;
            IsAlive = true;
        }

        public override void newPos(int xP, int yP)
        {
            XPos = xP;
            YPos = yP;
        }

        public override void combatWithEnemy(Unit Enemy) //Pass through unit object :0
        {
            if (Enemy.GetType() == typeof(MeleeUnit)) //This cast is used the convert a unit into its current class, this allows for the units properties to be used and called
            {
                MeleeUnit convertEnemy = (MeleeUnit)Enemy;
                convertEnemy.Health = convertEnemy.Health - Attack;//Enemy's health is minuseed by the units attack

                if (convertEnemy.Health <= 0)
                {
                    convertEnemy.isDead(); // Calls is dead method to change the enemies isAlive status
                }
            }
            else
            {
                RangedUnit convertEnemy = (RangedUnit)Enemy;
                convertEnemy.Health = convertEnemy.Health - Attack;

                if (convertEnemy.Health <= 0)
                {
                    convertEnemy.isDead(); // Calls is dead method to change the enemies isAlive status
                }
            }
        }

        public override bool inRange(Unit Enemy) //Returns a bool, if the enemy is in range or not
        {
            int Distance = 0; //A variable to store the enemies current distance from the unit
            bool TargetInRange = false; //A bool to store if the target is in attack range of unit

            if (Enemy.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit convertEnemy = (MeleeUnit)Enemy;
                Distance = Math.Abs(XPos - convertEnemy.XPos) + (YPos - convertEnemy.YPos); //A distance formula is used to calculate the distance between the two
            }
            else
            {
                RangedUnit convertEnemy = (RangedUnit)Enemy;
                Distance = Math.Abs(XPos - convertEnemy.XPos) + (YPos - convertEnemy.YPos);
            }

            if (Distance <= AttackRange) //Checks if the distance is less than or equal to the attack range of the unit, if it is then the unit is able to attack
            {
                TargetInRange = true;
            }

            return TargetInRange;
        }

        public override void isDead()
        {
            if (Health <= 0)
            {
                isalive = false; //Assigns the unit dead if their health is or below 0
                Health = 0;
            }
        }

        public override string toString() //Broken up to be more readible
        {
            string Temp = "";
            Temp = "Unit type: Ranged Unit \r\n";
            Temp = Temp + "Faction: " + Faction + "\r\n";
            Temp = Temp + "HP: " + Health.ToString() + "\r\n";
            Temp = Temp + "Damage: " + Attack.ToString() + "\r\n";
            Temp = Temp + "Range: " + AttackRange.ToString() + "\r\n";
            Temp = Temp + "Speed: " + Speed.ToString() + "\r\n";
            Temp = Temp + "Symbol: " + Symbol.ToString() + "\r\n";
            return Temp; //Returns all the units information as a string
        }

        public override Unit closestUnit(Unit[] MapOfUnits) //Returns a unit object, this being the closest enemy to the unit
        {
            Unit ClosestEnemy = this; //Assigned the unit to ensure it is not null when the loop starts
            int count = 0; //Used to instantiate first distance, this ensures that the unit isnt taken as the closest enemy

            for (int i = 0; i < 12; i++) //A loop to check through every unit
            {
                if (MapOfUnits[i].GetType() == typeof(MeleeUnit))
                {
                    MeleeUnit Current = (MeleeUnit)MapOfUnits[i];
                    if (XPos != Current.XPos && YPos != Current.YPos && Current.Faction != Faction && Current.IsAlive == true)
                    //This ensures the unit(this) isnt checked (it will be found as the closest enemy) and if the current unit is an enemy. The if statement also checks if the unit being checked is alive
                    {
                        if (count == 0)
                        {
                            Distance = Math.Abs(XPos - Current.XPos) + Math.Abs(YPos - Current.YPos); //Calculates the distance between the Unit and the current unit being checked
                            ClosestEnemy = MapOfUnits[i];
                            count++;
                        }
                        else
                        {
                            if (Distance > Math.Abs(XPos - Current.XPos) + Math.Abs(YPos - Current.YPos)) //This if statement checks if the new distance is less than the previous distance, if it is then it will overwrite the previous distance
                            {
                                Distance = Math.Abs(XPos - Current.XPos) + Math.Abs(YPos - Current.YPos);
                                ClosestEnemy = MapOfUnits[i]; //This stores the unit being checked if the distance is less than the previous distance
                            }
                        }
                    }
                }
                else
                {
                    RangedUnit Current = (RangedUnit)MapOfUnits[i];
                    if (XPos != Current.XPos && YPos != Current.YPos && Current.Faction != Faction && Current.IsAlive == true)
                    {
                        if (count == 0)
                        {
                            Distance = Math.Abs(XPos - Current.XPos) + Math.Abs(YPos - Current.YPos);
                            ClosestEnemy = MapOfUnits[i];
                            count++;
                        }
                        else
                        {
                            if (Distance > Math.Abs(XPos - Current.XPos) + Math.Abs(YPos - Current.YPos))
                            {
                                Distance = Math.Abs(XPos - Current.XPos) + Math.Abs(YPos - Current.YPos);
                                ClosestEnemy = MapOfUnits[i];
                            }
                        }
                    }
                }
            }
            return ClosestEnemy; //Returns the closest enemy unit
        }
    }
}
