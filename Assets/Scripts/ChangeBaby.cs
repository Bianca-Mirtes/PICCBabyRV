using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBaby : MonoBehaviour
{
    public Transform player;
    private float spawnDistance = 1;
    private GameObject myGameObject;
    private int currentBaby;

    [SerializeField]
    private List<GameObject> teleportsBaby;

    // Start is called before the first frame update
    void Start()
    {
        GameObject myGameObject = this.gameObject;
    }

    public void DisplayCanvas(Component button)
    {
        this.gameObject.SetActive(true);
        this.transform.position = button.transform.position -  new Vector3(-0.9f, -0.2f, -1.2f);
        this.transform.LookAt(new Vector3(player.position.x,this.transform.position.y, player.position.z));
        this.transform.forward *= -1;

        string nome = button.name;
        currentBaby = int.Parse(nome.Substring(13));
    }

    public void GoToBaby(int toChange)
    {
        Debug.Log(currentBaby);
        currentBaby += toChange;
        if (currentBaby < teleportsBaby.Count)
        {
            currentBaby = 5;
        }
        else if (currentBaby > teleportsBaby.Count)
        {
            currentBaby = 1;
        }
        var currentTeleport = teleportsBaby[currentBaby - 1];
        Debug.Log(currentTeleport);

        player.transform.position = currentTeleport.transform.position;
    }
}
