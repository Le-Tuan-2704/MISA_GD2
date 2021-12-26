using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Infrastructure.Repository
{
    public class BaseRepository<TEntity>
    {
        #region Fields
        string _connectionString;
        protected MySqlConnection _sqlConnection;
        string _tableName;
        #endregion

        #region constructor
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LocaConnection");
            // Khởi tạo kết nối
            _sqlConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(TEntity).Name.ToLower();
        }
        #endregion


        #region Methods
        async public Task<IEnumerable<TEntity>> GetAll()
        {
            var query = $"select * from {_tableName} ORDER BY createdDate DESC";
            var tEntiy = await _sqlConnection.QueryAsync<TEntity>(query, commandType: CommandType.Text);
            return tEntiy.ToList();
        }

        public async virtual Task<TEntity> GetById(Guid tEntityId)
        {
            // lấy dữ liệu
            var sqlCommand = $"select * from {_tableName} where {_tableName}Id = @{_tableName}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_tableName}Id", tEntityId);
            //QueryFirstOrDefault: Lấy bản ghi đầu tiên từ câu lệnh try vấn., nếu ko có trả về null;
            var tEntiy = await _sqlConnection.QueryFirstOrDefaultAsync<TEntity>(sqlCommand, param: parameters);
            return tEntiy;
        }

        public async virtual Task<int> Insert(TEntity tEntity)
        {
            var rowsAffected = 0;

            using (var transaction = _sqlConnection.BeginTransaction())
            {
                try
                {
                    var parameters = MappingDbType(tEntity);
                    rowsAffected = await _sqlConnection.ExecuteAsync($"Proc_Insert{_tableName}", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return rowsAffected;
        }
        public async virtual Task<int> Delete(string PotentialCustomerId)
        {
            var rowsAffected = 0;

            using (var transaction = _sqlConnection.BeginTransaction())
            {
                try
                {
                    var query = $"DELETE FROM {_tableName} WHERE {_tableName}Id = '{PotentialCustomerId}'";

                    //Thực thi commandText
                    rowsAffected = await _sqlConnection.ExecuteAsync(query, commandType: CommandType.Text, transaction: transaction);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return rowsAffected;
        }

        public async Task<int> Update(Guid tEntiyId, TEntity tEntity)
        {
            var rowsAffected = 0;

            using (var transaction = _sqlConnection.BeginTransaction())
            {
                try
                {
                    var parameters = MappingDbType(tEntity);
                    rowsAffected = await _sqlConnection.ExecuteAsync($"Proc_Update{_tableName}", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Map dữ liệu của 1 entity sang thành dynamic parameters dùng cho truy vấn SQL
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns>dynamic parameters đã được format đúng</returns>
        protected DynamicParameters MappingDbType<TEntity>(TEntity entity)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyName == "EntityState")
                {
                    continue;
                }
                else if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            return parameters;
        }
        #endregion
    }
}
