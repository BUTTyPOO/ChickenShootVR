using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PointsPopUp : MonoBehaviour
{
    GameObject txtObj;
    TMP_Text txt;
    

    void Update()
    {
        transform.LookAt(Valve.VR.InteractionSystem.Player.instance.hmdTransform);
    }

    public void ChangePtsTxt(int ptsEarned)
    {
        if(ptsEarned < 0)
        {
            txt.color = Color.red;
            txt.text = ptsEarned.ToString();
        }
        else
        {
            txt.color = Color.green;
            txt.text =  "+" + ptsEarned.ToString();
        }
    }

    void OnEnable()
    {
        txtObj = transform.GetChild(0).gameObject;
        txt = txtObj.GetComponent<TextMeshProUGUI>();
        txtObj.transform.localScale = Vector3.zero;
        txtObj.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBounce);
        txtObj.transform.DOShakePosition(1.0f).OnComplete(AnimateClosing);
    }

    void AnimateClosing()   //Animates text closing
    {
        txtObj.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InSine);
        Destroy(gameObject, 5.0f);
    }
}
