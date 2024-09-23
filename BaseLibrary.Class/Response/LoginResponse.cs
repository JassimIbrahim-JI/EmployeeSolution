namespace BaseLibrary.Class.Response
{
    public record LoginResponse(bool Successd, string Message = null!, string Token = null!, string RefreshToken = null!);
}
