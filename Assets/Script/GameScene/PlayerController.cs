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

    [SerializeField] private Light2D muzzleLight1;
    [SerializeField] private Light2D muzzleLight2;
    [SerializeField] private Light2D shieldLight;
    [SerializeField] private Light2D counterLight;

    [SerializeField] private Transform eyeGlow;
    [SerializeField] private Vector3 eyeGlowAttackPosition;
    [SerializeField] private Vector3 eyeGlowDefensePosition;
    [SerializeField] private Vector3 eyeGlowCounterPosition;
    private Vector3 eyeGlowDefaultPosition;
    public void Init()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        muzzleLight1.enabled = false;
        muzzleLight2.enabled = false;
        shieldLight.enabled = false;
        counterLight.enabled = false;
        eyeGlowDefaultPosition = eyeGlow.localPosition;
    }

    public void OnAttack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("[PlayerController] Attack!");

        muzzleLight1.enabled = true;
        muzzleLight2.enabled = true;

        Invoke("TurnOffMuzzleLight", 0.1f);

        eyeGlow.localPosition = eyeGlowAttackPosition;
        Invoke("ResetEyeGlow", 0.2f);
    }

    public void OnDefense()
    {
        animator.SetTrigger("Defense");
        Debug.Log("[PlayerController] Defense!");

        shieldLight.enabled = true;
        Invoke("TurnOffShieldLight", 0.1f);

        eyeGlow.localPosition = eyeGlowDefensePosition;
        Invoke("ResetEyeGlow", 0.2f);
    }
    public void OnCounter()
    {
        animator.SetTrigger("Counter");
        Debug.Log("[PlayerController] Counter!");

        counterLight.enabled = true;
        Invoke("TurnOffCounterLight", 0.1f);

        eyeGlow.localPosition = eyeGlowCounterPosition;
        Invoke("ResetEyeGlow", 0.2f);
    }
    private void TurnOffMuzzleLight()
    {
        muzzleLight1.enabled = false;
        muzzleLight2.enabled = false;
    }
    private void TurnOffShieldLight()
    {
        shieldLight.enabled = false;
    }
    private void TurnOffCounterLight()
    {
        counterLight.enabled = false;
    }
    private void ResetEyeGlow()
    {
        eyeGlow.localPosition = eyeGlowDefaultPosition;
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
