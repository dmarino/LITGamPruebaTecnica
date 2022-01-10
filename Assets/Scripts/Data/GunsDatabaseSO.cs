using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName ="New Gun Database", menuName ="Gun System/Guns/Database")]
public class GunsDatabaseSO : ScriptableObject
{
    public BaseGunSO[] guns; //stores the array of guns
}
