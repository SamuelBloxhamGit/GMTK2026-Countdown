using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
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
    public List<PlayerController> alivePlayers = new List<PlayerController>();

    public Color[] playerColours;
    public Color[] batColours;

    [SerializeField]
    GameObject endingUI;
    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    GameObject[] levels;

    [SerializeField]
    public Transform[] playerSpawns;

    public static GameManager instance;

    bool endUiOpen = false;
    bool pauseinCooldown = false;


    public void PauseGame()
    {

        if (pauseinCooldown)
        {
            return;
        }
        else
        {
            print("sdkjahdsa");
            pauseinCooldown = true;

            endingUI.SetActive(!endUiOpen);

            if (endUiOpen)
            {
                endUiOpen = false;
            }
            else
            {
                endUiOpen = true;
            }

            Invoke("ResetPauseCool", 0.1f);
        }
    }

    void ResetPauseCool()
    {
        pauseinCooldown = false;
    }

    private void Awake()
    {
        instance = this;

        levels[UnityEngine.Random.Range(0, levels.Length)].SetActive(true);
    }

    Coroutine hitStop;

    public void HitStop(float intensity)
    {
        if ((hitStop!=null)) StopCoroutine(hitStop);
        hitStop = StartCoroutine(iHitStop(intensity));
    }

    IEnumerator iHitStop(float intensity)
    {
        Time.timeScale = 0.05f;
        yield return new WaitForSeconds(1 * 0.005f);
        Time.timeScale = 1f;
    }

    private void DEBUGPopulatePlayers()
    {
        GameVariables.inputPlayers.Add(new InputPlayer(Keyboard.current, "Keyboard_WASD"));
        GameVariables.inputPlayers.Add(new InputPlayer(Keyboard.current, "Keyboard_Arrows"));
    }

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
                if (Mouse.current != null)
                {
                    InputUser.PerformPairingWithDevice(Mouse.current, newPlayer.user);
                }
                newPlayer.SwitchCurrentActionMap(GameVariables.inputPlayers[i].actionMap);
                newPlayer.currentActionMap.Enable();
            }
        }        
    }



    private void Start()
    {
#if UNITY_EDITOR
        //DEBUGPopulatePlayers();
#endif
        SpawnPlayers();


    }

    public TMP_Text winnerText;
    public TMP_Text topBatText;

    public Button playAgain;
    public Button back;

    public void CheckAlivePlayers()
    {
        print(alivePlayers.Count);
        if(alivePlayers.Count == 1)
        {
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        alivePlayers[0].StopAllCoroutines();
        alivePlayers[0].controlsActive = false;

        StartCoroutine(AudioManager.instance.FadeSound(AudioManager.instance.battleAudioSource, false));
        yield return new WaitForSeconds(1.6f);
        winnerText.gameObject.SetActive(false);
        topBatText.gameObject.SetActive(false);
        playAgain.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
        endingUI.SetActive(true);

        AudioManager.instance.PlaySound(4);

        yield return new WaitForSeconds(1f);

        winnerText.color = playerColours[alivePlayers[0].playerID];
        winnerText.text = "Player " + (alivePlayers[0].playerID + 1);
        winnerText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        topBatText.gameObject.SetActive(true);


        yield return new WaitForSeconds(2f);
        playAgain.gameObject.SetActive(true);
        back.gameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(playAgain.gameObject);
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

}
