using UnityEngine;

public class BGAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void DeathAnim()
    {
        animator.CrossFade("Death", 0f);
    }
}
