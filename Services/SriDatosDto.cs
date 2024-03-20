using System.ComponentModel.DataAnnotations;

namespace ApiAntDatosvehiculo.Services
{

    public class PlacaDTO
    {

        [Required(ErrorMessage = "Campo Requerido")]
        public string Placa { get; set; }

    }

    public class AntValoresPLacaDTO
    {
        public string Mensaje { get; set; }

        public string Marca { get; set; }
        public string Color { get; set; }
        public string AnioMatrícula { get; set; }
        public string Modelo { get; set; }
        public string Clase { get; set; }
        public string FechaMatricula { get; set; }
        public string Ani { get; set; }
        public string Servicio { get; set; }
        public string FechaCaducidad { get; set; }
        public string Polarizado { get; set; }

    }
}