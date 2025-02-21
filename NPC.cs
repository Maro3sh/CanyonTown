using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float moveSpeed = 2f;
    public bool isOutlaw = false;
    
    [SerializeField] private GameObject specialObject;
    private int currentWaypointIndex = 0;
    private SpriteRenderer spriteRenderer;
    private float defaultSpeed;
    private PlayerStats playerStats;
    private bool isDead = false;
    private SheriffJob sheriffJobScript;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSpeed = moveSpeed;
        playerStats = FindObjectOfType<PlayerStats>();
        sheriffJobScript = FindObjectOfType<SheriffJob>();
    }

    void Update()
    {
        if (waypoints.Length == 0 || isDead) return;

        MoveAlongWaypoints();

        

        if (Vector3.Distance(transform.position, playerStats.transform.position) < 1f && isOutlaw && !isDead)
        {
            playerStats.ShowDeathScreen();
        }
    }

    private void MoveAlongWaypoints()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        spriteRenderer.flipX = direction.x < 0;

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
                currentWaypointIndex = 0;
        }
    }

    public void SetOutlawStatus(bool status)
    {
        isOutlaw = status;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isOutlaw && !isDead)
        {
            playerStats.ShowDeathScreen();
            isDead = true;
            gameObject.SetActive(false);
            FindObjectOfType<MoneyManager>().AddMoney(500);
            Invoke("RespawnNPC", 120f);
        }
    }

    private void RespawnNPC()
    {
        isDead = false;
        gameObject.SetActive(true);
    }

    public bool IsOutlaw()
    {
        return isOutlaw;
    }

    public void ResetMovement()
    {
        currentWaypointIndex = 0;
        isDead = false;
    }
}
