namespace ChatApp.Shared.Events;

/// <summary>
/// Represents the types of events that can occur in the chat application.
/// </summary>
public enum EventType
{
    
    /// <summary>
    /// An Event indicating that a message has been received
    /// </summary>
    MessageReceived,
    
    /// <summary>
    /// An Event indicating that a message has been read
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
    UserInformation,
    
    /// <summary>
    /// An event indicating that a new user joined the session
    /// </summary>
    UserJoined,
    
    /// <summary>
    /// An event indicating that a user left the session
    /// </summary>
    UserLeft,
}