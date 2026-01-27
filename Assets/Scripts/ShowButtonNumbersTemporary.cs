using UnityEngine;
using TMPro;
using System.Collections;

public class ShowButtonNumbersTemporary : MonoBehaviour
{
    public TMP_Text displayText;       // Das TMP Text Objekt über dem Button
    [TextArea]
    public string numbersToShow;       // z. B. "1 / 2 / 3 / 4"
    public float displayTime = 5f;     // Sekunden, wie lange die Zahlen angezeigt werden

    private Coroutine currentCoroutine;

    // Diese Funktion rufst du bei Button Klick auf
    public void OnButtonPressed()
    {
        // set visibility of text to active
        displayText.enabled = true;
        // Wenn der Timer schon läuft, stoppe ihn, um neu zu starten
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        // Zahlen anzeigen und Timer starten
        displayText.text = numbersToShow;
        currentCoroutine = StartCoroutine(HideAfterSeconds());
    }

    private IEnumerator HideAfterSeconds()
    {
        yield return new WaitForSeconds(displayTime);
        displayText.text = "";  // Zahlen wieder ausblenden
        currentCoroutine = null;
    }
    void OnMouseDown()
    {
    OnButtonPressed();
    }

    void Start()
    {
        displayText.enabled = false;
    }
}