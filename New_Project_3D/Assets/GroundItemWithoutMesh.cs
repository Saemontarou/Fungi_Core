using UnityEditor.ProBuilder;
using UnityEditor;
using UnityEngine;
using EditorUtility = UnityEditor.EditorUtility;

public class GroundItemWithoutMesh : MonoBehaviour//, ISerializationCallbackReceiver
{
    internal ItemsObject item;
    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.Icon;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }
    
    public void OnAfterDeserialize()
    {
    
    }
}
