using UnityEditor;
using UnityEngine;

public class GunLoader : MonoBehaviour
{
    private GunsDatabaseSO database;

    private void Awake()
    {
        //here i load the scriptable object, the way of doing it depends if i'm on the editor or not
        #if UNITY_EDITOR
        database = (GunsDatabaseSO)AssetDatabase.LoadAssetAtPath("Assets/Resources/Gun Database.asset", typeof(GunsDatabaseSO));
        #else
        database = Resources.Load<GunsDatabaseSO>("Gun Database");
        #endif
    }

    private void Start()
    {
        if(database == null) return;
        foreach (BaseGunSO gun in database.guns)
        {
            Instantiate(gun.prefab, gun.spawnPosition, Quaternion.identity);
        }
    }
   
}
