using SFML.Graphics;
using SFML.System;

namespace Space_Invaders;

public class EnemyManager
{
    private readonly List<Enemy> _enemies = new();
    private readonly float _spawnCooldown;
    private readonly Clock _clock = new();
    private readonly float _enemySpeed;
    private readonly Vector2f _screenSize;
    private readonly Random _random = new();

    public EnemyManager(float spawnCooldown, float enemySpeed, Vector2f screenSize)
    {
        _spawnCooldown = spawnCooldown;
        _enemySpeed = enemySpeed;
        _screenSize = screenSize;
    }

    private void SpawnEnemy()
    {
        var lastEnemySpawn = _clock.ElapsedTime.AsSeconds();

        if (lastEnemySpawn < _spawnCooldown)
        {
            return;
        }

        var randomPositionX = _random.Next(0, (int)_screenSize.X);
        var enemyTexture = TextureManager.EnemyTexture;
        var spawnPosition = new Vector2f(randomPositionX, -enemyTexture.Size.Y);
        var enemy = new Enemy(_enemySpeed, enemyTexture, spawnPosition);
        _enemies.Add(enemy);
        _clock.Restart();
    }

    public void Update()
    {
        SpawnEnemy();
        UpdateEnemies();
    }

    private void UpdateEnemies()
    {
        for (var i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].Update();
            if (IsEnemyOutOfScreen(_enemies[i]))
            {
                _enemies.RemoveAt(i);
                i--;
            }
        }
    }


    public void Draw(RenderWindow window)
    {
        foreach (var enemy in _enemies)
        {
            enemy.Draw(window);
        }
    }

    private bool IsEnemyOutOfScreen(Enemy enemy)
    {
        return _screenSize.Y < enemy.Position.Y;
    }
}