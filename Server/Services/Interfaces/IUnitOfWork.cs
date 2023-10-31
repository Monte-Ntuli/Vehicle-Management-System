using BlazorApp1.Client.Repos.Interfaces;
using BlazorApp1.Server.Repos.Interfaces;

namespace BlazorApp1.Client.Services.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        IQuestionaireRepository Questionaire { get; }
        IVehicleTypeRepository VehicleType { get; }
        IVehicleRepository Vehicle { get; }
        IQuestionsRepository Questions { get; }
        IVehicleMakeRepository VehicleMake { get; }
        IAnswerRepository Answer { get; }
        IReportRepository Report { get; }
    }
}
