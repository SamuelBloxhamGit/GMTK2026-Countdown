using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    GameObject[] playerPortraits;


    [SerializeField]
    Button startButton;

    [SerializeField]
    Button resetButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameVariables.inputPlayers.Clear();
        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
    }
    public bool playerWASDJoined = false;
    public bool playerArrowsJoined = false;

    // Update is called once per frame
    void Update()
    {

        CheckKeyboardJoins();
        CheckGamepadJoins();


    }


    private void CheckKeyboardJoins()
    {
        if (Keyboard.current == null) return;

        // Check for WASD
        if (!playerWASDJoined && (Keyboard.current.wKey.wasPressedThisFrame ||
                                 Keyboard.current.aKey.wasPressedThisFrame ||
                                 Keyboard.current.sKey.wasPressedThisFrame ||
                                 Keyboard.current.dKey.wasPressedThisFrame))
        {
            SpawnKeyboardPlayer("Keyboard_WASD");
            playerWASDJoined = true;
        }

        // Check for Arrow Keys
        if (!playerArrowsJoined && (Keyboard.current.upArrowKey.wasPressedThisFrame ||
                                    Keyboard.current.downArrowKey.wasPressedThisFrame ||
                                    Keyboard.current.leftArrowKey.wasPressedThisFrame ||
                                    Keyboard.current.rightArrowKey.wasPressedThisFrame))
        {
            SpawnKeyboardPlayer("Keyboard_Arrows");
            playerArrowsJoined = true;
        }
    }

    private void CheckGamepadJoins()
    {
        // Loop through all connected gamepads
        foreach (Gamepad gamepad in Gamepad.all)
        {
            // Check if ANY button/control on this gamepad was pressed this frame
            if (WasAnyButtonPressed(gamepad))
            {
                // Check if this gamepad is already assigned to an existing player
                if (!IsGamepadAlreadyPaired(gamepad))
                {
                    PlayerInput newPlayer = PlayerInput.Instantiate(
            playerPrefab,
            pairWithDevice: gamepad,
            controlScheme: "Gamepad"
        );

                    if (newPlayer != null)
                    {
                        //newPlayer.neverAutoSwitchControlSchemes = true;
                        // Switch to your action map name (usually "Player")
                        InputUser.PerformPairingWithDevice(gamepad, newPlayer.user);
                        newPlayer.SwitchCurrentActionMap("Gamepad");
                        newPlayer.currentActionMap.Enable();

                        PlayerAdded(gamepad, "Gamepad");
                    }

                }
            }
        }
    }

    private bool IsGamepadAlreadyPaired(Gamepad gamepad)
    {
        foreach (var player in PlayerInput.all)
        {
            if (player.devices.Contains(gamepad))
            {
                return true;
            }
        }
        return false;
    }

    // Helper method to scan all controls on a specific gamepad
    private bool WasAnyButtonPressed(Gamepad gamepad)
    {
        foreach (var control in gamepad.allControls)
        {
            // Ensure control is a button and check if pressed this frame
            if (control is UnityEngine.InputSystem.Controls.ButtonControl button)
            {
                if (button.wasPressedThisFrame)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void SpawnKeyboardPlayer(string controlScheme)
    {
        // 1. Instantiate the player
        PlayerInput newPlayer = PlayerInput.Instantiate(
            playerPrefab,
            pairWithDevice: Keyboard.current
        );

        if (newPlayer != null)
        {
            InputUser.PerformPairingWithDevice(Keyboard.current, newPlayer.user);
            newPlayer.SwitchCurrentActionMap(controlScheme);
            newPlayer.currentActionMap.Enable();

            PlayerAdded(Keyboard.current, controlScheme);

            // --- FIX: Clear UI selection so Arrow Keys control player, not UI ---
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public TMP_Text countdownText;
    int countdown = 20;

    public void iStarterCountdown()
    {
        StartCoroutine(StarterCountdown());
    }

    public IEnumerator StarterCountdown()
    {

        countdownText.text = countdown.ToString();

        while (countdown > 0)
        {
            yield return new WaitForSeconds(1);

            //countdownText.color = Color.white;

            //if (countdown < 3)
            //{
            //    AudioManager.instance.PlaySound(2);
            //    countdownText.color = Color.red;
            //}

            countdown--;

            countdownText.text = countdown.ToString();


        }
        countdownText.text = "";
        countdown = 20;


            if (GameVariables.inputPlayers.Count > 1)
            {
            BeginGame();
        }
        else
        {
            notEnoughPlayers.gameObject.SetActive(true);
            iStarterCountdown();
        }

    }

    [SerializeField]
    TMP_Text notEnoughPlayers;

    void PlayerAdded(InputDevice device, string actionMap)
    {
        GameVariables.inputPlayers.Add(new InputPlayer(device, actionMap));
        playerPortraits[GameVariables.inputPlayers.Count -1].SetActive(true);

        if (GameVariables.inputPlayers.Count > 1)
        {
            notEnoughPlayers.gameObject.SetActive(false);
        }
    }

    public void QuickStart()
    {
        if (GameVariables.inputPlayers.Count > 1)
        {
            BeginGame();
        }
    }

    public void QUIT()
    {
        Application.Quit();
    }

    public void BeginGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    [SerializeField]
    Button beginButton;

}
