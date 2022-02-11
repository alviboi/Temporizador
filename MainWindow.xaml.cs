using System;
using System.Collections.Generic;
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
        public List <Pais> paises = new List <Pais> ();
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

        private void Activa_Despertador(object sender, RoutedEventArgs e)
        {
       
            if (Comprova_hora())
            {

                if ((bool)boto_despertador.IsChecked)
                {
                    Despertador_Activat = true;
                    ctDespertador.IsEnabled = false;
                    icona_activar.Visibility = Visibility.Visible;
                    icona_desactivar.Visibility = Visibility.Hidden;
                } else
                {
                    Despertador_Activat = false;
                    ctDespertador.IsEnabled = true;
                    icona_activar.Visibility = Visibility.Hidden;
                    icona_desactivar.Visibility = Visibility.Visible;
                }

                    /*button.Effect = new System.Windows.Media.Effects.DropShadowEffect()
                    {
                        BlurRadius = 10,
                        ShadowDepth = 5
                    };*/

            } else
            {
                MessageBox.Show("El format de l'hora no és correcte, ha de ser 00:00:00");
                boto_despertador.IsChecked = false;
            }
        }

        private bool Comprova_hora()
        {
            String[] hora = ctDespertador.Text.Split(':');


            var isNumeric1 = int.TryParse(hora[0], out int hores);
            var isNumeric2 = int.TryParse(hora[1], out int min);
            var isNumeric3 = int.TryParse(hora[2], out int seg);

            if (isNumeric1 && isNumeric2 && isNumeric3)
            {
                if (hores >= 0 && hores < 24 && min >= 0 && min < 60 && seg >= 0 && seg < 60)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } else
            {
                return false;
            }

      
        }

        private void Afegir_pais(object sender, RoutedEventArgs e)
        {
            AgregarPais agregarpais = new AgregarPais();
            agregarpais.Show();
        }

        public void Afegir_a_List(Pais a)
        {
            paises.Add(a);
        }

        private void menu_activat_Click(object sender, RoutedEventArgs e)
        {
            boto_despertador.IsChecked = true;
            Activa_Despertador(null, null);
        }

        private void menu_desactivat_Click(object sender, RoutedEventArgs e)
        {
            boto_despertador.IsChecked = false;
            Activa_Despertador(null, null);
        }
    }

}
