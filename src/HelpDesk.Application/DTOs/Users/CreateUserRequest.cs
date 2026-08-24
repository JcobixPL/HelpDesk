using HelpDesk.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.Application.DTOs.Users;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    UserRole Role);
