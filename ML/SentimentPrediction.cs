using Microsoft.ML.Data;

namespace ApiInteligenteTareas.ML
{
    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }
    }
}