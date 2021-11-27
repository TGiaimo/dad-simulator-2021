using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class wrenchInteraction : MonoBehaviour
{
    public Transform newNutHome;
    public GameObject wrench;
    public GameObject lug;
    public GameObject rightHand;
    public GameObject leftHand;
    public Transform endPos;
    public Transform endPosNut;
    public Transform startPos;
    public float sped;
    public float desiredDuration;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == wrench.name)
        {
            startPos = other.gameObject.transform;
            elapsedTime = 0;
            wrench.GetComponent<CircularDrive>().outAngle= 0;
            wrench.GetComponent<interactions>().trigger = this.gameObject;
            lug = this.gameObject.transform.parent.gameObject;
            wrench.GetComponent<interactions>().lug = lug;
            if (rightHand.GetComponent<Hand>().ObjectIsAttached(wrench))
            {
                rightHand.GetComponent<Hand>().DetachObject(wrench);
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            if (leftHand.GetComponent<Hand>().ObjectIsAttached(wrench))
            {
                leftHand.GetComponent<Hand>().DetachObject(wrench);
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            Destroy(other.gameObject.GetComponent<Throwable>());
            StartCoroutine(MoveWrench());
            other.gameObject.GetComponent<CircularDrive>().enabled=true;
        }
        

        if(other.gameObject.name=="hub_nut_lo_1" || other.gameObject.name=="hub_nut_lo_2" || other.gameObject.name=="hub_nut_lo_3" || other.gameObject.name=="hub_nut_lo_4" || other.gameObject.name == "hub_nut_lo_5")
        {
            GameObject temp;
            temp = other.gameObject; 
            lug = temp;
            startPos = temp.transform;
            wrench.GetComponent<interactions>().lug = lug;
            if (rightHand.GetComponent<Hand>().ObjectIsAttached(temp))
            {
                rightHand.GetComponent<Hand>().DetachObject(temp);
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            if (leftHand.GetComponent<Hand>().ObjectIsAttached(temp))
            {
                leftHand.GetComponent<Hand>().DetachObject(temp);
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }

            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<Interactable>().enabled = false;
            other.gameObject.GetComponent<MeshCollider>().enabled = false;
            other.gameObject.transform.GetChild(0).gameObject.GetComponent<HubCheck>().loosened = false;
            other.gameObject.transform.SetParent(newNutHome);
            StartCoroutine(lerpNut());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == wrench.name)
        {
            //other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    
    IEnumerator MoveWrench()
    {
        while (elapsedTime < desiredDuration)
        {
            wrench.transform.position=Vector3.Lerp(startPos.position, endPos.position, (elapsedTime/desiredDuration));
            //wrench.transform.rotation=Quaternion.Lerp(startPos.rotation, endPos.rotation, (elapsedTime/desiredDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("schmoovin");
        yield return null;
    }

    IEnumerator lerpNut()
    {
        while (elapsedTime < desiredDuration)
        {
            lug.transform.position=Vector3.Lerp(startPos.position, endPosNut.position, (elapsedTime/desiredDuration));
            lug.transform.rotation=Quaternion.Lerp(startPos.rotation, endPosNut.rotation, (elapsedTime/desiredDuration));
            //wrench.transform.rotation=Quaternion.Lerp(startPos.rotation, endPos.rotation, (elapsedTime/desiredDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("schmoovin");
        yield return null;
    }


}
