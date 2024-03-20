using ApiAntDatosvehiculo.Services;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ApiServiciosFrancisco.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly IWebHostEnvironment webHostEnvironment;

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly CDescargaAnt cDescarga;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,  IWebHostEnvironment webHostEnvironment, CDescargaAnt cDescarga)
        {
            _logger = logger;
            this.webHostEnvironment = webHostEnvironment;
            this.cDescarga = cDescarga;
        }



        [HttpGet("CAntConsultasDsotosJSON")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AntValoresPLacaDTO>> CAntConsultasDsotosJSON([FromQuery] PlacaDTO cedulaPlacaDTO)
        {
            var tipo = $"{Guid.NewGuid()}";


            if (!string.IsNullOrEmpty(cedulaPlacaDTO.Placa))
            {



                //cedulaPlacaInito.iNI = "I##@@RUC/Cédula/Pasaporte";


                var rutaBoleta = cDescarga.CConsultaDatosAutoAnt(cedulaPlacaDTO);

                if (rutaBoleta != null)
                {

                    return rutaBoleta;



                }




            }
      


            
            else
            {
                return BadRequest("NO existen datos a Consultar");

            }

            return BadRequest("NO se genero Vuelva a Intentar");


        }

    }
}