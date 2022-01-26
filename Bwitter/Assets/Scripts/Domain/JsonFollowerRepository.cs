using Newtonsoft.Json;
using System.Collections.Generic;

public class JsonFollowerRepository : IFollowerRepository
{
    IFollowerPersistenceService persistenceService;

    public JsonFollowerRepository(IFollowerPersistenceService persistenceService)
    {
        this.persistenceService = persistenceService;
    }

    public void Follow(string followerNickname, string followeeNickname)
    {
        var followees = GetFollowees();

        if(!followees.ContainsKey(followerNickname))
        {
            followees.Add(followerNickname, new List<string>());
        }

        var followeesList = followees[followerNickname];

        followeesList.Add(followeeNickname);

        string serializedFollowees = JsonConvert.SerializeObject(followees);

        persistenceService.Save(serializedFollowees);

    }

    public bool IsFollowing(string follower, string followee)
    {
        throw new System.NotImplementedException();
    }
    
    public Dictionary<string, List<string>> GetFollowees()
    {        
        if(string.IsNullOrEmpty(persistenceService.Load()))
        {
            return new Dictionary<string, List<string>>();
        }
        
        return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(persistenceService.Load());                     
    }

    

    


}



