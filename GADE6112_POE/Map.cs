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
    class Map
    {
        Random r = new Random();
        public Unit[] UnitsOnMap = new Unit[18]; //Holds the position of units on the map
        public Building[] BuildingsOnMap = new Building[6];

        //All components public because they are used in another class

        public int Minutes = 0;
        public int Seconds = 0;
        public int RedResources = 0;
        public int BlueResources = 0;

        public Map() //Map constructor. Generates a new map and displays it, as well as GUI components
        {
            GenerateNew();
        }

        public void GenerateNew() //This method assigns the UnitsOnMap array with new Units as well as spawns buildings
        {
            for (int Ranged = 0; Ranged < 6; Ranged++) //Generates ranged troops for each team
            {
                int rX = r.Next(0, 20); //Assigns the Unit with a random X position
                int ry = r.Next(0, 20); //ASsigns the Unit with a random Y position

                if (Ranged % 2 == 0 || Ranged == 0) //These if statments are used to determine which teams turn it is for a new unit
                {
                    UnitsOnMap[Ranged] = new RangedUnit("Mirkwood Archer",rX, ry, "Blue", "Blue_Arrow");
                }
                else
                {
                    UnitsOnMap[Ranged] = new RangedUnit("Ranger",rX, ry, "Red", "Red_Arrow");
                }
            }

            for (int Melee = 6; Melee < 12; Melee++) //Generates melee troops for each team
            {
                int rX = r.Next(0, 20);
                int ry = r.Next(0, 20);

                if (Melee % 2 == 0)
                {
                    UnitsOnMap[Melee] = new MeleeUnit("Elf",rX, ry, "Blue", "Blue_Sword");
                }
                else
                {
                    UnitsOnMap[Melee] = new MeleeUnit("Orc",rX, ry, "Red", "Red_Sword");
                }
            }

            for (int Barbarian = 12; Barbarian < 18; Barbarian++) //Generates units for an independant state, these will fight both teams
            {
                int rX = r.Next(0, 20);
                int ry = r.Next(0, 20);

                if (Barbarian % 2 == 0)
                {
                    UnitsOnMap[Barbarian] = new BarbarianMelee("Uneducated swordsman",rX, ry, "Independant State", "White_Sword");
                }
                else
                {
                    UnitsOnMap[Barbarian] = new BarbarianRanged("Uneducated Archer",rX, ry, "Independant State", "White_Arrow");
                }
            }

            for (int RedSpawn = 0; RedSpawn < 3; RedSpawn++) //This is used to spawn buildings for the red team
            {
                if (RedSpawn == 0)
                {
                    BuildingsOnMap[RedSpawn] = new ResourceBuilding("Gold", 2, 100, 0, 0, 100, "Red", "Red_Mine");
                }
                else if (RedSpawn == 1)
                {
                    int randomFactory = r.Next(0, 2);
                    if (randomFactory == 0)
                    {
                        BuildingsOnMap[RedSpawn] = new FactoryBuilding("Melee", 3, 3, 0, 2, 0 , 100, "Red", "Red_Factory");
                    }
                    else BuildingsOnMap[RedSpawn] = new FactoryBuilding("Ranged", 5, 3, 0, 2, 0, 100, "Red", "Red_Factory");
                }
                else
                {
                    int randomFactory = r.Next(0, 2);
                    if (randomFactory == 0)
                    {
                        BuildingsOnMap[RedSpawn] = new FactoryBuilding("Melee", 3, 0, 3, 0, 2, 100, "Red", "Red_Factory");
                    }
                    else BuildingsOnMap[RedSpawn] = new FactoryBuilding("Ranged", 5, 0, 3, 0, 2, 100, "Red", "Red_Factory");
                }
            }

            for (int BlueSpawn = 3; BlueSpawn < 6; BlueSpawn++) //This is used to spawn buildings for the blue team
            {
                if (BlueSpawn == 3)
                {
                    BuildingsOnMap[BlueSpawn] = new ResourceBuilding("Gold", 2, 100, 19, 19, 100, "Blue", "Blue_Mine");
                }
                else if (BlueSpawn == 4)
                {
                    int randomFactory = r.Next(0, 2);
                    if (randomFactory == 0)
                    {
                        BuildingsOnMap[BlueSpawn] = new FactoryBuilding("Melee", 3, 16, 19, 17, 19, 100, "Blue", "Blue_Factory");
                    }
                    else BuildingsOnMap[BlueSpawn] = new FactoryBuilding("Ranged", 3, 16, 19, 17, 19, 100, "Blue", "Blue_Factory");
                }
                else
                {
                    int randomFactory = r.Next(0, 2);
                    if (randomFactory == 0)
                    {
                        BuildingsOnMap[BlueSpawn] = new FactoryBuilding("Melee", 3, 19, 16, 19, 17, 100, "Blue", "Blue_Factory");
                    }
                    else BuildingsOnMap[BlueSpawn] = new FactoryBuilding("Ranged", 3, 19, 16, 19, 17, 100, "Blue", "Blue_Factory");
                }
            }
        } 
   
        public int getUnits() //This method is used to count the current number of units within the game
        {
            int Counter = 0;
            for (int i = 0; i < UnitsOnMap.Length; i++) //Checks through the Units array
            {
                if (UnitsOnMap[i] != null)
                {
                    Counter++;
                }
            }
            return Counter;
        }

        //Picture referencing
        //Royal Blue Sword Clip Art at Clker.com - vector clip art online, royalty free & public domain. 2018. Royal Blue Sword Clip Art at Clker.com - vector clip art online, royalty free & public domain. [ONLINE] Available at: http://www.clker.com/clipart-royal-blue-sword.html. [Accessed 15 August 2018].
        //Red Sword Clip Art at Clker.com - vector clip art online, royalty free & public domain. 2018. Red Sword Clip Art at Clker.com - vector clip art online, royalty free & public domain. [ONLINE] Available at: http://www.clker.com/clipart-red-sword-3.html. [Accessed 15 August 2018].
        //Red Bow And Arrow Clip Art at Clker.com - vector clip art online, royalty free & public domain. 2018. Red Bow And Arrow Clip Art at Clker.com - vector clip art online, royalty free & public domain. [ONLINE] Available at: http://www.clker.com/clipart-red-bow-and-arrow.html. [Accessed 15 August 2018].
        //Royal Blue Bow And Arrow Clip Art at Clker.com - vector clip art online, royalty free & public domain. 2018. Royal Blue Bow And Arrow Clip Art at Clker.com - vector clip art online, royalty free & public domain. [ONLINE] Available at: http://www.clker.com/clipart-royal-blue-bow-and-arrow.html. [Accessed 15 August 2018].
        //OpenGameArt.org. 2018. 30 Grass Textures (Tilable) | OpenGameArt.org. [ONLINE] Available at: https://opengameart.org/content/30-grass-textures-tilable. [Accessed 15 August 2018].\
        //VectorStock. 2018. Mine in mountain icon cartoon style Royalty Free Vector. [ONLINE] Available at: https://www.vectorstock.com/royalty-free-vector/mine-in-mountain-icon-cartoon-style-vector-16250667. [Accessed 07 September 2018]
        //Reference generated
        //Camping Tents, Cartoon, Tent, Camping PNG and Vector for Free Download. 2018. Camping Tents, Cartoon, Tent, Camping PNG and Vector for Free Download. [ONLINE] Available at: https://pngtree.com/freepng/camping-tents_2225385.html. [Accessed 07 September 2018].

    }
}
