namespace Day15Problem2
{
    public class LensStep
    {
        private const string INPUT_EXCEPTION = "Invalid lens step input string.";
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
                if (parts.Length != 1)
                {
                    throw new ArgumentException(INPUT_EXCEPTION, nameof(inputString));
                }
                LensLabel = parts[0];
                LensBox = GetStepBox(LensLabel);
            }
            else if (_inputString.Contains('='))
            {
                Action = LensStepAction.Add;
                string[] parts = _inputString.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                {
                    throw new ArgumentException(INPUT_EXCEPTION, nameof(inputString));
                }
                LensLabel = parts[0];
                LensFocalLength = int.Parse(parts[1]);
                LensBox = GetStepBox(LensLabel);
            }
            else
            {
                throw new ArgumentException(INPUT_EXCEPTION, nameof(inputString));
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
}
