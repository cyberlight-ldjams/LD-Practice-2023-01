using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Cannonball : MonoBehaviour
{
    public float BlastRadius = 3f;

    public float Damage = 5f;

    public bool DamageFallOff = true;

    public float MinimumDamagePercent = 0.1f;

    public float Speed = 10f;

    private Vector3 StartingPoint;

    private Vector3 EndingPoint;

    private float TimeToReach;

    private float TimeElapsed;

    // Update is called once per frame
    void Update()
    {

        TimeElapsed += Time.deltaTime;

        float progress = TimeElapsed / TimeToReach;

        if (progress > 1f)
        {
            progress = 1f;
        }

        gameObject.transform.position = Vector3.Lerp(StartingPoint, EndingPoint, progress);

        Debug.Log(gameObject.transform.position + " " + progress);

        if (progress >= 1f)
        {
            ExplodeAndDamage();

            GameObject.Destroy(this.gameObject);
        }
    }

    public void FireAt(Vector3 startingFrom, Vector3 goingTo)
    {
        StartingPoint = startingFrom;
        EndingPoint = goingTo;
        TimeToReach = FlightTime(startingFrom, goingTo, Speed);
        TimeElapsed = 0;
    }

    private void ExplodeAndDamage()
    {
        Collider [] colliders = Physics.OverlapSphere(EndingPoint, BlastRadius);

        foreach (Collider c in colliders)
        {
            if (c.gameObject.tag == "Enemy")
            {
                Enemy hitEnemy = c.gameObject.GetComponent<Enemy>();
                float damageDone = Damage;

                if (DamageFallOff)
                {
                    float distanceToCenter = Vector3.Distance(hitEnemy.gameObject.transform.position, EndingPoint);
                    float percentDamage = (BlastRadius - distanceToCenter) / BlastRadius;
                    bool dealDamage = true;

                    // If they're on the edge of taking damage
                    if (percentDamage < 0.05f)
                    {
                        // There's a 50% chance they'll take no damage
                        if (Random.Range(0f, 1f) < 0.5f)
                        {
                            dealDamage = false;
                        }
                    }

                    // Always do at least minimum % damage
                    if (percentDamage < MinimumDamagePercent)
                    {
                        percentDamage = MinimumDamagePercent;
                    }

                    if (dealDamage)
                    {
                        damageDone = damageDone * percentDamage;
                    }
                }

                hitEnemy.SubjectiveMortality.TakeDamage(damageDone);
            }
        }
    }

    private float FlightTime(Vector3 start, Vector3 end, float speed)
    {
        return (Vector3.Distance(start, end) / speed);
    }
}
