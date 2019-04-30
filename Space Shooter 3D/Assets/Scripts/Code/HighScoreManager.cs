﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class HighScoreManager
    {
        private static HighScoreManager _instance;

        public static HighScoreManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HighScoreManager();

                return _instance;
            }
        }

        private const int MaxHighScores = 10;
        private readonly List<HighScore> _scores;
        public IEnumerable<HighScore> Scores { get { return _scores; } }

        private HighScoreManager()
        {
            _scores = new List<HighScore>();
            for(int i = 0; i < MaxHighScores; i++)
            {
                string name = PlayerPrefs.GetString(string.Format("Highscores_{0}_Name", i));
                if (string.IsNullOrEmpty(name))
                    break;
                var score = PlayerPrefs.GetInt(string.Format("Highscores_{0}_Score", i));
                _scores.Add(new HighScore(name, score));
            }
        }

        public bool CanAddHighScore(int points)
        {
            if (_scores.Count < MaxHighScores)
                return true;
            return _scores[MaxHighScores - 1].Points < points;
        }

        public void AddHighScore(string name, int points)
        {
            if (!CanAddHighScore(points))
                throw new System.InvalidOperationException();

            int insertIndex = _scores.FindIndex(score => score.Points < points);
            _scores.Insert(
                insertIndex == -1 ? _scores.Count : insertIndex,
                new HighScore(name, points));

            var toRemove = _scores.Count - MaxHighScores;
            if (toRemove > 0)
                _scores.RemoveRange(MaxHighScores, toRemove);

            SaveHighScores();
        }
        private void SaveHighScores() 
        {
            for (int i = 0; i < _scores.Count; i++)
            {
                var score = _scores[i];
                PlayerPrefs.SetString(string.Format("Highscores_{0}_Name", i), score.Name);
                PlayerPrefs.SetInt(string.Format("Highscores_{0}_Score", i), score.Points);
            }
        }
    }
}