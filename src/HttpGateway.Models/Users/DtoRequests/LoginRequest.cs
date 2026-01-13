using System.ComponentModel.DataAnnotations;

namespace HttpGateway.Models.Users.DtoRequests;

public record LoginRequest(
    [Required] string Nickname,
    [Required] string Password);