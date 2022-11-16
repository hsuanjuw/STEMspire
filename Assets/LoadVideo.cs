using UnityEngine;
using UnityEngine.Video;

public class LoadVideo : MonoBehaviour
{
    public VideoPlayer myVideoPlayer;
    public string videoName;
    void Start()
    {
        string videoUrl= Application.streamingAssetsPath +"/"+videoName+".mp4";
        myVideoPlayer.url = videoUrl;
    }
}