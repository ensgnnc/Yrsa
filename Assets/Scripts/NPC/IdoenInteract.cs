using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdoenInteract : Interactable
{
    int waveLevel = 1;

    public WaveManager waveManager;

    private bool isFirstTime = true;

    public QuestManager questManager;
    public TalkManager talkManager;

    List<string> sentences = new List<string>()
    {
        "I heard that you want a new enemy. Here is some for you!",
        "You better get ready for this.",
    };

    List<string> notDone = new List<string>()
    {
        "Complate your wave first!",
    };

    public override void Interact()
    {
        if (isFirstTime)
        {
            questManager.nextQuest();
            isFirstTime = false;
        }
        if (waveManager.isWaveRunning)
        {
            talkManager.onInteractWithNPC(notDone[UnityEngine.Random.Range(0, notDone.Count)]);
            return;
        }

        talkManager.onInteractWithNPC(sentences[UnityEngine.Random.Range(0, sentences.Count)]);

        waveManager.startWave(waveLevel);

    }
}
