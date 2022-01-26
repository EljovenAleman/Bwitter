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
        var users = GetUsers();

        users.Add(nickname, name);

        SaveUsers(users);
    }

    public string GetNameFromNickName(string nickname)
    {        
        return GetUsers()[nickname];
    }

    public bool IsRegistered(string nickname)
    {
        return GetUsers().ContainsKey(nickname);
    }

    public void UpdateUserName(string newName, string nickname)
    {
        var users = GetUsers();
        if (!users.ContainsKey(nickname))
        {
            throw new UserDoesntExistException();
        }
        
        users[nickname] = newName;

        SaveUsers(users);        
    }

    private Dictionary<string, string> GetUsers()
    {
        if (string.IsNullOrEmpty(persistenceService.Load()))
        {
            return new Dictionary<string, string>();
        }

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(persistenceService.Load());
    }

    private void SaveUsers(Dictionary<string, string> users)
    {
        var serializedString = JsonConvert.SerializeObject(users);

        persistenceService.Save(serializedString);
    }
}


