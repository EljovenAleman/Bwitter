using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonUserRepository : IUserRepository
{    
    IUserPersistenceService persistenceService;

    public JsonUserRepository(IUserPersistenceService persistenceService)
    {
        this.persistenceService = persistenceService;
    }

    public void Register(string name, string nickname)
    {
        var deserializedDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(persistenceService.Load());

        deserializedDictionary.Add(nickname, name);
        var serializedString = JsonConvert.SerializeObject(deserializedDictionary);

        persistenceService.Save(serializedString);
    }

    public string GetNameFromNickName(string nickname)
    {
        var deserializedDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(persistenceService.Load());

        return deserializedDictionary[nickname];

    }

    public bool IsRegistered(string nickname)
    {
        return true;
    }


    public void UpdateUserName(string newName, string nickname)
    {
        throw new System.NotImplementedException();
    }
}

public interface IUserPersistenceService
{
    void Save(string serializedUsers);

    string Load();
}


