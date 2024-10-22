using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FaucetController : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private bool isOpen = true;

    public TextMeshProUGUI percent;
    public GameObject bubble;
    public ParticleSystem confetti;
    public GameObject congratulations;

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
        }
        else
        {
            particleSystem.Stop();
        }
    }

    public void Control()
    {
        isOpen = !isOpen;
        Faucet();
    }

    public void ChangeState()
    {
        StateController.Instance.SetState(State.PrepararUniforme);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("LeftHand") || other.gameObject.tag.Equals("RightHand"))
        {
            if(bubble.GetComponent<CanvasGroup>().alpha == 1)
            {
                if (!confetti.isPlaying)
                {
                    congratulations.SetActive(true);
                    confetti.Play();
                    AudioManager.instance.Play("correct_sound");
                    Invoke("ChangeState", 6f);
                    GameObject[] systemsBubble = GameObject.FindGameObjectsWithTag("BubbleSoap");
                    foreach (var system in systemsBubble)
                    {
                        FindObjectOfType<HandController>().BubbleSoapStop(system.GetComponent<ParticleSystem>());
                    }
                }
            }
            else
            {
                bubble.GetComponent<CanvasGroup>().alpha += Time.deltaTime / 12;
                percent.text = System.Math.Round(bubble.GetComponent<CanvasGroup>().alpha * 100, 1) + "%";
            }
            Debug.Log("lavandoo");
        }
    }
}
