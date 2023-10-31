using AutoMapper;
using BlazorApp1.Server.Entities;
using BlazorApp1.Server.Models;
using BlazorApp1.Shared.AppUserDTO;
using BlazorApp1.Shared.EmployeeDTO;
using BlazorApp1.Shared.QuestionaireDTO;
using BlazorApp1.Shared.QuestionsDTO;
using BlazorApp1.Shared.VehicleMakeDTO;
using BlazorApp1.Shared.VehiclesDTO;
using BlazorApp1.Shared.VehicleTypeDTO;

namespace BlazorApp1.Server
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            #region User Entity Maps
            CreateMap<EmployeeEntity, EmployeeModel>().ReverseMap();
            CreateMap<EmployeeEntity, LoginDTO>().ReverseMap();
            CreateMap<EmployeeEntity, CreateEmployeeDTO>().ReverseMap();
            CreateMap<EmployeeEntity, UpdateEmployeeDTO>().ReverseMap();
            #endregion

            #region Questionaire Entity Maps
            CreateMap<QuestionaireEntity, QuestionaireModel>().ReverseMap();
            CreateMap<QuestionaireEntity, CreateQuestionaireDTO>().ReverseMap();
            CreateMap<QuestionaireEntity, UpdateQuestionaireDTO>().ReverseMap();
            #endregion

            #region VehicleType Entity Maps
            CreateMap<VehicleTypeEntity, VehicleTypeModel>().ReverseMap();
            CreateMap<VehicleTypeEntity, CreateVehicleTypeDTO>().ReverseMap();
            #endregion

            #region Questions Entity Maps
            CreateMap<QuestionsEntity, CreateQuestionDTO>().ReverseMap();
            CreateMap<QuestionsEntity, QuestionaireModel>().ReverseMap();
            CreateMap<QuestionsEntity, QuestionsModel>().ReverseMap();
            #endregion

            #region Vehicle Entity Maps
            CreateMap<VehicleEntity, CreateVehicleDTO>().ReverseMap();
            CreateMap<VehicleEntity, VehicleModel>().ReverseMap();
            CreateMap<VehicleEntity, UpdateVehicleDTO>().ReverseMap();
            #endregion

            #region Vehicle Make Maps
            CreateMap<VehicleMakeEntity, VehicleMakeRM>().ReverseMap();
            CreateMap<VehicleMakeEntity, CreateVehicleMakeDTO>().ReverseMap();
            CreateMap<VehicleMakeEntity, VehicleMakeModel>().ReverseMap();
            CreateMap<VehicleMakeEntity, UpdateVehicleMakeDTO>();
            #endregion

            #region Vehicle Type Maps
            CreateMap<VehicleTypeEntity, VehicleTypeModel>().ReverseMap();
            CreateMap<VehicleTypeEntity, CreateVehicleTypeDTO>().ReverseMap();
            CreateMap<VehicleTypeEntity, UpdateVehicleTypeDTO>().ReverseMap();
            #endregion

            #region Answer Repository
            CreateMap<AnswerEntity, AnswerModel>().ReverseMap();
            #endregion

            #region Report Repository
            CreateMap<ReportEntity, ReportModel>().ReverseMap();
            #endregion
        }
    }
}
