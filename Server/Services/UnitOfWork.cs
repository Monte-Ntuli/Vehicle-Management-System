using BlazorApp1.Client.Repos;
using BlazorApp1.Client.Repos.Interfaces;
using BlazorApp1.Client.Services.Interfaces;

namespace BlazorApp1.Client.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly VehicleDbContext _context;

        private readonly IConfiguration _config;
        public UnitOfWork(VehicleDbContext context, IConfiguration config) 
        { 
            _context = context; 
            _config = config;
        }

        public IEmployeeRepository _employee;
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employee == null) 
                { 
                    _employee = new EmployeeRepository(_context, _config); 
                }
                return _employee;
            }
        }

        public IQuestionaireRepository _questionaire;
        public IQuestionaireRepository Questionaire
        {
            get
            {
                if (_questionaire == null ) { _questionaire = new QuestionaireRepository(_context); }
                return _questionaire;
            }
        }

        public IVehicleTypeRepository _vehicleType;
        public IVehicleTypeRepository VehicleType
        {
            get
            {
                if (_vehicleType == null) { _vehicleType = new VehicleTypeRepository(_context); }
                return _vehicleType;
            }
        }

        public IVehicleRepository _vehicle;
        public IVehicleRepository Vehicle
        {
            get
            {
                if (_vehicle == null) { _vehicle = new VehicleRepository(_context); }
                return _vehicle;
            }

        }

        public IQuestionsRepository _questions;
        public IQuestionsRepository Questions
        {
            get
            {
                if (_questions == null ) { _questions = new QuestionsRepository(_context); }
                return _questions;
            }
        }

        public IVehicleModelTypeRepository _vehicleModelType;
        public IVehicleModelTypeRepository VehicleModelType
        {
            get
            {
                if (_vehicleModelType == null) { _vehicleModelType = new VehicleModelTypeRepository(_context); }
                return _vehicleModelType;
            }
        }
    }
}
