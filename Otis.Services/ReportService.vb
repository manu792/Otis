Imports Otis.Commons
Imports Microsoft.Office.Interop.Excel

Public Class ReportService

    Private xla As Application = Nothing
    Private workbooks As Workbooks = Nothing
    Private wb As Workbook = Nothing
    Private worksheets As Sheets = Nothing
    Private ws As Worksheet = Nothing
    Private range As Range = Nothing
    Private font As Font = Nothing

    Public Function GenerateReport(nombreExamen As String, preguntasRespuestas As IEnumerable(Of ExamenRespuestaDto), observacion As String) As String
        'Create Excel file below
        xla = New Application()

        Try
            workbooks = xla.Workbooks
            wb = workbooks.Add()

            worksheets = wb.Sheets

            ws = worksheets.Add()
            ws.Name = "Resultado"

            Dim header As Range = ws.Cells(1, 3)
            header.Value = nombreExamen
            header.HorizontalAlignment = XlHAlign.xlHAlignCenter
            font = header.Font
            font.Bold = True
            font.Size = 25

            Dim row As Integer = 0
            Dim currentRow As Range

            For Each pregunta As ExamenRespuestaDto In preguntasRespuestas
                row = row + 3
                currentRow = ws.Cells(row, 3)
                currentRow.Value = pregunta.Pregunta.PreguntaTexto
                currentRow.Font.Bold = True
                currentRow.Font.Size = 20

                If pregunta.Pregunta.Respuestas.Count <= 0 Then
                    row = row + 1
                    currentRow = ws.Cells(row, 3)
                    currentRow.Value = "R/: " & pregunta.UsuarioRespuesta
                    currentRow.Font.Size = 16
                    currentRow.HorizontalAlignment = XlHAlign.xlHAlignLeft
                End If

                For Each respuesta As RespuestaDto In pregunta.Pregunta.Respuestas
                    row = row + 1
                    currentRow = ws.Cells(row, 3)
                    currentRow.Value = respuesta.RespuestaTexto
                    currentRow.Font.Size = 16
                    If respuesta.RespuestaTexto.Equals(pregunta.UsuarioRespuesta) Then
                        currentRow.Font.Color = XlRgbColor.rgbBlue
                    End If
                Next
            Next

            ' Observacion del especialista
            row = row + 3
            currentRow = ws.Cells(row, 3)
            currentRow.Value = "Observacion del Especialista"
            currentRow.Font.Bold = True
            currentRow.Font.Size = 20

            row = row + 1
            currentRow = ws.Cells(row, 3)
            currentRow.Value = observacion
            currentRow.Font.Size = 16
            currentRow.HorizontalAlignment = XlHAlign.xlHAlignLeft
            ' -------------------------------------

            ws.Columns.AutoFit()
            xla.Visible = True

            Return "Archivo creado correctamente."
        Catch ex As Exception
            Return "Hubo un problema al tratar de crear el archivo de Excel. Favor contacte a soporte."
        Finally
            ws = Nothing
            range = Nothing
            wb = Nothing
        End Try
    End Function

End Class
