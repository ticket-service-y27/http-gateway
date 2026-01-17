using System.ComponentModel.DataAnnotations;

namespace HttpGateway.Models.Users.Requests;

public sealed record CreateUserRequest(
    [Required] string Nickname,
    [Required][EmailAddress] string Email,
    [Required] string Password);