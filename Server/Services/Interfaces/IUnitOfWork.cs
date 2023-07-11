using BlazorApp1.Client.Repos.Interfaces;

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
    }
}
