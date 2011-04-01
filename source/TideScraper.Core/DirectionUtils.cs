using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TideScraper.Core
{
    public static class DirectionUtils
    {
        public static Direction GetDirection(string direction)
        {
            switch (direction.ToLower())
            {
                case "n": return Direction.North;
                case "s": return Direction.South;
                case "e": return Direction.East;
                case "w": return Direction.West;
                default: return Direction.North;
            }
        }

        public static double Parse(string direction)
        {
            if (string.IsNullOrWhiteSpace(direction)) return 0;

            var match = DirectionRegex.Match(direction);
            if (!match.Success) return 0;

            var degrees = double.Parse(match.Groups["degrees"].Value);
            var minutes = double.Parse(match.Groups["minutes"].Value);
            var compassDirection = GetDirection(match.Groups["direction"].Value);
            return ConvertDegree(degrees, minutes, 0, compassDirection);
        }
        private static Regex DirectionRegex = new Regex(@"(?<degrees>\d+)\s+(?<minutes>\d+(?:\.\d*)?)\s+(?<direction>(n|N|s|S|e|E|w|W))");

        public static double ConvertDegree(double degree, double minute, double second, Direction direction)
        {
            double result;

            minute = minute / 60;
            second = second / 3600;
            result = degree + minute + second;

            if (direction == Direction.South || direction == Direction.West)
                result *= -1;

            return result;
        }

        public static double ConvertDistance(double distance, string type)
        {
            switch (type)
            {
                case Measurement.Miles:
                    return distance * 3959;
                case Measurement.Kilometers:
                    return distance * 6371;
                default:
                    return distance;
            }
        }
    }

    public enum Direction
    {
        North = 0,
        South = 1,
        East = 2,
        West = 3
    }

    public class Measurement
    {
        public const string Miles = "mi";
        public const string Kilometers = "km";
    }

    public static class EarthRadians
    {
        public const long Kilometers = 6378;
        public const long Miles = 3959;
    }
}
