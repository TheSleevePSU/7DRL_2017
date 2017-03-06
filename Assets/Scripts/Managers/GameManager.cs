using UnityEngine;
using System.Collections.Generic;
 
/// GameManager is a singleton.
/// To avoid having to manually link an instance to every class that needs it, it has a static property called
/// instance, so other objects that need to access it can just call:
///        GameManager.instance.DoSomeThing();
///
public class GameManager : MonoBehaviour {
    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static GameManager s_Instance = null;
    public enum GameState { Paused, Running }
    
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
    
    // Add the rest of the code here...
    public void DoSomeThing() {
        Debug.Log("Doing something now", this);
    }
    
    void Start()
    {
         
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Space key was pressed");
        }
    }

    /* TODO
     * Enum of all possible game states
     *      PlayerInput : Wait for player to input command. Transition to PlayerExecute after valid input.
     *      PlayerExecute : Iterate through all active player objects and call Execute(). Transition to AiPlanning after all active objects complete their actions.
     *      AiPlanning : Iterate through all Ai objects and call Plan(). Transition to AiExecute after all Ai entities have selected a valid action.
     *      AiExecute : Iterate through all active Ai objects and call Execute(). Transition to PlayerInput after all active objects complete their actions.
     *      InGameMenu : When player presses Esc to view menu?
     *      LevelOver : When player finishes level and transitions to next level.
     *      GameOverWin : When player achieves win condition.
     *      GameOverLose : When player achieves lose condition.
     *      GameStart : When the game first starts up.
     * Keep track of game state and implement basic state machine for changing game state
     * Keep track of all entities in the scene
     * Render GUI overlay : void OnGUI()
    */
}