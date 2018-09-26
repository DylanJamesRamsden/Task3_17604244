using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_POE
{
    [Serializable]
    class ResourceBuilding : Building
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

        public int Health
        {
            get { return base.health; }
            set { base.health = value; }
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

        private string resourcetype;

        public string ResourceType
        {
            get { return resourcetype; }
            set { resourcetype = value; }
        }

        private int resourcepersec;

        public int ResourcePerSecond
        {
            get { return resourcepersec; }
            set { resourcepersec = value; }
        }

        private int resourceremaining;

        public int ResourceRemaining
        {
            get { return resourceremaining; }
            set { resourceremaining = value; }
        }

        public ResourceBuilding(string rType, int perSec, int rRemaining, int xP, int yP, int HP, string fac, string sym)
        {
            ResourceType = rType;
            ResourcePerSecond = perSec;
            ResourceRemaining = rRemaining;

            XPos = xP;
            YPos = yP;
            Health = HP;
            Faction = fac;
            Symbol = sym;
        }

        public override bool isDead() //isDead works off of mineral depletion
        {
            if (Health <= 0 || ResourceRemaining <= 0) //If there are no more minerals, the building is unfunctional/dead
            {
                Health = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string toString() //Returns the buildings information as a string
        {
            string Temp = "";
            Temp += "Resource Building: \r\n";
            Temp += "Resource type: " + ResourceType + "\r\n";
            Temp += "X position: " + XPos.ToString() + "\r\n";
            Temp += "Y position: " + YPos.ToString() + "\r\n";
            Temp += "HP: " + Health.ToString() + "\r\n";
            Temp += "Faction: " + Faction + "\r\n";
            Temp += "Symbol: " + Symbol + "\r\n";
            Temp += "Resources remaining: " + ResourceRemaining.ToString() + "\r\n";
            Temp += "Resources/sec: " + ResourcePerSecond.ToString() + "\r\n";

            return Temp;
        }

        public int GenerateResources() //This method is used to generate resources
        {
            //A few checks are done to ensure the right amount of resources are returned
            if (ResourceRemaining >= ResourcePerSecond)
            {
                ResourceRemaining = ResourceRemaining - ResourcePerSecond;
                return ResourcePerSecond;
            }
            else if (ResourceRemaining < ResourcePerSecond && ResourceRemaining > 0)
            {
                int amountLeft = 0;
                switch (ResourceRemaining)
                {
                    case 4:
                        amountLeft = 4;
                        break;
                    case 3:
                        amountLeft = 3;
                        break;
                    case 2:
                        amountLeft = 2;
                        break;
                    case 1:
                        amountLeft = 1;
                        break;
                }
                ResourceRemaining = 0;
                return amountLeft;
            }
            else
            {
                return 0; //Returns 0 if there are no more resources to return
            }
        }
    }
}
