using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    public AudioClip attackSE;
    public AudioClip defenseSE;
    public AudioClip counterSE;
    public AudioClip reloadSE;
    public AudioClip missSE;

    [SerializeField] private Light2D light1;
    [SerializeField] private Light2D light2;
    public void Init()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        light1.enabled = false;
        light2.enabled = false;
    }

    public void OnAttack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("[PlayerController] Attack!");

        light1.enabled = true;
        light2.enabled = true;

        Invoke("TurnOffLight", 0.1f);
    }
    private void TurnOffLight()
    {
        light1.enabled = false;
        light2.enabled = false;
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
