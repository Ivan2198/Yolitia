using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Animator animator;

   public void CameraZoomAnimation()
    {
        animator.CrossFade("CameraZoom", 0f);
    }
}
