using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{

    [Header("Video Settings: ")]
    public RawImage videoImage;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource; 
    public LevelLoader Fade;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        var waitforseconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitforseconds;
            break;
        }
        videoImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioSource.Play();
        while (videoPlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        Fade.LoadNextLevel();
    }
}
