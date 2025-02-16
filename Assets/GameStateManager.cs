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

    private void Start()
    {
        InitializeLevels();

    }

    private void InitializeLevels()
    {
        for (int i = 0; i < level.Count; i++)
        {
            level[i].SetActive(i == currentLevelIndex);
            currentLevelComplete = false;
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
    }

    private IEnumerator FadeOutAudio(string rtpcName, System.Action onComplete)
    {
        float elapsedTime = 0f;
        float startValue = 100f;
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
        yield return new WaitForSeconds(10);
        
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
}