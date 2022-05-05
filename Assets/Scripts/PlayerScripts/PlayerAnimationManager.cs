using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{

    [SerializeField] Animator animator;

    public void StartWalking() 
    {
        animator.SetBool("Walk", true);
    }
    public void StopWalking()
    {
        animator.SetBool("Walk", false);
    }
}
