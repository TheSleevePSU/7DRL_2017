using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Entities;

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
    public enum GameState { PlayerInput, PlayerExecute, ProjectileExecuteAfterPlayer, AiPlanning, AiExecute, ProjectileExecuteAfterAi, InGameMenu, LevelOver, GameOverWin, GameOverLose, GameStart }
    /// <summary>
    /// Current state of the game used by the GameManager's state machine
    /// </summary>
    public GameState gameState = GameState.GameStart;
    private GameState gameStateBeforeMenuDisplay = GameState.GameStart;

    private List<ITurnTaker> turnTakers = new List<ITurnTaker>();

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
        turnTakers = UpdateTurnTakerList();

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
            case GameState.ProjectileExecuteAfterPlayer:
                GameStateProjectileExecuteAfterPlayer();
                break;
            case GameState.AiPlanning:
                GameStateAiPlanning();
                break;
            case GameState.AiExecute:
                GameStateAiExecute();
                break;
            case GameState.ProjectileExecuteAfterAi:
                GameStateProjectileExecuteAfterAi();
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


    public static Instruction GetCurrentInstruction()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return Instruction.Up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return Instruction.Down;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return Instruction.Right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return Instruction.Left;
        }
        return Instruction.Nothing;
    }
    
    public static Vector2 GetTransformForInstruction(Instruction instruction, Vector2 currentLocation)
    {
        Vector2 targetPosition = currentLocation;

        switch (instruction)
        {
            case Instruction.Up:
                targetPosition += new Vector2(0, 1);
                break;
            case Instruction.Down:
                targetPosition += new Vector2(0, -1);
                break;
            case Instruction.Left:
                targetPosition += new Vector2(-1, 0);
                break;
            case Instruction.Right:
                targetPosition += new Vector2(1, 0);
                break;
        }

        return SnapToGrid(targetPosition, currentLocation);
    }

    private static Vector2 SnapToGrid(Vector2 destination, Vector2 origin)
    {
        Vector2 targetPosition = origin;

        Collider2D[] overlappingColliders = Physics2D.OverlapPointAll(destination);
        foreach (Collider2D collider in overlappingColliders)
        {
            Tile tile = collider.gameObject.GetComponent<Tile>();
            if (tile != null)
            {
                targetPosition = (tile.isWalkable) ? destination : origin;
            }
        }

        targetPosition.x = Mathf.RoundToInt(targetPosition.x);
        targetPosition.y = Mathf.RoundToInt(targetPosition.y);

        return targetPosition;
    }

    private void TransitionToGameState<T>(GameState nextGameState) where T : UnityEngine.Object
    {
        ITurnTaker[] thingsTakingTurns = FindObjectsOfType<T>() as ITurnTaker[];
        turnTakers = new List<ITurnTaker>(thingsTakingTurns);
        gameState = nextGameState;
        Debug.Log("GameState." + nextGameState.ToString());
        gameStateBeforeMenuDisplay = nextGameState;
    }

    private List<ITurnTaker> UpdateTurnTakerList()
    {
        List<ITurnTaker> cleanedList = new List<ITurnTaker>();
        foreach (ITurnTaker tt in turnTakers)
        {
            if (!tt.IsTurnFinished())
            {
                cleanedList.Add(tt);
            }
        }
        return cleanedList;
    } 

    /// <summary>
    /// Wait for player to input a command. If a valid input was received transition to PlayerExecute state.
    /// </summary>
    void GameStatePlayerInput()
    {
        if (turnTakers.Count == 0)
        {
            TransitionToGameState<Player>(GameState.PlayerExecute);
        }
    }
    /// <summary>
    /// The player is executing a turn. Iterate through all active Player objects (including projectiles) and call Execute(). Transition to AiPlanning after all active objects complete their actions.
    /// </summary>
    void GameStatePlayerExecute()
    {
        if (turnTakers.Count == 0)
        {
            TransitionToGameState<Assets.Scripts.Weapons.Projectile>(GameState.ProjectileExecuteAfterPlayer);
        }
    }
    void GameStateProjectileExecuteAfterPlayer()
    {
        if (turnTakers.Count == 0)
        {
            TransitionToGameState<Enemy>(GameState.AiPlanning);
        }
    }
    /// <summary>
    /// The Ai is planning its actions. Iterate through all Ai objects and call Plan(). Transition to AiExecute after all Ai entities have selected a valid action.
    /// </summary>
    void GameStateAiPlanning()
    {
        if (turnTakers.Count == 0)
        {
            TransitionToGameState<Enemy>(GameState.AiExecute);
        }
    }
    /// <summary>
    /// The Ai is executing its turn. Iterate through all active Ai objects and call Execute(). Transition to PlayerInput after all active objects complete their actions.
    /// </summary>
    void GameStateAiExecute()
    {
        if (turnTakers.Count == 0)
        {
            TransitionToGameState<Assets.Scripts.Weapons.Projectile>(GameState.ProjectileExecuteAfterAi);
        }
    }
    void GameStateProjectileExecuteAfterAi()
    {
        if (turnTakers.Count == 0)
        {
            TransitionToGameState<Player>(GameState.PlayerInput);
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
        //TODO Check for game start command
        if (true)
        {
            TransitionToGameState<Player>(GameState.PlayerInput);
        }
    }
}