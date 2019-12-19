using System.IO;
using TensorFlow;

namespace CaptchaTest
{
    class ImageUtil
    {
        // Convert the image in filename to a Tensor suitable as input to the Inception model.
        public static TFTensor CreateTensorFromImageFile(string file, int channel)
        {
            var contents = File.ReadAllBytes(file);

            // DecodeJpeg uses a scalar String-valued tensor as input.
            var tensor = TFTensor.CreateString(contents);
            TFOutput input, output;

            // Construct a graph to normalize the image
            using (var graph = ConstructGraphToNormalizeImage(channel, out input, out output))
            {
                // Execute that graph to normalize this one image
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
            TFOutput src = graph.Cast(graph.DecodeBmp(contents: input, channels: channel), DstT: TFDataType.Float);
            //[H * W * RGB]
            var swapaxes = graph.Transpose(src, graph.Const(new int[] { 1, 0, 2 }));
            //[W * H * RGB]
            output = graph.Div(
                swapaxes,
                y: graph.Const((float)255)
                );
            //[W * H * RGB / 255]
            output = graph.ExpandDims(output, dim: graph.Const(0));
            //[1 * W * H * RGB / 255]
            return graph;
        }
    }
}
