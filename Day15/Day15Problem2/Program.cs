using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Day15Problem2
{
    public enum LensStepAction
    {
        Add,
        Remove
    }

    public class LensStep
    {
        private readonly string _inputString;

        public string LensLabel { get; init; }
        public int? LensFocalLength { get; init; }
        public int LensBox { get; init; }
        public LensStepAction Action { get; init; }

        public LensStep(string inputString)
        {
            _inputString = inputString;
            if (_inputString.Contains('-'))
            {
                Action = LensStepAction.Remove;
                string[] parts = _inputString.Split('-', StringSplitOptions.RemoveEmptyEntries);
                Debug.Assert(parts.Length == 1);
                LensLabel = parts[0];
                LensBox = GetStepBox(LensLabel);
            }
            else if (_inputString.Contains('='))
            {
                Action = LensStepAction.Add;
                string[] parts = _inputString.Split('=', StringSplitOptions.RemoveEmptyEntries);
                Debug.Assert(parts.Length == 2);
                LensLabel = parts[0];
                LensFocalLength = int.Parse(parts[1]);
                LensBox = GetStepBox(LensLabel);
            }
            else
            {
                throw new ArgumentException("Invalid lens step input string.", nameof(inputString));
            }
        }

        private static int GetStepBox(string label)
        {
            int stepValue = 0;
            foreach (int charAsciiValue in label)
            {
                stepValue += charAsciiValue;
                stepValue *= 17;
                stepValue %= 256;
            }

            return stepValue;
        }
    }

    internal class Program
    {
        private static List<LensStep>[] InitEmptyBoxes()
        {
            const int boxCount = 256;
            var result = new List<LensStep>[boxCount];
            for (int i = 0; i < boxCount; i++)
            {
                result[i] = new List<LensStep>();
            }

            return result;
        }

        private static void AddLensToBox(List<LensStep> box, LensStep newLens)
        {
            bool replacedExisting = false;
            for (int i = 0; i < box.Count; i++)
            {
                if (box[i].LensLabel == newLens.LensLabel)
                {
                    box[i] = newLens;
                    replacedExisting = true;
                }
            }

            if (!replacedExisting)
            {
                box.Add(newLens);
            }
        }

        private static void RemoveLensFromBox(List<LensStep> box, LensStep toRemove)
        {
            for (int i = 0; i < box.Count; i++)
            {
                if (box[i].LensLabel == toRemove.LensLabel)
                {
                    box.RemoveAt(i);
                    break;
                }
            }
        }

        private static long GetAllBoxesPower(List<LensStep>[] allBoxes)
        {
            long allLensPower = 0;
            for (int i = 0; i < allBoxes.Length; i++)
            {
                for (int j = 0; j < allBoxes[i].Count; j++)
                {
                    Debug.Assert(allBoxes[i][j].LensFocalLength.HasValue);
                    allLensPower += (i + 1) * (j + 1) * allBoxes[i][j].LensFocalLength.Value;
                }
            }

            return allLensPower;
        }

        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            Debug.Assert(inputLines.Length == 1);
            string[] splitInput = inputLines[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
            long sumOfSteps = 0;
            List<LensStep>[] allBoxes = InitEmptyBoxes();
            foreach (string inputPart in splitInput)
            {
                var lensStep = new LensStep(inputPart);
                List<LensStep> box = allBoxes[lensStep.LensBox];
                if (lensStep.Action == LensStepAction.Add)
                {
                    AddLensToBox(box, lensStep);
                }
                else if (lensStep.Action == LensStepAction.Remove)
                {
                    RemoveLensFromBox(box, lensStep);
                }
                else
                {
                    throw new Exception("Unrecognized input step action.");
                }
            }

            Console.Out.WriteLine(GetAllBoxesPower(allBoxes));
        }
    }
}
