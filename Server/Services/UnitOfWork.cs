using BlazorApp1.Client.Repos;
using BlazorApp1.Client.Repos.Interfaces;
using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Server.Repos;
using BlazorApp1.Server.Repos.Interfaces;

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

        public IVehicleMakeRepository _vehicleMake;
        public IVehicleMakeRepository VehicleMake
        {
            get
            {
                if (_vehicleMake == null) { _vehicleMake = new VehicleMakeRepository(_context); }
                return _vehicleMake;
            }
        }

        public IAnswerRepository _answer;
        public IAnswerRepository Answer
        {
            get
            {
                if (_answer == null) { _answer = new AnswerRepository(_context); }
                return _answer;
            }
        }

        public IReportRepository _report;
        public IReportRepository Report
        {
            get
            {
                if (_report == null) { _report = new ReportRepository(_context); }
                return _report;
            }
        }
    }
}
