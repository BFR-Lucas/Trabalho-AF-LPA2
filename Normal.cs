using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogodaMemoria
{
    public partial class Normal : Form
    {
        bool allowClick = false;
        PictureBox firstGuess;
        Random rnd = new Random();
        Timer clickTimer = new Timer();
        int time = 120;
        Timer timer = new Timer { Interval = 1000 };
        public Normal()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            InitializeComponent();

            this.FormClosed += Normal_FormClosed;
        }
        private void Normal_FormClosed(object sender, FormClosedEventArgs e)
        {

            timer.Stop();
            clickTimer.Stop();


            timer.Tick -= null;
            clickTimer.Tick -= null;


            timer.Dispose();
            clickTimer.Dispose();
        }
        private PictureBox[] pictureBoxes
        {
            get { return Controls.OfType<PictureBox>().ToArray(); }
        }
        private static IEnumerable<Image> images
        {
            get
            {
                return new Image[]
                {
                Properties.Resources.img1,
                Properties.Resources.img2,
                Properties.Resources.img3,
                Properties.Resources.img4,
                Properties.Resources.img5,
                Properties.Resources.img6,
                Properties.Resources.img7,
                Properties.Resources.img8,
                Properties.Resources.img9,
                Properties.Resources.img10,
                Properties.Resources.img11,
                Properties.Resources.img12,
                Properties.Resources.img13,
                Properties.Resources.img14,
                Properties.Resources.img15,
                Properties.Resources.img16,
                Properties.Resources.img17,
                Properties.Resources.img18,
                };
            }
        }
        private void startGameTimer()
        {
            timer.Start();
            timer.Tick += delegate
            {
                time--;
                if (time < 0)
                {
                    timer.Stop();
                    MessageBox.Show("Acabou o tempo!");
                    ResetImages();
                }

                int minutos = time / 60;
                int segundos = time % 60;

                // Atualiza o label formatado corretamente
                label2.Text = $"{minutos}:{segundos:D2}";
            };
        }
        private void ResetImages()
        {
            foreach (var pic in pictureBoxes)
            {
                pic.Tag = null;
                pic.Enabled = true;
            }
            HideImages();
            setRandomImages();
            time = 80;
            timer.Start();
        }
        private void HideImages()
        {
            foreach (var pic in pictureBoxes)
            {
                if (pic.Enabled)
                {
                    pic.Image = Properties.Resources.Prancheta_1;
                }
            }
        }
        private PictureBox GetFreeSlot()
        {
            int num;

            do
            {
                num = rnd.Next(0, pictureBoxes.Count());
            }
            while (pictureBoxes[num].Tag != null);
            return pictureBoxes[num];
        }
        private void setRandomImages()
        {
            foreach (var image in images)
            {
                GetFreeSlot().Tag = image;
                GetFreeSlot().Tag = image;
            }
        }
        private void CLICKTIMER_TICK(object sender, EventArgs e)
        {

            HideImages();

            allowClick = true;
            clickTimer.Stop();
        }

        private void clickImage(object sender, EventArgs e)
        {
            if (!allowClick)
                return;
            var pic = (PictureBox)sender;
            if (firstGuess == null)
            {
                firstGuess = pic;
                pic.Image = (Image)pic.Tag;
                return;
            }
            pic.Image = (Image)pic.Tag;

            if (pic.Image == firstGuess.Image && pic != firstGuess)
            {
                pic.Enabled = false;
                firstGuess.Enabled = false;
            }
            else
            {
                allowClick = false;
                clickTimer.Start();
            }

            firstGuess = null;
            if (pictureBoxes.Any(p => p.Enabled))
                return;

            timer.Stop();

            DialogResult result = MessageBox.Show("Voçê Ganhou!\n\nDeseja jogar novamente?", "Vitória", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                ResetImages();
            }
            else
            {
                this.Close();
            }
        }

        private void startGame(object sender, EventArgs e)
        {
            allowClick = true;
            setRandomImages();
            HideImages();
            startGameTimer();
            clickTimer.Interval = 1000;
            clickTimer.Tick += CLICKTIMER_TICK;
            button2.Enabled = false;
        }
    }
}
