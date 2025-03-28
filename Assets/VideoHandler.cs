using UnityEngine; // Declaraciones using deben ir al inicio
using UnityEngine.Video;
using System.Collections;

public class VideoAudioManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;      // Asigna tu VideoPlayer en el inspector
    public AudioSource videoAudioSource; // AudioSource del audio del video
    public AudioSource nextAudioSource;  // AudioSource del siguiente audio
    public GameObject videoCanvas;       // El Canvas que contiene el video
    public float fadeDuration = 2f;      // Duración del fade out en segundos

    void Start()
    {
        videoPlayer.loopPointReached += EndReached; // Detectar fin del video
    }

    void EndReached(VideoPlayer vp)
    {
        StartCoroutine(FadeOutVideoAudioAndPlayNext()); // Iniciar fade out y reproducir siguiente audio
    }

    IEnumerator FadeOutVideoAudioAndPlayNext()
    {
        float startVolume = videoAudioSource.volume;

        // Realizar el fade out del audio del video
        for (float t = 0; t <= fadeDuration; t += Time.deltaTime)
        {
            videoAudioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null; // Esperar hasta el siguiente frame
        }

        // Asegurarse de que el volumen sea 0 y detener el audio
        videoAudioSource.volume = 0;
        videoAudioSource.Stop();

        // Desactivar el Canvas del video
        videoCanvas.SetActive(false);

        // Reproducir el siguiente audio
        nextAudioSource.Play();

        // Reanudar el juego
        Time.timeScale = 1;
    }
}
