using System; //Dylan James Ramsden 17604244
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_POE
{
    [Serializable]
    abstract class Unit //This class cannot be called directly
        //Defines what all its sub-classes must have in place
        //No implementations, just definitions
    {
        protected string name; //Holds the name of the unit

        protected int xpos; //Holds the units x position on the map

        protected int ypos; //Holds the units y position on the map

        protected int speed; //Holds the units running speed, this is only used when the unit is running away from combat

        protected int health; //Holds the units current HP

        protected int attack; //Holds the units attack damage

        protected int attackRange; //Holds the units attack range, varies if the unit is melee or ranged

        protected bool isalive; //Holds the units current alive status; alive or dead

        protected String faction; //Holds the units faction

        protected bool hasTurned; //Holds a boolean which states whether the unit has changed alliances or not

        protected String symbol; //Name of the picture file

        protected int unitcost; //Holds the production cost of the unit

        abstract public void newPos(int xP, int yP); //Assigns the unit a new x or y position

        abstract public void combatWithEnemy(Unit Enemy); //This method is used to carry out combat between the unit and its closest enemy

        abstract public bool inRange(Unit Enemy); //This method is used to check if the unit is in range to attack the closest enemy

        abstract public Unit closestUnit(Unit[] MapOfUnits);  //This method is used to obtain the closest enemy

        //abstract public Building closestBuilding(Building[] MapOfBuildings);

        abstract public void isDead(); //This method is used to assign the units current alive status

        abstract public string toString(); //This method builds the units information into a string
    }
}
