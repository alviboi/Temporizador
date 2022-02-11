using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporizador
{
    /// <summary>
    /// Aquesa classe gestiona la lectura i escriptura d'un fitxer
    /// </summary>
    internal class Fichero
    {
        /// <summary>
        /// Escribim en un fitxer quan es passa la Llista de Països
        /// </summary>
        /// <param name="paisos"></param>
        public void Escriure_fitxer (List <Pais> paisos)
        {
            try
            {
                StreamWriter sw = new StreamWriter(@"C:\Users\Alfredo\source\repos\Temporizador\Fila.txt");
                foreach (var item in paisos)
                {
                    sw.WriteLine(item.nom+";"+item.diferencia_horaria+";"+item.signo);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }


        /// <summary>
        /// Llegim l'arxiu i passa les dades a una Llista de Països
        /// </summary>
        /// <returns></returns>
        public List<Pais> Llegir_arxiu()
        {
            List < Pais > ret_paisos = new List<Pais> ();
            String line;
            try
            {
                StreamReader sr = new StreamReader(@"C:\Users\Alfredo\source\repos\Temporizador\Fila.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    Pais aux = new Pais();
                    String[] lineas = line.Split(';');
                    aux.nom = lineas[0];
                    aux.diferencia_horaria = int.Parse(lineas[1]);
                    aux.signo = bool.Parse(lineas[2]);
                    ret_paisos.Add(aux);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Acabat de llegir paisos");
            }
            return ret_paisos;
        }
    }
}
