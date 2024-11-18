namespace CardsAgainstHumanity
{
    internal class Manager
    {
        private IUI _ui;
        private List<string> _mainMenuList;
        private List<string> _yesNoMenuList;

        public IUI UI => _ui;

        public Manager(IUI ui) {
            _ui = ui;
            _mainMenuList = new List<string>() { "    Single player    ",
                                                 "  Local Multiplayer  ",
                                                 "   LAN Multiplayer   ",
                                                 "         Quit        " };
            _yesNoMenuList = new List<string>() { "    Yes    ",
                                                  "     No    " };
        }

        public void Run()
        {
            Initialize();
            Initialize();
            try
            {
                MainMenu();
            }
            catch (Exception ex) //ToDo: Catch exceptions properly!
            {

            }
        }

        public void Initialize()
        {

        }

        public void MainMenu()
        {
            int choice = 1;
            choice = UI.MenuPaged(choice, _mainMenuList);
            switch (choice)
            {
                case 1:
                    NewGame(1, 0, 4);       // ToDo: Single player (Start the server + 4 bots)
                    break;
                case 2:
                    newLobby();             // ToDo: Multiplayer (local) (Start the server + x bots)
                    break;
                case 3:
                    newLobby();             // ToDo: Multiplayer (remote) (Start the server + x bots)
                    break;
                case 4:
                    UI.Quit();
                    break;
            }
        }

        public void newLobby()
        {
            //  Establish number of local/hot-seat players.
            //  Establish number of remote players.
            //  Establish number of bots.
            
            //newGame();
        }

        public void NewGame(int players, int remote, int bots)
        {
            if (players < 1)
            {
                throw new ArgumentException("Too few players");
            }
            // ToDo: Add gameplay loop.
        }
    }
}