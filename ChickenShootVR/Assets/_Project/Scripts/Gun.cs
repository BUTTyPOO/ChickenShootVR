using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class Gun : MonoBehaviour
    {
        Rigidbody rb;
        Hand hand;
        [SerializeField] Transform barrelLoc;
        [SerializeField] GameObject bulletPrefab;

        float bulletSpeed = 1000.0f;
        const float shootDelay = 0.3f;
        float lastTimeFired = 0.0f;
        float recoilScaler = 100.0f;

        //-------------------------------------------------
        void OnAttachedToHand(Hand attachedHand)
        {
            hand = attachedHand;
            //ControllerButtonHints.ShowButtonHint(hand, SteamVR_Actions._default.InteractUI[hand.handType]);
        }

        //-------------------------------------------------
        void HandAttachedUpdate(Hand hand)
        {
            if (SteamVR_Actions._default.InteractUI[hand.handType].state)   // Fire Gun
            {
                if (Time.time - lastTimeFired > shootDelay)
                {
                    lastTimeFired = Time.time;
                    Shoot();
                }
            }
        }

        void Shoot()
        {
            GameObject spawnedBullet = Instantiate(bulletPrefab, barrelLoc.position, Quaternion.identity);
            Rigidbody bulletRb = spawnedBullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(barrelLoc.forward * bulletSpeed); // blast forward
            bulletRb.GetComponent<Renderer>().material.SetColor("_Color", GetRandColor());  // add rand Color
            //Destroy(spawnedBullet, 10);
            RecoilGun();
        }

        Color GetRandColor()
        {
            Color col = new Color();
            col.r = Random.value;
            col.b = Random.value;
            col.g = Random.value;
            return col;
        }

        void RecoilGun()
        {
            if (!rb)
                rb = GetComponent<Rigidbody>();

            rb.AddForce(-transform.forward * recoilScaler);
            rb.AddForce(transform.up * recoilScaler);
        }

        //-------------------------------------------------
        void OnHandFocusLost(Hand hand)
        {
        }

        //-------------------------------------------------
        void OnHandFocusAcquired(Hand hand)
        {
            OnAttachedToHand(hand);
        }

        //-------------------------------------------------
        void OnDetachedFromHand(Hand hand)
        {
            Destroy(gameObject);
        }

        //-------------------------------------------------
        void OnDestroy()
        {
        }
    }
}

