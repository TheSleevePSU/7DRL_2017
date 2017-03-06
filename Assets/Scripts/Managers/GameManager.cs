using UnityEngine;
using System.Collections.Generic;

/* TODO
 * Keep track of all entities in the scene
 * Render GUI overlay : void OnGUI()
*/

/// GameManager is a singleton.
/// To avoid having to manually link an instance to every class that needs it, it has a static property called
/// instance, so other objects that need to access it can just call:
///        GameManager.instance.DoSomeThing();
///
public class GameManager : MonoBehaviour {
    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static GameManager s_Instance = null;
    public enum GameState { PlayerInput, PlayerExecute, AiPlanning, AiExecute, InGameMenu, LevelOver, GameOverWin, GameOverLose, GameStart }
    /// <summary>
    /// Current state of the game used by the GameManager's state machine
    /// </summary>
    public GameState gameState = GameState.GameStart;
    private GameState gameStateBeforeMenuDisplay = GameState.GameStart;

    private List<Enemy> enemiesInScene;
    private List<Player> playersInScene;
    private List<Enemy> enemiesExecutingTurn;
    private List<Player> playersExecutingTurn;
    private bool playerInputReceived = false;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static GameManager instance {
        get {
            if (s_Instance == null) {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first GameManager object in the scene.
                s_Instance =  FindObjectOfType(typeof (GameManager)) as GameManager;
            }
    
            // If it is still null, create a new instance
            if (s_Instance == null) {
                GameObject obj = new GameObject("GameManager");
                s_Instance = obj.AddComponent(typeof (GameManager)) as GameManager;
                Debug.Log("Could not locate an GameManager object. GameManager was Generated Automatically.");
            }
    
            return s_Instance;
        }
    }
    
    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit() {
        s_Instance = null;
    }
    
