using AccessManagement.Domain.Entities;
using System;
using System.Collections.Generic;

namespace AccessManagement.Repository
{
    public class EmployeeRepository : IRepository<Employee>
    {
        public IEnumerable<Employee> Get()
        {
            const string command = @"SELECT e.Name, e.Email, e.Id, d.Name AS Department
                                    FROM Employees e
                                    JOIN Departments d
                                    ON d.Id = e.DepartmentId";

            //using (var command = DatabaseFactory.CreateCommand(ConfigurationService.DbConnectionString, Constants.Tariff.SelectTariffCommand, System.Data.CommandType.Text, new Dictionary<string, object> { { "@tariffRevisionId", tariffRevisionId } }))
            //{
            //    var iDataReader = command.ExecuteReader();
            //    if (iDataReader != null)
            //    {
            //        string entityId;
            //        var tariffRevision = MapTariffRevesion(iDataReader, out entityId);
            //        tariffRevision.Id = tariffRevisionId;
            //        using (var subCommand = DatabaseFactory.CreateCommand(ConfigurationService.DbConnectionString, Constants.Tariff.SelectProductsCommand, System.Data.CommandType.Text, new Dictionary<string, object> { { "@changesetId", entityId } }))
            //        {
            //            var subIDataReader = subCommand.ExecuteReader();
            //            if (subIDataReader != null)
            //            {
            //                tariffRevision.SelectedProducts = MapProducts(subIDataReader);
            //            }
            //        }
            //        return tariffRevision;
            //    }
            //    return null;
            //}
            return null;
        }

        public Employee Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
