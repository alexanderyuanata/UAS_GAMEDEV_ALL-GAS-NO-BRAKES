using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    [SerializeField] private string new_name;
    [SerializeField] private CombineInstance[] combine;
    [SerializeField] private MeshFilter[] meshFilters;

    [ContextMenu("Combine Meshes")]
    public void CombineMeshesWithColliders()
    {
        combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

            // Optional: Disable original mesh renderer
            /**
            meshFilters[i].gameObject.SetActive(false);
            **/

            // Optional: Disable original mesh collider
            /**
            MeshCollider meshCollider = meshFilters[i].GetComponent<MeshCollider>();
            if (meshCollider != null)
            {
                meshCollider.enabled = false;
            }
            **/
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);

        GameObject combinedObject = new GameObject(new_name);
        MeshFilter meshFilter = combinedObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = combinedMesh;
        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial;

        MeshCollider combinedCollider = combinedObject.AddComponent<MeshCollider>();
        combinedCollider.sharedMesh = combinedMesh;
        combinedCollider.enabled = true;

        combinedObject.transform.position = transform.position;
        combinedObject.transform.rotation = transform.rotation;
    }
}
