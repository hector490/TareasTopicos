using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows.Input;

namespace InventarioVideojuegos.ViewModels
{
    public class InventarioViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<VideojuegoModel> Lista { get; set; } = new();
        public VideojuegoModel VideoJuego { get; set; } = new();
        public string Vista { get; set; } = "Lista";
        public string Errores { get; set; } = "";
        public ICommand VerAgregarCommand { get; set; }
        public ICommand VerEditarCommand { get; set; }
        public ICommand VerEliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand AgregarCommmand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        private int index = 0;
        public InventarioViewModel()
        {
            VerAgregarCommand = new RelayCommand(VerAgregar);
            VerEditarCommand = new RelayCommand(VerEditar);
            VerEliminarCommand = new RelayCommand(VerEliminar);
            CancelarCommand = new RelayCommand(Cancelar);
            AgregarCommmand = new RelayCommand(Agregar);
            EditarCommand = new RelayCommand(Editar);
            EliminarCommand = new RelayCommand(Eliminar);

            Cargar();
        }

        private void Eliminar()
        {
            Lista.Remove(VideoJuego);
            Guardar();
            Cancelar();
        }

        private void Editar()
        {
            Errores = "";
            if(VideoJuego != null)
            {
                if (Verificar())
                {
                    Lista[index] = VideoJuego;
                    PropertyChanged?.Invoke(this, new(nameof(Vista)));
                    Guardar();
                    Cancelar();
                }
            }
        }

        private void Agregar()
        {
            Errores = "";
            if(VideoJuego != null)
            {
                if (Verificar())
                {
                    Lista.Add(VideoJuego);
                    Guardar();
                    Cancelar();
                }
            }
        }
        private bool Verificar()
        {
            if (string.IsNullOrWhiteSpace(VideoJuego.Nombre))
            {
                Errores += "Escribe el nombre del videojuego\n";
            }
            if (string.IsNullOrWhiteSpace(VideoJuego.Genero))
            {
                Errores += "Escribe un genero para el videojuego\n";
            }
            if (string.IsNullOrWhiteSpace(VideoJuego.Consola))
            {
                Errores += "Escribe la consola para jugar el videojuego\n";
            }
            if (VideoJuego.Año > DateTime.Now)
            {
                Errores += "La fecha no puede ser en el futuro\n";
            }

            if (string.IsNullOrWhiteSpace(Errores))
            {
                return true;
            }
            else
            {
                PropertyChanged?.Invoke(this, new(nameof(Errores)));
                return false;
            }
        }
        private void Cancelar()
        {
            Vista = "Lista";
            PropertyChanged?.Invoke(this, new(nameof(Vista)));
        }

        private void VerEliminar()
        {
            if (VideoJuego != null)
            {

                if (!string.IsNullOrWhiteSpace(VideoJuego.Nombre))
                {
                    Vista = "Eliminar";
                    PropertyChanged?.Invoke(this, new(nameof(Vista)));
                    PropertyChanged?.Invoke(this, new(nameof(VideoJuego)));
                }
            }
        }

        private void VerEditar()
        {
            if (VideoJuego != null)
            {
                if (!string.IsNullOrWhiteSpace(VideoJuego.Nombre))
                {
                    Vista = "Editar";
                    Errores = "";
                    index = Lista.IndexOf(VideoJuego);
                    VideojuegoModel v = new()
                    {
                        Nombre = VideoJuego.Nombre,
                        Genero = VideoJuego.Genero,
                        Año = VideoJuego.Año,
                        Consola = VideoJuego.Consola
                    };
                    VideoJuego = v;
                    PropertyChanged?.Invoke(this, new(nameof(Vista)));
                    PropertyChanged?.Invoke(this, new(nameof(Errores)));
                    PropertyChanged?.Invoke(this, new(nameof(VideoJuego)));
                }                
            }
        }

        private void VerAgregar()
        {
            Vista = "Agregar";
            Errores = "";
            VideoJuego = new();
            PropertyChanged?.Invoke(this, new(nameof(Vista)));
            PropertyChanged?.Invoke(this, new(nameof(Errores)));
            PropertyChanged?.Invoke(this, new(nameof(VideoJuego)));
        }
        private void Guardar()
        {
            var lst = JsonSerializer.Serialize(Lista);
            File.WriteAllText("ListaVideojuegos.json", lst);
        }
        private void Cargar()
        {
            if (File.Exists("ListaVideojuegos.json"))
            {
                var json = File.ReadAllText("ListaVideojuegos.json");
                var datos = JsonSerializer.Deserialize<ObservableCollection<VideojuegoModel>>(json);
                if (datos == null)
                {
                    Lista = new ObservableCollection<VideojuegoModel>();
                }
                else
                {
                    Lista = datos;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
