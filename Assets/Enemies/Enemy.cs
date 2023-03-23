using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int Value = 1;

    public float Range = 3f;

    public float Damage = 1f;

    // Attacks per second
    public float AttackSpeed = 0.67f;

    public Mortality SubjectiveMortality;

    private float TimeToAttack = 0f;

    public GameObject PlayerBase;

    private GameObject currentObjective;

    private Mortality objectiveMortality;

    public float distanceForCaring = 20;

    private NavMeshAgent nav;

    private readonly float waitForFirstObjective = 2;

    private float waitTimer = 0;

    private bool waitingForFirstObjective = true;

    private Dictionary<string, float> levelOfInterest;

    private Transform t;

    void Awake()
    {
        levelOfInterest = new Dictionary<string, float>
        {
            { "Base", 100 },
            { "Tower", 50 },
            { "Mine", 20 }
        };

        nav = GetComponent<NavMeshAgent>();

        t = this.gameObject.transform;
        nav.destination = t.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerBase == null)
        {
            PlayerBase = GameObject.FindGameObjectWithTag("Base");
        }

        if (SubjectiveMortality == null)
        {
            SubjectiveMortality = GetComponent<Mortality>();
        }

        currentObjective = PlayerBase;
        objectiveMortality = currentObjective.GetComponent<Mortality>();

        SubjectiveMortality.RegisterOnDeath(OnDeath);

        nav.destination = currentObjective.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Wait 2 seconds after spawning to pick your first objective
        // In the meantime, the base is your objective
        if (waitingForFirstObjective)
        {
            waitTimer += Time.deltaTime;

            if (waitForFirstObjective < waitTimer)
            {
                GetNewObjective();
                waitingForFirstObjective = false;
                waitTimer = 0;
            }
        }

        TimeToAttack += Time.deltaTime;

        // See if we can attack, and if so, ATTACK!!!
        if (Vector3.Distance(nav.destination, t.position) < Range)
        {
            nav.isStopped = true;

            if ((1f / AttackSpeed) < TimeToAttack)
            {
                float damage = Damage * (Random.value * 2);

                objectiveMortality.TakeDamage(damage);

                TimeToAttack = 0;
            }
        } else
        {
            nav.isStopped = false;
        }
    }

    private void GetNewObjective()
    {
        if (currentObjective != null)
        {
            objectiveMortality.UnregisterOnDeath(GetNewObjective);
        }

        List<GameObject> ooi = getObjectsOfInterest(distanceForCaring);

        float highestVal = 0;

        foreach (GameObject obj in ooi)
        {
            float dist = Vector3.Distance(obj.transform.position, t.position);
            float interest = (levelOfInterest[obj.tag] / dist);

            if (interest > highestVal)
            {
                highestVal = interest;
                currentObjective = obj;
            }
        }

        if (currentObjective != null)
        {
            objectiveMortality = currentObjective.GetComponent<Mortality>();
            objectiveMortality.RegisterOnDeath(GetNewObjective);
        } else
        {
            // Failsafe, if we have no objective for some reason, it's the base for the next 2 seconds
            waitingForFirstObjective = true;
            currentObjective = PlayerBase;
        }

        nav.destination = currentObjective.transform.position;
    }

    private List<GameObject> getObjectsOfInterest(float distance)
    {
        List<GameObject> ooi = new List<GameObject>();

        // Get the base
        ooi.Add(PlayerBase);

        // Get all the towers
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        foreach (GameObject tower in towers)
        {
            if (Vector3.Distance(tower.transform.position, t.position) <= distance)
            {
                ooi.Add(tower);
            }
        }

        // Get all the mines/lumberyards
        GameObject[] mines = GameObject.FindGameObjectsWithTag("Mine");

        foreach (GameObject mine in mines)
        {
            if (Vector3.Distance(mine.transform.position, t.position) <= distance)
            {
                ooi.Add(mine);
            }
        }

        return ooi;
    }

    void OnDeath()
    {
        objectiveMortality.UnregisterOnDeath(GetNewObjective);
        gameObject.SetActive(false);
    }
}
