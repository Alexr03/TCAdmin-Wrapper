namespace TCAdminWrapper.Extensions
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using TCAdmin.GameHosting.SDK.Objects;
    using TCAdmin.SDK.Database;

    /// <summary>
    /// Extensions for the Game Class.
    /// </summary>
    class GameExtensions : Game
    {
        public static List<Game> AllGames => GetGames();

        /// <summary>
        /// Preset for importing all features (scripts, variables, file manager) when importing a Game Config.
        /// </summary>
        public static GameImportFeatures AllFeatures = GameImportFeatures.FileManager | GameImportFeatures.FileSystem | GameImportFeatures.IpAndPorts | GameImportFeatures.FilesAndDirectories | GameImportFeatures.GeneralSettings | GameImportFeatures.CommandLines | GameImportFeatures.SteamSettings | GameImportFeatures.PunkbusterSettings | GameImportFeatures.FeaturePermissions | GameImportFeatures.RunAs | GameImportFeatures.QueryMonitoring | GameImportFeatures.Variables | GameImportFeatures.ConfigurationFiles | GameImportFeatures.CustomScripts | GameImportFeatures.MailTemplates | GameImportFeatures.Mods | GameImportFeatures.TextConsole | GameImportFeatures.WebConsole | GameImportFeatures.FastDownload | GameImportFeatures.BukGetSettings | GameImportFeatures.CustomLinks | GameImportFeatures.MapPacks | GameImportFeatures.Updates | GameImportFeatures.GameTracker | GameImportFeatures.Keys;

        /// <summary>
        /// Import the Game XML into TCAdmin. Will automatically import all the features.
        /// </summary>
        /// <param name="exportedGameXml">The XML document string that TCAdmin generated when Exporting the Game Config.</param>
        /// <param name="originalGameId">Specify if you want to overwrite an existing Game Configuration.</param>
        /// <returns></returns>
        public static int Import(string exportedGameXml, int originalGameId = 0)
        {
            return Import(exportedGameXml, AllFeatures, originalGameId);
        }

        /// <summary>
        /// Import the Game XML into TCAdmin along with the specification of the feature flags.
        /// </summary>
        /// <param name="exportedGameXml">The XML document string that TCAdmin generated when Exporting the Game Config.</param>
        /// <param name="features"><see cref="GameImportFeatures"/>The flags for the GameImportFeatures, specify exactly what you want to import.</param>
        /// <param name="originalGameId"></param>
        /// <returns></returns>
        public static int Import(string exportedGameXml, GameImportFeatures features, int originalGameId = 0)
        {
            if (string.IsNullOrEmpty(exportedGameXml))
                throw new ArgumentNullException("exportedGameXml", "The XML cannot be null or empty.");

            if (originalGameId > 0)
                return Game.Import(DatabaseManager.CreateDatabaseManager(true), exportedGameXml, new GameImportOptions { UpdateGameId = originalGameId, ImportFeatures = features });

            return Game.Import(DatabaseManager.CreateDatabaseManager(true), exportedGameXml, new GameImportOptions { ImportFeatures = features });
        }

        /// <summary>
        /// Create a Game Config on TCAdmin here. Do not worry about setting the GameId, this is assigned automatically.
        /// </summary>
        /// <param name="gameInfo"><see cref="Game"/>The Game Configuration</param>
        /// <returns></returns>
        public static Game CreateGame(Game gameInfo)
        {
            var games = Game.GetGames();
            gameInfo.GameId = games.Count + 1;
            gameInfo.Save();
            return new Game(gameInfo.GameId);
        }

        /// <summary>
        /// Export a Game to it's XML string by it's Game ID.
        /// </summary>
        /// <param name="gameId"></param>
        /// <exception cref="GameNotFoundException"></exception>
        /// <returns></returns>
        public static string Export(int gameId)
        {
            var game = GetGame(gameId);

            return Export(game);
        }

        /// <summary>
        /// Export a Game to it's XML string by it's Game Name.
        /// </summary>
        /// <param name="gameName">The Game Name</param>
        /// <exception cref="GameNotFoundException"></exception>
        /// <returns></returns>
        public static string Export(string gameName)
        {
            var game = GetGame(gameName);

            return Export(game);
        }

        /// <summary>
        /// Export a Game to it's XML string by the Game Class.
        /// </summary>
        /// <param name="game">The Game Class</param>
        /// <exception cref="GameNotFoundException"></exception>
        /// <returns></returns>
        public static string Export(Game game)
        {
            return game.Export();
        }

        /// <summary>
        /// Get all the Games currently on TCAdmin.
        /// </summary>
        /// <returns></returns>
        public new static List<Game> GetGames()
        {
            List<Game> games = new List<Game>();
            foreach (Game game in Game.GetGames())
            {
                games.Add(game);
            }

            return games;
        }

        /// <summary>
        /// Get a specific game by it's name.
        /// </summary>
        /// <param name="gameName">The name of the game.</param>
        /// <returns></returns>
        public static Game GetGame(string gameName)
        {
            if (!(Game.GetGames(gameName)[0] is Game game))
            {
                throw new GameNotFoundException(gameName);
            }
            if (!game.Find())
            {
                throw new GameNotFoundException(gameName);
            }

            return game;
        }

        /// <summary>
        /// Get a specific game by it's ID.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns></returns>
        public static Game GetGame(int gameId)
        {
            var game = new Game(gameId);
            if (!game.Find())
            {
                throw new GameNotFoundException(gameId);
            }

            return game;
        }
    }
}
