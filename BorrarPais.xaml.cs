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
    /// Lógica de interacción para BorrarPais.xaml
    /// </summary>
    public partial class BorrarPais : Window
    {
        List<Pais> paises_aux;
        public BorrarPais()
        {
            InitializeComponent();
            update_paises();
            omplir_list();
            
        }

        private void update_paises()
        {
            paises_aux = ((MainWindow)Application.Current.MainWindow).Paises;
        }

        public void omplir_list()
        {
            LListaPaisosBorrar.Items.Clear();
            foreach (var item in paises_aux)
            {
                LListaPaisosBorrar.Items.Add(item.nom);
            }
        }

        private void Borrar_pais_list(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show((string)LListaPaisosBorrar.SelectedItem);
            if (LListaPaisosBorrar.SelectedItem != null)
            {
                ((MainWindow)Application.Current.MainWindow).BorrardePaisesList((string)LListaPaisosBorrar.SelectedItem);
                update_paises();
                omplir_list();
            } else
            {
                MessageBox.Show("No has seleccionat cap país");
            }
            
        }
    }
}
