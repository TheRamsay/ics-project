using CommunityToolkit.Mvvm.Messaging;

namespace KlidecekIS.Services.Interfaces;

public interface IMessengerService
{
    IMessenger Messenger { get; }

    void Send<TMessage>(TMessage message)
        where TMessage : class; 
}