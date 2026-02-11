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
        public Guid Id { get; set; }
        public Guid UserId { get; set; }  
        public string Name { get; set; } = string.Empty;
        public int Relation { get; set; }
        public DateTime DOB { get; set; }
    
    }

