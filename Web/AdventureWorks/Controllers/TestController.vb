
Imports Syncfusion.JavaScript
Imports System.Collections.Generic
Imports System
Imports Syncfusion.EJ
Imports Syncfusion.EJ.Export
Imports Syncfusion.MVC.EJ







Namespace Controllers

    Public Class TestController
        Inherits Controller


        Public Class DefaultData
            Public Property StartDate As String
            Public Property EndDate As String
            Public Property TaskID As Integer
            Public Property TaskName As String
            Public Property Duration As Integer
            Public Property Progress As Integer
            Public Property SubTasks As List(Of DefaultData)
            Public Property Predecessors As String
            Public Property ResourceID As List(Of Integer)
        End Class

        Public Class GanttDefaultData
            Public Shared Function GetData() As List(Of DefaultData)
                Dim tasks As List(Of DefaultData) = New List(Of DefaultData)()
                tasks.Add(New DefaultData() With {
                .TaskID = 1,
                .TaskName = "Design",
                .StartDate = "02/10/2014",
                .EndDate = "02/14/2014",
                .Duration = 6
            })
                tasks(0).SubTasks = New List(Of DefaultData)()
                tasks(0).SubTasks.Add(New DefaultData() With {
                .TaskID = 2,
                .TaskName = "Software Specification",
                .StartDate = "02/10/2014",
                .EndDate = "02/12/2014",
                .Duration = 3,
                .Progress = 60,
                .ResourceID = New List(Of Integer)() From {
                    2
                }
            })
                tasks(0).SubTasks.Add(New DefaultData() With {
                .TaskID = 3,
                .TaskName = "Develop prototype",
                .StartDate = "02/10/2014",
                .EndDate = "02/12/2014",
                .Duration = 3,
                .Progress = 100,
                .Predecessors = "2FS",
                .ResourceID = New List(Of Integer)() From {
                    3
                }
            })
                tasks(0).SubTasks.Add(New DefaultData() With {
                .TaskID = 4,
                .TaskName = "Get approval from customer",
                .StartDate = "02/13/2014",
                .EndDate = "02/14/2014",
                .Duration = 2,
                .Progress = 100,
                .Predecessors = "3FS",
                .ResourceID = New List(Of Integer)() From {
                    1
                }
            })
                tasks(0).SubTasks.Add(New DefaultData() With {
                .TaskID = 5,
                .TaskName = "Design complete",
                .StartDate = "02/14/2014",
                .EndDate = "02/14/2014",
                .Duration = 0,
                .Predecessors = "4FS"
            })
                Return tasks
            End Function
        End Class









        Public Function Index() As ActionResult
            Dim DataSource = GanttDefaultData.GetData()
            ViewBag.datasource = DataSource
            Dim Resources = ResourceList.GetData()
            ViewBag.resources = Resources
            Return View()
        End Function

    End Class
    Public Class Resources
        Public Property ResourceID As Integer
        Public Property ResourceName As String
    End Class
    Public Class ResourceList
        Public Shared Function GetData() As List(Of Resources)
            Dim resourceDetails As List(Of Resources) = New List(Of Resources)()
            resourceDetails.Add(New Resources() With {
                    .ResourceID = 1,
                    .ResourceName = "Project Manager"
                })
            resourceDetails.Add(New Resources() With {
                    .ResourceID = 2,
                    .ResourceName = "Software Analyst"
                })
            resourceDetails.Add(New Resources() With {
                    .ResourceID = 3,
                    .ResourceName = "Developer"
                })
            resourceDetails.Add(New Resources() With {
                    .ResourceID = 4,
                    .ResourceName = "Testing Engineer"
                })
            Return resourceDetails
        End Function
    End Class






End Namespace