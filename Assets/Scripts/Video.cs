using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private bool hasStarted = false;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        hasStarted = false;
        videoPlayer.StepForward(); // Step forward once to initialize
        Invoke("StepForwardFrames", 0.1f); // Slight delay to step forward additional frames
    }

    void StepForwardFrames()
    {
        videoPlayer.StepForward();
        videoPlayer.StepForward();
        videoPlayer.StepForward();

        videoPlayer.frame = 0; // Ensure the frame is set to 0 after stepping forward
        videoPlayer.Play();
    }

    void Update()
    {
        if (!hasStarted && videoPlayer.isPlaying)
        {
            hasStarted = true;
        }
    }
}