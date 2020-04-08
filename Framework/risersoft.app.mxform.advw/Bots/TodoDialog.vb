Imports System.Reflection
Imports System.Threading
Imports AdaptiveCards
Imports Bot.Builder.Community.Dialogs.FormFlow
Imports Microsoft.Bot.Builder
Imports Microsoft.Bot.Builder.Dialogs
Imports Microsoft.Bot.Schema
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.Logging
Imports Newtonsoft.Json.Linq
Imports risersoft.shared
Imports risersoft.shared.bot
Imports risersoft.shared.web

Public Class TodoDialog
    Inherits ComponentDialog
    Protected ReadOnly _logger As ILogger
    Private Const ErrorMessage As String = "Not a valid option"
    Public Sub New(ByVal configuration As IConfiguration, ByVal logger As ILogger)
        MyBase.New("todo")
        _logger = logger
        AddDialog(New TextPrompt(NameOf(TextPrompt)))
        AddDialog(New WaterfallDialog(NameOf(WaterfallDialog),
        New WaterfallStep() {AddressOf PromptStepAsync, AddressOf ProcessStepAsync, AddressOf BeginFormflowAsync, AddressOf SaveResultAsync}))
        AddDialog(FormDialog.FromForm(AddressOf BuildLocalAdminForm, FormOptions.PromptInStart))
        InitialDialogId = NameOf(WaterfallDialog)
    End Sub
    Public Const CardImageUrl As String = "data:image/png;base64,{0}"
    Protected Friend Async Function PromptStepAsync(stepContext As WaterfallStepContext, cancellationToken As CancellationToken) As Task(Of DialogTurnResult)

        Dim bot = stepContext.Context.TurnState.Get(Of ActivityBotBase)("bot")
        If bot.Chat Is Nothing Then
            Await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Please register first, 
                        conversationid = '{stepContext.Context.Activity.Conversation.Id}'"), cancellationToken)
            Return Await stepContext.EndDialogAsync(cancellationToken:=cancellationToken)
        Else

            Dim lst = New List(Of String)(stepContext.Context.Activity.Text.ToLowerInvariant().Split(" "))
            If Await bot.myWebController.CheckInitModel(bot.myWebController.AppInfo, True) Then
                Dim attrib = bot.GetType.GetCustomAttribute(Of BotId)
                Dim lstMention = bot.GetMentions(stepContext.Context)
                If myUtils.IsInList("list", lst.ToArray) Then
                    Dim sql = $"select * from prjListPIDUnitWorkItem() where conversationid = '{bot.Chat.ConversationId}' and completedon is null"
                    Dim dt1 = bot.myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                    For Each r1 As DataRow In dt1.Select
                        Dim obj = myAssy.bytFromEmbed(Assembly.GetExecutingAssembly().GetName().Name, "BotPic.png", True)
                        Dim str1 = Convert.ToBase64String(obj)
                        Await stepContext.Context.SendActivityAsync(MessageFactory.Text(
                            $"TODO **{r1("title")}** for user **{r1("fullname")}**." & vbLf & vbLf & $"{r1("content")}"), cancellationToken)
                        'Dim heroCard = New ThumbnailCard(r1("title"), attrib.BotName, r1("content")) With {
                        '    .Buttons = New List(Of CardAction) From {
                        '        New CardAction(ActionTypes.ImBack, "Edit", text:="Edit", displayText:="Edit", value:="Edit"),
                        '        New CardAction(ActionTypes.ImBack, "Finish", text:="Finish", displayText:="Finish", value:="Finish"),
                        '        New CardAction(ActionTypes.ImBack, "Delete", text:="Delete", displayText:="Delete", value:="Delete")
                        '    },
                        '    .Images = New List(Of CardImage)() From {
                        '        New CardImage(String.Format(CardImageUrl, str1))
                        '        }
                        '    }

                        'Dim reply = MessageFactory.Attachment(New List(Of Attachment))
                        'reply.Attachments.Add(heroCard.ToAttachment())
                        'Await stepContext.Context.SendActivityAsync(reply, cancellationToken:=cancellationToken)
                    Next
                    Return Await stepContext.EndDialogAsync(cancellationToken:=cancellationToken)
                ElseIf lst.Count > 1 AndAlso myUtils.IsInList("create", lst(1)) Then
                    Return Await stepContext.NextAsync(cancellationToken:=cancellationToken)
                ElseIf lstMention.Count > 0 Then
                    Dim oRet As New clsProcOutput
                    Try
                        Dim sql = $"select pidunitid from pidunit where conversationid = '{bot.Chat.ConversationId}'"
                        Dim dt1 = bot.myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                        If dt1.Rows.Count > 0 Then
                            Dim info As New TodoPromptInfo
                            Dim lst2 = lst.Skip(1).ToList
                            Dim str1 = myUtils.MakeCSV(lst2, " ")
                            For Each obj In lstMention
                                Dim info2 = bot.GenerateBotFxInfo(stepContext.Context, obj.Mentioned)
                                Dim chat = ServiceAuthProvider.GenerateBotChatInfo(bot.Host, attrib.BotName, info2)
                                str1 = myUtils.Replace(str1, obj.Text, "").Trim
                                If chat Is Nothing Then
                                    Await stepContext.Context.SendActivityAsync($"Cannot find {obj.Text} for {obj.Mentioned.Id}.", cancellationToken:=cancellationToken)
                                    Return Await stepContext.EndDialogAsync(cancellationToken:=cancellationToken)
                                Else
                                    info.AssignUsers.Add(chat.User)
                                End If
                            Next
                            If IsDate(lst.Last) Then
                                info.DueOn = CDate(lst.Last)
                                str1 = str1.Replace(lst.Last, "")
                            Else
                                Await stepContext.Context.SendActivityAsync($"{lst.Last} is not a valid date.", cancellationToken:=cancellationToken)
                                Return Await stepContext.EndDialogAsync(cancellationToken:=cancellationToken)
                            End If
                            info.Title = str1
                            info.PIDUnitID = dt1.Rows(0)("pidunitid")
                            Dim model As New frmWorkItemModel(bot.myWebController)
                            oRet = model.SaveTodoInfo(info)
                        Else
                            Await stepContext.Context.SendActivityAsync("This conversation is not associated with any project. Use /Register to do so", cancellationToken:=cancellationToken)
                        End If

                    Catch ex As Exception
                        oRet.AddException(ex)
                    End Try
                    If oRet.Success Then
                        Await stepContext.Context.SendActivityAsync("Wow. Todo created.", cancellationToken:=cancellationToken)
                    ElseIf oRet.ErrorCount > 0 Then
                        Await stepContext.Context.SendActivityAsync("Could not create: " & oRet.Message & vbCrLf & oRet.StackTrace, cancellationToken:=cancellationToken)
                    End If
                    Return Await stepContext.EndDialogAsync(cancellationToken:=cancellationToken)
                Else
                    Await stepContext.Context.SendActivityAsync("Unrecognized command.", cancellationToken:=cancellationToken)
                    Await stepContext.Context.SendActivityAsync(bot.GreetMessage, cancellationToken:=cancellationToken)
                    Return Await stepContext.EndDialogAsync(cancellationToken:=cancellationToken)
                End If
            Else
                Await stepContext.Context.SendActivityAsync("Cannot Init.", cancellationToken:=cancellationToken)
                Return Await stepContext.EndDialogAsync(cancellationToken:=cancellationToken)
            End If


        End If
    End Function
    Private Async Function ProcessStepAsync(ByVal stepContext As WaterfallStepContext, ByVal cancellationToken As CancellationToken) As Task(Of DialogTurnResult)
        If stepContext.Result IsNot Nothing Then
            Dim GSTIN As String = myUtils.cStrTN(stepContext.Result)
            Dim bot = stepContext.Context.TurnState.Get(Of ActivityBotBase)("bot")
            If Await bot.myWebController.CheckInitModel(bot.myWebController.AppInfo, False) Then
                Dim card = New AdaptiveCard(New AdaptiveSchemaVersion(1, 0))
                card.Body.Add(New AdaptiveTextBlock() With {.Text = "", .Size = AdaptiveTextSize.Large})
                card.Body.Add(New AdaptiveTextBlock() With {.Text = "", .Size = AdaptiveTextSize.Medium})
                card.Body.Add(New AdaptiveTextBlock() With {.Text = "", .Size = AdaptiveTextSize.Small})
                card.Body.Add(New AdaptiveTextBlock() With {.Text = "", .Size = AdaptiveTextSize.Small})
                card.Body.Add(New AdaptiveTextBlock() With {.Text = myUtils.MakeCSV("", vbCrLf), .Size = AdaptiveTextSize.Small})
                card.Body.Add(New AdaptiveImage() With {.Url = New Uri("http://www.risersoft.com/Content/images/mpc.png"), .Size = AdaptiveImageSize.Medium})
                Await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(New Attachment With
                    {.ContentType = AdaptiveCard.ContentType, .Content = JObject.FromObject(card)}), cancellationToken:=cancellationToken)
            Else
                Await stepContext.Context.SendActivityAsync("Cannot Init.", cancellationToken:=cancellationToken)
            End If
        Else
            Await stepContext.Context.SendActivityAsync(MessageFactory.Text("We couldn't log you in. Please try again later."), cancellationToken)
        End If
        Return Await stepContext.EndDialogAsync(cancellationToken:=cancellationToken)

    End Function
    Private Async Function BeginFormflowAsync(ByVal stepContext As WaterfallStepContext, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of DialogTurnResult)
        Await stepContext.Context.SendActivityAsync("Great I will help you request local machine admin.")
        Return Await stepContext.BeginDialogAsync(NameOf(TodoPromptInfo), cancellationToken:=cancellationToken)
    End Function

    Private Async Function SaveResultAsync(ByVal stepContext As WaterfallStepContext, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of DialogTurnResult)
        If stepContext.Reason <> DialogReason.CancelCalled Then
            Dim admin = TryCast(stepContext.Result, TodoPromptInfo)
        End If

        Return Await stepContext.EndDialogAsync(Nothing, cancellationToken)
    End Function

    Private Function BuildLocalAdminForm() As IForm(Of TodoPromptInfo)
        Return New FormBuilder(Of TodoPromptInfo)().Field(
                      NameOf(TodoPromptInfo.Title),
                      validate:=Async Function(state, value)
                                    Dim result = New ValidateResult With {
                                    .IsValid = True,
                                    .Value = value
                                }
                                    Return result
                                End Function).Field(
                      NameOf(TodoPromptInfo.DueOn),
                      validate:=Async Function(state, value)
                                    Dim result = New ValidateResult With {
                                                                                                                                                                                              .IsValid = True,
                                                                                                                                                                                              .Value = value
                                                                                                                                                                                          }
                                    Return result
                                End Function).Build()
    End Function

End Class
