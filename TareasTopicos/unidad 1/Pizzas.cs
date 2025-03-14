using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TareasTopicos.unidad_1
{
    public class Pizzas : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;



        public string Tamaño
        {
            get { return tamaño; }
            set
            {
                tamaño = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }
        private string tamaño = "Individual";
        public byte IngredientesExtra { get; set; }
        public bool QuesoExtra { get; set; }
        public bool Masa { get; set; }
        public decimal Precio { get; set; }
        public ICommand CalcularTotalCommand { get; set; }

        public void CalculrTotal()
        {
            if (Tamaño == "Individual")
            {
                Precio = 49;
            }
            else if (Tamaño == "Mediana")
            {
                Precio = 79 + IngredientesExtra * 20;
                if (QuesoExtra)
                {
                    Precio += 15;
                }

            }
            else
            {
                Precio = 149 + (IngredientesExtra * 20);
                if (Masa)
                {
                    Precio += +40;
                }
                if (QuesoExtra)
                {
                    Precio += 15;
                }

            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public Pizzas()
        {
            CalcularTotalCommand = new RelayCommand(CalculrTotal);
        }

    }
}
