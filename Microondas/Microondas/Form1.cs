using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microondas
{
    public partial class Form1 : Form
    {
        private Display Display;
        private string tempo;

        public string Tempo { get => tempo; set => tempo = value;}
        public Form1()
        {
            InitializeComponent();
        }

        private void adicionarTempo(object sender, EventArgs e)
        {
            Display = new Display();
            Button btn = (Button)sender;
            Tempo += btn.Text;
            Display.separarMinutosESegundos(Convert.ToInt32(Tempo));
            this.mostarTempo();
        }

        private void mostarTempo()
        {
            counter.Text = Display.Counter;
        }
    }
}
