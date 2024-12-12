using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    public List<GameObject> level;
    public int currentLevelIndex = 0;

    public bool currentLevelComplete;

    public bool insideLift = false;

    public float transitionDelay = 2.0f;

    private void Start()
    {
        InitializeLevels();
    }
    private void Update()
    {

    }
    private void InitializeLevels()
    {
        for (int i = 0; i < level.Count; i++)
        {
            level[i].SetActive(i == currentLevelIndex);

            //LATER SET TRUE MANUALLY DEPENDING ON LEVEL, SET FALSE HERE!!!
            currentLevelComplete = true;
        }
    }

    public void TriggerLevelChange()
    {
        if (insideLift)
        {

            Invoke(nameof(DeactivateCurrentLevel), transitionDelay);
            Invoke(nameof(ActivateNextLevel), transitionDelay);

        }
    }
    private void DeactivateCurrentLevel()
    {
        //deactivate current
        level[currentLevelIndex].SetActive(false);

        //update index
        currentLevelIndex = (currentLevelIndex + 1) % level.Count;

    }
    private void ActivateNextLevel()
    {
        //activate current
        level[currentLevelIndex].SetActive(true);

        Debug.Log($"Switched to level {currentLevelIndex}");
    }
}
