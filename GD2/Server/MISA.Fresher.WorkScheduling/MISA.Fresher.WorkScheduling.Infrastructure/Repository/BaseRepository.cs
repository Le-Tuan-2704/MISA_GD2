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
        public List<TEntity> GetAll()
        {
            var query = $"select * from {_tableName} ORDER BY createdDate DESC";
            var tEntiy = _sqlConnection.Query<TEntity>(query, commandType: CommandType.Text);
            return tEntiy.ToList();
        }

        public TEntity GetById(Guid tEntityId)
        {
            // lấy dữ liệu
            var sqlCommand = $"select * from {_tableName} where {_tableName}Id = @{_tableName}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_tableName}Id", tEntityId);
            //QueryFirstOrDefault: Lấy bản ghi đầu tiên từ câu lệnh try vấn., nếu ko có trả về null;
            var tEntiy = _sqlConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: parameters);
            return tEntiy;
        }
        public int Insert(TEntity tEntity)
        {
            //Khai bái chuỗi SQL động;
            var sqlColumDynamic = "";
            var sqlParamDynamic = "";
            var dynamicParams = new DynamicParameters();
            // Lấy ra các properties của đối tượng
            var props = tEntity.GetType().GetProperties();
            // Duyệt từng property
            foreach (var prop in props)
            {
                // Lấy tên của property
                var propName = prop.Name;
                // Lấy ra giá trị của property
                var propValue = prop.GetValue(tEntity);
                // Lấy ra kiểu dữ liệu của property
                var propType = prop.PropertyType;

                if (propName == $"{_tableName}Id" && propType == typeof(Guid))
                {
                    propValue = Guid.NewGuid();
                }

                //Bổ sung vào chuổi Colum động
                sqlColumDynamic += $"{propName},";
                sqlParamDynamic += $"@{propName},";
                dynamicParams.Add($"@{propName}", propValue);
            }

            // căt dấu phẩy cuối cùng
            sqlColumDynamic = sqlColumDynamic.Substring(0, sqlColumDynamic.Length - 1);
            sqlParamDynamic = sqlParamDynamic.Substring(0, sqlParamDynamic.Length - 1);
            var sqlDynamic = $"INSERT INTO {_tableName}({sqlColumDynamic}) VALUES({sqlParamDynamic})";

            var res = _sqlConnection.Execute(sqlDynamic, param: dynamicParams, commandType: System.Data.CommandType.Text);
            return res;
        }
        public int Delete(string PotentialCustomerId)
        {
            var sqlCheckCustomerCode = $"delete from {_tableName} where @PotentialCustomerId LIKE CONCAT('%',PotentialCustomerId,'%')";
            var paramCode = new DynamicParameters();
            paramCode.Add("@PotentialCustomerId", PotentialCustomerId);
            var res = _sqlConnection.Execute(sqlCheckCustomerCode, paramCode);
            return res;
        }

        public int Update(Guid tEntiyId, TEntity tEntity)
        {
            //Khai bái chuỗi SQL động;
            var sqlColumDynamic = "";
            var sqlParamDynamic = "";
            var sqlUpdate = "";
            var dynamicParams = new DynamicParameters();
            // Lấy ra các properties của đối tượng
            var props = tEntity.GetType().GetProperties();
            // Duyệt từng property
            foreach (var prop in props)
            {
                // Lấy tên của property
                var propName = prop.Name;
                // Lấy ra giá trị của property
                var propValue = prop.GetValue(tEntity);
                // Lấy ra kiểu dữ liệu của property
                var propType = prop.PropertyType;



                if (propName == $"{_tableName}Id" && propType == typeof(Guid))
                {
                    continue;
                }
                dynamicParams.Add($"@{propName}", propValue);
                //Bổ sung vào chuổi Colum động
                sqlColumDynamic = $"{propName}";
                sqlParamDynamic = $"@{propName}";
                sqlUpdate += $"{sqlColumDynamic}={sqlParamDynamic},";

            }

            // căt dấu phẩy cuối cùng
            sqlUpdate = sqlUpdate.Substring(0, sqlUpdate.Length - 1);

            var sqlDynamic = $"UPDATE {_tableName} SET {sqlUpdate} Where PotentialCustomerId = @PotentialCustomerId";
            dynamicParams.Add($"@PotentialCustomerId", tEntiyId);
            var res = _sqlConnection.Execute(sqlDynamic, param: dynamicParams, commandType: System.Data.CommandType.Text);
            return res;
        }
        #endregion
    }
}
