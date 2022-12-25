using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public TextMeshProUGUI talkText;
    public GameObject interactUI;

    public void onInteractWithNPC(string text)
    {
        talkText.text = text;
        interactUI.SetActive(true);
        StartCoroutine(keepForSeconds());
    }

    private IEnumerator keepForSeconds()
    {
        yield return new WaitForSeconds(5f);
        interactUI.SetActive(false);
    }
}
