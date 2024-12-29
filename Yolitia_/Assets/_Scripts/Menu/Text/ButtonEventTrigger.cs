using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonEventTrigger : MonoBehaviour
{
    public TremblingText targetText;

    private void Start()
    {
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { targetText.StartTrembling(); });
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { targetText.StopTrembling(); });
        trigger.triggers.Add(entryExit);

        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => { targetText.StopTrembling(); });
        trigger.triggers.Add(entryDown);
    }
}
