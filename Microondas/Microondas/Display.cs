using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microondas
{
    class Display
    {
        private int minutes = 00;
        private int secunds = 00;
        private string contador;
        private int potencia;
        private string indicadorDePotencia = ".";
        private int contadorDePotencia = 0;

        public int Minutes { get => minutes; set => minutes = value;}
        public int Secunds { get => secunds; set => secunds = value;}
        public string Contador { get => contador; set => contador = value;}
        public int Potencia { get => potencia; set => potencia = value; }
        public string IndicadorDePotencia { get => indicadorDePotencia; set => indicadorDePotencia = value;}

        /**
         * nome: makeDisplay;
         * Descricao: Esse método irá construir o display timer, para a contagem;
         * return void;
         */
        public void makeDisplay(int tempo)
        {
            Minutes += tempo / 60;
            Secunds = tempo % 60;

            if (Minutes >= 2 && Secunds > 0)
            {
                setarDoisMinutos();
            }

            atualizarContador();
        }

        public bool contagemDePrepaparo()
        {
            if(IndicadorDePotencia.Contains(".") || indicadorDePotencia == "")
            {
                if (contadorDePotencia >= Potencia)
                {
                    contadorDePotencia = 0;
                    IndicadorDePotencia = "";
                }
                else
                {
                    IndicadorDePotencia += ".";
                    contadorDePotencia++;
                }
            }

            if (Minutes == 0 && Secunds == 0)
            {
                IndicadorDePotencia = "";
                return true;
            }
            else
            {
                Secunds--;
                
                if (Secunds < 0)
                {
                    Secunds = 59;
                    Minutes--;

                }

                atualizarContador();
            }
            
            return false;
        }

        public void atualizarContador()
        {

            if (Minutes <= 2)
                if (Convert.ToString(Secunds).Length == 1)
                    Contador = "0" + Minutes + ":0" + Secunds + " " + IndicadorDePotencia;
                else
                    Contador = "0" + Minutes + ":" + Secunds + " " + IndicadorDePotencia;
            else
                setarDoisMinutos();
                
        }

        public void cancel()
        {
            Minutes = 00;
            Secunds = 00;
            indicadorDePotencia = "";
            atualizarContador();
        }

        public void setarDoisMinutos()
        {
            Minutes = 02;
            Secunds = 00;
        }
    }
}
