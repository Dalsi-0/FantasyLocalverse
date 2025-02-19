using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static DialogueDataSO;

public class ViewPoint : InteractableBase
{
    [SerializeField] private PlayableDirector playableDirector;

    private List<DialogueLine> dialogueData;

    private void Start()
    {
        SetVirtualCameraActive(false);
        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineEnd;  
        }
        else { Debug.LogWarning("playableDirector is null!!"); }

        dialogueData = DialogueManager.Instance.repository.GetDialogue(EDialogueKey.Object_viewPoint);

        onInteract = () => DialogueManager.Instance.StartDialogue(dialogueData, virtualCamera, PlayViewPointTimeline);
    }
        
    private void PlayViewPointTimeline()
    {
        GameManager.Instance.PlayerController.SetMoveLock(true);

        SetVirtualCameraActive(true);
        playableDirector.Play();
    }


    private void OnTimelineEnd(PlayableDirector director)
    {
        playableDirector.Stop();
        SetVirtualCameraActive(false);
        GameManager.Instance.PlayerController.SetMoveLock(false);
    }
}
