using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class InputPlayer
{
    public InputDevice device;
    public string actionMap;

    public InputPlayer(InputDevice device, string actionMap)
    {
        this.device = device;
        this.actionMap = actionMap;
    }
}
public static class GameVariables
{
    public static List<InputPlayer> inputPlayers = new List<InputPlayer>();
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;
    private void SpawnPlayers()
    {
        for (int i = 0; i < GameVariables.inputPlayers.Count; i++)
        {
            // 1. Instantiate the player
            PlayerInput newPlayer = PlayerInput.Instantiate(
                playerPrefab,
                pairWithDevice: GameVariables.inputPlayers[i].device
            );

            // 2. FORCE activate the default action map for this new player
            if (newPlayer != null)
            {
                newPlayer.neverAutoSwitchControlSchemes = true;
                // Switch to your action map name (usually "Player")
                InputUser.PerformPairingWithDevice(GameVariables.inputPlayers[i].device, newPlayer.user);
                newPlayer.SwitchCurrentActionMap(GameVariables.inputPlayers[i].actionMap);
                newPlayer.currentActionMap.Enable();
            }
        }        
    }


    private void Start()
    {
        SpawnPlayers();
    }

}
