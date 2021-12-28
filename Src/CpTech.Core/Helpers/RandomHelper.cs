﻿using System;
using System.Linq;

namespace CpTech.Core.Helpers
{
    public static class RandomHelper
    {
        private static readonly Random Random = new Random();

        public static string RandomString(int length, string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)])
                .ToArray());
        }
    }
}