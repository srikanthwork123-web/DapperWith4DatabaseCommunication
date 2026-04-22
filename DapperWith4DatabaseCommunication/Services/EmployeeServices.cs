using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;

namespace DapperWith4DatabaseCommunication.Services
{
    public class EmployeeServices: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeServices(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> AddEmployes(EmployeeDto empdetail)
        {
            Employee emp = new Employee();
            emp.empid = empdetail.empid;
            emp.empsalary = empdetail.empsalary;
            emp.empname = empdetail.empname;
            var res = await _employeeRepository.AddEmployes(emp);
            return res;
        }

        public async Task<bool> DeleteEmployesById(int empid)
        {
            await _employeeRepository.DeleteEmployesById(empid);
            return true;
        }

        public async Task<EmployeeDto> GetEmployeeById(int empid)
        {
            var res = await _employeeRepository.GetEmployeeById(empid);
            EmployeeDto empdto = new EmployeeDto();
            empdto.empid = res.empid;
            empdto.empname = res.empname;
            empdto.empsalary = res.empsalary;
            return empdto;
        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            List<EmployeeDto> lstempdto = new List<EmployeeDto>();
            var res = await _employeeRepository.GetEmployees();
            foreach (Employee emp in res)
            {
                EmployeeDto empdto = new EmployeeDto();
                empdto.empid = emp.empid;
                empdto.empsalary = emp.empsalary;
                empdto.empname = emp.empname;
                lstempdto.Add(empdto);

            }
            return lstempdto;
        }

        public async  Task<bool> UpdateEmploye(EmployeeDto empdetail)
        {
            Employee emp = new Employee();
            emp.empid = empdetail.empid;
            emp.empsalary = empdetail.empsalary;
            emp.empname = empdetail.empname;
            await _employeeRepository.UpdateEmploye(emp);
            return true;
        }
    }
}
