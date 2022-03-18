using System;
namespace Scanner
{
    public readonly struct Input
    {
        public string Source { get; }

        public int Length { get; }

        public Position Position { get; }

        public Input(string source)
            : this(source, Position.Start, source.Length)
        {
        }

        public Input(string source, Position position, int length)
        {
            Source = source;
            Length = length;
            Position = position;
        }

        public Result<char> NextChar()
        {
            if (Length == 0)
            {
                return Result<char>.Empty(this);
            }

            var @char = Source[Position.Absolute];
            return Result<char>.Valued(@char, new Input(Source, Position.MovePointer(@char), Length - 1));
        }

        public Result<char> BackChar(int x)
        {
            var @char = Source[Position.Absolute];
            return Result<char>.Valued(@char, new Input(Source, new Position(Position.Absolute - x, Position.Line, Position.Column - x), Length + x));
        }


    }
}