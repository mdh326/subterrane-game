using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footprintScript : MonoBehaviour {

    public Transform rightFoot, leftFoot;
    public Material leftMat, rightMat; //fix it so we need just 1 mat
    public float minFootDistance = 1f;
    public Vector3 lastPosition;
    bool left = false;
	void Start () {
        lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, lastPosition) >= minFootDistance)
        {
            PlaceFootPrint();
            lastPosition = transform.position;
        }
	}

    public void PlaceFootPrint()
    {
        Vector3 startPos = left ? leftFoot.position : rightFoot.position;
        
        Ray ray = new Ray(startPos, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5f))
        {
            Vector3 first = Vector3.zero;
            first.x -= 0.5f;                //0.5
            first.y += 0.1f;                //0.1
            first.z -= 0.8f;                //0.5
            Vector3 second = first;
            second.x += 0.5f;               //1
            Vector3 third = first;
            third.z -= 0.8f;                //0.5
            Vector3 fourth = third;
            fourth.x += 0.5f;               //1
            GameObject game = new GameObject();
            game.transform.position = hit.point;
            game.transform.rotation = this.transform.rotation;
            game.AddComponent<MeshFilter>().mesh = createQuad(first, second, third, fourth);
            game.AddComponent<MeshRenderer>().material = left ? leftMat : rightMat;
            game.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            game.name = left ? "leftFoot" : "rightFoot";
            Destroy(game, 5f);
        }
        left = !left;
    }

    public Mesh createQuad(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        Mesh m = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        verts.Add(first);
        verts.Add(second);
        verts.Add(third);
        verts.Add(fourth);

        int[] tris = new int[] { 0, 1, 2, 2, 1, 3 };
        List<Vector2> uvs = new List<Vector2>();
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(1, 1));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(1, 0));
        m.SetVertices(verts);
        m.SetTriangles(tris, 0);
        m.SetUVs(0, uvs);

        return m;
    }
}

