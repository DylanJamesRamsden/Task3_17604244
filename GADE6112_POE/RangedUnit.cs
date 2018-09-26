using System; //Dylan James Ramsden 17604244
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADE6112_POE
{
    [Serializable]
    class RangedUnit : Unit //Identical to that of the Melee Unit, thus is why there are no comments
    {
        public string Name
        {
            get { return base.name; }
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

        public RangedUnit(string uname, int xP, int yP, String unitFaction, string unitSymbol) 
        {
            Name = uname;
            XPos = xP;
            YPos = yP;
            Health = 100;
            Speed = 3;
            Attack = 50;
            AttackRange = 1; 
            Faction = unitFaction;
            HasTurned = false;
            Symbol = unitSymbol;
            IsAlive = true;
            unitcost = 8;
        }

        public override void newPos(int xP, int yP)
        {
            XPos = xP;
            YPos = yP;
        }

        public override void combatWithEnemy(Unit Enemy) 
        {
            if (Enemy.GetType() == typeof(MeleeUnit)) 
            {
                MeleeUnit convertEnemy = (MeleeUnit)Enemy;
                convertEnemy.Health = convertEnemy.Health - Attack;

                if (convertEnemy.Health <= 0)
                {
                    convertEnemy.isDead(); 
                }
            }
            else if (Enemy.GetType() == typeof(RangedUnit))
            {
                RangedUnit convertEnemy = (RangedUnit)Enemy;
                convertEnemy.Health = convertEnemy.Health - Attack;

                if (convertEnemy.Health <= 0)
                {
                    convertEnemy.isDead(); 
                }
            }
            else if (Enemy.GetType() == typeof(BarbarianMelee))
            {
                BarbarianMelee convertEnemy = (BarbarianMelee)Enemy;
                convertEnemy.Health = convertEnemy.Health - Attack;

                if (convertEnemy.Health <= 0)
                {
                    convertEnemy.isDead(); 
                }
            }
            else
            {
                BarbarianRanged convertEnemy = (BarbarianRanged)Enemy;
                convertEnemy.Health = convertEnemy.Health - Attack;

                if (convertEnemy.Health <= 0)
                {
                    convertEnemy.isDead(); 
                }
            }
        }

        public override bool inRange(Unit Enemy) 
        {
            int Distance = 0; 
            bool TargetInRange = false; 

            if (Enemy.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit convertEnemy = (MeleeUnit)Enemy;
                Distance = Math.Abs(XPos - convertEnemy.XPos) + (YPos - convertEnemy.YPos); 
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

            return TargetInRange;
        }

        public override void isDead()
        {
            if (Health <= 0)
            {
                isalive = false; 
                Health = 0;
            }
        }

        public override string toString() 
        {
            string Temp = "";
            Temp = "Unit type: Ranged Unit \r\n";
            Temp = Temp + "Name: " + Name + "\r\n";
            Temp = Temp + "Faction: " + Faction + "\r\n";
            Temp = Temp + "HP: " + Health.ToString() + "\r\n";
            Temp = Temp + "Damage: " + Attack.ToString() + "\r\n";
            Temp = Temp + "Range: " + AttackRange.ToString() + "\r\n";
            Temp = Temp + "Speed: " + Speed.ToString() + "\r\n";
            Temp = Temp + "Symbol: " + Symbol.ToString() + "\r\n";
            return Temp; 
        }

        public override Unit closestUnit(Unit[] MapOfUnits) 
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

        private int DistanceTo(Unit u)
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
