//
// Copyright (c) 2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Logging;

public static class LoggerExtensions
{

    private static readonly Action<ILogger, string?, Exception?> unhandledError =
        LoggerMessage.Define<string?>(
            LogLevel.Critical,
            new EventId(1),
            "[{MemberName}] 予期しない問題が発生しました。"
        );

    public static void UnhandledError(
        this ILogger logger,
        Exception? exception,
        [CallerMemberName()] string? memberName = null
    )
    {
        unhandledError.Invoke(logger, memberName, exception);
    }

    private static readonly Action<ILogger, string?, Exception?> functionStarted =
        LoggerMessage.Define<string?>(
            LogLevel.Information,
            new EventId(1001),
            "[{MemberName}] 関数を開始しました。"
        );

    public static void FunctionStarted(this ILogger logger, [CallerMemberName()] string? memberName = null)
    {
        functionStarted.Invoke(logger, memberName, null);
    }

    private static readonly Action<ILogger, string?, Exception?> functionEnded =
        LoggerMessage.Define<string?>(
            LogLevel.Information,
            new EventId(1002),
            "[{MemberName}] 関数を終了しました。"
        );

    public static void FunctionEnded(this ILogger logger, [CallerMemberName()] string? memberName = null)
    {
        functionEnded.Invoke(logger, memberName, null);
    }

    private static readonly Action<ILogger, string?, Exception?> functionFailed =
        LoggerMessage.Define<string?>(
            LogLevel.Error,
            new EventId(1003),
            "[{MemberName}] 関数が失敗しました。"
        );

    public static void FunctionFailed(this ILogger logger, [CallerMemberName()] string? memberName = null)
    {
        functionFailed.Invoke(logger, memberName, null);
    }

}
