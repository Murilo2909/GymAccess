using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using OpenCvSharp;

using System.Globalization;
namespace GymAccess.API.Services
{
    public class FacialRecognitionService
    {
        private readonly InferenceSession _session;

        public FacialRecognitionService(IConfiguration config)
        {
            var modelPath = Path.Combine(Directory.GetCurrentDirectory(), "Models", "arcface.onnx");
            _session = new InferenceSession(modelPath);
        }

        public float[] ExtractEmbedding(byte[] imageBytes)
        {
            // 1. Carrega imagem com o OpenCV
            Mat img = Cv2.ImDecode(imageBytes, ImreadModes.Color);
            if (img.Empty())
            {
                throw new Exception("Não foi possível carregar a imagem.");
            }

            // 2. Redimensiona para 112x112
            Mat resized = new Mat();
            Cv2.Resize(img, resized, new Size(112, 112));

            // 3. Converte BGR → RGB
            Cv2.CvtColor(resized, resized, ColorConversionCodes.BGR2RGB);

            // 4. Cria tensor NCHW 1x3x112x112
            var inputTensor = new DenseTensor<float>(new[] { 1, 112, 112, 3 });

            for (int y = 0; y < 112; y++)
            {
                for (int x = 0; x < 112; x++)
                {
                    Vec3b pixel = resized.At<Vec3b>(y, x); // RGB
                    inputTensor[0, y, x, 0] = pixel.Item0 / 255f; // R
                    inputTensor[0, y, x, 1] = pixel.Item1 / 255f; // G
                    inputTensor[0, y, x, 2] = pixel.Item2 / 255f; // B
                }
            }

            // 5. Cria NamedOnnxValue usando o nome correto do input do modelo
            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input_1", inputTensor) // Ajuste "input.1" conforme _session.InputMetadata
            };

            // 6. Executa o modelo
            using var outputs = _session.Run(inputs);
            var result = outputs.First().AsEnumerable<float>().ToArray();
            return result;
        }

        private DenseTensor<float> ToTensor(Mat img)
        {
            var tensor = new DenseTensor<float>(new[] { 1, 3, 112, 112 });

            for (int y = 0; y < 112; y++)
            {
                for (int x = 0; x < 112; x++)
                {
                    Vec3b pixel = img.Get<Vec3b>(y, x);

                    tensor[0, 0, y, x] = (pixel.Item0 - 127.5f) / 128f; // R
                    tensor[0, 1, y, x] = (pixel.Item1 - 127.5f) / 128f; // G
                    tensor[0, 2, y, x] = (pixel.Item2 - 127.5f) / 128f; // B
                }
            }

            return tensor;
        }

        public double Compare(float[] emb1, float[] emb2)
        {
            double normA = Math.Sqrt(emb1.Sum(x => x * x));
            double normB = Math.Sqrt(emb2.Sum(x => x * x));

            double dot = 0;
            for (int i = 0; i < emb1.Length; i++)
                dot += emb1[i] / normA * emb2[i] / normB;

            return dot;
        }
    }
}
