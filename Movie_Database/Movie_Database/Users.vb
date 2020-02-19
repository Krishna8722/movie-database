Imports Devart.Data.MySql
Imports System.Text.RegularExpressions

Public Class Users
    Dim constr = "Host=localhost;port=3306;user=root;password=tiger;database=movie"
    Dim COMMAND As MySqlCommand
    Dim READER As MySqlDataReader
    Private Sub Users_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Loadusers()
    End Sub
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
    Private Sub Loadusers()
        Dim MysqlConn As New MySqlConnection(constr)
        Try
            Dim table As New DataTable()
            Dim adapter As New MySqlDataAdapter("SELECT user_id,username,mobile,email FROM user", MysqlConn)
            adapter.Fill(table)
            DataGridView3.DataSource = table
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
        Admin_Home.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If IsEmailValid(TextBox2.Text) Then
            If IsPhoneNumberValid(TextBox3.Text) Then
                If (TextBox1.Text <> "" And TextBox16.Text <> "") Then

                    Dim MysqlConn As New MySqlConnection(constr)
                    Try
                        MysqlConn.Open()
                        Dim Query As String
                        Query = "insert into user values ('" & TextBox1.Text & "','" & TextBox16.Text & "','" & TextBox3.Text & "','" & TextBox2.Text & "','" & TextBox4.Text & "')"
                        COMMAND = New MySqlCommand(Query, MysqlConn)
                        READER = COMMAND.ExecuteReader
                        MessageBox.Show("User Saved sucessfully")
                        MysqlConn.Close()
                        Loadusers()
                        TextBox1.Text = ""
                        TextBox16.Text = ""
                        TextBox3.Text = ""
                        TextBox2.Text = ""
                    Catch ex As MySqlException
                        MessageBox.Show("Something Went Wrong Please Check the Details And Check Again Later!")
                        TextBox1.Text = ""
                        TextBox16.Text = ""
                        TextBox3.Text = ""
                        TextBox2.Text = ""
                    Finally
                        MysqlConn.Dispose()
                    End Try
                Else
                    MessageBox.Show("Please Enter All the Required Details")
                    TextBox1.Text = ""
                    TextBox16.Text = ""
                    TextBox3.Text = ""
                    TextBox2.Text = ""
                End If
            Else
                MessageBox.Show("Please Enter Valid Mobile Number!")
                TextBox3.Clear()
                TextBox3.Focus()
            End If
        Else
            MessageBox.Show("Please Enter Valid Email ID!")
            TextBox2.Clear()
            TextBox2.Focus()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If IsEmailValid(TextBox2.Text) Then
            If IsPhoneNumberValid(TextBox3.Text) Then
                If (TextBox1.Text <> "" And TextBox16.Text <> "") Then

                    Dim MysqlConn As New MySqlConnection(constr)
                    Try
                        MysqlConn.Open()
                        Dim Query As String
                        Query = "SELECT * FROM user WHERE user_id ='" & TextBox1.Text & "'"
                        COMMAND = New MySqlCommand(Query, MysqlConn)
                        READER = COMMAND.ExecuteReader
                        Dim count As Integer
                        count = 0
                        While READER.Read
                            count = count + 1
                        End While
                        If (count = 1) Then

                            Query = "Update user SET username='" & TextBox16.Text & "',mobile='" & TextBox3.Text & "',email='" & TextBox2.Text & "',password='" & TextBox4.Text & "' WHERE user_id ='" & TextBox1.Text & "'"
                            COMMAND = New MySqlCommand(Query, MysqlConn)
                            READER = COMMAND.ExecuteReader
                            MessageBox.Show("User Updated sucessfully")
                            MysqlConn.Close()
                            Loadusers()
                            TextBox1.Text = ""
                            TextBox16.Text = ""
                            TextBox3.Text = ""
                            TextBox2.Text = ""
                        Else
                            MessageBox.Show("Record Not Found")
                        End If
                    Catch ex As MySqlException
                        MessageBox.Show("Something Went Wrong Please Check the Details And Check Again Later!")
                        TextBox1.Text = ""
                        TextBox16.Text = ""
                        TextBox3.Text = ""
                        TextBox2.Text = ""
                    Finally
                        MysqlConn.Dispose()
                    End Try
                Else
                    MessageBox.Show("Please Enter All the Required Details")
                    TextBox1.Text = ""
                    TextBox3.Text = ""
                    TextBox16.Text = ""
                    TextBox2.Text = ""
                End If
            Else
                MessageBox.Show("Please Enter Valid Mobile Number!")
                TextBox3.Clear()
                TextBox3.Focus()
            End If
        Else
            MessageBox.Show("Please Enter Valid Email ID!")
            TextBox2.Clear()
            TextBox2.Focus()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (TextBox1.Text <> "" And TextBox16.Text <> "") Then

            Dim MysqlConn As New MySqlConnection(constr)
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "SELECT * FROM user WHERE user_id ='" & TextBox1.Text & "'"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                Dim count As Integer
                count = 0
                While READER.Read
                    count = count + 1
                End While
                If (count = 1) Then

                    Query = "DELETE FROM user WHERE user_id='" & TextBox1.Text & "' AND username = '" & TextBox16.Text & "' "
                    COMMAND = New MySqlCommand(Query, MysqlConn)
                    READER = COMMAND.ExecuteReader
                    MessageBox.Show("User Deleted sucessfully")
                    MysqlConn.Close()
                    Loadusers()
                    TextBox1.Text = ""
                    TextBox16.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                Else
                    MessageBox.Show("Record Not Found")
                End If
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
                TextBox1.Text = ""
                TextBox16.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
            Finally
                MysqlConn.Dispose()
            End Try
        Else
            MessageBox.Show("Please Enter All the Required Details")
            TextBox1.Text = ""
            TextBox16.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
        End If
    End Sub

    Private Sub DataGridView3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick, DataGridView3.CellClick
        Dim index As Integer
        index = e.RowIndex
        Dim selectedRow As DataGridViewRow
        selectedRow = DataGridView3.Rows(index)

        TextBox1.Text = selectedRow.Cells(0).Value.ToString()
        TextBox16.Text = selectedRow.Cells(1).Value.ToString()
        TextBox3.Text = selectedRow.Cells(2).Value.ToString()
        TextBox2.Text = selectedRow.Cells(3).Value.ToString()
    End Sub
End Class