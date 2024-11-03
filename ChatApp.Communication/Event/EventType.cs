namespace ChatApp.Communication.Event;

/// <summary>
/// Represents the types of events that can occur in the chat application.
/// </summary>
public enum EventType
{
    
    /// <summary>
    /// An Event indicating a message has been received
    /// </summary>
    MessageReceived,
    
    /// <summary>
    /// An Event indicating a message has been read
    /// </summary>
    MessageRead,
    
    /// <summary>
    /// An event containing a message
    /// </summary>
    SendMessage,
    
    /// <summary>
    /// An event requesting user information
    /// </summary>
    UserInformationRequest,
    
    /// <summary>
    /// An Event containing user information
    /// </summary>
    UserInformationResponse,
}