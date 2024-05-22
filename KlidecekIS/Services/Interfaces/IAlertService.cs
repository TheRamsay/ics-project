namespace KlidecekIS.Services.Interfaces;

public interface IAlertService
{
    Task DisplayAsync(string title, string message);
}