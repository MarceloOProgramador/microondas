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
        private string tempo = "";
        private int potencia = 10;
        private bool pausado = false;
        private string pratosPreDefinidos = "Carne;Frango;Peixe;Feijao;Batata;Macarrao;Arroz";

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
            atualizarPotencia();
            if (Display.Minutes != 00 || Display.Secunds != 00)
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
            if (!timer1.Enabled)
            {
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
            if(Tempo.Length < 4)
            {
                Tempo += Convert.ToString(numeroSelecionado);
            }
            checarTempo();
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
            pausado = false;
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
            pausado = true;
            resetar();
        }

        private void MaisPotencia_Click(object sender, EventArgs e)
        {
            if (Potencia < 10 && timer1.Enabled != true)
            {
                Potencia++;
            }
            else
                return;

            atualizarPotencia();
        }

        private void menosPotencia(object sender, EventArgs e)
        {
            if (Potencia > 1 && timer1.Enabled != true)
            {
                Potencia--;
            }
            else
                return;
            atualizarPotencia();
        }

        private void atualizarPotencia()
        {
            Display.Potencia = Potencia;
            lb_potencia.Text = "Potência: " + Convert.ToString(Potencia);
        }

        private void inicioRapido(object sender, EventArgs e)
        {
            Potencia = 8;
            Tempo = Convert.ToString(Display.Secunds + 30);
            checarTempo();
            if (timer1.Enabled == false)
            {
                atualizarPotencia();
                iniciar();
            }
            
        }

        private void checarTempo()
        {
            Display.makeDisplay(Convert.ToInt16(Tempo));
            atualizarContador();
        }

        private void funcoesPreDefinidas(object sender, EventArgs e)
        {
            Button opcao = (Button)sender;
            string []pratos = pratosPreDefinidos.Split(';');

            if(timer1.Enabled != true && !pausado)
            {
                if (pratos.Contains(opcao.Text))
                {
                    if (encontrarFuncaoPredefinida(opcao.Text))
                    {
                        checarTempo();
                        atualizarPotencia();
                        iniciar();
                    }
                }
                else
                {
                    counter.Text = "Não existe!";
                }
            }
        }

        private bool encontrarFuncaoPredefinida(string prato)
        {
            bool prato_encontrado = false;
            switch(prato)
            {
                case "Carne":
                    prato_encontrado = true;
                    Tempo = "120";
                    Potencia = 9;
                    Display.IndicadorDePotencia = "C";
                    break;
                case "Frango":
                    prato_encontrado = true;
                    Tempo = "60";
                    Potencia = 10;
                    Display.IndicadorDePotencia = "F";
                    break;
                case "Peixe":
                    prato_encontrado = true;
                    Tempo = "90";
                    Potencia = 5;
                    Display.IndicadorDePotencia = "F";
                    break;
                case "Feijao":
                    prato_encontrado = true;
                    Tempo = "60";
                    potencia = 4;
                    Display.IndicadorDePotencia = "B";
                    break;
                case "Batata":
                    prato_encontrado = true;
                    Tempo = "60";
                    potencia = 7;
                    Display.IndicadorDePotencia = "P";
                    break;
                case "Macarrao":
                    prato_encontrado = true;
                    Tempo = "30";
                    potencia = 8;
                    Display.IndicadorDePotencia = "M";
                    break;
                case "Arroz":
                    prato_encontrado = true;
                    Tempo = "120";
                    potencia = 4;
                    Display.IndicadorDePotencia = "A";
                    break;
                    
            }
            return prato_encontrado;
        }
    }
}
