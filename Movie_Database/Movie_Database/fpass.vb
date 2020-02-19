Imports Devart.Data.MySql
Public Class fpass
    Dim constr = "Host=localhost;port=3306;user=root;password=tiger;database=movie"
    Dim COMMAND As MySqlCommand
    Dim READER As MySqlDataReader
    Private Sub fpass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim MysqlConn As New MySqlConnection(constr)
        Try
            MysqlConn.Open()
            Dim query, name As String
            name = "ADMIN"
            query = "Select * from admin where username='" & name & "'"
            COMMAND = New MySqlCommand(query, MysqlConn)
            READER = COMMAND.ExecuteReader
            While READER.Read
                Dim aname, mob, mail As String
                aname = READER.GetString("username")
                mob = READER.GetString("mobile")
                mail = READER.GetString("email")
                Label3.Text = "To Reset Your Password Please Contact Admin" + vbCrLf + "Mobile:" + mob + vbCrLf + "Email:" + mail
            End While

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()

        End Try

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Me.Close()
    End Sub
End Class