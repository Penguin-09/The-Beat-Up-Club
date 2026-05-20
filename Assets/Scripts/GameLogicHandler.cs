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

        GameObject player1 = Instantiate(playerPrefab, spawn1.position, spawn1.rotation);
        player1.name = "Player1";
        GameObject player2 = Instantiate(playerPrefab, spawn2.position, spawn2.rotation);
        player2.name = "Player2";

        var player1Input = player1.GetComponent<PlayerInput>();
        var player2Input = player2.GetComponent<PlayerInput>();

        // Pair Keyboard + Gamepad[0] to player1
        if (Keyboard.current != null)
        {
            if (Gamepad.all.Count > 0)
            {
                player1Input.SwitchCurrentControlScheme("Keyboard+Gamepad", Keyboard.current, Gamepad.all[0]);
            }
            else
            {
                player1Input.SwitchCurrentControlScheme("Keyboard", Keyboard.current);
            }
        }

        // Pair second gamepad to player2
        if (Gamepad.all.Count > 1)
        {
            player2Input.SwitchCurrentControlScheme("Gamepad", Gamepad.all[1]);
        }

        cameraController.player1 = player1.transform;
        cameraController.player2 = player2.transform;
        cameraController.enabled = true;
    }
}