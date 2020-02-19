Imports Devart.Data.MySql
Public Class Login
    Dim constr = "Host=localhost;port=3306;user=root;password=tiger;database=movie"
    Dim COMMAND As MySqlCommand
    Dim READER As MySqlDataReader

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim MysqlConn As New MySqlConnection(constr)
        Dim READER As MySqlDataReader



        If (TextBox6.Text = "ADMIN") Then
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "select * from admin where username='" & TextBox6.Text & "' and password='" & TextBox5.Text & "' "
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                Dim count As Integer
                count = 0
                While READER.Read
                    count = count + 1
                End While
                If count = 1 Then
                    MessageBox.Show("Username and password are correct")
                    MysqlConn.Dispose()
                    TextBox5.Text = ""
                    Me.Hide()
                    Admin_Home.Show()
                ElseIf count > 1 Then
                    MessageBox.Show("Username and password are Duplicated")
                Else
                    MessageBox.Show("Username and password are not correct")
                End If
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            Finally
                MysqlConn.Dispose()
            End Try
        Else
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "select * from user where username='" & TextBox6.Text & "' and password='" & TextBox5.Text & "' "
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                Dim count As Integer
                count = 0
                While READER.Read
                    count = count + 1
                End While
                If count = 1 Then
                    MessageBox.Show("Username and password are correct")
                    MysqlConn.Dispose()
                    TextBox5.Text = ""
                    Me.Hide()
                    User_Home.Show()
                ElseIf count > 1 Then
                    MessageBox.Show("Username and password are Duplicated")
                Else
                    MessageBox.Show("Username and password are not correct")
                End If
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            Finally
                MysqlConn.Dispose()
            End Try
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        fpass.Show()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Application.Exit()
    End Sub

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        Button1.Visible = False
        Button9.Enabled = False
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        TextBox5.Enabled = True
        TextBox6.Enabled = False
        TextBox6.Text = "ADMIN"
        Button1.Hide()
        Button9.Enabled = True
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        TextBox6.Text = ""
        Button1.Visible = True
        Button9.Enabled = True
    End Sub
End Class
