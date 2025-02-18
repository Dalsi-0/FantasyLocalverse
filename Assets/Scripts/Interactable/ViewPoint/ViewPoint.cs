using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ViewPoint : InteractableBase
{
    private PlayableDirector playableDirector;
 
    private void Start()
    {
        playableDirector = transform.GetChild(0).GetComponent<PlayableDirector>();

        if (playableDirector != null)
        {
            playableDirector.gameObject.SetActive(false);
            playableDirector.stopped += OnTimelineEnd;
        }

        onInteract = () => PlayViewPointTimeline();
    }

    private void PlayViewPointTimeline()
    {
        GameManager.Instance.player.transform.GetComponent<PlayerController>().SetMoveLock(true);
        playableDirector.gameObject.SetActive(true);
        playableDirector.Play();
    }


    private void OnTimelineEnd(PlayableDirector director)
    {
        Debug.Log("타임라인 종료");

        playableDirector.Stop();
        playableDirector.gameObject.SetActive(false);
        GameManager.Instance.player.transform.GetComponent<PlayerController>().SetMoveLock(false);
    }
}
