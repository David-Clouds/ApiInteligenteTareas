# API Inteligente de Tareas y Análisis

Proyecto desarrollado con ASP.NET Core Web API, SQLite, Entity Framework Core y ML.NET.

## Tecnologías

- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- ML.NET
- Swagger
- GitHub

---

## Ejecución del proyecto

Restaurar paquetes:

```bash
dotnet restore
```

Compilar:

```bash
dotnet build
```

Ejecutar:

```bash
dotnet run
```

Abrir Swagger:

```text
http://localhost:5097/swagger
```

---

## Migraciones

Crear migración:

```bash
dotnet ef migrations add InitialCreate
```

Actualizar base de datos:

```bash
dotnet ef database update
```

---

# Pregunta 1 - API RESTful

Modelo Tarea:

- Id
- Titulo
- Descripcion
- Estado
- Prioridad
- FechaCreacion
- FechaVencimiento

Endpoints:

```http
GET /api/Tareas
GET /api/Tareas/{id}
POST /api/Tareas
PUT /api/Tareas/{id}
DELETE /api/Tareas/{id}
```

Validaciones:

- Título obligatorio
- Estado obligatorio
- Prioridad obligatoria
- Fecha de vencimiento válida

---

# Pregunta 2 - Filtros

Se implementaron filtros por:

Estado:

```http
GET /api/Tareas?estado=Pendiente
```

Prioridad:

```http
GET /api/Tareas?prioridad=Alta
```

Rango de fechas:

```http
GET /api/Tareas?fechaInicio=2026-05-01&fechaFin=2026-06-30
```

---

# Pregunta 3 - API Externa

API utilizada:

https://jsonplaceholder.typicode.com/todos

Endpoints:

```http
GET /api/tareas-externas
GET /api/tareas-externas/{id}
```

DTO utilizado:

```json
{
  "externalId": 1,
  "titulo": "delectus aut autem",
  "completado": false
}
```

---

# Pregunta 4 - ML.NET

Endpoint:

```http
POST /api/ml/sentimiento
```

Ejemplo Request:

```json
{
  "comentario": "La tarea fue completada correctamente y el sistema funciona bien"
}
```

Ejemplo Response:

```json
{
  "comentario": "La tarea fue completada correctamente y el sistema funciona bien",
  "sentimiento": "Positivo"
}
```

Dataset utilizado:

- Frases positivas
- Frases negativas

Modelo entrenado con ML.NET.

---

# Evidencias

Las capturas se encuentran en:

```text
/evidencias
```

- pregunta1.png
- pregunta2.png
- pregunta3.png
- pregunta4.png

---

# Ramas utilizadas

```text
main
feature/api-tareas
feature/filtros-tareas
feature/api-externa-todos
feature/mlnet-basico
```

Cada funcionalidad fue desarrollada en una rama independiente y posteriormente integrada mediante Pull Request.