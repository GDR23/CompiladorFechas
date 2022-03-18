using System;
namespace Scanner
{
    public class Result<T>
    {
        public T Value { get; set; }

        public Input Reminder { get; set; }


        internal Result(T value, Input reminder)
        {
            Value = value;
            Reminder = reminder;
        }

        internal Result(Input reminder)
        {
            Reminder = reminder;
        }

        public static Result<T> Empty(Input reminder)
        {

            return new Result<T>(reminder);
        }
        public static Result<T> Valued(T value, Input reminder)
        {
            return new Result<T>(value, reminder);
        }
    }
}
