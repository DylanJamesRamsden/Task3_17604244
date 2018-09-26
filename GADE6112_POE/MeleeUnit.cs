using System; //Dylan James Ramsden 17604244
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADE6112_POE
{
    [Serializable]
    class MeleeUnit : Unit
    {
        public string Name
        {
            get { return base. name; }
            set { base.name = value; }
        }

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

        public bool HasTurned
        {
            get { return base.hasTurned; }
            set { base.hasTurned = value; }
        }

        public string Symbol
        {
            get { return base.symbol; }
            set { base.symbol = value; }
        }

        public int UnitCost
        {
            get { return base.unitcost; }
            set { base.unitcost = value; }
        }

        public MeleeUnit(string uname, int xP, int yP, String unitFaction, string unitSymbol) //Constructor to set the Melee units stats
        {
            Name = uname;
            XPos = xP;
            YPos = yP;
            Health = 100; //All units start off with 100 HP, used to balance the game
            Speed = 1;
            Attack = 25;
            AttackRange = 1;
            Faction = unitFaction; //Faction is either red or blue
            HasTurned = false;
            Symbol = unitSymbol; //Doesnt hold the image of the unit but rather a text representation of what it looks like on the map
            IsAlive = true; //a unit always atarts alive
            UnitCost = 6; //How mant resources it costs to spawn this type of unit
        }

        public override void newPos(int xP, int yP) //Updates the position of this unit
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
            else if (Enemy.GetType() == typeof(RangedUnit))
            {
                RangedUnit convertEnemy = (RangedUnit)Enemy;
                convertEnemy.Health = convertEnemy.Health - Attack;

                if (convertEnemy.Health <= 0)
                {
                    convertEnemy.isDead(); // Calls is dead method to change the enemies isAlive status
                }
            }
            else if (Enemy.GetType() == typeof(BarbarianMelee))
            {
                BarbarianMelee convertEnemy = (BarbarianMelee)Enemy;
                convertEnemy.Health = convertEnemy.Health - Attack;

                if (convertEnemy.Health <= 0)
                {
                    convertEnemy.isDead(); // Calls is dead method to change the enemies isAlive status
                }
            }
            else
            {
                BarbarianRanged convertEnemy = (BarbarianRanged)Enemy;
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
                Distance = Math.Abs(XPos - convertEnemy.XPos) + (YPos - convertEnemy.YPos); //A distance formula is used to calculate the distance between the two
            }
            else if (Enemy.GetType() == typeof(RangedUnit))
            {
                RangedUnit convertEnemy = (RangedUnit)Enemy;
                Distance = Math.Abs(XPos - convertEnemy.XPos) + (YPos - convertEnemy.YPos);
            }
            else if (Enemy.GetType() == typeof(BarbarianMelee))
            {
                BarbarianMelee convertEnemy = (BarbarianMelee)Enemy;
                Distance = Math.Abs(XPos - convertEnemy.XPos) + (YPos - convertEnemy.YPos);
            }
            else
            {
                BarbarianRanged convertEnemy = (BarbarianRanged)Enemy;
                Distance = Math.Abs(XPos - convertEnemy.XPos) + (YPos - convertEnemy.YPos);
            }

            if (Distance <= AttackRange)
            {
                TargetInRange = true;
            }

            return TargetInRange; //Returns whether or not the enemy is in range to attack or not
        }

        public override void isDead()//Assigns the unit dead
        {
           if (Health <= 0)
            {
                isalive = false;
                Health = 0;
            }
        } 

        public override string toString() //Broken up to be more readible, collects all the units information
        {
            string Temp = "";
            Temp = "Unit type: Melee Unit \r\n";
            Temp = Temp + "Name: " + Name + "\r\n";
            Temp = Temp + "Faction: " + Faction + "\r\n";
            Temp = Temp + "HP: " + Health.ToString() + "\r\n";
            Temp = Temp + "Damage: " + Attack.ToString() + "\r\n";
            Temp = Temp + "Range: " + AttackRange.ToString() + "\r\n";
            Temp = Temp + "Speed: " + Speed.ToString() + "\r\n";
            Temp = Temp + "Symbol: " + Symbol.ToString() + "\r\n";
            return Temp;
        }

        public override Unit closestUnit(Unit[] MapOfUnits) //This method finds the closest enemy unit away from this unit
        {
            Unit ClosestEnemy = this;
            int Distance = 50;

            for (int i = 0; i < 18; i++)
            {
                if (MapOfUnits[i] != null)
                {
                    if (MapOfUnits[i].GetType() == typeof(MeleeUnit))
                    {
                        MeleeUnit Current = (MeleeUnit)MapOfUnits[i];
                        if (XPos != Current.XPos && YPos != Current.YPos && Current.Faction != Faction && Current.IsAlive == true)
                        {
                            if (Distance > DistanceTo(Current))
                            {
                                Distance = DistanceTo(Current);
                                ClosestEnemy = MapOfUnits[i];
                            }
                        }
                    }
                    else if (MapOfUnits[i].GetType() == typeof(RangedUnit))
                    {
                        RangedUnit Current = (RangedUnit)MapOfUnits[i];
                        if (XPos != Current.XPos && YPos != Current.YPos && Current.Faction != Faction && Current.IsAlive == true)
                        {
                            if (Distance > DistanceTo(Current))
                            {
                                Distance = DistanceTo(Current);
                                ClosestEnemy = MapOfUnits[i];
                            }
                        }
                    }
                    else if (MapOfUnits[i].GetType() == typeof(BarbarianMelee))
                    {
                        BarbarianMelee Current = (BarbarianMelee)MapOfUnits[i];
                        if (XPos != Current.XPos && YPos != Current.YPos && Current.Faction != Faction && Current.IsAlive == true)
                        {
                            if (Distance > DistanceTo(Current))
                            {
                                Distance = DistanceTo(Current);
                                ClosestEnemy = MapOfUnits[i];
                            }
                        }
                    }
                    else
                    {
                        BarbarianRanged Current = (BarbarianRanged)MapOfUnits[i];
                        if (XPos != Current.XPos && YPos != Current.YPos && Current.Faction != Faction && Current.IsAlive == true)
                        {
                            if (Distance > DistanceTo(Current))
                            {
                                Distance = DistanceTo(Current);
                                ClosestEnemy = MapOfUnits[i];
                            }
                        }
                    }
                }
            }
            return ClosestEnemy;
        }

        private int DistanceTo(Unit u) //This method calculates the distance between this unit and another one and then returns it as an int
        {
            if (u.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit m = (MeleeUnit)u;
                int d = Math.Abs(XPos - m.XPos) + Math.Abs(YPos - m.YPos);
                return d;
            }
            else if (u.GetType() == typeof(RangedUnit))
            {
                RangedUnit r = (RangedUnit)u;
                int d = Math.Abs(XPos - r.XPos) + Math.Abs(YPos - r.YPos);
                return d;
            }
            else if (u.GetType() == typeof(BarbarianMelee))
            {
                BarbarianMelee r = (BarbarianMelee)u;
                int d = Math.Abs(XPos - r.XPos) + Math.Abs(YPos - r.YPos);
                return d;
            }
            else
            {
                BarbarianRanged r = (BarbarianRanged)u;
                int d = Math.Abs(XPos - r.XPos) + Math.Abs(YPos - r.YPos);
                return d;
            }
        }
    }

    
}

