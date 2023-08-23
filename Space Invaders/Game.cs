using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Space_Invaders;

public class Game
{
    // Создали длину и высоту окна игры, а так же его название
    private readonly RenderWindow _window;
    private readonly Sprite _background;
    private readonly Player _player;
    private readonly EnemyManager _enemyManager;
    private readonly CollisionHandler _collisionHandler;

    // Создаем котнструктор класса и переносим в него всю логику включения
    public Game(GameConfiguration gameConfiguration)
    {
        var mode = new VideoMode((uint)gameConfiguration.Width, (uint)gameConfiguration.Height);
        _window = new RenderWindow(mode, gameConfiguration.Title);

        _window.SetVerticalSyncEnabled(true);
        _window.Closed += (_, _) => _window.Close();

        _background = new Sprite(TextureManager.BackgroundTexture);

        _player = CreatePlayer(gameConfiguration);
        var screenSize = new Vector2f(gameConfiguration.Width, gameConfiguration.Height);
        _enemyManager =
            new EnemyManager(gameConfiguration.EnemySpawnCooldown, gameConfiguration.EnemySpeed, screenSize);
        _collisionHandler = new CollisionHandler(_player, _enemyManager);
    }


    private Player CreatePlayer(GameConfiguration gameConfiguration)
    {
        var shootingCooldown = gameConfiguration.PlayerSettings.ShootingCooldown;
        var shootingManager = new ShootingManager(shootingCooldown, gameConfiguration.BulletSpeed,
            gameConfiguration.BulletRadius);
        var playerSpawnPosition = GetPlayerSpawnPosition(gameConfiguration, TextureManager.PlayerTexture);
        var playerMovement = new PlayerMovement(gameConfiguration.PlayerSettings);
        var shootingButton = gameConfiguration.PlayerSettings.ShootingButton;
        return new Player(shootingManager, shootingButton, TextureManager.PlayerTexture, playerSpawnPosition,
            playerMovement);
    }


    private Vector2f GetPlayerSpawnPosition(GameConfiguration gameConfiguration, Texture texture)
    {
        var screenCenter = new Vector2f(gameConfiguration.Width / 2f, gameConfiguration.Height / 2f);
        var playerSpawnPosition = screenCenter - (Vector2f)texture.Size / 2f;
        return playerSpawnPosition;
    }


    public void Run()
    {
        // Пока окно открыто будем выполнять действие. 
        // Получаем события от пользователя
        // Так как все это в цикле, то мы постоянно перерисовываем окно в синий цвет
        // Ну и отображаем само окно.
        while (_window.IsOpen)
        {
            HandleEvents();
            Update();
            Draw();
        }
    }

    // Метод HandleEvents() будет отвечать за обработку событий в окне приложения,
    // таких как нажатия клавиш, перемещения мыши, клики и т.д.
    private void HandleEvents()
    {
        _window.DispatchEvents();
    }

    // Метод Update() будет обновлять игровую логику, которая может включать в себя изменение положения объектов на экране
    // и другие действия, не связанные с графикой.
    private void Update()
    {
        _player.Update();
        _enemyManager.Update();
        _collisionHandler.Update();
    }

    // Метод Draw() будет рисовать изображение на экране.
    private void Draw()
    {
        _window.Draw(_background);
        _player.Draw(_window);
        _enemyManager.Draw(_window);

        _window.Display();
    }
}