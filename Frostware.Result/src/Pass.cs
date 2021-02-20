
namespace Frostware.Result
{
    public class Pass : Result { }
    public class Pass<T> : Pass
    {
        public T Value { get; }
        public Pass(T value)
        {
            Value = value;
        }
        public static implicit operator T(Pass<T> x) => x.Value;
    }
}