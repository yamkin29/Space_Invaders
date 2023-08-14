using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Space_Invaders;

public class ShootingManager
{
    private readonly List<Bullet> _bullets = new();
    private readonly float _bulletSpeed;
    private readonly float _bulletRadius;
    private readonly float _shootingCooldown;
    private readonly Clock _clock = new();

    public ShootingManager(float shootingCooldown, float bulletSpeed, float bulletRadius)
    {
        _shootingCooldown = shootingCooldown;
        _bulletRadius = bulletRadius;
        _bulletSpeed = bulletSpeed;
    }

    public void TryShoot(Vector2f bulletSpawnPosition)
    {
        var lastShotTime = _clock.ElapsedTime.AsSeconds();

        if (lastShotTime >= _shootingCooldown)
        {
            var bullet = new Bullet(bulletSpawnPosition, _bulletSpeed, _bulletRadius);
            _bullets.Add(bullet);
            _clock.Restart();
        }
    }

    private bool IsBulletOutOfScreen(Bullet bullet)
    {
        return bullet.Position.Y < 0;
    }

    public void Update()
    {
        for (var i = 0; i < _bullets.Count; i++)
        {
            _bullets[i].Update();

            if (IsBulletOutOfScreen(_bullets[i]))
            {
                _bullets.RemoveAt(i);
                i--;
            }
        }
    }

    public void Draw(RenderWindow window)
    {
        for (var i = 0; i < _bullets.Count; i++)
        {
            _bullets[i].Draw(window);
        }
    }
}