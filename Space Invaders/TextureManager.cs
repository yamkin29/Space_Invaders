using SFML.Graphics;

namespace Space_Invaders;

public static class TextureManager
{
    private const string ASSETS_PATH = "C:/Users/dartw/RiderProjects/Space Invaders/Space Invaders/Assets";
    private const string PLAYER_TEXTURE_PATH = "/Ships/playerShip2_red.png";
    private const string BACKGROUND_TEXTURE_PATH = "/Backgrounds/greenBG.png";
    private const string ENEMY_TEXTURE_PATH = "/Enemies/enemyBlue1.png";

    public static Texture BackgroundTexture;
    public static Texture PlayerTexture;
    public static readonly Texture EnemyTexture;

    static TextureManager()
    {
        BackgroundTexture = new Texture(ASSETS_PATH + BACKGROUND_TEXTURE_PATH);
        PlayerTexture = new Texture(ASSETS_PATH + PLAYER_TEXTURE_PATH);
        EnemyTexture = new Texture(ASSETS_PATH + ENEMY_TEXTURE_PATH);
    }
}