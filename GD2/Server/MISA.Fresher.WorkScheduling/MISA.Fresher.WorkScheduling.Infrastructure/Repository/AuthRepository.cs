using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Fresher.WorkScheduling.Core.Entities;
using MISA.Fresher.WorkScheduling.Core.Interfaces.IRepository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Infrastructure.Repository
{

    public class AuthRepository : IAuthRepository
    {
        #region Fields
        string _connectionString;
        protected MySqlConnection _sqlConnection;
        string _tableName;
        #endregion

        #region constructor
        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LocaConnection");
            // Khởi tạo kết nối
            _sqlConnection = new MySqlConnection(_connectionString);
        }
        #endregion

        public User Registration()
        {
            // lấy dữ liệu
            var sqlCommand = $"select * from users where userName = @userName";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@userName", "a");
            //QueryFirstOrDefault: Lấy bản ghi đầu tiên từ câu lệnh try vấn., nếu ko có trả về null;
            var tEntiy = _sqlConnection.QueryFirstOrDefault<User>(sqlCommand, param: parameters);
            return tEntiy;
        }

        public User CheckDuplicateName(string name)
        {
            // lấy dữ liệu
            var sqlCommand = $"select * from users where userName = @userName";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@userName", name);
            //QueryFirstOrDefault: Lấy bản ghi đầu tiên từ câu lệnh try vấn., nếu ko có trả về null;
            var tEntiy = _sqlConnection.QueryFirstOrDefault<User>(sqlCommand, param: parameters);
            return tEntiy;
        }

        public bool createUser(User user)
        {
            var sqlCommand = $"INSERT INTO users(idUser ,userName, password, role)VALUES(UUID(), @userName, @password, @role); ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@userName", user.userName);
            parameters.Add($"@password", user.password);
            parameters.Add($"@role", user.role);
            var res = _sqlConnection.Execute(sqlCommand, param: parameters, commandType: System.Data.CommandType.Text);
            if (res == 1)
            {
                return true;
            }
            return false;
        }

        public bool saveToken(Token token)
        {
            var sqlCommand = $"INSERT INTO tokens(idToken ,token, idUser)VALUES(@idToken, @token, @idUser);";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@idToken", token.idToken);
            parameters.Add($"@token", token.token);
            parameters.Add($"@idUser", token.idUser);
            var res = _sqlConnection.Execute(sqlCommand, param: parameters, commandType: System.Data.CommandType.Text);
            if (res == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<User> GetEntityById(Guid userid)
        {
            var query = $"SELECT * FROM users WHERE idUser = '{userid}'";

            //Khởi tạo commandText
            var entity = await _sqlConnection.QueryFirstOrDefaultAsync<User>(query, commandType: CommandType.Text);

            return entity;
        }
    }
}
