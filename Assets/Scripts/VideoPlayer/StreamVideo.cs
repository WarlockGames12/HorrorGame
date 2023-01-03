using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public bool Ending1 = false;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        if (!Ending1)
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
        else
        {
            videoPlayer.Prepare();
            var WaitForSeconds = new WaitForSeconds(1);
            while (!videoPlayer.isPrepared)
            {
                yield return WaitForSeconds;
                break;
            }
            videoImage.texture = videoPlayer.texture;
            videoPlayer.Play();
            audioSource.Play();
            while (videoPlayer.isPlaying)
            {
                yield return new WaitForEndOfFrame();
            }
            SceneManager.LoadScene("MainMenu");
        }
    }
}
