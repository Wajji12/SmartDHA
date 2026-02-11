using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR; 
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyCommands.UpdateUserFamilyCommandHandler;

using MediatR;

    public class UpdateUserFamilyCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? UserId { get; set; }  
        public string? Name { get; set; }
        public string? Relation { get; set; }
        public int Age { get; set; }
    
    }

