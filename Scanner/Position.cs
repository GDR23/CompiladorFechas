﻿using System;
namespace Scanner
{
    public readonly struct Position
    {
        public int Absolute { get; }

        public int Line { get; }

        public int Column { get; }

        public static Position Start => new Position(0, 0, 0);

        public Position(int absolute, int line, int column)
        {
            Absolute = absolute;
            Line = line;
            Column = column;
        }

        public Position MovePointer(char @char)
        {
            if (@char == '\n')
            {
                return new Position(Absolute + 1, Line + 1, 1);

            }

            return new Position(Absolute + 1, Line, Column + 1);
        }
    }
}
