using System; //Dylan James Ramsden 17604244
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADE6112_POE
{
    class MeleeUnit : Unit
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

        public MeleeUnit(int xP, int yP, String unitFaction, string unitSymbol) //Constructor to set the Melee units stats
        {
            XPos = xP;
            YPos = yP;
            Health = 100; //All units start off with 100 HP, used to balance the game
            Speed = 1;
            Attack = 25;
            AttackRange = 1;
            Faction = unitFaction; //Faction is either red or blue
            Symbol = unitSymbol; //Doesnt hold the image of the unit but rather a text representation of what it looks like on the map
            IsAlive = true; //a unit always atarts alive
        }

        public override void newPos(int xP, int yP)
        {
            XPos = xP; //Assigns the units current x position with a new x position
            YPos = yP; //Assigns the units current y position with a new y position
        }

        public override void combatWithEnemy(Unit Enemy) //Pass through unit object
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

        public override bool inRange(Unit Enemy) //Checks to see if enemy is in range
        {
            int Distance = 0;
            bool TargetInRange = false;

            if (Enemy.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit convertEnemy = (MeleeUnit)Enemy;
                Distance = Math.Abs(XPos - convertEnemy.XPos) + (YPos - convertEnemy.YPos);
            }
            else
            {
                RangedUnit convertEnemy = (RangedUnit)Enemy;
                Distance = Math.Abs(XPos - convertEnemy.XPos) + (YPos - convertEnemy.YPos);
            }

            if (Distance <= AttackRange)
            {
                TargetInRange = true;
            }

            return TargetInRange;
        }

        public override void isDead()//Assigns the player dead
        {
           if (Health <= 0)
            {
                isalive = false;
                Health = 0;
            }
        } 

        public override string toString() //Broken up to be more readible
        {
            string Temp = "";
            Temp = "Unit type: Melee Unit \r\n";
            Temp = Temp + "Faction: " + Faction + "\r\n";
            Temp = Temp + "HP: " + Health.ToString() + "\r\n";
            Temp = Temp + "Damage: " + Attack.ToString() + "\r\n";
            Temp = Temp + "Range: " + AttackRange.ToString() + "\r\n";
            Temp = Temp + "Speed: " + Speed.ToString() + "\r\n";
            Temp = Temp + "Symbol: " + Symbol.ToString() + "\r\n";
            return Temp;
        }

        public override Unit closestUnit(Unit[] MapOfUnits) //need to do***
        {
            Unit ClosestEnemy = this;
            int count = 0; //Used to instantiate first distance

            for (int i = 0; i < 12; i++)
            {
                if (MapOfUnits[i].GetType() == typeof(MeleeUnit))
                {
                    MeleeUnit Current = (MeleeUnit)MapOfUnits[i];
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
            return ClosestEnemy;
        }
    }

    
}

