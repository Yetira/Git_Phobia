using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    public List<GameObject> level;
    public int currentLevelIndex = 0;
    public bool currentLevelComplete;
    public bool insideLift = false;
    public float transitionDelay = 2.0f;

    //public List<string> volumeRTPCs = new List<string>();

    public float fadeDuration = 5.0f;

    public float focusFadeDuration = 5;
    private bool isFocusingHum;

    private void Start()
    {
        InitializeLevels();
    }

    private void Update()
    {
        if(currentLevelComplete && !isFocusingHum)
        {
            isFocusingHum=true;
            StartCoroutine(FocusElevatorHum("LevelAudioFade_RTCP"));
        }
    }

    private void InitializeLevels()
    {
        for (int i = 0; i < level.Count; i++)
        {
            level[i].SetActive(i == currentLevelIndex);
            currentLevelComplete = false;
            isFocusingHum = false;
        }
    }

    public void TriggerLevelChange()
    {
        if (insideLift)
        {
            // Fade out before deactivating
            StartCoroutine(FadeOutAudio("LevelAudioFade_RTCP", () =>
            {
                DeactivateCurrentLevel();
                Invoke(nameof(ActivateNextLevel), transitionDelay);

            }));
        }
    }

    private void DeactivateCurrentLevel()
    {
        StopAllSoundsInLevel(level[currentLevelIndex]);

        level[currentLevelIndex].SetActive(false);
        currentLevelIndex = (currentLevelIndex + 1) % level.Count;
  
    }


    private void StopAllSoundsInLevel(GameObject level)
    {
        foreach (Transform child in level.transform)
        {
            AkSoundEngine.StopAll(child.gameObject);
        }
    }
   

    private void ActivateNextLevel()
    {
        
        level[currentLevelIndex].SetActive(true);
        Debug.Log($"Switched to level {currentLevelIndex}");

        StartCoroutine(FadeInAudio("LevelAudioFade_RTCP"));

        currentLevelComplete = false;
        isFocusingHum = false;

    }

    private IEnumerator FadeOutAudio(string rtpcName, System.Action onComplete)
    {
        float elapsedTime = 0f;
        // start 50 when focus hum goes down to there?
        float startValue = 15;
        float targetValue = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, targetValue, elapsedTime / fadeDuration);
            AkSoundEngine.SetRTPCValue(rtpcName, newValue);
            yield return null;
        }

        AkSoundEngine.SetRTPCValue(rtpcName, targetValue);

        onComplete?.Invoke();
    }

    private IEnumerator FadeInAudio(string rtpcName)
    {
        yield return new WaitForSeconds(15);
        
        float elapsedTime = 0f;
        float startValue = 0f;
        float targetValue = 100f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, targetValue, elapsedTime / fadeDuration);
            AkSoundEngine.SetRTPCValue(rtpcName, newValue);
            yield return null;
        }

        AkSoundEngine.SetRTPCValue(rtpcName, targetValue);
    }

    private IEnumerator FocusElevatorHum(string rtpcName)
    {
        Debug.Log("focusing elevator.");
        
        yield return new WaitForSeconds(20);

        float elapsedTime = 0f;
        float startValue = 100f;
        float targetValue = 15f;

        while (elapsedTime < focusFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, targetValue, elapsedTime / focusFadeDuration);
            AkSoundEngine.SetRTPCValue(rtpcName, newValue);
            yield return null;
        }

        AkSoundEngine.SetRTPCValue(rtpcName, targetValue);
    }
}