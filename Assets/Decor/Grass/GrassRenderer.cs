using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GrassRenderer : MonoBehaviour {

    public Mesh grassMesh;
    public Material grassMaterial;

    public Vector2 size;
    [Range(0, 1022)]
    public int numGrasses;
    public int seed;

    public float radius = 1000f;
    public float GrassOffSet = 0f;
	public LayerMask mask;
    private List<Matrix4x4> _matrices;

    // Use this for initialization
    void Start () {
		Random.InitState(seed);
        _matrices = new List<Matrix4x4>(numGrasses);
        for(int i = 0; i < numGrasses; i++)
        {
            Vector3 origin = new Vector3(
                this.transform.position.x + size.x * Random.Range(-.5f, .5f),
                this.transform.position.y,
                transform.position.z + size.y * Random.Range(-.5f, .5f));

            Ray ray = new Ray(origin, Vector3.down);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, radius, mask))
            {
                _matrices.Add(Matrix4x4.TRS(hit.point + (Vector3.up * GrassOffSet), Quaternion.identity, Vector3.one));
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        Graphics.DrawMeshInstanced(grassMesh,0, grassMaterial, _matrices, null, UnityEngine.Rendering.ShadowCastingMode.Off);
	}
}
