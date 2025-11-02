using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogodaMemoria
{
    public partial class LevelSelect : Form
    {
        private SoundPlayer player;
        public LevelSelect()
        {
            InitializeComponent();

            player = new SoundPlayer(Properties.Resources.musica4);
            player.PlayLooping();
        }

        private void btnEasy_Click(object sender, EventArgs e)
        {
            Easy easy = new Easy();
            easy.Show();
            player.Stop();
            player = new SoundPlayer(Properties.Resources.musica);
            player.PlayLooping();
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            Normal normal = new Normal();
            normal.Show();
            player.Stop();
            player = new SoundPlayer(Properties.Resources.musica2);
            player.PlayLooping();
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            Hard hard = new Hard();
            hard.Show();
            player.Stop();
            player = new SoundPlayer(Properties.Resources.musica3);
            player.PlayLooping();
        }
    }
}
