using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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
        private List<Pais> paises = new List<Pais>();
        public Pais pais_seleccionat = null;
        SoundPlayer simpleSound;

        public List<Pais> Paises { get => paises; set => paises = value; }

        public MainWindow()
        {
            InitializeComponent();
            CrearTemporizador();
            simpleSound = new SoundPlayer(Properties.Resources.alarma);

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

            if (pais_seleccionat == null)
            {
                ctHoraPais.Text = DateTime.Now.ToLongTimeString(); // hora pais
            } else
            {
                if (pais_seleccionat.signo)
                {
                    ctHoraPais.Text = DateTime.Now.AddHours(-pais_seleccionat.diferencia_horaria).ToLongTimeString();
                }
                else
                {
                    ctHoraPais.Text = DateTime.Now.AddHours(pais_seleccionat.diferencia_horaria).ToLongTimeString();
                }
            }

            
            
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

        private void ObtenerHoraDespertador(object sender, RoutedEventArgs e)
        {
            String[] arr_hor = ctDespertador.Text.Split(':');
            int hores = int.Parse(arr_hor[0]);
            int min = int.Parse(arr_hor[1]);
            int seg = int.Parse(arr_hor[2]);
            long tornar = (seg + min * 60 + hores * 3600);
            MessageBox.Show("Número de pasos: "+tornar.ToString()+ "0000000");
            //MessageBox.Show(ctDespertador.Text);
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9|:]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Acerca_de(object sender, RoutedEventArgs e)
        {
            AboutBox1 info = new AboutBox1();

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
                    simpleSound.Stop();
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
            
            if (paises.Count > 0)
            {
                eliminar_menus_paisos(null, null);
            }
            Paises.Add(a);
            afegix_alMenu(null,null);

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

        private void afegix_alMenu(object sender, RoutedEventArgs e)
        {

  

            MenuItem newExistMenuItem = (MenuItem)this.MenuPaisos;
            MenuItem newMenuItemp = new MenuItem();
            newMenuItemp.Header = "Països";
            newExistMenuItem.Items.Add(newMenuItemp);

            ContextMenu menu = this.contextpaisos;
            MenuItem newMenuItempc = new MenuItem();
            newMenuItempc.Header = "Països";
            int a = menu.Items.Add(newMenuItempc);
        

            foreach (var item in Paises)
            {
                MenuItem newMenuItem2 = new MenuItem();

                //MenuItem newExistMenuItem2 = (MenuItem)this.MenuPaisos.Items[3];

                newMenuItem2.Header = item.nom;
                newMenuItemp.Items.Add(newMenuItem2);
                newMenuItem2.Click += selecciona_pais;

                MenuItem newMenuItem2c = new MenuItem();

                newMenuItem2c.Header = item.nom;
                newMenuItempc.Items.Add(newMenuItem2c);
                newMenuItem2c.Click += selecciona_pais;
            }
            

        }

        private void eliminar_menus_paisos(object sender, RoutedEventArgs e)
        {
            this.MenuPaisos.Items.Remove((MenuItem)this.MenuPaisos.Items[3]);
        }

        private void selecciona_pais(object sender, RoutedEventArgs e)
        {
            MenuItem aux = (MenuItem)sender;
            //MessageBox.Show((string)aux.Header);

            //ctHoraPais_txt.Text = "ALARMA PAÍS: " + (string)aux.Header;

            foreach (var item in Paises)
            {
                if (item.nom == (string)aux.Header)
                {
                    pais_seleccionat = item;
                    ctHoraPais_txt.Text = "HORA PAÍS: " + item.nom;
                }
            }
        }

        private void ReproduceAlarma(object sender, RoutedEventArgs e)
        {
            simpleSound.PlayLooping();
        }

        private void Surt(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ObrirBorrarPais(object sender, RoutedEventArgs e)
        { 
            BorrarPais borrarpais = new BorrarPais();
            borrarpais.Show();
        }

        public void BorrardePaisesList(String pais)
        {
            foreach (var item in paises)
            {
                if (item.nom == pais)
                {
                    paises.Remove(item);
                    eliminar_menus_paisos(null, null);
                    afegix_alMenu(null, null);
                    break;
                }
            }
        }   
    }

}
