using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Spg.Codechatter.Domain.V1.Model;
using Xunit;

namespace Spg.Codechatter.Domain.Test.V1;

public class ModelTests
{
    [Fact] 
    public void DomainModel_AddTextCahnnelToChatroom_Success_Test()
    {
        // Arrange
        
        Chatroom c2 = new Chatroom("BobTest");

        
        TextChannel tx1 = new TextChannel("General", c2.Guid);
        
        // Act
        c2.AddTextChannel(tx1);

        // Assert
        Assert.Equal(1, c2.TextChannels.Count());
    }
    
    [Fact] 
    public void DomainModel_AddTextCahnnelToChatroom_ExceptionBecauseNull_Test()
    {
        // Arrange
        
        Chatroom c2 = new Chatroom("BobTest");

        
        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => c2.AddTextChannel(null));
    }
    
    [Fact] 
    public void DomainModel_AddTextCahnnelToChatroom_ExceptionBecauseSame_Test()
    {
        // Arrange
        
        Chatroom c2 = new Chatroom("BobTest");

        
        TextChannel tx1 = new TextChannel("General", c2.Guid);
        TextChannel tx2 = new TextChannel("General", c2.Guid);
        
        // Act
        c2.AddTextChannel(tx1);
        Assert.Throws<ArgumentException>(() => c2.AddTextChannel(tx2));

        // Assert
        Assert.Equal(1, c2.TextChannels.Count());
    }
    
    [Fact] 
    public void DomainModel_RemoveTextCahnnelFromChatroom_Success_Test()
    {
        // Arrange
        
        Chatroom c2 = new Chatroom("BobTest");

        
        TextChannel tx1 = new TextChannel("General", c2.Guid);
        c2.AddTextChannel(tx1);
        // Act
        c2.RemoveTextChannel(tx1);

        // Assert
        Assert.Equal(0, c2.TextChannels.Count());
    }
    
    [Fact] 
    public void DomainModel_RemoveTextCahnnelFromChatroom_ExceptionBecauseNull_Test()
    {
        // Arrange
        
        Chatroom c2 = new Chatroom("BobTest");

        
        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => c2.RemoveTextChannel(null));
    }
    
    [Fact] 
    public void DomainModel_RemoveTextCahnnelFromChatroom_ExceptionBecauseNotFound_Test()
    {
        // Arrange
        
        Chatroom c2 = new Chatroom("BobTest");

        
        TextChannel tx1 = new TextChannel("General", c2.Guid);
        TextChannel tx2 = new TextChannel("Bob", c2.Guid);
        c2.AddTextChannel(tx1);
        
        // Act
        Assert.Throws<ArgumentException>(() => c2.RemoveTextChannel(tx2));

        // Assert
        Assert.Equal(1, c2.TextChannels.Count());
    }

    [Fact]
    public void DomainModel_AddMessageToTextChannel_Success_Test()
    {
        // Arrange

        Chatroom c2 = new Chatroom("BobTest");

        User u4 = new User(c2.Guid, "franco@hotmail.com", "Logoa", "12345");

        TextChannel tx5 = new TextChannel("General", c2.Guid);
        c2.AddTextChannel(tx5);
        
        // Act
        Message m4 = new Message("test", u4.Guid, c2.Guid, tx5.Guid);
        tx5.AddMessage(m4);

        // Assert
        Assert.Equal(1, tx5.Messages.Count());
    }
    
    [Fact] 
    public void DomainModel_AddMessageToTextChannel_ExceptionBecauseNull_Test()
    {
        // Arrange
        
        
        Chatroom c2 = new Chatroom("BobTest");

        TextChannel tx5 = new TextChannel("General", c2.Guid);
        c2.AddTextChannel(tx5);
        
        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => tx5.AddMessage(null));
    }
    
    [Fact]
    public void DomainModel_RemoveMessageFromTextChannel_Success_Test()
    {
        // Arrange

        Chatroom c2 = new Chatroom("BobTest");

        User u4 = new User(c2.Guid, "franco@hotmail.com", "Droei", "12345");

        TextChannel tx5 = new TextChannel("General", c2.Guid);
        c2.AddTextChannel(tx5);
        
        Message m4 = new Message("test", u4.Guid, c2.Guid, tx5.Guid);
        tx5.AddMessage(m4);

        // Act
        tx5.RemoveMessage(m4);

        // Assert
        Assert.Equal(0, tx5.Messages.Count());
    }

    
    [Fact] 
    public void DomainModel_RemoveMessageFromTextChannel_ExceptionBecauseNull_Test()
    {
        // Arrange
        
        
        Chatroom c2 = new Chatroom("BobTest");

        TextChannel tx5 = new TextChannel("General", c2.Guid);
        c2.AddTextChannel(tx5);
        
        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => tx5.RemoveMessage(null));
    }
}