using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Models;

namespace DapperWith4DatabaseCommunication.Services
{
    public class DepartementService:IDepartementService
    {
        private readonly IDepartmentRepository _repository;
        public DepartementService(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> AddDeparment(DepartmentDto deptdetail)
        {
            Department objDept = new Department();
            objDept.deptname = deptdetail.deptname;
            objDept.deptlocation = deptdetail.deptlocation;
            objDept.deptid = deptdetail.deptid;
            var res = await _repository.AddDeparment(objDept);
            return res;
        }

        public async Task<String> DeleteDepartmentById(int deptid)
        {
        var result= await _repository.DeleteDepartmentById(deptid);
            return result;
        }


        public async Task<List<DepartmentDto>> GetDepartMentDetails()
        {

            List<DepartmentDto> lstempdto = new List<DepartmentDto>();
            var res = await _repository.GetDepartMentDetails();
            foreach (Department dept in res)
            {
                DepartmentDto deptdto = new DepartmentDto();
                deptdto.deptid = dept.deptid;
                deptdto.deptname = dept.deptname;
                deptdto.deptlocation = dept.deptlocation;
                lstempdto.Add(deptdto);

            }
            return lstempdto;
        }

        public async Task<DepartmentDto> GetDepartmentDetailsById(int deptid)
        {
            var res = await _repository.GetDepartmentDetailsById(deptid);
            DepartmentDto deptdto = new DepartmentDto();
            deptdto.deptid = res.deptid;
            deptdto.deptname = res.deptname;
            deptdto.deptlocation = res.deptlocation;
            return deptdto;
        }

        public async Task<String> UpdateDepartment(DepartmentDto deptdetail)
        {
            Department objDept = new Department();
            objDept.deptid = deptdetail.deptid;
            objDept.deptname = deptdetail.deptname;
            objDept.deptlocation = deptdetail.deptlocation;
            var result = await _repository.UpdateDepartment(objDept);
            return result;
        }
    }
}

