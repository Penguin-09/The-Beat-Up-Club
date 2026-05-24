using UnityEngine;
using TMPro;

public class DebugHandling : MonoBehaviour
{
    public TextMeshProUGUI debugText;

    public void PrintDebugText(string debugText)
    {
        this.debugText.text = debugText;
        StartCoroutine(ClearDebugText());
    }

    private System.Collections.IEnumerator ClearDebugText()
    {
        yield return new WaitForSeconds(3);
        this.debugText.text = "";
    }
}
