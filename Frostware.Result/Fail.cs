
namespace Frostware.Result
{
    public class Fail : Result
    {
        public string ErrorMessage { get; }

        public Fail(string errorMessage = default)
        {
            ErrorMessage = errorMessage;
        }
    }

    public class Fail<T> : Fail
    {
        public T Value { get; }

        public Fail(T value, string errorMessage = default) : base(errorMessage)
        {
            Value = value;
        }

        public void Deconstruct(out T value, out string errorMessage)
        {
            value = Value;
            errorMessage = ErrorMessage;
        }

        public static implicit operator T(Fail<T> x) => x.Value;
    }
}
