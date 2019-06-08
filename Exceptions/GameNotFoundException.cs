namespace TCAdminWrapper.Exceptions
{
    using System;

    class GameNotFoundException : Exception
    {
        public string GameName { get; }

        public int GameId { get; }

        public GameNotFoundException(string gameName)
        {
            GameName = gameName;
        }

        public GameNotFoundException(int gameId)
        {
            GameId = gameId;
        }
    }
}
