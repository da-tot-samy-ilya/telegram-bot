using telegram_bot.enums;
namespace telegram_bot;

public class BotUser
{
    public GameStatus gameStatus = GameStatus.NotPlaying;
    public long id = 0;
    public string name = null;
    public Game gameProps = null;

    public BotUser(GameStatus Status, long Id, string Name, Game GameProps)
    {
        var props = new Game(0, 0, 0);
        gameStatus = Status;
        id = Id;
        name = Name;
        gameProps = props;

    }
}