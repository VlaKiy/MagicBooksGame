using UnityEngine;

public class PlayerChangeEnemy : MonoBehaviour
{
    public GameObject circleChoice;
    private GameObject circle;
    [HideInInspector] public GameObject enm;

    void Update()
    {
        Change(enm);
    }

    void Change(GameObject obj)
    {
        
        if (obj != null)
        {
            if (GameObject.FindGameObjectsWithTag("CircleChoice").Length < 1)
            {
                var newCircle = Instantiate(circleChoice);
                newCircle.name = "CircleChoice";
                circle = newCircle;
            }

            circle.GetComponent<FollowGameobject>().lookAt = obj.transform;
        }
    }
}
