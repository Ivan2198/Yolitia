using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string JUMP_TRIGGER = "Jump"; // New trigger for jumping

    [SerializeField] private PlayerController player;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

    public void Walk()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

    public void Jump()
    {
        animator.SetTrigger(JUMP_TRIGGER); // Use a trigger for jumping
    }

    public void Idle()
    {
        animator.CrossFade("Idle", 0f);
    }
}