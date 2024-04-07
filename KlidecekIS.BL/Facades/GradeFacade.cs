using AutoMapper;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.UnitOfWork;

namespace KlidecekIS.BL.Facades;

public class GradeFacade(IMapper modelMapper, IUnitOfWorkFactory unitOfWorkFactory) :
    FacadeBase<
        GradeEntity,
        GradeListModel,
        GradeDetailModel
    >(modelMapper, unitOfWorkFactory), IGradeFacade;
