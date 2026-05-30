using ApiInteligenteTareas.DTOs;
using ApiInteligenteTareas.ML;
using Microsoft.AspNetCore.Mvc;

namespace ApiInteligenteTareas.Controllers
{
    [ApiController]
    [Route("api/ml")]
    public class MlController : ControllerBase
    {
        private readonly SentimentModel _model;

        public MlController(SentimentModel model)
        {
            _model = model;
        }

        [HttpPost("sentimiento")]
        public IActionResult AnalizarSentimiento(
            SentimentRequestDto request)
        {
            bool resultado =
                _model.Predict(request.Comentario);

            return Ok(new SentimentResponseDto
            {
                Comentario = request.Comentario,
                Sentimiento = resultado
                    ? "Positivo"
                    : "Negativo"
            });
        }
    }
}