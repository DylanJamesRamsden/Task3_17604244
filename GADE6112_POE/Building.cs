using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_POE
{
    [Serializable]
    abstract class Building
    {
        protected int xpos; //X position of the building

        protected int ypos; //Y position of the building

        protected int health; //Health of the building

        protected string faction;

        protected string symbol; //WQhat symbol is used to display the building

        abstract public bool isDead(); //Holds a boolean stating whether the building is dead or not

        abstract public string toString();
    }
}
