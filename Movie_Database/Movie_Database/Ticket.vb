Imports Devart.Data.MySql
Imports System.Text.RegularExpressions

Public Class Ticket
    Dim constr = "Host=localhost;port=3306;user=root;password=tiger;database=movie"
    Dim COMMAND As MySqlCommand
    Dim READER, READER1 As MySqlDataReader
    Private Sub Ticket_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        show1()
        cate()
        Loadticket()
    End Sub
    Private Sub Loadticket()
        Dim MysqlConn As New MySqlConnection(constr)
        Try
            Dim table As New DataTable()
            Dim adapter As New MySqlDataAdapter("SELECT * FROM ticket", MysqlConn)
            adapter.Fill(table)
            DataGridView3.DataSource = table
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

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
    Private Sub show1()
        Dim MysqlConn As New MySqlConnection(constr)
        Dim adapter As New MySqlDataAdapter()
        Dim table As New DataTable()
        Dim ds As New DataSet()
        Dim Sql As String = "SELECT show_name FROM shows"
        Try
            MysqlConn.Open()
            COMMAND = New MySqlCommand(Sql, MysqlConn)
            adapter.SelectCommand = COMMAND
            adapter.Fill(ds)
            adapter.Dispose()
            COMMAND.Dispose()
            ComboBox2.DataSource = ds.Tables(0)
            ComboBox2.ValueMember = "show_name"
            ComboBox2.DisplayMember = "show_name"
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show("Can not open connection ! ")
        End Try
    End Sub
    Private Sub cate()
        Dim MysqlConn As New MySqlConnection(constr)
        Dim adapter As New MySqlDataAdapter()
        Dim table As New DataTable()
        Dim ds As New DataSet()
        Dim Sql As String = "SELECT category_name FROM category"
        Try
            MysqlConn.Open()
            COMMAND = New MySqlCommand(Sql, MysqlConn)
            adapter.SelectCommand = COMMAND
            adapter.Fill(ds)
            adapter.Dispose()
            COMMAND.Dispose()
            ComboBox1.DataSource = ds.Tables(0)
            ComboBox1.ValueMember = "category_name"
            ComboBox1.DisplayMember = "category_name"
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show("Can not open connection ! ")
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim MysqlConn As New MySqlConnection(constr)
        Try
            MysqlConn.Open()
            Dim query As String
            query = "SELECT * FROM shows WHERE show_name='" & ComboBox2.Text & "'"
            COMMAND = New MySqlCommand(query, MysqlConn)
            READER = COMMAND.ExecuteReader
            While READER.Read
                Dim date1, time, movie, price As String
                date1 = READER.GetString("date")
                time = READER.GetString("time")
                movie = READER.GetString("movie")
                price = READER.GetString("price")
                TextBox2.Text = time
                TextBox1.Text = date1
                TextBox16.Text = movie
                TextBox5.Text = price
            End While
            MysqlConn.Close()
        Catch ex As MySqlException
            MsgBox(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim per, price As Integer
        per = Convert.ToInt16(TextBox4.Text)
        price = Convert.ToInt16(TextBox5.Text)
        TextBox5.Text = per * price
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If IsPhoneNumberValid(TextBox3.Text) Then
            Dim MysqlConn As New MySqlConnection(constr)
            Try
                MysqlConn.Open()
                Dim Query, Query1, Query2 As String
                Dim temp, temp1 As Integer

                Query = "insert into ticket(show_name,c_name,c_mobile,category,no_of_seats) values ('" & ComboBox2.Text & "','" & TextBox6.Text & "','" & TextBox3.Text & "','" & ComboBox1.Text & "','" & TextBox4.Text & "')"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                Query1 = "SELECT * FROM category WHERE category_name ='" & ComboBox1.Text & "'"
                COMMAND = New MySqlCommand(Query1, MysqlConn)
                READER = COMMAND.ExecuteReader
                While READER.Read
                    temp1 = READER.GetString("no_of_seats")
                    temp = Convert.ToInt16(temp1) - Convert.ToInt16(TextBox4.Text)
                End While
               
                Query2 = "Update category SET no_of_seats ='" & temp & "' WHERE category_name ='" & ComboBox1.Text & "'"
                COMMAND = New MySqlCommand(Query2, MysqlConn)
                READER1 = COMMAND.ExecuteReader

                MessageBox.Show("Ticket Booked sucessfully")
                MysqlConn.Close()
                Loadticket()
                Seats.Show()
            Catch ex As MySqlException
                MessageBox.Show("Something Went Wrong Please Check the Details And Check Again Later!")
                TextBox1.Text = ""
                TextBox16.Text = ""
                TextBox2.Text = ""
            Finally
                MysqlConn.Dispose()
            End Try
        Else
            MessageBox.Show("Please Enter Valid Mobile Number!")
            TextBox3.Clear()
            TextBox3.Focus()
        End If
    End Sub
End Class