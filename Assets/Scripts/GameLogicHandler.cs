using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameLogicHandler : MonoBehaviour
{
    public DebugHandling debugHandler;
    public Button startButton;
    public GameObject playerPrefab;
    public Transform spawn1;
    public Transform spawn2;
    public CameraController cameraController;
    private PlayerInput player1Input;
    private PlayerInput player2Input;

    public void StartFight()
    {
        startButton.gameObject.SetActive(false);
        debugHandler.PrintDebugText("Fight started");

        AssignDevices();
        
        // Initialize and activate camera
        cameraController.player1 = player1Input.transform;
        cameraController.player2 = player2Input.transform;
        cameraController.enabled = true;
    }

    public void AssignDevices()
    {
        // PLAYER 1
        if (Keyboard.current != null && Gamepad.all.Count == 0)
        {
            // Keyboard only
            player1Input = PlayerInput.Instantiate(
                playerPrefab,
                controlScheme: "Keyboard",
                pairWithDevice: Keyboard.current);
        }
        else if (Keyboard.current != null && Gamepad.all.Count > 0)
        {
            // Keyboard + Gamepad
            player1Input = PlayerInput.Instantiate(
                playerPrefab,
                controlScheme: "Keyboard+Gamepad",
                pairWithDevices: new InputDevice[] { Keyboard.current, Gamepad.all[0] });
        }
        else
        {
            // No input devices found, instantiate without pairing
            player1Input = PlayerInput.Instantiate(playerPrefab);
        }

        player1Input.transform.SetPositionAndRotation(spawn1.position, spawn1.rotation);
        player1Input.name = "Player1";
        
        // PLAYER 2
        if (Gamepad.all.Count > 1)
        {
            // Gamepad only
            player2Input = PlayerInput.Instantiate(
                playerPrefab,
                controlScheme: "Gamepad",
                pairWithDevice: Gamepad.all[1]);
        }
        else
        {
            // Not enough gamepads, instantiate without pairing
            player2Input = PlayerInput.Instantiate(playerPrefab);
        }
        
        player2Input.transform.SetPositionAndRotation(spawn2.position, spawn2.rotation);
        player2Input.name = "Player2";
    }
}