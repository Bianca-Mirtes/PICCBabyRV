using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FaucetController : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private bool isOpen = true;

    public TextMeshProUGUI percent;
    public GameObject support;
    public GameObject bubble;
    public ParticleSystem confetti;
    public GameObject congratulations;
    public GameObject seta;

    private bool isFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Faucet()
    {
        if (!isOpen)
        {
            particleSystem.Play();
            particleSystem.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            particleSystem.Stop();
            particleSystem.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void Control()
    {
        isOpen = !isOpen;
        Faucet();
    }

    private void ChangeState()
    {
        StateController.Instance.SetState(State.PrepararUniforme);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("LeftHand") || other.gameObject.tag.Equals("RightHand"))
        {
            if(bubble.GetComponent<CanvasGroup>().alpha == 1)
            {
                if (!isFinished)
                {
                    isFinished = true;
                    congratulations.SetActive(true);
                    seta.SetActive(false);
                    GameObject[] systemsBubble = GameObject.FindGameObjectsWithTag("BubbleSoap");
                    Control();
                    support.GetComponent<AnimationsController>().Control();
                    AudioManager.instance.Play("correct_sound");
                    GetComponent<AudioSource>().Stop();
                    confetti.Play();
                    foreach (var system in systemsBubble)
                    {
                        FindObjectOfType<HandController>().BubbleSoapStop(system.GetComponent<ParticleSystem>());
                    }
                    Invoke("ChangeState", 5f);
                }
            }
            else
            {
                bubble.GetComponent<CanvasGroup>().alpha += Time.deltaTime / 12;
                percent.text = System.Math.Round(bubble.GetComponent<CanvasGroup>().alpha * 100, 1) + "%";
            }
        }
    }
}
