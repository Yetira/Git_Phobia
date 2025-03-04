using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerEndVoiceline : MonoBehaviour
{
    public VoiceLineManager voiceLineManager;

    private bool isProcessing;

    public float EndFadeDuration;

    private void Start()
    {
        isProcessing = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isProcessing)
        {
            isProcessing=true;
            
            voiceLineManager.PlayOutro();

            Debug.Log("Play Game Outro");

            StartCoroutine(FadeOutAudio("allSoundsButNarratorVolume", () =>
            {
                //end game here
                Debug.Log("Game is over and you won, I promise!");
            }));
        }
    }
    private IEnumerator FadeOutAudio(string rtpcName, System.Action onComplete)
    {
        float elapsedTime = 0f;
        float startValue = 100f;
        float targetValue = 0f;

        while (elapsedTime < EndFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, targetValue, elapsedTime / EndFadeDuration);
            AkSoundEngine.SetRTPCValue(rtpcName, newValue);
            yield return null;
        }

        AkSoundEngine.SetRTPCValue(rtpcName, targetValue);

        onComplete?.Invoke();
    }
}
