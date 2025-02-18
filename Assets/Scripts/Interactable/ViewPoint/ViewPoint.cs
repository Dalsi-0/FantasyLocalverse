using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ViewPoint : InteractableBase
{
    [SerializeField] private PlayableDirector playableDirector;

    private DialogueData[] dialogueData;

    private void Start()
    {
        SetVirtualCameraActive(false);
        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineEnd;  
        }
        else { Debug.LogWarning("playableDirector is null!!"); }

        dialogueData = DialogueManager.Instance.repository.GetDialogue("Object_viewPoint");

        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera, PlayViewPointTimeline);
    }
        
    private void PlayViewPointTimeline()
    {
        GameManager.Instance.player.transform.GetComponent<PlayerController>().SetMoveLock(true);

        SetVirtualCameraActive(true);
        playableDirector.Play();
    }


    private void OnTimelineEnd(PlayableDirector director)
    {
        Debug.Log("타임라인 종료");

        playableDirector.Stop();
        SetVirtualCameraActive(false);
        GameManager.Instance.player.transform.GetComponent<PlayerController>().SetMoveLock(false);
    }
}
