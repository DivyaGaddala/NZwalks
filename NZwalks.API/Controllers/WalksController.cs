using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.DTO;
using NZwalks.API.Models.Domain;
using NZwalks.API.Repository;
using NZwalks.API.CustomActionFilters;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkrepository;

        public WalksController(IMapper mapper, IWalkRepository walkrepository)
        {
            this.mapper = mapper;
            this.walkrepository = walkrepository;
        }



        //CREATE walk
        //POST:/api/Walks

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addwalkrequestdto)
        {
           
                //DTO - Domain
                var walkdomainmodel = mapper.Map<Walk>(addwalkrequestdto);

                //call the repository
                await walkrepository.CreateAsync(walkdomainmodel);

                //Map Domain - DTO
                return Ok(mapper.Map<WalkDTO>(walkdomainmodel));
            }
            
        //GET walks
        //GET:/api/walks

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]string? filterOn, [FromQuery]string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery]int pageNumber=1 , [FromQuery] int pageSize =1000 )
        {
           

            var walksdomainmodel = await walkrepository.GetAllAsync(filterOn,filterQuery, sortBy, isAscending,pageNumber,pageSize);
            //create an exception

            throw new Exception("This is a new Exception");

            //Map Domain - DTO
            return Ok(mapper.Map<List<WalkDTO>>(walksdomainmodel));
        }

        //GET waik by id
        //GET:/api/Walks{id}
        [HttpGet]
        [Route("{id:Guid}")]
       public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //call therepository
            var walkdomainmodel = await walkrepository.GetByIdAsync(id);

            //check if the idd exists or not
            if (walkdomainmodel == null)
            {
                return NotFound();
            }

            //map Domain to DTO
            return Ok(mapper.Map<WalkDTO>(walkdomainmodel));
        }

        //update wqlk by id
        //PUT:/api/Walks{id}
        [HttpPut("{id:Guid}")]
        [ValidateModel]
        
        public async Task<IActionResult> Update( Guid id, UpdateWalkRequestDTO updatewalkrequestdto)
        {
                //Map DTo-Domain
                var domainmodel = mapper.Map<Walk>(updatewalkrequestdto);

                //Call the repository
                await walkrepository.UpdateAsync(id, domainmodel);

                //check if exoists
                if (domainmodel == null)
                {
                    return NotFound();
                }

                //Domain - DTO
                return Ok(mapper.Map<WalkDTO>(domainmodel));

            }
           
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var walkDomainModel = await walkrepository.DeleteAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }




    }
}
