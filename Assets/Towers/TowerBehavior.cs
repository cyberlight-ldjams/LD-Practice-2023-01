using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TowerBehavior : MonoBehaviour
{
    public float Range = 10f;

    public Cannonball CB;

    public Vector3 MuzzleLocation;

    public float TargetingDelay = 1f;

    public Mortality SubjectiveMortality;

    public Enemy target;

    private float Delay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (CB == null)
        {
            CB = GetComponent<Cannonball>();
        }

        if (SubjectiveMortality == null)
        {
            SubjectiveMortality = GetComponent<Mortality>();
        }

        SubjectiveMortality.RegisterOnDeath(OnDeath);
    }

    // Update is called once per frame
    void Update()
    {
        Delay += Time.deltaTime;

        // If we don't have a target, try to acquire one after a second
        if (target == null)
        {
            if (Delay > TargetingDelay)
            {
                MuzzleLocation = this.gameObject.transform.position;
                target = TargetEnemy();

                // If we still don't have a target, reset the delay
                if (target == null)
                {
                    Delay = 0;
                } else
                {
                    target.GetComponent<Mortality>().RegisterOnDeath(ChangeTarget);
                }
            }
        } 
        // If the target is now out of range, we don't have one anymore
        else if (!InRange(target))
        {
            target = null;
        }
        // Fire at the target after a delay
        else if (Delay > TargetingDelay)
        {
            FireAt(target);
            Delay = 0;
        }
    }

    private void ChangeTarget()
    {
        if (target != null) {
            target.GetComponent<Mortality>().UnregisterOnDeath(ChangeTarget);
            target = null;
        }
    }

    private void FireAt(Vector3 location)
    {
        Cannonball cb = Instantiate(CB);
        cb.transform.position = MuzzleLocation;
        cb.FireAt(MuzzleLocation, location);
    }

    private void FireAt(Enemy e)
    {
        FireAt(e.gameObject.transform.position);
    }
    

    private Enemy TargetEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(MuzzleLocation, Range);

        foreach (Collider c in colliders)
        {
            if (c.gameObject.tag.Equals("Enemy"))
            {
                return c.gameObject.GetComponent<Enemy>();
            }
        }

        return null;
    }

    private bool InRange(GameObject go)
    {
        if (Vector3.Distance(transform.position, go.transform.position) <= Range)
        {
            return true;
        }

        return false;
    }

    private bool InRange(Enemy e)
    {
        return InRange(e.gameObject);
    }

    private void OnDeath()
    {
        ChangeTarget();
        gameObject.SetActive(false);
    }
}
