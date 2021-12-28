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
    public class EventRepository : BaseRepository<EventCalendar>, IEventRepository
    {
        string _connectionString;
        protected MySqlConnection _sqlConnection;
        public EventRepository(IConfiguration configuration) : base(configuration)
        {
            _connectionString = configuration.GetConnectionString("LocaConnection");
            // Khởi tạo kết nối
            _sqlConnection = new MySqlConnection(_connectionString);
        }

        #region Methods
        public async Task<IEnumerable<object>> GetEntitiesByEmployeeId(Guid employeeId)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add($"@p_EmployeeId", employeeId, DbType.String);

                var result = await _sqlConnection.QueryAsync($"Proc_Get{_tableName}sByEmployeeId", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<object>> GetPendingEntities()
        {
            try
            {
                var parameters = new DynamicParameters();
                /*
                                parameters.Add($"@GroupId", groupId, DbType.String);
                */
                var result = await _sqlConnection.QueryAsync($"Proc_GetPending{_tableName}sByGroupId", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<object>> GetEntitiesByManagerId(Guid managerId)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add($"@ManagerId", managerId, DbType.String);

                var result = await _sqlConnection.QueryAsync($"Proc_Get{_tableName}sByManagerId", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<object>> GetEntitiesOfEmployeeByRange(Guid employeeId, DateTime start, DateTime end)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add($"@p_EmployeeId", employeeId);
                parameters.Add($"@p_StartTime", start);
                parameters.Add($"@p_EndTime", end);

                var result = await _sqlConnection.QueryAsync($"Proc_Get{_tableName}sOfEmployeeByTimeRange", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeleteEntitesByIds(string idsString)
        {
            var rowsAffected = 0;

            _sqlConnection.Open();
            using (var transaction = _sqlConnection.BeginTransaction())
            {
                try
                {
                    var parameters = new DynamicParameters();

                    parameters.Add($"@IdsString", idsString, DbType.String);

                    rowsAffected = await _sqlConnection.ExecuteAsync($"Proc_Delete{_tableName}sByIds", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
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

        public async Task<int> ApproveEntitesByIds(string idsString, Guid approverId)
        {
            var rowsAffected = 0;

            _sqlConnection.Open();
            using (var transaction = _sqlConnection.BeginTransaction())
            {
                try
                {
                    var parameters = new DynamicParameters();

                    parameters.Add($"@IdsString", idsString, DbType.String);
                    parameters.Add($"@ApproverId", approverId, DbType.String);

                    rowsAffected = await _sqlConnection.ExecuteAsync($"Proc_Approve{_tableName}sByIds", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
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

        public async Task<int> CompleteById(Guid id)
        {
            var rowsAffected = 0;

            _sqlConnection.Open();
            using (var transaction = _sqlConnection.BeginTransaction())
            {
                try
                {
                    var parameters = new DynamicParameters();

                    parameters.Add($"@EventId", id, DbType.String);

                    rowsAffected = await _sqlConnection.ExecuteAsync($"Proc_Complete{_tableName}ById", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
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
        #endregion
    }
}
