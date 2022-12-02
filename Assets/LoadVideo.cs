using UnityEngine;
using UnityEngine.Video;

public class LoadVideo : MonoBehaviour
{
    public VideoPlayer myVideoPlayer;
    public string videoName;
    private bool waiting = true;
    void Start()
    {
        string videoUrl= Application.streamingAssetsPath +"/"+videoName+".mp4";
        myVideoPlayer.url = videoUrl;
        myVideoPlayer.Prepare();
    }

    void Update()
    {
        if (waiting && myVideoPlayer.isPrepared)
        {
            waiting = false;
            myVideoPlayer.Play();
        }
    }
}