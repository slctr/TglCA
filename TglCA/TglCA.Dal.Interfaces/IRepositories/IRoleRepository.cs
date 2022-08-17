﻿using Microsoft.AspNetCore.Identity;
using TglCA.Dal.Interfaces.Entities.Identity;

namespace TglCA.Dal.Interfaces.IRepositories;

public interface IRoleRepository : IRoleStore<Role>
{
    
}