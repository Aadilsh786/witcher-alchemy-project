using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    [Header("Objects To Control")]
    public GameObject objectToDelete;  // Object that will be deleted
    public GameObject objectToEnable;  // Object that will be enabled

    // Call this function from a UI button's OnClick event
    public void SwitchObjects()
    {
        if (objectToDelete != null)
        {
            Destroy(objectToDelete);
        }

        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
    }
}
