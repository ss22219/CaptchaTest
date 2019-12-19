using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace CaptchaTest
{
    public class Config
    {
        [YamlMember(Alias = "System")]
        public SystemConfig System { get; set; }
        [YamlMember(Alias = "Model")]
        public Model Model { get; set; }
        [YamlMember(Alias = "FieldParam")]
        public FieldParam FieldParam { get; set; }
        public NeuralNet NeuralNet { get; set; }
        public Label Label { get; set; }
        public DataAugmentation DataAugmentation { get; set; }
        public Trains Trains { get; set; }
    }
    public class SystemConfig
    {
        public string MemoryUsage { get; set; }
        public string Version { get; set; }
    }
    public class NeuralNet
    {
        public string CNNNetwork { get; set; }
        public string RecurrentNetwork { get; set; }
        public int UnitsNum { get; set; }
        public string Optimizer { get; set; }
        public OutputLayer OutputLayer { get; set; }
    }
    public class OutputLayer
    {
        public string LossFunction { get; set; }
        public string Decoder { get; set; }
    }
    public class Model
    {
        public string ModelName { get; set; }
        public string ModelField { get; set; }
        public string ModelScene { get; set; }
    }

    public class FieldParam
    {
        public string Category { get; set; }
        public List<int> Resize { get; set; }
        public int Binaryzation { get; set; }
        public int Smoothing { get; set; }
        public int Blur { get; set; }
        public int ImageChannel { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public int MaxLabelNum { get; set; }
        public bool ReplaceTransparent { get; set; }
        public bool HorizontalStitching { get; set; }
        public object OutputSplit { get; set; }
    }

    public class Label
    {
        public string LabelFrom { get; set; }
        public string ExtractRegex { get; set; }
        public object LabelSplit { get; set; }
    }
    public class DatasetPath
    {
        public List<string> Validation { get; set; }
        public List<string> Training { get; set; }
    }

    public class Trains
    {
        public DatasetPath DatasetPath { get; set; }
        public DatasetPath SourcePath { get; set; }

        public int ValidationSetNum { get; set; }
        public int SavedSteps { get; set; }
        public int ValidationSteps { get; set; }
        public double EndAcc { get; set; }
        public double EndCost { get; set; }
        public int EndEpochs { get; set; }
        public int BatchSize { get; set; }
        public int ValidationBatchSize { get; set; }
        public double LearningRate { get; set; }

    }

    public class DataAugmentation
    {
        public int Binaryzation { get; set; }
        public int MedianBlur { get; set; }
        public int GaussianBlur { get; set; }
        public int EqualizeHist { get; set; }
        public int Laplace { get; set; }
        public int WarpPerspective { get; set; }
        public int Rotate { get; set; }
        public double PepperNoise { get; set; }
    }
}
