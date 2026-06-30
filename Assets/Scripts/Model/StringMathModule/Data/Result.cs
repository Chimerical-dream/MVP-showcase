namespace StringMath.Data
{
    [System.Serializable]
    public struct Result
    {
        public string Value;
        public ResultType Type;

        public Result(ResultType type)
        {
            Type = type;
            Value = null;
        }

        public Result(ResultType type, string value)
        {
            Type = type;
            Value = value;
        }

        public static Result Error => new Result(ResultType.Error);
    }
}
