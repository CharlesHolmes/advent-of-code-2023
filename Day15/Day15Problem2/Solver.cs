namespace Day15Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            if (inputLines.Length != 1)
            {
                throw new ArgumentException("Input text must have exactly one line.", nameof(inputLines));
            }
            string[] splitInput = inputLines[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
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
                    throw new ArgumentException("Unrecognized input step action.");
                }
            }

            return GetAllBoxesPower(allBoxes);
        }

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
                    int? lensFocalLength = allBoxes[i][j].LensFocalLength;
                    if (lensFocalLength.HasValue)
                    {
                        allLensPower += (i + 1) * (j + 1) * lensFocalLength.Value;
                    }
                    else
                    {
                        throw new InvalidOperationException("Lens focal length must have a value.");
                    }
                }
            }

            return allLensPower;
        }
    }
}
