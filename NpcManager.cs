using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private NPC[] npcs;
    [SerializeField] private Transform outlawSpawnLocation;
    [SerializeField] private SheriffJob sheriffJobScript;
    [SerializeField] private GameObject outlaw;
    private float spawnTimer = 15f;
    private float timer;
    private bool isTimerRunning = false;

    void Start()
    {
        if (sheriffJobScript.IsSheriff())
        {
            StartTimer();
            if (outlaw != null)
                outlaw.SetActive(true);
        }
        else
        {
            StopTimer();
            if (outlaw != null)
                outlaw.SetActive(false);
        }
    }

    void Update()
    {
        if (sheriffJobScript.IsSheriff())
        {
            if (!isTimerRunning)
            {
                StartTimer();
                if (outlaw != null)
                    outlaw.SetActive(true);
            }
            else
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    ActivateOutlawNPC();
                    timer = spawnTimer;
                }
            }
        }
        else
        {
            StopTimer();
            if (outlaw != null && outlaw.activeSelf)
                outlaw.SetActive(false);
        }
    }

    public void SetPlayerSheriffStatus(bool status)
    {
        if (outlaw != null)
            outlaw.SetActive(status);

        if (status)
            StartTimer();
        else
            StopTimer();
    }

    private void StartTimer()
    {
        timer = spawnTimer;
        isTimerRunning = true;
    }

    private void StopTimer()
    {
        isTimerRunning = false;
    }

    private void ActivateOutlawNPC()
    {
        foreach (NPC npc in npcs)
        {
            if (npc.isOutlaw && !npc.gameObject.activeSelf)
            {
                npc.gameObject.SetActive(true);
                npc.transform.position = outlawSpawnLocation.position;
                npc.ResetMovement();
                break;
            }
        }
    }

    // Call this method when the outlaw is shot.
    public void OutlawShot()
    {
        // Disable any active outlaw NPC
        foreach (NPC npc in npcs)
        {
            if (npc.isOutlaw && npc.gameObject.activeSelf)
                npc.gameObject.SetActive(false);
        }
        // Also disable the standalone outlaw GameObject if needed
        if (outlaw != null)
            outlaw.SetActive(false);
        // Stop the timer and restart it after 15 seconds so the outlaw can reappear.
        StopTimer();
        Invoke("StartTimer", 15f);
    }

    public void RestartNPCs()
    {
        foreach (NPC npc in npcs)
        {
            npc.gameObject.SetActive(true);
            npc.SetOutlawStatus(true);
        }
    }

    public void StopOutlaws()
    {
        foreach (NPC npc in npcs)
        {
            if (npc != null)
                npc.SetOutlawStatus(false);
        }
    }
}
