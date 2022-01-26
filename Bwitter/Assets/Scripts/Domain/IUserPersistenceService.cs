public interface IUserPersistenceService
{
    void Save(string serializedUsers);

    string Load();
}


