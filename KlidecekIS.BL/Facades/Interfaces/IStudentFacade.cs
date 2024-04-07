using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

namespace KlidecekIS.BL.Facades.Interfaces;

public interface IStudentFacade :  IFacade<StudentEntity, StudentListModel, StudentDetailModel>
{
    Task<List<StudentListModel>> SearchStudentByName(string name);
}