using ApiInteligenteTareas.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiInteligenteTareas.Controllers
{
    [ApiController]
    [Route("api/tareas-externas")]
    public class TareasExternasController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public TareasExternasController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetTodas()
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    "https://jsonplaceholder.typicode.com/todos"
                );

                if (!response.IsSuccessStatusCode)
                    return StatusCode(500,
                        "Error al consumir API externa");

                var json = await response.Content.ReadAsStringAsync();

                var datos =
                    JsonSerializer.Deserialize<List<JsonPlaceholderTodo>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                var resultado = datos!.Select(t => new TareaExternaDto
                {
                    ExternalId = t.Id,
                    Titulo = t.Title,
                    Completado = t.Completed
                });

                return Ok(resultado);
            }
            catch
            {
                return StatusCode(
                    500,
                    "La API externa no está disponible"
                );
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"https://jsonplaceholder.typicode.com/todos/{id}"
                );

                if (!response.IsSuccessStatusCode)
                    return NotFound();

                var json = await response.Content.ReadAsStringAsync();

                var tarea =
                    JsonSerializer.Deserialize<JsonPlaceholderTodo>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                var resultado = new TareaExternaDto
                {
                    ExternalId = tarea!.Id,
                    Titulo = tarea.Title,
                    Completado = tarea.Completed
                };

                return Ok(resultado);
            }
            catch
            {
                return StatusCode(
                    500,
                    "La API externa no está disponible"
                );
            }
        }

        private class JsonPlaceholderTodo
        {
            public int Id { get; set; }

            public string Title { get; set; } = string.Empty;

            public bool Completed { get; set; }
        }
    }
}