Imports Devart.Data.MySql
Public Class cpass
    Dim constr = "Host=localhost;port=3306;user=root;password=tiger;database=movie"
    Dim COMMAND As MySqlCommand
    Dim READER As MySqlDataReader

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim MysqlConn As New MySqlConnection(constr)
        If (TextBox1.Text = TextBox16.Text And TextBox1.Text <> "") Then
            If (Login.TextBox6.Text = "ADMIN") Then
                Try
                    MysqlConn.Open()
                    Dim Query As String
                    Query = "Update admin SET password='" & TextBox16.Text & "' WHERE username='" & Login.TextBox6.Text & "'"
                    COMMAND = New MySqlCommand(Query, MysqlConn)
                    READER = COMMAND.ExecuteReader

                    MessageBox.Show("Password Changed sucessfully")
                    MysqlConn.Close()
                    TextBox16.Text = ""
                    TextBox1.Text = ""

                Catch ex As MySqlException
                    MessageBox.Show(ex.Message)
                Finally
                    MysqlConn.Dispose()

                End Try
            Else
                Try
                    MysqlConn.Open()
                    Dim Query As String
                    Query = "Update user SET password='" & TextBox16.Text & "' WHERE username='" & Login.TextBox6.Text & "'"
                    COMMAND = New MySqlCommand(Query, MysqlConn)
                    READER = COMMAND.ExecuteReader

                    MessageBox.Show("Password Changed sucessfully")
                    MysqlConn.Close()
                    TextBox16.Text = ""
                    TextBox1.Text = ""

                Catch ex As MySqlException
                    MessageBox.Show(ex.Message)
                Finally
                    MysqlConn.Dispose()

                End Try
            End If

        Else
            MessageBox.Show("Password Mismatch")
            TextBox16.Text = ""
            TextBox1.Text = ""
        End If
    End Sub

End Class