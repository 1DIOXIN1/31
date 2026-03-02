using System;

public class PlayerProvider
{
    private Player _player;

    public PlayerProvider(CharactersFactory charactersFactory)
    {
        charactersFactory.PlayerCreated += OnPlayerCreated;
    }

    public Player Player 
    {
        get
        {
            if(_player == null)
                throw new InvalidOperationException("Игрок ещё не создан");

            return _player;
        }
    }

    private void OnPlayerCreated(Player player) => _player = player;
}
