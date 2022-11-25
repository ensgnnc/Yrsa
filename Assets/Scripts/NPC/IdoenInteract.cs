using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdoenInteract : Interactable
{
    int waveLevel = 1;

    public WaveManager waveManager;

    List<string> sentences = new List<string>()
    {
        "So, you want a new wave!",
        "You better get ready for this.",
    };

    public override void Interact()
    {
        if (waveManager.isWaveRunning)
        {
            print("Complate your wave first!");
            return;
        }

        int index = UnityEngine.Random.Range(0, sentences.Count);
        string selectedSentence = sentences[index];

        waveManager.startWave(waveLevel);

        print(selectedSentence);
    }
}
