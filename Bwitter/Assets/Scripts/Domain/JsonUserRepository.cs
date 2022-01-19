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
        Dictionary<string, string> users = new Dictionary<string, string>();
        users.Add(nickname, name);
        var serializedString = JsonConvert.SerializeObject(users);
        persistenceService.Save(serializedString);
    }

    public string GetNameFromNickName(string nickname)
    {
        throw new System.NotImplementedException();
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
}


