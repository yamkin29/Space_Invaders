using SFML.Graphics;
using SFML.Window;
using Space_Invaders;

class Program
{
    private const string GAME_CONFIGURATION_JSON_PATH = "GameConfiguration.json";
    
    static void Main(string[] args)
    {
        var gameConfiguration = new GameConfiguration(GAME_CONFIGURATION_JSON_PATH);
        Game game = new Game(gameConfiguration);
        game.Run();
    }
}