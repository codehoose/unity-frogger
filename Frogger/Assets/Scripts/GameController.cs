using UnityEngine;

public class GameController : MonoBehaviour
{
    private int _score;
    private int _hiScore = 4630;
    private int _lives = 3;
    private bool _gameOver;

    public VehicleSpawnPoint[] points;

    public int Score { get { return _score; } }

    public int HiScore { get { return _hiScore; } }

    public int Lives { get { return _lives; } }
    
    public bool GameOver { get { return _gameOver; } }

    public void IncrementScore(int scoreValue)
    {
        _score += scoreValue;
        if (_score > _hiScore)
        {
            _hiScore = _score;
        }
    }

    public void DecrementLives()
    {
        _lives--;
        if (_lives <= 0)
        {
            if (!_gameOver)
            {
                foreach(var spawnPoint in points)
                {
                    spawnPoint.GameOver();
                }
            }
            _gameOver = true;
            _lives = 0;
        }
    }
}
