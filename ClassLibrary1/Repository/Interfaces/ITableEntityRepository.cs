using MockarooLibrary.Model;

namespace MockarooLibrary.Repository.Interfaces
{
    public interface ITableEntityRepository
    {
        void Create(TableEntity tableEntity);

        void Delete(int id, Type entityType);

        TableEntity Get(int id, Type entityType);

        List<TableEntity> GetAll(Type entityType);

        List<TableEntity> GetEntities(int neededAmount, Type entityType);

        void Update(TableEntity tableEntity);
    }
}