using Newtonsoft.Json;
using System.Collections.Generic;

public class JsonFollowerRepository : IFollowerRepository
{
    IFollowerPersistenceService persistenceService;    
    IUserRepository userRepository;

    public JsonFollowerRepository(IFollowerPersistenceService persistenceService, IUserRepository userRepository)
    {
        this.persistenceService = persistenceService;
        this.userRepository = userRepository;
    }

    public void Follow(string followerNickname, string followeeNickname)
    {
        if(!userRepository.IsRegistered(followerNickname) || !userRepository.IsRegistered(followeeNickname))
        {
            throw new UserDoesntExistException();
        }
        
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
        if (!userRepository.IsRegistered(follower) || !userRepository.IsRegistered(followee))
        {
            throw new UserDoesntExistException();
        }

        var followees = GetFollowees();

        try
        {
            var followeesList = followees[follower];

            return followeesList.Contains(followee);
        }
        catch(KeyNotFoundException exception)
        {
            return false;
        }
        
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





