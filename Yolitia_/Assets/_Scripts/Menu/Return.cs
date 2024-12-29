using UnityEngine;
using UnityEngine.Events;

public class Return : MonoBehaviour
{
    [SerializeField] private UnityEvent OnScape;
    [SerializeField] private AudioClip _returnClip;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnScape?.Invoke();
            SoundManager.Instance.PlaySound(_returnClip, 0.8f);
        }
    }
    public void ReturnFunc()
    {
        OnScape?.Invoke();
    }
}
