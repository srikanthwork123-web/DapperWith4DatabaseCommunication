using DapperWith4DatabaseCommunication.Models;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartMentDetails();
        Task<Department> GetDepartmentDetailsById(int deptid);
        Task<int> AddDeparment(Department deptdetail);
        Task<string> DeleteDepartmentById(int deptid);
        Task<String> UpdateDepartment(Department deptdetail);
    }
}
