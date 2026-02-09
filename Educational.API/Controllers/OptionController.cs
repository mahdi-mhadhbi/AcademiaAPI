using AutoMapper;
using Educational.Core.Repositories;
using Educational.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Educational.Core.Dtos;
using Educational.Core.Models;

namespace Educational.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        public readonly IMapper _mapper;

        public OptionController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddList(List<Options> options)
        {
            _context.Options.AddRange(options);
        }

        public void RemoveList(List<Options> options)
        {
            _context.Options.RemoveRange(options);
        }

      
    }
}
