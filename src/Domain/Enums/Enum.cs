using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Domain.Enums;
public enum CategoryType
{
    Resident = 1,
    Commercial = 2,
}
public enum Zone
{
    ZoneA = 1,
    ZoneB = 2,
    ZoneC = 3,
    ZoneD = 4,
    ZoneE = 5,
}
public enum Relation
{
    Father = 1,
    Mother = 2,
    Son = 3,
    Daughter = 4,
    Brother = 5,
    Sister = 6,
    Spouse = 7,
}
public enum Phase
{
    Phase1 = 1,
    Phase2 = 2,
    Phase3 = 3,
    Phase4 = 4,
    Phase5 = 5,
    Phase6 = 6,
    Phase7 = 7,
    Phase8 = 8,
}
public enum PropertyType
{
    Bunglow = 1,
    Flat = 2,
    Shop = 3,
    Office = 4,
    Portion = 5,
}
public enum PossessionType
{
    Owner = 1,
    Tenant = 2
}
public enum PriorityLevel
{
    None = 0,
    Low = 1,
    Medium = 2,
    High = 3
}
public enum ResidenceStatus
{
    Owner = 1,
    Tenant = 2,
    Vacant = 3  
}
