using UnityEngine;

public class ChangeObjectColor : MonoBehaviour
{
    // This function is called when the script is enabled
    void Start()
    {
        // Get the Renderer component of the GameObject and change its material color
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // "_Color" (or "_BaseColor" for URP) is the property name for the main color
            print("Should change");
            renderer.material.SetColor("_BaseColor", Color.red);
        }
        print("No render");
    }
}