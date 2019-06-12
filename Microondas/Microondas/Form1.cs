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
        private int potencia = 10;

        public string Tempo { get => tempo; set => tempo = value;}
        public int Potencia { get => potencia; set => potencia = value; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Display = new Display();
            atualizarPotencia();
        }

        private void iniciar()
        {
            if (Display.Minutes <= 2 && (Display.Secunds >= 00 || Display.Secunds == 00))
            {
                timer1.Enabled = true;
            }
            else
            {
                counter.Text = "Inválido";
            }
        }

        private void adicionarTempo(object sender, EventArgs e)
        {
            if (timer1.Enabled != true)
            {
                Display = new Display();
                Button btn = (Button)sender;

                concatenarTempo(Convert.ToInt16(btn.Text));
            }
            else
                return;
        }

        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            iniciar();
        }

        private void concatenarTempo(int numeroSelecionado)
        {
            Tempo += Convert.ToString(numeroSelecionado);

            checarTempo(Tempo);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Display.contagemDePrepaparo())
            {
                counter.Text = "Aquecida!";
                timer1.Enabled = false;
                resetar();
                atualizarPotencia();
            }
            else
                atualizarContador();

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            resetar();
            timer1.Enabled = false;
            Display.cancel();
            atualizarPotencia();
            atualizarContador();
        }

        private void atualizarContador()
        {
            counter.Text = Display.Contador;
        }

        private void resetar()
        {
            Tempo = "";
            Potencia = 10;
        }

        private void BtnPausar_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            resetar();
        }

        private void MaisPotencia_Click(object sender, EventArgs e)
        {
            if (Potencia < 10)
            {
                Potencia++;
            }
            else
                return;

            atualizarPotencia();
        }

        private void menosPotencia(object sender, EventArgs e)
        {
            if (Potencia > 1)
            {
                Potencia--;
            }
            else
                return;
            atualizarPotencia();
        }

        private void atualizarPotencia()
        {
            lb_potencia.Text = "Potência: " + Convert.ToString(Potencia);
        }

        private void inicioRapido(object sender, EventArgs e)
        {
            bool inici_rapido = true;
            if(timer1.Enabled != true || inici_rapido)
            {
                Potencia = 8;
                checarTempo(Convert.ToString(Display.Secunds + 30));
                if (timer1.Enabled == false)
                {
                    atualizarPotencia();
                    iniciar();
                }
                resetar();
            }
        }

        private void checarTempo(string tempo)
        {
            Tempo = tempo;
            if (Convert.ToInt16(Tempo) <= 200)
            {
                Display.makeDisplay(Convert.ToInt16(Tempo));
                atualizarContador();
            }
            else
            {
                Display.Minutes = 02;
                Display.Secunds = 00;
            }
        }
    }
}
