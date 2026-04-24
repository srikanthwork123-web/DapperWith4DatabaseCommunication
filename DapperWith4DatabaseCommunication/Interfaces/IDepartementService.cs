using DapperWith4DatabaseCommunication.Dtos;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IDepartementService
    {
        Task<List<DepartmentDto>> GetDepartMentDetails();
        Task<DepartmentDto> GetDepartmentDetailsById(int deptid);
        Task<int> AddDeparment(DepartmentDto deptdetail);
        Task<string> DeleteDepartmentById(int deptid);
        Task<string> UpdateDepartment(DepartmentDto deptdetail);
    }
}
