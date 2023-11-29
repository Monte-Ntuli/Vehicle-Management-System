using AutoMapper;
using BlazorApp1.Client.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AnswerController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AnswerController(IUnitOfWork unitOfWork, ILogger<AnswerController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }
    }
}
