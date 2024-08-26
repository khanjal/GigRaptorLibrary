﻿using GigRaptorLib.Entities;
using GigRaptorLib.Enums;
using GigRaptorLib.Utilities.Extensions;

namespace GigRaptorLib.Utilities;

public static class MessageHelper
{
    public static MessageEntity CreateMessage(MessageEntity message)
    {
        if (string.IsNullOrEmpty(message.Type))
        {
            message.Type = MessageTypeEnum.General.DisplayName();
        }

        message.Time = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
        return message;
    }
    public static MessageEntity CreateErrorMessage(string message, MessageTypeEnum type)
    {
        return CreateMessage(new MessageEntity { Message = message, Level = MessageLevelEnum.Error.UpperName(), Type = type.DisplayName() });
    }

    public static MessageEntity CreateWarningMessage(string message, MessageTypeEnum type)
    {
        return CreateMessage(new MessageEntity { Message = message, Level = MessageLevelEnum.Warning.UpperName(), Type = type.DisplayName() });
    }

    public static MessageEntity CreateInfoMessage(string message, MessageTypeEnum type)
    {
        return CreateMessage(new MessageEntity { Message = message, Level = MessageLevelEnum.Info.UpperName(), Type = type.DisplayName() });
    }
}
