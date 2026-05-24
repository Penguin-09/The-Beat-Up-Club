using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameLogicHandler : MonoBehaviour
{
    public Button startButton;
    public GameObject playerPrefab;
    public Transform spawn1;
    public Transform spawn2;
    public CameraController cameraController;

    public void StartFight()
    {
        startButton.gameObject.SetActive(false);

        PlayerInput player1Input;
        if (Keyboard.current != null && Gamepad.all.Count > 0)
        {
            player1Input = PlayerInput.Instantiate(
                playerPrefab,
                controlScheme: "Keyboard+Gamepad",
                pairWithDevices: new InputDevice[] { Keyboard.current, Gamepad.all[0] });
        }
        else if (Keyboard.current != null)
        {
            player1Input = PlayerInput.Instantiate(
                playerPrefab,
                controlScheme: "Keyboard",
                pairWithDevice: Keyboard.current);
        }
        else if (Gamepad.all.Count > 0)
        {
            player1Input = PlayerInput.Instantiate(
                playerPrefab,
                controlScheme: "Gamepad",
                pairWithDevice: Gamepad.all[0]);
        }
        else
        {
            player1Input = PlayerInput.Instantiate(playerPrefab);
        }

        player1Input.transform.SetPositionAndRotation(spawn1.position, spawn1.rotation);
        player1Input.name = "Player1";

        PlayerInput player2Input;
        if (Gamepad.all.Count > 1)
        {
            player2Input = PlayerInput.Instantiate(
                playerPrefab,
                controlScheme: "Gamepad",
                pairWithDevice: Gamepad.all[1]);
            player2Input.transform.SetPositionAndRotation(spawn2.position, spawn2.rotation);
            player2Input.name = "Player2";
        }
        else
        {
            GameObject player2 = Instantiate(playerPrefab, spawn2.position, spawn2.rotation);
            player2.name = "Player2";
            player2Input = player2.GetComponent<PlayerInput>();
        }

        cameraController.player1 = player1Input.transform;
        cameraController.player2 = player2Input.transform;
        cameraController.enabled = true;
    }
}