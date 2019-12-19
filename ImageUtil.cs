using System.IO;
using TensorFlow;

namespace CaptchaTest
{
    class ImageUtil
    {
        public static TFTensor CreateTensorFromImageFile(string file, int channel)
        {
            var contents = File.ReadAllBytes(file);

            var tensor = TFTensor.CreateString(contents);
            TFOutput input, output;
            using (var graph = ConstructGraphToNormalizeImage(channel, out input, out output))
            {
                using (var session = new TFSession(graph))
                {
                    var normalized = session.Run(
                        inputs: new[] { input },
                        inputValues: new[] { tensor },
                        outputs: new[] { output });

                    return normalized[0];
                }
            }
        }

        private static TFGraph ConstructGraphToNormalizeImage(int channel, out TFOutput input, out TFOutput output)
        {
            var graph = new TFGraph();
            input = graph.Placeholder(TFDataType.String);
            output =
                //[1 * W * H * RGB / 255]
                graph.ExpandDims(
                     //[W * H * RGB / 255]
                     graph.Div(
                        //[W * H * RGB]
                        graph.Transpose(
                            //[H * W * RGB]
                            graph.Cast(graph.DecodeBmp(contents: input, channels: channel), DstT: TFDataType.Float)
                        , graph.Const(new int[] { 1, 0, 2 }))
                    ,y: graph.Const(255f))
                , dim: graph.Const(0));
            return graph;
        }
    }
}
