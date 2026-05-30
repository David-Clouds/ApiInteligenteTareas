using Microsoft.ML;

namespace ApiInteligenteTareas.ML
{
    public class SentimentModel
    {
        private readonly PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;

        public SentimentModel()
        {
            var mlContext = new MLContext();

            var trainingData = new List<SentimentData>
            {
                new() { Comentario = "Excelente trabajo", Label = true },
                new() { Comentario = "Muy buen sistema", Label = true },
                new() { Comentario = "Funciona correctamente", Label = true },
                new() { Comentario = "Me gusta mucho", Label = true },
                new() { Comentario = "Todo salió perfecto", Label = true },

                new() { Comentario = "Muy mal servicio", Label = false },
                new() { Comentario = "No funciona", Label = false },
                new() { Comentario = "Hay demasiados errores", Label = false },
                new() { Comentario = "Sistema terrible", Label = false },
                new() { Comentario = "Experiencia negativa", Label = false }
            };

            var dataView = mlContext.Data.LoadFromEnumerable(trainingData);

            var pipeline =
                mlContext.Transforms.Text.FeaturizeText(
                    outputColumnName: "Features",
                    inputColumnName: nameof(SentimentData.Comentario))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

            var model = pipeline.Fit(dataView);

            _predictionEngine =
                mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
        }

        public bool Predict(string comentario)
        {
            var prediction = _predictionEngine.Predict(
                new SentimentData
                {
                    Comentario = comentario
                });

            return prediction.Prediction;
        }
    }
}