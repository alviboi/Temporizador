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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Temporizador
{
    /// <summary>
    /// Lógica de interacción para UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {


        public int valor_numero
        {
            get { return (int)GetValue(valor_numeroProperty); }
            set { SetValue(valor_numeroProperty, value); }
        }

       

        // Using a DependencyProperty as the backing store for valor_numero.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty valor_numeroProperty =
            DependencyProperty.Register("valor_numero", typeof(int), typeof(UserControl1), new PropertyMetadata(0));



        public UserControl1()
        {
            InitializeComponent();
            valor_numero = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            valor_numero = combo.SelectedIndex;
        }
    }
}
