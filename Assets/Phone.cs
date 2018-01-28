using UnityEngine;

internal class Phone : MonoBehaviour
{
    public void Start()
    {

        MessageSpawner.s_instance.SpawnMessage("A new server has booted up, try connecting two users");
    }

    public Receiver recieverA;
    public Receiver recieverB;
}