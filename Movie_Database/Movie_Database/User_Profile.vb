Imports Devart.Data.MySql
Imports System.Text.RegularExpressions

Public Class User_Profile
    Dim constr = "Host=localhost;port=3306;user=root;password=tiger;database=movie"
    Dim COMMAND As MySqlCommand
    Dim READER As MySqlDataReader


    Private Function IsPhoneNumberValid(ByVal Number As String) As Boolean

        Dim PhoneValid As Boolean
        Dim PhoneNumber As String = "^[0-9]\d{9}$"
        Dim ChekPhone As New Regex(PhoneNumber)
        If Not String.IsNullOrEmpty(Number) Then

            PhoneValid = ChekPhone.IsMatch(Number)
        Else
            PhoneValid = False

        End If

        Return PhoneValid
    End Function
    Private Function IsEmailValid(ByVal email As String) As Boolean

        Dim EmailValid As Boolean
        Dim emailid As String = "^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$"
        Dim Checkemail As New Regex(emailid)
        If Not String.IsNullOrEmpty(email) Then

            EmailValid = Checkemail.IsMatch(email)
        Else
            EmailValid = False

        End If
        Return EmailValid
    End Function

    Private Sub User_Profile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim MysqlConn As New MySqlConnection(constr)
        Try
            MysqlConn.Open()
            Dim query As String
            query = "Select * from user where username='" & Login.TextBox6.Text & "'"
            COMMAND = New MySqlCommand(query, MysqlConn)
            READER = COMMAND.ExecuteReader
            While READER.Read
                Dim name, id, mob, mail As String
                name = READER.GetString("username")
                id = READER.GetString("user_id")
                mob = READER.GetString("mobile")
                mail = READER.GetString("email")
                TextBox3.Text = name
                TextBox1.Text = id
                TextBox4.Text = mob
                TextBox2.Text = mail
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()

        End Try
    End Sub






    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
        User_Home.Show()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If IsEmailValid(TextBox2.Text) Then
            If IsPhoneNumberValid(TextBox4.Text) Then
                Dim MysqlConn As New MySqlConnection(constr)
                If (TextBox4.Text <> "" And TextBox2.Text <> "") Then
                    Try
                        MysqlConn.Open()
                        Dim Query As String
                        Query = "Update user SET mobile='" & TextBox4.Text & "', email ='" & TextBox2.Text & "' WHERE username='" & TextBox3.Text & "'"
                        COMMAND = New MySqlCommand(Query, MysqlConn)
                        READER = COMMAND.ExecuteReader

                        MessageBox.Show("Profile Updated sucessfully")
                        MysqlConn.Close()
                    Catch ex As MySqlException
                        MessageBox.Show(ex.Message)
                    Finally
                        MysqlConn.Dispose()

                    End Try
                Else
                    MessageBox.Show("Something Went Wrong Please try Again Later")
                End If
            Else
                MessageBox.Show("Please Enter Valid Mobile Number!")
                TextBox4.Clear()
                TextBox4.Focus()
            End If
        Else
            MessageBox.Show("Please Enter Valid Email ID!")
            TextBox2.Clear()
            TextBox2.Focus()
        End If
    End Sub
End Class