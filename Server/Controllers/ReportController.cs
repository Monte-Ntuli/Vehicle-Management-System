using AutoMapper;
using BlazorApp1.Client.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReportController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public ReportController(IUnitOfWork unitOfWork, ILogger<ReportController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }
    }
}
