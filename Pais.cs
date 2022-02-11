using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporizador
{
    /// <summary>
    /// Aquesta classe té els elements necessaris per a gestionar els Països
    /// </summary>
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
        /// <summary>
        /// Té les variables necessàries definides per a cada País.
        /// </summary>
        public String nom { get; set; }

        public int diferencia_horaria { get; set; }
       
        public Boolean signo { get; set; }


    }
}
