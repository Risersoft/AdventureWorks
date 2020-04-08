Imports System
Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.Bot.Builder
Imports Microsoft.Bot.Builder.Dialogs
Imports Microsoft.Bot.Schema
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.Logging
Imports risersoft.[shared]
Imports risersoft.[shared].bot
Imports risersoft.[shared].portable.Models.Auth
Imports System.Linq
Imports System.Reflection

<BotId(BotName:="KakaBot", CallerInfo:=True)>
Public Class KakaBot
    Inherits ActivityBotBase

    Private _dialogs As DialogSet
    Private _accessor As IStatePropertyAccessor(Of DialogState)
    Public Sub New(ByVal configuration As IConfiguration, ByVal conversationState As ConversationState, ByVal userState As UserState, ByVal logger As ILogger(Of KakaBot))
        MyBase.New(conversationState, userState, logger)
        _accessor = conversationState.CreateProperty(Of DialogState)(NameOf(DialogState))
        _dialogs = New DialogSet(_accessor)
        Dim lst As New List(Of ComponentDialog)
        lst.Add(New RSAuthDialog(configuration, logger))
        lst.Add(New RSGroupDialog(configuration, logger))
        lst.Add(New TodoDialog(configuration, logger))
        lst.ForEach(Sub(x) _dialogs.Add(x))
    End Sub

    Public Overrides Sub Initialize(turnContext As ITurnContext)
        MyBase.Initialize(turnContext)
        Dim attrib = Me.GetType.GetCustomAttribute(Of BotId)
        GreetMessage = $"Welcome to **{attrib.BotName} chat bot from {Me.myWebController.CalcPublisher}**." & vbLf & vbLf & "Please enter /register or /todo."
    End Sub
    Protected Overrides Async Function OnMembersAddedAsync(ByVal membersAdded As IList(Of ChannelAccount), ByVal turnContext As ITurnContext(Of IConversationUpdateActivity), ByVal cancellationToken As CancellationToken) As Task

        For Each member In turnContext.Activity.MembersAdded
            If member.Id <> turnContext.Activity.Recipient.Id Then
                Await turnContext.SendActivityAsync(MessageFactory.Text(GreetMessage), cancellationToken)
            End If
        Next
    End Function

    Protected Overrides Async Function OnTokenResponseEventAsync(ByVal turnContext As ITurnContext(Of IEventActivity), ByVal cancellationToken As CancellationToken) As Task
        Logger.LogInformation("Running dialog with Token Response Event Activity.")
        Await _dialogs.Run(String.Empty, turnContext, cancellationToken)
    End Function

    Protected Overrides Async Function OnMessageActivityAsync(ByVal turnContext As ITurnContext(Of IMessageActivity), ByVal cancellationToken As CancellationToken) As Task
        Dim bot = turnContext.TurnState.Get(Of ActivityBotBase)("bot")
        Dim attrib = bot.GetType.GetCustomAttribute(Of BotId)

        Dim convProp = Await _accessor.GetAsync(turnContext, Function() New DialogState(), cancellationToken)

        If convProp.DialogStack.Count = 0 Then
            Dim text = turnContext.Activity.Text.ToLowerInvariant()

            If myUtils.StartsWith(text, "/register") Then

                If turnContext.Activity.Conversation.IsGroup.GetValueOrDefault() Then
                    Await _dialogs.Run("rsgroup", turnContext, cancellationToken)
                Else
                    Await _dialogs.Run("rsauth", turnContext, cancellationToken)
                End If
            ElseIf myUtils.StartsWith(text, "/todo") Then
                Await _dialogs.Run("todo", turnContext, cancellationToken)
            ElseIf myUtils.StartsWith(text, "/start") Then
                Await turnContext.SendActivityAsync(MessageFactory.Text(GreetMessage), cancellationToken)
            ElseIf myUtils.StartsWith(text, "/") Then
                Await turnContext.SendActivityAsync(MessageFactory.Text($"Do not recognize that command."), cancellationToken)
            End If
        Else
            Logger.LogInformation("Running dialog with Message Activity.")
            Await _dialogs.Run(String.Empty, turnContext, cancellationToken)
        End If
    End Function
End Class

