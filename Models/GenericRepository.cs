﻿using Dapper;
using Microsoft.Data.SqlClient;
namespace GentsOutfits_Authentication_.Models
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly string _connectionString;

        public GenericRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(TEntity entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;
                var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "Id");

                var columnNames = string.Join(",", properties.Select(p => p.Name));
                var parameterNames = string.Join(",", properties.Select(p => "@" + p.Name));

                var query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames});";
                connection.Execute(query, entity);
                //using (var command = new SqlCommand(query, connection))
                //{
                //    foreach (var property in properties)
                //    {
                //        command.Parameters.AddWithValue("@" + property.Name, property.GetValue(entity));
                //    }

                //    command.ExecuteNonQuery();
                //}
            }
        }

        public TEntity GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;
                var primaryKey = "Id";

                var query = $"SELECT * FROM {tableName} WHERE {primaryKey} = @Id;";
                return connection.QuerySingleOrDefault<TEntity>(query, new { Id = id });
                //using (var command = new SqlCommand(query, connection))
                //{
                //    command.Parameters.AddWithValue("@Id", id);
                //    var reader = command.ExecuteReader();
                //    if (reader.Read())
                //    {
                //        return MapReaderToObject(reader);
                //    }
                //    return null;
                //}
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;

                var query = $"SELECT * FROM {tableName};";
                return connection.Query<TEntity>(query).ToList();
                //using (var command = new SqlCommand(query, connection))
                //{
                //    var reader = command.ExecuteReader();
                //    var entities = new List<TEntity>();
                //    while (reader.Read())
                //    {
                //        entities.Add(MapReaderToObject(reader));
                //    }
                //    return entities;
                //}
            }
        }

        public void Update(TEntity entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;
                var primaryKey = "Id";

                var properties = typeof(TEntity).GetProperties().Where(p => p.Name != primaryKey);

                var setClause = string.Join(",", properties.Select(p => $"{p.Name} = @{p.Name}"));
                var query = $"UPDATE {tableName} SET {setClause} WHERE {primaryKey} = @{primaryKey};";

                connection.Execute(query, entity);
                //using (var command = new SqlCommand(query, connection))
                //{
                //    foreach (var property in properties)
                //    {
                //        command.Parameters.AddWithValue("@" + property.Name, property.GetValue(entity));
                //    }
                //    command.Parameters.AddWithValue("@" + primaryKey, typeof(TEntity).GetProperty(primaryKey).GetValue(entity));

                //    command.ExecuteNonQuery();
                //}
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;
                var primaryKey = "Id";

                var query = $"DELETE FROM {tableName} WHERE {primaryKey} = @Id;";
                connection.Execute(query, new { Id = id });
                //using (var command = new SqlCommand(query, connection))
                //{
                //    command.Parameters.AddWithValue("@Id", id);
                //    command.ExecuteNonQuery();
                //}
            }
        }

        private TEntity MapReaderToObject(SqlDataReader reader)
        {
            var entity = Activator.CreateInstance<TEntity>();
            foreach (var property in typeof(TEntity).GetProperties())
            {
                    property.SetValue(entity, reader[property.Name]);
                //if (property.Name != "Id")
                //{
                //}
            }
            return entity;
        }


    }

}
