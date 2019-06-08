namespace TCAdminWrapper.Extensions
{
    using System;
    using Exceptions;
    using TCAdmin.GameHosting.SDK.Objects;
    using TCAdmin.SDK.Database;

    class GameExtensions : Game
    {
        public static GameImportFeatures AllFeatures = GameImportFeatures.FileManager | GameImportFeatures.FileSystem | GameImportFeatures.IpAndPorts | GameImportFeatures.FilesAndDirectories | GameImportFeatures.GeneralSettings | GameImportFeatures.CommandLines | GameImportFeatures.SteamSettings | GameImportFeatures.PunkbusterSettings | GameImportFeatures.FeaturePermissions | GameImportFeatures.RunAs | GameImportFeatures.QueryMonitoring | GameImportFeatures.Variables | GameImportFeatures.ConfigurationFiles | GameImportFeatures.CustomScripts | GameImportFeatures.MailTemplates | GameImportFeatures.Mods | GameImportFeatures.TextConsole | GameImportFeatures.WebConsole | GameImportFeatures.FastDownload | GameImportFeatures.BukGetSettings | GameImportFeatures.CustomLinks | GameImportFeatures.MapPacks | GameImportFeatures.Updates | GameImportFeatures.GameTracker | GameImportFeatures.Keys;

        public static int Import(string exportedGameXml, int originalGameId)
        {
            return Import(exportedGameXml, AllFeatures, originalGameId);
        }

        public static int Import(string exportedGameXml, GameImportFeatures features, int originalGameId = 0)
        {
            if (string.IsNullOrEmpty(exportedGameXml))
                throw new ArgumentNullException("exportedGameXml", "The XML cannot be null or empty.");

            if (originalGameId > 0)
                return Game.Import(DatabaseManager.CreateDatabaseManager(true), exportedGameXml, new GameImportOptions{ UpdateGameId = originalGameId, ImportFeatures = features });

            return Game.Import(DatabaseManager.CreateDatabaseManager(true), exportedGameXml, new GameImportOptions { ImportFeatures = features} );
        }

        public static Game CreateGame(Game gameInfo)
        {
            var games = Game.GetGames();
            gameInfo.GameId = games.Count + 1;
            gameInfo.Save();
            return new Game(gameInfo.GameId);
        }

        public static string Export(int gameId)
        {
            var game = new Game(gameId);
            if (!game.Find())
            {
                throw new GameNotFoundException(gameId);
            }

            return Export(game);
        }

        public static string Export(string gameName)
        {
            if (!(GetGames(gameName)[0] is Game game))
                throw new GameNotFoundException(gameName);

            return Export(game);
        }

        public static string Export(Game game)
        {
            return game.Export();
        }
    }
}
