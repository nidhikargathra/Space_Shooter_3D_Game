using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class HighScore
    {
        public string Name { get; private set; }
        public int Points { get; private set; }

        public HighScore(string name, int points)
        {
            Name = name;
            Points = points;
        }

    }
}