using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporizador
{
    public class Pais
    {
        public Pais()
        {
        }

        public Pais(string nom, int diferencia_horaria, bool signo)
        {
            this.nom = nom;
            this.diferencia_horaria = diferencia_horaria;
            this.signo = signo;
        }

        public String nom { get; set; }

        public int diferencia_horaria { get; set; }
       
        public Boolean signo { get; set; }


    }
}
