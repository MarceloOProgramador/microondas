using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microondas
{
    class Display
    {
        private int minutes;
        private int secunds;
        private string contador;

        public int Minutes { get => minutes; set => minutes = value;}
        public int Secunds { get => secunds; set => secunds = value;}
        public string Contador { get => contador; set => contador = value;}

        /**
         * nome: makeDisplay;
         * Descricao: Esse método irá construir o display timer, para a contagem;
         * return void;
         */
        public void makeDisplay(int tempo)
        {
            if (Convert.ToString(tempo).Length <= 2)
            {
                if (Convert.ToInt16(tempo) >= 60)
                {
                    if((Minutes + Convert.ToInt16(tempo) / 60) < 2)
                    {
                        Minutes += Convert.ToInt16(tempo) / 60;
                        Secunds = Convert.ToInt16(tempo) % 60;
                    }
                    else
                    {
                        Minutes = 02;
                        Secunds = 00;
                    }
                }
                else
                {
                    if (Minutes >= 2)
                    {
                        Minutes = 02;
                        Secunds = 00;
                    }
                    else
                        Secunds = tempo;
                }  
            }
            else
            {
                if(Convert.ToString(tempo).Length == 3)
                {
                    Minutes += Convert.ToInt16(Convert.ToString(tempo).Substring(0, 1));
                    
                    if (Convert.ToInt16(Convert.ToString(tempo).Substring(1, 2)) >= 60)
                    {

                        if (Convert.ToInt16(tempo) / 60 < 2)
                        {
                            Minutes = Convert.ToInt16(tempo) / 60;
                            Secunds = Convert.ToInt16(tempo) % 60;
                        }
                        else
                        {
                            Minutes = 02;
                            Secunds = 00;
                        }
                    }
                    else
                        Secunds = Convert.ToInt16(Convert.ToString(tempo).Substring(1, 2));
                }
            }

            atualizarContador();
        }

        public bool contagemDePrepaparo()
        {
            if (Minutes == 0 && Secunds == 0)
            {
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
                if(Convert.ToString(Secunds).Length == 1)
                    Contador = "0" + Minutes + ":0" + Secunds;
                else
                    Contador = "0" + Minutes + ":" + Secunds;
            else
            {
                Minutes = 02;
                Secunds = 00;
            }
                
        }

        public void cancel()
        {
            Minutes = 00;
            Secunds = 00;
            atualizarContador();
        }
    }
}
