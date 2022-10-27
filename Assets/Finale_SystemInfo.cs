using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finale_SystemInfo : MonoBehaviour
{
    public enum Phase
    {
        Phase0,
        Phase1,
        Phase2,
        Phase3,
        Complete
    };

    public GameObject[] enableInPhase1;
    public GameObject[] enableInPhase2;
    public GameObject[] enableInPhase3;

    public Phase currentPhase = Phase.Phase0;
    public void NextPhase()
    {
        if (currentPhase != Phase.Complete)
        {
            currentPhase++;
            StartPhase();   
        }
    }

    void StartPhase()
    {
        switch (currentPhase)
        {
            case Phase.Phase1:
                EnableSymbols(enableInPhase1);
                break;
            case Phase.Phase2:
                EnableSymbols(enableInPhase2);
                break;
            case Phase.Phase3:
                EnableSymbols(enableInPhase3);
                break;
            case Phase.Complete:
                break;
        }
    }

    void EnableSymbols(GameObject[] symbolsToEnable)
    {
        foreach (var symbolGroup in symbolsToEnable)
        {
            symbolGroup.transform.Find("Active").gameObject.SetActive(true);
            symbolGroup.transform.Find("Inactive").gameObject.SetActive(false);
        }
    }
}
