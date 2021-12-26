using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Fresher.WorkScheduling.Core.Enums;
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


            var properties = tEntity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                // Lấy tên của property
                var propertyName = property.Name;
                // Lấy ra giá trị của property
                var propertyValue = property.GetValue(tEntity);
                // Lấy ra kiểu dữ liệu của property
                var propertyType = property.PropertyType;

                if ((propertyType == typeof(Guid) || propertyType == typeof(Guid?)))
                {
                    propertyValue = Guid.NewGuid();
                }

                parameters.Add($"@p_{propertyName}", propertyValue);

            }
            rowsAffected = await _sqlConnection.ExecuteAsync($"Proc_Insert{_tableName}", parameters, commandType: CommandType.StoredProcedure);

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

            var properties = tEntity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                // Lấy tên của property
                var propertyName = property.Name;
                // Lấy ra giá trị của property
                var propertyValue = property.GetValue(tEntity);
                // Lấy ra kiểu dữ liệu của property
                var propertyType = property.PropertyType;

                if ((propertyType == typeof(Guid) || propertyType == typeof(Guid?)))
                {
                    propertyValue = tEntiyId;
                }

                parameters.Add($"@p_{propertyName}", propertyValue);

            }
            rowsAffected = await _sqlConnection.ExecuteAsync($"Proc_Update{_tableName}", parameters, commandType: CommandType.StoredProcedure);

            return rowsAffected;
        }

        #endregion
    }
}
