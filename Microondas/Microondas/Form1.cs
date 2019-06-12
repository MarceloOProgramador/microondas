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

        /**
         * Nome: Iniciar
         * Descricao: Metodo responsavel pela iniciação do aquecimento
         * return void
         */
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

        /**
         * Nome: adcionarTempo
         * Params: Object sender, Event e
         * Descricao: Metodo responsável por pegar o valor digitado e enviar ao Display
         * return void;
         */
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

        /**
         * Nome: BtnIniciar_Click
         * Params: Object sender, Event e
         * Descricao: Metodo disparado ao clicar em iniciar
         * return void;
         */
        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            iniciar();
        }

        /**
         * Nome: ConcatenarTempo
         * Params: int numeroSelecionado
         * Descricao: Metodo responsável por concatenar os valores informados pelo usuário
         * return void
         */
        private void concatenarTempo(int numeroSelecionado)
        {
            if(Tempo.Length < 4)
            {
                Tempo += Convert.ToString(numeroSelecionado);
            }
            checarTempo();
        }

        /**
         * Nome: Timer1_Tick
         * Params: Object sender, Event e
         * Descricao: Metodo disparado por cada tick ou segundo do timer
         * return void
         */
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

        /**
         * Nome: BtnCancelar_Click
         * Params: Object sender, Event e
         * Descricao: Metodo responsável por cancelar a contagem e resetar os dados
         * return void
         */
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            resetar();
            timer1.Enabled = false;
            pausado = false;
            Display.cancel();
            atualizarPotencia();
            atualizarContador();
        }

        /**
         * Nome: atualizarContador
         * Descricao: Metodo responsável por atualizar o Display mostrado ao usuário
         * return void
         */
        private void atualizarContador()
        {
            counter.Text = Display.Contador;
        }

        /**
         * Nome: resetar
         * Descricao: Metodo responsavel por setar os valores padrões as propriedades
         * return void
         */
        private void resetar()
        {
            Tempo = "";
            Potencia = 10;
        }

        /**
         * Nome: BtnPausar_Click
         * Params: Object sender, Event e
         * Descricao: Metodo responsável por pausar o timer
         * return void
         */
        private void BtnPausar_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            pausado = true;
            resetar();
        }

        /**
         * Nome: MaisPotencia_Click
         * Params: Object sender, Event e
         * Descricao: Metodo reponsável por adiciona potencia.
         * return void
         */
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

        /**
         * Nome: menosPotencia
         * Params: Object sender, Event e
         * Descricao: Metodo responsável por decrementar potência;
         * return void
         */
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

        /**
         * Nome: atualizarPotencia
         * Params: Object sender, Event e
         * Descricao: Metodo responsável atualizar a potencia do display
         * return void
         */
        private void atualizarPotencia()
        {
            Display.Potencia = Potencia;
            lb_potencia.Text = "Potência: " + Convert.ToString(Potencia);
        }

        /**
         * Nome: inicioRapido
         * Params: Object sender, Event e
         * Descricao: Metodo adicionará 30 segundo ao timer, e setará potencia = 8, caso o timer ja 
         * estaja rodando será somente adicionado os 30 segundos
         * return void
         */
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

        /**
         * Nome: checarTempo
         * Params: Object sender, Event e
         * Descricao: Metodo responsável por verificar se o tempo fornecido é válido
         * return void
         */
        private void checarTempo()
        {
            Display.makeDisplay(Convert.ToInt16(Tempo));
            atualizarContador();
        }

        /**
         * Nome: funcoesPreDefinidas
         * Params: Object sender, Event e
         * Descricao: Metodo responsavel por pegar os falores das funções de aquecimento pré-definidas
         * e setar os valores para o display
         * return void
         */
        private void funcoesPreDefinidas(object sender, EventArgs e)
        {
            Button opcao = (Button)sender;
            string []pratos = pratosPreDefinidos.Split(';');
            
            if(timer1.Enabled != true && !pausado)//Verificar se o ticker está ativado, e não esteja pausado
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

        /**
         * Nome: encontrarFuncaoPredefininida
         * Params: string prato
         * Descricao: Metodo responsável por encontrar a função de aquecimento desejada e setar 
         * os valores definidos
         * return bool prato_encontrado
         */
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
