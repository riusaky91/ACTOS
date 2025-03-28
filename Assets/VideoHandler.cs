using UnityEngine;
using UnityEngine.Video;

public class VideoHandlerWithAudio : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Asigna tu VideoPlayer en el inspector
    public AudioSource audioSource; // Asigna tu AudioSource en el inspector
    public GameObject videoCanvas;  // El Canvas que contiene el video

    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        audioSource.Stop();           // Detiene el audio
        videoCanvas.SetActive(false); // Desactiva el Canvas del video
        Time.timeScale = 1;           // Reanuda el juego
    }
}
