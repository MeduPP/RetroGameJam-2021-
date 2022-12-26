using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   
    public Field field;

    private void Start()
    {
        var listOfStones = GameObject.FindGameObjectsWithTag("Stone").ToArray();

        field = new Field()
        {
            MinX = listOfStones.Min(x => x.transform.position.x) - 0.5f,
            MinY = listOfStones.Min(x => x.transform.position.y) - 0.5f,
            MaxX = listOfStones.Max(x => x.transform.position.x) + 0.5f,
            MaxY = listOfStones.Max(x => x.transform.position.y) + 0.5f
        };
    }

    private void Update()
    {
        float camerHalfHeight = GetComponent<Camera>().orthographicSize;
        float camerHalfWhidth = camerHalfHeight * ((float)Screen.width / Screen.height);

        var duck = FindObjectOfType<Duck>().transform.position;
        
        var x = duck.x;
        var y = duck.y;

        x = Mathf.Clamp(x, field.MinX + camerHalfWhidth, field.MaxX - camerHalfWhidth);
        y = Mathf.Clamp(y, field.MinY + camerHalfHeight, field.MaxY - camerHalfHeight);

        transform.position = new Vector3(x, y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(field.MinX, field.MinY), new Vector2(field.MaxX, field.MinY));
        Gizmos.DrawLine(new Vector2(field.MinX, field.MaxY), new Vector2(field.MaxX, field.MaxY));
        Gizmos.DrawLine(new Vector2(field.MinX, field.MinY), new Vector2(field.MinX, field.MaxY));
        Gizmos.DrawLine(new Vector2(field.MaxX, field.MinY), new Vector2(field.MaxX, field.MaxY));
    }

    public struct Field
    {
        public float MinX;
        public float MinY;
        public float MaxX;
        public float MaxY;
    }
}
