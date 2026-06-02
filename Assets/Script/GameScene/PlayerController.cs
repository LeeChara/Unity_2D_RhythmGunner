using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    public AudioClip attackSE;
    public AudioClip defenseSE;
    public AudioClip counterSE;
    public AudioClip reloadSE;
    public AudioClip missSE;
    public void Init()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnAttack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("[PlayerController] Attack!");
    }
    public void OnDefense()
    {
        animator.SetTrigger("Defense");
        Debug.Log("[PlayerController] Defense!");
    }
    public void OnCounter()
    {
        animator.SetTrigger("Counter");
        Debug.Log("[PlayerController] Counter!");
    }

    public void PlaySE(string soundString)
    {
        switch (soundString)
        {
            case "Attack":
                audioSource.PlayOneShot(attackSE);
                break;
            case "Defense":
                audioSource.PlayOneShot(defenseSE);
                break;
            case "Counter":
                audioSource.PlayOneShot(counterSE);
                break;
            case "Reload":
                audioSource.PlayOneShot(reloadSE);
                break;
            case "Miss":
                audioSource.PlayOneShot(missSE);
                break;
        }
    }
}
