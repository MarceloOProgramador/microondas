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
        private string counter;

        public int Minutes { get => minutes; set => minutes = value;}
        public int Secunds { get => secunds; set => secunds = value;}
        public string Counter { get => counter; set => counter = value;}

        /**
         * nome: makeDisplay;
         * Descricao: Esse método irá construir o display timer, para a contagem;
         * return string Counter
         */
        public void makeDisplay()
        {
            Counter = Convert.ToString(Minutes) + ':' + Convert.ToString(Secunds);            
        }

        /**
         * Nome: separarMinutosESegundos;
         * Descricao: Esse método irá pegar a assinatura passada, e em cima desse valor 
         * irá separar os minutos e os segundos passando-os para o metodo makeDisplay();
         * return void;
         */
        public void separarMinutosESegundos(int numeroAdicionar)
        {
            if (numeroAdicionar > 60) 
            {
                Minutes = numeroAdicionar / 60;
                Secunds = numeroAdicionar % 60;
            }
            else
            {
                Secunds = numeroAdicionar;
            }
            this.makeDisplay();
        }
    }
}