    void Start()
    {
         
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState == GameState.InGameMenu)
            {
                gameState = gameStateBeforeMenuDisplay;
                Debug.Log("Menu cleared");
            }
            else
            {
                gameState = GameState.InGameMenu;
                Debug.Log("GameState.InGameMenu");
            }
        }

        switch (gameState)
        {
            case GameState.PlayerInput:
                GameStatePlayerInput();
                break;
            case GameState.PlayerExecute:
                GameStatePlayerExecute();
                break;
            case GameState.AiPlanning:
                GameStateAiPlanning();
                break;
            case GameState.AiExecute:
                GameStateAiExecute();
                break;
            case GameState.InGameMenu:
                GameStateInGameMenu();
                break;
            case GameState.LevelOver:
                GameStateLevelOver();
                break;
            case GameState.GameOverWin:
                GameStateGameOverWin();
                break;
            case GameState.GameOverLose:
                GameStateGameOverLose();
                break;
            case GameState.GameStart:
                GameStateGameStart();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Wait for player to input a command. If a valid input was received transition to PlayerExecute state.
    /// </summary>
    void GameStatePlayerInput()
    {
        if (playerInputReceived)
        {
            playersExecutingTurn = new List<Player>(playersInScene);
            gameState = GameState.PlayerExecute;
            Debug.Log("GameState.PlayerExecute");
            gameStateBeforeMenuDisplay = GameState.PlayerExecute;
            playerInputReceived = false;
        }
    }
    /// <summary>
    /// The player is executing a turn. Iterate through all active Player objects (including projectiles) and call Execute(). Transition to AiPlanning after all active objects complete their actions.
    /// </summary>
    void GameStatePlayerExecute()
    {
        if (playersExecutingTurn.Count == 0)
        {
            gameState = GameState.AiPlanning;
            Debug.Log("GameState.AiPlanning");
            gameStateBeforeMenuDisplay = GameState.AiPlanning;
        }
    }
    /// <summary>
    /// The Ai is planning its actions. Iterate through all Ai objects and call Plan(). Transition to AiExecute after all Ai entities have selected a valid action.
    /// </summary>
    void GameStateAiPlanning()
    {
        //TODO Check for Ai planning completed
        if (true)
        {
            gameState = GameState.AiExecute;
            Debug.Log("GameState.AiExecute");
            gameStateBeforeMenuDisplay = GameState.AiExecute;
        }
    }
    /// <summary>
    /// The Ai is executing its turn. Iterate through all active Ai objects and call Execute(). Transition to PlayerInput after all active objects complete their actions.
    /// </summary>
    void GameStateAiExecute()
    {
        //TODO Check for Ai turn executed
        if (true)
        {
            gameState = GameState.PlayerInput;
            Debug.Log("GameState.PlayerInput");
            gameStateBeforeMenuDisplay = GameState.PlayerInput;
        }
    }
    /// <summary>
    /// Player is viewing the in-game menu, which pauses the action.
    /// </summary>
    void GameStateInGameMenu()
    {
        
    }
    /// <summary>
    /// The player has finished the level. Show stats from the completed level. Check to see if the game has been won. If not, generate a new level.
    /// </summary>
    void GameStateLevelOver()
    {
        //TODO Check for level over condition
        if (true)
        {
            gameState = GameState.PlayerInput;
            Debug.Log("GameState.PlayerInput");
            gameStateBeforeMenuDisplay = GameState.PlayerInput;
        }
    }
    /// <summary>
    /// The player has won the game. Display win screen and provide option to restart game.
    /// </summary>
    void GameStateGameOverWin()
    {
        //TODO Check for game win condition
        if (true)
        {
            gameState = GameState.GameStart;
            Debug.Log("GameState.GameStart");
            gameStateBeforeMenuDisplay = GameState.GameStart;
        }
    }
    /// <summary>
    /// The player has lost the game. Display lose screen and provide option to restart game.
    /// </summary>
    void GameStateGameOverLose()
    {
        //TODO Check for game lose condition
        if (true)
        {
            gameState = GameState.GameStart;
            Debug.Log("GameState.GameStart");
            gameStateBeforeMenuDisplay = GameState.GameStart;
        }
    }
    /// <summary>
    /// When the game first starts up. Display title and menu.
    /// </summary>
    void GameStateGameStart()
    {
        RegisterAllObjectsInScene();
        //TODO Check for game start command
        if (true)
        {
            gameState = GameState.PlayerInput;
            Debug.Log("GameState.PlayerInput");
            gameStateBeforeMenuDisplay = GameState.PlayerInput;
        }
    }

    void RegisterAllObjectsInScene()
    {
        enemiesInScene = new List<Enemy>();
        playersInScene = new List<Player>();

        Enemy[] foundEnemies = FindObjectsOfType(typeof(Enemy)) as Enemy[];
        foreach (Enemy e in foundEnemies)
        {
            Enemy enemy = e.GetComponent<Enemy>();
            if (enemy != null) RegisterEnemy(e);
        }
        Player[] foundPlayers = FindObjectsOfType(typeof(Player)) as Player[];
        foreach (Player p in foundPlayers)
        {
            Player player = p.GetComponent<Player>();
            if (player != null) RegisterPlayer(p);
        }
    }

    public void RegisterEnemy(Enemy e)
    {
        if (!enemiesInScene.Contains(e)) enemiesInScene.Add(e);
    }

    public void DeRegisterEnemy(Enemy e)
    {
        if (enemiesInScene.Contains(e)) enemiesInScene.Remove(e);
        if (enemiesExecutingTurn.Contains(e)) enemiesExecutingTurn.Remove(e);
    }

    public void RegisterPlayer(Player p)
    {
        if (!playersInScene.Contains(p)) playersInScene.Add(p);
    }

    public void DeRegisterPlayer(Player p)
    {
        if (playersInScene.Contains(p)) playersInScene.Remove(p);
        if (playersExecutingTurn.Contains(p)) playersExecutingTurn.Remove(p);
    }

    public void OnValidPlayerInputReceived()
    {
        playerInputReceived = true;
    }

    public void OnPlayerTurnCompleted(object o)
    {
        if (o.GetType() == typeof(Player))
        {
            Player p = o as Player;
            if (playersExecutingTurn.Contains(p)) playersExecutingTurn.Remove(p);
        }
    }
}