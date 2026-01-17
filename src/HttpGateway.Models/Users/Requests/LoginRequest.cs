using System.ComponentModel.DataAnnotations;

namespace HttpGateway.Models.Users.Requests;

public record LoginRequest(
    [Required] string Nickname,
    [Required] string Password);