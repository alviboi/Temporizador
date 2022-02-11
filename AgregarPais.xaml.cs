using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Temporizador
{
    /// <summary>
    /// Lógica de interacción para AgregarPais.xaml
    /// </summary>
    public partial class AgregarPais : Window
    {
        public AgregarPais()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Pais pais_aux = new Pais();

            if (pais_t.Text != "" && difhor_t.Text != "")
            {
                var isNumeric = int.TryParse(difhor_t.Text, out int _);
   
                if (isNumeric)
                {
                    pais_aux.nom = pais_t.Text;

                    pais_aux.signo = (bool)signo_t.IsChecked;

                    pais_aux.diferencia_horaria = int.Parse(difhor_t.Text);

                    ((MainWindow)System.Windows.Application.Current.MainWindow).Afegir_a_List(pais_aux);

                    MessageBox.Show("País afegit");

                    pais_t.Text = "";
                    signo_t.IsChecked = false;
                    difhor_t.Text = "";
                    //MainWindow.Afegir_a_List(pais_aux);
                } else
                {
                    MessageBox.Show("Diferencia horaria incorrecta");
                }
            } else
            {
                MessageBox.Show("Algun camp de text és incorrecte");
            }
            
        }

        private void Salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
