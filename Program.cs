using System;
using System.IO;
using System.Text;
using TensorFlow;

namespace CaptchaTest
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var graph = new TFGraph())
            {
                graph.Import(File.ReadAllBytes("model.pb"));
                using (var session = new TFSession(graph))
                {
                    var charset = Constants.AlphabetLower;
                    var opInput = graph["input"][0];
                    var opDenseDecode = graph["dense_decoded"][0];
                    var imageTensor = ImageUtil.CreateTensorFromImageFile("demo.bmp", 3);
                    var output = session.Run(
                        inputs: new TFOutput[] { opInput },
                        inputValues: new TFTensor[] { imageTensor },
                        outputs: new TFOutput[] { opDenseDecode }
                    );
                    var result = output[0];
                    var stringBuffer = new StringBuilder();
                    foreach (int s in (long[,])result.GetValue())
                    {
                        if (s > charset.Count - 1)
                        {
                            Console.WriteLine("Current character set do not match the model.");
                            break;
                        }
                        stringBuffer.Append(charset[s]);
                    }
                    Console.WriteLine(stringBuffer.ToString());
                }
            }
            Console.ReadKey();
        }
    }
}
