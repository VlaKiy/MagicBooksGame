using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyButton : MonoBehaviour
{
    public GameObject btn;
    public GameObject useBtn;
    public Tilemap tilemap;
    private GameObject obj;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.gameObject.tag == "DestroingObj")
        {
            btn.SetActive(true);
            obj = coll.collider.gameObject;
        }
        else if (coll.collider.gameObject.tag == "UsedObj")
        {
            useBtn.SetActive(true);
            obj = coll.collider.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.collider.gameObject.tag == "DestroingObj")
        {
            btn.SetActive(false);
        }
        else if (coll.collider.gameObject.tag == "UsedObj")
        {
            useBtn.SetActive(false);
        }
    }

    public void DestroyObject()
    {
        Destroy(obj);
        btn.SetActive(false);
    }

    public void UseObject()
    {
        var changeObj = obj.gameObject.GetComponent<UsedObjInfo>().changeObj;
        var oldObjPos = obj.gameObject.transform.position;

        Destroy(obj.gameObject);
        var newObj = Instantiate(changeObj);
        newObj.transform.position = oldObjPos;
    }
}
