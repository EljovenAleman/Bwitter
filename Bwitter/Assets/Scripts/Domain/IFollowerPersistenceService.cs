using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowerPersistenceService
{
    void Save(string serializedObject);

    string Load();
}
