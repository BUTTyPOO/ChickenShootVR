using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArenaActor : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] GameObject pointsPopUp;
    [SerializeField] int pointsForHit;
    [SerializeField] GameObject particles;  // used for when chicken gets hit


    public float moveTime = 5.0f;  // Time it takes to move from start to end
    bool ragdolled = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SpawnAnimation();
        StartMovingTorwardEnd();
    }

    void SpawnAnimation()
    {
        Vector3 size = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(size, 0.2f).OnComplete(StartIdleShakes).SetEase(Ease.OutSine);
    }

    void DeSpawnAnimation()
    {
        transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutSine);
    }

    void StartMovingTorwardEnd()
    {
        StartCoroutine(moveTween(start.position, end.position));
    }

    void StartIdleShakes()
    {
        transform.DOShakeRotation(10.0f, 35, 5);
        transform.DOShakeScale(10.0f, 0.2f, 1);
    }

    public void Init(Transform _start, Transform _end)
    {
        start = _start;
        end = _end;
    }


    IEnumerator moveTween(Vector3 startPos, Vector3 endPos) // Tweens linearly from start to end
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < moveTime)
        {
            if (ragdolled)
                break;
            transform.position = Vector3.Lerp(startPos, endPos, EaseInOutSin(elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;  // skip a frame
        }
        if (!ragdolled)
            OnDestinationReached();
    }

    float EaseInOutSin(float t)
    {
        return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
    }

    void OnDestinationReached()
    {
        transform.DOKill();
        DeSpawnAnimation();
        Destroy(gameObject, 1.0f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "projectile")
        {
            if (ragdolled)
                return;

            SpawnParticles();
            PointsDisplay.instance.AddPoints(pointsForHit);
            SpawnPointsPopUp(pointsForHit);
            EnableRagdoll();
        }
    }

    void SpawnParticles()
    {
        print("HERE");
        GameObject particleObj = Instantiate(particles, transform.position, Quaternion.identity);
        //Destroy(particleObj, 5.0f);
    }

    void SpawnPointsPopUp(int pts)
    {
        GameObject obj = Instantiate(pointsPopUp, transform.position, Quaternion.identity);
        obj.GetComponent<PointsPopUp>().ChangePtsTxt(pts);
    }

    void EnableRagdoll()
    {
        ragdolled = true;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
