//
// Copyright (c) 2024-2025 karamem0
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

    private static readonly Action<ILogger, string?, Exception?> functionExecuting = LoggerMessage.Define<string?>(
        LogLevel.Information,
        new EventId(1001),
        "[{MemberName}] 関数を実行しています。"
    );

    public static void FunctionExecuting(
        this ILogger logger,
        [CallerMemberName()] string? memberName = null,
        Exception? exception = null
    )
    {
        functionExecuting.Invoke(
            logger,
            memberName,
            exception
        );
    }

    private static readonly Action<ILogger, string?, Exception?> functionExecuted = LoggerMessage.Define<string?>(
        LogLevel.Information,
        new EventId(1002),
        "[{MemberName}] 関数を実行しました。"
    );

    public static void FunctionExecuted(
        this ILogger logger,
        [CallerMemberName()] string? memberName = null,
        Exception? exception = null
    )
    {
        functionExecuted.Invoke(
            logger,
            memberName,
            exception
        );
    }

    private static readonly Action<ILogger, string?, object?, Exception?> functionRequestData = LoggerMessage.Define<string?, object?>(
        LogLevel.Debug,
        new EventId(2001),
        "[{MemberName}] 要求データ: {RequestData}"
    );

    public static void FunctionRequestData(
        this ILogger logger,
        [CallerMemberName()] string? memberName = null,
        object? requestData = null,
        Exception? exception = null
    )
    {
        functionRequestData.Invoke(
            logger,
            memberName,
            requestData,
            exception
        );
    }

    private static readonly Action<ILogger, string?, object?, Exception?> functionResponseData = LoggerMessage.Define<string?, object?>(
        LogLevel.Debug,
        new EventId(2002),
        "[{MemberName}] 応答データ: {ResponseData}"
    );

    public static void FunctionResponseData(
        this ILogger logger,
        [CallerMemberName()] string? memberName = null,
        object? responseData = null,
        Exception? exception = null
    )
    {
        functionResponseData.Invoke(
            logger,
            memberName,
            responseData,
            exception
        );
    }

    private static readonly Action<ILogger, string?, Exception?> functionFailed = LoggerMessage.Define<string?>(
        LogLevel.Error,
        new EventId(8001),
        "[{MemberName}] 関数の実行に失敗しました。"
    );

    public static void FunctionFailed(
        this ILogger logger,
        [CallerMemberName()] string? memberName = null,
        Exception? exception = null
    )
    {
        functionFailed.Invoke(
            logger,
            memberName,
            exception
        );
    }

    private static readonly Action<ILogger, string?, Exception?> unhandledErrorOccurred = LoggerMessage.Define<string?>(
        LogLevel.Error,
        new EventId(9001),
        "[{MemberName}] 予期しない問題が発生しました。"
    );

    public static void UnhandledErrorOccurred(
        this ILogger logger,
        [CallerMemberName()] string? memberName = null,
        Exception? exception = null
    )
    {
        unhandledErrorOccurred.Invoke(
            logger,
            memberName,
            exception
        );
    }

}
