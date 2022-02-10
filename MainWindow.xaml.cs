using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Temporizador
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Timers.Timer temporizador;
        private TimeSpan tAntes;
        private TimeSpan tDespues;
        private Boolean Despertador_Activat = true;
        private int prova = 0;
        private String hora_despertador_fin = "";
        private Boolean alarma_sonant_bool = false;
        public MainWindow()
        {
            InitializeComponent();
            CrearTemporizador();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tAntes = DateTime.Now.TimeOfDay;
            temporizador.Interval = 1; // milisegundo
        }

        private void CrearTemporizador()
        {
            temporizador = new System.Timers.Timer();
            temporizador.Interval = 1000;
            temporizador.Elapsed += new System.Timers.ElapsedEventHandler(Temporizador_Elapsed);
            temporizador.Enabled = true;
        }

        void Temporizador_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //etHora.Content = DateTime.Now.ToLongTimeString();
            //Dispatcher.BeginInvoke((Action)delegate() { etHora.Content = DateTime.Now.ToLongTimeString(); });
            Dispatcher.BeginInvoke(new Action(ActualizarReloj));
            tDespues = DateTime.Now.TimeOfDay;
            System.Diagnostics.Debug.WriteLine((tDespues - tAntes).Milliseconds + " mseg.");
            tAntes = tDespues;
            
        }

        private void ActualizarReloj()
        {
            etHora.Text = DateTime.Now.ToLongTimeString(); // hora actual
            if (etHora.Text == ctDespertador.Text)
            {
                alarma_sonant.Visibility= Visibility.Visible;
                hora_despertador_fin = Afegix_5_min(ctDespertador.Text);
            } else if (etHora.Text == hora_despertador_fin)
            {
                alarma_sonant.Visibility = Visibility.Hidden;
            }
            //prova++;
            //ctDespertador.Text = prova.ToString();
        }

        private String Afegix_5_min(String hora)
        {
            var time = TimeSpan.Parse(hora); 
            var time5 = new TimeSpan(0, 0, 5, 0);
            var hora_despertador = DateTime.Today.Add(time);
            hora_despertador = hora_despertador.Add(time5);
            return hora_despertador.ToLongTimeString();

        }

        private int ObtenerHoraDespertador()
        {
            String[] arr_hor = ctDespertador.Text.Split(':');
            int hores = int.Parse(arr_hor[0]);
            int min = int.Parse(arr_hor[1]);
            int seg = int.Parse(arr_hor[2]);
            int tornar = (seg + min * 60 + hores * 3600) * 10000000;
            return tornar;
        }

        /*
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int a = UC1.valor_numero;
            MessageBoxResult messageBoxResult = MessageBox.Show(a.ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int b = UC2.valor_numero;
            MessageBoxResult messageBoxResult = MessageBox.Show(b.ToString());
        }
        */
        private void Acerca_de(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            info.Show();
        }

        private void Activa_despertador(object sender, RoutedEventArgs e)
        {
            ToggleButton button = sender as ToggleButton;
            
            if (button.IsEnabled)
            {
                Despertador_Activat = true;
            } else
            {
                Despertador_Activat = false;
            }

            /*button.Effect = new System.Windows.Media.Effects.DropShadowEffect()
            {
                BlurRadius = 10,
                ShadowDepth = 5
            };*/
        }

        private void Activa_Alarma(object sender, RoutedEventArgs e)
        {

        }


    }

}
