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
using Reservas_Final.Models;
using Reservas_Final.Models.Reservas;

namespace Reservas_Final.View.Reservas
{
    /// <summary>
    /// Interaction logic for InformacionReserva.xaml
    /// </summary>
    public partial class InformacionReserva : Window
    {
        private ReservaBase _reserva; // Almacena la reserva seleccionada

        public InformacionReserva(ReservaBase reserva)
        {
            InitializeComponent();
            _reserva = reserva; // Almacena la reserva pasada desde el MainWindow

            // Asigna los valores de la reserva a los controles de la interfaz
            txtNombre.Text = $"A nombre de: {_reserva.id_usu}";
            txtUsuario.Text = _reserva.id_usu;
            txtHabitacion.Text = _reserva.id_hab;
            txtFechaEntrada.Text = _reserva.fecha_check_in;
            txtFechaSalida.Text = _reserva.fecha_check_out;
            txtEstadoReserva.Text = _reserva.estado_reserva;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
