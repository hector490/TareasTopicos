using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareasTopicos.unidad_1
{
    public  class Calendario: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set
            {
                if (fecha != value)
                {
                    fecha = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                }
            }
        }

        public Calendario()
        {
            Fecha = DateTime.Now;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
