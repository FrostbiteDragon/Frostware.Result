
namespace Frostware.Result
{
    public class Result
    {
        public static Result Pass() => new Pass();
        public static Result Pass<T>(T value) => new Pass<T>(value);

        public static Result Fail(string errorMessage = "") => new Fail(errorMessage);
        public static Result Fail<T>(T value, string errorMessage = "") => new Fail<T>(value, errorMessage);
    }
}
