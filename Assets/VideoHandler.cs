using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.UI; // Todavía necesario por si usas otros elementos de UI estándar
using TMPro;         // ¡Importante! Agrega el namespace de TextMesh Pro

public class VideoAudioManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource videoAudioSource;
    public AudioSource nextAudioSource;
    public GameObject videoCanvas;
    public GameObject dialogueCanvas;
    public float fadeDuration = 2f;

    [System.Serializable]
    public class Subtitle
    {
        public string text;
        public float startTime;
        public float endTime;
    }

    public Subtitle[] subtitles;
    public TMP_Text subtitleText; // Cambia el tipo a TMP_Text (clase base para TextMeshProUGUI)

    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false);
        }
        if (subtitleText != null)
        {
            subtitleText.text = "";
        }
    }

    void Update()
    {
        if (videoPlayer.isPlaying && subtitleText != null)
        {
            DisplaySubtitles();
        }
    }

    void DisplaySubtitles()
    {
        float currentTime = (float)videoPlayer.time;
        string currentSubtitle = "";

        foreach (Subtitle sub in subtitles)
        {
            if (currentTime >= sub.startTime && currentTime <= sub.endTime)
            {
                currentSubtitle = sub.text;
                break;
            }
        }

        subtitleText.text = currentSubtitle;
    }

    void EndReached(VideoPlayer vp)
    {
        StartCoroutine(FadeOutVideoAudioAndPlayNext());
    }

    IEnumerator FadeOutVideoAudioAndPlayNext()
    {
        float startVolume = videoAudioSource.volume;

        for (float t = 0; t <= fadeDuration; t += Time.deltaTime)
        {
            videoAudioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        videoAudioSource.volume = 0;
        videoAudioSource.Stop();

        if (videoCanvas != null)
        {
            videoCanvas.SetActive(false);
        }

        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(true);
        }

        nextAudioSource.Play();

        Time.timeScale = 1;
    }
}