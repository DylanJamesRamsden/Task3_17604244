using System; //Dylan James Ramsden 17604244
using System.Collections.Generic; 
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADE6112_POE //tooX4
{
    public partial class frmGameMat : Form
    {
        GameEngine NewGame;

        public frmGameMat()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NewGame = new GameEngine(this, gameTimer); //
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NewGame.UnitAction();
        }
    }
}
