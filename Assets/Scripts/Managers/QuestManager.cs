using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    List<string> quests = new List<string>()
    {
        "Talk with Idoen to start a new wave!",
        "Clear the wave!",
        "Go back to run table to upgrade your skills.",
        "Now you're ready to do this thousand times more!",
    };

    public int questNumber = 0;

    public TextMeshProUGUI questText;

    // Start is called before the first frame update
    void Start()
    {
        questText.text = quests[questNumber];
    }

    public void nextQuest()
    {
        questNumber += 1;
        questText.text = quests[questNumber];
    }
}
