using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarme
{
    public partial class FormDesperta : Form
    {
        Timer timer;
        static string path = Directory.GetCurrentDirectory();

        public FormDesperta()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Stop();
            label1.Text = "Despertador desligado";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Start();
            label1.Text = "Despertador ligado e rodando...";
        }

        delegate void UpdateLabel(Label lbl, string value);

        void UpdateDataLabel(Label lbl, string value)
        {
            lbl.Text = value;
        }

        private void FormDesperta_Load(object sender, EventArgs e)
        {
            notifyIcon1.Icon = new Icon($@"{path}\favicon.ico");
            timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
               
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
          //  dateTimePicker.Value = dateTimePicker.Value.AddMilliseconds(timer.Interval);
            DateTime currentTimer = DateTime.Now;
            DateTime userTimer = dateTimePicker.Value;

            UpdateLabel upd = UpdateDataLabel;
            if (label1.InvokeRequired)
                Invoke(upd, label1, "Stop");

            if(currentTimer.Hour == userTimer.Hour &&
                currentTimer.Minute == userTimer.Minute &&
                currentTimer.Second == userTimer.Second)
            {
                timer.Stop();
                label1.Text = "Despertador desligado";
                playSimpleSound();
               // MessageBox.Show("Ring, Ring, Ring, Ring", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                notifyIcon1.ShowBalloonTip(2000, "Info", "O seu despertador tocou. " + txtMensagem.Text, ToolTipIcon.Info);
            }
        }

        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer($@"{path}\desperta.wav");
            simpleSound.Play();
        }
    }
}
