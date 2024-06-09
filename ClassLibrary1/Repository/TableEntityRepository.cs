using Dapper;
using MockarooLibrary.Model;
using MockarooLibrary.Repository.Interfaces;
using Npgsql;
using System.Data;

namespace MockarooLibrary.Repository
{
    public class TableEntityRepository : ITableEntityRepository
    {
        private string connectionString = null;

        public TableEntityRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(TableEntity tableEntity)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = String.Format("INSERT INTO \"{0}\" (\"Value\") VALUES('{1}')", tableEntity.GetType().Name, tableEntity.Value);
                db.Execute(sqlQuery, tableEntity);
            }
        }

        public void Delete(int id, Type entityType)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = String.Format("DELETE FROM \"{0}\" WHERE Id = {1}", entityType.Name, id);
                db.Execute(sqlQuery, new { id });
            }
        }

        public TableEntity Get(int id, Type entityType)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = String.Format("DELETE FROM \"{0}\" WHERE Id = {1}", entityType.Name, id);
                return db.Query<TableEntity>(sqlQuery, new { id }).FirstOrDefault();
            }
        }

        public List<TableEntity> GetEntities(int neededAmount, Type entityType)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = String.Format("SELECT * FROM \"{0}\" ORDER BY RANDOM() LIMIT {1} ", entityType.Name, neededAmount);
                return db.Query<TableEntity>(sqlQuery).ToList();
            }
        }

        public List<TableEntity> GetAll(Type entityType)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = String.Format("SELECT * FROM \"{0}\"", entityType.Name);
                return db.Query<TableEntity>(sqlQuery).ToList();
            }
        }

        public void Update(TableEntity tableEntity)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = String.Format("UPDATE \"{0}\" SET Name = @Name, Age = @Age WHERE Id = @Id");
                db.Execute(sqlQuery, tableEntity);
            }
        }
    }
}