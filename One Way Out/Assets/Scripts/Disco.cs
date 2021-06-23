using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco : MonoBehaviour
{
    public Transform discoBall;
    public List<GameObject> discoPoints = new List<GameObject>();
    public List<GameObject> statues = new List<GameObject>();
    float scaleRate = 0.0001f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        discoBall.Rotate(new Vector3(discoBall.rotation.x, discoBall.rotation.y + 10, discoBall.rotation.z));

        for (int i = 0; i < discoPoints.Count; i++)
        {
            ScrollTexture(discoPoints[i]);
        }

        for (int i = 0; i < statues.Count; i++)
        {
            scaleStatues(statues[i].transform);
        }
        
    }

  
        
    

    public void scaleStatues(Transform gameObject)
    {
       
        //if we exceed the defined range then correct the sign of scaleRate.
        if (gameObject.localScale.y < 0.001759633)
        {
            scaleRate = Mathf.Abs(scaleRate);
        }
        else if (gameObject.localScale.y > 0.003759633)
        {
            scaleRate = -Mathf.Abs(scaleRate);
        }
        gameObject.localScale = new Vector3(gameObject.localScale.x, gameObject.localScale.y + 1 * scaleRate, gameObject.localScale.z);
    }

    void ScrollTexture(GameObject discoPoint)
    {
        Vector2[] uvList = discoPoint.GetComponent<MeshFilter>().mesh.uv;

        for (int i = 0; i < uvList.Length; i++)
     {
            uvList[i] += new Vector2(Time.deltaTime, 0);
        }

        discoPoint.GetComponent<MeshFilter>().mesh.uv = uvList;
    }
}
