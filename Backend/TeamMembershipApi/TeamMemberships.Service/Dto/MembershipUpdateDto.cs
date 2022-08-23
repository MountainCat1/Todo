﻿using TeamMemberships.Domain.Enums;

namespace TeamMemberships.Service.Dto;

public class MembershipUpdateDto
{
    public Guid AccountGuid { get; set; }
    public Guid TeamGuid { get; set; }
    public UserRole Role { get; set; }
}